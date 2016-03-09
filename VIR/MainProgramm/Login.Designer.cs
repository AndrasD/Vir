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
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.rendben = new System.Windows.Forms.Button();
            this.megsem = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textUser = new System.Windows.Forms.TextBox();
            this.textPWD = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.comboConn = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Black;
            this.imageList.Images.SetKeyName(0, "rendben-16x16.bmp");
            this.imageList.Images.SetKeyName(1, "eldob-16x16.bmp");
            // 
            // rendben
            // 
            this.rendben.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rendben.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rendben.ImageIndex = 0;
            this.rendben.ImageList = this.imageList;
            this.rendben.Location = new System.Drawing.Point(53, 169);
            this.rendben.Name = "rendben";
            this.rendben.Size = new System.Drawing.Size(104, 27);
            this.rendben.TabIndex = 3;
            this.rendben.Text = "Rendben";
            this.rendben.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rendben.UseVisualStyleBackColor = true;
            this.rendben.Click += new System.EventHandler(this.rendben_Click);
            // 
            // megsem
            // 
            this.megsem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.megsem.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.megsem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.megsem.ImageIndex = 1;
            this.megsem.ImageList = this.imageList;
            this.megsem.Location = new System.Drawing.Point(166, 169);
            this.megsem.Name = "megsem";
            this.megsem.Size = new System.Drawing.Size(104, 27);
            this.megsem.TabIndex = 4;
            this.megsem.Text = "Mégsem";
            this.megsem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.megsem.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(100, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "Azonosító:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(100, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 19);
            this.label2.TabIndex = 3;
            this.label2.Text = "Jelszó:";
            // 
            // textUser
            // 
            this.textUser.Location = new System.Drawing.Point(205, 38);
            this.textUser.MaxLength = 30;
            this.textUser.Name = "textUser";
            this.textUser.Size = new System.Drawing.Size(124, 26);
            this.textUser.TabIndex = 0;
            // 
            // textPWD
            // 
            this.textPWD.Location = new System.Drawing.Point(205, 66);
            this.textPWD.MaxLength = 15;
            this.textPWD.Name = "textPWD";
            this.textPWD.Size = new System.Drawing.Size(124, 26);
            this.textPWD.TabIndex = 1;
            this.textPWD.UseSystemPasswordChar = true;
            this.textPWD.Leave += new System.EventHandler(this.textPWD_Leave);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.ImageList = this.imageList;
            this.button1.Location = new System.Drawing.Point(279, 169);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 27);
            this.button1.TabIndex = 5;
            this.button1.Text = "Beállítások >>";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(101, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 19);
            this.label3.TabIndex = 9;
            this.label3.Text = "Év:";
            // 
            // comboConn
            // 
            this.comboConn.DisplayMember = "year";
            this.comboConn.FormattingEnabled = true;
            this.comboConn.Location = new System.Drawing.Point(205, 95);
            this.comboConn.Name = "comboConn";
            this.comboConn.Size = new System.Drawing.Size(124, 26);
            this.comboConn.TabIndex = 10;
            this.comboConn.ValueMember = "connstring";
            // 
            // Bejelentkezes
            // 
            this.AcceptButton = this.rendben;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.megsem;
            this.ClientSize = new System.Drawing.Size(429, 229);
            this.Controls.Add(this.comboConn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textPWD);
            this.Controls.Add(this.textUser);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.megsem);
            this.Controls.Add(this.rendben);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Bejelentkezes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bejelentkezés";
            this.Activated += new System.EventHandler(this.Bejelentkezes_Activated);
            this.Load += new System.EventHandler(this.Bejelentkezes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.Button megsem;
        private System.Windows.Forms.Button rendben;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textPWD;
        private System.Windows.Forms.TextBox textUser;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboConn;
    }
}