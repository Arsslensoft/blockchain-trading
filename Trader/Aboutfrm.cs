using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro;
using System.Diagnostics;

namespace Trader
{
    public partial class Aboutfrm : MetroForm
    {
        public Aboutfrm()
        {
            InitializeComponent();
        }

        private void Aboutfrm_Load(object sender, EventArgs e)
        {
            try
            {
                labelX4.Text = labelX4.Text.Replace("{user}", Environment.UserName);
            }
            catch
            {

            }

        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("http://www.arsslensoft.com/donate");
            }
            catch
            {

            }
        }
    }
}
