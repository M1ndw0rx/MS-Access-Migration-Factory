using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppFactory.BusinessLogic
{

    public class D365Solution
    {
        private string _schemaname;
        private string _displayname;
        private string _version;
        private Guid _id;
        private D365Publisher _pub;
        private D365App _app;

        public D365Publisher Publisher { get => _pub; set => _pub = value; }
        public Guid SolutionId { get => _id; set => _id = value; }
        public string Schemaname { get => _schemaname; set => _schemaname = value; }
        public string Displayname { get => _displayname; set => _displayname = value; }
        public string Version { get => _version; set => _version = value; }
        public D365App D365App { get => _app; set => _app = value; }

        public string getSolutionXml()
        {
            string xml = string.Format(
                "<ImportExportXml>" +
                "<Publisher>" +
                "<UniqueName>{2}</UniqueName>" +
                "</Publisher>" +
                "<AppModules>" +
                    "<AppModule>" +
                        "<UniqueName>{0}</UniqueName>" +
                        "<IntroducedVersion>1.0.0</IntroducedVersion>" +
                        "<OptimizedFor></OptimizedFor>" +
                        "<FormFactor>1</FormFactor>" +
                        "<ClientType>4</ClientType>" +
                        "<NavigationType>0</NavigationType>" +
                        "<LocalizedNames>" +
                            "<LocalizedName description = '{1}' languagecode = '1031'/>" +
                        "</LocalizedNames>" +
                    "</AppModule>" +
                "</AppModules>" +
                "</ImportExportXml>",
                this.D365App.Schemaname,
                this.D365App.Displayname,
                this.Publisher.Schemaname);

            return xml;
        }
    }
}