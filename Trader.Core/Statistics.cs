using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Trader.Core.Models;
using Trader.Core.Stats;

namespace Trader.Core
{
    public static class StatisticsExtensions
    {
        public static void IncrementEntry(this List<KeyValuePair<string, int>> list, string key, int value)
       {
       
            if (list.Any(x => x.Key == key))
           {
               var item = list.FirstOrDefault(x => x.Key == key);
               list.RemoveAll(x => x.Key == key);
               list.Add(new KeyValuePair<string, int>(key, item.Value + value));
           }
            else list.Add(new KeyValuePair<string, int>(key, value));
        }


    }
    public class Statistics
    {
        public Dictionary<BigInteger, AssetStats> AssetStats { get; set; }= new Dictionary<BigInteger, AssetStats>();
        public GlobalStats GlobalStats { get; set; } = new GlobalStats();
        public Account Account { get; set; }    
        public Statistics(Account account)
        {
            Account = account;
        
        }

        public event EventHandler OnStatsUpdated;
        public object UpdateLock = new object();
        public bool UpdatingStats { get; private set; }
        public void ContractOnChanged(object sender, EventArgs e)
        {
            lock (UpdateLock)
                if (UpdatingStats)
                    return;
                else
                    UpdatingStats = true;
            try
            {
                CalculateStats().Wait();
                UpdatingStats = false;
                OnStatsUpdated?.Invoke(this, EventArgs.Empty);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                UpdatingStats = false;
            }

         
        }

        AssetStats GetOrCreateAssetsStats(BigInteger index)
        {
            if (AssetStats.ContainsKey(index))
                return AssetStats[index];
            else
            {
                var ass = new AssetStats();
                AssetStats.Add(index,ass);
                return ass;
            }
        }

      
        async Task CalculateStats()
        {
            var assets = await Account.Contract.GetAssetsAsync();
            var transactions = await Account.Contract.GetTransactionsAsync();
       AssetStats  = new Dictionary<BigInteger, AssetStats>();
         GlobalStats  = new GlobalStats();


            foreach (var transaction in transactions)
            {
                var sourceStats = GetOrCreateAssetsStats(transaction.SourceIndex);
                var targetStats = GetOrCreateAssetsStats(transaction.TargetIndex);
                
                // global stats
                if (transaction.State == TransactionState.Validated)
                {
                    GlobalStats.TransactionStates.IncrementEntry("Valid", 1);
                    sourceStats.TransactionStates.IncrementEntry("Valid", 1);
                }
                else if (transaction.State == TransactionState.Rejected)
                {
                    GlobalStats.TransactionStates.IncrementEntry("Rejected", 1);
                    sourceStats.TransactionStates.IncrementEntry("Rejected", 1);
                }
                else
                {
                    GlobalStats.TransactionStates.IncrementEntry("Pending", 1);
                    sourceStats.TransactionStates.IncrementEntry("Pending", 1);
                }

              


                // stats per asset
                if (transaction.State == TransactionState.Validated)
                {  
                    // market value
                    GlobalStats.MarketValue.IncrementEntry(transaction.Source, (int)(transaction.Quantity * transaction.Price));

                    sourceStats.BuySell.IncrementEntry("Buy", 1);
                    targetStats.BuySell.IncrementEntry("Sell", 1);
                }


            }
            foreach (var asset in assets)
                GlobalStats.MarketValue.IncrementEntry(asset.Id, (int)(asset.Quantity * asset.Price));


         

          var total_value = GlobalStats.MarketValue.Sum(x => x.Value);
          foreach (var asset in assets)
            {
                var stats = GetOrCreateAssetsStats(asset.Index);

                if (total_value > 0)
                    stats.MarketValue = (100 * GlobalStats.MarketValue.FirstOrDefault(x => asset.Id == x.Key).Value /
                                        (double)total_value) ;
                else stats.MarketValue = 0;

                if (transactions.Count > 0)
                    stats.MessagesPercentage = (100*transactions.Count(x => x.Source == asset.Id) / (double)transactions.Count);
                else stats.MessagesPercentage = 0;

                // my ownerships
                var my_valid_transactions = transactions.Where(x => x.Source == asset.Id && x.State == TransactionState.Validated);
                var other_assets = my_valid_transactions.Select(x => x.Target).Distinct();
               
                foreach (var otherAsset in other_assets)
                {
                    var asset_value = my_valid_transactions.Where(y => y.Target == otherAsset)
                        .Sum(x => (int) (x.Quantity));
                    stats.SharesByAsset.IncrementEntry(otherAsset, asset_value);
                }
                stats.SharesByAsset.IncrementEntry(asset.Id, (int)asset.Quantity);

                // who owns me
                var their_valid_transactions = transactions.Where(x => x.Target == asset.Id && x.State == TransactionState.Validated);
                other_assets = their_valid_transactions.Select(x => x.Source).Distinct();
                foreach (var otherAsset in other_assets)
                {
                    var asset_value = their_valid_transactions.Where(y => y.Source == otherAsset)
                        .Sum(x => (int)(x.Quantity));
                    stats.AssetSharesByAsset.IncrementEntry(otherAsset, asset_value);
                }
                stats.AssetSharesByAsset.IncrementEntry(asset.Id, (int)asset.Quantity);

            }



        }
    }
}
