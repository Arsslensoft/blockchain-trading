using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Trader.Core.Models
{
    public enum TransactionState : byte
    {
        Pending,
        Validated,
        Rejected
    }
   public class Transaction
    {
        public string Source { get; set; }
        public string Target { get; set; }

        public BigInteger SourceIndex { get; set; }
        public BigInteger TargetIndex { get; set; }
        public BigInteger Quantity { get; set; }
        public BigInteger Price { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public TransactionState State { get; set; }

    }
}
