using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trader.Core.Reporting
{
   public abstract class Exporter
    { 
        public abstract string ExporterName { get; set; }
        public abstract string FileExtensions { get; set; }
        public abstract void Export(string f, DataTable dt, string additionalData = null);
      
    }
}
