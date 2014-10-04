namespace Könyvtar.Printlib
{
    partial class PrintForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintForm));
            this.printDialog = new System.Windows.Forms.PrintDialog();
            this.printDocument = new System.Drawing.Printing.PrintDocument();
            this.pageSetupDialog = new System.Windows.Forms.PageSetupDialog();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.viewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.btnClose = new System.Windows.Forms.Button();
            this.Export_Button = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboBoxZoom = new System.Windows.Forms.ComboBox();
            this.btnZoomAuto = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.LastPage_Button = new System.Windows.Forms.Button();
            this.NextPage_Button = new System.Windows.Forms.Button();
            this.BackPage_Button = new System.Windows.Forms.Button();
            this.FirstPage_Button = new System.Windows.Forms.Button();
            this.PageNo = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // printDialog
            // 
            this.printDialog.AllowSomePages = true;
            this.printDialog.Document = this.printDocument;
            this.printDialog.UseEXDialog = true;
            // 
            // pageSetupDialog
            // 
            this.pageSetupDialog.Document = this.printDocument;
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Black;
            this.imageList.Images.SetKeyName(0, "");
            this.imageList.Images.SetKeyName(1, "");
            this.imageList.Images.SetKeyName(2, "page setup.bmp");
            this.imageList.Images.SetKeyName(3, "elejere.bmp");
            this.imageList.Images.SetKeyName(4, "elözö.bmp");
            this.imageList.Images.SetKeyName(5, "következö.bmp");
            this.imageList.Images.SetKeyName(6, "vegere.bmp");
            this.imageList.Images.SetKeyName(7, "export.bmp");
            // 
            // viewer
            // 
            this.viewer.ActiveViewIndex = -1;
            this.viewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.viewer.DisplayGroupTree = false;
            this.viewer.DisplayStatusBar = false;
            this.viewer.DisplayToolbar = false;
            this.viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewer.EnableToolTips = false;
            this.viewer.Location = new System.Drawing.Point(0, 0);
            this.viewer.Name = "viewer";
            this.viewer.Padding = new System.Windows.Forms.Padding(0, 0, 150, 0);
            this.viewer.SelectionFormula = "";
            this.viewer.ShowCloseButton = false;
            this.viewer.ShowExportButton = false;
            this.viewer.ShowGotoPageButton = false;
            this.viewer.ShowGroupTreeButton = false;
            this.viewer.ShowPageNavigateButtons = false;
            this.viewer.ShowPrintButton = false;
            this.viewer.ShowRefreshButton = false;
            this.viewer.ShowTextSearchButton = false;
            this.viewer.ShowZoomButton = false;
            this.viewer.Size = new System.Drawing.Size(1059, 623);
            this.viewer.TabIndex = 0;
            this.viewer.ViewTimeSelectionFormula = "";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.ImageIndex = 1;
            this.btnClose.ImageList = this.imageList;
            this.btnClose.Location = new System.Drawing.Point(934, 588);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 23);
            this.btnClose.TabIndex = 24;
            this.btnClose.Text = "Bezár";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // Export_Button
            // 
            this.Export_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Export_Button.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Export_Button.ImageIndex = 7;
            this.Export_Button.ImageList = this.imageList;
            this.Export_Button.Location = new System.Drawing.Point(934, 533);
            this.Export_Button.Name = "Export_Button";
            this.Export_Button.Size = new System.Drawing.Size(100, 49);
            this.Export_Button.TabIndex = 23;
            this.Export_Button.Text = "Export";
            this.Export_Button.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.Export_Button.Click += new System.EventHandler(this.Export_Button_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnPrint.ImageIndex = 0;
            this.btnPrint.ImageList = this.imageList;
            this.btnPrint.Location = new System.Drawing.Point(934, 478);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(100, 49);
            this.btnPrint.TabIndex = 22;
            this.btnPrint.Text = "Nyomtat";
            this.btnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.comboBoxZoom);
            this.groupBox2.Controls.Add(this.btnZoomAuto);
            this.groupBox2.Location = new System.Drawing.Point(913, 111);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(132, 82);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Zoom";
            // 
            // comboBoxZoom
            // 
            this.comboBoxZoom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxZoom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxZoom.FormatString = "123 %";
            this.comboBoxZoom.FormattingEnabled = true;
            this.comboBoxZoom.Items.AddRange(new object[] {
            "200 %",
            "150 %",
            "100 %",
            " 75 %",
            " 50 %",
            " 25 %"});
            this.comboBoxZoom.Location = new System.Drawing.Point(19, 20);
            this.comboBoxZoom.Name = "comboBoxZoom";
            this.comboBoxZoom.Size = new System.Drawing.Size(100, 23);
            this.comboBoxZoom.TabIndex = 11;
            this.comboBoxZoom.SelectedIndexChanged += new System.EventHandler(this.comboBoxZoom_SelectedIndexChanged);
            // 
            // btnZoomAuto
            // 
            this.btnZoomAuto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnZoomAuto.Location = new System.Drawing.Point(19, 49);
            this.btnZoomAuto.Name = "btnZoomAuto";
            this.btnZoomAuto.Size = new System.Drawing.Size(100, 22);
            this.btnZoomAuto.TabIndex = 10;
            this.btnZoomAuto.Text = "Automatikus";
            this.btnZoomAuto.Click += new System.EventHandler(this.btnZoomAuto_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.LastPage_Button);
            this.groupBox1.Controls.Add(this.NextPage_Button);
            this.groupBox1.Controls.Add(this.BackPage_Button);
            this.groupBox1.Controls.Add(this.FirstPage_Button);
            this.groupBox1.Controls.Add(this.PageNo);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(915, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(132, 93);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Oldal";
            // 
            // LastPage_Button
            // 
            this.LastPage_Button.ImageIndex = 6;
            this.LastPage_Button.ImageList = this.imageList;
            this.LastPage_Button.Location = new System.Drawing.Point(100, 57);
            this.LastPage_Button.Name = "LastPage_Button";
            this.LastPage_Button.Size = new System.Drawing.Size(26, 26);
            this.LastPage_Button.TabIndex = 5;
            this.LastPage_Button.UseVisualStyleBackColor = true;
            this.LastPage_Button.Click += new System.EventHandler(this.LastPage_Button_Click);
            // 
            // NextPage_Button
            // 
            this.NextPage_Button.ImageIndex = 5;
            this.NextPage_Button.ImageList = this.imageList;
            this.NextPage_Button.Location = new System.Drawing.Point(68, 57);
            this.NextPage_Button.Name = "NextPage_Button";
            this.NextPage_Button.Size = new System.Drawing.Size(26, 26);
            this.NextPage_Button.TabIndex = 4;
            this.NextPage_Button.UseVisualStyleBackColor = true;
            this.NextPage_Button.Click += new System.EventHandler(this.NextPage_Button_Click);
            // 
            // BackPage_Button
            // 
            this.BackPage_Button.Enabled = false;
            this.BackPage_Button.ImageIndex = 4;
            this.BackPage_Button.ImageList = this.imageList;
            this.BackPage_Button.Location = new System.Drawing.Point(38, 57);
            this.BackPage_Button.Name = "BackPage_Button";
            this.BackPage_Button.Size = new System.Drawing.Size(26, 26);
            this.BackPage_Button.TabIndex = 3;
            this.BackPage_Button.UseVisualStyleBackColor = true;
            this.BackPage_Button.Click += new System.EventHandler(this.BackPage_Button_Click);
            // 
            // FirstPage_Button
            // 
            this.FirstPage_Button.Enabled = false;
            this.FirstPage_Button.ImageIndex = 3;
            this.FirstPage_Button.ImageList = this.imageList;
            this.FirstPage_Button.Location = new System.Drawing.Point(6, 57);
            this.FirstPage_Button.Name = "FirstPage_Button";
            this.FirstPage_Button.Size = new System.Drawing.Size(26, 26);
            this.FirstPage_Button.TabIndex = 2;
            this.FirstPage_Button.UseVisualStyleBackColor = true;
            this.FirstPage_Button.Click += new System.EventHandler(this.FirstPage_Button_Click);
            // 
            // PageNo
            // 
            this.PageNo.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.PageNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PageNo.Location = new System.Drawing.Point(57, 19);
            this.PageNo.Name = "PageNo";
            this.PageNo.Size = new System.Drawing.Size(62, 24);
            this.PageNo.TabIndex = 1;
            this.PageNo.Text = "1";
            this.PageNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "szám:";
            // 
            // PrintForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1059, 623);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.Export_Button);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.viewer);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimizeBox = false;
            this.Name = "PrintForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nyomtatás megtekintése";
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.PrintDialog printDialog;
        public System.Drawing.Printing.PrintDocument printDocument;
        public System.Windows.Forms.PageSetupDialog pageSetupDialog;
        private System.Windows.Forms.ImageList imageList;
        public CrystalDecisions.Windows.Forms.CrystalReportViewer viewer;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button Export_Button;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox comboBoxZoom;
        private System.Windows.Forms.Button btnZoomAuto;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button LastPage_Button;
        private System.Windows.Forms.Button NextPage_Button;
        private System.Windows.Forms.Button BackPage_Button;
        private System.Windows.Forms.Button FirstPage_Button;
        private System.Windows.Forms.Label PageNo;
        private System.Windows.Forms.Label label3;
    }
}