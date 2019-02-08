using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Trader.Core.Attributes;
using Trader.Core.DTO;
using Trader.Core.Models;
using Transaction = Nethereum.RPC.Eth.DTOs.Transaction;

namespace Trader.Core
{
   public class TradingContract
    {
        public Account Account { get; set; }
        public Contract Contract { get; set; }
        public event EventHandler OnChanged;
        private List<Asset> _baseAssets;
        private List<Models.Transaction> _baseTransactions;
        public Dictionary<string, Function>  Functions { get; set; } = new Dictionary<string, Function>();
        public TradingContract(Contract c, Account a)
        {
            Account = a;
            Contract = c;
            GetFunctions(c);
            a.Stats = new Statistics(a);
            OnChanged += a.Stats.ContractOnChanged;
        }

        public void LoadContract(Contract c)
        {
            Contract = c;
            GetFunctions(c);
        }
        HexBigInteger filterAssetsJoined, filterAssetsUpdated, filterTransactionExecuted;
        Event<AssetJoinedEventDTO> ajEventHandler;
        Event<AssetUpdatedEventDTO> auEventHandler;
        Event<TransactionEventDTO> tEventHandler;

        async Task GetFilters()
        {
            filterAssetsJoined = await ajEventHandler.CreateFilterAsync();
            filterAssetsUpdated = await auEventHandler.CreateFilterAsync();
            filterTransactionExecuted = await tEventHandler.CreateFilterAsync();
        }
        void GetFunctions(Contract c)
        {
            foreach (var methodInfo in GetType().GetMethods())
            {
                var funcRef = methodInfo.GetCustomAttribute<FunctionReferenceAttribute>();
                if (funcRef != null)
                    Functions.Add(funcRef.Name, c.GetFunction(funcRef.Name));  
            }

            ajEventHandler = Account.Web3.Eth.GetEvent<AssetJoinedEventDTO>(c.Address);
            auEventHandler = Account.Web3.Eth.GetEvent<AssetUpdatedEventDTO>(c.Address);
            tEventHandler = Account.Web3.Eth.GetEvent<TransactionEventDTO>(c.Address);

            GetFilters().Wait();
        }

        async Task<HexBigInteger> EstimateGasAsync(Function function, params object[] p)
        {
            return await function.EstimateGasAsync(Account.Address, null, null, p);
        }
       public async Task<HexBigInteger> EstimateGasAsync(string methodName = "", params object[] p)
        {
            var method = GetType().GetMethod(methodName);
            Function function = null;
            var fr = method?.GetCustomAttribute<FunctionReferenceAttribute>();
            if (fr == null)
                return null;
            else
                function = Functions[fr.Name];

            return await EstimateGasAsync(function, p);
        }


        async Task<TransactionReceipt> CallPayableAsync(Function function, params object[] p)
        {
            var gas = await function.EstimateGasAsync(Account.Address, null, null, p);
            return await function.SendTransactionAndWaitForReceiptAsync(Account.Address, gas, null, null, p);
        }
        async Task<TransactionReceipt> CallPayableAsync(string methodName, params object[] p)
        {
            var method = GetType().GetMethod(methodName);
            Function function = null;
            var fr = method?.GetCustomAttribute<FunctionReferenceAttribute>();
            if (fr == null)
                return null;
            else
                function = Functions[fr.Name];

            return await CallPayableAsync(function, p);
        }
        async Task<T> CallAsync<T>( string methodName , params object[] p)
        {
            var method = GetType().GetMethod(methodName);

            var fr = method?.GetCustomAttribute<FunctionReferenceAttribute>();
            if (fr == null)
                return default(T);
            else
                return await Functions[fr.Name].CallAsync<T>(p);

        }

        async Task<T> CallDeserializeAsync<T>(string methodName, params object[] p) where T:new()
        {
            var method = GetType().GetMethod(methodName);

            var fr = method?.GetCustomAttribute<FunctionReferenceAttribute>();
            if (fr == null)
                return default(T);
            else
                return await Functions[fr.Name].CallDeserializingToObjectAsync<T>(p);

        }


        public object[] AssetToParameters(Asset asset)
        {
            return new object[] {Encoding.UTF8.GetBytes( asset.Id), asset.Price, asset.Quantity};
        }

        [FunctionReference("register")]
        public async Task<bool> RegisterAsync(Asset asset)
        {
            if(string.IsNullOrWhiteSpace(asset.Id) || asset.Id.Length != 6)
                throw  new ArgumentException("Asset id length must be equal to 6");
            if(asset.Price < 0 || asset.Quantity < 0)
                throw new ArgumentException("Asset quantity & price must be a non negative value");

            return (await CallPayableAsync(nameof(RegisterAsync), AssetToParameters(asset))).Status == new HexBigInteger(1);
        }

        [FunctionReference("getTransactionsCount")]
        public async Task<int> GetTransactionsCountAsync()
        {
            return await CallAsync<int>(nameof(GetTransactionsCountAsync));
        }

        [FunctionReference("getAssetsCount")]
        public async Task<int> GetAssetsCountAsync()
        {
            return await CallAsync<int>(nameof(GetAssetsCountAsync));
        }

        [FunctionReference("getAssetIndex")]
        public async Task<int> GetAssetIndexAsync()
        {
            return await CallAsync<int>(nameof(GetAssetIndexAsync));
        }

        [FunctionReference("getAsset")]
        public async Task<Asset> GetAssetAsync(string id)
        {
            var a = await CallDeserializeAsync<AssetDTO>(nameof(GetAssetAsync), Encoding.UTF8.GetBytes(id));
            if (a.Quantity == -1 && a.Price == -1)
                return null;
            else return new Asset(){ Id=Encoding.UTF8.GetString(a.Name), Quantity = a.Quantity, Price = a.Price};
        }

        [FunctionReference("getAssetByIndex")]
        public async Task<Asset> GetAssetByIndexAsync(int index)
        {
            var a = await CallDeserializeAsync<AssetDTO>(nameof(GetAssetByIndexAsync), index);
            if (a.Quantity == -1 && a.Price == -1)
                return null;
            else return new Asset() { Id = Encoding.UTF8.GetString(a.Name), Quantity = a.Quantity, Price = a.Price };
        }


        [FunctionReference("getTransactionByIndex")]
        public async Task<Models.Transaction> GetTransactionByIndexAsync(int index)
        {
            var a = await CallDeserializeAsync<TransactionDTO>(nameof(GetTransactionByIndexAsync), index);
            if (a.Quantity == -1 && a.Price == -1)
                return null;
            else return new Models.Transaction() { Source = Encoding.UTF8.GetString(a.Source), Target = Encoding.UTF8.GetString(a.Target), Quantity = a.Quantity, Price = a.Price, State = (TransactionState)(byte)a.State, Timestamp = UnixTimeStampToDateTime((double)a.Timestamp)};
        }

        [FunctionReference("transact")]
        public async Task<bool> TransactAsync(string source, string target, int quantity)
        {
            if (string.IsNullOrWhiteSpace(source) || source.Length != 6)
                throw new ArgumentException("Transaction source id length must be equal to 6");
            if (string.IsNullOrWhiteSpace(target) || target.Length != 6)
                throw new ArgumentException("Transaction target id length must be equal to 6");
            if (quantity <= 0)
                throw new ArgumentException("Transaction quantity must be a non negative value");


            return (await CallPayableAsync(nameof(TransactAsync), Encoding.UTF8.GetBytes(source), Encoding.UTF8.GetBytes(target), quantity)).Status == new HexBigInteger("0x1");
        }


        public async Task<List<Models.Asset>> GetAssetsAsync()
        {

            bool changed = false;
            Task<List<EventLog<AssetJoinedEventDTO>>> aj = null;
            Task<List<EventLog<AssetUpdatedEventDTO>>> au =null;
            if (_baseAssets != null)
            {
                aj = ajEventHandler.GetFilterChanges(filterAssetsJoined);
                au = auEventHandler.GetFilterChanges(filterAssetsUpdated);
            }
            else
            {
                _baseAssets = new List<Asset>();
                aj = ajEventHandler.GetAllChanges(filterAssetsJoined);
                au = auEventHandler.GetAllChanges(filterAssetsUpdated);
            }
  
            foreach (var aji in (await aj))
            {
                changed = true;
                if (_baseAssets.Any(x => x.Id == Encoding.UTF8.GetString(aji.Event.Id)))
                {
                    // update
                    var a = _baseAssets.FirstOrDefault(x => x.Id == Encoding.UTF8.GetString(aji.Event.Id));
                    a.Quantity = aji.Event.Quantity;
                    a.Price = aji.Event.Price;
                    a.Address = aji.Event.Address;
                    a.Index = aji.Event.Index;
                }
                else
                    _baseAssets.Add(new Asset { Address = aji.Event.Address, Price = aji.Event.Price, Quantity = aji.Event.Quantity, Id = Encoding.UTF8.GetString(aji.Event.Id), Index = aji.Event.Index});

            }
            foreach (var aji in (await au))
            {
                changed = true;
                if (_baseAssets.Any(x => x.Id == Encoding.UTF8.GetString(aji.Event.Id)))
                {
                    // update
                    var a = _baseAssets.FirstOrDefault(x => x.Id == Encoding.UTF8.GetString(aji.Event.Id));
                    a.Quantity = aji.Event.Quantity;
                    a.Price = aji.Event.Price;
                    a.Address = aji.Event.Address;
                    a.Index = aji.Event.Index;
                }
                else
                    _baseAssets.Add(new Asset { Address = aji.Event.Address, Price = aji.Event.Price, Quantity = aji.Event.Quantity, Id = Encoding.UTF8.GetString(aji.Event.Id), Index = aji.Event.Index });

            }
            if (changed)
                OnChanged?.Invoke(this, EventArgs.Empty);
            return _baseAssets;
        }
        public static DateTimeOffset UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTimeOffset dtDateTime = new DateTimeOffset(1970, 1, 1, 0, 0, 0, 0, new TimeSpan());
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp); // .ToLocalTime();
            return dtDateTime;
        }
        public async Task<List<Models.Transaction>> GetTransactionsAsync()
        {
            Task<List<EventLog<TransactionEventDTO>>> tj = null;
            bool changed = false;
            if (_baseTransactions != null)
                tj = tEventHandler.GetFilterChanges(filterTransactionExecuted);
            else
            {
                _baseTransactions = new List<Models.Transaction>();
                tj = tEventHandler.GetAllChanges(filterTransactionExecuted);
            }


            foreach (var aji in (await tj))
            {
                changed = true;
                _baseTransactions.Add(new Models.Transaction()
                {
                    Price = aji.Event.Price,
                    Quantity = aji.Event.Quantity,
                    Source = Encoding.UTF8.GetString(aji.Event.Source),
                    Target = Encoding.UTF8.GetString(aji.Event.Target),
                    State = (TransactionState)(byte)aji.Event.State,
                    Timestamp = UnixTimeStampToDateTime((double)aji.Event.Timestamp),
                    SourceIndex = aji.Event.SourceIndex,
                    TargetIndex = aji.Event.TargetIndex,
                });
            }
            if(changed)
                OnChanged?.Invoke(this,EventArgs.Empty);
            return _baseTransactions;
        }

   
        public List<Models.Transaction> GetTransactionsByAsset(BigInteger index)
        {
            return _baseTransactions?.Where(x => x.SourceIndex == index || x.TargetIndex == index).ToList();
        }
    }

}
