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
using Trader.Core.Models;
using Trader.Interfaces;

namespace Trader.Controls
{
    public partial class AssetDetails : BaseControl, ITransactionControl, IAssetControl, IOwnerControl
    {
        public AssetDetails()
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
            if (x is Asset)
            {
                var account = x as Asset;

                listViewEx1.Invoke(new Action(() =>
                {
                    listViewEx1.Items.Clear();
                    AddInfo("Account Address", account.Address);
                    AddInfo("Index", account.Index.ToString());
                    AddInfo("Id", account.Id);
                    AddInfo("Price", account.Price.ToString());
                    AddInfo("Quantity", account.Quantity.ToString());

                }));
            }
            else if (x is Transaction)
            {
                var t = x as Transaction;

                listViewEx1.Invoke(new Action(() =>
                {
                    listViewEx1.Items.Clear();
                    AddInfo("Source", t.Source);
                    AddInfo("Source Index", t.SourceIndex.ToString());
                    AddInfo("Target", t.Target);      
                    AddInfo("Target Index", t.TargetIndex.ToString());
                    AddInfo("Price", t.Price.ToString());
                    AddInfo("Quantity", t.Quantity.ToString());
                    AddInfo("Date", t.Timestamp.ToString());
                    AddInfo("State", (t.State == TransactionState.Pending ? "Pending" : (t.State == TransactionState.Validated ? "Validated" : "Rejected")));

                }));
            }
            else if (x is string)
            {
                var t = x as string;
                listViewEx1.Invoke(new Action(() =>
                {
                    listViewEx1.Items.Clear();
                    AddInfo("Account Address", t);
                }));
            }
        }

        private Thread GetInfoThread;
        public void UpdateAsset(Asset account)
        {
            if (account == null) return;
            if (GetInfoThread != null && GetInfoThread.IsAlive)
                return;

            GetInfoThread = new Thread(GetInfo);
            GetInfoThread.SetApartmentState(ApartmentState.MTA);
            GetInfoThread.Start(account);
        }
        public void UpdateOwner(string adr)
        {
            if (adr == null) return;
            if (GetInfoThread != null && GetInfoThread.IsAlive)
                return;

            GetInfoThread = new Thread(GetInfo);
            GetInfoThread.SetApartmentState(ApartmentState.MTA);
            GetInfoThread.Start(adr);
        }
        public void UpdateTransaction(Transaction account)
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

        private void listViewEx1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

     

       public void Changed(Asset a)
        {
            if(a != null)
                UpdateAsset(a);
            else listViewEx1.Items.Clear();
        }
        public void Changed(Transaction t)
        {
            UpdateTransaction(t);
        }

        public void Changed(string address)
        {
            UpdateOwner(address);
        }
    }
}
