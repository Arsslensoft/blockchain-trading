using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trader.Core.Models;
using DevComponents.AdvTree;
using Trader.Core;
using System.Threading;
using DevComponents.DotNetBar;
using Trader.Interfaces;

namespace Trader.Controls
{
    public partial class AssetsList : BaseControl
    {
        public AssetsList()
        {
            InitializeComponent();
        }
        public override void OnLogin()
        {
            node1.Nodes.Clear();
        }
        public override void OnLogout()
        {
            node1.Nodes.Clear();
        }
        private Thread GetInfoThread;
        void AddAsset(Asset a, Asset sa)
        {
            var i = new Node(a.Id);
            i.Tag = a;
            i.ImageIndex = 0;
           
            if (a.Address.ToLower() == Account.Address.ToLower())
                i.Style = new ElementStyle(Color.Blue);

            node1.Nodes.Add(i);
            if (sa != null && sa.Id == a.Id)
                advTree1.SelectedNode = i;
        }
        void GetInfo(object x)
        {
            var account = x as Account;
            var assets = account.Contract.GetAssetsAsync();
            assets.Wait();
           
            
            advTree1.Invoke(new Action(() =>
            {
                var sa = advTree1.SelectedNode != null ? advTree1.SelectedNode.Tag as Asset : null;
                node1.Nodes.Clear();
                foreach (var a in assets.Result)
                    AddAsset(a,sa);
            }));

            }
        public void UpdateAccount(Account account)
        {
            if (account == null) return;
            if (GetInfoThread != null && GetInfoThread.IsAlive)
                return;

            GetInfoThread = new Thread(GetInfo);
            GetInfoThread.SetApartmentState(ApartmentState.MTA);
            GetInfoThread.Start(account);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateAccount(Account);
        }

        private void advTree1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (GetInfoThread == null || GetInfoThread.IsAlive) return;
            var sa = advTree1.SelectedNode != null ? advTree1.SelectedNode.Tag as Asset : null;
                foreach (var assetControl in Registry.GetControls<IAssetControl>())
                    assetControl.Changed(sa);

       
        }
    }
}
