using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trader.Core;
using Trader.Core.Models;
using Trader.Core.Reporting;
using Trader.Interfaces;

namespace Trader.Controls
{
    public partial class TransactionList : BaseControl, IAssetControl, IExportable
    {
        public TransactionList()
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
        private IEnumerable<Transaction> source;

        void Display()
        {

            listViewEx1.Invoke(new Action(() =>
            {
                var i = listViewEx1.SelectedIndices.Count > 0 ? listViewEx1.SelectedIndices[0] : -1;

                listViewEx1.Items.Clear();
                foreach (var transaction in source)
                {
                    ListViewItem item = listViewEx1.Items.Add(new ListViewItem(transaction.Source));
                    if (SelectedAsset == null)
                    {
                        if (transaction.State == TransactionState.Pending)
                            item.ImageKey = "pending";
                        else if (transaction.State == TransactionState.Validated)
                            item.ImageKey = "ok";
                        else
                            item.ImageKey = "error";
                    }
                    else
                    {
                        if (transaction.SourceIndex == SelectedAsset.Index)
                            item.ImageKey = "out";
                        else 
                            item.ImageKey = "in";
                    }


                    item.SubItems.Add(transaction.Target);
                    item.SubItems.Add(transaction.Price.ToString());
                    item.SubItems.Add(transaction.Quantity.ToString());
                    item.SubItems.Add(transaction.Timestamp.ToString());
                    item.SubItems.Add((transaction.State == TransactionState.Pending ? "Pending" : (transaction.State == TransactionState.Validated ? "Validated" : "Rejected")));
                    item.Tag = transaction;

                    if (transaction.State == TransactionState.Pending)
                    {
                        item.ForeColor = Color.OrangeRed;
                        foreach (var itemSubItem in item.SubItems)
                            (itemSubItem as ListViewItem.ListViewSubItem).ForeColor = Color.OrangeRed;
                    }
                    else if (transaction.State == TransactionState.Validated)
                    {
                        item.ForeColor = Color.Green;
                        foreach (var itemSubItem in item.SubItems)
                            (itemSubItem as ListViewItem.ListViewSubItem).ForeColor = Color.Green;
                    }
                    else
                    {
                        item.ForeColor = Color.Red;
                        foreach (var itemSubItem in item.SubItems)
                            (itemSubItem as ListViewItem.ListViewSubItem).ForeColor = Color.Red;
                    }
                
                }
                if (i > -1)
                {
                    listViewEx1.SelectedIndices.Clear();
                    listViewEx1.SelectedIndices.Add(i);
                }

            }));
        }

        void GetInfo(object x)
        {
            if (x is Account)
            {
                var account = x as Account;
                var t = account.Contract.GetTransactionsAsync();
                t.Wait();
                source = t.Result;
                Display();
            }
            else if (x is Asset)
            {
                var asset = x as Asset;
                source = Account.Contract.GetTransactionsByAsset(asset.Index);
                Display();
            }

        }
        private Thread GetInfoThread;

        public void UpdateTransactions(object p)
        {
            if (p == null) return;
            if (GetInfoThread != null && GetInfoThread.IsAlive)
                return;

            GetInfoThread = new Thread(GetInfo);
            GetInfoThread.SetApartmentState(ApartmentState.MTA);
            GetInfoThread.Start(p);
        }
        void UpdateColumnsWidth()
        {
            foreach (var messagesListColumn in listViewEx1.Columns)
                (messagesListColumn as ColumnHeader).Width = listViewEx1.Width / (listViewEx1.Columns.Count);
        }

        private void TransactionList_SizeChanged(object sender, EventArgs e)
        {
            UpdateColumnsWidth();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(SelectedAsset == null && Account != null)
                UpdateTransactions(Account);
            else if (SelectedAsset != null && Account != null)
                UpdateTransactions(SelectedAsset);
            // get by asset
        }

        private void listViewEx1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GetInfoThread.IsAlive) return;
            var i = listViewEx1.SelectedIndices.Count > 0 ? listViewEx1.SelectedIndices[0] : -1;
            if (i > -1)
            {
                var t = listViewEx1.Items[i].Tag as Transaction;
                foreach (var tControl in Registry.GetControls<ITransactionControl>())
                    tControl.Changed(t);

            }
        }

        private Asset SelectedAsset;
        private void slider2_ValueChanged(object sender, EventArgs e)
        {

        }

        public void Changed(Asset a)
        {
       
            SelectedAsset = a;
            listViewEx1.Items.Clear();
        }

        public void Export(Exporter e)
        {
            saveFileDialog1.Filter = $"*.{e.FileExtensions}|*.{e.FileExtensions}";
            if(saveFileDialog1.ShowDialog() == DialogResult.OK)
                 e.Export(saveFileDialog1.FileName, Utils.ListViewToDataTable(listViewEx1), SelectedAsset == null ? "All assets included" : SelectedAsset.Id + " transactions");
        }
    }
}
