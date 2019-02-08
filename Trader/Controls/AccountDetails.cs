using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Nethereum.Web3;
using Trader.Core;

namespace Trader.Controls
{
    public partial class AccountDetails : BaseControl
    {
        public AccountDetails()
        {
            InitializeComponent();
        }
        public override void OnLogin()
        {
            listViewEx1.Items.Clear();
        }
        public override void OnLogout()
        {
            listViewEx1.Items.Clear();
        }

        void AddInfo(string key, string value)
        {
           var i = listViewEx1.Items.Add(new ListViewItem(key));
            i.SubItems.Add(value);
        }
        void GetInfo(object x)
        {
       
            var account = x as Account;
            var wei = account.BalanceWei;
            var eth = Web3.Convert.FromWei(wei);
            var cprice =  account.Contract.Contract.Eth.GasPrice.SendRequestAsync();
            var cbalance = account.Contract.Contract.Eth.GetBalance.SendRequestAsync(account.Contract.Contract.Address);
            cprice.Wait();
            cbalance.Wait();
            listViewEx1.Invoke(new Action(() =>
            {
                listViewEx1.Items.Clear();
                AddInfo("Address", account.Address);
                AddInfo("Eth Balance", eth.ToString());
                AddInfo("Wei Balance", wei.Value.ToString());
                AddInfo("Login date", account.LoggedInAt.ToString());
                AddInfo("Session Expires", account.SessionExpiresAt.ToString());
                
                AddInfo("Contract Address", account.Contract.Contract.Address);
                AddInfo("Contract Supported Functions Count", account.Contract.Functions.Count.ToString());
                var sb = new StringBuilder();
                foreach (var f in account.Contract.Functions)
                    sb.Append(f.Key + ", ");

                var funcs = sb.ToString();
                if (funcs.Length > 0) funcs = funcs.Substring(0, funcs.Length - 2);
                AddInfo("Contract Supported Functions", funcs);
                AddInfo("Contract Supported Events Count", account.Contract.Contract.ContractBuilder.ContractABI.Events.Length.ToString());
                sb.Clear();
              
                foreach (var f in account.Contract.Contract.ContractBuilder.ContractABI.Events)
                    sb.Append(f.Name + ", ");

                funcs = sb.ToString();
                if (funcs.Length > 0) funcs = funcs.Substring(0, funcs.Length - 2);

                AddInfo("Contract Supported Events", funcs);
                AddInfo("Contract Gas Price", cprice.Result.Value.ToString());
                AddInfo("Contract Balance", cbalance.Result.Value.ToString());
            }));
        }

        private Thread GetInfoThread;
        public void UpdateAccount(Account account)
        {
            if (account == null) return;
            if (GetInfoThread != null && GetInfoThread.IsAlive)
                return;

            GetInfoThread = new Thread(GetInfo);
            GetInfoThread.SetApartmentState(ApartmentState.MTA);
            GetInfoThread.Start(account);
        }
        void UpdateColumnsWidth()
        {
            foreach (var messagesListColumn in listViewEx1.Columns)
                (messagesListColumn as ColumnHeader).Width = listViewEx1.Width / (listViewEx1.Columns.Count);
        }

        private void AccountDetails_SizeChanged(object sender, EventArgs e)
        {
            UpdateColumnsWidth();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateAccount(Account);
        }
    }
}
