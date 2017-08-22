namespace VIR
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.adatokImportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.adatokExportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.nyomtatóBeállításToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.kilépésToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.infoToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.adatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.import = new System.Windows.Forms.ToolStripMenuItem();
            this.export = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.nyomtatoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.kilépésToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extrákToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.egyebToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.adatEv = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.labelUzenetek = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.panel3 = new System.Windows.Forms.Panel();
            this.labelPrg = new System.Windows.Forms.Label();
            this.labelUser = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.imageMenu = new System.Windows.Forms.ImageList(this.components);
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel6 = new System.Windows.Forms.Panel();
            this.menuStrip.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Font = new System.Drawing.Font("Arial", 9F);
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.infoToolStripMenuItem1});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.ShowItemToolTips = true;
            this.menuStrip.Size = new System.Drawing.Size(1016, 25);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.adatokImportToolStripMenuItem,
            this.adatokExportToolStripMenuItem,
            this.toolStripSeparator3,
            this.nyomtatóBeállításToolStripMenuItem,
            this.toolStripSeparator4,
            this.kilépésToolStripMenuItem1});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(43, 24);
            this.toolStripMenuItem1.Text = "File";
            // 
            // adatokImportToolStripMenuItem
            // 
            this.adatokImportToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("adatokImportToolStripMenuItem.Image")));
            this.adatokImportToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.adatokImportToolStripMenuItem.Name = "adatokImportToolStripMenuItem";
            this.adatokImportToolStripMenuItem.Size = new System.Drawing.Size(201, 26);
            this.adatokImportToolStripMenuItem.Text = "Adatok import";
            this.adatokImportToolStripMenuItem.Click += new System.EventHandler(this.import_Click);
            // 
            // adatokExportToolStripMenuItem
            // 
            this.adatokExportToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("adatokExportToolStripMenuItem.Image")));
            this.adatokExportToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.adatokExportToolStripMenuItem.Name = "adatokExportToolStripMenuItem";
            this.adatokExportToolStripMenuItem.Size = new System.Drawing.Size(201, 26);
            this.adatokExportToolStripMenuItem.Text = "Adatok export";
            this.adatokExportToolStripMenuItem.Click += new System.EventHandler(this.export_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(198, 6);
            // 
            // nyomtatóBeállításToolStripMenuItem
            // 
            this.nyomtatóBeállításToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("nyomtatóBeállításToolStripMenuItem.Image")));
            this.nyomtatóBeállításToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.nyomtatóBeállításToolStripMenuItem.Name = "nyomtatóBeállításToolStripMenuItem";
            this.nyomtatóBeállításToolStripMenuItem.Size = new System.Drawing.Size(201, 26);
            this.nyomtatóBeállításToolStripMenuItem.Text = "Nyomtató beállítás";
            this.nyomtatóBeállításToolStripMenuItem.Click += new System.EventHandler(this.nyomtatoToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(198, 6);
            // 
            // kilépésToolStripMenuItem1
            // 
            this.kilépésToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("kilépésToolStripMenuItem1.Image")));
            this.kilépésToolStripMenuItem1.ImageTransparentColor = System.Drawing.Color.White;
            this.kilépésToolStripMenuItem1.Name = "kilépésToolStripMenuItem1";
            this.kilépésToolStripMenuItem1.Size = new System.Drawing.Size(201, 26);
            this.kilépésToolStripMenuItem1.Text = "Kilépés";
            this.kilépésToolStripMenuItem1.Click += new System.EventHandler(this.kilépésToolStripMenuItem_Click);
            // 
            // infoToolStripMenuItem1
            // 
            this.infoToolStripMenuItem1.Name = "infoToolStripMenuItem1";
            this.infoToolStripMenuItem1.Size = new System.Drawing.Size(43, 24);
            this.infoToolStripMenuItem1.Text = "Info";
            this.infoToolStripMenuItem1.Click += new System.EventHandler(this.infoToolStripMenuItem_Click);
            // 
            // adatToolStripMenuItem
            // 
            this.adatToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.import,
            this.export,
            this.toolStripSeparator2,
            this.nyomtatoToolStripMenuItem,
            this.toolStripSeparator1,
            this.kilépésToolStripMenuItem});
            this.adatToolStripMenuItem.Name = "adatToolStripMenuItem";
            this.adatToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.adatToolStripMenuItem.Text = "File";
            // 
            // import
            // 
            this.import.Image = ((System.Drawing.Image)(resources.GetObject("import.Image")));
            this.import.ImageTransparentColor = System.Drawing.Color.Black;
            this.import.Name = "import";
            this.import.Size = new System.Drawing.Size(211, 26);
            this.import.Text = "Adatok import";
            this.import.Click += new System.EventHandler(this.import_Click);
            // 
            // export
            // 
            this.export.Image = ((System.Drawing.Image)(resources.GetObject("export.Image")));
            this.export.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.export.Name = "export";
            this.export.Size = new System.Drawing.Size(211, 26);
            this.export.Text = "Adatok export";
            this.export.Click += new System.EventHandler(this.export_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(208, 6);
            // 
            // nyomtatoToolStripMenuItem
            // 
            this.nyomtatoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("nyomtatoToolStripMenuItem.Image")));
            this.nyomtatoToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.nyomtatoToolStripMenuItem.Name = "nyomtatoToolStripMenuItem";
            this.nyomtatoToolStripMenuItem.Size = new System.Drawing.Size(211, 26);
            this.nyomtatoToolStripMenuItem.Text = "Nyomtató beállítás";
            this.nyomtatoToolStripMenuItem.Click += new System.EventHandler(this.nyomtatoToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(208, 6);
            // 
            // kilépésToolStripMenuItem
            // 
            this.kilépésToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("kilépésToolStripMenuItem.Image")));
            this.kilépésToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.kilépésToolStripMenuItem.Name = "kilépésToolStripMenuItem";
            this.kilépésToolStripMenuItem.Size = new System.Drawing.Size(211, 26);
            this.kilépésToolStripMenuItem.Text = "Kilépés";
            this.kilépésToolStripMenuItem.Click += new System.EventHandler(this.kilépésToolStripMenuItem_Click);
            // 
            // extrákToolStripMenuItem
            // 
            this.extrákToolStripMenuItem.Name = "extrákToolStripMenuItem";
            this.extrákToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.extrákToolStripMenuItem.Text = "Extrák";
            // 
            // egyebToolStripMenuItem
            // 
            this.egyebToolStripMenuItem.Name = "egyebToolStripMenuItem";
            this.egyebToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.egyebToolStripMenuItem.Text = "Egyéb";
            // 
            // infoToolStripMenuItem
            // 
            this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            this.infoToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.infoToolStripMenuItem.Text = "Infó";
            this.infoToolStripMenuItem.Click += new System.EventHandler(this.infoToolStripMenuItem_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 707);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1016, 27);
            this.panel2.TabIndex = 5;
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel5.Controls.Add(this.label3);
            this.panel5.Controls.Add(this.adatEv);
            this.panel5.Controls.Add(this.label2);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(513, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(323, 27);
            this.panel5.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.Control;
            this.label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(4, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 15);
            this.label3.TabIndex = 13;
            this.label3.Text = "Adatbázis:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // adatEv
            // 
            this.adatEv.AutoSize = true;
            this.adatEv.BackColor = System.Drawing.SystemColors.Control;
            this.adatEv.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.adatEv.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.adatEv.Location = new System.Drawing.Point(74, 5);
            this.adatEv.Name = "adatEv";
            this.adatEv.Size = new System.Drawing.Size(0, 19);
            this.adatEv.TabIndex = 11;
            this.adatEv.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(159, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 18);
            this.label2.TabIndex = 12;
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.labelUzenetek);
            this.panel4.Controls.Add(this.progressBar);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(513, 27);
            this.panel4.TabIndex = 1;
            // 
            // labelUzenetek
            // 
            this.labelUzenetek.Font = new System.Drawing.Font("Arial", 9F);
            this.labelUzenetek.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelUzenetek.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelUzenetek.Location = new System.Drawing.Point(153, 5);
            this.labelUzenetek.Name = "labelUzenetek";
            this.labelUzenetek.Size = new System.Drawing.Size(353, 14);
            this.labelUzenetek.TabIndex = 6;
            this.labelUzenetek.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.progressBar.Location = new System.Drawing.Point(3, 5);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(144, 15);
            this.progressBar.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.labelUser);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(836, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(180, 27);
            this.panel3.TabIndex = 0;
            //
            // labelPrg
            //
            this.panel3.Controls.Add(this.labelPrg);
            this.labelPrg.Name = "labelPrg";
            // 
            // labelUser
            // 
            this.labelUser.AutoSize = true;
            this.labelUser.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelUser.Location = new System.Drawing.Point(44, 4);
            this.labelUser.Name = "labelUser";
            this.labelUser.Size = new System.Drawing.Size(46, 17);
            this.labelUser.TabIndex = 1;
            this.labelUser.Text = "akárki";
            this.labelUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Kezelö:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(154, 682);
            this.panel1.TabIndex = 6;
            // 
            // imageMenu
            // 
            this.imageMenu.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageMenu.ImageStream")));
            this.imageMenu.TransparentColor = System.Drawing.Color.Transparent;
            this.imageMenu.Images.SetKeyName(0, "BankMozgForm.Image.bmp");
            this.imageMenu.Images.SetKeyName(1, "PenztarMozgForm.Image.bmp");
            this.imageMenu.Images.SetKeyName(2, "KiadasForm.Image.bmp");
            this.imageMenu.Images.SetKeyName(3, "BevetelForm.Image.bmp");
            this.imageMenu.Images.SetKeyName(4, "PartnerForm.Image.bmp");
            this.imageMenu.Images.SetKeyName(5, "GrafikonForm.Image.bmp");
            this.imageMenu.Images.SetKeyName(6, "KontirForm.Image.bmp");
            this.imageMenu.Images.SetKeyName(7, "SzamlakForm.Image.bmp");
            this.imageMenu.Images.SetKeyName(8, "TablazatForm.Image.bmp");
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(154, 25);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(4, 682);
            this.splitter1.TabIndex = 8;
            this.splitter1.TabStop = false;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.SystemColors.Control;
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(158, 25);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(858, 682);
            this.panel6.TabIndex = 10;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 734);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.menuStrip);
            this.Font = new System.Drawing.Font("Arial", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SIXIS-VIR";
            this.Load += new System.EventHandler(this.VIR_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem adatToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem kilépésToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem extrákToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem infoToolStripMenuItem;
        public System.Windows.Forms.ImageList imageMenu;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label labelUser;
        private System.Windows.Forms.ToolStripMenuItem nyomtatoToolStripMenuItem;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ToolStripMenuItem egyebToolStripMenuItem;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.ToolStripMenuItem import;
        private System.Windows.Forms.ToolStripMenuItem export;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem adatokImportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem adatokExportToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem nyomtatóBeállításToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem kilépésToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem infoToolStripMenuItem1;
        private System.Windows.Forms.Label adatEv;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel4;
        public System.Windows.Forms.Label labelUzenetek;
        private System.Windows.Forms.ProgressBar progressBar;
        public System.Windows.Forms.Label labelPrg;

    }
}

