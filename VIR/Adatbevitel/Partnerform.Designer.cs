namespace Adatbevitel
{
    partial class Partnerform
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Partnerform));
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.buttonUj = new System.Windows.Forms.ToolStripButton();
            this.buttonTöröl = new System.Windows.Forms.ToolStripButton();
            this.buttonMentes = new System.Windows.Forms.ToolStripButton();
            this.buttonElolrol = new System.Windows.Forms.ToolStripButton();
            this.buttonVissza = new System.Windows.Forms.ToolStripButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox4 = new FormattedTextBox.FormattedTextBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.button3 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.buttonUjtetel = new System.Windows.Forms.ToolStripButton();
            this.buttonTeteltorol = new System.Windows.Forms.ToolStripButton();
            this.dataGVPartntetel = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dataGVPartn = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataViewPartner = new System.Data.DataView();
            this.dataViewTetel = new System.Data.DataView();
            this.toolStrip.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGVPartntetel)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGVPartn)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataViewPartner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataViewTetel)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonUj,
            this.buttonTöröl,
            this.buttonMentes,
            this.buttonElolrol,
            this.buttonVissza});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip.Size = new System.Drawing.Size(848, 25);
            this.toolStrip.TabIndex = 12;
            this.toolStrip.Text = "toolStrip1";
            // 
            // buttonUj
            // 
            this.buttonUj.Image = ((System.Drawing.Image)(resources.GetObject("buttonUj.Image")));
            this.buttonUj.ImageTransparentColor = System.Drawing.Color.Black;
            this.buttonUj.Name = "buttonUj";
            this.buttonUj.Size = new System.Drawing.Size(84, 22);
            this.buttonUj.Text = "Új partner";
            this.buttonUj.Click += new System.EventHandler(this.Uj_Partner);
            // 
            // buttonTöröl
            // 
            this.buttonTöröl.Image = ((System.Drawing.Image)(resources.GetObject("buttonTöröl.Image")));
            this.buttonTöröl.ImageTransparentColor = System.Drawing.Color.Black;
            this.buttonTöröl.Name = "buttonTöröl";
            this.buttonTöröl.Size = new System.Drawing.Size(56, 22);
            this.buttonTöröl.Text = "Töröl";
            this.buttonTöröl.ToolTipText = "Partner torles";
            this.buttonTöröl.Click += new System.EventHandler(this.Partner_torol);
            // 
            // buttonMentes
            // 
            this.buttonMentes.Image = ((System.Drawing.Image)(resources.GetObject("buttonMentes.Image")));
            this.buttonMentes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonMentes.Name = "buttonMentes";
            this.buttonMentes.Size = new System.Drawing.Size(71, 22);
            this.buttonMentes.Text = "Mentés";
            this.buttonMentes.Click += new System.EventHandler(this.Mentes);
            // 
            // buttonElolrol
            // 
            this.buttonElolrol.Image = ((System.Drawing.Image)(resources.GetObject("buttonElolrol.Image")));
            this.buttonElolrol.ImageTransparentColor = System.Drawing.Color.Black;
            this.buttonElolrol.Name = "buttonElolrol";
            this.buttonElolrol.Size = new System.Drawing.Size(64, 22);
            this.buttonElolrol.Text = "Elölröl";
            this.buttonElolrol.Click += new System.EventHandler(this.Elolrol);
            // 
            // buttonVissza
            // 
            this.buttonVissza.Image = ((System.Drawing.Image)(resources.GetObject("buttonVissza.Image")));
            this.buttonVissza.ImageTransparentColor = System.Drawing.Color.Black;
            this.buttonVissza.Name = "buttonVissza";
            this.buttonVissza.Size = new System.Drawing.Size(68, 22);
            this.buttonVissza.Text = "Vissza";
            this.buttonVissza.Click += new System.EventHandler(this.Vissza);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.textBox5);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.textBox4);
            this.groupBox4.Controls.Add(this.comboBox3);
            this.groupBox4.Controls.Add(this.button3);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Location = new System.Drawing.Point(3, 577);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(842, 70);
            this.groupBox4.TabIndex = 18;
            this.groupBox4.TabStop = false;
            this.groupBox4.Tag = "C,PARTNER_FOLYOSZ";
            this.groupBox4.Text = "Folyószámla";
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(502, 33);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(213, 21);
            this.textBox5.TabIndex = 17;
            this.textBox5.Tag = "MEGJEGYZES";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(499, 17);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 15);
            this.label8.TabIndex = 16;
            this.label8.Text = "Megjegyés";
            // 
            // textBox4
            // 
            this.textBox4.Form = FormattedTextBox.FormattedTextBox.formnum.None;
            this.textBox4.Format = null;
            this.textBox4.Location = new System.Drawing.Point(290, 36);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(206, 21);
            this.textBox4.TabIndex = 15;
            this.textBox4.Tag = "FOLYOSZLASZAM,,2";
            this.textBox4.Validated += new System.EventHandler(this.Elem_Validated);
            this.textBox4.TextChanged += new System.EventHandler(this.Elem_Validated);
            // 
            // comboBox3
            // 
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(21, 36);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(238, 23);
            this.comboBox3.TabIndex = 13;
            this.comboBox3.Tag = "BANKSZLA,,2";
            this.comboBox3.Validated += new System.EventHandler(this.Elem_Validated);
            this.comboBox3.TextChanged += new System.EventHandler(this.Elem_Validated);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(775, 30);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(32, 27);
            this.button3.TabIndex = 12;
            this.button3.Text = "OK";
            this.button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Tetel_ok);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(287, 17);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(106, 15);
            this.label7.TabIndex = 3;
            this.label7.Text = "Folyószámlaszám";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(112, 15);
            this.label6.TabIndex = 1;
            this.label6.Text = "Számlavezető bank";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.toolStrip1);
            this.groupBox3.Controls.Add(this.dataGVPartntetel);
            this.groupBox3.Location = new System.Drawing.Point(3, 351);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(842, 220);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Folyószámlaszámok";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonUjtetel,
            this.buttonTeteltorol});
            this.toolStrip1.Location = new System.Drawing.Point(3, 17);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(836, 25);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // buttonUjtetel
            // 
            this.buttonUjtetel.Image = ((System.Drawing.Image)(resources.GetObject("buttonUjtetel.Image")));
            this.buttonUjtetel.ImageTransparentColor = System.Drawing.Color.Black;
            this.buttonUjtetel.Name = "buttonUjtetel";
            this.buttonUjtetel.Size = new System.Drawing.Size(42, 22);
            this.buttonUjtetel.Text = "Új ";
            this.buttonUjtetel.Click += new System.EventHandler(this.Uj_tetel);
            // 
            // buttonTeteltorol
            // 
            this.buttonTeteltorol.Image = ((System.Drawing.Image)(resources.GetObject("buttonTeteltorol.Image")));
            this.buttonTeteltorol.ImageTransparentColor = System.Drawing.Color.Black;
            this.buttonTeteltorol.Name = "buttonTeteltorol";
            this.buttonTeteltorol.Size = new System.Drawing.Size(55, 22);
            this.buttonTeteltorol.Text = "Töröl";
            this.buttonTeteltorol.Click += new System.EventHandler(this.Tetel_torol);
            // 
            // dataGVPartntetel
            // 
            this.dataGVPartntetel.AllowUserToAddRows = false;
            this.dataGVPartntetel.AllowUserToDeleteRows = false;
            this.dataGVPartntetel.AllowUserToResizeColumns = false;
            this.dataGVPartntetel.AllowUserToResizeRows = false;
            this.dataGVPartntetel.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGVPartntetel.Location = new System.Drawing.Point(6, 45);
            this.dataGVPartntetel.MultiSelect = false;
            this.dataGVPartntetel.Name = "dataGVPartntetel";
            this.dataGVPartntetel.ReadOnly = true;
            this.dataGVPartntetel.RowHeadersVisible = false;
            this.dataGVPartntetel.RowHeadersWidth = 24;
            this.dataGVPartntetel.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGVPartntetel.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGVPartntetel.Size = new System.Drawing.Size(826, 169);
            this.dataGVPartntetel.TabIndex = 1;
            this.dataGVPartntetel.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGVPartntetel_CellMouseClick);
            this.dataGVPartntetel.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGVPartntetel_CellMouseClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.checkBox1);
            this.groupBox2.Controls.Add(this.comboBox2);
            this.groupBox2.Controls.Add(this.textBox3);
            this.groupBox2.Controls.Add(this.textBox2);
            this.groupBox2.Controls.Add(this.comboBox1);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(3, 261);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(842, 84);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Tag = "C,PARTNER";
            this.groupBox2.Text = "Partner";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(726, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 15);
            this.label4.TabIndex = 21;
            this.label4.Text = "Saját?";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(738, 41);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 20;
            this.checkBox1.Tag = "SAJAT";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Validated += new System.EventHandler(this.Elem_Validated);
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(502, 37);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(84, 23);
            this.comboBox2.TabIndex = 19;
            this.comboBox2.Tag = "PKOZTJ";
            this.comboBox2.Validated += new System.EventHandler(this.Elem_Validated);
            this.comboBox2.TextChanged += new System.EventHandler(this.Elem_Validated);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(615, 37);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 21);
            this.textBox3.TabIndex = 18;
            this.textBox3.Tag = "PHAZSZ";
            this.textBox3.Validated += new System.EventHandler(this.Elem_Validated);
            this.textBox3.TextChanged += new System.EventHandler(this.Elem_Validated);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(374, 37);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(122, 21);
            this.textBox2.TabIndex = 17;
            this.textBox2.Tag = "PKOZT";
            this.textBox2.Validated += new System.EventHandler(this.Elem_Validated);
            this.textBox2.TextChanged += new System.EventHandler(this.Elem_Validated);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(198, 35);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(170, 23);
            this.comboBox1.TabIndex = 16;
            this.comboBox1.Tag = "PIRSZ";
            this.comboBox1.Validated += new System.EventHandler(this.Elem_Validated);
            this.comboBox1.TextChanged += new System.EventHandler(this.Elem_Validated);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(6, 37);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(186, 21);
            this.textBox1.TabIndex = 15;
            this.textBox1.Tag = "AZONOSITO,,1";
            this.textBox1.Validated += new System.EventHandler(this.Elem_Validated);
            this.textBox1.TextChanged += new System.EventHandler(this.Elem_Validated);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(499, 17);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(70, 15);
            this.label12.TabIndex = 14;
            this.label12.Text = "Közt.jellege";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(775, 34);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(32, 27);
            this.button1.TabIndex = 13;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Partner_ok);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Partnerazonositó";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(195, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Irsz,helyiség(ker)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(371, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 15);
            this.label5.TabIndex = 8;
            this.label5.Text = "Közterület";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(612, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Házszám/Hrsz";
            // 
            // dataGVPartn
            // 
            this.dataGVPartn.AllowUserToAddRows = false;
            this.dataGVPartn.AllowUserToDeleteRows = false;
            this.dataGVPartn.AllowUserToResizeColumns = false;
            this.dataGVPartn.AllowUserToResizeRows = false;
            this.dataGVPartn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGVPartn.Location = new System.Drawing.Point(6, 20);
            this.dataGVPartn.MultiSelect = false;
            this.dataGVPartn.Name = "dataGVPartn";
            this.dataGVPartn.ReadOnly = true;
            this.dataGVPartn.RowHeadersVisible = false;
            this.dataGVPartn.RowHeadersWidth = 24;
            this.dataGVPartn.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGVPartn.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGVPartn.Size = new System.Drawing.Size(826, 201);
            this.dataGVPartn.TabIndex = 12;
            this.dataGVPartn.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGVPartn_CellMouseDoubleClick);
            this.dataGVPartn.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGVPartn_CellMouseClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGVPartn);
            this.groupBox1.Location = new System.Drawing.Point(3, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(842, 227);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Tag = "";
            this.groupBox1.Text = "Partnerek";
            // 
            // Partnerform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.toolStrip);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Partnerform";
            this.Size = new System.Drawing.Size(848, 650);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGVPartntetel)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGVPartn)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataViewPartner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataViewTetel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    


        #endregion
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton buttonUj;
        private System.Windows.Forms.ToolStripButton buttonTöröl;
        private System.Windows.Forms.ToolStripButton buttonMentes;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dataGVPartntetel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripButton buttonElolrol;
        private System.Windows.Forms.DataGridView dataGVPartn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton buttonUjtetel;
        private System.Windows.Forms.ToolStripButton buttonTeteltorol;
        private System.Data.DataView dataViewPartner;
        private System.Data.DataView dataViewTetel;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ToolStripButton buttonVissza;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBox1;
        private FormattedTextBox.FormattedTextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label8;
    

    }
}
