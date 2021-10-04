using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFactory.BusinessLogic
{
    public abstract class D365AttributeFactory
    {
        public abstract iD365Attribute CreateAttribute();
    }
}
