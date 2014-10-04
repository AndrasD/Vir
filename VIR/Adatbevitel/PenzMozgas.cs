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

        public PenzMozgas(string szoveg, object[] obj)
        {
            MyTag[] myTag = (MyTag[])obj[1];
            mozgastag = myTag[0];
            Mozgasinfo = mozgastag.AdatTablainfo;
            partnerinfo = myTag[1].AdatTablainfo;
            partnertetelinfo = myTag[2].AdatTablainfo;
            penztarinfo = myTag[3].AdatTablainfo;
            mozgasadatok = new Tablainfo[] { Mozgasinfo };
            string[] pidt = partnerinfo.FindIdentityArray(new string[] { "SAJAT" }, new string[] { "I" });
            if (pidt == null)
            {
                MessageBox.Show("Nincs 'saját' jelü partner!");
                this.Visible = false;
            }
            if (this.Visible)
            {
                ArrayList fszidk = new ArrayList();
                for (int i = 0; i < pidt.Length; i++)
                {
                    string[] egyst = partnertetelinfo.FindIdentityArray(new string[] { "PID" }, new string[] { pidt[i] });
                    for (int j = 0; j < egyst.Length; j++)
                        fszidk.Add(egyst[j]);
                }
                fszids = new string[fszidk.Count];
                for (int i = 0; i < fszids.Length; i++)
                    fszids[i] = fszidk[i].ToString();

                InitializeComponent();

                Fak = mozgastag.Fak;
                buttonMentes.Enabled = false;
                dataGVMozg.AutoGenerateColumns = false;
                DataGridViewColumn[] mozggridcols = Mozgasinfo.GetGridColumns();
                dataGVMozg.Columns.AddRange(mozggridcols);
                Mozgasidcol = Mozgasinfo.Identitycol;
                MozgasAdattabla = Mozgasinfo.Adattabla;
                dataViewMozgas.Table = MozgasAdattabla;
                dataViewMozgas.Sort = Mozgasinfo.Sort;
                dateTimePicker4.Value = Convert.ToDateTime(dateTimePicker4.Value.Year.ToString() + "." + dateTimePicker4.Value.Month.ToString() + ".01");
                DateTime dat = Convert.ToDateTime(dateTimePicker5.Value.Year.ToString() + "." + dateTimePicker5.Value.Month.ToString() + ".01");
                dateTimePicker5.Value = Convert.ToDateTime(dat.AddMonths(1).AddDays(-1));
                dateTimePicker1.MinDate = dateTimePicker4.Value;
                dateTimePicker1.MaxDate = dateTimePicker5.Value;
                dateTimePicker1.Value = dateTimePicker4.Value;
                dateTimePicker2.MinDate = dateTimePicker4.Value;
                dateTimePicker2.Value = dateTimePicker4.Value;
                dateTimePicker3.MinDate = dateTimePicker4.Value;
                dateTimePicker3.Value = dateTimePicker4.Value;
                mozgasadatok  = Fak.Adatoktolt("C", Fak.Aktintervallum, mozgasadatok, 
                    " WHERE DATUM between cast('" + dateTimePicker1.MinDate.ToShortDateString() + "' as datetime) AND cast('" + dateTimePicker1.MaxDate.ToShortDateString() + "' as datetime)", false);
                if (dataViewMozgas.Count == 0)
                {
                    Mozgasaktgridrowind = -1;
                    click = 2;
                }
                else
                {
                    Mozgasaktgridrowind = 0;
                    click = 1;
                }
                dataGVMozg.DataSource = dataViewMozgas;
                Fak.ControlTagokTolt(this,new Control[] { groupBox2, groupBox3, groupBox4 });
                egyallapot = Mozgasinfo.GetEgyallapotinfo(this);
                Mozgasinfo.Comboinfoszures(comboBox2, fszids);
                Mozgasinfo.Comboinfoszures(comboBox4, fszids);
                Mozgas_beallit(Mozgasaktgridrowind);
                igazivaltozas = true;
            }
        }

        private void Elem_Validated(object sender, EventArgs e)
        {
            if (this.Visible&&this.Focused)
            {
                Control cont = (Control)sender;
                if (cont.Tag != null && igazivaltozas)
                {
                    Taggyart tg = (Taggyart)((Control)sender).Tag;
                    string hibaszov = Fak.Hibavizsg(this,tg.Tabinfo, (Control)sender);
                }
            }
        }

        private void buttonVissza_Click(object sender, EventArgs e)
        {
            if (!buttonMentes.Enabled ||
                MessageBox.Show("A valtozasok elvesszenek?", "", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                igazivaltozas = false;
                Fak.ForceAdattolt(Mozgasinfo);
                egyallapot.Modositott = false;
                this.Visible = false;
            }
        }

        private void Elolrol(object sender, EventArgs e)
        {
            igazivaltozas = false;
            MozgasAdattabla = Fak.ForceAdattolt(Mozgasinfo);
            if (dataViewMozgas.Count == 0)
            {
                Mozgasaktgridrowind = -1;
                click = 2;
            }
            else
            {
                Mozgasaktgridrowind = 0;
                click = 1;
            }
            Mozgas_beallit(Mozgasaktgridrowind);
            egyallapot.Modositott = false;
            igazivaltozas = true;
        }

        private void buttonUj_Click(object sender, EventArgs e)
        {
            click = 2;
            Mozgas_beallit(-1);

        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            string hiba = Fak.Hibavizsg(this,Mozgasinfo, null);
            if (hiba == "")
            {
                string hibaszov="";
                if(comboBox2.Text != "" && comboBox4.Text != "" && comboBox2.Text == comboBox4.Text)
                    hibaszov="Honnan Bank = Hová Bank!\n";
                if (comboBox6.Text != "" && comboBox7.Text != "" && comboBox6.Text == comboBox7.Text)
                    hibaszov += "Honnan Pénztár = Hova Pénztár!";
                if (hibaszov == "")
                {
                    int clickint;
                    buttonUj.Enabled = false;
                    buttonTöröl.Enabled = true;
                    if (Mozgasaktgridrowind == -1)
                    {
                        MozgasAdattabla = Mozgasinfo.Ujsor();
                        Mozgasaktsorindex = MozgasAdattabla.Rows.Count - 1;
                        clickint = 2;
                    }
                    else
                    {
                        clickint = 1;
                    }
                    DataRow dr = MozgasAdattabla.Rows[Mozgasaktsorindex];
                    dr = Mozgasinfo.Adatsortolt(Mozgasaktsorindex);
                    click = clickint;
                    Mozgas_beallit(Mozgasaktgridrowind);
                    buttonMentes.Enabled = true;
                    egyallapot.Modositott = true;
                }
                Fak.ErrorProvider.SetError(buttonOK, hibaszov);
            }

        }

        private void dataGVMozg_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            click = e.Clicks;
            if(click==1)
            {
                Mozgas_beallit(e.RowIndex);
            }
        }

        private void dataGVMozg_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            buttonUj.Enabled = false;
            buttonTöröl.Enabled = false;
            click = 2;
            Mozgas_beallit(e.RowIndex);

        }

        private void buttonMentes_Click(object sender, EventArgs e)
        {
            ArrayList list = new ArrayList();
            list.Add(Mozgasinfo);
            igazivaltozas = false;
            if (!Fak.UpdateTransaction(list))
            {
                this.Enabled = true;
                this.Visible = false;
            }
            if (this.Visible)
            {
                if (dataViewMozgas.Count == 0)
                {
                    Mozgasaktgridrowind = -1;
                    click = 2;
                }
                else
                {
                    Mozgasaktgridrowind = dataViewMozgas.Count - 1;
                    click = 1;
                }
                Mozgas_beallit(Mozgasaktgridrowind);
            }
            buttonMentes.Enabled = false;
            egyallapot.Modositott = false;
            egyallapot.Mentett = true;
        }

        private void Mozgas_torol(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGVMozg.Rows.Count; i++)
            {
                if (dataGVMozg.Rows[i].Selected)
                {
                    Mozgasaktgridrowind = i;
                    dataGVMozg.Rows[i].Selected = false;
                    Mozgasaktsorindex = MozgasAdattabla.Rows.IndexOf(dataViewMozgas[Mozgasaktgridrowind].Row);
                    break;
                }
            }
            MozgasAdattabla = Mozgasinfo.Adatsortorol(Mozgasaktsorindex);
            if (dataViewMozgas.Count == 0)
            {
                Mozgasaktgridrowind = -1;
                Mozgasaktsorindex = -1;
                click = 2;
            }
            else
            {
                click = 1;
                if (Mozgasaktgridrowind > dataViewMozgas.Count - 1)
                    Mozgasaktgridrowind--;
                dataGVMozg.Rows[Mozgasaktgridrowind].Selected = true;
                Mozgasaktsorindex = MozgasAdattabla.Rows.IndexOf(dataViewMozgas[Mozgasaktgridrowind].Row);
            }
            Mozgas_beallit(Mozgasaktgridrowind);
            buttonMentes.Enabled = true;
            egyallapot.Modositott = true;
        }

        private void Mozgas_beallit(int gridind)
        {
            igazivaltozas = false;
            dateTimePicker1.MinDate = tol;
            dateTimePicker1.MaxDate = ig;
            dateTimePicker1.MinDate = dateTimePicker4.Value;
            dateTimePicker1.MaxDate = dateTimePicker5.Value;
            dateTimePicker1.Value = dateTimePicker4.Value;

            for (int i = 0; i < dataViewMozgas.Count; i++)
                dataGVMozg.Rows[i].Selected = false;
            Mozgasaktgridrowind = gridind;
            if (click == 1&&groupBox2.Enabled )
            {
                groupBox2.Enabled = false;
                groupBox3.Enabled = false;
                groupBox4.Enabled = false;
            }
            if (click == 2 && !groupBox2.Enabled)
            {
                groupBox2.Enabled = true;
                groupBox3.Enabled = true;
                groupBox4.Enabled = true;
            }
            if (Mozgasaktgridrowind != -1)
            {
                Mozgasaktsorindex = MozgasAdattabla.Rows.IndexOf(dataViewMozgas[Mozgasaktgridrowind].Row);
                Mozgasinfo.Aktsorindex = Mozgasaktsorindex;
                dataGVMozg.Rows[Mozgasaktgridrowind].Selected = true;
                buttonUj.Enabled = true;
                buttonTöröl.Enabled = true;
                if (tobbhonap)
                {
                    groupBox2.Enabled = false;
                    groupBox3.Enabled = false;
                    groupBox4.Enabled = false;
                }
            }
            else
            {
                Mozgasaktid = -1;
                Mozgasaktsorindex = -1;
                Mozgasinfo.Aktsorindex = Mozgasaktsorindex;
                buttonUj.Enabled = false;
                buttonTöröl.Enabled = false;
                dateTimePicker2.Enabled = false;
                dateTimePicker3.Enabled = false;
                comboBox1.Enabled = false;
                comboBox3.Enabled = false;
                checkBox1.Enabled = false;
                checkBox2.Enabled = false;
            }
            dateTimePicker2.MinDate = tol;
            if (dateTimePicker2.Value.CompareTo(dateTimePicker1.Value) < 0 || Mozgasaktid == -1)
                dateTimePicker2.Value = dateTimePicker1.Value;
            dateTimePicker2.MinDate = dateTimePicker1.Value;
            dateTimePicker3.MinDate = tol;
            if (dateTimePicker3.Value.CompareTo(dateTimePicker1.Value) < 0 || Mozgasaktid == -1)
                dateTimePicker3.Value = dateTimePicker1.Value;
            dateTimePicker3.MinDate = dateTimePicker1.Value;
            RadiobuttonBeallitasok();
            igazivaltozas = true;
        }

        private void ComboBeallitasok()
        {
            if(click==2)
            {
                if (comboBox1.Enabled)
                    Fak.SetCombodef(comboBox1);
                else
                    Fak.ClearCombodef(comboBox1);
                if (comboBox2.Enabled)
                    Fak.SetCombodef(comboBox2);
                else
                    Fak.ClearCombodef(comboBox2);
                if (comboBox3.Enabled)
                    Fak.SetCombodef(comboBox3);
                else
                    Fak.ClearCombodef(comboBox3);
                if (comboBox4.Enabled)
                    Fak.SetCombodef(comboBox4);
                else
                    Fak.ClearCombodef(comboBox4);
                if (comboBox6.Enabled)
                    Fak.SetCombodef(comboBox6);
                else
                    Fak.ClearCombodef(comboBox6);
                if (comboBox7.Enabled)
                    Fak.SetCombodef(comboBox7);
                else
                    Fak.ClearCombodef(comboBox7);
            }
        }
        private void RadiobuttonBeallitasok()
        {
            bool elozoigazi = igazivaltozas;
            igazivaltozas = false;
            if (rBHovaKulso.Checked)
            {
                rBHovaKulso.Enabled = true;
                rBHonnanKulso.Checked = false;
                rBHonnanKulso.Enabled = false;
                dateTimePicker2.Enabled = false;
                comboBox1.Enabled = true; ;
                checkBox1.Enabled = false;
                checkBox1.Checked = false;
                comboBox3.Enabled = false; ;
                dateTimePicker3.Enabled = true;
                checkBox2.Enabled = true;
                comboBox2.Enabled = false;
                comboBox6.Enabled = false;
                comboBox7.Enabled = false;
                comboBox4.Enabled = false;
            }
            else if (rBHonnanKulso.Checked)
            {
                rBHonnanKulso.Enabled = true;
                rBHovaKulso.Checked = false;
                rBHovaKulso.Enabled = false;
                comboBox3.Enabled = true; ;
                dateTimePicker3.Enabled = false;
                checkBox2.Enabled = false;
                checkBox2.Checked = false;
                dateTimePicker2.Enabled = true;
                comboBox1.Enabled = false;
                checkBox1.Enabled = true;
                comboBox2.Enabled = false;
                comboBox6.Enabled = false;
                comboBox7.Enabled = false;
                comboBox4.Enabled = false;
            }
            else
            {
                rBHonnanKulso.Enabled = true;
                rBHovaKulso.Enabled = true;
                dateTimePicker2.Enabled = false;
                comboBox1.Enabled = true;
                checkBox1.Enabled = false;
                checkBox1.Checked = false;
                comboBox3.Enabled = false; 
                dateTimePicker3.Enabled = false; 
                checkBox2.Enabled = false;
                checkBox2.Checked = false;
            }

            if (rBHonnanBank.Checked)
            {
                comboBox6.Enabled = false;
                comboBox2.Enabled = true;
            }
            if (rBHonnanPenztar.Checked)
            {
                comboBox6.Enabled = true;
                comboBox2.Enabled = false;
            }
            if (rBHovaBank.Checked)
            {
                comboBox7.Enabled = false;
                comboBox4.Enabled = true;
            }
            if (rBHovaPenztar.Checked)
            {
                comboBox7.Enabled = true;
                comboBox4.Enabled = false;
            }

            if (click == 2)
                ComboBeallitasok();
            igazivaltozas = elozoigazi;
        }
  
        private void comboBox_EnabledChanged(object sender, EventArgs e)
        {
            if(igazivaltozas)
                ComboBeallitasok();
        }

        private void rb_CheckChanged(object sender, EventArgs e)
        {
            if (igazivaltozas && groupBox2.Enabled)
            {
                click = 2;
                RadiobuttonBeallitasok();
            }
        }
        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            bool elozoigazi = igazivaltozas;
            if (igazivaltozas)
            {
                DateTime dat;
                igazivaltozas = false;
                DateTimePicker kuldo = (DateTimePicker)sender;
                Fak.ErrorProvider.SetError(kuldo, "");
                if (kuldo == dateTimePicker4)
                {
                    dateTimePicker4.Value = Convert.ToDateTime(dateTimePicker4.Value.Year.ToString() + "." + dateTimePicker4.Value.Month.ToString() + ".01");
                    dateTimePicker5.Value = Convert.ToDateTime(dateTimePicker4.Value.AddMonths(1).AddDays(-1));
                }
                else
                {
                    dat = Convert.ToDateTime(dateTimePicker5.Value.Year.ToString() + "." + dateTimePicker5.Value.Month.ToString() + ".01");
                    dateTimePicker5.Value = Convert.ToDateTime(dat.AddMonths(1).AddDays(-1));
                    if (dateTimePicker4.Value.CompareTo(dateTimePicker5.Value) > 0)
                        dateTimePicker4.Value = Convert.ToDateTime(dateTimePicker5.Value.Year.ToString() + "." + dateTimePicker5.Value.Month.ToString() + ".01");
                }
                dat = Convert.ToDateTime(dateTimePicker5.Value.Year.ToString() + "." + dateTimePicker5.Value.Month.ToString() + ".01");
                dateTimePicker5.Value = Convert.ToDateTime(dat.AddMonths(1).AddDays(-1));
                mozgasadatok  = Fak.Adatoktolt("C", Fak.Aktintervallum, mozgasadatok,
                    " WHERE DATUM between cast('" + dateTimePicker4.Value.ToShortDateString() + "' as datetime) AND cast('" + dateTimePicker5.Value.ToShortDateString() + "' as datetime)", false);
                if (dateTimePicker4.Value.Year != dateTimePicker5.Value.Year || dateTimePicker4.Value.Year == dateTimePicker5.Value.Year &&
                    dateTimePicker4.Value.Month != dateTimePicker5.Value.Month)
                    tobbhonap = true;
                else
                    tobbhonap = false;
                if (dataViewMozgas.Count == 0)
                    Mozgasaktgridrowind = -1;
                else
                    Mozgasaktgridrowind = 0;
                click = 2;
                Mozgas_beallit(Mozgasaktgridrowind);
            }
            igazivaltozas = elozoigazi;
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            bool elozoigazi = igazivaltozas;
            DateTimePicker kuldo = (DateTimePicker)sender;
            if (igazivaltozas)
            {
                igazivaltozas = false;
                dateTimePicker2.MinDate = tol;
                if (dateTimePicker2.Value.CompareTo(dateTimePicker1.Value) < 0)
                    dateTimePicker2.Value = dateTimePicker1.Value;
                dateTimePicker2.MinDate = dateTimePicker1.Value;
                dateTimePicker3.MinDate = tol;
                if (dateTimePicker3.Value.CompareTo(dateTimePicker1.Value) < 0)
                    dateTimePicker3.Value = dateTimePicker1.Value;
                dateTimePicker3.MinDate = dateTimePicker1.Value;
            }
            igazivaltozas = elozoigazi;
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "0")
                textBox1.Text = "";
        }


    }
}
