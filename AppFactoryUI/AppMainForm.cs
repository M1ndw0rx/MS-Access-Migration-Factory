using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.OleDb;

using Microsoft.Xrm.Tooling.Connector;
using Microsoft.Xrm.Tooling.CrmConnectControl;

using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Metadata; 
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Client; 
using Microsoft.Crm.Sdk.Messages;



using System.Windows.Input;
using System.Windows.Documents;
using System.Runtime.InteropServices;
using System.CodeDom;
using System.Data.Common;
using System.Xml;
using AppFactory.BusinessLogic;
using System.Diagnostics;
using System.Collections;
using System.Xml.Linq;
using System.Activities.Expressions;
using System.ServiceModel.Configuration;
using Label = Microsoft.Xrm.Sdk.Label;
using System.Windows.Controls;
using ComboBox = System.Windows.Forms.ComboBox;
using PowerApps.Samples.LoginUX;
//using System.Windows;


namespace AppFactory.UI
{
    public partial class AppMainForm : Form
    {
        //private delegate void SafeCallDelegateMsgStart(string text);
        //private delegate void SafeCallDelegateMsgEnd(bool status);

        // this is the connection to D365 environment
        //CRMLoginForm1 _ctrl = null;
        ExampleLoginForm _ctrl = null;

        // reference the global DB Migration object
        DBMigration dbmigration;

        public int LanguageCode = 1031;

        // internal lists 
        List<D365Solution> _solutions;         // contains all solutions for a given D365 Org
        List<D365Publisher> _publisher;        // contains the list of the publisher for a given D365 Org

        Stack operations;                       // contains all operations in sequence performed with the platform

        string currenttable; 

        private class D365Operation
        {
            public string operation;
            public Guid id;
        }

        public AppMainForm()
        {
            InitializeComponent();

            operations = new Stack();
            currenttable = null; 

            dbmigration = new DBMigration();

            _solutions = new List<D365Solution>();
            _publisher = new List<D365Publisher>();

            groupBox3.Enabled = false;
            groupBox4.Enabled = false;
            groupBox5.Enabled = false;
            groupBox6.Enabled = false;

            chkCreateSecurityRole.Enabled = false;
            chkCreateDataMigrationPackage.Enabled = false;

        }


        #region MS Access Examination
        private void btnExamine_Click(object sender, EventArgs e)
        {
            dbmigration.AccessDB = txtDatabase.Text;
            dbmigration.ExamineAccessDB();

            // load the tables from Solution Entities
            loadTableSourceFromEntities();


        }

        //private DataTable CreateTableSource()
        //{
        //    // create the datatable
        //    DataTable dt = new DataTable();

        //    // add the Columns to the table
        //    foreach (AccessTable a in MigrateTables)
        //    {
        //        dt.Columns.Add(new DataColumn
        //        {
        //            ColumnName = a.tablename,
        //            DataType = a.datatype
        //        });
        //    }
        //    return dt;
        //}

        private void loadTableSourceFromEntities()
        {
            //grdTables.Rows.Clear();

            foreach (D365Entity e in dbmigration.SolutionEntities)
            {
                grdTables.Rows.Add(
                    e.MigrateTable,
                    e.AccessDB_Tablename,
                    e.HasActivities,
                    e.HasNotes,
                    e.Schemaname,
                    e.Displayname,
                    e.Displayname_plural
                    );
            }


        }


        private void loadTableColumnsSource(string tablename)
        {

            grdRelations.Rows.Clear();
            grdTableColumns.Rows.Clear();
            if (tablename != null) {

                D365Entity e = dbmigration.SolutionEntities.FirstOrDefault(ent => ent.AccessDB_Tablename == tablename);

                foreach (iD365Attribute a in e.Attributes)
                {
                    grdTableColumns.Rows.Add(
                        a.MigrateAttribute,
                        a.AccessFieldname,
                        a.AccessDatatype,
                        a.AccessPrimaryKey,
                        a.AccessForeignKey,
                        a.Schemaname,
                        a.IsPrimaryAttribute
                    );
                };

                foreach(D365Entity.ForeignKey fk in e.ForeignKeys)
                {
                    grdRelations.Rows.Add(
                        fk.fk_name,
                        fk.pk_table_name,
                        fk.pk_column_name,
                        fk.fk_column_name
                        );
                };

            }



            
        }
        #endregion

        #region Startup Routines

        private void BtnConnectD365_Click(object sender, EventArgs e)
        {

            //_ctrl = new CRMLoginForm1();
            _ctrl = new ExampleLoginForm();
            _ctrl.ConnectionToCrmCompleted += ctrl_ConnectionToCrmCompleted;
            _ctrl.ShowDialog();

            // Handle the returned CRM connection object.  
            // On successful connection, display the CRM version and connected org name   
            if (_ctrl.CrmConnectionMgr != null && _ctrl.CrmConnectionMgr.CrmSvc != null && _ctrl.CrmConnectionMgr.CrmSvc.IsReady)
            {
                UseWaitCursor = true;

                lblD365Connection.Text = String.Format("Connected to D365 Version: {0} Org: {1}",
                   _ctrl.CrmConnectionMgr.CrmSvc.ConnectedOrgVersion.ToString(),
                   _ctrl.CrmConnectionMgr.CrmSvc.ConnectedOrgUniqueName);

                loadPublishersAndSolutions();
                setOrgParameter();

                groupBox3.Enabled = true;
                groupBox4.Enabled = true;
                groupBox5.Enabled = true;
                chkImportData.Enabled = true;
                groupBox6.Enabled = true;
                UseWaitCursor = false;
            }
            else
            {
                MessageBox.Show("Cannot connect; try again!", "Connection Status");
            }
        }

        private void setOrgParameter()
        {
            QueryExpression queryExpression = new QueryExpression("organization")
            {
                ColumnSet = new ColumnSet(true)
            };

            var settings = _ctrl.CrmConnectionMgr.CrmSvc.RetrieveMultiple(queryExpression);

            LanguageCode = (int)((Organization)settings.Entities[0]).LanguageCode;
            logText.AppendText(String.Format(Environment.NewLine + "retrieved settings for Organization '{0}'", ((Organization)settings.Entities[0]).Name));

        }

        private void populateD365Solutions()
        {
            cmbSolutions.Items.Clear();
            // load solutions into Combobox
            foreach (D365Solution v in _solutions)
            {
                cmbSolutions.Items.Add(v.Displayname);
            }
        }

        private void ctrl_ConnectionToCrmCompleted(object sender, EventArgs e)
        {

            if (sender is PowerApps.Samples.LoginUX.ExampleLoginForm)
                ((PowerApps.Samples.LoginUX.ExampleLoginForm)sender).Close();
        }
        #endregion

        #region Event Handler

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            ComboBox editingComboBox = (ComboBox)e.Control;
            if (editingComboBox != null)
                editingComboBox.SelectedIndexChanged += new System.EventHandler(this.editingComboBox_SelectedIndexChanged);
        }

        private void editingComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            ComboBox comboBox1 = (ComboBox)sender;
        }

        private void btnLookupDB_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                // set the text field to the selected file
                txtDatabase.Text = openFileDialog1.FileName;
            }
        }

        private void btnNewSolution_Click_1(object sender, EventArgs e)
        {
            frmSolution f_solution = new frmSolution(_publisher);
            DialogResult result = f_solution.ShowDialog();

            if (result == DialogResult.OK)
            {
                if (result == DialogResult.OK)
                {
                    string selected_sol = f_solution.Controls["txtSolutionDisplayName"].Text;
                    // select the new solution in Combo Box
                    cmbSolutions.SelectedText = selected_sol;

                    // replace the list of publishers to add a new one
                    this._publisher = f_solution.publisher;

                    // add new solution to the array of solutions
                    D365Solution s = new D365Solution
                    {
                        Displayname = f_solution.Controls["txtSolutionDisplayName"].Text,
                        Schemaname = f_solution.Controls["txtSolutionName"].Text,
                        Version = f_solution.Controls["txtSolutionVersion"].Text,
                        Publisher = _publisher.FirstOrDefault(p => p.Displayname == f_solution.Controls["cmbPublisher"].Text)
                    };

                    _solutions.Add(s);

                    populateD365Solutions();

                }
            }
        }
        private void cmbSolutions_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.ComboBox cbx = (System.Windows.Forms.ComboBox)sender;
            //MessageBox.Show(cbx.SelectedItem.ToString());

            for (int i = 0; i < _solutions.Count; i++)
            {
                if (_solutions[i].Displayname.Equals(cbx.SelectedItem.ToString()))
                {
                    lblCustomizationPrefix.Text = _solutions[i].Publisher.Prefix;

                    // set the selected solution in the data model 
                    dbmigration.Solution = _solutions[i];

                }
            }

        }

        private void cmbSolutions_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox cbx = (ComboBox)sender;
            for (int i = 0; i < _solutions.Count; i++)
            {
                if (_solutions[i].Displayname.Equals(cbx.SelectedItem.ToString()))
                {
                    lblCustomizationPrefix.Text = _solutions[i].Publisher.Prefix;

                    // set the selected solution in the data model 
                    dbmigration.Solution = _solutions[i];

                }
            }
        }

        private void txtAppName_Leave(object sender, EventArgs e)
        {
            txtAppSchemaname.Text = txtAppName.Text.Replace(" ", string.Empty).ToLower();
        }

        private void AppMainForm_Load(object sender, EventArgs e)
        {


        }

        private void grdTables_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Column Index for Table Structure 
            // migrateTable = 0
            // Activities Checkbox = 2
            // Notes Checkbox = 3

            if (grdTables.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn && e.RowIndex != -1)
            {
                // find out which cell has been clicked --> value is BEFORE click-event

                var value = grdTables.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                var entity = grdTables.Rows[e.RowIndex].Cells[1].Value;                 // this is the entity name

                D365Entity d365e = dbmigration.SolutionEntities.FirstOrDefault(ent => ent.Displayname == entity.ToString());

                switch (e.ColumnIndex)
                {
                    case 0:
                        d365e.MigrateTable = !Convert.ToBoolean(value);
                        break;
                    case 2:
                        d365e.HasActivities = !Convert.ToBoolean(value);
                        break;
                    case 3:
                        d365e.HasNotes = !Convert.ToBoolean(value);
                        break;
                }



            }


        }

        #endregion

        #region Background Worker

        private void BackgroundWorker_UndoMigration(object sender, DoWorkEventArgs e)
        {
            int totalsteps = operations.Count;
            int progress = 0; //Between 0-100

            ((BackgroundWorker)sender).ReportProgress(0);

            foreach (D365Operation op in operations)
            {
                progress++;
                switch (op.operation)
                {
                    case "app":
                        ((BackgroundWorker)sender).ReportProgress(progress * 100 / totalsteps, "deleting App Module " + op.id.ToString());
                        _ctrl.CrmConnectionMgr.CrmSvc.Delete("appmodule", op.id);
                        break;
                    case "sitemap":
                        ((BackgroundWorker)sender).ReportProgress(progress * 100 / totalsteps, "deleting Sitemap " + op.id.ToString());
                        _ctrl.CrmConnectionMgr.CrmSvc.Delete("sitemap", op.id);
                        break;
                    default:
                        DeleteEntityRequest request = new DeleteEntityRequest
                        {
                            LogicalName = op.operation
                        };                        
                        _ctrl.CrmConnectionMgr.CrmSvc.Execute(request);
                        //_ctrl.CrmConnectionMgr.CrmSvc.Delete(op.operation, op.id);
                        Debug.Print("{0} {1} deleted.", op.operation, op.id.ToString());
                        ((BackgroundWorker)sender).ReportProgress(progress * 100 / totalsteps, "deleting Entity " + op.id.ToString());
                        break;
                }
            }
        }

        private int countTotalSteps()
        {
            int steps = 0;

            steps += dbmigration.Solution.Publisher.PublisherId == new Guid("{00000000-0000-0000-0000-000000000000}") ? 1 : 0;
            steps += dbmigration.Solution.SolutionId == new Guid("{00000000-0000-0000-0000-000000000000}") ? 1 : 0;
            steps += dbmigration.SolutionEntities.Count;
            steps += chkCreateApp.Checked == true ? 1 : 0;
            steps += chkPublishApp.Checked == true ? 1 : 0;
            steps += chkImportData.Checked == true ? 1 : 0;

            return steps; 
        }

        /// <summary>
        /// Background Worker does the heavy lifting stuff in a specific order
        /// 1.  Check all assets
        /// 2.  create Publisher if necessary
        /// 3.  create Solution if necessary
        /// 4.  Iterate through all collected entities 
        /// 4.1 create all foreign key relations
        /// 5.  create the App if necesssary
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackgroundWorker_DoMigration(object sender, DoWorkEventArgs e)
        {
            int totalsteps = countTotalSteps();
                
            int progress = 0; //Between 0-100
            ((BackgroundWorker)sender).ReportProgress(0);

            // create publisher 
            
            //((BackgroundWorker)sender).ReportProgress(progress * 100 / totalsteps, "creating Publisher " + dbmigration.Solution.Publisher.Displayname);
            if (dbmigration.Solution.Publisher.PublisherId == new Guid("{00000000-0000-0000-0000-000000000000}"))
            {
                progress++;
                ((BackgroundWorker)sender).ReportProgress(progress * 100 / totalsteps, "created Publisher " + dbmigration.Solution.Publisher.PublisherId.ToString());
                logOperation("publisher", dbmigration.Solution.Publisher.PublisherId);
                dbmigration.Solution.Publisher.PublisherId = CreatePublisher();
            }

            // create solution 
            if (dbmigration.Solution.SolutionId == new Guid("{00000000-0000-0000-0000-000000000000}"))
            {
                progress++;
                ((BackgroundWorker)sender).ReportProgress(progress * 100 / totalsteps, "creating Solution " + dbmigration.Solution.SolutionId.ToString());
                logOperation("solution", dbmigration.Solution.SolutionId);
                dbmigration.Solution.SolutionId = CreateSolution();
            }

            //iterate through solution entities and create D365 entity
            foreach (D365Entity entity in dbmigration.SolutionEntities)
            {
                progress++;
                ((BackgroundWorker)sender).ReportProgress(progress * 100 / totalsteps, "creating Entity " + entity.Displayname);
                
                if (entity.MigrateTable)
                {
                    entity.EntityId = CreateCustomEntity(entity);
                    logOperation(dbmigration.Solution.Publisher.Prefix + "_" + entity.Schemaname, entity.EntityId);
                    //iterate through every attribute in the entity and create 
                    CreateCustomAttributes(entity);
                }
            }

            // iterate again through the entities and create foreign key relations
            foreach (D365Entity entity in dbmigration.SolutionEntities)
            {
                progress++;
                ((BackgroundWorker)sender).ReportProgress(progress * 100 / totalsteps, "creating Relations for " + entity.Displayname);
                if (entity.MigrateTable && entity.ForeignKeys.Count > 0)
                {
                    CreateRelation(entity);
                }
            }
            
            if (chkCreateApp.Checked)
            {
                progress++;
                ((BackgroundWorker)sender).ReportProgress(progress * 100 / totalsteps, "creating App " + dbmigration.Solution.D365App.Displayname);
                dbmigration.Solution.D365App.AppId = createD365App();

                createAppReferences();

                // Validate app 
                bool val = validateD365App();

                logOperation("app", dbmigration.Solution.D365App.AppId);
            }

            if (chkPublishApp.Checked)
            {
                progress++;
                ((BackgroundWorker)sender).ReportProgress(progress * 100 / totalsteps, "publishing Customizations ");
                // publish all customizations
                publishCustomizations(); 
                
            }

            // import data ?? 
            if (chkImportData.Checked)
            {
                progress++;
                ((BackgroundWorker)sender).ReportProgress((2 + (++progress)) * 100 / totalsteps, "importing Data");
                ImportData(); 
            }
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            logText.AppendText(Environment.NewLine + "Operation successful.");
            MessageBox.Show("Operation erfolgreich beendet!", "MS Access Migration", MessageBoxButtons.OK, MessageBoxIcon.Information);
            toggleForm(true);
        }

        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState != null)
            {
                toolStripStatusLabel.Text = e.UserState.ToString();
                logText.AppendText(String.Format(Environment.NewLine + e.UserState.ToString()));

            }
            if(e.ProgressPercentage <= 100)
                toolStripProgressBar1.Value = e.ProgressPercentage;
        }
        #endregion

        #region Migration Area


        private string CheckMigration()
        {

            toolStripStatusLabel.Text = "Checking Migration...";

            //is a solution connected?
            if (dbmigration.Solution is null)
            {
                return "Keine Solution ausgewählt...";
            }

            // is Publisher valid? 
            if (dbmigration.Solution.Publisher.PublisherId == new Guid("{00000000-0000-0000-0000-000000000000}") &&
                PublisherExists())
            {
                return "Der Herausgeber existiert bereits in der Umgebung...";
            }

            // check if we need to create an App
            if (chkCreateApp.Checked)
            {
                if (txtAppName.Text == "")
                {
                    return "Bitte einen Namen für die App vergeben...";
                }
                D365App a = new D365App
                {
                    Displayname = txtAppName.Text,
                    Schemaname = txtAppSchemaname.Text
                };

                dbmigration.Solution.D365App = a;

            }

            // check entities if they exist
            //SchemaName = dbmigration.Solution.Publisher.Prefix + "_" + entity.Schemaname,
            foreach (D365Entity e in dbmigration.SolutionEntities)
            {
                if (e.MigrateTable && EntityExists(dbmigration.Solution.Publisher.Prefix + "_" + e.Schemaname))
                {
                    return $"Die Entität  {dbmigration.Solution.Publisher.Prefix + "_" + e.Schemaname}  exisitiert schon...";
                }
            }

            return null;
        }

        private void BtnMigrate_Click(object sender, EventArgs e)
        {
            toggleForm(false);
            tabControl1.SelectTab(1);
            string err = CheckMigration();
            if (err != null)
            {
                MessageBox.Show("Prüfung fehlgeschlagen!" + err, "MS Access Migration", MessageBoxButtons.OK, MessageBoxIcon.Error);
                toggleForm(true);
                return;
            }
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += BackgroundWorker_DoMigration;
            worker.ProgressChanged += BackgroundWorker_ProgressChanged;
            worker.WorkerReportsProgress = true;
            worker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
            worker.RunWorkerAsync();

            //toggleForm(true);

        }
        private void btnUndoMigration_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult =
                MessageBox.Show("Do you really want to undo the migration?", "Undo Migration", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            Debug.Print("Anzahl Operationen" + operations.Count.ToString());
            toggleForm(false);
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += BackgroundWorker_UndoMigration;
            worker.ProgressChanged += BackgroundWorker_ProgressChanged;
            worker.WorkerReportsProgress = true;
            worker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
            worker.RunWorkerAsync();
            toggleForm(true);
        }

        private void toggleForm(bool status)
        {
            statusStrip1.Visible = !status;
            statusStrip1.UseWaitCursor = !status;
            toolStripProgressBar1.Visible = !status;

            btnConnectD365.Enabled = status;
            btnExamineDB.Enabled = status;
            btnLookupDB.Enabled = status;
            btnMigrate.Enabled = status;
            btnUndoMigration.Enabled = status;

        }
        #endregion

        #region D365 Stuff
        /// <summary>
        /// logs a specific Platform Operation onto the stack
        /// </summary>
        /// <param name="operationname"></param>
        /// <param name="operationid"></param>
        private void logOperation(string operationname, Guid operationid)
        {
            operations.Push(new D365Operation
            {
                operation = operationname,
                id = operationid
            });
        }

        private bool EntityExists(string entity_name)
        {
            RetrieveEntityRequest re = new RetrieveEntityRequest
            {
                LogicalName = entity_name
            };
            try
            {
                RetrieveEntityResponse response = (RetrieveEntityResponse)_ctrl.CrmConnectionMgr.CrmSvc.Execute(re);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        private bool SolutionExists()
        {
            QueryExpression querySolution = new QueryExpression
            {
                EntityName = "solution",
                ColumnSet = new ColumnSet(),
                Criteria = new FilterExpression()
            };
            querySolution.Criteria.AddCondition("uniquename", ConditionOperator.Equal, dbmigration.Solution.Schemaname);
            EntityCollection querySolutionResults = _ctrl.CrmConnectionMgr.CrmSvc.RetrieveMultiple(querySolution);

            return querySolutionResults.Entities.Count > 0 ? true : false;

        }

        private bool PublisherExists()
        {
            QueryExpression queryPublisher = new QueryExpression
            {
                EntityName = "publisher",
                ColumnSet = new ColumnSet("publisherid", "customizationprefix"),
                Criteria = new FilterExpression()
            };
            queryPublisher.Criteria.AddCondition("uniquename", ConditionOperator.Equal, dbmigration.Solution.Publisher.Schemaname);
            EntityCollection queryPublisherResults = _ctrl.CrmConnectionMgr.CrmSvc.RetrieveMultiple(queryPublisher);

            return queryPublisherResults.Entities.Count > 0 ? true : false;

        }

        private void loadPublishersAndSolutions()
        {
            logText.AppendText("loading Publishers and Solutions...");
            // load Publishers from Environment 
            EntityCollection p = _ctrl.CrmConnectionMgr.CrmSvc.RetrieveMultiple(new QueryExpression("publisher")
            {
                ColumnSet = new ColumnSet(true)
            });
            if (p != null)
            {
                _publisher.AddRange(
                    from Entity p_e in p.Entities
                    select new D365Publisher
                    {
                        Displayname = p_e.Attributes["friendlyname"].ToString(),
                        Schemaname = p_e.Attributes["uniquename"].ToString(),
                        Prefix = p_e.Attributes["customizationprefix"].ToString(),
                        PublisherId = new Guid(p_e.Attributes["publisherid"].ToString())
                    });
            }
            logText.AppendText(String.Format(Environment.NewLine + "found {0} Publishers..", _publisher.Count));

            // load Solutions from Environment
            EntityCollection s = _ctrl.CrmConnectionMgr.CrmSvc.RetrieveMultiple(new QueryExpression("solution")
            {
                ColumnSet = new ColumnSet(true)
            });

            if (s != null)
            {
                _solutions.AddRange(
                    from Entity s_e in s.Entities
                    where Convert.ToBoolean(s_e.Attributes["isvisible"]).Equals(true)
                    select new D365Solution
                    {
                        Displayname = s_e.Attributes["friendlyname"].ToString(),
                        Schemaname = s_e.Attributes["uniquename"].ToString(),
                        SolutionId = new Guid(s_e.Attributes["solutionid"].ToString()),
                        Publisher = _publisher.FirstOrDefault(pu => pu.PublisherId == ((EntityReference)s_e.Attributes["publisherid"]).Id)
                    });

                populateD365Solutions();
            }
            logText.AppendText(String.Format(Environment.NewLine + "found {0} Solutions..", _solutions.Count));
        }

        private Guid CreatePublisher()
        {
            //logText.AppendText(String.Format(Environment.NewLine + "creating Publisher '{0}'", dbmigration.Solution.Publisher.Displayname));
            Publisher _crmSdkPublisher = new Publisher
            {
                UniqueName = dbmigration.Solution.Publisher.Schemaname,
                FriendlyName = dbmigration.Solution.Publisher.Displayname,
                //SupportingWebsiteUrl = "https://msdn.microsoft.com/dynamics/crm/default.aspx",
                CustomizationPrefix = dbmigration.Solution.Publisher.Prefix
                //EMailAddress = "someone@microsoft.com",
                //Description = "This publisher was created with samples from the Microsoft Dynamics CRM SDK"
            };

            Guid _publisherid = _ctrl.CrmConnectionMgr.CrmSvc.Create(_crmSdkPublisher);



            return _publisherid;
        }

        private Guid CreateSolution()
        {
            //logText.AppendText(String.Format(Environment.NewLine + "creating Solution '{0}'", dbmigration.Solution.Displayname));
            Solution newsolution = new Solution
            {
                PublisherId = new EntityReference("publisher", dbmigration.Solution.Publisher.PublisherId),
                FriendlyName = dbmigration.Solution.Displayname,
                UniqueName = dbmigration.Solution.Schemaname,
                Version = dbmigration.Solution.Version
            };

            Guid newsolutionid = _ctrl.CrmConnectionMgr.CrmSvc.Create(newsolution);
            return newsolutionid;
        }

        private Guid CreateCustomEntity(D365Entity entity)
        {
            // find the primary attribute 
            iD365Attribute primaryAttribute = entity.Attributes.FirstOrDefault(a => a.IsPrimaryAttribute == true);

            CreateEntityRequest newentity = new CreateEntityRequest
            {
                HasActivities = entity.HasActivities,
                HasNotes = entity.HasNotes,
                SolutionUniqueName = dbmigration.Solution.Schemaname,

                PrimaryAttribute = new StringAttributeMetadata
                {
                    SchemaName = dbmigration.Solution.Publisher.Prefix + "_" + primaryAttribute.Schemaname,
                    RequiredLevel = new AttributeRequiredLevelManagedProperty(AttributeRequiredLevel.None),
                    MaxLength = 100,
                    DisplayName = new Microsoft.Xrm.Sdk.Label(primaryAttribute.AccessFieldname, LanguageCode)
                },
                Entity = new EntityMetadata
                {
                    IsActivity = false,
                    SchemaName = dbmigration.Solution.Publisher.Prefix + "_" + entity.Schemaname,
                    DisplayName = new Microsoft.Xrm.Sdk.Label(entity.Displayname, LanguageCode),
                    DisplayCollectionName = new Microsoft.Xrm.Sdk.Label(entity.Displayname_plural, LanguageCode),
                    OwnershipType = OwnershipTypes.UserOwned,
                    IsAvailableOffline = false
                }

            };
            CreateEntityResponse response = (CreateEntityResponse)_ctrl.CrmConnectionMgr.CrmSvc.Execute(newentity);
            return response.EntityId;

        }

        private void CreateCustomAttributes(D365Entity e)
        {
            foreach (iD365Attribute a in e.Attributes)
            {
                // do not create the primary attribute again !!
                if (a.IsPrimaryAttribute == false)
                {
                    //logText.AppendText(String.Format(Environment.NewLine + "creating Attribute '{0}'", a.AccessFieldname));
                    CreateAttributeRequest attributerequest = new CreateAttributeRequest
                    {
                        EntityName = dbmigration.Solution.Publisher.Prefix + "_" + e.Schemaname,
                        Attribute = new StringAttributeMetadata
                        {
                            SchemaName = dbmigration.Solution.Publisher.Prefix + "_" + a.Schemaname,
                            DisplayName = new Microsoft.Xrm.Sdk.Label(a.AccessFieldname, LanguageCode),
                            MaxLength = 100
                        }
                    };
                    CreateAttributeResponse response = (CreateAttributeResponse)_ctrl.CrmConnectionMgr.CrmSvc.Execute(attributerequest);
                }
            };
        }

        private void CreateRelation(D365Entity entity)
        {
            foreach(D365Entity.ForeignKey fk in entity.ForeignKeys)
            {
                //logText.AppendText(String.Format(Environment.NewLine + "creating Relation '{0}'", fk.fk_name));
                //https://docs.microsoft.com/en-us/powerapps/developer/data-platform/org-service/metadata-relationshipmetadata
                //Checks whether the specified entity can be the primary entity in one-to-many
                //relationship.
                string referenced_entity = dbmigration.Solution.Publisher.Prefix + "_" + fk.D365LookupSchemaname;
                string referencing_entity = dbmigration.Solution.Publisher.Prefix + "_" + entity.Schemaname;

                CanBeReferencedRequest canBeReferencedRequest = new CanBeReferencedRequest
                {
                    EntityName = referenced_entity
                };

                CanBeReferencedResponse canBeReferencedResponse =
                    (CanBeReferencedResponse)_ctrl.CrmConnectionMgr.CrmSvc.Execute(canBeReferencedRequest);

                if (!canBeReferencedResponse.CanBeReferenced)
                {
                    Console.WriteLine(
                        "Entity {0} can't be the primary entity in this one-to-many relationship",
                        referenced_entity);
                }

                //Checks whether the specified entity can be the referencing entity in one-to-many
                //relationship.
                CanBeReferencingRequest canBereferencingRequest = new CanBeReferencingRequest
                {
                    EntityName = referencing_entity
                };

                CanBeReferencingResponse canBeReferencingResponse =
                    (CanBeReferencingResponse)_ctrl.CrmConnectionMgr.CrmSvc.Execute(canBereferencingRequest);

                if (!canBeReferencingResponse.CanBeReferencing)
                {
                    Console.WriteLine(
                        "Entity {0} can't be the referencing entity in this one-to-many relationship",
                        referencing_entity);
                }
                if (canBeReferencedResponse.CanBeReferenced == true && canBeReferencingResponse.CanBeReferencing == true)
                {
                    CreateOneToManyRequest createOneToManyRelationshipRequest = new CreateOneToManyRequest
                    {
                        OneToManyRelationship = new OneToManyRelationshipMetadata
                        {
                            ReferencedEntity = referenced_entity,
                            ReferencingEntity = referencing_entity,
                            SchemaName = referenced_entity + "_" + referencing_entity, 
                            AssociatedMenuConfiguration = new AssociatedMenuConfiguration
                            {
                                Behavior = AssociatedMenuBehavior.UseLabel,
                                Group = AssociatedMenuGroup.Details,
                                Label = new Label(fk.pk_table_name, LanguageCode),
                                Order = 10000
                            },
                            CascadeConfiguration = new CascadeConfiguration
                            {
                                Assign = CascadeType.NoCascade,
                                Delete = CascadeType.RemoveLink,
                                Merge = CascadeType.NoCascade,
                                Reparent = CascadeType.NoCascade,
                                Share = CascadeType.NoCascade,
                                Unshare = CascadeType.NoCascade
                            }
                        },
                        Lookup = new LookupAttributeMetadata
                        {
                            SchemaName = dbmigration.Solution.Publisher.Prefix + "_" + fk.pk_table_name + "id",
                            DisplayName = new Label(fk.pk_table_name + " Lookup", LanguageCode),
                            RequiredLevel = new AttributeRequiredLevelManagedProperty(AttributeRequiredLevel.None),
                            Description = new Label("Sample Lookup", LanguageCode)
                        }
                    };
                    _ctrl.CrmConnectionMgr.CrmSvc.Execute(createOneToManyRelationshipRequest);
                }
            }
        }

        private Guid createD365App()
        {
            //logText.AppendText(String.Format(Environment.NewLine + "creating App '{0}'", dbmigration.Solution.D365App.Displayname));
            AppModule app = new AppModule
            {
                ClientType = 4,
                Name = dbmigration.Solution.D365App.Displayname,
                UniqueName = dbmigration.Solution.D365App.Schemaname,
                WebResourceId = new Guid("953b9fac-1e5e-e611-80d6-00155ded156f"),   // Standard Picture
                PublisherId = new EntityReference("publisher", dbmigration.Solution.Publisher.PublisherId)
            };

            // create the app and store ID
            Guid _appid = _ctrl.CrmConnectionMgr.CrmSvc.Create(app);
            return _appid;
        }

        private bool validateD365App()
        {
            ValidateAppRequest request = new ValidateAppRequest
            {
                AppModuleId = dbmigration.Solution.D365App.AppId
            };
            ValidateAppResponse response = (ValidateAppResponse)_ctrl.CrmConnectionMgr.CrmSvc.Execute(request);

            return response.AppValidationResponse.ValidationSuccess;
        }

        private void createAppReferences()
        {
            EntityReferenceCollection related_entities = new EntityReferenceCollection();
            foreach (D365Entity e in dbmigration.SolutionEntities)
            {
                if (e.MigrateTable)
                {
                    related_entities.Add(new EntityReference(
                        dbmigration.Solution.Publisher.Prefix + "_" + e.Schemaname, e.EntityId));
                }
            };

            // create a sitemap
            SiteMap sitemap = new SiteMap
            {
                SiteMapName = dbmigration.Solution.D365App.Displayname,
                SiteMapNameUnique = dbmigration.Solution.D365App.Schemaname
            };
            sitemap.SiteMapXml = generateSiteMap();

            Guid sitemapresponse = _ctrl.CrmConnectionMgr.CrmSvc.Create(sitemap);
            logOperation("sitemap", sitemapresponse);

            // add Sitemap to Solution
            AddSolutionComponentRequest r_sol = new AddSolutionComponentRequest
            {
                ComponentType = 62,                                 // App Module
                SolutionUniqueName = dbmigration.Solution.Schemaname,
                ComponentId = (Guid)sitemapresponse
            };
            var answer = _ctrl.CrmConnectionMgr.CrmSvc.Execute(r_sol);
            
            // add sitemap to App References
            related_entities.Add(new EntityReference("sitemap", sitemapresponse));

            AddAppComponentsRequest r = new AddAppComponentsRequest
            {
                AppId = dbmigration.Solution.D365App.AppId,
                Components = related_entities
            };

            AddAppComponentsResponse response = (AddAppComponentsResponse)_ctrl.CrmConnectionMgr.CrmSvc.Execute(r);

            // add App to solution
            AddSolutionComponentRequest r_sol2 = new AddSolutionComponentRequest
            {
                ComponentType = 80,                                 // App Module
                SolutionUniqueName = dbmigration.Solution.Schemaname,
                ComponentId = dbmigration.Solution.D365App.AppId
            };

            var answer2 = _ctrl.CrmConnectionMgr.CrmSvc.Execute(r_sol2);

        }

        private string generateSiteMap()
        {
            XDocument sitemap = new XDocument();
            sitemap.Add(
                new XElement("SiteMap",
                    new XElement("Area",
                        new XAttribute("Id", "New_Area12"),
                        new XAttribute("ResourceId", "SitemapDesigner.NewArea"),
                        new XAttribute("DescriptionResourceId", "SitemapDesigner.NewArea"),
                        new XAttribute("ShowGroups", "true"),
                        new XElement("Titles",
                            new XElement("Title",
                            new XAttribute("LCID", LanguageCode.ToString()),
                            new XAttribute("Title", dbmigration.Solution.D365App.Displayname))
                        ),
                        new XElement("Group",
                            new XAttribute("Id", "New_Group1"),
                            new XAttribute("ResourceId", "SitemapDesigner.NewGroup"),
                            new XAttribute("DescriptionResourceId", "SitemapDesigner.NewGroup"),
                            new XAttribute("IsProfile", "false"),
                            new XElement("Titles",
                                new XElement("Title",
                                new XAttribute("LCID", LanguageCode.ToString()),
                                new XAttribute("Title", dbmigration.Solution.D365App.Displayname))
                            )
                        )

                    )
                )
            );

            XElement group = sitemap.Descendants("Group").FirstOrDefault();

            XElement subarea;
            int counter = 0;
            foreach (D365Entity entity in dbmigration.SolutionEntities)
            {
                counter++; 
                subarea = new XElement("SubArea",
                   new XAttribute("Id", "New_Subarea" + counter.ToString()),
                   //new XAttribute("Icon", "/_imgs/imagestrips/transparent_spacer.gif"),
                   //new XAttribute("Client", "All,Outlook,OutlookLaptopClient,OutlookWorkstationClient,Web"),
                   //new XAttribute("AvailableOffline", "true"),
                   //new XAttribute("PassParams", "false"),
                   //new XAttribute("Title", entity.Schemaname),
                   new XAttribute("Entity", dbmigration.Solution.Publisher.Prefix + "_" + entity.Schemaname)
                   );
                group.Add(subarea);
            }

            return sitemap.ToString();
        }

        private void publishCustomizations()
        {
            var p = new PublishAllXmlRequest();
            PublishAllXmlResponse response = (PublishAllXmlResponse)_ctrl.CrmConnectionMgr.CrmSvc.Execute(p);
        }

        #endregion  

        #region Data Import Stuff

        public void ImportData()
        {
            // only select entities which should be imported
            foreach (D365Entity entity in dbmigration.SolutionEntities.Where(e => e.MigrateTable))
            {    
                #region Create an import map
                ImportMap importMap = new ImportMap()
                {
                    Name = "Import Map " + entity.Displayname + " " + DateTime.Now.Ticks.ToString(),
                    Source = String.Format("Import {0}", entity.Displayname),
                    Description = "Description of data being imported",
                    EntitiesPerFile = new OptionSetValue((int)ImportMapEntitiesPerFile.SingleEntityPerFile),
                    EntityState = EntityState.Created
                };
                Guid importMapId = _ctrl.CrmConnectionMgr.CrmSvc.Create(importMap);
                #endregion

                #region column mapping for each field.
                foreach (iD365Attribute att in entity.Attributes)
                {
                    ColumnMapping colMapping = new ColumnMapping()
                    {
                        // Set source properties.
                        SourceAttributeName = att.AccessFieldname,
                        SourceEntityName = entity.AccessDB_Tablename,

                        // Set target properties.
                        TargetAttributeName = dbmigration.Solution.Publisher.Prefix + "_" + att.Schemaname,
                        TargetEntityName = dbmigration.Solution.Publisher.Prefix + "_" + entity.Schemaname,

                        // Relate this column mapping with the data map.
                        ImportMapId = new EntityReference(ImportMap.EntityLogicalName, importMapId),

                        // Force this column to be processed.
                        ProcessCode = new OptionSetValue((int)ColumnMappingProcessCode.Process)
                    };
                    
                    Guid colMappingId = _ctrl.CrmConnectionMgr.CrmSvc.Create(colMapping);

                    // add lookup mapping if attribute is a foreign key
                    Debug.Print(att.GetType().ToString());
                    //if (att.AccessForeignKey)
                    //{
                    //    // look at the foreign keys for this entity for the attribute
                    //    string primarytable = String.Empty;
                    //    for (int i=0; i<entity.ForeignKeys.Count; i++)
                    //    {
                    //        if (((D365Entity.ForeignKey)entity.ForeignKeys[i]).fk_column_name == att.AccessFieldname)
                    //        {
                    //            primarytable = ((D365Entity.ForeignKey)entity.ForeignKeys[i]).pk_table_name;
                    //        }
                    //    }

                    //    if(primarytable != null)
                    //    {
                    //        LookUpMapping lookupMapping = new LookUpMapping()
                    //        {
                    //            // Relate this mapping with its parent column mapping.
                    //            ColumnMappingId = new EntityReference(ColumnMapping.EntityLogicalName, colMappingId),

                    //            // Force this column to be processed.
                    //            ProcessCode = new OptionSetValue((int)LookUpMappingProcessCode.Process),

                    //            // Set the lookup for an account entity by its name attribute.
                    //            LookUpEntityName = primarytable,
                    //            LookUpAttributeName = dbmigration.Solution.Publisher.Prefix + "_name",
                    //            LookUpSourceCode = new OptionSetValue((int)LookUpMappingLookUpSourceCode.System)
                    //        };

                    //        // Create the lookup mapping.
                    //        Guid parentLookupMappingId = _ctrl.CrmConnectionMgr.CrmSvc.Create(lookupMapping);
                    //    }
                    //}

                }
                #endregion

                #region Create Import
                Import import = new Import()
                {
                    // IsImport is obsolete; use ModeCode to declare Create or Update.
                    ModeCode = new OptionSetValue((int)ImportModeCode.Create),
                    Name = "Importing data"
                };
                Guid importId = _ctrl.CrmConnectionMgr.CrmSvc.Create(import);

                #endregion

                #region Create Import File
                ImportFile importFile = new ImportFile()
                {
                    //Content = BulkImportHelper.ReadCsvFile("Import Accounts.csv"), // Read contents from disk.
                    Content = dbmigration.getTableData(entity.AccessDB_Tablename),
                    Name = String.Format("{0} record import", entity.Schemaname),
                    IsFirstRowHeader = true,
                    ImportMapId = new EntityReference(ImportMap.EntityLogicalName, importMapId),
                    UseSystemMap = false,
                    Source = String.Format("Import MS Access Table {0}", entity.AccessDB_Tablename),
                    SourceEntityName = entity.AccessDB_Tablename,
                    TargetEntityName = dbmigration.Solution.Publisher.Prefix + "_" + entity.Schemaname,
                    ImportId = new EntityReference(Import.EntityLogicalName, importId),
                    EnableDuplicateDetection = false,
                    FieldDelimiterCode = new OptionSetValue((int)ImportFileFieldDelimiterCode.Comma),
                    DataDelimiterCode = new OptionSetValue((int)ImportFileDataDelimiterCode.SingleQuote),
                    ProcessCode = new OptionSetValue((int)ImportFileProcessCode.Process)
                };
                #endregion

                #region Get the current user to set as record owner.
                WhoAmIRequest systemUserRequest = new WhoAmIRequest();
                WhoAmIResponse systemUserResponse = (WhoAmIResponse)_ctrl.CrmConnectionMgr.CrmSvc.Execute(systemUserRequest);

                // Set the owner ID.				
                importFile.RecordsOwnerId = new EntityReference(SystemUser.EntityLogicalName, systemUserResponse.UserId);
                #endregion

                #region create import file, parse, transform and import
                Guid importFileId = _ctrl.CrmConnectionMgr.CrmSvc.Create(importFile);

                ParseImportRequest parseImportRequest = new ParseImportRequest()
                {
                    ImportId = importId
                };
                ParseImportResponse parseImportResponse = 
                    (ParseImportResponse)_ctrl.CrmConnectionMgr.CrmSvc.Execute(parseImportRequest);

                TransformImportRequest transRequest = new TransformImportRequest()
                {
                    ImportId = importId
                };

                TransformImportResponse transResponse = 
                    (TransformImportResponse)_ctrl.CrmConnectionMgr.CrmSvc.Execute(transRequest);

                ImportRecordsImportRequest importrequest = new ImportRecordsImportRequest()
                {
                    ImportId = importId
                };
                ImportRecordsImportResponse importresponse =
                    (ImportRecordsImportResponse)_ctrl.CrmConnectionMgr.CrmSvc.Execute(importrequest);
                #endregion
            }
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            ImportData();
        }

        private void GrdTables_SelectionChanged(object sender, EventArgs e)
        {
            // this selects the table name and triggers the reload of the table columns grid
            string tb_name = grdTables.CurrentRow.Cells[1].Value.ToString();
            Debug.Print("GrdTables Selection change " + tb_name);
            currenttable = tb_name;
            loadTableColumnsSource(tb_name);

        }

        private void grdTables_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Debug.Print("grdTable Cell Click");
        }

        private void grdTableColumns_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Debug.Print("grdTableColumns Cell Content Click, current table: " + currenttable);
            // check which entity is selected
            var value = grdTableColumns.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            var att = grdTableColumns.Rows[e.RowIndex].Cells[1].Value;                 // this is the attribute name

        }

        private void grdTableColumns_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Debug.Print("TableColumns Cell Click");
            if (grdTableColumns.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn && e.RowIndex != -1)
            {
                // find out which cell has been clicked --> value is BEFORE click-event

                var value = grdTableColumns.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                var att = grdTableColumns.Rows[e.RowIndex].Cells[1].Value;                 // this is the attribute name

                Debug.Print(att.ToString());

                D365Entity d365e = dbmigration.SolutionEntities.FirstOrDefault(ent => ent.Displayname == currenttable);

                switch (e.ColumnIndex)
                {
                    case 6:     // change primary attribute
                        d365e.Attributes.FirstOrDefault(a => a.AccessFieldname.Equals(att.ToString())).IsPrimaryAttribute = !Convert.ToBoolean(value);
                        break;
                    //    case 2:
                    //        d365e.HasActivities = !Convert.ToBoolean(value);
                    //        break;
                    //    case 3:
                    //        d365e.HasNotes = !Convert.ToBoolean(value);
                    //        break;
                }
            }

        }



    }
}
