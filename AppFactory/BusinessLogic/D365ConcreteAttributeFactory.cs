using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFactory.BusinessLogic
{
    public class D365ConcreteAttributeFactory : D365AttributeFactory
    {
        DataRow datarow;
        public D365ConcreteAttributeFactory(DataRow dr)
        {
            datarow = dr;
        }

        public DataRow AttributeInfo
        {
            get => datarow;
            set
            {
                datarow = value;
            }
        }

        public override iD365Attribute CreateAttribute()
        {
            iD365Attribute att = null;

            switch (datarow["Datatype"].ToString())
            {
                case "System.String":
                    att = new D365TextAttribute(datarow);
                    break;
                case "System.Int32":
                    att = new D365TextAttribute(datarow);
                    //att = new D365IntegerAttribute();
                    break;
                case "System.Boolean":
                    att = new D365TextAttribute(datarow);
                    //att = new D365BoolAttribute(datarow);
                    break;
                default:
                    break;
            }

            return att;
        }
    }
}
