using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nethereum.Contracts;
using Nethereum.Geth;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Hex.HexTypes;
using Nethereum.KeyStore;
using Nethereum.Util;
using Nethereum.Web3;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Trader.Core.Models;

namespace Trader.Core
{
    public class Account
    {
   
        public Statistics Stats { get; set; }
        public DateTimeOffset LoggedInAt { get; set; }
        public Web3Geth Web3 { get; set; }
        public DateTimeOffset SessionExpiresAt { get; set; }
        public string Address { get; set; }
        public HexBigInteger BalanceWei
        {
            get
            {
                var t=  Web3.Eth.GetBalance.SendRequestAsync(Address);
                t.Wait();
                return t.Result;
            }
        }
        public decimal BalanceEth
        {
            get
            {
                var t = Web3.Eth.GetBalance.SendRequestAsync(Address);
                t.Wait();
                return Nethereum.Web3.Web3.Convert.FromWei(t.Result);
            }
        }

        public List<Asset> Assets { get; set; } = new List<Asset>();

        public TradingContract Contract {get;set;}


        public static async Task<Account> Register(string url, string password,  double time)
        {

            Account acc = new Account()
            {
                SessionExpiresAt = DateTimeOffset.Now.AddSeconds(time),
                LoggedInAt = DateTimeOffset.Now
            };
            acc.Web3 = new Web3Geth(url);
            var adr = await acc.Web3.Personal.NewAccount.SendRequestAsync(password);
            acc.Address = adr;
            var res = await acc.Web3.Personal.UnlockAccount.SendRequestAsync(adr, password, (ulong)time);

            if (res)
                return acc;

            return null;
        }
        public  static async Task<Account> Login(string url, string address, string password, double time)
        {
            Account acc = new Account()
            {
                Address = address,
                SessionExpiresAt = DateTimeOffset.Now.AddSeconds(time),
                LoggedInAt = DateTimeOffset.Now
            };
            acc.Web3 = new Web3Geth(url);
            var res = await acc.Web3.Personal.UnlockAccount.SendRequestAsync(address, password, (ulong)time);

            if (res)
                return acc;

            return null;
        }
        public bool Mining { get; set; }
        public async Task StartMining()
        {
            if (!Mining)
            {
                await Web3.Miner.Start.SendRequestAsync(1);
                Mining = true;
            }
        }
        public async Task StopMining()
        {
            if (Mining)
            {
                await Web3.Miner.Stop.SendRequestAsync();
                Mining = false;
            }
        }
        public void LoadContract(string address, string abiFile)
        {
            var abi = File.ReadAllText(abiFile);
            var contract = Web3.Eth.GetContract(abi, address);
            Contract = new TradingContract(contract,this) ;
        }

        public async Task<JObject> GetNodeInfo()
        {
           return await Web3.Admin.NodeInfo.SendRequestAsync();
           
        }
        public void ReloadContract(string address, string abiFile)
        {
            var abi = File.ReadAllText(abiFile);
            var contract = Web3.Eth.GetContract(abi, address);
            Contract.LoadContract(contract);
        }
    }


}
