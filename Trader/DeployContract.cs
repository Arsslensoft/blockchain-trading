using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro;
using Nethereum.Web3;
using Trader.Core;

namespace Trader
{
    public partial class DeployContract : MetroForm
    {
        public DeployContract()
        {
            InitializeComponent();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
                textBoxX4.Text = openFileDialog2.FileName;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                textBoxX1.Text = openFileDialog1.FileName;
        }
        private Thread GetInfoThread;

        async Task<string> Deploy(string abiFile, string BinFile, Web3 web3, string sender)
        {
            var contractByteCode = File.ReadAllText(BinFile); ;
            var abi = File.ReadAllText(abiFile);
            var cgas = await web3.Eth.DeployContract.EstimateGasAsync(abi, contractByteCode, sender);
            var receipt = await web3.Eth.DeployContract.SendRequestAndWaitForReceiptAsync(abi, contractByteCode, sender, cgas, null);
        
            return receipt.ContractAddress;
        }
        void Exec(object x)
        {
            var account = x as Account;
            string abiFile = null, binFile = null;
            this.Invoke(new Action(() =>
            {
                abiFile = textBoxX1.Text;
                binFile = textBoxX4.Text;
            }));
            try
            {
                var t = Deploy(abiFile, binFile, account.Web3, account.Address);
                t.Wait();
                this.Invoke(new Action(() =>
                {

                    if (MessageBoxEx.Show("Your contract has been successfully deployed to the ledger"+Environment.NewLine + "Contract Address: "+ t.Result  +Environment.NewLine + "Do you want to copy it in the clipboard?" , "Deploy Contract", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        Clipboard.SetText(t.Result);

                    if (MessageBoxEx.Show("Your contract has been successfully deployed to the ledger" + Environment.NewLine + "Contract Address: " + t.Result + Environment.NewLine + "Do you want to use that contract instead?", "Update Contract", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        Program.MainInstance.Account.ReloadContract(t.Result, abiFile);
                    this.Close();
                }));

            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "Deployment Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void Deploy(Account account)
        {
            if (account == null) return;
            if (GetInfoThread != null && GetInfoThread.IsAlive)
                return;

            GetInfoThread = new Thread(Exec);
            GetInfoThread.SetApartmentState(ApartmentState.MTA);
            GetInfoThread.Start(account);
        }
        private void buttonX3_Click(object sender, EventArgs e)
        {
            Deploy(Program.MainInstance.Account);
        }
    }
}
