using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFactory.BusinessLogic
{

    // test comment
    public class D365App
    {
        private string _schemaname;
        private string _displayname;
        private Guid _appid;
        private bool _publishapp;

        public string Schemaname { get => _schemaname; set => _schemaname = value; }
        public string Displayname { get => _displayname; set => _displayname = value; }
        public Guid AppId { get => _appid; set => _appid = value; }
        public bool PublishApp { get => _publishapp; set => _publishapp = value; }
    }
}
