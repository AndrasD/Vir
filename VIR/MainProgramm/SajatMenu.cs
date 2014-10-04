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
        public int maxMagassag = 527;
        public int maxSzelesseg = 200;
        public int bilMagassag = 30;  ///23
        public int lvMagassag = 457;
        public int elemszam;
        public Control Control;
        public string[] Buttonstrings;
        public MyMenu[] menuArr;
        public object[] Elemek;


        public MyMenuArray(string[] buttonstrings, object[] elemek, Control control)
        {
            elemszam = buttonstrings.Length;
            Buttonstrings = buttonstrings;
            Elemek = elemek;
            Control = control;
            maxMagassag = Control.Size.Height;
            maxSzelesseg = Control.Size.Width;
            lvMagassag = maxMagassag - (elemszam * bilMagassag) - 1;
            Control.SizeChanged += new EventHandler(Control_SizeChanged);
            MyMenuInicializalas();
            //ControlSizeChange();

        }

        private void MyMenuInicializalas()
        {
            menuArr = new MyMenu[elemszam];
            for (int i = 0; i < elemszam; i++)
            {
                menuArr[i] = new MyMenu(this, i);
                menuArr[i].button.Click += new System.EventHandler(button_Click);

                menuArr[i].bill_felso_kor = i * bilMagassag;
                menuArr[i].list_poz       = (i + 1) * bilMagassag;
                if (i == 0)
                {
                    menuArr[i].bill_also_kor = 0;
                    menuArr[i].bill_felul = true;
                    menuArr[i].list.Visible = true;
                }
                else
                {
                    menuArr[i].bill_also_kor = maxMagassag - ((elemszam - i) * bilMagassag);
                    menuArr[i].bill_felul = false;
                    menuArr[i].list.Visible = false;
                }

                menuArr[i].button.Anchor = ((AnchorStyles)((AnchorStyles.Left | AnchorStyles.Right)));
                menuArr[i].button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                menuArr[i].button.FlatAppearance.BorderSize = 0;
                menuArr[i].button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
                menuArr[i].button.Location = new Point(0, menuArr[i].bill_also_kor);
                menuArr[i].button.Size = new System.Drawing.Size(maxSzelesseg, bilMagassag);
                menuArr[i].button.Text = Buttonstrings[i];
                menuArr[i].button.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                menuArr[i].button.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
                menuArr[i].button.BackColor = System.Drawing.Color.LightSteelBlue;
                menuArr[i].button.Cursor = System.Windows.Forms.Cursors.Hand;

                menuArr[i].list.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
                menuArr[i].list.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
                menuArr[i].list.Activation = System.Windows.Forms.ItemActivation.OneClick;
                menuArr[i].list.BorderStyle = System.Windows.Forms.BorderStyle.None;
                menuArr[i].list.FullRowSelect = true;
                menuArr[i].list.HoverSelection = false;
                menuArr[i].list.HideSelection = false;
                menuArr[i].list.LabelWrap = false;
                menuArr[i].list.ShowGroups = false;
                menuArr[i].list.ShowItemToolTips = true;
                menuArr[i].list.TabIndex = 0;
                menuArr[i].list.UseCompatibleStateImageBehavior = false;
                menuArr[i].list.View = System.Windows.Forms.View.List;
                menuArr[i].list.Size = new Size(maxSzelesseg, lvMagassag);
                menuArr[i].list.Location = new Point(0, menuArr[i].list_poz);
                menuArr[i].list.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(255)), ((System.Byte)(235)));
            }
        }

        public void Control_SizeChanged(object sender, EventArgs e)
        {
            ControlSizeChange();
        }

        public void ControlSizeChange()
        {
            maxMagassag = Control.Size.Height;
            maxSzelesseg = Control.Size.Width;
            lvMagassag = maxMagassag - (elemszam * bilMagassag) - 1;
            for (int i = 0; i < elemszam; i++)
            {
                menuArr[i].ControlSizeChanged();
            }
        }

        private void button_Click(object sender, System.EventArgs e)
        {
            Button b = (Button)sender;
            MyMenu menu = (MyMenu)b.Tag;
            for (int i = 0; i < elemszam; i++)
            {
                MyMenu menui = menuArr[i];
                menui.ButtonClick(menu.menuindex);

            }
        }
    }

    public class MyMenu
    {
        //       public int menuElem;
        //       public string[] menuButton;
        //       public string[][] listViewArray;
        //       public string[][] newFormArr;
        //       public string[][] aktFormArr;
        public MyMenuArray MenuArray;
        public Button button = new Button();
        public ListView list = new ListView();
        public int bill_felso_kor;
        public int bill_also_kor;
        public bool bill_felul;
        public int list_poz;
        public int menuindex;

        public MyMenu(MyMenuArray menuarr, int menuind)
        {
            MenuArray = menuarr;
            menuindex = menuind;
            MenuArray.Control.Controls.Add(button);
            button.Tag = this;
            MenuArray.Control.Controls.Add(list);
            list.Tag = this;
            button.TabStop = false;
            list.TabStop = false;
            object[] listelemek = (object[])MenuArray.Elemek[menuindex];
            for (int i = 0; i < listelemek.Length; i++)
            {
                ListViewItem listViewItem = new ListViewItem();
                list.Items.Add(listViewItem);
                object[] egyelem = (object[])listelemek[i];
  //              object egyalelem =egyelem[0];
                listViewItem.Text = egyelem[0].ToString();
                listViewItem.Tag = egyelem[1];
            }
        }

        public void ControlSizeChanged()
        {
            bill_felso_kor = menuindex * MenuArray.bilMagassag;
            bill_also_kor = MenuArray.maxMagassag - ((MenuArray.elemszam - menuindex) * MenuArray.bilMagassag);
            list_poz = (menuindex + 1) * MenuArray.bilMagassag;
            if (bill_felul)
                button.Location = new Point(0, bill_felso_kor);
            else
                button.Location = new Point(0, bill_also_kor);
            list.Size = new Size(MenuArray.maxSzelesseg, MenuArray.lvMagassag);
            list.Location = new Point(0, list_poz);
        }

        public void ButtonClick(int bill)
        {
            int i = menuindex;
            if (i < bill)
            {
                list.Visible = false;
                button.Location = new Point(0, bill_felso_kor);
                bill_felul = true;
            }
            if (i == bill)
            {
                button.Location = new Point(0, bill_felso_kor);
                bill_felul = true;
                list.Visible = true;
            }
            if (i > bill)
            {
                list.Visible = false;
                button.Location = new Point(0, bill_also_kor);
                bill_felul = false;
            }
        }

    }
}

