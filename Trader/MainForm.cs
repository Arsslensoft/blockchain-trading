using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro;
using Newtonsoft.Json;
using TachyonFix.Core.Reporting;
using Trader.Controls;
using Trader.Core;
using Trader.Core.Models;
using Trader.Core.Reporting;
using Trader.Interfaces;

namespace Trader
{
    public partial class MainForm : MetroForm, IControlRegistry
    {
        public Account Account { get; set; }
        public MainForm()
        {
            InitializeComponent();
          

        }

        public void LoadAccounts()
        {
            try
            {
                buttonItem10.Visible = File.Exists(Application.StartupPath + @"\Accounts.dat");
                if (buttonItem10.Visible)
                {
                    buttonItem10.SubItems.Clear();
                    foreach (var acc in JsonConvert
                        .DeserializeObject<SavedAccounts>(File.ReadAllText(Application.StartupPath + @"\Accounts.dat"))
                        .Accounts)
                    {
                        ButtonItem i = new ButtonItem(acc.Address, acc.Address);
                        i.Tag = acc;
                        buttonItem10.SubItems.Add(i);
                        i.Click += (sender, args) =>
                        {
                            var button = sender as ButtonItem;
                            var a = button.Tag as SavedAccount;
                            if (Account == null)
                                new LoginForm(a.Url, a.Address, a.Time, a.ContractAddress, a.ContractABI).ShowDialog(
                                    this);
                            else UnloadAccount();
                        };
                    }
                }
            }
            catch
            {

            }
        }

        public void UpdateAccounts(SavedAccount account)
        {
            try
            {
                SavedAccounts sa = new SavedAccounts();
                if (File.Exists(Application.StartupPath + @"\Accounts.dat"))
                    sa = JsonConvert.DeserializeObject<SavedAccounts>(
                        File.ReadAllText(Application.StartupPath + @"\Accounts.dat"));
                sa.Accounts.RemoveAll(x => x.Address.ToLower() == account.Address.ToLower());
                sa.Accounts.Insert(0, account);
                File.WriteAllText(Application.StartupPath + @"\Accounts.dat", JsonConvert.SerializeObject(sa));
                LoadAccounts();
            }
            catch
            {

            }
        }
        public List<Control> RegistredControls { get; set; } = new List<Control>();
        public IEnumerable<T> GetControls<T>()
        {
            return RegistredControls.Where(x => x is T).Cast<T>();
        }

        public void InitializeRegistry()
        {
            var transactions = new TransactionList();
            transactions.BindTo(Transactions);
            RegistredControls.Add(transactions);

            var ad = new AssetDetails();
            ad.BindTo(Properties);
            RegistredControls.Add(ad);

            var asl = new AssetsList();
            asl.BindTo(Assets);
            RegistredControls.Add(asl);

            var st = new Stats();
            st.BindTo(Statistics);
            RegistredControls.Add(st);

            var acd = new AccountDetails();
            acd.BindTo(AccountTab);
            RegistredControls.Add(acd);


            var dg = new DistributionGraph();
            dg.BindTo(Network);
            RegistredControls.Add(dg);

            var seq = new Sequence();
            seq.BindTo(Sequence);
            RegistredControls.Add(seq);

            var ni = new NodeInfoDetails();
            ni.BindTo(NodeInfo);
            RegistredControls.Add(ni);
        }


        public void SetupAccount(Account account)
        {
            Account = account;
            buttonItem18.Text = "Logout";
            buttonItem8.Text = "Logout";
            labelItem1.Text = "Connected";
            labelItem2.Visible = true;
            Network.Visible = true;
            Transactions.Visible = true;
            Statistics.Visible = true;
            Sequence.Visible = true;
            NodeInfo.Visible = true;
            NetworkGraph.Visible = true;
            buttonItem8.Image = Trader.Properties.Resources.icons8_export_52;
            buttonItem11.Visible = true;
            buttonItem14.Text = "Start Mining";
            foreach (var accountControl in GetControls<IAccountControl>())
                accountControl.OnLogin();
        }

        public void UnloadAccount()
        {
            buttonItem8.Image = Trader.Properties.Resources.icons8_import_52;
            buttonItem18.Text = "Login";
            buttonItem8.Text = "Login";
            labelItem1.Text = "Ready";
            labelItem2.Visible = false;
            Network.Visible = false;
            Transactions.Visible = false; buttonItem11.Visible = false;
            Statistics.Visible = false; Sequence.Visible = false;
            NodeInfo.Visible = false;
            NetworkGraph.Visible = false;
            Account = null;
            foreach (var accountControl in GetControls<IAccountControl>())
                accountControl.OnLogout();
            
        }

        private void buttonItem26_Click(object sender, EventArgs e)
        {
            new Aboutfrm().ShowDialog(this);
        }
        private void buttonItem18_Click(object sender, EventArgs e)
        {
            if (Account == null)
                new LoginForm().ShowDialog(this);
            else UnloadAccount();
        }

        private void buttonItem4_Click(object sender, EventArgs e)
        {
            if (Account != null)
                new AddAsset().ShowDialog(this);
            else MessageBoxEx.Show("You can't register an asset while you are offline", "Registration", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void buttonItem2_Click(object sender, EventArgs e)
        {
            // register
            if (Account == null)
                new Register().ShowDialog(this);
            else MessageBoxEx.Show("You can't register an account while you are online", "Register Account", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void buttonItem6_Click(object sender, EventArgs e)
        {
            // deploy contract
            if (Account != null)
                new DeployContract().ShowDialog(this);
            else MessageBoxEx.Show("You can't deploy a contract while you are offline", "Deployment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void buttonItem12_Click(object sender, EventArgs e)
        {
            // transact
            if (Account != null)
                new AddTransaction().ShowDialog(this);
            else MessageBoxEx.Show("You can't transact while you are offline", "Transaction", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void buttonItem13_Click(object sender, EventArgs e)
        {
           // export to excel
            var ee = new ExcelExporter();
            foreach (var exportable in GetControls<IExportable>())
                exportable.Export(ee);
        }

        private void buttonItem7_Click(object sender, EventArgs e)
        {
            // export to pdf
            var pe = new PDFExporter();
            foreach (var exportable in GetControls<IExportable>())
                exportable.Export(pe);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadAccounts();
            InitializeRegistry();
        }
        private Thread GetInfoThread;
        void GetInfo()
        {

            var assets = Account.Web3.Net.PeerCount.SendRequestAsync();
            assets.Wait();
                this.Invoke(new Action(() =>
                {
                    labelItem1.Text = "Connected";
                    labelItem2.Visible = true;
                    labelItem2.Text = "Peers " + assets.Result.Value.ToString();
                }));
        }
        void UpdateNetInfo()
        {
            if (Account == null) return;
            if (GetInfoThread != null && GetInfoThread.IsAlive)
                return;

            GetInfoThread = new Thread(GetInfo);
            GetInfoThread.SetApartmentState(ApartmentState.MTA);
            GetInfoThread.Start();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateNetInfo();
        }
        private Thread MiningThread;

        void Mine()
        {
            try
            {
                if (!Account.Mining)
                {
                    Account.StartMining().Wait();
                    this.Invoke(new Action(() => { buttonItem14.Text = "Stop Mining"; }));
                }
                else
                {
                    Account.StopMining().Wait();
                    this.Invoke(new Action(() => { buttonItem14.Text = "Start Mining"; }));
                }

                

            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "Mining Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }
        private void buttonItem14_Click(object sender, EventArgs e)
        {
            // mine
            if (Account == null) return;
            if (MiningThread != null && MiningThread.IsAlive)
                MessageBoxEx.Show("A mining command is already executing", "Mining", MessageBoxButtons.OK, MessageBoxIcon.Information);

            MiningThread = new Thread(Mine);
            MiningThread.SetApartmentState(ApartmentState.MTA);
            MiningThread.Start();
        }
    }
}