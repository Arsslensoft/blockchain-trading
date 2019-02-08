namespace Trader.Controls
{
    partial class DistributionGraph
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DistributionGraph));
            this.treeGX1 = new DevComponents.Tree.TreeGX();
            this.node1 = new DevComponents.Tree.Node();
            this.nodeConnector1 = new DevComponents.Tree.NodeConnector();
            this.nodeConnector2 = new DevComponents.Tree.NodeConnector();
            this.elementStyle1 = new DevComponents.Tree.ElementStyle();
            this.ElementStyle2 = new DevComponents.Tree.ElementStyle();
            this.ElementStyle3 = new DevComponents.Tree.ElementStyle();
            this.ElementStyle4 = new DevComponents.Tree.ElementStyle();
            this.ElementStyle5 = new DevComponents.Tree.ElementStyle();
            this.ElementStyle6 = new DevComponents.Tree.ElementStyle();
            this.ElementStyle7 = new DevComponents.Tree.ElementStyle();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.treeGX1)).BeginInit();
            this.SuspendLayout();
            // 
            // treeGX1
            // 
            this.treeGX1.AllowDrop = true;
            this.treeGX1.CommandBackColorGradientAngle = 90;
            this.treeGX1.CommandMouseOverBackColor2SchemePart = DevComponents.Tree.eColorSchemePart.ItemHotBackground2;
            this.treeGX1.CommandMouseOverBackColorGradientAngle = 90;
            this.treeGX1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeGX1.ExpandLineColorSchemePart = DevComponents.Tree.eColorSchemePart.BarDockedBorder;
            this.treeGX1.ImageList = this.imageList1;
            this.treeGX1.Location = new System.Drawing.Point(0, 0);
            this.treeGX1.Name = "treeGX1";
            this.treeGX1.Nodes.AddRange(new DevComponents.Tree.Node[] {
            this.node1});
            this.treeGX1.NodesConnector = this.nodeConnector2;
            this.treeGX1.NodeStyle = this.elementStyle1;
            this.treeGX1.PathSeparator = ";";
            this.treeGX1.RootConnector = this.nodeConnector1;
            this.treeGX1.Size = new System.Drawing.Size(759, 347);
            this.treeGX1.Styles.Add(this.elementStyle1);
            this.treeGX1.Styles.Add(this.ElementStyle3);
            this.treeGX1.Styles.Add(this.ElementStyle2);
            this.treeGX1.Styles.Add(this.ElementStyle4);
            this.treeGX1.Styles.Add(this.ElementStyle5);
            this.treeGX1.Styles.Add(this.ElementStyle6);
            this.treeGX1.Styles.Add(this.ElementStyle7);
            this.treeGX1.SuspendPaint = false;
            this.treeGX1.TabIndex = 0;
            this.treeGX1.Text = "treeGX1";
            this.treeGX1.NodeClick += new DevComponents.Tree.TreeGXNodeMouseEventHandler(this.treeGX1_NodeClick);
            // 
            // node1
            // 
            this.node1.Expanded = true;
            this.node1.ImageIndex = 1;
            this.node1.Name = "node1";
            this.node1.Text = "<b><font size=\"+4\"> Market</font></b>";
            // 
            // ElementStyle3
            // 
            this.ElementStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(230)))), ((int)(((byte)(247)))));
            this.ElementStyle3.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(168)))), ((int)(((byte)(228)))));
            this.ElementStyle3.BackColorGradientAngle = 90;
            this.ElementStyle3.BorderBottom = DevComponents.Tree.eStyleBorderType.Solid;
            this.ElementStyle3.BorderBottomWidth = 1;
            this.ElementStyle3.BorderColor = System.Drawing.Color.DarkGray;
            this.ElementStyle3.BorderLeft = DevComponents.Tree.eStyleBorderType.Solid;
            this.ElementStyle3.BorderLeftWidth = 1;
            this.ElementStyle3.BorderRight = DevComponents.Tree.eStyleBorderType.Solid;
            this.ElementStyle3.BorderRightWidth = 1;
            this.ElementStyle3.BorderTop = DevComponents.Tree.eStyleBorderType.Solid;
            this.ElementStyle3.BorderTopWidth = 1;
            this.ElementStyle3.CornerDiameter = 4;
            this.ElementStyle3.CornerType = DevComponents.Tree.eCornerType.Rounded;
            this.ElementStyle3.Description = "Blue";
            this.ElementStyle3.Name = "ElementStyle3";
            this.ElementStyle3.PaddingBottom = 3;
            this.ElementStyle3.PaddingLeft = 3;
            this.ElementStyle3.PaddingRight = 3;
            this.ElementStyle3.PaddingTop = 3;
            this.ElementStyle3.TextColor = System.Drawing.Color.Black;   // 
            // ElementStyle2
            // 
            this.ElementStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(108)))), ((int)(((byte)(152)))));
            this.ElementStyle2.BackColor2 = System.Drawing.Color.Navy;
            this.ElementStyle2.BackColorGradientAngle = 90;
            this.ElementStyle2.BorderBottom = DevComponents.Tree.eStyleBorderType.Solid;
            this.ElementStyle2.BorderBottomWidth = 1;
            this.ElementStyle2.BorderColor = System.Drawing.Color.Navy;
            this.ElementStyle2.BorderLeft = DevComponents.Tree.eStyleBorderType.Solid;
            this.ElementStyle2.BorderLeftWidth = 1;
            this.ElementStyle2.BorderRight = DevComponents.Tree.eStyleBorderType.Solid;
            this.ElementStyle2.BorderRightWidth = 1;
            this.ElementStyle2.BorderTop = DevComponents.Tree.eStyleBorderType.Solid;
            this.ElementStyle2.BorderTopWidth = 1;
            this.ElementStyle2.CornerDiameter = 4;
            this.ElementStyle2.CornerType = DevComponents.Tree.eCornerType.Rounded;
            this.ElementStyle2.Description = "BlueNight";
            this.ElementStyle2.Name = "ElementStyle2";
            this.ElementStyle2.PaddingBottom = 3;
            this.ElementStyle2.PaddingLeft = 3;
            this.ElementStyle2.PaddingRight = 3;
            this.ElementStyle2.PaddingTop = 3;
            this.ElementStyle2.TextColor = System.Drawing.Color.White;// 
            // ElementStyle4
            // 
            this.ElementStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(225)))), ((int)(((byte)(226)))));
            this.ElementStyle4.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(149)))), ((int)(((byte)(151)))));
            this.ElementStyle4.BackColorGradientAngle = 90;
            this.ElementStyle4.BorderBottom = DevComponents.Tree.eStyleBorderType.Solid;
            this.ElementStyle4.BorderBottomWidth = 1;
            this.ElementStyle4.BorderColor = System.Drawing.Color.DarkGray;
            this.ElementStyle4.BorderLeft = DevComponents.Tree.eStyleBorderType.Solid;
            this.ElementStyle4.BorderLeftWidth = 1;
            this.ElementStyle4.BorderRight = DevComponents.Tree.eStyleBorderType.Solid;
            this.ElementStyle4.BorderRightWidth = 1;
            this.ElementStyle4.BorderTop = DevComponents.Tree.eStyleBorderType.Solid;
            this.ElementStyle4.BorderTopWidth = 1;
            this.ElementStyle4.CornerDiameter = 4;
            this.ElementStyle4.CornerType = DevComponents.Tree.eCornerType.Rounded;
            this.ElementStyle4.Description = "Red";
            this.ElementStyle4.Name = "ElementStyle4";
            this.ElementStyle4.PaddingBottom = 3;
            this.ElementStyle4.PaddingLeft = 3;
            this.ElementStyle4.PaddingRight = 3;
            this.ElementStyle4.PaddingTop = 3;
            this.ElementStyle4.TextColor = System.Drawing.Color.Black; // 
            // ElementStyle5
            // 
            this.ElementStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(244)))), ((int)(((byte)(213)))));
            this.ElementStyle5.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(216)))), ((int)(((byte)(105)))));
            this.ElementStyle5.BackColorGradientAngle = 90;
            this.ElementStyle5.BorderBottom = DevComponents.Tree.eStyleBorderType.Solid;
            this.ElementStyle5.BorderBottomWidth = 1;
            this.ElementStyle5.BorderColor = System.Drawing.Color.DarkGray;
            this.ElementStyle5.BorderLeft = DevComponents.Tree.eStyleBorderType.Solid;
            this.ElementStyle5.BorderLeftWidth = 1;
            this.ElementStyle5.BorderRight = DevComponents.Tree.eStyleBorderType.Solid;
            this.ElementStyle5.BorderRightWidth = 1;
            this.ElementStyle5.BorderTop = DevComponents.Tree.eStyleBorderType.Solid;
            this.ElementStyle5.BorderTopWidth = 1;
            this.ElementStyle5.CornerDiameter = 4;
            this.ElementStyle5.CornerType = DevComponents.Tree.eCornerType.Rounded;
            this.ElementStyle5.Description = "Yellow";
            this.ElementStyle5.Name = "ElementStyle5";
            this.ElementStyle5.PaddingBottom = 3;
            this.ElementStyle5.PaddingLeft = 3;
            this.ElementStyle5.PaddingRight = 3;
            this.ElementStyle5.PaddingTop = 3;
            this.ElementStyle5.TextColor = System.Drawing.Color.Black;   // 
            // ElementStyle6
            // 
            this.ElementStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(240)))), ((int)(((byte)(226)))));
            this.ElementStyle6.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(201)))), ((int)(((byte)(151)))));
            this.ElementStyle6.BackColorGradientAngle = 90;
            this.ElementStyle6.BorderBottom = DevComponents.Tree.eStyleBorderType.Solid;
            this.ElementStyle6.BorderBottomWidth = 1;
            this.ElementStyle6.BorderColor = System.Drawing.Color.DarkGray;
            this.ElementStyle6.BorderLeft = DevComponents.Tree.eStyleBorderType.Solid;
            this.ElementStyle6.BorderLeftWidth = 1;
            this.ElementStyle6.BorderRight = DevComponents.Tree.eStyleBorderType.Solid;
            this.ElementStyle6.BorderRightWidth = 1;
            this.ElementStyle6.BorderTop = DevComponents.Tree.eStyleBorderType.Solid;
            this.ElementStyle6.BorderTopWidth = 1;
            this.ElementStyle6.CornerDiameter = 4;
            this.ElementStyle6.CornerType = DevComponents.Tree.eCornerType.Rounded;
            this.ElementStyle6.Description = "Green";
            this.ElementStyle6.Name = "ElementStyle6";
            this.ElementStyle6.PaddingBottom = 3;
            this.ElementStyle6.PaddingLeft = 3;
            this.ElementStyle6.PaddingRight = 3;
            this.ElementStyle6.PaddingTop = 3;
            this.ElementStyle6.TextColor = System.Drawing.Color.Black;            // 
            // ElementStyle7
            // 
            this.ElementStyle7.Description = "White";
            this.ElementStyle7.Name = "ElementStyle7";
            this.ElementStyle7.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            // 
            // nodeConnector1
            // 
            this.nodeConnector1.LineWidth = 5;
            // 
            // nodeConnector2
            // 
            this.nodeConnector2.LineWidth = 5;
            // 
            // elementStyle1
            // 
            this.elementStyle1.BackColor2SchemePart = DevComponents.Tree.eColorSchemePart.BarBackground2;
            this.elementStyle1.BackColorGradientAngle = 90;
            this.elementStyle1.BackColorSchemePart = DevComponents.Tree.eColorSchemePart.BarBackground;
            this.elementStyle1.BorderBottom = DevComponents.Tree.eStyleBorderType.Solid;
            this.elementStyle1.BorderBottomWidth = 1;
            this.elementStyle1.BorderColorSchemePart = DevComponents.Tree.eColorSchemePart.BarDockedBorder;
            this.elementStyle1.BorderLeft = DevComponents.Tree.eStyleBorderType.Solid;
            this.elementStyle1.BorderLeftWidth = 1;
            this.elementStyle1.BorderRight = DevComponents.Tree.eStyleBorderType.Solid;
            this.elementStyle1.BorderRightWidth = 1;
            this.elementStyle1.BorderTop = DevComponents.Tree.eStyleBorderType.Solid;
            this.elementStyle1.BorderTopWidth = 1;
            this.elementStyle1.CornerDiameter = 4;
            this.elementStyle1.CornerType = DevComponents.Tree.eCornerType.Rounded;
            this.elementStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.elementStyle1.Name = "elementStyle1";
            this.elementStyle1.PaddingBottom = 3;
            this.elementStyle1.PaddingLeft = 3;
            this.elementStyle1.PaddingRight = 3;
            this.elementStyle1.PaddingTop = 3;
            this.elementStyle1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "asset");
            this.imageList1.Images.SetKeyName(1, "market");
            this.imageList1.Images.SetKeyName(2, "biz");
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // DistributionGraph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeGX1);
            this.Name = "DistributionGraph";
            this.Size = new System.Drawing.Size(759, 347);
            ((System.ComponentModel.ISupportInitialize)(this.treeGX1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.Tree.TreeGX treeGX1;
        private DevComponents.Tree.Node node1;
        private DevComponents.Tree.NodeConnector nodeConnector2;
        private DevComponents.Tree.ElementStyle elementStyle1;
        private DevComponents.Tree.NodeConnector nodeConnector1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Timer timer1;
        internal DevComponents.Tree.ElementStyle ElementStyle2;
        internal DevComponents.Tree.ElementStyle ElementStyle3;
        internal DevComponents.Tree.ElementStyle ElementStyle4;
        internal DevComponents.Tree.ElementStyle ElementStyle5;
        internal DevComponents.Tree.ElementStyle ElementStyle6;
        internal DevComponents.Tree.ElementStyle ElementStyle7;
    }
}
