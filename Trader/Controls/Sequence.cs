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
using Northwoods.Go;
using Trader.Core;
using Trader.Core.Models;
using Trader.Interfaces;

namespace Trader.Controls
{
    public partial class Sequence : BaseControl, IAssetControl
    {
        Dictionary<string, Lifeline> lifelines = new Dictionary<string, Lifeline>();
        public override void OnLogin()
        {
            lifelines.Clear();
            GoDocument doc = goView1.Document;
            doc.AllowLink = false;
            doc.AllowEdit = false;
            doc.AllowResize = false;
            doc.Clear();
        }
        public override void OnLogout()
        {
            lifelines.Clear();
            GoDocument doc = goView1.Document;
            doc.AllowLink = false;
            doc.AllowEdit = false;
            doc.AllowResize = false;
            doc.Clear();
        }
        public Sequence()
        {
            InitializeComponent();
        }
        private IEnumerable<Transaction> source, old_source;

        Lifeline CreateOrGetLifeLine(GoDocument doc, string name)
        {
            if (!lifelines.ContainsKey(name))
            {
                var lf = new Lifeline(name);
                doc.Add(lf);
                lifelines.Add(lf.Text, lf);
                return lf;
            }
            else return lifelines[name];
        }
        void DisplaySequence()
        {
            lifelines.Clear();
            GoDocument doc = goView1.Document;
            doc.AllowLink = false;
            doc.AllowEdit = false;
            doc.AllowResize = false;
            doc.Clear();
            goView1.Invoke(new Action(() =>
            {
                int i = 0;
                foreach (var transaction in source)
                {
                    var s = CreateOrGetLifeLine(doc, transaction.Source);
                    var t = CreateOrGetLifeLine(doc, transaction.Target);

                    var m = new Message(++i, s, t, "Buy ("+transaction.State.ToString().Remove(0, transaction.State.ToString().LastIndexOf('.') + 1) + ")", 2, transaction);
                    m.OnMessageClicked += message =>
                    {
                        foreach (var transactionControl in Registry.GetControls<ITransactionControl>())
                            transactionControl.Changed(message);
                    };
                    doc.Add(m);
                }

                var margin = 300;
                foreach (var lifeline in lifelines)
                {
                    lifeline.Value.Left = margin;
                    margin *= 2;
                }
                doc.Bounds = doc.ComputeBounds();
                goView1.DocPosition = doc.TopLeft;

                goView1.GridUnboundedSpots = GoObject.BottomLeft | GoObject.BottomRight;
                goView1.Grid.Top = Lifeline.LineStart;
                goView1.GridOriginY = Lifeline.LineStart;
                goView1.GridCellSizeHeight = Lifeline.MessageSpacing;

                // support undo/redo
                doc.UndoManager = new GoUndoManager();
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
                if (old_source == null || (source?.Count() != old_source?.Count()) || !(source.All(sx => old_source.Any(y => sx.SourceIndex == y.SourceIndex && sx.TargetIndex == y.TargetIndex))))
                    DisplaySequence();
                old_source = source;
            }
            else if (x is Asset)
            {
                var asset = x as Asset;
                source = Account.Contract.GetTransactionsByAsset(asset.Index);
                if (old_source == null|| (source?.Count() != old_source?.Count()) || !(source.All(sx => old_source.Any(y => sx.SourceIndex == y.SourceIndex && sx.TargetIndex == y.TargetIndex))))
                    DisplaySequence();
                old_source = source;
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

        private void timer1_Tick(object sender, EventArgs e)
        {
       
            if (SelectedAsset == null && Account != null)
                UpdateTransactions(Account);
            else if (SelectedAsset != null && Account != null)
                UpdateTransactions(SelectedAsset);
            // get by asset
        }



        private Asset SelectedAsset;

        public void Changed(Asset a)
        {

            SelectedAsset = a;
  
        }
    }
}
