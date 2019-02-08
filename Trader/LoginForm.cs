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
using Trader.Core;

namespace Trader
{
    public partial class LoginForm : DevComponents.DotNetBar.Metro.MetroForm
    {
        public LoginForm(string url, string adr, int time, string cadr, string abi)
        {
            InitializeComponent();
            this.url.Text = url;
            textBoxX2.Text = "";
            textBoxX1.Text = adr;
            integerInput1.Value = time;
            textBoxX3.Text = cadr;
            textBoxX4.Text = abi;
        }
        public LoginForm()
        {
            InitializeComponent();
        }

        async Task Login(string url, string adr, string pass, int time, string cadr, string abi)
        {
            try
            {
                var acc = await Account.Login(url, adr, pass, time);
                if (acc != null)
                {
                 acc.LoadContract(cadr, abi);
                    this.Invoke(new Action(() =>
                    {
                        Program.MainInstance.SetupAccount(acc);
                        Program.MainInstance.UpdateAccounts(new SavedAccount(){Address = acc.Address, ContractABI =  abi, ContractAddress = cadr, Time = time, Url = url});
                        this.Close();

                    }));
                }
                else MessageBoxEx.Show("Failed to login for an unknown reason", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Thread LoginThread;
        private void buttonX2_Click(object sender, EventArgs e)
        {
            if(LoginThread != null && LoginThread.IsAlive)
                MessageBoxEx.Show("Please wait while we log you in", "Login", MessageBoxButtons.OK, MessageBoxIcon.Information);

            LoginThread = new Thread(() =>
            {
                var t = Login(url.Text, textBoxX1.Text, textBoxX2.Text, integerInput1.Value, textBoxX3.Text, textBoxX4.Text);
                t.Wait();
            });
            LoginThread.SetApartmentState(ApartmentState.MTA);
            LoginThread.Start();
         
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (LoginThread != null && LoginThread.IsAlive) LoginThread.Abort();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                textBoxX4.Text = openFileDialog1.FileName;
        }

    
    }
}
