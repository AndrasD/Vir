﻿namespace Törzsadatok
{
    partial class Kodtablak
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Kodtablak));
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.buttonUj = new System.Windows.Forms.ToolStripButton();
            this.buttonTöröl = new System.Windows.Forms.ToolStripButton();
            this.buttonMentes = new System.Windows.Forms.ToolStripButton();
            this.buttonVissza = new System.Windows.Forms.ToolStripButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.textAzonosito = new System.Windows.Forms.TextBox();
            this.textMegnevezes = new System.Windows.Forms.TextBox();
            this.bankid = new System.Windows.Forms.Label();
            this.dataView1 = new System.Data.DataView();
            this.dataView2 = new System.Data.DataView();
            this.toolStrip.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataView2)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(445, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Azonosító:";
            // 
            // toolStrip
            // 
            this.toolStrip.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonUj,
            this.buttonTöröl,
            this.buttonMentes,
            this.buttonVissza});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip.Size = new System.Drawing.Size(856, 25);
            this.toolStrip.TabIndex = 4;
            this.toolStrip.Text = "toolStrip1";
            // 
            // buttonUj
            // 
            this.buttonUj.Image = ((System.Drawing.Image)(resources.GetObject("buttonUj.Image")));
            this.buttonUj.ImageTransparentColor = System.Drawing.Color.Black;
            this.buttonUj.Name = "buttonUj";
            this.buttonUj.Size = new System.Drawing.Size(79, 22);
            this.buttonUj.Text = "Új felvitel";
            this.buttonUj.Click += new System.EventHandler(this.buttonUj_Click);
            // 
            // buttonTöröl
            // 
            this.buttonTöröl.Image = ((System.Drawing.Image)(resources.GetObject("buttonTöröl.Image")));
            this.buttonTöröl.ImageTransparentColor = System.Drawing.Color.Black;
            this.buttonTöröl.Name = "buttonTöröl";
            this.buttonTöröl.Size = new System.Drawing.Size(56, 22);
            this.buttonTöröl.Text = "Töröl";
            this.buttonTöröl.Click += new System.EventHandler(this.buttonTorol_Click);
            // 
            // buttonMentes
            // 
            this.buttonMentes.Enabled = false;
            this.buttonMentes.Image = ((System.Drawing.Image)(resources.GetObject("buttonMentes.Image")));
            this.buttonMentes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonMentes.Name = "buttonMentes";
            this.buttonMentes.Size = new System.Drawing.Size(71, 22);
            this.buttonMentes.Text = "Mentés";
            this.buttonMentes.Click += new System.EventHandler(this.buttonMentes_Click);
            // 
            // buttonVissza
            // 
            this.buttonVissza.Image = ((System.Drawing.Image)(resources.GetObject("buttonVissza.Image")));
            this.buttonVissza.ImageTransparentColor = System.Drawing.Color.Black;
            this.buttonVissza.Name = "buttonVissza";
            this.buttonVissza.Size = new System.Drawing.Size(68, 22);
            this.buttonVissza.Text = "Vissza";
            this.buttonVissza.Click += new System.EventHandler(this.buttonVissza_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Location = new System.Drawing.Point(12, 45);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(365, 499);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Pénzintézetek";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(7, 23);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 24;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(350, 470);
            this.dataGridView1.TabIndex = 3;
            this.dataGridView1.TabStop = false;
            this.dataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(445, 109);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "Megnevezés:";
            // 
            // textAzonosito
            // 
            this.textAzonosito.Location = new System.Drawing.Point(529, 70);
            this.textAzonosito.MaxLength = 45;
            this.textAzonosito.Name = "textAzonosito";
            this.textAzonosito.ReadOnly = true;
            this.textAzonosito.Size = new System.Drawing.Size(314, 21);
            this.textAzonosito.TabIndex = 7;
            this.textAzonosito.Tag = "KOD";
            this.textAzonosito.Validated += new System.EventHandler(this.Control_Validated);
            // 
            // textMegnevezes
            // 
            this.textMegnevezes.Location = new System.Drawing.Point(529, 106);
            this.textMegnevezes.MaxLength = 45;
            this.textMegnevezes.Name = "textMegnevezes";
            this.textMegnevezes.Size = new System.Drawing.Size(314, 21);
            this.textMegnevezes.TabIndex = 8;
            this.textMegnevezes.Tag = "SZOVEG";
            this.textMegnevezes.Validated += new System.EventHandler(this.Control_Validated);
            // 
            // bankid
            // 
            this.bankid.AutoSize = true;
            this.bankid.Location = new System.Drawing.Point(445, 45);
            this.bankid.Name = "bankid";
            this.bankid.Size = new System.Drawing.Size(17, 15);
            this.bankid.TabIndex = 9;
            this.bankid.Tag = "id";
            this.bankid.Text = "id";
            this.bankid.Visible = false;
            // 
            // Kodtablak
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.bankid);
            this.Controls.Add(this.textMegnevezes);
            this.Controls.Add(this.textAzonosito);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Kodtablak";
            this.Size = new System.Drawing.Size(856, 563);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton buttonUj;
        private System.Windows.Forms.ToolStripButton buttonTöröl;
        private System.Windows.Forms.ToolStripButton buttonMentes;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textAzonosito;
        private System.Windows.Forms.TextBox textMegnevezes;
        private System.Windows.Forms.Label bankid;
        private System.Data.DataView dataView1;
        private System.Windows.Forms.ToolStripButton buttonVissza;
        private System.Data.DataView dataView2;

    }
}