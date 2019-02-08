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
    public partial class NodeInfoDetails : BaseControl
    {
        public NodeInfoDetails()
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
            var cprice =  account.GetNodeInfo();
            cprice.Wait();
            listViewEx1.Invoke(new Action(() =>
            {
                listViewEx1.Items.Clear();
                AddInfo("Enode", cprice.Result["enode"].ToString());
                AddInfo("Enr", cprice.Result["enr"].ToString());
                AddInfo("Id", cprice.Result["id"].ToString());
                AddInfo("Ip", cprice.Result["ip"].ToString());
                AddInfo("Name", cprice.Result["name"].ToString());
                AddInfo("Listen Address", cprice.Result["listenAddr"].ToString());
                AddInfo("Listen Port", cprice.Result["ports"]["listener"].ToString());
                AddInfo("Discovery Port", cprice.Result["ports"]["discovery"].ToString());



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
