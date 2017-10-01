using Könyvtar.ClassGyujtemeny;
using MainProgramm.Properties;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MainProgramm.Bejelentkezes
{
    partial class Bejelentkezes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Bejelentkezes));
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textUser = new System.Windows.Forms.TextBox();
            this.textPWD = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboConn = new System.Windows.Forms.ComboBox();
            this.rendben = new System.Windows.Forms.Button();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.megsem = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // pictureBox1
            // 
            this.errorProvider.SetIconAlignment(this.pictureBox1, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(21, 61);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(101, 111);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(158, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "Azonosító:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(158, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 19);
            this.label2.TabIndex = 3;
            this.label2.Text = "Jelszó:";
            // 
            // textUser
            // 
            this.textUser.Location = new System.Drawing.Point(251, 62);
            this.textUser.MaxLength = 30;
            this.textUser.Name = "textUser";
            this.textUser.Size = new System.Drawing.Size(124, 26);
            this.textUser.TabIndex = 0;
            // 
            // textPWD
            // 
            this.textPWD.Location = new System.Drawing.Point(251, 90);
            this.textPWD.MaxLength = 15;
            this.textPWD.Name = "textPWD";
            this.textPWD.Size = new System.Drawing.Size(124, 26);
            this.textPWD.TabIndex = 1;
            this.textPWD.UseSystemPasswordChar = true;
            this.textPWD.Leave += new System.EventHandler(this.textPWD_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(159, 121);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 19);
            this.label3.TabIndex = 9;
            this.label3.Text = "Év:";
            // 
            // comboConn
            // 
            this.comboConn.DisplayMember = "year";
            this.comboConn.FormattingEnabled = true;
            this.comboConn.Location = new System.Drawing.Point(251, 119);
            this.comboConn.Name = "comboConn";
            this.comboConn.Size = new System.Drawing.Size(124, 26);
            this.comboConn.TabIndex = 10;
            this.comboConn.ValueMember = "connstring";
            // 
            // rendben
            // 
            this.rendben.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rendben.ImageIndex = 0;
            this.rendben.ImageList = this.imageList;
            this.rendben.Location = new System.Drawing.Point(165, 178);
            this.rendben.Name = "rendben";
            this.rendben.Size = new System.Drawing.Size(97, 27);
            this.rendben.TabIndex = 3;
            this.rendben.Text = "Rendben";
            this.rendben.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rendben.UseVisualStyleBackColor = true;
            this.rendben.Click += new System.EventHandler(this.rendben_Click);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Black;
            this.imageList.Images.SetKeyName(0, "rendben-16x16.bmp");
            this.imageList.Images.SetKeyName(1, "eldob-16x16.bmp");
            // 
            // megsem
            // 
            this.megsem.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.megsem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.megsem.ImageIndex = 1;
            this.megsem.ImageList = this.imageList;
            this.megsem.Location = new System.Drawing.Point(278, 178);
            this.megsem.Name = "megsem";
            this.megsem.Size = new System.Drawing.Size(97, 27);
            this.megsem.TabIndex = 4;
            this.megsem.Text = "Mégsem";
            this.megsem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.megsem.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.Controls.Add(this.label4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(411, 34);
            this.panel1.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label4.Location = new System.Drawing.Point(147, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(140, 24);
            this.label4.TabIndex = 6;
            this.label4.Text = "Bejelentkezés";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Control;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(22, 179);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 19);
            this.textBox1.TabIndex = 13;
            this.textBox1.Text = "VIR 2.0";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Bejelentkezes
            // 
            this.AcceptButton = this.rendben;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.megsem;
            this.ClientSize = new System.Drawing.Size(411, 217);
            this.ControlBox = false;
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.comboConn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textPWD);
            this.Controls.Add(this.textUser);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.megsem);
            this.Controls.Add(this.rendben);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Bejelentkezes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Activated += new System.EventHandler(this.Bejelentkezes_Activated);
            this.Load += new System.EventHandler(this.Bejelentkezes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Button megsem;
        private System.Windows.Forms.Button rendben;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textPWD;
        private System.Windows.Forms.TextBox textUser;
        private Label label2;
        private Label label3;
        private Label label4;
        private Panel panel1;
        private System.Windows.Forms.ComboBox comboConn;
        private PictureBox pictureBox1;
        private ImageList imageList;
        private TextBox textBox1;
    }
}