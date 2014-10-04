namespace Törzsadatok
{
    partial class Ktgfelosztas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ktgfelosztas));
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.buttonMentes = new System.Windows.Forms.ToolStripButton();
            this.buttonVissza = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.treeView = new System.Windows.Forms.TreeView();
            this.cbSema = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.felosztva = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGVFelo = new System.Windows.Forms.DataGridView();
            this.button_be = new System.Windows.Forms.Button();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.button_ki = new System.Windows.Forms.Button();
            this.toolStrip.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGVFelo)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonMentes,
            this.buttonVissza});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip.Size = new System.Drawing.Size(860, 25);
            this.toolStrip.TabIndex = 4;
            this.toolStrip.Text = "toolStrip1";
            // 
            // buttonMentes
            // 
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.treeView);
            this.groupBox1.Location = new System.Drawing.Point(591, 43);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(253, 613);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Költség struktura";
            // 
            // treeView
            // 
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.HideSelection = false;
            this.treeView.Location = new System.Drawing.Point(3, 17);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(247, 593);
            this.treeView.TabIndex = 0;
            this.treeView.TabStop = false;
            this.treeView.DoubleClick += new System.EventHandler(this.button_be_Click);
            this.treeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseClick);
            // 
            // cbSema
            // 
            this.cbSema.FormattingEnabled = true;
            this.cbSema.Location = new System.Drawing.Point(6, 43);
            this.cbSema.Name = "cbSema";
            this.cbSema.Size = new System.Drawing.Size(131, 23);
            this.cbSema.TabIndex = 11;
            this.cbSema.Tag = "";
            this.cbSema.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 15);
            this.label1.TabIndex = 12;
            this.label1.Text = "Felosztási séma:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.felosztva);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.dataGVFelo);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cbSema);
            this.groupBox2.Location = new System.Drawing.Point(3, 43);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(537, 613);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Tag = "C,KTGFELOSZTAS";
            this.groupBox2.Text = "Költségfelosztás";
            // 
            // felosztva
            // 
            this.felosztva.AutoSize = true;
            this.felosztva.Location = new System.Drawing.Point(73, 86);
            this.felosztva.Name = "felosztva";
            this.felosztva.Size = new System.Drawing.Size(14, 15);
            this.felosztva.TabIndex = 16;
            this.felosztva.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 15);
            this.label3.TabIndex = 15;
            this.label3.Text = "Felosztva:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(140, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 15);
            this.label2.TabIndex = 14;
            this.label2.Text = "Felosztás";
            // 
            // dataGVFelo
            // 
            this.dataGVFelo.AllowUserToAddRows = false;
            this.dataGVFelo.AllowUserToDeleteRows = false;
            this.dataGVFelo.AllowUserToResizeRows = false;
            this.dataGVFelo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGVFelo.Location = new System.Drawing.Point(143, 43);
            this.dataGVFelo.MultiSelect = false;
            this.dataGVFelo.Name = "dataGVFelo";
            this.dataGVFelo.RowHeadersVisible = false;
            this.dataGVFelo.RowHeadersWidth = 24;
            this.dataGVFelo.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGVFelo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGVFelo.Size = new System.Drawing.Size(388, 564);
            this.dataGVFelo.TabIndex = 13;
            this.dataGVFelo.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGVFelo_CellValidated);
            this.dataGVFelo.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGVFelo_CellValidating);
            this.dataGVFelo.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGVFelo_RowsAdded);
            this.dataGVFelo.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGVFelo_CellClick);
            this.dataGVFelo.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dataGVFelo_RowsRemoved);
            // 
            // button_be
            // 
            this.button_be.ImageKey = "elözö.bmp";
            this.button_be.ImageList = this.imageList;
            this.button_be.Location = new System.Drawing.Point(546, 196);
            this.button_be.Name = "button_be";
            this.button_be.Size = new System.Drawing.Size(39, 36);
            this.button_be.TabIndex = 14;
            this.button_be.UseVisualStyleBackColor = true;
            this.button_be.Click += new System.EventHandler(this.button_be_Click);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Black;
            this.imageList.Images.SetKeyName(0, "következö.bmp");
            this.imageList.Images.SetKeyName(1, "elözö.bmp");
            // 
            // button_ki
            // 
            this.button_ki.ImageKey = "következö.bmp";
            this.button_ki.ImageList = this.imageList;
            this.button_ki.Location = new System.Drawing.Point(546, 248);
            this.button_ki.Name = "button_ki";
            this.button_ki.Size = new System.Drawing.Size(39, 36);
            this.button_ki.TabIndex = 15;
            this.button_ki.UseVisualStyleBackColor = true;
            this.button_ki.Click += new System.EventHandler(this.button_ki_Click);
            // 
            // Ktgfelosztas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.button_ki);
            this.Controls.Add(this.button_be);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Ktgfelosztas";
            this.Size = new System.Drawing.Size(860, 659);
            this.Load += new System.EventHandler(this.KtgFelosztas_Load);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGVFelo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton buttonMentes;
        private System.Windows.Forms.ToolStripButton buttonVissza;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.ComboBox cbSema;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGVFelo;
        private System.Windows.Forms.Button button_be;
        private System.Windows.Forms.Button button_ki;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.Label felosztva;
        private System.Windows.Forms.Label label3;

    }
}