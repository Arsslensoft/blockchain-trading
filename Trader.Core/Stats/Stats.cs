using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trader.Core.Stats
{
  
    public class AssetStats
   {
       public List<KeyValuePair<string, int>> SharesByAsset { get; set; } = new List<KeyValuePair<string, int>>();  // other assets share in me
        public List<KeyValuePair<string, int>> BuySell { get; set; } = new List<KeyValuePair<string, int>>();
        public List<KeyValuePair<string, int>> AssetSharesByAsset { get; set; } = new List<KeyValuePair<string, int>>(); // my shares in others
        public List<KeyValuePair<string, int>> TransactionStates { get; set; } = new List<KeyValuePair<string, int>>(); // rejected vs Valid
        public double MessagesPercentage { get; set; }
       public double MarketValue { get; set; }

    }

    public class GlobalStats
    {
        public List<KeyValuePair<string, int>> MarketValue { get; set; } = new List<KeyValuePair<string, int>>(); // quantity * price per asset
        public List<KeyValuePair<string, int>> TransactionStates { get; set; } = new List<KeyValuePair<string, int>>(); // rejected vs Valid
    }

}
