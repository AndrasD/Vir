namespace Lekerdezesek
{
    partial class Bizonylatok
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Bizonylatok));
            this.rBPenztar = new System.Windows.Forms.RadioButton();
            this.rBBank = new System.Windows.Forms.RadioButton();
            this.dateTol = new System.Windows.Forms.DateTimePicker();
            this.dateIg = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGV = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.azonosito = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.partner = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.jel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.netto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.afa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.brutto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.megjegyzes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fsz_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.h_fsz_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.penztarid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.h_penztar_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.buttonNyomtat = new System.Windows.Forms.ToolStripButton();
            this.comboSzallito = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbOsszes = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbPenzm = new System.Windows.Forms.CheckBox();
            this.rbSzallito = new System.Windows.Forms.CheckBox();
            this.rbVevo = new System.Windows.Forms.CheckBox();
            this.nettoForgalom = new FormattedTextBox.FormattedTextBox();
            this.afaForgalom = new FormattedTextBox.FormattedTextBox();
            this.bruttoForgalom = new FormattedTextBox.FormattedTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGV)).BeginInit();
            this.toolStrip.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // rBPenztar
            // 
            this.rBPenztar.AutoSize = true;
            this.rBPenztar.Location = new System.Drawing.Point(18, 34);
            this.rBPenztar.Name = "rBPenztar";
            this.rBPenztar.Size = new System.Drawing.Size(66, 19);
            this.rBPenztar.TabIndex = 24;
            this.rBPenztar.TabStop = true;
            this.rBPenztar.Tag = "KULSO_E";
            this.rBPenztar.Text = "Pénztár";
            this.rBPenztar.UseVisualStyleBackColor = true;
            // 
            // rBBank
            // 
            this.rBBank.AutoSize = true;
            this.rBBank.Checked = true;
            this.rBBank.Location = new System.Drawing.Point(18, 12);
            this.rBBank.Name = "rBBank";
            this.rBBank.Size = new System.Drawing.Size(53, 19);
            this.rBBank.TabIndex = 23;
            this.rBBank.TabStop = true;
            this.rBBank.Tag = "KULSO_E";
            this.rBBank.Text = "Bank";
            this.rBBank.UseVisualStyleBackColor = true;
            this.rBBank.CheckedChanged += new System.EventHandler(this.rBBank_CheckedChanged);
            // 
            // dateTol
            // 
            this.dateTol.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTol.Location = new System.Drawing.Point(155, 31);
            this.dateTol.Name = "dateTol";
            this.dateTol.Size = new System.Drawing.Size(93, 21);
            this.dateTol.TabIndex = 2;
            this.dateTol.Tag = "";
            // 
            // dateIg
            // 
            this.dateIg.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateIg.Location = new System.Drawing.Point(254, 31);
            this.dateIg.Name = "dateIg";
            this.dateIg.Size = new System.Drawing.Size(93, 21);
            this.dateIg.TabIndex = 3;
            this.dateIg.Tag = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 15);
            this.label1.TabIndex = 27;
            this.label1.Text = "Dátum intervallum:";
            // 
            // dataGV
            // 
            this.dataGV.AllowUserToAddRows = false;
            this.dataGV.AllowUserToDeleteRows = false;
            this.dataGV.AllowUserToResizeRows = false;
            this.dataGV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.azonosito,
            this.datum,
            this.partner,
            this.jel,
            this.netto,
            this.afa,
            this.brutto,
            this.megjegyzes,
            this.fsz_id,
            this.h_fsz_id,
            this.penztarid,
            this.h_penztar_id});
            this.dataGV.Location = new System.Drawing.Point(3, 139);
            this.dataGV.MultiSelect = false;
            this.dataGV.Name = "dataGV";
            this.dataGV.ReadOnly = true;
            this.dataGV.RowHeadersVisible = false;
            this.dataGV.RowHeadersWidth = 24;
            this.dataGV.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGV.Size = new System.Drawing.Size(852, 517);
            this.dataGV.TabIndex = 6;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "id";
            this.Column1.HeaderText = "id";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Visible = false;
            // 
            // azonosito
            // 
            this.azonosito.DataPropertyName = "azonosito";
            this.azonosito.HeaderText = "Azonosító";
            this.azonosito.Name = "azonosito";
            this.azonosito.ReadOnly = true;
            this.azonosito.Width = 150;
            // 
            // datum
            // 
            this.datum.DataPropertyName = "datum";
            dataGridViewCellStyle1.Format = "D";
            dataGridViewCellStyle1.NullValue = null;
            this.datum.DefaultCellStyle = dataGridViewCellStyle1;
            this.datum.HeaderText = "Dátum";
            this.datum.Name = "datum";
            this.datum.ReadOnly = true;
            this.datum.Width = 120;
            // 
            // partner
            // 
            this.partner.DataPropertyName = "partner";
            this.partner.HeaderText = "Partner";
            this.partner.Name = "partner";
            this.partner.ReadOnly = true;
            this.partner.Width = 150;
            // 
            // jel
            // 
            this.jel.DataPropertyName = "jel";
            this.jel.HeaderText = "Jel";
            this.jel.Name = "jel";
            this.jel.ReadOnly = true;
            // 
            // netto
            // 
            this.netto.DataPropertyName = "netto";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N0";
            dataGridViewCellStyle2.NullValue = null;
            this.netto.DefaultCellStyle = dataGridViewCellStyle2;
            this.netto.HeaderText = "Netto";
            this.netto.Name = "netto";
            this.netto.ReadOnly = true;
            this.netto.Width = 150;
            // 
            // afa
            // 
            this.afa.DataPropertyName = "afa";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N0";
            dataGridViewCellStyle3.NullValue = null;
            this.afa.DefaultCellStyle = dataGridViewCellStyle3;
            this.afa.HeaderText = "ÁFA";
            this.afa.Name = "afa";
            this.afa.ReadOnly = true;
            this.afa.Width = 150;
            // 
            // brutto
            // 
            this.brutto.DataPropertyName = "brutto";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N0";
            dataGridViewCellStyle4.NullValue = null;
            this.brutto.DefaultCellStyle = dataGridViewCellStyle4;
            this.brutto.HeaderText = "Brutto";
            this.brutto.Name = "brutto";
            this.brutto.ReadOnly = true;
            this.brutto.Width = 150;
            // 
            // megjegyzes
            // 
            this.megjegyzes.DataPropertyName = "megjegyzes";
            this.megjegyzes.HeaderText = "Megjegyzés";
            this.megjegyzes.Name = "megjegyzes";
            this.megjegyzes.ReadOnly = true;
            this.megjegyzes.Width = 200;
            // 
            // fsz_id
            // 
            this.fsz_id.DataPropertyName = "fsz_id";
            this.fsz_id.HeaderText = "fszid";
            this.fsz_id.Name = "fsz_id";
            this.fsz_id.ReadOnly = true;
            this.fsz_id.Visible = false;
            // 
            // h_fsz_id
            // 
            this.h_fsz_id.DataPropertyName = "h_fsz_id";
            this.h_fsz_id.HeaderText = "hfszid";
            this.h_fsz_id.Name = "h_fsz_id";
            this.h_fsz_id.ReadOnly = true;
            this.h_fsz_id.Visible = false;
            // 
            // penztarid
            // 
            this.penztarid.DataPropertyName = "penztar_id";
            this.penztarid.HeaderText = "penztarid";
            this.penztarid.Name = "penztarid";
            this.penztarid.ReadOnly = true;
            this.penztarid.Visible = false;
            // 
            // h_penztar_id
            // 
            this.h_penztar_id.DataPropertyName = "h_penztar_id";
            this.h_penztar_id.HeaderText = "hpenztarid";
            this.h_penztar_id.Name = "h_penztar_id";
            this.h_penztar_id.ReadOnly = true;
            this.h_penztar_id.Visible = false;
            // 
            // toolStrip
            // 
            this.toolStrip.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonNyomtat,
            this.toolStripButton1});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip.Size = new System.Drawing.Size(858, 25);
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
            this.buttonNyomtat.Click += new System.EventHandler(this.button2_Click);
            // 
            // comboSzallito
            // 
            this.comboSzallito.FormattingEnabled = true;
            this.comboSzallito.Location = new System.Drawing.Point(421, 32);
            this.comboSzallito.Name = "comboSzallito";
            this.comboSzallito.Size = new System.Drawing.Size(219, 23);
            this.comboSzallito.Sorted = true;
            this.comboSzallito.TabIndex = 4;
            this.comboSzallito.Tag = "PID";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(365, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 15);
            this.label3.TabIndex = 38;
            this.label3.Text = "Partner:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbOsszes);
            this.groupBox1.Controls.Add(this.rBBank);
            this.groupBox1.Controls.Add(this.rBPenztar);
            this.groupBox1.Location = new System.Drawing.Point(84, 52);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(128, 81);
            this.groupBox1.TabIndex = 39;
            this.groupBox1.TabStop = false;
            // 
            // rbOsszes
            // 
            this.rbOsszes.AutoSize = true;
            this.rbOsszes.Location = new System.Drawing.Point(18, 58);
            this.rbOsszes.Name = "rbOsszes";
            this.rbOsszes.Size = new System.Drawing.Size(67, 19);
            this.rbOsszes.TabIndex = 25;
            this.rbOsszes.TabStop = true;
            this.rbOsszes.Tag = "KULSO_E";
            this.rbOsszes.Text = "Összes";
            this.rbOsszes.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbPenzm);
            this.groupBox2.Controls.Add(this.rbSzallito);
            this.groupBox2.Controls.Add(this.rbVevo);
            this.groupBox2.Location = new System.Drawing.Point(219, 52);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(128, 81);
            this.groupBox2.TabIndex = 40;
            this.groupBox2.TabStop = false;
            // 
            // rbPenzm
            // 
            this.rbPenzm.AutoSize = true;
            this.rbPenzm.Location = new System.Drawing.Point(18, 59);
            this.rbPenzm.Name = "rbPenzm";
            this.rbPenzm.Size = new System.Drawing.Size(67, 19);
            this.rbPenzm.TabIndex = 2;
            this.rbPenzm.Text = "Pénztár";
            this.rbPenzm.UseVisualStyleBackColor = true;
            // 
            // rbSzallito
            // 
            this.rbSzallito.AutoSize = true;
            this.rbSzallito.Location = new System.Drawing.Point(18, 34);
            this.rbSzallito.Name = "rbSzallito";
            this.rbSzallito.Size = new System.Drawing.Size(65, 19);
            this.rbSzallito.TabIndex = 1;
            this.rbSzallito.Text = "Szállító";
            this.rbSzallito.UseVisualStyleBackColor = true;
            // 
            // rbVevo
            // 
            this.rbVevo.AutoSize = true;
            this.rbVevo.Location = new System.Drawing.Point(18, 12);
            this.rbVevo.Name = "rbVevo";
            this.rbVevo.Size = new System.Drawing.Size(52, 19);
            this.rbVevo.TabIndex = 0;
            this.rbVevo.Text = "Vevő";
            this.rbVevo.UseVisualStyleBackColor = true;
            // 
            // nettoForgalom
            // 
            this.nettoForgalom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nettoForgalom.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.nettoForgalom.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nettoForgalom.Form = FormattedTextBox.FormattedTextBox.formnum.N0;
            this.nettoForgalom.Format = "N0";
            this.nettoForgalom.Location = new System.Drawing.Point(661, 60);
            this.nettoForgalom.Name = "nettoForgalom";
            this.nettoForgalom.ReadOnly = true;
            this.nettoForgalom.Size = new System.Drawing.Size(194, 22);
            this.nettoForgalom.TabIndex = 41;
            this.nettoForgalom.Text = "0";
            this.nettoForgalom.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // afaForgalom
            // 
            this.afaForgalom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.afaForgalom.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.afaForgalom.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.afaForgalom.Form = FormattedTextBox.FormattedTextBox.formnum.N0;
            this.afaForgalom.Format = "N0";
            this.afaForgalom.Location = new System.Drawing.Point(661, 88);
            this.afaForgalom.Name = "afaForgalom";
            this.afaForgalom.ReadOnly = true;
            this.afaForgalom.Size = new System.Drawing.Size(194, 22);
            this.afaForgalom.TabIndex = 42;
            this.afaForgalom.Text = "0";
            this.afaForgalom.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // bruttoForgalom
            // 
            this.bruttoForgalom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bruttoForgalom.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.bruttoForgalom.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bruttoForgalom.Form = FormattedTextBox.FormattedTextBox.formnum.N0;
            this.bruttoForgalom.Format = "N0";
            this.bruttoForgalom.Location = new System.Drawing.Point(661, 116);
            this.bruttoForgalom.Name = "bruttoForgalom";
            this.bruttoForgalom.ReadOnly = true;
            this.bruttoForgalom.Size = new System.Drawing.Size(194, 22);
            this.bruttoForgalom.TabIndex = 43;
            this.bruttoForgalom.Text = "0";
            this.bruttoForgalom.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(613, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 15);
            this.label4.TabIndex = 44;
            this.label4.Text = "Netto:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(613, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 15);
            this.label2.TabIndex = 45;
            this.label2.Text = "ÁFA:";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(613, 122);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 15);
            this.label5.TabIndex = 46;
            this.label5.Text = "Brutto:";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(509, 66);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 15);
            this.label6.TabIndex = 47;
            this.label6.Text = "Forgalom";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Black;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(101, 22);
            this.toolStripButton1.Text = "Gyűjtés indít";
            this.toolStripButton1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Bizonylatok
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.bruttoForgalom);
            this.Controls.Add(this.afaForgalom);
            this.Controls.Add(this.nettoForgalom);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboSzallito);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.dataGV);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateIg);
            this.Controls.Add(this.dateTol);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Bizonylatok";
            this.Size = new System.Drawing.Size(858, 659);
            this.Tag = "C,SZAMLA";
            this.Load += new System.EventHandler(this.Penzforgalom_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGV)).EndInit();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rBPenztar;
        private System.Windows.Forms.RadioButton rBBank;
        private System.Windows.Forms.DateTimePicker dateTol;
        private System.Windows.Forms.DateTimePicker dateIg;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGV;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton buttonNyomtat;
        private System.Windows.Forms.ComboBox comboSzallito;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbOsszes;
        private System.Windows.Forms.GroupBox groupBox2;
        private FormattedTextBox.FormattedTextBox nettoForgalom;
        private FormattedTextBox.FormattedTextBox afaForgalom;
        private FormattedTextBox.FormattedTextBox bruttoForgalom;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn azonosito;
        private System.Windows.Forms.DataGridViewTextBoxColumn datum;
        private System.Windows.Forms.DataGridViewTextBoxColumn partner;
        private System.Windows.Forms.DataGridViewTextBoxColumn jel;
        private System.Windows.Forms.DataGridViewTextBoxColumn netto;
        private System.Windows.Forms.DataGridViewTextBoxColumn afa;
        private System.Windows.Forms.DataGridViewTextBoxColumn brutto;
        private System.Windows.Forms.DataGridViewTextBoxColumn megjegyzes;
        private System.Windows.Forms.DataGridViewTextBoxColumn fsz_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn h_fsz_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn penztarid;
        private System.Windows.Forms.DataGridViewTextBoxColumn h_penztar_id;
        private System.Windows.Forms.CheckBox rbSzallito;
        private System.Windows.Forms.CheckBox rbVevo;
        private System.Windows.Forms.CheckBox rbPenzm;
        private System.Windows.Forms.ToolStripButton toolStripButton1;



    }
}