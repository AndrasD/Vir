using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using System.Drawing;
using System.Drawing.Printing;
using Törzsadatok;
using Adatbevitel;
using Lekerdezesek;
using TableInfo;

namespace MainProgramm.SajatMenu
{
    public class MyMenuArray
    {
        public int bilMagassag = 30;
        public string[] Buttonstrings;
        public System.Windows.Forms.Control Control;
        public object[] Elemek;
        public int elemszam;
        public int lvMagassag = 0x1c9;
        public int maxMagassag = 0x20f;
        public int maxSzelesseg = 200;
        public MyMenu[] menuArr;

        public MyMenuArray(string[] buttonstrings, object[] elemek, System.Windows.Forms.Control control)
        {
            this.elemszam = buttonstrings.Length;
            this.Buttonstrings = buttonstrings;
            this.Elemek = elemek;
            this.Control = control;
            this.maxMagassag = this.Control.Size.Height;
            this.maxSzelesseg = this.Control.Size.Width;
            this.lvMagassag = (this.maxMagassag - (this.elemszam * this.bilMagassag)) - 1;
            this.Control.SizeChanged += new EventHandler(this.Control_SizeChanged);
            this.MyMenuInicializalas();
        }

        private void button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            MyMenu tag = (MyMenu)button.Tag;
            for (int i = 0; i < this.elemszam; i++)
            {
                this.menuArr[i].ButtonClick(tag.menuindex);
            }
        }

        public void Control_SizeChanged(object sender, EventArgs e)
        {
            this.ControlSizeChange();
        }

        public void ControlSizeChange()
        {
            this.maxMagassag = this.Control.Size.Height;
            this.maxSzelesseg = this.Control.Size.Width;
            this.lvMagassag = (this.maxMagassag - (this.elemszam * this.bilMagassag)) - 1;
            for (int i = 0; i < this.elemszam; i++)
            {
                this.menuArr[i].ControlSizeChanged();
            }
        }

        private void MyMenuInicializalas()
        {
            this.menuArr = new MyMenu[this.elemszam];
            for (int i = 0; i < this.elemszam; i++)
            {
                this.menuArr[i] = new MyMenu(this, i);
                this.menuArr[i].button.Click += new EventHandler(this.button_Click);
                this.menuArr[i].bill_felso_kor = i * this.bilMagassag;
                this.menuArr[i].list_poz = (i + 1) * this.bilMagassag;
                if (i == 0)
                {
                    this.menuArr[i].bill_also_kor = 0;
                    this.menuArr[i].bill_felul = true;
                    this.menuArr[i].list.Visible = true;
                }
                else
                {
                    this.menuArr[i].bill_also_kor = this.maxMagassag - ((this.elemszam - i) * this.bilMagassag);
                    this.menuArr[i].bill_felul = false;
                    this.menuArr[i].list.Visible = false;
                }
                this.menuArr[i].button.Anchor = AnchorStyles.Right | AnchorStyles.Left;
                this.menuArr[i].button.FlatStyle = FlatStyle.Flat;
                this.menuArr[i].button.FlatAppearance.BorderSize = 0;
                this.menuArr[i].button.FlatAppearance.MouseOverBackColor = Color.WhiteSmoke;
                this.menuArr[i].button.Location = new Point(0, this.menuArr[i].bill_also_kor);
                this.menuArr[i].button.Size = new Size(this.maxSzelesseg, this.bilMagassag);
                this.menuArr[i].button.Text = this.Buttonstrings[i];
                this.menuArr[i].button.TextAlign = ContentAlignment.MiddleLeft;
                this.menuArr[i].button.Font = new Font("Arial", 8.25f, FontStyle.Bold);
                this.menuArr[i].button.BackColor = Color.LightSteelBlue;
                this.menuArr[i].button.Cursor = Cursors.Hand;
                this.menuArr[i].list.Anchor = AnchorStyles.Right | AnchorStyles.Left;
                this.menuArr[i].list.Font = new Font("Arial", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
                this.menuArr[i].list.Activation = ItemActivation.OneClick;
                this.menuArr[i].list.BorderStyle = BorderStyle.None;
                this.menuArr[i].list.FullRowSelect = true;
                this.menuArr[i].list.HoverSelection = false;
                this.menuArr[i].list.HideSelection = false;
                this.menuArr[i].list.LabelWrap = false;
                this.menuArr[i].list.ShowGroups = false;
                this.menuArr[i].list.ShowItemToolTips = true;
                this.menuArr[i].list.TabIndex = 0;
                this.menuArr[i].list.UseCompatibleStateImageBehavior = false;
                this.menuArr[i].list.View = View.List;
                this.menuArr[i].list.Size = new Size(this.maxSzelesseg, this.lvMagassag);
                this.menuArr[i].list.Location = new Point(0, this.menuArr[i].list_poz);
                this.menuArr[i].list.BackColor = Color.FromArgb(0xff, 0xff, 0xeb);
            }
        }
    }

    public class MyMenu
    {
        public int bill_also_kor;
        public int bill_felso_kor;
        public bool bill_felul;
        public Button button = new Button();
        public ListView list = new ListView();
        public int list_poz;
        public MyMenuArray MenuArray;
        public int menuindex;

        public MyMenu(MyMenuArray menuarr, int menuind)
        {
            this.MenuArray = menuarr;
            this.menuindex = menuind;
            this.MenuArray.Control.Controls.Add(this.button);
            this.button.Tag = this;
            this.MenuArray.Control.Controls.Add(this.list);
            this.list.Tag = this;
            this.button.TabStop = false;
            this.list.TabStop = false;
            object[] objArray = (object[])this.MenuArray.Elemek[this.menuindex];
            for (int i = 0; i < objArray.Length; i++)
            {
                ListViewItem item = new ListViewItem();
                this.list.Items.Add(item);
                object[] objArray2 = (object[])objArray[i];
                item.Text = objArray2[0].ToString();
                item.Tag = objArray2[1];
            }
        }

        public void ButtonClick(int bill)
        {
            int menuindex = this.menuindex;
            if (menuindex < bill)
            {
                this.list.Visible = false;
                this.button.Location = new Point(0, this.bill_felso_kor);
                this.bill_felul = true;
            }
            if (menuindex == bill)
            {
                this.button.Location = new Point(0, this.bill_felso_kor);
                this.bill_felul = true;
                this.list.Visible = true;
            }
            if (menuindex > bill)
            {
                this.list.Visible = false;
                this.button.Location = new Point(0, this.bill_also_kor);
                this.bill_felul = false;
            }
        }

        public void ControlSizeChanged()
        {
            this.bill_felso_kor = this.menuindex * this.MenuArray.bilMagassag;
            this.bill_also_kor = this.MenuArray.maxMagassag - ((this.MenuArray.elemszam - this.menuindex) * this.MenuArray.bilMagassag);
            this.list_poz = (this.menuindex + 1) * this.MenuArray.bilMagassag;
            if (this.bill_felul)
            {
                this.button.Location = new Point(0, this.bill_felso_kor);
            }
            else
            {
                this.button.Location = new Point(0, this.bill_also_kor);
            }
            this.list.Size = new Size(this.MenuArray.maxSzelesseg, this.MenuArray.lvMagassag);
            this.list.Location = new Point(0, this.list_poz);
        }
    }
}

