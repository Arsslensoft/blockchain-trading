using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using Trader.Core;
using Trader.Interfaces;

namespace Trader.Controls
{
    public partial class BaseControl : UserControl, IAccountControl
    {
        public BaseControl()
        {
            InitializeComponent();
        }

        public void BindTo(DockContainerItem item)
        {
            Dock = DockStyle.Fill;
            (item.Control as PanelDockContainer).Controls.Add(this);
        }

        public void AddAsTab(string name,Bar bar)
        {
            var i = new DockContainerItem(name);
            i.Text = name;
            var panel = new PanelDockContainer();
            panel.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            panel.DisabledBackColor = System.Drawing.Color.Empty;
            panel.Location = new System.Drawing.Point(3, 28);
            panel.Name = name +"Container";
            panel.Size = new System.Drawing.Size(688, 234);
            panel.Style.Alignment = System.Drawing.StringAlignment.Center;
            panel.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            panel.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            panel.Style.GradientAngle = 90;
            panel.Dock =  DockStyle.Fill;
            i.Control = panel;
            bar.Controls.Add(panel);
            bar.Items.Add(i);

        }

        public IControlRegistry Registry => Program.MainInstance;
        public Account Account => Program.MainInstance.Account;

        public virtual void OnLogout()
        {
            
        }

        public virtual void OnLogin()
        {
          
        }
    }
}
