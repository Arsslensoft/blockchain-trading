using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro;
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

namespace Trader
{
    public partial class AddAsset : MetroForm
    {
        public AddAsset()
        {
            InitializeComponent();
        }
        async Task Register(string id, int price, int qt)
        {
            try
            {
                var acc = await Program.MainInstance.Account.Contract.RegisterAsync(new Core.Models.Asset { Id=id, Price = price, Quantity = qt});
                    this.Invoke(new Action(() =>
                    {
                        this.Close();
                    }));
         
               
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Thread AddThread;

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (AddThread != null && AddThread.IsAlive)
                MessageBoxEx.Show("Please wait while we register your asset", "Registration", MessageBoxButtons.OK, MessageBoxIcon.Information);
            AddThread = new Thread(() =>
            {
                var t = Register(textBoxX1.Text, integerInput1.Value, integerInput2.Value);
                t.Wait();
            });
            AddThread.SetApartmentState(ApartmentState.MTA);
            AddThread.Start();
        }
    }
}
