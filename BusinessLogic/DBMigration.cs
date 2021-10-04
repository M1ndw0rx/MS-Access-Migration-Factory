using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.ServiceModel.Channels;
using System.Text;
using System.Xml.Linq;

namespace AppFactory.BusinessLogic
{
    public class DBMigrationLogEventArgs : EventArgs
    {
        private string message;

        public string Message
        {
            get { return message; }
            set { message = value; }
        }
        public DBMigrationLogEventArgs(string s)
        {
            message = s;
        }
    }
    public class DBMigration
    {
        // most important class in the project
        string _accessdb;
        List<D365Entity> _solutionentities;    // containts all entities to be migrated

        // deprecated
        List<D365Solution> _solutions;         // contains all solutions for a given D365 Org
        List<D365Publisher> _publisher;        // contains the list of the publisher for a given D365 Org
        // deprecated

        D365Solution _selectedsolution;

        #region public properties

        public int LanguageCode = 1031;

        public List<D365Publisher> OrgPublisher
        {
            get => _publisher;
            set
            {
                _publisher = value;

            }
        }

        public List<D365Solution> OrgSolutions
        {
            get => _solutions;
            set => _solutions = value;
        }

        public DBMigration()
        {
            _solutionentities = new List<D365Entity>();
            _solutions = new List<D365Solution>();
            _publisher = new List<D365Publisher>();
        }

        public string AccessDB
        {
            get => _accessdb;
            set
            {
                _accessdb = value;
            }
        }

        public D365Solution Solution
        {
            get => _selectedsolution;
            set
            {
                _selectedsolution = value;
                Console.WriteLine("solution set: " + value.Schemaname);
            }
        }


        private XNamespace mcwb = "clr -namespace:Microsoft.Crm.Workflow.BusinessProcessFlowActivities;assembly=Microsoft.Crm.Workflow, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35";
        private XNamespace mcwc = "clr-namespace:Microsoft.Crm.Workflow.ClientActivities;assembly=Microsoft.Crm.Workflow, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35";
        private XNamespace mcwo = "clr-namespace:Microsoft.Crm.Workflow.ObjectModel;assembly=Microsoft.Crm, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35";
        private XNamespace mva = "clr -namespace:Microsoft.VisualBasic.Activities;assembly=System.Activities, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35";
        private XNamespace mxs = "clr-namespace:Microsoft.Xrm.Sdk;assembly=Microsoft.Xrm.Sdk, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35";
        private XNamespace mxsq = "clr-namespace:Microsoft.Xrm.Sdk.Query;assembly=Microsoft.Xrm.Sdk, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35";
        private XNamespace mxswa = "clr-namespace:Microsoft.Xrm.Sdk.Workflow.Activities;assembly=Microsoft.Xrm.Sdk.Workflow, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35";
        private XNamespace s = "clr-namespace:System;assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
        private XNamespace scg = "clr-namespace:System.Collections.Generic;assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
        private XNamespace sco = "clr-namespace:System.Collections.ObjectModel;assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
        private XNamespace srs = "clr-namespace:System.Runtime.Serialization;assembly=System.Runtime.Serialization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
        private XNamespace thas = "clr-namespace:";
        private XNamespace x = "http://schemas.microsoft.com/winfx/2006/xaml";

        public List<D365Entity> SolutionEntities
        {
            get => _solutionentities;
            set
            {
                _solutionentities = value;
            }
        }
        #endregion

        /// <summary>
        /// adds a new Publisher to the Data Model, but not to the environment yet
        /// </summary>
        public void AddPublisher(string displayname, string schemaname, string prefix)
        {
            D365Publisher p = new D365Publisher
            {
                Displayname = displayname,
                Schemaname = schemaname,
                Prefix = prefix
            };
            this.OrgPublisher.Add(p);
        }

        /// <summary>
        /// adds a new Solution to the Data Model, but not to the environment yet
        /// </summary>
        public void addSolution(string displayname, string schemaname, string solutionversion, string publishername)
        {
            D365Solution s = new D365Solution
            {
                Displayname = displayname,
                Schemaname = schemaname,
                Version = solutionversion,
                Publisher = OrgPublisher.FirstOrDefault(pu => pu.Displayname.Equals(publishername))
            };

            OrgSolutions.Add(s);
        }

        #region private MS Access Examiniation Methods

        public void ExamineAccessDB()
        {
            var con = String.Format("Provider=Microsoft.ACE.OLEDB.12.0; Data Source={0}", _accessdb);

            var oledbconn = new OleDbConnection(con);
            oledbconn.Open();
            var dt = new DataTable();
            dt = oledbconn.GetSchema("tables");

            // get all tables from MS Access

            foreach (DataRow row in dt.Rows)
            {
                // select only custom tables --> exclude system tables
                if (row["TABLE_TYPE"].ToString() == "TABLE")
                {
                    string tablename = row["TABLE_NAME"].ToString();
                    string tableschemaname = tablename.Replace(" ", string.Empty).ToLower();

                    // create a new D365 Entity in Memory with the Properties of this MS Access Table
                    D365Entity entity = new D365Entity(
                        true,               // migrate this table? 
                        tablename,          // MS Access Table Name
                        tableschemaname,    // Schemaname
                        tablename,          // Singularname
                        tablename,          // Pluralname
                        false,              // Has Activities? 
                        false);             // Has Notes? 

                    // get all attributes from this table
                    object[] restrictions = { null, null, tablename, null };

                    OleDbCommand cmd = new OleDbCommand(tablename, oledbconn);
                    cmd.CommandType = CommandType.TableDirect;
                    OleDbDataReader reader = cmd.ExecuteReader(CommandBehavior.SchemaOnly);
                    DataTable sch = reader.GetSchemaTable();

                    #region create attributes for entity
                    D365AttributeFactory factory = null;
                    foreach (DataRow dr in sch.Rows)
                    {

                        factory = new D365ConcreteAttributeFactory(dr);
                        iD365Attribute att = factory.CreateAttribute();

                        // att should not be null, otherwise there is a missing datatype 
                        if (att != null)
                        {
                            // get primary key for this table
                            DataTable mySchema = (oledbconn as OleDbConnection).
                                GetOleDbSchemaTable(OleDbSchemaGuid.Primary_Keys, new Object[] { null, null, tablename });

                            int pk_ordinal = mySchema.Columns["COLUMN_NAME"].Ordinal;

                            foreach (DataRow r in mySchema.Rows)
                            {
                                if (att.AccessFieldname == r.ItemArray[pk_ordinal].ToString())
                                    att.AccessPrimaryKey = true;

                            };

                            // check foreign key 
                            string[] fkRestrictions = new string[] { null, null, null, null, null, tablename };
                            DataTable dtForeignKeys = (oledbconn as OleDbConnection).
                                GetOleDbSchemaTable(OleDbSchemaGuid.Foreign_Keys, fkRestrictions);

                            foreach (DataRow fk_row in dtForeignKeys.Rows)
                            {
                                if (att.AccessFieldname == fk_row[9].ToString())
                                {
                                    att.AccessForeignKey = true;

                                    string pktablename = (string)fk_row["PK_TABLE_NAME"];
                                    entity.ForeignKeys.Add(new D365Entity.ForeignKey
                                    {
                                        fk_name = (string)fk_row["FK_NAME"],
                                        pk_table_name = pktablename,
                                        fk_column_name = (string)fk_row["FK_COLUMN_NAME"],
                                        pk_column_name = (string)fk_row["PK_COLUMN_NAME"],
                                        D365LookupSchemaname = pktablename.Replace(" ", string.Empty).ToLower()
                                    });
                                }
                            }
                             entity.Attributes.Add(att);
                        }
                    }
                    #endregion

                    this.SolutionEntities.Add(entity);
                }
            }

            oledbconn.Close();
            oledbconn.Dispose();
        }

        //private ArrayList getForeignKeys(string tableName, OleDbConnection conn)
        //{
        //    ArrayList _foreignkeys = new ArrayList();

        //    string[] fkRestrictions = new string[] { null, null, null, null, null, tableName };
        //    DataTable dtForeignKeys = (conn as OleDbConnection).
        //        GetOleDbSchemaTable(OleDbSchemaGuid.Foreign_Keys, fkRestrictions);

        //    int columnOrdinalForName = dtForeignKeys.Columns["FK_NAME"].Ordinal;

        //    foreach (DataRow row in dtForeignKeys.Rows)
        //    {
        //        string pktablename = (string)row["PK_TABLE_NAME"];
        //        _foreignkeys.Add(new D365Entity.ForeignKey
        //        {
        //            fk_name = (string)row["FK_NAME"],
        //            pk_table_name = pktablename,
        //            fk_column_name = (string)row["FK_COLUMN_NAME"],
        //            pk_column_name = (string)row["PK_COLUMN_NAME"],
        //            D365LookupSchemaname = SolutionEntities.FirstOrDefault(e => e.Displayname.Equals(pktablename)).Schemaname
        //        });
        //    }

        //    return _foreignkeys;
        //}

        #endregion

        #region Data Import Stuff

        public string getTableData(string tablename) 
        {
            string tabledata = "";
            // iterate through all fields and create select statement
            string sql = "";
            foreach(iD365Attribute att in SolutionEntities.FirstOrDefault(e => e.AccessDB_Tablename.Equals(tablename)).Attributes)
            {
                sql += att.AccessFieldname + ","; 
            }
            tabledata = sql.Substring(0, sql.Length - 1);
            sql = "SELECT " + tabledata + " FROM " + tablename;
            var con = String.Format("Provider=Microsoft.ACE.OLEDB.12.0; Data Source={0}", _accessdb);
            var o = new OleDbConnection(con);
            var cmd = new OleDbCommand(sql, o);
            o.Open();
            OleDbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            string currentrow = "";
            while (reader.Read())
            {
                currentrow = "";
                for(int i=0; i<reader.FieldCount; i++)
                {
                    switch (reader.GetDataTypeName(i))
                    {
                        case "DBTYPE_I4":           // integer
                            currentrow += "'" + reader.GetInt32(i).ToString() + "',";
                            break;
                        case "DBTYPE_WVARCHAR":      //string
                            currentrow += "'" + reader.GetString(i) + "',";
                            break;
                        default:
                            currentrow += "'<<datatype not implemented>>',";
                            break;
                    }
                    Debug.Print(reader.GetDataTypeName(i));
                    //Debug.Print(reader.GetString(1));
                    //currentrow += reader.GetString(i) + ",";
                }
                tabledata += "\n" + currentrow.Substring(0, currentrow.Length - 1); 
            };

            reader.Close();
            return tabledata; 
        }



        #endregion

        #region BPF Stuff

        private XDocument createBasicBPF()
        {
            #region Create Header Document
            //XNamespace mcwb = "clr -namespace:Microsoft.Crm.Workflow.BusinessProcessFlowActivities;assembly=Microsoft.Crm.Workflow, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35";
            //XNamespace mcwc = "clr-namespace:Microsoft.Crm.Workflow.ClientActivities;assembly=Microsoft.Crm.Workflow, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35";
            //XNamespace mcwo = "clr-namespace:Microsoft.Crm.Workflow.ObjectModel;assembly=Microsoft.Crm, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"; 
            //XNamespace mva = "clr -namespace:Microsoft.VisualBasic.Activities;assembly=System.Activities, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35";
            //XNamespace mxs = "clr-namespace:Microsoft.Xrm.Sdk;assembly=Microsoft.Xrm.Sdk, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35";
            //XNamespace mxsq = "clr-namespace:Microsoft.Xrm.Sdk.Query;assembly=Microsoft.Xrm.Sdk, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35";
            //XNamespace mxswa = "clr-namespace:Microsoft.Xrm.Sdk.Workflow.Activities;assembly=Microsoft.Xrm.Sdk.Workflow, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35";
            //XNamespace s = "clr-namespace:System;assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
            //XNamespace scg = "clr-namespace:System.Collections.Generic;assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
            //XNamespace sco = "clr-namespace:System.Collections.ObjectModel;assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
            //XNamespace srs = "clr-namespace:System.Runtime.Serialization;assembly=System.Runtime.Serialization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"; 
            //XNamespace thas = "clr-namespace:";
            //XNamespace x = "http://schemas.microsoft.com/winfx/2006/xaml"; 

            XElement root = new XElement("Activity",
                new XAttribute(XNamespace.Xmlns + "mcwb", mcwb.NamespaceName),
                new XAttribute(XNamespace.Xmlns + "mcwc", mcwc.NamespaceName),
                new XAttribute(XNamespace.Xmlns + "mcwo", mcwo.NamespaceName),
                new XAttribute(XNamespace.Xmlns + "mva", mva.NamespaceName),
                new XAttribute(XNamespace.Xmlns + "mxs", mxs.NamespaceName),
                new XAttribute(XNamespace.Xmlns + "mxsq", mxsq.NamespaceName),
                new XAttribute(XNamespace.Xmlns + "s", s.NamespaceName),
                new XAttribute(XNamespace.Xmlns + "scg", scg.NamespaceName),
                new XAttribute(XNamespace.Xmlns + "sco", sco.NamespaceName),
                new XAttribute(XNamespace.Xmlns + "srs", srs.NamespaceName),
                new XAttribute(XNamespace.Xmlns + "thas", thas.NamespaceName),
                new XAttribute(XNamespace.Xmlns + "x", x.NamespaceName),

                new XAttribute(x + "Class", "SampleWF")
            );

            XElement members = new XElement(x + "Members",
                new XElement(x + "Property",
                    new XAttribute("Name", "InputEntities"),
                    new XAttribute("Type", "InArgument(scg:IDictionary(x:String, mxs:Entity))")
                ),
                new XElement(x + "Property",
                    new XAttribute("Name", "CreatedEntities"),
                    new XAttribute("Type", "InArgument(scg:IDictionary(x:String, mxs:Entity))")
                )
            );

            root.Add(members);

            /*---Add Node InputEntities to Document----------------------------*/

            XElement InputEntities = new XElement(thas + "SampleWF.InputEntities",
                new XElement("InArgument",
                    new XAttribute(x + "TypeArguments", "scg:IDictionary(x:String, mxs:Entity)")
                )
            );
            root.Add(InputEntities);

            /*---Add Node CreatedEntities to Document----------------------------*/
            XElement CreatedEntities = new XElement(thas + "SampleWF.CreatedEntities",
                new XElement("InArgument",
                    new XAttribute(x + "TypeArguments", "scg:IDictionary(x:String, mxs:Entity)")
                )
            );
            root.Add(CreatedEntities);

            /*---Add Node VBSettings to Document----------------------------*/
            XElement VBSettings = new XElement(
                mva + "VisualBasic.Settings",
                "Assembly references and imported namespaces for internal implementation");

            root.Add(VBSettings);
            #endregion

            /*---Add Node VBSettings to Document----------------------------*/
            XElement wf = new XElement(mxswa + "Workflow");

            /*---Add Node for the start of the BPF----------------------------*/

            XElement startactivity = new XElement(mxswa + "ActivityReference",
                new XAttribute("AssemblyQualifiedName", "Microsoft.Crm.Workflow.BusinessProcessFlowActivities.StageRelationshipCollectionComposite, Microsoft.Crm.Workflow, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"),
                new XAttribute("DisplayName", "RelationshipCollectionStep2")
                );

            startactivity.Add(ActivityReferenceProps());

            wf.Add(startactivity);

            /*---Add Node for activity 2 of the BPF----------------------------*/
            XElement activity2 = new XElement(mxswa + "ActivityReference",
                new XAttribute("AssemblyQualifiedName", "Microsoft.Crm.Workflow.Activities.EntityComposite, Microsoft.Crm.Workflow, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"),
                 new XAttribute("DisplayName", "EntityStep2: account")
            );
            activity2.Add(ActivityReferenceProps());

            activity2.Descendants(sco + "Collection").Last().Add(createStageComposite());


            activity2.Element(mxswa + "ActivityReference.Properties").Add(
                new XElement(x + "Null",
                    new XAttribute(x + "Key", "RelationshipName")
                ),
                new XElement(x + "Null",
                    new XAttribute(x + "Key", "AttributeName")
                ),
                new XElement(x + "Boolean",
                    new XAttribute(x + "Key", "IsClosedLoop"),
                    "False"
                )
            );


            wf.Add(activity2);

            root.Add(wf);

            XDocument bpf_xaml = new XDocument(new XDeclaration("1.0", "utf-16", ""));
            bpf_xaml.Add(root);

            return bpf_xaml;
        }

        private XElement createStageComposite()
        {
            Guid newstageid = new Guid();

            XElement e = new XElement(mxswa + "ActivityReference",
                new XAttribute("AssemblyQualifiedName", "Microsoft.Crm.Workflow.Activities.StageComposite, Microsoft.Crm.Workflow, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"),
                new XAttribute("DisplayName", "StageStep3: erste Phase")
            );

            e.Add(ActivityReferenceProps());

            // add the necessary steps (fields) for this stage
            e.Descendants(sco + "Collection").Last().Add(createStepComposite());

            // add additional fields here to the ActivityReference.Properties section
            XElement props = (XElement)
                (from el in e.Descendants(mxswa + "ActivityReference.Properties")
                 select el).Last();

            props.Add(new XElement(sco + "Collection",
                new XAttribute(x + "TypeArguments", "mcwo:StepLabel"),
                new XAttribute(x + "Key", "StepLabels"),
                new XElement(mcwo + "StepLabel",
                    new XAttribute("Description", "Erste Phase"),
                    new XAttribute("LabelId", "abb840ed-490a-47b2-ae7f-3bb220289555"),
                    new XAttribute("LanguageCode", "1031")
                )));
            props.Add(
                new XElement(x + "String",
                    new XAttribute(x + "Key", "StageId"),
                    newstageid.ToString()
                //"abb840ed-490a-47b2-ae7f-3bb220289555"
                ),
                new XElement(x + "String",
                    new XAttribute(x + "Key", "StageCategory"),
                    "-1"
                ),
                //new XElement(x + "String",
                //    new XAttribute(x + "Key", "NextStageId"),
                //    "57e99e70-cda7-444c-a73d-7829ebc012b8"
                //)
                new XElement(x + "Null",
                    new XAttribute(x + "Key", "NextStageId"),
                    ""
                )
            );

            return e;
        }

        private XElement createStepComposite()
        {
            XElement e = new XElement(mxswa + "ActivityReference",
                new XAttribute("AssemblyQualifiedName", "Microsoft.Crm.Workflow.Activities.StepComposite, Microsoft.Crm.Workflow, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"),
                new XAttribute("DisplayName", "StepStep4: Neuer Schritt")
            ,
            new XElement(mxswa + "ActivityReference.Properties",
                new XElement(sco + "Collection",
                    new XAttribute(x + "TypeArguments", "Variable"),
                    new XAttribute(x + "Key", "Activities"),
                    new XElement("Sequence",
                        new XAttribute("DisplayName", "ControlStep5"),
                        new XElement(mcwb + "Control",
                            new XAttribute("ClassId", "4273edbd-ac1d-40d3-9fb2-095c621b552d"),
                            new XAttribute("ControlDisplayName", "Adresse 1: Ort"),
                            new XAttribute("ControlId", "address1_city"),
                            new XAttribute("DataFieldName", "address1_city"),
                            new XAttribute("IsSystemControl", "False"),
                            new XAttribute("IsUnbound", "False"),
                            new XAttribute("SystemStepType", "0"),
                            new XElement(mcwb + "Control.Parameters",
                                new XElement("InArgument",
                                    new XAttribute(x + "TypeArguments", "x:String"),
                                    new XElement("Literal",
                                        new XAttribute(x + "TypeArguments", "x:String"),
                                        new XAttribute("Value", "")
                                    )
                                )
                           )
                        )
                    )
                ),
                new XElement(sco + "Collection",
                    new XAttribute(x + "TypeArguments", "mcwo:StepLabel"),
                    new XAttribute(x + "Key", "StepLabels"),
                    new XElement(mcwo + "StepLabel",
                        new XAttribute("Description", "erster Schritt"),
                        new XAttribute("LabelId", "d60cd448-06e9-4c41-a5e3-11aa50433c25"),
                        new XAttribute("LanguageCode", "1031")
                    ),
                    new XElement(x + "String",
                        new XAttribute(x + "Key", "ProcessStepId"),
                        "d60cd448-06e9-4c41-a5e3-11aa50433c25"),
                    new XElement(x + "Boolean",
                        new XAttribute(x + "Key", "IsProcessRequired"),
                        "True")
                ),
                new XElement(sco + "Collection",
                    new XAttribute(x + "TypeArguments", "mcwo:StepLabel"),
                    new XAttribute(x + "Key", "StepLabels"),
                    new XElement(mcwo + "StepLabel",
                        new XAttribute("Description", "Erste Phase"),
                        new XAttribute("LabelId", "abb840ed-490a-47b2-ae7f-3bb220289555"),
                        new XAttribute("LanguageCode", "1031")
                    )
                )
             )
            );

            return e;
        }

        private XElement ActivityReferenceProps()
        {
            XElement e = new XElement(mxswa + "ActivityReference.Properties",
                    new XElement(sco + "Collection",
                        new XAttribute(x + "TypeArguments", "Variable"),
                        new XAttribute(x + "Key", "Variables")
                    ),
                    new XElement(sco + "Collection",
                        new XAttribute(x + "TypeArguments", "Activity"),
                        new XAttribute(x + "Key", "Activities")
                    )
                );
            return e;
        }

        //public void createBPF()
        //{
        //    QueryExpression query = new QueryExpression
        //    {
        //        ColumnSet = new ColumnSet(true),
        //        EntityName = "workflow"
        //    };

        //    query.Criteria = new FilterExpression();
        //    query.Criteria.AddCondition("name", ConditionOperator.Equal, "TestBPF4");

        //    EntityCollection results = D365Service.RetrieveMultiple(query);



        //    #region add Namespaces

        //    //string xmlFilePath = @"C:\Users\petkra\Source\AppFactoryUI\SampleWF\sample-generated.xaml";
        //    string xmlFilePath = @"C:\Users\petkra\Source\AppFactoryUI\SampleWF\sample-wf1.xaml";

        //    //XDocument bpf_xaml = createBasicBPF(); 
        //    XDocument bpf_xaml = XDocument.Load(xmlFilePath); 

        //    #endregion

        //    // https://docs.microsoft.com/en-us/dynamics365/customer-engagement/web-api/workflow?view=dynamics-ce-odata-9

        //    Workflow w = new Workflow
        //    {
        //        BusinessProcessType = new OptionSetValue(0),        // Business Flow
        //        Type = new OptionSetValue(1),
        //        Category = new OptionSetValue(4),                   // Business Process Flow
        //        Name = "test",                                      // Name of the Process 
        //        Scope = new OptionSetValue(4),
        //        PrimaryEntity = "account",
        //        Xaml = bpf_xaml.ToString()
        //    };

        //    D365Service.Create(w); 

        //}

        #endregion
    }
}
