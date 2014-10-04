namespace Törzsadatok
{
    partial class Dolgozo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Dolgozo));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.buttonUj = new System.Windows.Forms.ToolStripButton();
            this.buttonTorol = new System.Windows.Forms.ToolStripButton();
            this.buttonMentes = new System.Windows.Forms.ToolStripButton();
            this.dataGV = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.jelszo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.azonosito = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dolgozo_nev = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.beosztas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.szint = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.textMegnevezes = new System.Windows.Forms.TextBox();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.textBeosztas = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textAzonosito = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textJelszo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboSzint = new System.Windows.Forms.ComboBox();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonUj,
            this.buttonTorol,
            this.buttonMentes});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip.Size = new System.Drawing.Size(842, 25);
            this.toolStrip.TabIndex = 1;
            // 
            // buttonUj
            // 
            this.buttonUj.Image = ((System.Drawing.Image)(resources.GetObject("buttonUj.Image")));
            this.buttonUj.ImageTransparentColor = System.Drawing.Color.Black;
            this.buttonUj.Name = "buttonUj";
            this.buttonUj.Size = new System.Drawing.Size(81, 22);
            this.buttonUj.Text = "Új felvítel";
            this.buttonUj.ToolTipText = "Új elem felvétele";
            this.buttonUj.Click += new System.EventHandler(this.buttonUj_Click);
            // 
            // buttonTorol
            // 
            this.buttonTorol.Image = ((System.Drawing.Image)(resources.GetObject("buttonTorol.Image")));
            this.buttonTorol.ImageTransparentColor = System.Drawing.Color.Black;
            this.buttonTorol.Name = "buttonTorol";
            this.buttonTorol.Size = new System.Drawing.Size(58, 22);
            this.buttonTorol.Text = "Töröl";
            this.buttonTorol.Click += new System.EventHandler(this.buttonTorol_Click);
            // 
            // buttonMentes
            // 
            this.buttonMentes.Image = ((System.Drawing.Image)(resources.GetObject("buttonMentes.Image")));
            this.buttonMentes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonMentes.Name = "buttonMentes";
            this.buttonMentes.Size = new System.Drawing.Size(69, 22);
            this.buttonMentes.Text = "Mentés";
            this.buttonMentes.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // dataGV
            // 
            this.dataGV.AllowUserToAddRows = false;
            this.dataGV.AllowUserToDeleteRows = false;
            this.dataGV.AllowUserToResizeColumns = false;
            this.dataGV.AllowUserToResizeRows = false;
            this.dataGV.BackgroundColor = System.Drawing.SystemColors.ControlDark;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGV.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.jelszo,
            this.azonosito,
            this.dolgozo_nev,
            this.beosztas,
            this.szint});
            this.dataGV.Location = new System.Drawing.Point(3, 51);
            this.dataGV.MultiSelect = false;
            this.dataGV.Name = "dataGV";
            this.dataGV.ReadOnly = true;
            this.dataGV.RowHeadersWidth = 24;
            this.dataGV.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGV.Size = new System.Drawing.Size(458, 540);
            this.dataGV.TabIndex = 2;
            this.dataGV.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGV_CellClick);
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "Id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.id.Visible = false;
            // 
            // jelszo
            // 
            this.jelszo.DataPropertyName = "jelszo";
            this.jelszo.HeaderText = "jelszo";
            this.jelszo.Name = "jelszo";
            this.jelszo.ReadOnly = true;
            this.jelszo.Visible = false;
            // 
            // azonosito
            // 
            this.azonosito.DataPropertyName = "azonosito";
            this.azonosito.HeaderText = "Azonosító";
            this.azonosito.Name = "azonosito";
            this.azonosito.ReadOnly = true;
            this.azonosito.Width = 70;
            // 
            // dolgozo_nev
            // 
            this.dolgozo_nev.DataPropertyName = "dolgozo_nev";
            this.dolgozo_nev.HeaderText = "Dolgozo neve";
            this.dolgozo_nev.Name = "dolgozo_nev";
            this.dolgozo_nev.ReadOnly = true;
            this.dolgozo_nev.Width = 150;
            // 
            // beosztas
            // 
            this.beosztas.DataPropertyName = "beosztas";
            this.beosztas.HeaderText = "Beosztása";
            this.beosztas.Name = "beosztas";
            this.beosztas.ReadOnly = true;
            // 
            // szint
            // 
            this.szint.DataPropertyName = "szint";
            this.szint.HeaderText = "Jog.szint";
            this.szint.Name = "szint";
            this.szint.ReadOnly = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(467, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Név:";
            // 
            // textMegnevezes
            // 
            this.textMegnevezes.Location = new System.Drawing.Point(547, 116);
            this.textMegnevezes.MaxLength = 45;
            this.textMegnevezes.Name = "textMegnevezes";
            this.textMegnevezes.Size = new System.Drawing.Size(289, 22);
            this.textMegnevezes.TabIndex = 1;
            this.textMegnevezes.Tag = "dolgozo_nev";
            this.textMegnevezes.Validating += new System.ComponentModel.CancelEventHandler(this.textMegnevezes_Validating);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(467, 161);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 16);
            this.label1.TabIndex = 10;
            this.label1.Text = "Beosztása:";
            // 
            // textBeosztas
            // 
            this.textBeosztas.FormattingEnabled = true;
            this.textBeosztas.Items.AddRange(new object[] {
            "Rögzítő",
            "Adminisztrátor",
            "Igazgató"});
            this.textBeosztas.Location = new System.Drawing.Point(547, 158);
            this.textBeosztas.Name = "textBeosztas";
            this.textBeosztas.Size = new System.Drawing.Size(289, 24);
            this.textBeosztas.TabIndex = 2;
            this.textBeosztas.Tag = "beosztas";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(467, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 16);
            this.label3.TabIndex = 11;
            this.label3.Text = "Azonosító:";
            // 
            // textAzonosito
            // 
            this.textAzonosito.Location = new System.Drawing.Point(547, 79);
            this.textAzonosito.MaxLength = 10;
            this.textAzonosito.Name = "textAzonosito";
            this.textAzonosito.ReadOnly = true;
            this.textAzonosito.Size = new System.Drawing.Size(125, 22);
            this.textAzonosito.TabIndex = 0;
            this.textAzonosito.Tag = "azonosito";
            this.textAzonosito.Validating += new System.ComponentModel.CancelEventHandler(this.textAzonosito_Validating);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(467, 206);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 16);
            this.label4.TabIndex = 12;
            this.label4.Text = "Jelszó:";
            // 
            // textJelszo
            // 
            this.textJelszo.Location = new System.Drawing.Point(547, 203);
            this.textJelszo.MaxLength = 45;
            this.textJelszo.Name = "textJelszo";
            this.textJelszo.Size = new System.Drawing.Size(289, 22);
            this.textJelszo.TabIndex = 3;
            this.textJelszo.Tag = "jelszo";
            this.textJelszo.UseSystemPasswordChar = true;
            this.textJelszo.Validating += new System.ComponentModel.CancelEventHandler(this.textJelszo_Validating);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(467, 245);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 37);
            this.label5.TabIndex = 13;
            this.label5.Text = "Jogosultsági szint:";
            // 
            // comboSzint
            // 
            this.comboSzint.FormattingEnabled = true;
            this.comboSzint.Items.AddRange(new object[] {
            "1",
            "2",
            "3"});
            this.comboSzint.Location = new System.Drawing.Point(547, 252);
            this.comboSzint.Name = "comboSzint";
            this.comboSzint.Size = new System.Drawing.Size(59, 24);
            this.comboSzint.TabIndex = 14;
            this.comboSzint.Tag = "szint";
            this.comboSzint.Leave += new System.EventHandler(this.comboSzint_Leave);
            this.comboSzint.Validating += new System.ComponentModel.CancelEventHandler(this.comboSzint_Validating);
            // 
            // Dolgozo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.comboSzint);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textJelszo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textAzonosito);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBeosztas);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textMegnevezes);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGV);
            this.Controls.Add(this.toolStrip);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Dolgozo";
            this.Size = new System.Drawing.Size(842, 612);
            this.Tag = "Dolgozók karbantartása";
            this.Load += new System.EventHandler(this.Dolgozo_Load);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton buttonUj;
        private System.Windows.Forms.ToolStripButton buttonTorol;
        private System.Windows.Forms.DataGridView dataGV;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textMegnevezes;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox textBeosztas;
        private System.Windows.Forms.ToolStripButton buttonMentes;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textJelszo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textAzonosito;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboSzint;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn jelszo;
        private System.Windows.Forms.DataGridViewTextBoxColumn azonosito;
        private System.Windows.Forms.DataGridViewTextBoxColumn dolgozo_nev;
        private System.Windows.Forms.DataGridViewTextBoxColumn beosztas;
        private System.Windows.Forms.DataGridViewTextBoxColumn szint;
    }
}
