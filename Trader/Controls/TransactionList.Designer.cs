namespace Trader.Controls
{
    partial class TransactionList
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TransactionList));
            this.listViewEx1 = new DevComponents.DotNetBar.Controls.ListViewEx();
            this.desccol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.filecol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.linecol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columncol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.projcol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.slider2 = new DevComponents.DotNetBar.Controls.Slider();
            this.slider1 = new DevComponents.DotNetBar.Controls.Slider();
            this.panel2 = new System.Windows.Forms.Panel();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewEx1
            // 
            this.listViewEx1.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.listViewEx1.Border.Class = "ListViewBorder";
            this.listViewEx1.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.listViewEx1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.desccol,
            this.filecol,
            this.linecol,
            this.columncol,
            this.projcol,
            this.columnHeader1});
            this.listViewEx1.DisabledBackColor = System.Drawing.Color.Empty;
            this.listViewEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewEx1.ForeColor = System.Drawing.Color.Black;
            this.listViewEx1.FullRowSelect = true;
            this.listViewEx1.Location = new System.Drawing.Point(0, 0);
            this.listViewEx1.Name = "listViewEx1";
            this.listViewEx1.Size = new System.Drawing.Size(939, 460);
            this.listViewEx1.SmallImageList = this.imageList1;
            this.listViewEx1.TabIndex = 3;
            this.listViewEx1.UseCompatibleStateImageBehavior = false;
            this.listViewEx1.View = System.Windows.Forms.View.Details;
            this.listViewEx1.SelectedIndexChanged += new System.EventHandler(this.listViewEx1_SelectedIndexChanged);
            // 
            // desccol
            // 
            this.desccol.Text = "Source";
            this.desccol.Width = 144;
            // 
            // filecol
            // 
            this.filecol.Text = "Target";
            this.filecol.Width = 200;
            // 
            // linecol
            // 
            this.linecol.Text = "Price";
            // 
            // columncol
            // 
            this.columncol.Text = "Quantity";
            // 
            // projcol
            // 
            this.projcol.Text = "Date";
            this.projcol.Width = 114;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "State";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "error");
            this.imageList1.Images.SetKeyName(1, "in");
            this.imageList1.Images.SetKeyName(2, "ok");
            this.imageList1.Images.SetKeyName(3, "out");
            this.imageList1.Images.SetKeyName(4, "pending");
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.slider2);
            this.panel1.Controls.Add(this.slider1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(939, 31);
            this.panel1.TabIndex = 4;
            this.panel1.Visible = false;
            // 
            // slider2
            // 
            // 
            // 
            // 
            this.slider2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.slider2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.slider2.LabelWidth = 150;
            this.slider2.Location = new System.Drawing.Point(0, 0);
            this.slider2.Name = "slider2";
            this.slider2.Size = new System.Drawing.Size(629, 31);
            this.slider2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.slider2.TabIndex = 1;
            this.slider2.Text = "Transaction per page";
            this.slider2.Value = 0;
            this.slider2.ValueChanged += new System.EventHandler(this.slider2_ValueChanged);
            // 
            // slider1
            // 
            // 
            // 
            // 
            this.slider1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.slider1.Dock = System.Windows.Forms.DockStyle.Right;
            this.slider1.LabelWidth = 80;
            this.slider1.Location = new System.Drawing.Point(629, 0);
            this.slider1.Name = "slider1";
            this.slider1.Size = new System.Drawing.Size(310, 31);
            this.slider1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.slider1.TabIndex = 0;
            this.slider1.Text = "Page";
            this.slider1.Value = 0;
            this.slider1.ValueChanged += new System.EventHandler(this.slider2_ValueChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.listViewEx1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 31);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(939, 460);
            this.panel2.TabIndex = 5;
            // 
            // TransactionList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "TransactionList";
            this.Size = new System.Drawing.Size(939, 491);
            this.SizeChanged += new System.EventHandler(this.TransactionList_SizeChanged);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.ListViewEx listViewEx1;
        private System.Windows.Forms.ColumnHeader desccol;
        private System.Windows.Forms.ColumnHeader filecol;
        private System.Windows.Forms.ColumnHeader linecol;
        private System.Windows.Forms.ColumnHeader columncol;
        private System.Windows.Forms.ColumnHeader projcol;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel1;
        private DevComponents.DotNetBar.Controls.Slider slider2;
        private DevComponents.DotNetBar.Controls.Slider slider1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}
