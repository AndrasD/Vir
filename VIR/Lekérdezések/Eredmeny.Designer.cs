namespace Lekerdezesek
{
    partial class Eredmeny
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Eredmeny));
            this.havi = new System.Windows.Forms.RadioButton();
            this.eves = new System.Windows.Forms.RadioButton();
            this.honap = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGV = new System.Windows.Forms.DataGridView();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.buttonNyomtat = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ev = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGVBev = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGVKiad = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.comboCsoport = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGV)).BeginInit();
            this.toolStrip.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGVBev)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGVKiad)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // havi
            // 
            this.havi.AutoSize = true;
            this.havi.Checked = true;
            this.havi.Location = new System.Drawing.Point(99, 3);
            this.havi.Name = "havi";
            this.havi.Size = new System.Drawing.Size(49, 19);
            this.havi.TabIndex = 0;
            this.havi.TabStop = true;
            this.havi.Text = "Havi";
            this.havi.UseVisualStyleBackColor = true;
            this.havi.CheckedChanged += new System.EventHandler(this.havi_CheckedChanged);
            // 
            // eves
            // 
            this.eves.AutoSize = true;
            this.eves.Location = new System.Drawing.Point(299, 3);
            this.eves.Name = "eves";
            this.eves.Size = new System.Drawing.Size(52, 19);
            this.eves.TabIndex = 1;
            this.eves.Text = "Éves";
            this.eves.UseVisualStyleBackColor = true;
            this.eves.CheckedChanged += new System.EventHandler(this.eves_CheckedChanged);
            // 
            // honap
            // 
            this.honap.CustomFormat = "yyyy.MMMM";
            this.honap.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.honap.Location = new System.Drawing.Point(154, 1);
            this.honap.Name = "honap";
            this.honap.Size = new System.Drawing.Size(126, 21);
            this.honap.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(769, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 22);
            this.button1.TabIndex = 3;
            this.button1.Text = "mehet";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGV
            // 
            this.dataGV.AllowUserToAddRows = false;
            this.dataGV.AllowUserToDeleteRows = false;
            this.dataGV.AllowUserToResizeRows = false;
            this.dataGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGV.Location = new System.Drawing.Point(3, 17);
            this.dataGV.MultiSelect = false;
            this.dataGV.Name = "dataGV";
            this.dataGV.ReadOnly = true;
            this.dataGV.RowHeadersWidth = 24;
            this.dataGV.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGV.Size = new System.Drawing.Size(848, 264);
            this.dataGV.TabIndex = 14;
            this.dataGV.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGV_CellPainting);
            this.dataGV.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGV_CellMouseDoubleClick);
            // 
            // toolStrip
            // 
            this.toolStrip.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonNyomtat,
            this.toolStripSeparator1});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip.Size = new System.Drawing.Size(860, 25);
            this.toolStrip.TabIndex = 33;
            this.toolStrip.Text = "toolStrip1";
            // 
            // buttonNyomtat
            // 
            this.buttonNyomtat.Image = ((System.Drawing.Image)(resources.GetObject("buttonNyomtat.Image")));
            this.buttonNyomtat.ImageTransparentColor = System.Drawing.Color.Black;
            this.buttonNyomtat.Name = "buttonNyomtat";
            this.buttonNyomtat.Size = new System.Drawing.Size(77, 22);
            this.buttonNyomtat.Text = "Nyomtat";
            this.buttonNyomtat.Click += new System.EventHandler(this.buttonNyomtat_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // ev
            // 
            this.ev.Enabled = false;
            this.ev.FormattingEnabled = true;
            this.ev.Location = new System.Drawing.Point(357, 1);
            this.ev.Name = "ev";
            this.ev.Size = new System.Drawing.Size(60, 23);
            this.ev.TabIndex = 34;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.dataGVBev);
            this.groupBox1.Location = new System.Drawing.Point(3, 318);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(854, 166);
            this.groupBox1.TabIndex = 35;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Bevételek";
            // 
            // dataGVBev
            // 
            this.dataGVBev.AllowUserToAddRows = false;
            this.dataGVBev.AllowUserToDeleteRows = false;
            this.dataGVBev.AllowUserToResizeRows = false;
            this.dataGVBev.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGVBev.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGVBev.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGVBev.Location = new System.Drawing.Point(3, 17);
            this.dataGVBev.MultiSelect = false;
            this.dataGVBev.Name = "dataGVBev";
            this.dataGVBev.ReadOnly = true;
            this.dataGVBev.RowHeadersWidth = 24;
            this.dataGVBev.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGVBev.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGVBev.Size = new System.Drawing.Size(848, 146);
            this.dataGVBev.TabIndex = 15;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.dataGVKiad);
            this.groupBox2.Location = new System.Drawing.Point(3, 490);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(854, 166);
            this.groupBox2.TabIndex = 36;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Kiadások";
            // 
            // dataGVKiad
            // 
            this.dataGVKiad.AllowUserToAddRows = false;
            this.dataGVKiad.AllowUserToDeleteRows = false;
            this.dataGVKiad.AllowUserToResizeRows = false;
            this.dataGVKiad.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGVKiad.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGVKiad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGVKiad.Location = new System.Drawing.Point(3, 17);
            this.dataGVKiad.MultiSelect = false;
            this.dataGVKiad.Name = "dataGVKiad";
            this.dataGVKiad.ReadOnly = true;
            this.dataGVKiad.RowHeadersWidth = 24;
            this.dataGVKiad.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGVKiad.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGVKiad.Size = new System.Drawing.Size(848, 146);
            this.dataGVKiad.TabIndex = 16;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.dataGV);
            this.groupBox3.Location = new System.Drawing.Point(3, 28);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(854, 284);
            this.groupBox3.TabIndex = 37;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Eredmény";
            // 
            // comboCsoport
            // 
            this.comboCsoport.FormattingEnabled = true;
            this.comboCsoport.Items.AddRange(new object[] {
            "1. Főcsoport",
            "2. Alcsoport",
            "3. Termék"});
            this.comboCsoport.Location = new System.Drawing.Point(470, 1);
            this.comboCsoport.Name = "comboCsoport";
            this.comboCsoport.Size = new System.Drawing.Size(121, 23);
            this.comboCsoport.TabIndex = 38;
            // 
            // Eredmeny
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.comboCsoport);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ev);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.eves);
            this.Controls.Add(this.havi);
            this.Controls.Add(this.honap);
            this.Controls.Add(this.toolStrip);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Eredmeny";
            this.Size = new System.Drawing.Size(860, 659);
            this.Load += new System.EventHandler(this.Eredmeny_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGV)).EndInit();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGVBev)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGVKiad)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton havi;
        private System.Windows.Forms.RadioButton eves;
        private System.Windows.Forms.DateTimePicker honap;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGV;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton buttonNyomtat;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ComboBox ev;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGVBev;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dataGVKiad;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox comboCsoport;



    }
}