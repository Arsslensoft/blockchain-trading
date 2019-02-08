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
using DevComponents.Tree;
using Trader.Core;
using Trader.Core.Models;
using Trader.Interfaces;
using ElementStyle = DevComponents.DotNetBar.ElementStyle;

namespace Trader.Controls
{
    public partial class DistributionGraph : BaseControl
    {
        public DistributionGraph()
        {
            InitializeComponent();
        }
        public override void OnLogin()
        {
            ownerNodes.Clear();
            node1.Nodes.Clear();
        }
        public override void OnLogout()
        {
            ownerNodes.Clear();
            node1.Nodes.Clear();
        }
        private Thread GetInfoThread;
        private List<string> oldOwners;
        private List<Asset> oldAssets;
        bool HasChanged(List<string> owners, List<Asset> assets)
        {
            if (oldAssets == null || oldOwners == null)
                return true;

            return (oldAssets.Count != assets.Count || owners.Count != oldOwners.Count);
        }

        void UpdateTagsOnly(List<Asset> assets)
        {
            foreach (var ownerNode in ownerNodes)
            {
                foreach (Node valueNode in ownerNode.Value.Nodes)
                    valueNode.Tag = assets.FirstOrDefault(w => w.Index == (valueNode.Tag as Asset).Index);
                
            }
        }
        private Dictionary<string, Node> ownerNodes = new Dictionary<string, Node>();
       
        void AddAsset(Asset a, Asset sa)
        {
            var i = new Node();
            i.Style = ElementStyle3;
            i.Text = a.Id;
            i.Tag = a;
            i.ImageIndex = 0;

            ownerNodes[a.Address.ToLower()].Nodes.Add(i);

            if (sa != null)
            {
                treeGX1.SelectedNode = i;
                foreach (var assetControl in Registry.GetControls<IAssetControl>())
                    assetControl.Changed(sa);
            }
        }
        void AddOwner(string a, string sa)
        {
            var i = new Node();
            i.Text = "Account " + node1.Nodes.Count;
            i.Style = ElementStyle2;
            i.Tag = a;
            i.ImageIndex = 2;
            if (!ownerNodes.ContainsKey(a.ToLower()))
            {
                ownerNodes.Add(a.ToLower(), i);
                node1.Nodes.Add(i);
            }

            if (sa != null)
            {
                treeGX1.SelectedNode = i;
                i.Expanded = true;
                foreach (var ownerControl in Registry.GetControls<IOwnerControl>())
                    ownerControl.Changed(sa);
            }
        }
        void GetInfo(object x)
        {
            var account = x as Account;
            var assets = account.Contract.GetAssetsAsync();
            assets.Wait();
            var owners = assets.Result.Select(y => y.Address.ToLower()).Distinct();
        
            treeGX1.Invoke(new Action(() =>
            {
                Asset sa = treeGX1.SelectedNode != null && treeGX1.SelectedNode != node1 && treeGX1.SelectedNode.Tag is Asset ? treeGX1.SelectedNode.Tag as Asset : null;
                string o = treeGX1.SelectedNode != null && treeGX1.SelectedNode != node1 && treeGX1.SelectedNode.Tag is string ? treeGX1.SelectedNode.Tag as string : null;

                if (sa == null && o == null)
                    treeGX1.SelectedNode = node1;
                if (HasChanged(owners.ToList(), assets.Result))
                {
                    foreach (var ownerNode in ownerNodes)
                        ownerNode.Value.Nodes.Clear();

                    node1.Nodes.Clear();
                    ownerNodes.Clear();
                    foreach (var owner in owners)
                        AddOwner(owner, o);

                    foreach (var a in assets.Result)
                        AddAsset(a, sa);

                    oldAssets = assets.Result;
                    oldOwners = owners.ToList();
                }
                else UpdateTagsOnly(assets.Result);
               
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

   

        private void treeGX1_NodeClick(object sender, TreeGXNodeMouseEventArgs e)
        {
            if (GetInfoThread == null || GetInfoThread.IsAlive) return;
       
            if (e.Node != null)
            {
                if (e.Node == node1 || e.Node.Tag is Asset)
                {
                    var sa = e.Node != null ? e.Node.Tag as Asset : null;
                    foreach (var assetControl in Registry.GetControls<IAssetControl>())
                        assetControl.Changed(sa);
                }
                else if (e.Node.Tag != null)
                {
                    foreach (var assetControl in Registry.GetControls<IAssetControl>())
                        assetControl.Changed(null);
                    foreach (var ownerControl in Registry.GetControls<IOwnerControl>())
                        ownerControl.Changed(e.Node.Tag.ToString());
                }

            }
        }
    }
}
