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

namespace Trader
{
    public partial class Register : MetroForm
    {
        public Register()
        {
            InitializeComponent();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                textBoxX4.Text = openFileDialog1.FileName;
        }
        async Task RegisterAccount(string url, string pass, long time, string cadr, string abi)
        {
            try
            {
                var acc = await Account.Register(url, pass,  time);
                if (acc != null)
                {
                    acc.LoadContract(cadr, abi);
                    this.Invoke(new Action(() =>
                    {
                        Program.MainInstance.SetupAccount(acc);
                        this.Close();
                        if(MessageBoxEx.Show("Your account has been successfully created"+Environment.NewLine + "Do you want to save it's address in the clipboard?", "Registration", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                            Clipboard.SetText(Program.MainInstance.Account.Address);
                    }));
                }
                else MessageBoxEx.Show("Failed to register and login for an unknown reason", "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private Thread RegisterThread;
        private void buttonX2_Click(object sender, EventArgs e)
        {
            if (RegisterThread != null && RegisterThread.IsAlive)
                MessageBoxEx.Show("Please wait while we register you and log you in", "Register", MessageBoxButtons.OK, MessageBoxIcon.Information);
            RegisterThread = new Thread(() =>
            {
                var t = RegisterAccount(url.Text, textBoxX2.Text,  integerInput1.Value, textBoxX3.Text, textBoxX4.Text);
                t.Wait();
            });
            RegisterThread.SetApartmentState(ApartmentState.MTA);
            RegisterThread.Start();

        }


    }
}
