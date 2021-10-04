using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFactory.BusinessLogic
{
    public class D365Publisher
    {
        private string _name;
        private string _schemaname;
        private string _prefix;
        private Guid _id;

        public string Displayname
        {
            get => _name;
            set
            {
                _name = value;
            }
        }

        public string Schemaname
        {
            get => _schemaname;
            set
            {
                _schemaname = value;
            }
        }

        public string Prefix
        {
            get => _prefix;
            set
            {
                _prefix = value;
            }
        }

        public Guid PublisherId
        {
            get => _id;
            set
            {
                _id = value;
            }
        }

    }
}
