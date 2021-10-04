using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFactory.BusinessLogic
{
        public interface iD365Attribute
    {
        string Schemaname { get; set; }
        /// <summary>
        /// This is the original fieldname from the MS Access table.
        /// </summary>
        string AccessFieldname { get; set; }
        bool MigrateAttribute { get; set; }
        string AccessDatatype { get; set; }
        bool AccessPrimaryKey { get; set; }
        bool AccessForeignKey { get; set; }
        bool IsPrimaryAttribute { get; set; }
    }


    public class D365BoolAttribute : iD365Attribute
    {
        public string Schemaname { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string AccessFieldname { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool MigrateAttribute { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string AccessDatatype { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool AccessPrimaryKey { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool AccessForeignKey { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsPrimaryAttribute { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

    public class D365DateTimeAttribute : iD365Attribute
    {
        public string Schemaname { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string AccessFieldname { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool MigrateAttribute { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string AccessDatatype { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool AccessPrimaryKey { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool AccessForeignKey { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsPrimaryAttribute { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

    public class D365DecimalAttribute : iD365Attribute
    {
        public string Schemaname { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string AccessFieldname { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool MigrateAttribute { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string AccessDatatype { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool AccessPrimaryKey { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool AccessForeignKey { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsPrimaryAttribute { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

    public class D365TextAttribute : iD365Attribute
    {

        string _schemaname;
        string _accessfieldname;
        bool _migrateattribute;
        string _accessdatatype;
        bool _accessprimarykey;
        bool _accessforeignkey;
        bool _isprimaryattribute;


        public D365TextAttribute(System.Data.DataRow datarow)
        {

            this.AccessDatatype = datarow["DataType"].ToString();
            this.AccessFieldname = datarow["ColumnName"].ToString();
            this.MigrateAttribute = true;
            this.AccessPrimaryKey = false;

            this.Schemaname = datarow["ColumnName"].ToString().ToLower();

        }

        public string Schemaname { get => _schemaname; set => _schemaname = value; }
        public string AccessFieldname { get => _accessfieldname; set => _accessfieldname = value; }
        public bool MigrateAttribute { get => _migrateattribute; set => _migrateattribute = value; }
        public string AccessDatatype { get => _accessdatatype; set => _accessdatatype = value; }
        public bool AccessPrimaryKey { get => _accessprimarykey; set => _accessprimarykey = value; }
        public bool AccessForeignKey { get => _accessforeignkey; set => _accessforeignkey = value; }
        public bool IsPrimaryAttribute { get => _isprimaryattribute; set => _isprimaryattribute = value; }
    }

    public class D365IntegerAttribute : iD365Attribute
    {
        public string Schemaname { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string AccessFieldname { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool MigrateAttribute { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string AccessDatatype { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool AccessPrimaryKey { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool AccessForeignKey { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsPrimaryAttribute { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

    public class D365MemoAttribute : iD365Attribute
    {
        public string Schemaname { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string AccessFieldname { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool MigrateAttribute { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string AccessDatatype { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool AccessPrimaryKey { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool AccessForeignKey { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsPrimaryAttribute { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

    public class D365MoneyAttribute : iD365Attribute
    {
        public string Schemaname { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string AccessFieldname { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool MigrateAttribute { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string AccessDatatype { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool AccessPrimaryKey { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool AccessForeignKey { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsPrimaryAttribute { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

    public class D365PicklistAttribute : iD365Attribute
    {
        public string Schemaname { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string AccessFieldname { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool MigrateAttribute { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string AccessDatatype { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool AccessPrimaryKey { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool AccessForeignKey { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsPrimaryAttribute { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

    
}
