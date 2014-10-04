namespace Lekerdezesek
{
    partial class AfaLista
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AfaLista));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.buttonNyomtat = new System.Windows.Forms.ToolStripButton();
            this.label1 = new System.Windows.Forms.Label();
            this.dateIg = new System.Windows.Forms.DateTimePicker();
            this.dateTol = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGV = new System.Windows.Forms.DataGridView();
            this.radioVevo = new System.Windows.Forms.RadioButton();
            this.radioSzallito = new System.Windows.Forms.RadioButton();
            this.osszesen = new FormattedTextBox.FormattedTextBox();
            this.azonosito = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datum_telj = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datum_fiz = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.partner = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.jel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.osszeg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.afakulcs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AFA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.megjegyzes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGV)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonNyomtat});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip.Size = new System.Drawing.Size(860, 25);
            this.toolStrip.TabIndex = 35;
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 15);
            this.label1.TabIndex = 38;
            this.label1.Text = "Dátum intervallum:";
            // 
            // dateIg
            // 
            this.dateIg.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateIg.Location = new System.Drawing.Point(281, 40);
            this.dateIg.Name = "dateIg";
            this.dateIg.Size = new System.Drawing.Size(93, 21);
            this.dateIg.TabIndex = 37;
            this.dateIg.Tag = "";
            // 
            // dateTol
            // 
            this.dateTol.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTol.Location = new System.Drawing.Point(167, 40);
            this.dateTol.Name = "dateTol";
            this.dateTol.Size = new System.Drawing.Size(93, 21);
            this.dateTol.TabIndex = 36;
            this.dateTol.Tag = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(580, 39);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 39;
            this.button1.Text = "mehet";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGV
            // 
            this.dataGV.AllowUserToAddRows = false;
            this.dataGV.AllowUserToDeleteRows = false;
            this.dataGV.AllowUserToResizeRows = false;
            this.dataGV.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.azonosito,
            this.datum,
            this.datum_telj,
            this.datum_fiz,
            this.partner,
            this.jel,
            this.osszeg,
            this.afakulcs,
            this.AFA,
            this.megjegyzes});
            this.dataGV.Location = new System.Drawing.Point(3, 127);
            this.dataGV.MultiSelect = false;
            this.dataGV.Name = "dataGV";
            this.dataGV.ReadOnly = true;
            this.dataGV.RowHeadersVisible = false;
            this.dataGV.RowHeadersWidth = 24;
            this.dataGV.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGV.Size = new System.Drawing.Size(854, 529);
            this.dataGV.TabIndex = 40;
            // 
            // radioVevo
            // 
            this.radioVevo.AutoSize = true;
            this.radioVevo.Checked = true;
            this.radioVevo.Location = new System.Drawing.Point(165, 67);
            this.radioVevo.Name = "radioVevo";
            this.radioVevo.Size = new System.Drawing.Size(51, 19);
            this.radioVevo.TabIndex = 41;
            this.radioVevo.TabStop = true;
            this.radioVevo.Text = "Vevő";
            this.radioVevo.UseVisualStyleBackColor = true;
            // 
            // radioSzallito
            // 
            this.radioSzallito.AutoSize = true;
            this.radioSzallito.Location = new System.Drawing.Point(165, 87);
            this.radioSzallito.Name = "radioSzallito";
            this.radioSzallito.Size = new System.Drawing.Size(64, 19);
            this.radioSzallito.TabIndex = 42;
            this.radioSzallito.TabStop = true;
            this.radioSzallito.Text = "Szállító";
            this.radioSzallito.UseVisualStyleBackColor = true;
            // 
            // osszesen
            // 
            this.osszesen.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.osszesen.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.osszesen.Form = FormattedTextBox.FormattedTextBox.formnum.N0;
            this.osszesen.Format = "N0";
            this.osszesen.Location = new System.Drawing.Point(633, 83);
            this.osszesen.Name = "osszesen";
            this.osszesen.ReadOnly = true;
            this.osszesen.Size = new System.Drawing.Size(209, 22);
            this.osszesen.TabIndex = 43;
            this.osszesen.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // azonosito
            // 
            this.azonosito.DataPropertyName = "azonosito";
            this.azonosito.HeaderText = "Azonosító";
            this.azonosito.Name = "azonosito";
            this.azonosito.ReadOnly = true;
            this.azonosito.Width = 75;
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
            // 
            // datum_telj
            // 
            this.datum_telj.DataPropertyName = "datum_telj";
            this.datum_telj.HeaderText = "Teljesítés";
            this.datum_telj.Name = "datum_telj";
            this.datum_telj.ReadOnly = true;
            // 
            // datum_fiz
            // 
            this.datum_fiz.DataPropertyName = "datum_fiz";
            this.datum_fiz.HeaderText = "Fizetés";
            this.datum_fiz.Name = "datum_fiz";
            this.datum_fiz.ReadOnly = true;
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
            this.jel.Width = 75;
            // 
            // osszeg
            // 
            this.osszeg.DataPropertyName = "osszeg";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N2";
            dataGridViewCellStyle2.NullValue = null;
            this.osszeg.DefaultCellStyle = dataGridViewCellStyle2;
            this.osszeg.HeaderText = "Összeg";
            this.osszeg.Name = "osszeg";
            this.osszeg.ReadOnly = true;
            // 
            // afakulcs
            // 
            this.afakulcs.DataPropertyName = "afakulcs";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.afakulcs.DefaultCellStyle = dataGridViewCellStyle3;
            this.afakulcs.HeaderText = "%";
            this.afakulcs.Name = "afakulcs";
            this.afakulcs.ReadOnly = true;
            this.afakulcs.Width = 40;
            // 
            // AFA
            // 
            this.AFA.DataPropertyName = "afa";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N0";
            this.AFA.DefaultCellStyle = dataGridViewCellStyle4;
            this.AFA.HeaderText = "Áfa";
            this.AFA.Name = "AFA";
            this.AFA.ReadOnly = true;
            // 
            // megjegyzes
            // 
            this.megjegyzes.DataPropertyName = "megjegyzes";
            this.megjegyzes.HeaderText = "megjegyzes";
            this.megjegyzes.Name = "megjegyzes";
            this.megjegyzes.ReadOnly = true;
            this.megjegyzes.Visible = false;
            // 
            // AfaLista
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.osszesen);
            this.Controls.Add(this.radioSzallito);
            this.Controls.Add(this.radioVevo);
            this.Controls.Add(this.dataGV);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateIg);
            this.Controls.Add(this.dateTol);
            this.Controls.Add(this.toolStrip);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "AfaLista";
            this.Size = new System.Drawing.Size(860, 659);
            this.Tag = "C,SZAMLA";
            this.Load += new System.EventHandler(this.AfaLista_Load);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton buttonNyomtat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateIg;
        private System.Windows.Forms.DateTimePicker dateTol;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGV;
        private System.Windows.Forms.RadioButton radioVevo;
        private System.Windows.Forms.RadioButton radioSzallito;
        private FormattedTextBox.FormattedTextBox osszesen;
        private System.Windows.Forms.DataGridViewTextBoxColumn azonosito;
        private System.Windows.Forms.DataGridViewTextBoxColumn datum;
        private System.Windows.Forms.DataGridViewTextBoxColumn datum_telj;
        private System.Windows.Forms.DataGridViewTextBoxColumn datum_fiz;
        private System.Windows.Forms.DataGridViewTextBoxColumn partner;
        private System.Windows.Forms.DataGridViewTextBoxColumn jel;
        private System.Windows.Forms.DataGridViewTextBoxColumn osszeg;
        private System.Windows.Forms.DataGridViewTextBoxColumn afakulcs;
        private System.Windows.Forms.DataGridViewTextBoxColumn AFA;
        private System.Windows.Forms.DataGridViewTextBoxColumn megjegyzes;
    }
}
