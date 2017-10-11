using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using TableInfo;

namespace Adatbevitel
{
    public partial class PenzMozgas : UserControl
    {
        private Tablainfo Mozgasinfo;
        private DataTable MozgasAdattabla;
        private int Mozgasaktsorindex = -1;
        private int Mozgasaktgridrowind = -1;
        private int Mozgasidcol = -1;
        private int Mozgasaktid;
        private Fak Fak;
        private MyTag mozgastag;
        private Tablainfo partnerinfo;
        private Tablainfo partnertetelinfo;
        private Tablainfo penztarinfo;
        private Tablainfo[] mozgasadatok;
        private bool igazivaltozas = false;
        private string[] fszids;
        private int click = -1;
        private bool tobbhonap = false;
        private DateTime tol = Convert.ToDateTime("1801.01.01");
        private DateTime ig = Convert.ToDateTime("9900.12.31");
        private Egyallapotinfo egyallapot;
        private string szint;
        private MyTag penztar;


        public PenzMozgas(string szoveg, object[] obj)
        {
            MyTag[] tagArray = (MyTag[])obj[1];
            this.mozgastag = tagArray[0];
            this.Mozgasinfo = this.mozgastag.AdatTablainfo;
            this.partnerinfo = tagArray[1].AdatTablainfo;
            this.partnertetelinfo = tagArray[2].AdatTablainfo;
            this.penztarinfo = tagArray[3].AdatTablainfo;
            this.mozgasadatok = new Tablainfo[] { this.Mozgasinfo };
            string[] strArray = this.partnerinfo.FindIdentityArray(new string[] { "SAJAT" }, new string[] { "I" });
            if (strArray == null)
            {
                MessageBox.Show("Nincs 'saj\x00e1t' jel\x00fc partner!");
                base.Visible = false;
            }
            if (base.Visible)
            {
                int num;
                ArrayList list = new ArrayList();
                for (num = 0; num < strArray.Length; num++)
                {
                    string[] strArray2 = this.partnertetelinfo.FindIdentityArray(new string[] { "PID" }, new string[] { strArray[num] });
                    for (int i = 0; i < strArray2.Length; i++)
                    {
                        list.Add(strArray2[i]);
                    }
                }
                this.fszids = new string[list.Count];
                for (num = 0; num < this.fszids.Length; num++)
                {
                    this.fszids[num] = list[num].ToString();
                }
                this.InitializeComponent();
                this.Fak = this.mozgastag.Fak;
                this.buttonMentes.Enabled = false;
                this.dataGVMozg.AutoGenerateColumns = false;
                DataGridViewColumn[] gridColumns = this.Mozgasinfo.GetGridColumns();
                this.dataGVMozg.Columns.AddRange(gridColumns);
                this.Mozgasidcol = this.Mozgasinfo.Identitycol;
                this.MozgasAdattabla = this.Mozgasinfo.Adattabla;
                this.dataViewMozgas.Table = this.MozgasAdattabla;
                this.dataViewMozgas.Sort = this.Mozgasinfo.Sort;
                this.dateTimePicker4.Value = Convert.ToDateTime(this.dateTimePicker4.Value.Year.ToString() + "." + this.dateTimePicker4.Value.Month.ToString() + ".01");
                DateTime time = Convert.ToDateTime(this.dateTimePicker5.Value.Year.ToString() + "." + this.dateTimePicker5.Value.Month.ToString() + ".01");
                this.dateTimePicker5.Value = Convert.ToDateTime(time.AddMonths(1).AddDays(-1.0));
                this.dateTimePicker1.MinDate = this.dateTimePicker4.Value;
                this.dateTimePicker1.MaxDate = this.dateTimePicker5.Value;
                this.dateTimePicker1.Value = this.dateTimePicker4.Value;
                this.dateTimePicker2.MinDate = this.dateTimePicker4.Value;
                this.dateTimePicker2.Value = this.dateTimePicker4.Value;
                this.dateTimePicker3.MinDate = this.dateTimePicker4.Value;
                this.dateTimePicker3.Value = this.dateTimePicker4.Value;
                this.szint = obj[2].ToString();
                if (this.szint == "1")
                {
                    this.mozgasadatok = this.Fak.Adatoktolt("C", this.Fak.Aktintervallum, this.mozgasadatok, " WHERE DATUM between cast('" + this.dateTimePicker1.MinDate.ToShortDateString() + "' as datetime) AND cast('" + this.dateTimePicker1.MaxDate.ToShortDateString() + "' as datetime)", false);
                }
                else
                {
                    this.mozgasadatok = this.Fak.Adatoktolt("C", this.Fak.Aktintervallum, this.mozgasadatok, " WHERE DATUM between cast('" + this.dateTimePicker1.MinDate.ToShortDateString() + "' as datetime) AND cast('" + this.dateTimePicker1.MaxDate.ToShortDateString() + "' as datetime) and penztar_id <> 407 and h_penztar_id <> 407", false);
                }
                if (this.dataViewMozgas.Count == 0)
                {
                    this.Mozgasaktgridrowind = -1;
                    this.click = 2;
                }
                else
                {
                    this.Mozgasaktgridrowind = 0;
                    this.click = 1;
                }
                this.dataGVMozg.DataSource = this.dataViewMozgas;
                this.Fak.ControlTagokTolt(this, new Control[] { this.groupBox2, this.groupBox3, this.groupBox4 });
                this.penztar = this.Fak.GetKodtab("C", "PENZT");
                this.egyallapot = this.Mozgasinfo.GetEgyallapotinfo(this);
                this.Mozgasinfo.Comboinfoszures(this.comboBox2, this.fszids);
                this.Mozgasinfo.Comboinfoszures(this.comboBox4, this.fszids);
                this.Mozgas_beallit(this.Mozgasaktgridrowind);
                this.igazivaltozas = true;
            }
        }

        private void buttonMentes_Click(object sender, EventArgs e)
        {
            ArrayList modositandok = new ArrayList {
                this.Mozgasinfo
            };
            this.igazivaltozas = false;
            if (!this.Fak.UpdateTransaction(modositandok))
            {
                base.Enabled = true;
                base.Visible = false;
            }
            if (base.Visible)
            {
                if (this.dataViewMozgas.Count == 0)
                {
                    this.Mozgasaktgridrowind = -1;
                    this.click = 2;
                }
                else
                {
                    this.Mozgasaktgridrowind = this.dataViewMozgas.Count - 1;
                    this.click = 1;
                }
                this.Mozgas_beallit(this.Mozgasaktgridrowind);
            }
            this.buttonMentes.Enabled = false;
            this.egyallapot.Modositott = false;
            this.egyallapot.Mentett = true;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            DataRow[] rowArray;
            string text = "";
            if (this.rBHonnanPenztar.Checked)
            {
                rowArray = this.penztar.AdatTablainfo.Adattabla.Select("szoveg = '" + this.comboBox6.Text + "'");
                if (((rowArray.Length > 0) && (rowArray[0]["megj"].ToString() != "")) && (rowArray[0]["megj"].ToString() != this.szint))
                {
                    text = "Nincs joga erre a p\x00e9nzt\x00e1rra k\x00f6nyvelni!";
                    MessageBox.Show(text);
                }
            }
            if (this.rBHovaPenztar.Checked && (text == ""))
            {
                rowArray = this.penztar.AdatTablainfo.Adattabla.Select("szoveg = '" + this.comboBox7.Text + "'");
                if (((rowArray.Length > 0) && (rowArray[0]["megj"].ToString() != "")) && (rowArray[0]["megj"].ToString() != this.szint))
                {
                    text = "Nincs joga erre a p\x00e9nzt\x00e1rra k\x00f6nyvelni!";
                    MessageBox.Show(text);
                }
            }
            if ((text == "") && (this.Fak.Hibavizsg(this, this.Mozgasinfo, null) == ""))
            {
                string str2 = "";
                if (((this.comboBox2.Text != "") && (this.comboBox4.Text != "")) && (this.comboBox2.Text == this.comboBox4.Text))
                {
                    str2 = "Honnan Bank = Hov\x00e1 Bank!\n";
                }
                if (((this.comboBox6.Text != "") && (this.comboBox7.Text != "")) && (this.comboBox6.Text == this.comboBox7.Text))
                {
                    str2 = str2 + "Honnan P\x00e9nzt\x00e1r = Hova P\x00e9nzt\x00e1r!";
                }
                if (str2 == "")
                {
                    int num;
                    this.buttonUj.Enabled = false;
                    this.buttonTöröl.Enabled = true;
                    if (this.Mozgasaktgridrowind == -1)
                    {
                        this.MozgasAdattabla = this.Mozgasinfo.Ujsor();
                        this.Mozgasaktsorindex = this.MozgasAdattabla.Rows.Count - 1;
                        num = 2;
                    }
                    else
                    {
                        num = 1;
                    }
                    DataRow row = this.MozgasAdattabla.Rows[this.Mozgasaktsorindex];
                    row = this.Mozgasinfo.Adatsortolt(this.Mozgasaktsorindex);
                    this.click = num;
                    this.Mozgas_beallit(this.Mozgasaktgridrowind);
                    this.buttonMentes.Enabled = true;
                    this.egyallapot.Modositott = true;
                }
                this.Fak.ErrorProvider.SetError(this.buttonOK, str2);
            }
        }

        private void buttonUj_Click(object sender, EventArgs e)
        {
            this.click = 2;
            this.Mozgas_beallit(-1);
        }

        private void buttonVissza_Click(object sender, EventArgs e)
        {
            if (!(this.buttonMentes.Enabled && (MessageBox.Show("A valtozasok elvesszenek?", "", System.Windows.Forms.MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)))
            {
                this.igazivaltozas = false;
                this.Fak.ForceAdattolt(this.Mozgasinfo);
                this.egyallapot.Modositott = false;
                base.Visible = false;
            }
        }

        private void ComboBeallitasok()
        {
            if (this.click == 2)
            {
                if (this.comboBox1.Enabled)
                {
                    this.Fak.SetCombodef(this.comboBox1);
                }
                else
                {
                    this.Fak.ClearCombodef(this.comboBox1);
                }
                if (this.comboBox2.Enabled)
                {
                    this.Fak.SetCombodef(this.comboBox2);
                }
                else
                {
                    this.Fak.ClearCombodef(this.comboBox2);
                }
                if (this.comboBox3.Enabled)
                {
                    this.Fak.SetCombodef(this.comboBox3);
                }
                else
                {
                    this.Fak.ClearCombodef(this.comboBox3);
                }
                if (this.comboBox4.Enabled)
                {
                    this.Fak.SetCombodef(this.comboBox4);
                }
                else
                {
                    this.Fak.ClearCombodef(this.comboBox4);
                }
                if (this.comboBox6.Enabled)
                {
                    this.Fak.SetCombodef(this.comboBox6);
                }
                else
                {
                    this.Fak.ClearCombodef(this.comboBox6);
                }
                if (this.comboBox7.Enabled)
                {
                    this.Fak.SetCombodef(this.comboBox7);
                }
                else
                {
                    this.Fak.ClearCombodef(this.comboBox7);
                }
            }
        }

        private void comboBox_EnabledChanged(object sender, EventArgs e)
        {
            if (this.igazivaltozas)
            {
                this.ComboBeallitasok();
            }
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.rBHonnanPenztar.Checked)
            {
                DataRow[] rowArray = this.penztar.AdatTablainfo.Adattabla.Select("szoveg = '" + this.comboBox6.Text + "'");
                if (((rowArray.Length > 0) && (rowArray[0]["megj"].ToString() != "")) && (rowArray[0]["megj"].ToString() != this.szint))
                {
                    MessageBox.Show("Nincs joga erre a p\x00e9nzt\x00e1rra k\x00f6nyvelni!");
                }
            }
        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.rBHovaPenztar.Checked)
            {
                DataRow[] rowArray = this.penztar.AdatTablainfo.Adattabla.Select("szoveg = '" + this.comboBox7.Text + "'");
                if (((rowArray.Length > 0) && (rowArray[0]["megj"].ToString() != "")) && (rowArray[0]["megj"].ToString() != this.szint))
                {
                    MessageBox.Show("Nincs joga erre a p\x00e9nzt\x00e1rra k\x00f6nyvelni!");
                }
            }
        }

        private void dataGVMozg_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.click = e.Clicks;
            if (this.click == 1)
            {
                this.Mozgas_beallit(e.RowIndex);
            }
        }

        private void dataGVMozg_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.buttonUj.Enabled = false;
            this.buttonTöröl.Enabled = false;
            this.click = 2;
            this.Mozgas_beallit(e.RowIndex);
        }

        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            bool igazivaltozas = this.igazivaltozas;
            if (this.igazivaltozas)
            {
                DateTime time;
                this.igazivaltozas = false;
                DateTimePicker control = (DateTimePicker)sender;
                this.Fak.ErrorProvider.SetError(control, "");
                if (control == this.dateTimePicker4)
                {
                    this.dateTimePicker4.Value = Convert.ToDateTime(this.dateTimePicker4.Value.Year.ToString() + "." + this.dateTimePicker4.Value.Month.ToString() + ".01");
                    this.dateTimePicker5.Value = Convert.ToDateTime(this.dateTimePicker4.Value.AddMonths(1).AddDays(-1.0));
                }
                else
                {
                    time = Convert.ToDateTime(this.dateTimePicker5.Value.Year.ToString() + "." + this.dateTimePicker5.Value.Month.ToString() + ".01");
                    this.dateTimePicker5.Value = Convert.ToDateTime(time.AddMonths(1).AddDays(-1.0));
                    if (this.dateTimePicker4.Value.CompareTo(this.dateTimePicker5.Value) > 0)
                    {
                        this.dateTimePicker4.Value = Convert.ToDateTime(this.dateTimePicker5.Value.Year.ToString() + "." + this.dateTimePicker5.Value.Month.ToString() + ".01");
                    }
                }
                time = Convert.ToDateTime(this.dateTimePicker5.Value.Year.ToString() + "." + this.dateTimePicker5.Value.Month.ToString() + ".01");
                this.dateTimePicker5.Value = Convert.ToDateTime(time.AddMonths(1).AddDays(-1.0));
                if (this.szint == "1")
                {
                    this.mozgasadatok = this.Fak.Adatoktolt("C", this.Fak.Aktintervallum, this.mozgasadatok, " WHERE DATUM between cast('" + this.dateTimePicker4.Value.ToShortDateString() + "' as datetime) AND cast('" + this.dateTimePicker5.Value.ToShortDateString() + "' as datetime)", false);
                }
                else
                {
                    this.mozgasadatok = this.Fak.Adatoktolt("C", this.Fak.Aktintervallum, this.mozgasadatok, " WHERE DATUM between cast('" + this.dateTimePicker4.Value.ToShortDateString() + "' as datetime) AND cast('" + this.dateTimePicker5.Value.ToShortDateString() + "' as datetime) and penztar_id <> 407 and h_penztar_id <> 407", false);
                }
                if ((this.dateTimePicker4.Value.Year != this.dateTimePicker5.Value.Year) || ((this.dateTimePicker4.Value.Year == this.dateTimePicker5.Value.Year) && (this.dateTimePicker4.Value.Month != this.dateTimePicker5.Value.Month)))
                {
                    this.tobbhonap = true;
                }
                else
                {
                    this.tobbhonap = false;
                }
                if (this.dataViewMozgas.Count == 0)
                {
                    this.Mozgasaktgridrowind = -1;
                }
                else
                {
                    this.Mozgasaktgridrowind = 0;
                }
                this.click = 2;
                this.Mozgas_beallit(this.Mozgasaktgridrowind);
            }
            this.igazivaltozas = igazivaltozas;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            bool igazivaltozas = this.igazivaltozas;
            DateTimePicker picker = (DateTimePicker)sender;
            if (this.igazivaltozas)
            {
                this.igazivaltozas = false;
                this.dateTimePicker2.MinDate = this.tol;
                if (this.dateTimePicker2.Value.CompareTo(this.dateTimePicker1.Value) < 0)
                {
                    this.dateTimePicker2.Value = this.dateTimePicker1.Value;
                }
                this.dateTimePicker2.MinDate = this.dateTimePicker1.Value;
                this.dateTimePicker3.MinDate = this.tol;
                if (this.dateTimePicker3.Value.CompareTo(this.dateTimePicker1.Value) < 0)
                {
                    this.dateTimePicker3.Value = this.dateTimePicker1.Value;
                }
                this.dateTimePicker3.MinDate = this.dateTimePicker1.Value;
            }
            this.igazivaltozas = igazivaltozas;
        }

        private void Elem_Validated(object sender, EventArgs e)
        {
            if (base.Visible && this.Focused)
            {
                Control control = (Control)sender;
                if ((control.Tag != null) && this.igazivaltozas)
                {
                    Taggyart tag = (Taggyart)((Control)sender).Tag;
                    string str = this.Fak.Hibavizsg(this, tag.Tabinfo, (Control)sender);
                }
            }
        }

        private void Elolrol(object sender, EventArgs e)
        {
            this.igazivaltozas = false;
            this.MozgasAdattabla = this.Fak.ForceAdattolt(this.Mozgasinfo);
            if (this.dataViewMozgas.Count == 0)
            {
                this.Mozgasaktgridrowind = -1;
                this.click = 2;
            }
            else
            {
                this.Mozgasaktgridrowind = 0;
                this.click = 1;
            }
            this.Mozgas_beallit(this.Mozgasaktgridrowind);
            this.egyallapot.Modositott = false;
            this.igazivaltozas = true;
        }

        private void Mozgas_beallit(int gridind)
        {
            this.igazivaltozas = false;
            this.dateTimePicker1.MinDate = this.tol;
            this.dateTimePicker1.MaxDate = this.ig;
            this.dateTimePicker1.MinDate = this.dateTimePicker4.Value;
            this.dateTimePicker1.MaxDate = this.dateTimePicker5.Value;
            this.dateTimePicker1.Value = this.dateTimePicker4.Value;
            for (int i = 0; i < this.dataViewMozgas.Count; i++)
            {
                this.dataGVMozg.Rows[i].Selected = false;
            }
            this.Mozgasaktgridrowind = gridind;
            if ((this.click == 1) && this.groupBox2.Enabled)
            {
                this.groupBox2.Enabled = false;
                this.groupBox3.Enabled = false;
                this.groupBox4.Enabled = false;
            }
            if (!((this.click != 2) || this.groupBox2.Enabled))
            {
                this.groupBox2.Enabled = true;
                this.groupBox3.Enabled = true;
                this.groupBox4.Enabled = true;
            }
            if (this.Mozgasaktgridrowind != -1)
            {
                this.Mozgasaktsorindex = this.MozgasAdattabla.Rows.IndexOf(this.dataViewMozgas[this.Mozgasaktgridrowind].Row);
                this.Mozgasinfo.Aktsorindex = this.Mozgasaktsorindex;
                this.dataGVMozg.Rows[this.Mozgasaktgridrowind].Selected = true;
                this.buttonUj.Enabled = true;
                this.buttonTöröl.Enabled = true;
                if (this.tobbhonap)
                {
                    this.groupBox2.Enabled = false;
                    this.groupBox3.Enabled = false;
                    this.groupBox4.Enabled = false;
                }
            }
            else
            {
                this.Mozgasaktid = -1;
                this.Mozgasaktsorindex = -1;
                this.Mozgasinfo.Aktsorindex = this.Mozgasaktsorindex;
                this.buttonUj.Enabled = false;
                this.buttonTöröl.Enabled = false;
                this.dateTimePicker2.Enabled = false;
                this.dateTimePicker3.Enabled = false;
                this.comboBox1.Enabled = false;
                this.comboBox3.Enabled = false;
                this.checkBox1.Enabled = false;
                this.checkBox2.Enabled = false;
            }
            this.dateTimePicker2.MinDate = this.tol;
            if ((this.dateTimePicker2.Value.CompareTo(this.dateTimePicker1.Value) < 0) || (this.Mozgasaktid == -1))
            {
                this.dateTimePicker2.Value = this.dateTimePicker1.Value;
            }
            this.dateTimePicker2.MinDate = this.dateTimePicker1.Value;
            this.dateTimePicker3.MinDate = this.tol;
            if ((this.dateTimePicker3.Value.CompareTo(this.dateTimePicker1.Value) < 0) || (this.Mozgasaktid == -1))
            {
                this.dateTimePicker3.Value = this.dateTimePicker1.Value;
            }
            this.dateTimePicker3.MinDate = this.dateTimePicker1.Value;
            this.RadiobuttonBeallitasok();
            this.igazivaltozas = true;
        }

        private void Mozgas_torol(object sender, EventArgs e)
        {
            for (int i = 0; i < this.dataGVMozg.Rows.Count; i++)
            {
                if (this.dataGVMozg.Rows[i].Selected)
                {
                    this.Mozgasaktgridrowind = i;
                    this.dataGVMozg.Rows[i].Selected = false;
                    this.Mozgasaktsorindex = this.MozgasAdattabla.Rows.IndexOf(this.dataViewMozgas[this.Mozgasaktgridrowind].Row);
                    break;
                }
            }
            this.MozgasAdattabla = this.Mozgasinfo.Adatsortorol(this.Mozgasaktsorindex);
            if (this.dataViewMozgas.Count == 0)
            {
                this.Mozgasaktgridrowind = -1;
                this.Mozgasaktsorindex = -1;
                this.click = 2;
            }
            else
            {
                this.click = 1;
                if (this.Mozgasaktgridrowind > (this.dataViewMozgas.Count - 1))
                {
                    this.Mozgasaktgridrowind--;
                }
                this.dataGVMozg.Rows[this.Mozgasaktgridrowind].Selected = true;
                this.Mozgasaktsorindex = this.MozgasAdattabla.Rows.IndexOf(this.dataViewMozgas[this.Mozgasaktgridrowind].Row);
            }
            this.Mozgas_beallit(this.Mozgasaktgridrowind);
            this.buttonMentes.Enabled = true;
            this.egyallapot.Modositott = true;
        }

        private void RadiobuttonBeallitasok()
        {
            bool igazivaltozas = this.igazivaltozas;
            this.igazivaltozas = false;
            if (this.rBHovaKulso.Checked)
            {
                this.rBHovaKulso.Enabled = true;
                this.rBHonnanKulso.Checked = false;
                this.rBHonnanKulso.Enabled = false;
                this.dateTimePicker2.Enabled = false;
                this.comboBox1.Enabled = true;
                this.checkBox1.Enabled = false;
                this.checkBox1.Checked = false;
                this.comboBox3.Enabled = false;
                this.dateTimePicker3.Enabled = true;
                this.checkBox2.Enabled = true;
                this.comboBox2.Enabled = false;
                this.comboBox6.Enabled = false;
                this.comboBox7.Enabled = false;
                this.comboBox4.Enabled = false;
            }
            else if (this.rBHonnanKulso.Checked)
            {
                this.rBHonnanKulso.Enabled = true;
                this.rBHovaKulso.Checked = false;
                this.rBHovaKulso.Enabled = false;
                this.comboBox3.Enabled = true;
                this.dateTimePicker3.Enabled = false;
                this.checkBox2.Enabled = false;
                this.checkBox2.Checked = false;
                this.dateTimePicker2.Enabled = true;
                this.comboBox1.Enabled = false;
                this.checkBox1.Enabled = true;
                this.comboBox2.Enabled = false;
                this.comboBox6.Enabled = false;
                this.comboBox7.Enabled = false;
                this.comboBox4.Enabled = false;
            }
            else
            {
                this.rBHonnanKulso.Enabled = true;
                this.rBHovaKulso.Enabled = true;
                this.dateTimePicker2.Enabled = false;
                this.comboBox1.Enabled = true;
                this.checkBox1.Enabled = false;
                this.checkBox1.Checked = false;
                this.comboBox3.Enabled = false;
                this.dateTimePicker3.Enabled = false;
                this.checkBox2.Enabled = false;
                this.checkBox2.Checked = false;
            }
            if (this.rBHonnanBank.Checked)
            {
                this.comboBox6.Enabled = false;
                this.comboBox2.Enabled = true;
            }
            if (this.rBHonnanPenztar.Checked)
            {
                this.comboBox6.Enabled = true;
                this.comboBox2.Enabled = false;
            }
            if (this.rBHovaBank.Checked)
            {
                this.comboBox7.Enabled = false;
                this.comboBox4.Enabled = true;
            }
            if (this.rBHovaPenztar.Checked)
            {
                this.comboBox7.Enabled = true;
                this.comboBox4.Enabled = false;
            }
            if (this.click == 2)
            {
                this.ComboBeallitasok();
            }
            this.igazivaltozas = igazivaltozas;
        }

        private void rb_CheckChanged(object sender, EventArgs e)
        {
            if (this.igazivaltozas && this.groupBox2.Enabled)
            {
                this.click = 2;
                this.RadiobuttonBeallitasok();
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (this.textBox1.Text == "0")
            {
                this.textBox1.Text = "";
            }
        }


    }
}
