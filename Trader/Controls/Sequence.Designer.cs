namespace Trader.Controls
{
    partial class Sequence
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
            this.goView1 = new Northwoods.Go.GoView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // goView1
            // 
            this.goView1.ArrowMoveLarge = 10F;
            this.goView1.ArrowMoveSmall = 1F;
            this.goView1.BackColor = System.Drawing.Color.White;
            this.goView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.goView1.DragsRealtime = true;
            this.goView1.GridCellSizeHeight = 15F;
            this.goView1.GridOriginY = 75F;
            this.goView1.GridStyle = Northwoods.Go.GoViewGridStyle.HorizontalLine;
            this.goView1.GridUnboundedSpots = 24;
            this.goView1.Location = new System.Drawing.Point(0, 0);
            this.goView1.Name = "goView1";
            this.goView1.Size = new System.Drawing.Size(752, 400);
            this.goView1.TabIndex = 2;
            this.goView1.Text = "goView1";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Sequence
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.goView1);
            this.Name = "Sequence";
            this.Size = new System.Drawing.Size(752, 400);
            this.ResumeLayout(false);

        }

        #endregion

        private Northwoods.Go.GoView goView1;
        private System.Windows.Forms.Timer timer1;
    }
}
