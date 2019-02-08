using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Trader.Core.Models
{
   public class Asset
    {
        public string Address { get; set; }
        public string Id { get; set; }
        public BigInteger Index { get; set; }
        public BigInteger Quantity { get; set; }
        public BigInteger Price { get; set; }
    

    }
}
