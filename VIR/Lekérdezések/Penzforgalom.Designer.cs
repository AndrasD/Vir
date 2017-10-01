namespace Lekerdezesek
{
    partial class Penzforgalom
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Penzforgalom));
            this.rBPenztar = new System.Windows.Forms.RadioButton();
            this.rBBank = new System.Windows.Forms.RadioButton();
            this.cbBank = new System.Windows.Forms.ComboBox();
            this.cbPenztar = new System.Windows.Forms.ComboBox();
            this.dateTol = new System.Windows.Forms.DateTimePicker();
            this.dateIg = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGV = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.azonosito = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.partner = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.jel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.brutton = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bruttoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.megjegyzes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.buttonNyomtat = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.comboSzallito = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.osszesen = new FormattedTextBox.FormattedTextBox();
            this.idoszaki = new FormattedTextBox.FormattedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGV)).BeginInit();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // rBPenztar
            // 
            this.rBPenztar.AutoSize = true;
            this.rBPenztar.Location = new System.Drawing.Point(42, 72);
            this.rBPenztar.Name = "rBPenztar";
            this.rBPenztar.Size = new System.Drawing.Size(80, 21);
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
            this.rBBank.Location = new System.Drawing.Point(42, 43);
            this.rBBank.Name = "rBBank";
            this.rBBank.Size = new System.Drawing.Size(62, 21);
            this.rBBank.TabIndex = 23;
            this.rBBank.TabStop = true;
            this.rBBank.Tag = "KULSO_E";
            this.rBBank.Text = "Bank";
            this.rBBank.UseVisualStyleBackColor = true;
            this.rBBank.CheckedChanged += new System.EventHandler(this.rBBank_CheckedChanged);
            // 
            // cbBank
            // 
            this.cbBank.FormattingEnabled = true;
            this.cbBank.Location = new System.Drawing.Point(114, 42);
            this.cbBank.Name = "cbBank";
            this.cbBank.Size = new System.Drawing.Size(270, 25);
            this.cbBank.TabIndex = 0;
            this.cbBank.Tag = "H_FSZ_ID";
            // 
            // cbPenztar
            // 
            this.cbPenztar.Enabled = false;
            this.cbPenztar.FormattingEnabled = true;
            this.cbPenztar.Location = new System.Drawing.Point(114, 71);
            this.cbPenztar.Name = "cbPenztar";
            this.cbPenztar.Size = new System.Drawing.Size(270, 25);
            this.cbPenztar.TabIndex = 1;
            this.cbPenztar.Tag = "H_PENZTAR_ID";
            this.cbPenztar.SelectedIndexChanged += new System.EventHandler(this.cbPenztar_SelectedIndexChanged);
            // 
            // dateTol
            // 
            this.dateTol.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTol.Location = new System.Drawing.Point(177, 100);
            this.dateTol.Name = "dateTol";
            this.dateTol.Size = new System.Drawing.Size(93, 25);
            this.dateTol.TabIndex = 2;
            this.dateTol.Tag = "";
            // 
            // dateIg
            // 
            this.dateIg.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateIg.Location = new System.Drawing.Point(291, 100);
            this.dateIg.Name = "dateIg";
            this.dateIg.Size = new System.Drawing.Size(93, 25);
            this.dateIg.TabIndex = 3;
            this.dateIg.Tag = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 17);
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
            this.brutton,
            this.bruttoc,
            this.megjegyzes});
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
            // brutton
            // 
            this.brutton.DataPropertyName = "brutton";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N2";
            dataGridViewCellStyle2.NullValue = null;
            this.brutton.DefaultCellStyle = dataGridViewCellStyle2;
            this.brutton.HeaderText = "Bevétel";
            this.brutton.Name = "brutton";
            this.brutton.ReadOnly = true;
            this.brutton.Width = 150;
            // 
            // bruttoc
            // 
            this.bruttoc.DataPropertyName = "bruttoc";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = null;
            this.bruttoc.DefaultCellStyle = dataGridViewCellStyle3;
            this.bruttoc.HeaderText = "Kiadás";
            this.bruttoc.Name = "bruttoc";
            this.bruttoc.ReadOnly = true;
            this.bruttoc.Width = 150;
            // 
            // megjegyzes
            // 
            this.megjegyzes.DataPropertyName = "megjegyzes";
            this.megjegyzes.HeaderText = "Megjegyzés";
            this.megjegyzes.Name = "megjegyzes";
            this.megjegyzes.ReadOnly = true;
            this.megjegyzes.Width = 200;
            // 
            // toolStrip
            // 
            this.toolStrip.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonNyomtat,
            this.toolStripButton1});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip.Size = new System.Drawing.Size(858, 27);
            this.toolStrip.TabIndex = 34;
            this.toolStrip.Text = "toolStrip1";
            // 
            // buttonNyomtat
            // 
            this.buttonNyomtat.Image = ((System.Drawing.Image)(resources.GetObject("buttonNyomtat.Image")));
            this.buttonNyomtat.ImageTransparentColor = System.Drawing.Color.Black;
            this.buttonNyomtat.Name = "buttonNyomtat";
            this.buttonNyomtat.Size = new System.Drawing.Size(92, 24);
            this.buttonNyomtat.Text = "Nyomtat";
            this.buttonNyomtat.Click += new System.EventHandler(this.button2_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Black;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(129, 24);
            this.toolStripButton1.Text = "Keresés indít";
            this.toolStripButton1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboSzallito
            // 
            this.comboSzallito.FormattingEnabled = true;
            this.comboSzallito.Location = new System.Drawing.Point(472, 42);
            this.comboSzallito.Name = "comboSzallito";
            this.comboSzallito.Size = new System.Drawing.Size(219, 25);
            this.comboSzallito.Sorted = true;
            this.comboSzallito.TabIndex = 4;
            this.comboSzallito.Tag = "PID";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(416, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 17);
            this.label3.TabIndex = 38;
            this.label3.Text = "Partner:";
            // 
            // osszesen
            // 
            this.osszesen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.osszesen.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.osszesen.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.osszesen.Form = FormattedTextBox.FormattedTextBox.formnum.N0;
            this.osszesen.Format = "N0";
            this.osszesen.Location = new System.Drawing.Point(646, 83);
            this.osszesen.Name = "osszesen";
            this.osszesen.ReadOnly = true;
            this.osszesen.Size = new System.Drawing.Size(209, 28);
            this.osszesen.TabIndex = 39;
            this.osszesen.Text = "0";
            this.osszesen.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // idoszaki
            // 
            this.idoszaki.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.idoszaki.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.idoszaki.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.idoszaki.Form = FormattedTextBox.FormattedTextBox.formnum.N0;
            this.idoszaki.Format = "N0";
            this.idoszaki.Location = new System.Drawing.Point(646, 111);
            this.idoszaki.Name = "idoszaki";
            this.idoszaki.ReadOnly = true;
            this.idoszaki.Size = new System.Drawing.Size(209, 28);
            this.idoszaki.TabIndex = 40;
            this.idoszaki.Text = "0";
            this.idoszaki.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(575, 117);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 17);
            this.label2.TabIndex = 41;
            this.label2.Text = "Záró:";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(575, 89);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 17);
            this.label4.TabIndex = 42;
            this.label4.Text = "Nyító:";
            // 
            // Penzforgalom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.idoszaki);
            this.Controls.Add(this.osszesen);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboSzallito);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.dataGV);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateIg);
            this.Controls.Add(this.dateTol);
            this.Controls.Add(this.rBPenztar);
            this.Controls.Add(this.rBBank);
            this.Controls.Add(this.cbBank);
            this.Controls.Add(this.cbPenztar);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Penzforgalom";
            this.Size = new System.Drawing.Size(858, 659);
            this.Tag = "C,SZAMLA";
            this.Load += new System.EventHandler(this.Penzforgalom_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGV)).EndInit();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rBPenztar;
        private System.Windows.Forms.RadioButton rBBank;
        private System.Windows.Forms.ComboBox cbBank;
        private System.Windows.Forms.ComboBox cbPenztar;
        private System.Windows.Forms.DateTimePicker dateTol;
        private System.Windows.Forms.DateTimePicker dateIg;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGV;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton buttonNyomtat;
        private System.Windows.Forms.ComboBox comboSzallito;
        private System.Windows.Forms.Label label3;
        private FormattedTextBox.FormattedTextBox osszesen;
        private FormattedTextBox.FormattedTextBox idoszaki;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn azonosito;
        private System.Windows.Forms.DataGridViewTextBoxColumn datum;
        private System.Windows.Forms.DataGridViewTextBoxColumn partner;
        private System.Windows.Forms.DataGridViewTextBoxColumn jel;
        private System.Windows.Forms.DataGridViewTextBoxColumn brutton;
        private System.Windows.Forms.DataGridViewTextBoxColumn bruttoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn megjegyzes;
        private System.Windows.Forms.ToolStripButton toolStripButton1;



    }
}