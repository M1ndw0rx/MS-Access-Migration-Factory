using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFactory.BusinessLogic
{
    public class D365Entity
    {
        #region private members
        bool _migrate_entity;
        string _access_tablename;
        string _schemaname;
        string _singularname;
        string _pluralname;
        bool _has_activities;
        bool _has_notes;
        Guid _entityid;

        List<iD365Attribute> _attributes;
        ArrayList _foreignkeys;

        #endregion

        /// <summary>
        /// This class represents a foreign key relationship of this entity.
        /// </summary>
        public class ForeignKey
        {
            public string fk_name;              // Name of the foreign Key Constraint
            public string pk_table_name;        // Name of the primary table
            public string fk_column_name;       // Name of the attribute in the foreign table
            public string pk_column_name;       // Name of the attribute in the primary table

            public string D365Name;
            public bool D365Searchable;
            public bool D365Hierachy;
            public string D365LookupDisplayname;
            public string D365LookupSchemaname;

        }

        public D365Entity()
        {

        }

        public D365Entity(bool migrate_entity, string access_tablename, string schemaname, string singular, string plural, bool has_activities, bool has_notes)
        {
            this._migrate_entity = migrate_entity;
            this._access_tablename = access_tablename;
            this._schemaname = schemaname;
            this._singularname = singular;
            this._pluralname = plural;
            this._has_activities = has_activities;
            this._has_notes = has_notes;
            this._foreignkeys = new ArrayList();
            this.Attributes = new List<iD365Attribute>();
        }

        #region public properties

        /// <summary>
        /// This is the corresponding name of the MS Access Table name which belongs to this entity.
        /// </summary>
        public bool MigrateTable { get => _migrate_entity; set => _migrate_entity = value; }
        public string AccessDB_Tablename { get => _access_tablename; set => _access_tablename = value; }
        public bool HasActivities { get => _has_activities; set => _has_activities = value; }
        public bool HasNotes { get => _has_notes; set => _has_notes = value; }
        public string Schemaname { get => _schemaname; set => _schemaname = value; }
        public string Displayname { get => _singularname; set => _singularname = value; }
        public string Displayname_plural { get => _pluralname; set => _pluralname = value; }
        public int OwnershipType { get => default; }
        public int IsAvailableOffline { get => default; }
        public Guid EntityId { get => _entityid; set => _entityid = value; }
        public List<iD365Attribute> Attributes { get => _attributes; set => _attributes = value; }

        public ArrayList ForeignKeys { get => _foreignkeys; set => _foreignkeys = value; }

        #endregion
    }
}
