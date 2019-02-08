using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro;
using Trader.Core;
using Trader.Core.Models;

namespace Trader
{
    public partial class AddTransaction : MetroForm
    {
        public AddTransaction()
        {
            InitializeComponent();
        }
        private Thread GetInfoThread;
        void GetInfo(object x)
        {
            var account = x as Account;
            var assets = account.Contract.GetAssetsAsync();
            assets.Wait();
            foreach (var asset in assets.Result)
            {
             
                this.Invoke(new Action(() =>
                {
                    var item = new ComboBoxItem(asset.Id);
                    item.Text = asset.Id;
                    item.Tag = asset;
                    if (asset.Address.ToLower() == account.Address.ToLower())
                        comboBoxEx1.Items.Add(item);

                    comboBoxEx2.Items.Add(item);
                }));
         
            }

        }

        void LoadSources(Account account)
        {
            if (account == null) return;
            if (GetInfoThread != null && GetInfoThread.IsAlive)
                return;

            GetInfoThread = new Thread(GetInfo);
            GetInfoThread.SetApartmentState(ApartmentState.MTA);
            GetInfoThread.Start(account);
        }

        void Exec(object x)
        {
            var account = x as Account;
            string source=null, target = null;
            int quantity=0;
            this.Invoke(new Action(() =>
            {
                source = comboBoxEx1.Text;
                target = comboBoxEx2.Text;
                quantity = integerInput1.Value;
            }));
            try
            {
                var t = account.Contract.TransactAsync(source,target,quantity);
                    t.Wait();
                this.Invoke(new Action(this.Close));
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "Transaction Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void ExecuteTransaction(Account account)
        {
            if (account == null) return;
            if (GetInfoThread != null && GetInfoThread.IsAlive)
                MessageBoxEx.Show("Please wait while we execute the previous transaction", "Transaction", MessageBoxButtons.OK, MessageBoxIcon.Information);
            GetInfoThread = new Thread(Exec);
            GetInfoThread.SetApartmentState(ApartmentState.MTA);
            GetInfoThread.Start(account);
        }
        private void buttonX1_Click(object sender, EventArgs e)
        {
            ExecuteTransaction(Program.MainInstance.Account);
        }

        private void AddTransaction_Load(object sender, EventArgs e)
        {
            LoadSources(Program.MainInstance.Account);
        }

        private void comboBoxEx2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var asset = (comboBoxEx2.SelectedItem as ComboBoxItem)?.Tag as Asset;
            if (asset != null)
            {
                if (integerInput1.Value > asset.Quantity)
                {
                    labelX4.Text = "This transaction will be partially executed, " +
                                   (integerInput1.Value - asset.Quantity) + " will be rejected.";
                    labelX4.Visible = true;
                }
                else if (comboBoxEx1.Text == comboBoxEx2.Text)
                {
                    labelX4.Text = "This transaction will be rejected cannot transfer stocks within the same asset" ;
                    labelX4.Visible = true;
                }
                else labelX4.Visible = false;
            }
            else labelX4.Visible = false;
        }
    }
}
