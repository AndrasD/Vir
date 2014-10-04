namespace Lekerdezesek
{
    partial class BevetKiad
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
            this.dataGV = new System.Windows.Forms.DataGridView();
            this.ev = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBevetelkod = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textKoltsegkod = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboPartner = new System.Windows.Forms.ComboBox();
            this.comboSzoveg = new System.Windows.Forms.ComboBox();
            this.dataGV2 = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.osszeg1 = new FormattedTextBox.FormattedTextBox();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.buttonNyomtat = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonMentes = new System.Windows.Forms.ToolStripButton();
            this.buttonBetolt = new System.Windows.Forms.ToolStripButton();
            this.osszeg2 = new FormattedTextBox.FormattedTextBox();
            this.egyenleg = new FormattedTextBox.FormattedTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGV)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGV2)).BeginInit();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // havi
            // 
            this.havi.AutoSize = true;
            this.havi.Checked = true;
            this.havi.Location = new System.Drawing.Point(6, 19);
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
            this.eves.Location = new System.Drawing.Point(6, 46);
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
            this.honap.Location = new System.Drawing.Point(61, 18);
            this.honap.Name = "honap";
            this.honap.Size = new System.Drawing.Size(144, 21);
            this.honap.TabIndex = 0;
            this.honap.ValueChanged += new System.EventHandler(this.honap_ValueChanged);
            // 
            // dataGV
            // 
            this.dataGV.AllowUserToAddRows = false;
            this.dataGV.AllowUserToDeleteRows = false;
            this.dataGV.AllowUserToResizeRows = false;
            this.dataGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGV.Location = new System.Drawing.Point(3, 188);
            this.dataGV.MultiSelect = false;
            this.dataGV.Name = "dataGV";
            this.dataGV.ReadOnly = true;
            this.dataGV.RowHeadersWidth = 24;
            this.dataGV.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGV.Size = new System.Drawing.Size(418, 396);
            this.dataGV.TabIndex = 14;
            this.dataGV.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGV_CellPainting);
            this.dataGV.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGV_CellMouseDoubleClick);
            // 
            // ev
            // 
            this.ev.Enabled = false;
            this.ev.FormattingEnabled = true;
            this.ev.Location = new System.Drawing.Point(61, 45);
            this.ev.Name = "ev";
            this.ev.Size = new System.Drawing.Size(77, 23);
            this.ev.TabIndex = 1;
            this.ev.SelectedIndexChanged += new System.EventHandler(this.ev_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.textBevetelkod);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.textKoltsegkod);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.comboPartner);
            this.groupBox1.Controls.Add(this.comboSzoveg);
            this.groupBox1.Controls.Add(this.honap);
            this.groupBox1.Controls.Add(this.ev);
            this.groupBox1.Controls.Add(this.havi);
            this.groupBox1.Controls.Add(this.eves);
            this.groupBox1.Location = new System.Drawing.Point(6, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(846, 136);
            this.groupBox1.TabIndex = 35;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Szűrő feltételek";
            // 
            // textBevetelkod
            // 
            this.textBevetelkod.Location = new System.Drawing.Point(82, 75);
            this.textBevetelkod.Multiline = true;
            this.textBevetelkod.Name = "textBevetelkod";
            this.textBevetelkod.Size = new System.Drawing.Size(337, 52);
            this.textBevetelkod.TabIndex = 26;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 77);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 15);
            this.label8.TabIndex = 25;
            this.label8.Text = "Bevételkód:";
            // 
            // textKoltsegkod
            // 
            this.textKoltsegkod.Location = new System.Drawing.Point(513, 74);
            this.textKoltsegkod.Multiline = true;
            this.textKoltsegkod.Name = "textKoltsegkod";
            this.textKoltsegkod.Size = new System.Drawing.Size(321, 52);
            this.textKoltsegkod.TabIndex = 24;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(435, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 15);
            this.label3.TabIndex = 23;
            this.label3.Text = "Költségkód:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(435, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 15);
            this.label2.TabIndex = 21;
            this.label2.Text = "Megnevezés:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(435, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 15);
            this.label1.TabIndex = 20;
            this.label1.Text = "Partner:";
            // 
            // comboPartner
            // 
            this.comboPartner.FormattingEnabled = true;
            this.comboPartner.Location = new System.Drawing.Point(513, 13);
            this.comboPartner.Name = "comboPartner";
            this.comboPartner.Size = new System.Drawing.Size(321, 23);
            this.comboPartner.TabIndex = 2;
            this.comboPartner.SelectedIndexChanged += new System.EventHandler(this.comboPartner_SelectedIndexChanged);
            // 
            // comboSzoveg
            // 
            this.comboSzoveg.FormattingEnabled = true;
            this.comboSzoveg.Location = new System.Drawing.Point(513, 42);
            this.comboSzoveg.Name = "comboSzoveg";
            this.comboSzoveg.Size = new System.Drawing.Size(321, 23);
            this.comboSzoveg.TabIndex = 3;
            // 
            // dataGV2
            // 
            this.dataGV2.AllowUserToAddRows = false;
            this.dataGV2.AllowUserToDeleteRows = false;
            this.dataGV2.AllowUserToResizeRows = false;
            this.dataGV2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGV2.Location = new System.Drawing.Point(431, 188);
            this.dataGV2.MultiSelect = false;
            this.dataGV2.Name = "dataGV2";
            this.dataGV2.ReadOnly = true;
            this.dataGV2.RowHeadersWidth = 24;
            this.dataGV2.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGV2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGV2.Size = new System.Drawing.Size(423, 396);
            this.dataGV2.TabIndex = 36;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 170);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 15);
            this.label4.TabIndex = 37;
            this.label4.Text = "Bevétel (Vevő)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(428, 170);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 15);
            this.label5.TabIndex = 38;
            this.label5.Text = "Kiadás (Szállító)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 599);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 15);
            this.label6.TabIndex = 39;
            this.label6.Text = "Összesen:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(438, 599);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 15);
            this.label7.TabIndex = 40;
            this.label7.Text = "Összesen:";
            // 
            // osszeg1
            // 
            this.osszeg1.Enabled = false;
            this.osszeg1.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.osszeg1.Form = FormattedTextBox.FormattedTextBox.formnum.N2;
            this.osszeg1.Format = "N2";
            this.osszeg1.Location = new System.Drawing.Point(255, 590);
            this.osszeg1.Name = "osszeg1";
            this.osszeg1.ReadOnly = true;
            this.osszeg1.Size = new System.Drawing.Size(166, 29);
            this.osszeg1.TabIndex = 41;
            this.osszeg1.TabStop = false;
            this.osszeg1.Tag = "";
            this.osszeg1.Text = "0";
            this.osszeg1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // toolStrip
            // 
            this.toolStrip.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonNyomtat,
            this.toolStripButton1,
            this.toolStripSeparator1,
            this.buttonMentes,
            this.buttonBetolt});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip.Size = new System.Drawing.Size(855, 25);
            this.toolStrip.TabIndex = 43;
            this.toolStrip.Text = "toolStrip1";
            // 
            // buttonNyomtat
            // 
            this.buttonNyomtat.Image = ((System.Drawing.Image)(resources.GetObject("buttonNyomtat.Image")));
            this.buttonNyomtat.ImageTransparentColor = System.Drawing.Color.Black;
            this.buttonNyomtat.Name = "buttonNyomtat";
            this.buttonNyomtat.Size = new System.Drawing.Size(77, 22);
            this.buttonNyomtat.Text = "Nyomtat";
            this.buttonNyomtat.Click += new System.EventHandler(this.buttonNyomtat_Click_1);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Black;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(104, 22);
            this.toolStripButton1.Text = "Keresés indít";
            this.toolStripButton1.Click += new System.EventHandler(this.button1_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // buttonMentes
            // 
            this.buttonMentes.Image = ((System.Drawing.Image)(resources.GetObject("buttonMentes.Image")));
            this.buttonMentes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonMentes.Name = "buttonMentes";
            this.buttonMentes.Size = new System.Drawing.Size(71, 22);
            this.buttonMentes.Text = "Mentés";
            // 
            // buttonBetolt
            // 
            this.buttonBetolt.Image = ((System.Drawing.Image)(resources.GetObject("buttonBetolt.Image")));
            this.buttonBetolt.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonBetolt.Name = "buttonBetolt";
            this.buttonBetolt.Size = new System.Drawing.Size(59, 22);
            this.buttonBetolt.Text = "Open";
            // 
            // osszeg2
            // 
            this.osszeg2.Enabled = false;
            this.osszeg2.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.osszeg2.Form = FormattedTextBox.FormattedTextBox.formnum.N2;
            this.osszeg2.Format = "N2";
            this.osszeg2.Location = new System.Drawing.Point(688, 590);
            this.osszeg2.Name = "osszeg2";
            this.osszeg2.ReadOnly = true;
            this.osszeg2.Size = new System.Drawing.Size(166, 29);
            this.osszeg2.TabIndex = 42;
            this.osszeg2.TabStop = false;
            this.osszeg2.Tag = "";
            this.osszeg2.Text = "0";
            this.osszeg2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // egyenleg
            // 
            this.egyenleg.Enabled = false;
            this.egyenleg.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.egyenleg.Form = FormattedTextBox.FormattedTextBox.formnum.N2;
            this.egyenleg.Format = "N2";
            this.egyenleg.Location = new System.Drawing.Point(688, 625);
            this.egyenleg.Name = "egyenleg";
            this.egyenleg.ReadOnly = true;
            this.egyenleg.Size = new System.Drawing.Size(166, 29);
            this.egyenleg.TabIndex = 44;
            this.egyenleg.TabStop = false;
            this.egyenleg.Tag = "";
            this.egyenleg.Text = "0";
            this.egyenleg.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // BevetKiad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.egyenleg);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.osszeg2);
            this.Controls.Add(this.osszeg1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dataGV2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGV);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "BevetKiad";
            this.Size = new System.Drawing.Size(855, 654);
            this.Load += new System.EventHandler(this.Szamlak_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGV)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGV2)).EndInit();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton havi;
        private System.Windows.Forms.RadioButton eves;
        private System.Windows.Forms.DateTimePicker honap;
        private System.Windows.Forms.DataGridView dataGV;
        private System.Windows.Forms.ComboBox ev;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboPartner;
        private System.Windows.Forms.ComboBox comboSzoveg;
        private System.Windows.Forms.DataGridView dataGV2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textKoltsegkod;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private FormattedTextBox.FormattedTextBox osszeg1;
        private System.Windows.Forms.TextBox textBevetelkod;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton buttonNyomtat;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton buttonMentes;
        private System.Windows.Forms.ToolStripButton buttonBetolt;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private FormattedTextBox.FormattedTextBox osszeg2;
        private FormattedTextBox.FormattedTextBox egyenleg;



    }
}