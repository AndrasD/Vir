namespace Lekerdezesek
{
    partial class Szamlak
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BevetKiad));
            this.havi = new System.Windows.Forms.RadioButton();
            this.eves = new System.Windows.Forms.RadioButton();
            this.honap = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGV = new System.Windows.Forms.DataGridView();
            this.ev = new System.Windows.Forms.ComboBox();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.buttonNyomtat = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboPartner = new System.Windows.Forms.ComboBox();
            this.comboSzoveg = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGV)).BeginInit();
            this.toolStrip.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // havi
            // 
            this.havi.AutoSize = true;
            this.havi.Checked = true;
            this.havi.Location = new System.Drawing.Point(66, 22);
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
            this.eves.Location = new System.Drawing.Point(66, 48);
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
            this.honap.Location = new System.Drawing.Point(147, 20);
            this.honap.Name = "honap";
            this.honap.Size = new System.Drawing.Size(144, 21);
            this.honap.TabIndex = 0;
            this.honap.ValueChanged += new System.EventHandler(this.honap_ValueChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(773, 105);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "mehet";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGV
            // 
            this.dataGV.AllowUserToAddRows = false;
            this.dataGV.AllowUserToDeleteRows = false;
            this.dataGV.AllowUserToResizeRows = false;
            this.dataGV.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGV.Location = new System.Drawing.Point(3, 171);
            this.dataGV.MultiSelect = false;
            this.dataGV.Name = "dataGV";
            this.dataGV.ReadOnly = true;
            this.dataGV.RowHeadersWidth = 24;
            this.dataGV.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGV.Size = new System.Drawing.Size(854, 485);
            this.dataGV.TabIndex = 14;
            this.dataGV.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGV_CellPainting);
            this.dataGV.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGV_CellMouseDoubleClick);
            // 
            // ev
            // 
            this.ev.Enabled = false;
            this.ev.FormattingEnabled = true;
            this.ev.Location = new System.Drawing.Point(147, 47);
            this.ev.Name = "ev";
            this.ev.Size = new System.Drawing.Size(77, 23);
            this.ev.TabIndex = 1;
            this.ev.SelectedIndexChanged += new System.EventHandler(this.ev_SelectedIndexChanged);
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
            this.toolStrip.TabIndex = 34;
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.comboPartner);
            this.groupBox1.Controls.Add(this.comboSzoveg);
            this.groupBox1.Controls.Add(this.honap);
            this.groupBox1.Controls.Add(this.ev);
            this.groupBox1.Controls.Add(this.havi);
            this.groupBox1.Controls.Add(this.eves);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Location = new System.Drawing.Point(3, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(854, 137);
            this.groupBox1.TabIndex = 35;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Szűrő feltételek";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(65, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 15);
            this.label2.TabIndex = 21;
            this.label2.Text = "Megnevezés:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(65, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 15);
            this.label1.TabIndex = 20;
            this.label1.Text = "Partner:";
            // 
            // comboPartner
            // 
            this.comboPartner.FormattingEnabled = true;
            this.comboPartner.Location = new System.Drawing.Point(147, 76);
            this.comboPartner.Name = "comboPartner";
            this.comboPartner.Size = new System.Drawing.Size(407, 23);
            this.comboPartner.TabIndex = 2;
            this.comboPartner.SelectedIndexChanged += new System.EventHandler(this.comboPartner_SelectedIndexChanged);
            // 
            // comboSzoveg
            // 
            this.comboSzoveg.FormattingEnabled = true;
            this.comboSzoveg.Location = new System.Drawing.Point(147, 105);
            this.comboSzoveg.Name = "comboSzoveg";
            this.comboSzoveg.Size = new System.Drawing.Size(407, 23);
            this.comboSzoveg.TabIndex = 3;
            // 
            // Szamlak
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGV);
            this.Controls.Add(this.toolStrip);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Szamlak";
            this.Size = new System.Drawing.Size(860, 659);
            this.Load += new System.EventHandler(this.Szamlak_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGV)).EndInit();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton havi;
        private System.Windows.Forms.RadioButton eves;
        private System.Windows.Forms.DateTimePicker honap;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGV;
        private System.Windows.Forms.ComboBox ev;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton buttonNyomtat;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboPartner;
        private System.Windows.Forms.ComboBox comboSzoveg;



    }
}