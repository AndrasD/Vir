using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;
using System.Data.SqlClient;
using TableInfo;

namespace Adatbevitel
{
    public partial class Szamla : UserControl
    {
        private Tablainfo Szamlainfo;
        private Tablainfo Szamlatetelinfo;
        private DataTable SzamlaAdattabla;
        private DataTable SzamlatetelAdattabla;
        private int Szamlaaktsorindex=-1;
        private int Szamlaaktgridrowind = -1;
        private int Szamlatetelaktgridrowind = -1;
        private int Szamlaaktid = -1;
        private int Szamlatetelaktsorindex = -1;
        private int Szamlatetelaktid = -1;
        private int Szamlaidcol = -1;
        private int Szamlatetelidcol = -1;
        private int Lastszamlaaktgridindex = -1;
        private int Lasttetelaktgridindex = -1;
        private Fak       Fak;
        private MyTag szamlatag;
        private MyTag szamlateteltag;
        private Tablainfo folyoszlainfo;
        private Tablainfo partnerinfo;
        private Tablainfo[] szamlaadatok;
        private Tablainfo[] teteladatok;
        private Tablainfo[] partneradatok;
        private Tablainfo[] folyoszlainfoadatok;
   //     private Tablainfo partnertetelinfo;
        private string jel;
        private bool igazivaltozas = false;
//        private MyTag konttag;
        private MyTag kodtag;
        private Egycontrolinfo szamlaegycont;
        private Egycontrolinfo tetelegycont;
        private bool tobbhonap = false;
        private DateTime tol = Convert.ToDateTime("1801.01.01");
        private DateTime ig = Convert.ToDateTime("9900.12.31");
        private DataGridViewColumn[] szlatetelgridcols;// = Szamlatetelinfo.GetGridColumns();
        private Egyallapotinfo Szamlaegyallapot;
        private Egyallapotinfo Tetelegyallapot;

        private PartnerMenetKozben PartnerMenetKozben;
        private string SzamlaOriginSort;
        private bool modositas = true;

        private SqlConnection myconn = new SqlConnection();
        private DataSet ds = new DataSet();
        private SqlDataAdapter da;
        private DataTable afaKulcs = new DataTable();
        private DataTable sema = new DataTable();
        private DataTable felosztas = new DataTable();
        private DataTable KtgfelosztasAdattabla = new DataTable();

        private SemaMegtekint SemaMegtekint;
        private string szint;
        private MyTag penztar;

        public Szamla(string szoveg, object[] obj)
        {
            this.szamlatag = ((MyTag[])obj[1])[0];
            this.Fak = this.szamlatag.Fak;
            this.partnerinfo = this.Fak.GetTablaInfo("C", "PARTNER");
            this.folyoszlainfo = this.Fak.GetTablaInfo("C", "PARTNER_FOLYOSZ");
            this.partneradatok = new Tablainfo[] { this.partnerinfo };
            this.partneradatok = this.Fak.Adatoktolt("C", this.Fak.Aktintervallum, this.partneradatok, "", false);
            this.folyoszlainfoadatok = new Tablainfo[] { this.folyoszlainfo };
            this.folyoszlainfoadatok = this.Fak.Adatoktolt("C", this.Fak.Aktintervallum, this.folyoszlainfoadatok, "", false);
            this.Szamlainfo = this.szamlatag.AdatTablainfo;
            this.szamlateteltag = ((MyTag[])obj[1])[1];
            this.Szamlatetelinfo = this.szamlateteltag.AdatTablainfo;
            this.szamlaadatok = new Tablainfo[] { this.Szamlainfo };
            this.SzamlaAdattabla = this.Szamlainfo.Adattabla;
            this.teteladatok = new Tablainfo[] { this.Szamlatetelinfo };
            this.SzamlatetelAdattabla = this.Szamlatetelinfo.Adattabla;
            this.jel = obj[2].ToString();
            this.szint = obj[3].ToString();
            this.penztar = this.Fak.GetKodtab("C", "PENZT");
            this.InitializeComponent();
            this.comboBox6.SelectedIndexChanged -= new EventHandler(this.comboBox6_SelectedIndexChanged);
            if (this.jel == "V")
            {
                this.comboBox2.Visible = false;
                this.label12.Visible = false;
                this.label18.Visible = false;
                this.comboSEMA.Visible = false;
                this.button4.Visible = false;
                ((Cols)this.Szamlainfo.TablaColumns[this.Szamlainfo.GetTablaColIndex("FSZ_ID")]).ReadOnly = true;
                ((Cols)this.Szamlainfo.KiegColumns[this.Szamlainfo.GetKiegcolIndex("FSZ_ID_K")]).Lathato = false;
                this.kodtag = this.Fak.GetKodtab("C", "Term");
            }
            else
            {
                ((Cols)this.Szamlainfo.TablaColumns[this.Szamlainfo.GetTablaColIndex("FSZ_ID")]).ReadOnly = false;
                ((Cols)this.Szamlainfo.KiegColumns[this.Szamlainfo.GetKiegcolIndex("FSZ_ID_K")]).Lathato = true;
                this.kodtag = this.Fak.GetKodtab("C", "Kolt");
            }
            if (!this.Szamlatetelinfo.AktCombotolt("MEGNID", this.kodtag))
            {
                MessageBox.Show("Nincs termék- vagy költségg kód!");
                base.Visible = false;
            }
            string[] kellfileinfo = this.partnerinfo.FindIdentityArray(new string[] { "SAJAT" }, new string[] { "N" });
            string[] tartalom = this.partnerinfo.FindIdentityArray(new string[] { "SAJAT" }, new string[] { "I" });
            if (kellfileinfo == null)
            {
                MessageBox.Show("Csak 'saját' jelü partner van!");
                base.Visible = false;
            }
            else if (tartalom == null)
            {
                MessageBox.Show("Nincs 'sajat' jelü partner!");
                base.Visible = false;
            }
            if (base.Visible)
            {
                this.Fak.ControlTagokTolt(this, this.groupBox2);
                this.Szamlaegyallapot = this.Szamlainfo.GetEgyallapotinfo(this);
                this.dateTimePicker3.Value = Convert.ToDateTime(this.dateTimePicker3.Value.Year.ToString() + "." + this.dateTimePicker3.Value.Month.ToString() + ".01");
                DateTime time = Convert.ToDateTime(this.dateTimePicker4.Value.Year.ToString() + "." + this.dateTimePicker4.Value.Month.ToString() + ".01");
                this.dateTimePicker4.Value = Convert.ToDateTime(time.AddMonths(1).AddDays(-1.0));
                this.dateSzamla.MinDate = this.dateTimePicker3.Value;
                this.dateSzamla.MaxDate = this.dateTimePicker4.Value;
                this.dateSzamla.Value = this.dateTimePicker3.Value;
                this.dateFizetes.Value = this.dateTimePicker3.Value;
                this.dateTeljesit.Value = this.dateTimePicker3.Value;
                this.Szamlainfo.Comboinfoszures(this.comboSzallito, kellfileinfo);
                string[] strArray3 = this.folyoszlainfo.FindIdentityArray(new string[] { "PID" }, tartalom);
                this.Szamlainfo.Comboinfoszures(this.comboBox4, strArray3);
                this.szamlaegycont = this.Szamlainfo.GetEgycontrolinfo(this);
                if (this.comboSzallito.Items.Count == 0)
                {
                    MessageBox.Show("Előbb vezesse fel a partnereit!");
                    base.Visible = false;
                }
                if (base.Visible)
                {
                    this.Fak.ControlTagokTolt(this, this.groupBox4);
                    this.tetelegycont = this.Szamlatetelinfo.GetEgycontrolinfo(this);
                    this.Tetelegyallapot = this.Szamlatetelinfo.GetEgyallapotinfo(this);
                    this.dataGVSzla.AutoGenerateColumns = false;
                    DataGridViewColumn[] gridColumns = this.Szamlainfo.GetGridColumns();
                    this.dataGVSzla.Columns.AddRange(gridColumns);
                    this.dataGVSzlatetel.AutoGenerateColumns = false;
                    this.szlatetelgridcols = this.Szamlatetelinfo.GetGridColumns();
                    this.dataGVSzlatetel.Columns.AddRange(this.szlatetelgridcols);
                    this.Szamlaidcol = this.Szamlainfo.Identitycol;
                    if (this.szint == "1")
                    {
                        this.szamlaadatok = this.Fak.Adatoktolt("C", this.Fak.Aktintervallum, this.szamlaadatok, " where VS='" + this.jel + "' AND DATUM between cast('" + this.dateSzamla.MinDate.ToShortDateString() + "' as datetime) AND cast('" + this.dateSzamla.MaxDate.ToShortDateString() + "' as datetime)", false);
                    }
                    else
                    {
                        this.szamlaadatok = this.Fak.Adatoktolt("C", this.Fak.Aktintervallum, this.szamlaadatok, " where VS='" + this.jel + "' AND DATUM between cast('" + this.dateSzamla.MinDate.ToShortDateString() + "' as datetime) AND cast('" + this.dateSzamla.MaxDate.ToShortDateString() + "' as datetime) AND H_PENZTAR_ID <> 407", false);
                    }
                    this.SzamlaAdattabla = this.Szamlainfo.Adattabla;
                    this.Szamlatetelidcol = this.Szamlatetelinfo.Identitycol;
                    this.dataViewSzamla.Table = this.SzamlaAdattabla;
                    this.dataViewSzamla.Sort = this.Szamlainfo.Sort;
                    this.SzamlaOriginSort = this.Szamlainfo.Sort;
                    if (this.dataViewSzamla.Count == 0)
                    {
                        this.Szamlaaktgridrowind = -1;
                    }
                    else
                    {
                        this.Szamlaaktgridrowind = 0;
                    }
                    this.dataGVSzla.DataSource = this.dataViewSzamla;
                    this.dataViewTetel.Table = this.SzamlatetelAdattabla;
                    this.dataViewTetel.Sort = this.Szamlatetelinfo.Sort;
                    this.dataGVSzlatetel.DataSource = this.dataViewTetel;
                    this.groupBox2.Enabled = false;
                    this.Szamla_beallit(this.Szamlaaktgridrowind);
                    this.da = new SqlDataAdapter("select sorszam, szoveg, kod from kodtab where kodtipus = 'AFA' order by kod ", this.szamlatag.AdatTablainfo.Adatconn);
                    this.da.Fill(this.ds, "afaKulcs");
                    this.afaKulcs = this.ds.Tables["afaKulcs"];
                    this.comboAFA.DataSource = this.afaKulcs;
                    if (this.comboAFA.Items.Count > 0)
                    {
                        this.comboAFA.SelectedIndex = 0;
                    }
                    this.comboAFA.DisplayMember = "szoveg";
                    this.comboAFA.ValueMember = "kod";
                    this.comboAFA.SelectedIndex = this.comboAFA.FindString("25%");
                    this.da = new SqlDataAdapter("select distinct 0, '' as nev from ktgfelosztas ", this.szamlatag.AdatTablainfo.Adatconn);
                    this.da.Fill(this.ds, "sema");
                    this.da = new SqlDataAdapter("select distinct koltseg_id, k.kod+' '+k.szoveg as nev from ktgfelosztas a, kodtab k where a.koltseg_id = k.sorszam ", this.szamlatag.AdatTablainfo.Adatconn);
                    this.da.Fill(this.ds, "sema");
                    this.sema = this.ds.Tables["sema"];
                    this.comboSEMA.DataSource = this.sema;
                    if (this.comboSEMA.Items.Count > 0)
                    {
                        this.comboSEMA.SelectedIndex = 0;
                    }
                    this.comboSEMA.DisplayMember = "nev";
                    this.comboSEMA.ValueMember = "koltseg_id";
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.PartnerMenetKozben == null)
            {
                this.PartnerMenetKozben = new Adatbevitel.PartnerMenetKozben(this.Szamlainfo, this.partnerinfo, this.comboSzallito, this.Fak);
            }
            else
            {
                this.PartnerMenetKozben.PartnerMenetKozbenInit();
            }
            if (this.PartnerMenetKozben.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if ((this.comboSEMA.SelectedValue != null) && (this.comboSEMA.SelectedValue.ToString() != ""))
            {
                this.SemaMegtekint = new Adatbevitel.SemaMegtekint(this.Fak, this.comboSEMA.SelectedValue.ToString(), this.szamlatag.AdatTablainfo.Adatconn);
                if (this.SemaMegtekint.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                }
            }
        }

        private void comboAFA_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.comboBox1.SelectedIndex = this.comboAFA.SelectedIndex;
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.button1.Enabled = true;
            if (this.rBPenztar.Checked)
            {
                DataRow[] rowArray = this.penztar.AdatTablainfo.Adattabla.Select("szoveg = '" + this.comboBox6.Text + "'");
                if (((rowArray.Length > 0) && (rowArray[0]["megj"].ToString() != "")) && (rowArray[0]["megj"].ToString() != this.szint))
                {
                    MessageBox.Show("Nincs joga erre a pénztárra könyvelni!");
                    this.button1.Enabled = false;
                }
            }
        }

        private void comboSEMA_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void comboSzallitoTextChanged(object sender, EventArgs e)
        {
            if (((this.comboSzallito.Text != "") && this.igazivaltozas) && this.comboBox2.Visible)
            {
                this.igazivaltozas = false;
                string actualCombofileinfo = this.Szamlainfo.GetActualCombofileinfo(this.comboSzallito);
                if (actualCombofileinfo != "")
                {
                    this.partnerinfo.Aktidentity = Convert.ToInt32(actualCombofileinfo);
                    string[] kellfileinfo = this.folyoszlainfo.FindIdentityArray(new string[] { "PID" }, new string[] { actualCombofileinfo });
                    if (kellfileinfo != null)
                    {
                        this.Szamlainfo.Comboinfoszures(this.comboBox2, kellfileinfo);
                    }
                    this.igazivaltozas = true;
                }
            }
        }

        private void controlOsszeg_Leave(object sender, EventArgs e)
        {
            if ((this.controlOsszeg.Text != "") && (Convert.ToDecimal(this.controlOsszeg.Text) > 0M))
            {
                this.controlOsszeg.Text = $"{Convert.ToInt32(this.controlOsszeg.Text):N}";
            }
        }

        private void dataGVSzla_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Clicks == 1)
            {
                this.groupBox2.Enabled = false;
                if (e.RowIndex != -1)
                {
                    this.Szamla_beallit(e.RowIndex);
                }
            }
        }

        private void dataGVSzla_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                this.modositas = true;
                this.Szamla_beallit(e.RowIndex);
                if (!this.tobbhonap)
                {
                    this.groupBox2.Enabled = true;
                }
            }
        }

        private void dataGVSzlatetel_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                this.Szamlatetelaktgridrowind = e.RowIndex;
                this.Tetel_beallit(e.RowIndex);
            }
        }

        private void dateSzamla_Changed(object sender, EventArgs e)
        {
            igazivaltozas = igazivaltozas;
        }

        private void dateTimePicker_Changed(object sender, EventArgs e)
        {
            this.comboBox6.SelectedIndexChanged -= new EventHandler(this.comboBox6_SelectedIndexChanged);
            bool igazivaltozas = this.igazivaltozas;
            if (this.igazivaltozas)
            {
                DateTime time;
                this.igazivaltozas = false;
                DateTimePicker control = (DateTimePicker)sender;
                this.Fak.ErrorProvider.SetError(control, "");
                if (control == this.dateTimePicker3)
                {
                    this.dateTimePicker3.Value = Convert.ToDateTime(this.dateTimePicker3.Value.Year.ToString() + "." + this.dateTimePicker3.Value.Month.ToString() + ".01");
                    this.dateTimePicker4.Value = Convert.ToDateTime(this.dateTimePicker3.Value.AddMonths(1).AddDays(-1.0));
                }
                else
                {
                    time = Convert.ToDateTime(this.dateTimePicker4.Value.Year.ToString() + "." + this.dateTimePicker4.Value.Month.ToString() + ".01");
                    this.dateTimePicker4.Value = Convert.ToDateTime(time.AddMonths(1).AddDays(-1.0));
                    if (this.dateTimePicker3.Value.CompareTo(this.dateTimePicker4.Value) > 0)
                    {
                        this.dateTimePicker3.Value = Convert.ToDateTime(this.dateTimePicker4.Value.Year.ToString() + "." + this.dateTimePicker4.Value.Month.ToString() + ".01");
                    }
                }
                time = Convert.ToDateTime(this.dateTimePicker4.Value.Year.ToString() + "." + this.dateTimePicker4.Value.Month.ToString() + ".01");
                this.dateTimePicker4.Value = Convert.ToDateTime(time.AddMonths(1).AddDays(-1.0));
                if (this.szint == "1")
                {
                    this.szamlaadatok = this.Fak.Adatoktolt("C", this.Fak.Aktintervallum, this.szamlaadatok, " where VS='" + this.jel + "' AND DATUM between cast('" + this.dateTimePicker3.Value.ToShortDateString() + "' as datetime) AND cast('" + this.dateTimePicker4.Value.ToShortDateString() + "' as datetime)", false);
                }
                else
                {
                    this.szamlaadatok = this.Fak.Adatoktolt("C", this.Fak.Aktintervallum, this.szamlaadatok, " where VS='" + this.jel + "' AND DATUM between cast('" + this.dateTimePicker3.Value.ToShortDateString() + "' as datetime) AND cast('" + this.dateTimePicker4.Value.ToShortDateString() + "' as datetime) AND H_PENZTAR_ID <> 407", false);
                }
                if ((this.dateTimePicker3.Value.Year != this.dateTimePicker4.Value.Year) || ((this.dateTimePicker3.Value.Year == this.dateTimePicker4.Value.Year) && (this.dateTimePicker3.Value.Month != this.dateTimePicker4.Value.Month)))
                {
                    this.tobbhonap = true;
                }
                else
                {
                    this.tobbhonap = false;
                }
                if (this.dataViewSzamla.Count == 0)
                {
                    this.Szamlaaktgridrowind = -1;
                }
                else
                {
                    this.Szamlaaktgridrowind = 0;
                }
                this.Szamla_beallit(this.Szamlaaktgridrowind);
            }
            this.igazivaltozas = igazivaltozas;
        }

        private void DatumSzerint_Click(object sender, EventArgs e)
        {
            this.dataViewSzamla.Sort = "DATUM";
            this.Szamla_beallit(this.Szamlaaktgridrowind);
        }

        private void Elem_Validated(object sender, EventArgs e)
        {
            if (this.igazivaltozas)
            {
                Taggyart tag = (Taggyart)((Control)sender).Tag;
                if (this.Fak.Hibavizsg(this, tag.Tabinfo, (Control)sender) == "")
                {
                    this.Validalas((Control)sender);
                }
                this.tetelOsszegSzamitas();
            }
        }

        private void Elolrol(object sender, EventArgs e)
        {
            this.igazivaltozas = false;
            this.SzamlaAdattabla = this.Fak.ForceAdattolt(this.Szamlainfo);
            this.SzamlatetelAdattabla = this.Fak.ForceAdattolt(this.Szamlatetelinfo);
            Rectangle columnDisplayRectangle = this.dataGVSzlatetel.GetColumnDisplayRectangle(5, false);
            if (this.dataViewSzamla.Count == 0)
            {
                this.Szamlaaktgridrowind = -1;
            }
            else
            {
                this.Szamlaaktgridrowind = 0;
            }
            this.Szamla_beallit(this.Szamlaaktgridrowind);
            this.igazivaltozas = true;
        }

        private void FelvitelSzerint_Click(object sender, EventArgs e)
        {
            this.dataViewSzamla.Sort = "ID";
            this.Szamla_beallit(this.Szamlaaktgridrowind);
        }

        private void Mentes(object sender, EventArgs e)
        {
            if ((!this.modositas && (Convert.ToDecimal(this.controlOsszeg.Text) > 0M)) && (Convert.ToDecimal(this.textBox8.Text) != Convert.ToDecimal(this.controlOsszeg.Text)))
            {
                MessageBox.Show("Még van maradék vagy túl sok a tételek összege!", "", System.Windows.Forms.MessageBoxButtons.OKCancel);
            }
            else
            {
                this.dataViewSzamla.Sort = this.SzamlaOriginSort;
                ArrayList modositandok = new ArrayList {
                    this.Szamlainfo,
                    this.Szamlatetelinfo
                };
                this.igazivaltozas = false;
                if ((this.dataViewSzamla.Count > 0) && (this.dataViewSzamla[0].Row.RowState == DataRowState.Added))
                {
                    this.Szamlaaktgridrowind = this.dataViewSzamla.Count - 1;
                }
                if (!this.Fak.UpdateTransaction(modositandok))
                {
                    base.Enabled = true;
                    base.Visible = false;
                    this.modositas = false;
                }
                if (base.Visible)
                {
                    if (this.dataViewSzamla.Count == 0)
                    {
                        this.Szamlaaktgridrowind = -1;
                    }
                    this.Szamla_beallit(this.Szamlaaktgridrowind);
                    if (this.Szamlaaktgridrowind != -1)
                    {
                        this.dataGVSzla.FirstDisplayedScrollingRowIndex = this.Szamlaaktgridrowind;
                    }
                    this.buttonMentes.Enabled = false;
                    this.Szamlaegyallapot.Modositott = false;
                    this.Tetelegyallapot.Modositott = false;
                    this.Szamlaegyallapot.Mentett = true;
                    this.Tetelegyallapot.Mentett = true;
                }
            }
        }

        private bool osszeg_felosztasa(object sender, EventArgs e)
        {
            decimal num = 100M;
            decimal num2 = Convert.ToDecimal(this.comboAFA.SelectedValue);
            decimal num3 = Convert.ToDecimal(this.controlOsszeg.Text) / ((num2 + 100M) / 100M);
            decimal num4 = 0M;
            this.KtgfelosztasAdattabla.Clear();
            this.da = new SqlDataAdapter("select a.*, k.kod + ' ' + k.szoveg as nev from ktgfelosztas a, kodtab k where a.termek_id = k.sorszam and a.koltseg_id = " + this.comboSEMA.SelectedValue.ToString(), this.szamlatag.AdatTablainfo.Adatconn);
            this.da.Fill(this.ds, "KtgfelosztasAdattabla");
            this.KtgfelosztasAdattabla = this.ds.Tables["KtgfelosztasAdattabla"];
            if (this.KtgfelosztasAdattabla.Rows.Count > 0)
            {
                for (int i = 0; i < this.KtgfelosztasAdattabla.Rows.Count; i++)
                {
                    if (this.textBox1.Text == "")
                    {
                        this.textBox2.Text = "S\x00e9ma:" + this.comboSEMA.Text;
                    }
                    else
                    {
                        this.textBox2.Text = this.textBox1.Text;
                    }
                    this.comboBox5.SelectedIndex = this.comboBox5.FindString(this.KtgfelosztasAdattabla.Rows[i]["nev"].ToString());
                    this.comboBox1.SelectedIndex = this.comboAFA.SelectedIndex;
                    num = Convert.ToDecimal(this.KtgfelosztasAdattabla.Rows[i]["szazalek"].ToString());
                    num4 = (num3 * num) / 100M;
                    this.textBox3.Text = Convert.ToString(num4);
                    this.textBox5.Text = Convert.ToString((decimal)((num4 * num2) / 100M));
                    this.textBox6.Text = Convert.ToString((decimal)(Convert.ToDecimal(this.textBox3.Text) + Convert.ToDecimal(this.textBox5.Text)));
                    this.Tetel_ok(sender, e);
                }
            }
            return false;
        }

        private void PartnerSzerint_Click(object sender, EventArgs e)
        {
            this.dataViewSzamla.Sort = "PID_K";
            this.Szamla_beallit(this.Szamlaaktgridrowind);
        }

        private void RadiobuttonBeallitasok()
        {
            bool igazivaltozas = this.igazivaltozas;
            this.igazivaltozas = false;
            if (this.rBBank.Checked)
            {
                this.comboBox4.Enabled = true;
                this.Fak.SetCombodef(this.comboBox4);
                this.comboBox6.Enabled = false;
                this.Fak.ClearCombodef(this.comboBox6);
            }
            if (this.rBPenztar.Checked)
            {
                this.comboBox4.Enabled = false;
                this.Fak.ClearCombodef(this.comboBox4);
                this.comboBox6.Enabled = true;
                this.Fak.SetCombodef(this.comboBox6);
            }
            this.igazivaltozas = igazivaltozas;
        }

        private void rb_CheckChanged(object sender, EventArgs e)
        {
            if (this.rBBank.Checked)
            {
                this.comboBox6.SelectedIndexChanged -= new EventHandler(this.comboBox6_SelectedIndexChanged);
            }
            if (!this.rBPenztar.Checked)
            {
                this.button1.Enabled = true;
                this.comboBox6.SelectedIndexChanged -= new EventHandler(this.comboBox6_SelectedIndexChanged);
            }
            if (this.igazivaltozas && (this.rBPenztar == ((RadioButton)sender)))
            {
                if (this.rBPenztar.Checked)
                {
                    this.checkBox1.Checked = true;
                    this.checkBox1.Enabled = false;
                }
                else
                {
                    this.checkBox1.Enabled = true;
                }
            }
            this.RadiobuttonBeallitasok();
            if (this.rBPenztar.Checked)
            {
                this.comboBox6.SelectedIndexChanged += new EventHandler(this.comboBox6_SelectedIndexChanged);
            }
        }

        private void Summaz()
        {
            decimal num = 0M;
            decimal num2 = 0M;
            decimal num3 = 0M;
            for (int i = 0; i < this.dataViewTetel.Count; i++)
            {
                DataRow row = this.dataViewTetel[i].Row;
                num += Convert.ToDecimal(row["AFA"].ToString());
                num2 += Convert.ToDecimal(row["NETTO"].ToString());
                num3 += Convert.ToDecimal(row["BRUTTO"].ToString());
            }
            this.textBox4.Text = num2.ToString();
            this.textBox4.InsertFormatCharacters();
            this.textBox7.Text = num.ToString();
            this.textBox7.InsertFormatCharacters();
            this.textBox8.Text = num3.ToString();
            this.textBox8.InsertFormatCharacters();
        }

        private void Szamla_beallit(int gridind)
        {
            this.igazivaltozas = false;
            this.dateSzamla.MinDate = this.tol;
            this.dateSzamla.MaxDate = this.ig;
            this.dateSzamla.MinDate = this.dateTimePicker3.Value;
            this.dateSzamla.MaxDate = this.dateTimePicker4.Value;
            this.dateSzamla.Value = this.dateTimePicker3.Value;
            this.buttonMentes.Enabled = false;
            this.Szamlaegyallapot.Modositott = false;
            this.Tetelegyallapot.Modositott = false;
            for (int i = 0; i < this.dataViewSzamla.Count; i++)
            {
                this.dataGVSzla.Rows[i].Selected = false;
            }
            this.Szamlaaktgridrowind = gridind;
            this.Lastszamlaaktgridindex = gridind;
            if (this.Szamlaaktgridrowind != -1)
            {
                this.Szamlaaktsorindex = this.SzamlaAdattabla.Rows.IndexOf(this.dataViewSzamla[this.Szamlaaktgridrowind].Row);
                this.Szamlainfo.Aktsorindex = this.Szamlaaktsorindex;
                this.dataGVSzla.Rows[this.Szamlaaktgridrowind].Selected = true;
                this.Szamlaaktid = Convert.ToInt32(this.SzamlaAdattabla.Rows[this.Szamlaaktsorindex][this.Szamlaidcol].ToString());
                this.Szamlainfo.Aktidentity = this.Szamlaaktid;
                this.buttonUj.Enabled = true;
                this.buttonTöröl.Enabled = true;
                this.panelControl.Visible = false;
            }
            else
            {
                this.Szamlaaktid = -1;
                this.Szamlaaktsorindex = -1;
                this.Szamlainfo.Aktsorindex = -1;
                this.buttonUj.Enabled = false;
                this.buttonTöröl.Enabled = false;
                this.panelControl.Visible = true;
                this.controlOsszeg.Text = "";
                if (this.sema.Rows.Count > 0)
                {
                    this.comboSEMA.SelectedIndex = 0;
                }
            }
            this.dateFizetes.MinDate = this.tol;
            if ((this.dateFizetes.Value.CompareTo(this.dateSzamla.Value) < 0) || (this.Szamlaaktid == -1))
            {
                this.dateFizetes.Value = this.dateSzamla.Value;
            }
            this.dateFizetes.MinDate = this.dateSzamla.Value;
            this.dateTeljesit.MinDate = this.tol;
//            if ((this.dateTeljesit.Value.CompareTo(this.dateFizetes.Value) < 0) || (this.Szamlaaktid == -1))
//            {
//                this.dateTeljesit.Value = this.dateFizetes.Value;
//            }
//            this.dateTeljesit.MinDate = this.dateFizetes.Value;
            if (this.comboBox2.Visible)
            {
                string actualCombofileinfo = this.Szamlainfo.GetActualCombofileinfo(this.comboSzallito);
                this.partnerinfo.Aktidentity = Convert.ToInt32(actualCombofileinfo);
                Tablainfo[] tablainfok = new Tablainfo[] { this.folyoszlainfo };
                tablainfok = this.Fak.Adatoktolt("C", this.Fak.Aktintervallum, tablainfok, "", false);
                this.Szamlainfo.Aktsorindex = this.Szamlaaktsorindex;
                string[] kellfileinfo = this.folyoszlainfo.FindIdentityArray(new string[] { "PID" }, new string[] { actualCombofileinfo });
                if (kellfileinfo != null)
                {
                    this.Szamlainfo.Comboinfoszures(this.comboBox2, kellfileinfo);
                }
            }
            if (this.Szamlaaktid == -1)
            {
                this.groupBox2.Enabled = true;
            }
            this.groupBox3.Enabled = false;
            this.groupBox4.Enabled = false;
            this.groupBox1.Enabled = true;
            this.teteladatok = this.Fak.Adatoktolt("T", this.Fak.Aktintervallum, this.teteladatok, "", false);
            if (this.dataViewTetel.Count == 0)
            {
                this.Szamlatetelaktgridrowind = -1;
            }
            else
            {
                this.Szamlatetelaktgridrowind = 0;
            }
            this.Tetel_beallit(this.Szamlatetelaktgridrowind);
            this.RadiobuttonBeallitasok();
            this.igazivaltozas = true;
        }

        private void Szamla_Load(object sender, EventArgs e)
        {
            Rectangle columnDisplayRectangle = new Rectangle();
            Rectangle rectangle2 = new Rectangle();
            Rectangle rectangle3 = new Rectangle();
            for (int i = 0; i < this.szlatetelgridcols.Length; i++)
            {
                DataGridViewColumn column = this.szlatetelgridcols[i];
                string dataPropertyName = column.DataPropertyName;
                if (dataPropertyName != null)
                {
                    if (dataPropertyName != "NETTO")
                    {
                        if (dataPropertyName == "AFA")
                        {
                            goto Label_0071;
                        }
                        if (dataPropertyName == "BRUTTO")
                        {
                            goto Label_0081;
                        }
                    }
                    else
                    {
                        columnDisplayRectangle = this.dataGVSzlatetel.GetColumnDisplayRectangle(i, false);
                    }
                }
                continue;
            Label_0071:
                rectangle2 = this.dataGVSzlatetel.GetColumnDisplayRectangle(i, false);
                continue;
            Label_0081:
                rectangle3 = this.dataGVSzlatetel.GetColumnDisplayRectangle(i, false);
            }
            this.textBox4.Bounds = new Rectangle(columnDisplayRectangle.X + this.dataGVSzlatetel.Location.X, this.textBox4.Location.Y, columnDisplayRectangle.Width, this.textBox4.Height);
            this.textBox7.Bounds = new Rectangle(rectangle2.X + this.dataGVSzlatetel.Location.X, this.textBox7.Location.Y, rectangle2.Width, this.textBox7.Height);
            this.textBox8.Bounds = new Rectangle(rectangle3.X + this.dataGVSzlatetel.Location.X, this.textBox8.Location.Y, rectangle3.Width, this.textBox8.Height);
            this.textBox3.Bounds = new Rectangle(rectangle3.X + this.dataGVSzlatetel.Location.X, this.textBox3.Location.Y, rectangle3.Width, this.textBox3.Height);
            this.textBox5.Bounds = new Rectangle(rectangle3.X + this.dataGVSzlatetel.Location.X, this.textBox5.Location.Y, rectangle3.Width, this.textBox5.Height);
            this.textBox6.Bounds = new Rectangle(rectangle3.X + this.dataGVSzlatetel.Location.X, this.textBox6.Location.Y, rectangle3.Width, this.textBox6.Height);
        }

        private void Szamla_ok(object sender, EventArgs e)
        {
            if (this.rBPenztar.Checked)
            {
                this.checkBox1.Checked = true;
            }
            if ((this.Fak.Hibavizsg(this, this.Szamlainfo, null) == "") && (this.Validalas(this.comboSzallito) == ""))
            {
                this.buttonUj.Enabled = false;
                this.buttonTöröl.Enabled = true;
                this.buttonMentes.Enabled = false;
                this.groupBox1.Enabled = false;
                this.groupBox2.Enabled = false;
                this.groupBox3.Enabled = true;
                this.groupBox4.Enabled = true;
                if (this.Szamlaaktgridrowind == -1)
                {
                    this.SzamlaAdattabla = this.Szamlainfo.Ujsor();
                    this.Szamlaaktsorindex = this.SzamlaAdattabla.Rows.Count - 1;
                    this.Szamlaaktgridrowind = 0;
                    this.Szamlatetelaktgridrowind = -1;
                }
                if (!this.modositas)
                {
                    this.Szamlaaktgridrowind = this.dataViewSzamla.Count + 1;
                }
                DataRow row = this.SzamlaAdattabla.Rows[this.Szamlaaktsorindex];
                this.Szamlainfo.Adatsortolt(this.Szamlaaktsorindex)["VS"] = this.jel;
                this.dataViewSzamla.Table = this.SzamlaAdattabla;
                this.dataViewSzamla.Sort = this.Szamlainfo.Sort;
            }
            if (this.rBPenztar.Checked)
            {
                this.textBox3.ReadOnly = true;
                this.textBox6.ReadOnly = false;
            }
            else
            {
                this.textBox6.ReadOnly = true;
                this.textBox3.ReadOnly = false;
            }
            if ((((this.panelControl.Visible && (this.controlOsszeg.Text.Trim() != "")) && ((Convert.ToDecimal(this.controlOsszeg.Text) > 0M) && (this.comboSEMA.SelectedIndex > 0))) && ((this.comboSEMA.SelectedValue != null) && (this.comboSEMA.SelectedValue.ToString() != ""))) && this.osszeg_felosztasa(sender, e))
            {
            }
        }

        private void Szamla_torol(object sender, EventArgs e)
        {
            int num;
            this.dataViewTetel.RowStateFilter = DataViewRowState.CurrentRows | DataViewRowState.Deleted;
            for (num = 0; num < this.dataViewTetel.Count; num++)
            {
                DataRow row = this.dataViewTetel[num].Row;
                this.SzamlatetelAdattabla = this.Szamlatetelinfo.Adatsortorol(this.SzamlatetelAdattabla.Rows.IndexOf(row));
            }
            this.dataViewTetel.RowStateFilter = DataViewRowState.CurrentRows;
            for (num = 0; num < this.dataGVSzla.Rows.Count; num++)
            {
                if (this.dataGVSzla.Rows[num].Selected)
                {
                    this.Szamlaaktgridrowind = num;
                    this.dataGVSzla.Rows[num].Selected = false;
                    this.Szamlaaktsorindex = this.SzamlaAdattabla.Rows.IndexOf(this.dataViewSzamla[this.Szamlaaktgridrowind].Row);
                    break;
                }
            }
            this.SzamlaAdattabla = this.Szamlainfo.Adatsortorol(this.Szamlaaktsorindex);
            if (this.dataViewSzamla.Count == 0)
            {
                this.Szamla_beallit(-1);
            }
            else
            {
                if (this.Szamlaaktgridrowind > (this.dataViewSzamla.Count - 1))
                {
                    this.Szamlaaktgridrowind--;
                }
                this.dataGVSzla.Rows[this.Szamlaaktgridrowind].Selected = true;
                this.Szamlaaktsorindex = this.SzamlaAdattabla.Rows.IndexOf(this.dataViewSzamla[this.Szamlaaktgridrowind].Row);
                this.Szamla_beallit(this.Szamlaaktgridrowind);
            }
            this.buttonMentes.Enabled = true;
            this.Szamlaegyallapot.Modositott = true;
            this.Tetelegyallapot.Modositott = true;
        }

        private void Tetel_beallit(int gridindex)
        {
            this.igazivaltozas = false;
            this.Szamlatetelaktgridrowind = gridindex;
            if (this.Szamlatetelaktgridrowind != -1)
            {
                this.Szamlatetelaktsorindex = this.SzamlatetelAdattabla.Rows.IndexOf(this.dataViewTetel[this.Szamlatetelaktgridrowind].Row);
                if (this.SzamlatetelAdattabla.Rows[this.Szamlatetelaktsorindex].RowState != DataRowState.Added)
                {
                    this.Szamlatetelaktid = Convert.ToInt32(this.SzamlatetelAdattabla.Rows[this.Szamlatetelaktsorindex][this.Szamlatetelidcol].ToString());
                }
                else
                {
                    this.Szamlatetelaktid = -1;
                }
                this.buttonUjtetel.Enabled = true;
            }
            else
            {
                this.Szamlatetelaktid = -1;
                this.Szamlatetelaktsorindex = -1;
                this.buttonUjtetel.Enabled = false;
            }
            this.Lasttetelaktgridindex = gridindex;
            this.Szamlatetelinfo.Aktsorindex = this.Szamlatetelaktsorindex;
            if (this.dataViewTetel.Count == 0)
            {
                this.buttonTeteltorol.Enabled = false;
            }
            else
            {
                this.buttonTeteltorol.Enabled = true;
            }
            this.Summaz();
            this.igazivaltozas = true;
        }

        private void Tetel_ok(object sender, EventArgs e)
        {
            this.igazivaltozas = false;
            if (this.Fak.Hibavizsg(this, this.Szamlatetelinfo, null) == "")
            {
                this.tetelOsszegSzamitas();
                this.Fak.Hibavizsg(this, this.Szamlatetelinfo, null);
                this.Lasttetelaktgridindex = this.Szamlatetelaktgridrowind;
                if (this.Szamlatetelaktgridrowind == -1)
                {
                    this.SzamlatetelAdattabla = this.Szamlatetelinfo.Ujsor();
                    this.Szamlatetelaktsorindex = this.SzamlatetelAdattabla.Rows.Count - 1;
                }
                DataRow row = this.SzamlatetelAdattabla.Rows[this.Szamlatetelaktsorindex];
                row = this.Szamlatetelinfo.Adatsortolt(this.Szamlatetelaktsorindex);
                this.dataViewTetel.Table = this.SzamlatetelAdattabla;
                this.dataViewTetel.Sort = this.Szamlatetelinfo.Sort;
                this.Summaz();
                if (this.Lasttetelaktgridindex == -1)
                {
                    this.Szamlatetelaktsorindex = -1;
                    this.Szamlatetelinfo.Aktsorindex = -1;
                    this.textBox5.Text = "";
                    this.textBox6.Text = "";
                }
                this.buttonMentes.Enabled = true;
                this.Tetelegyallapot.Modositott = true;
                this.buttonTeteltorol.Enabled = true;
                this.igazivaltozas = true;
            }
        }

        private void Tetel_torol(object sender, EventArgs e)
        {
            if (this.dataViewTetel.Count > 0)
            {
                this.buttonUj.Enabled = false;
                this.buttonTöröl.Enabled = false;
                for (int i = 0; i < this.dataViewTetel.Count; i++)
                {
                    if (this.dataGVSzlatetel.Rows[i].Selected)
                    {
                        this.Szamlatetelaktgridrowind = i;
                        break;
                    }
                }
                this.Szamlatetelaktsorindex = this.SzamlatetelAdattabla.Rows.IndexOf(this.dataViewTetel[this.Szamlatetelaktgridrowind].Row);
                this.SzamlatetelAdattabla = this.Szamlatetelinfo.Adatsortorol(this.Szamlatetelaktsorindex);
                this.dataViewTetel.Table = this.SzamlatetelAdattabla;
                this.Summaz();
                if (this.dataViewTetel.Count == 0)
                {
                    this.Tetel_beallit(-1);
                    this.buttonMentes.Enabled = false;
                }
                else
                {
                    if (this.Szamlatetelaktgridrowind > (this.dataViewTetel.Count - 1))
                    {
                        this.Szamlatetelaktgridrowind--;
                    }
                    if (this.Szamlatetelaktgridrowind != -1)
                    {
                        this.dataGVSzlatetel.Rows[this.Szamlatetelaktgridrowind].Selected = true;
                    }
                    this.Tetel_beallit(this.Szamlatetelaktgridrowind);
                }
                this.Tetelegyallapot.Modositott = true;
            }
        }

        private void tetelOsszegSzamitas()
        {
            decimal num2;
            Egyinputinfo egyinputinfo = (Egyinputinfo)this.Szamlatetelinfo.Inputinfo[this.Szamlatetelinfo.GetInputColIndex("AFAKULCS")];
            decimal num = Convert.ToDecimal(egyinputinfo.Comboaktfileba);
            decimal dec;

            if (this.rBPenztar.Checked)
            {
                decimal num3 = Convert.ToDecimal(this.textBox6.Text.PadRight(1, '0'));
                dec = num / 100M;
                num2 = num3 / ++(dec);
                num2 = decimal.Round(num2, 2);
                this.textBox3.Text = num2.ToString();
                this.textBox5.Text = (num3 - num2).ToString();
            }
            else
            {
                num2 = Convert.ToDecimal(this.textBox3.Text.PadRight(1, '0'));
                decimal num5 = (num * num2) / 100M;
                this.textBox5.Text = num5.ToString();
                this.textBox6.Text = (num2 + ((num * num2) / 100M)).ToString();
            }
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (this.textBox3.Text == "0")
            {
                this.textBox3.Text = "";
            }
        }

        private void textBox6_Enter(object sender, EventArgs e)
        {
            if (this.textBox6.Text == "0")
            {
                this.textBox6.Text = "";
            }
        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            this.tetelOsszegSzamitas();
        }

        private void Uj_szamla(object sender, EventArgs e)
        {
            this.Szamla_beallit(-1);
        }

        private void Uj_tetel(object sender, EventArgs e)
        {
            this.Tetel_beallit(-1);
            this.groupBox1.Enabled = false;
            this.groupBox2.Enabled = false;
            this.buttonUj.Enabled = false;
            this.buttonTöröl.Enabled = false;
            if (this.rBPenztar.Checked)
            {
                this.textBox3.ReadOnly = true;
                this.textBox6.ReadOnly = false;
            }
            else
            {
                this.textBox6.ReadOnly = true;
                this.textBox3.ReadOnly = false;
            }
        }

        private string Validalas(Control cont)
        {
            string str = "";
            string str2 = cont.Text.Trim();
            Taggyart tag = (Taggyart)cont.Tag;
            switch (tag.Valid)
            {
                case 1:
                    if ((this.comboBox1.Text != "") && ((!this.rBPenztar.Checked && (this.textBox3.Text != "")) || (this.rBPenztar.Checked && (this.textBox6.Text != ""))))
                    {
                        this.tetelOsszegSzamitas();
                    }
                    break;

                case 2:
                    {
                        string actualCombofileinfo = "";
                        if (this.comboSzallito.Text != "")
                        {
                            actualCombofileinfo = this.Szamlainfo.GetActualCombofileinfo(this.comboSzallito);
                            if (this.jel == "S")
                            {
                            }
                        }
                        if (this.jel == "S")
                        {
                            if (((this.comboSzallito.Text != "") && (this.szamlaszam.Text != "")) && (this.Szamlainfo.FindRow(new string[] { "VS", "PID", "AZONOSITO" }, new string[] { this.jel, actualCombofileinfo, this.szamlaszam.Text.Trim() }, this.Szamlaaktsorindex) != null))
                            {
                                str = this.comboSzallito.Text.Trim() + "- nak már van ilyen számlaszáma!";
                            }
                        }
                        else if ((this.szamlaszam.Text != "") && (this.Szamlainfo.FindRow(new string[] { "VS", "AZONOSITO" }, new string[] { this.jel, this.szamlaszam.Text.Trim() }, this.Szamlaaktsorindex) != null))
                        {
                            str = "Már van ilyen szamú vevőszámla!";
                        }
                        break;
                    }
                case 3:
                    {
                        string str4 = this.Szamlainfo.GetActualCombofileinfo(cont);
                        break;
                    }
            }
            this.Fak.ErrorProvider.SetError(cont, str);
            return str;
        }

        private void Vissza(object sender, EventArgs e)
        {
            if (!(this.buttonMentes.Enabled && (MessageBox.Show("A változások elvesszenek?", "", System.Windows.Forms.MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)))
            {
                this.igazivaltozas = false;
                this.Fak.ForceAdattolt(this.Szamlainfo);
                this.Fak.ForceAdattolt(this.Szamlatetelinfo);
                this.Szamlaegyallapot.Modositott = false;
                this.Tetelegyallapot.Modositott = false;
                base.Visible = false;
            }
        }
    }
}
