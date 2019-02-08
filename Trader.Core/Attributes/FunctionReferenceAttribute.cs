using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trader.Core.Attributes
{
   public class FunctionReferenceAttribute : Attribute
    {
        public string Name { get; set; }

        public FunctionReferenceAttribute(string name)
        {
            Name = name;
        }
    }
}
