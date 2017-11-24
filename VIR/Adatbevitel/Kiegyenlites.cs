
/*using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Könyvtar.ClassGyujtemeny;
using Könyvtar.Printlib;
using TableInfo;
using System.Data.SqlClient;
using MainProgramm.Listák;
*/

using FormattedTextBox;
using Könyvtar.Printlib;
using MainProgramm;
using MainProgramm.Listák;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using TableInfo;
using VIR;

namespace Adatbevitel
{
    public partial class Kiegyenlites : UserControl
    {
        private VIR.MainForm mainForm;
        private DataSet ds = new DataSet();

        private PrintForm nyomtat = new PrintForm();
        private Kintlevöseg kintlevösegLista = new Kintlevöseg();

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
        private string jel;
        private bool igazivaltozas = false;
        private MyTag kodtag;
        private Egycontrolinfo szamlaegycont;
        private Egycontrolinfo tetelegycont;
        private bool tobbhonap = false;
        private DateTime tol = Convert.ToDateTime("1801.01.01");
        private DateTime ig = Convert.ToDateTime("9900.12.31");
        private DataGridViewColumn[] szlatetelgridcols;// = Szamlatetelinfo.GetGridColumns();
        private Egyallapotinfo Szamlaegyallapot;
        private Egyallapotinfo Tetelegyallapot;

        public Kiegyenlites(string szoveg, object[] obj)
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
            this.InitializeComponent();
            if (this.jel == "V")
            {
                this.comboBox2.Visible = false;
                this.label12.Visible = false;
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
                MessageBox.Show("Nincs termék- vagy kötség kód!");
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
                MessageBox.Show("Nincs 'saját' jelü partner!");
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
                this.dateTimePicker1.Value = this.dateTimePicker3.Value;
                this.dateTeljesit.Value = this.dateTimePicker3.Value;
                this.Szamlainfo.Comboinfoszures(this.comboSzallito, kellfileinfo);
                string[] strArray3 = this.folyoszlainfo.FindIdentityArray(new string[] { "PID" }, tartalom);
                this.Szamlainfo.Comboinfoszures(this.comboBox4, strArray3);
                this.szamlaegycont = this.Szamlainfo.GetEgycontrolinfo(this);
                if (this.comboSzallito.Items.Count == 0)
                {
                    MessageBox.Show("Elöbb vezesse fel a partnereit!");
                    base.Visible = false;
                }
                if (base.Visible)
                {
                    this.tetelegycont = this.Szamlatetelinfo.GetEgycontrolinfo(this);
                    this.Tetelegyallapot = this.Szamlatetelinfo.GetEgyallapotinfo(this);
                    this.dataGVSzla.AutoGenerateColumns = false;
                    DataGridViewColumn[] gridColumns = this.Szamlainfo.GetGridColumns();
                    for (int i = 0; i < gridColumns.Length; i++)
                    {
                        gridColumns[i].Name = gridColumns[i].DataPropertyName;
                    }
                    this.dataGVSzla.Columns.AddRange(gridColumns);
                    DataGridViewColumn dataGridViewColumn = this.dataGVSzla.Columns["FIZETVE"];
                    this.dataGVSzla.Columns.Remove("FIZETVE");
                    dataGridViewColumn.DisplayIndex = 0;
                    dataGridViewColumn.ReadOnly = false;
                    this.dataGVSzla.Columns.Insert(0, dataGridViewColumn);
                    this.dataGVSzla.Columns[0].Frozen = true;
                    this.dataGVSzla.Columns[1].Frozen = true;
                    this.dataGVSzla.Columns[2].SortMode = DataGridViewColumnSortMode.Automatic;
                    this.Szamlaidcol = this.Szamlainfo.Identitycol;
                    this.szamlaadatok = this.Fak.Adatoktolt("C", this.Fak.Aktintervallum, this.szamlaadatok, " where VS='" + this.jel + "' AND DATUM between cast('" + this.dateSzamla.MinDate.ToShortDateString() + "' as datetime) AND cast('" + this.dateSzamla.MaxDate.ToShortDateString() + "' as datetime) and fizetve = 'N'", false);
                    this.SzamlaAdattabla = this.Szamlainfo.Adattabla;
                    this.Szamlatetelidcol = this.Szamlatetelinfo.Identitycol;
                    this.dataViewSzamla.Table = this.SzamlaAdattabla;
                    this.dataViewSzamla.Sort = this.Szamlainfo.Sort;
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
                    this.groupBox2.Enabled = false;
                    this.Szamla_beallit(this.Szamlaaktgridrowind);
                }
            }
        }

        private void Kiegyenlites_Load(object sender, EventArgs e)
        {
            this.mainForm = (VIR.MainForm)base.ParentForm;
        }

        private void Szamla_beallit(int gridind)
        {
            int num;
            this.igazivaltozas = false;
            this.dateSzamla.MinDate = this.tol;
            this.dateSzamla.MaxDate = this.ig;
            this.dateSzamla.MinDate = this.dateTimePicker3.Value;
            this.dateSzamla.MaxDate = this.dateTimePicker4.Value;
            this.dateSzamla.Value = this.dateTimePicker3.Value;
            this.buttonMentes.Enabled = false;
            this.Szamlaegyallapot.Modositott = false;
            for (num = 0; num < this.dataViewSzamla.Count; num++)
            {
                this.dataGVSzla.Rows[num].Selected = false;
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
            }
            else
            {
                this.Szamlaaktid = -1;
                this.Szamlaaktsorindex = -1;
                this.Szamlainfo.Aktsorindex = -1;
            }
            this.dateTimePicker1.MinDate = this.tol;
            if ((this.dateTimePicker1.Value.CompareTo(this.dateSzamla.Value) < 0) || (this.Szamlaaktid == -1))
            {
                this.dateTimePicker1.Value = this.dateSzamla.Value;
            }
            this.dateTimePicker1.MinDate = this.dateSzamla.Value;
            this.dateTeljesit.MinDate = this.tol;
            if ((this.dateTeljesit.Value.CompareTo(this.dateTimePicker1.Value) < 0) || (this.Szamlaaktid == -1))
            {
                this.dateTeljesit.Value = this.dateTimePicker1.Value;
            }
            this.dateTeljesit.MinDate = this.dateTimePicker1.Value;
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
            this.groupBox1.Enabled = true;
            decimal num2 = 0M;
            this.teteladatok = this.Fak.Adatoktolt("T", this.Fak.Aktintervallum, this.teteladatok, "", false);
            for (num = 0; num < this.dataViewTetel.Count; num++)
            {
                num2 += Convert.ToDecimal(this.dataViewTetel[num].Row["brutto"].ToString());
            }
            if (this.dataViewTetel.Count == 0)
            {
                this.Szamlatetelaktgridrowind = -1;
            }
            else
            {
                this.Szamlatetelaktgridrowind = 0;
            }
            this.RadiobuttonBeallitasok();
            this.osszesen.Text = $"{num2:N}";
            this.igazivaltozas = true;
        }

        private void Szamla_ok()
        {
            if ((this.Fak.Hibavizsg(this, this.Szamlainfo, null) == "") && (this.Validalas(this.comboSzallito) == ""))
            {
                this.groupBox1.Enabled = false;
                this.groupBox2.Enabled = false;
                if (this.Szamlaaktgridrowind == -1)
                {
                    this.SzamlaAdattabla = this.Szamlainfo.Ujsor();
                    this.Szamlaaktsorindex = this.SzamlaAdattabla.Rows.Count - 1;
                    this.Szamlaaktgridrowind = 0;
                    this.Szamlatetelaktgridrowind = -1;
                }
                DataRow row = this.SzamlaAdattabla.Rows[this.Szamlaaktsorindex];
                this.Szamlainfo.Adatsortolt(this.Szamlaaktsorindex)["VS"] = this.jel;
                this.dataViewSzamla.Table = this.SzamlaAdattabla;
                this.dataViewSzamla.Sort = this.Szamlainfo.Sort;
            }
        }

        private string Validalas(Control cont)
        {
            string str = "";
            string str2 = cont.Text.Trim();
            Taggyart tag = (Taggyart)cont.Tag;
            switch (tag.Valid)
            {
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
                                str = this.comboSzallito.Text.Trim() + "- nak mar van ilyen szamlaszama!";
                            }
                        }
                        else if ((this.szamlaszam.Text != "") && (this.Szamlainfo.FindRow(new string[] { "VS", "AZONOSITO" }, new string[] { this.jel, this.szamlaszam.Text.Trim() }, this.Szamlaaktsorindex) != null))
                        {
                            str = "Mar van ilyen szamu vevoszamla!";
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

        private void Mentes(object sender, EventArgs e)
        {
            this.Szamla_ok();
            ArrayList modositandok = new ArrayList {
                this.Szamlainfo,
                this.Szamlatetelinfo
            };
            this.igazivaltozas = false;
            if (!this.Fak.UpdateTransaction(modositandok))
            {
                base.Enabled = true;
                base.Visible = false;
            }
            if (base.Visible)
            {
                if (this.dataViewSzamla.Count == 0)
                {
                    this.Szamlaaktgridrowind = -1;
                }
                else
                {
                    this.Szamlaaktgridrowind = 0;
                }
                this.Szamla_beallit(this.Szamlaaktgridrowind);
                this.buttonMentes.Enabled = false;
                this.Szamlaegyallapot.Modositott = false;
                this.Szamlaegyallapot.Mentett = true;
            }
        }

        private void Vissza(object sender, EventArgs e)
        {
            if (!(this.buttonMentes.Enabled && (MessageBox.Show("A változások elvesszenek?", "", System.Windows.Forms.MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)))
            {
                this.igazivaltozas = false;
                this.Fak.ForceAdattolt(this.Szamlainfo);
                this.Fak.ForceAdattolt(this.Szamlatetelinfo);
                this.Szamlaegyallapot.Modositott = false;
                base.Visible = false;
            }
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
            }
        }

        private void dataGVSzla_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string name = this.dataGVSzla.Columns[e.ColumnIndex].Name;
            if ((e.Clicks == 1) && (e.RowIndex > -1))
            {
                if ((name == "FIZETVE") && (this.dataGVSzla.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "N"))
                {
                    this.dataGVSzla.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "I";
                    this.dataGVSzla.Rows[e.RowIndex].Cells["DATUM_FIZETVE"].Value = DateTime.Today.ToShortDateString();
                }
                else if ((name == "FIZETVE") && (this.dataGVSzla.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "I"))
                {
                    this.dataGVSzla.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "N";
                    this.dataGVSzla.Rows[e.RowIndex].Cells["DATUM_FIZETVE"].Value = this.dateFizetve.MinDate.ToShortDateString();
                }
                this.Szamla_beallit(e.RowIndex);
            }
        }

        private void Elolrol(object sender, EventArgs e)
        {
            this.igazivaltozas = false;
            this.SzamlaAdattabla = this.Fak.ForceAdattolt(this.Szamlainfo);
            this.SzamlatetelAdattabla = this.Fak.ForceAdattolt(this.Szamlatetelinfo);
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

        private void rb_CheckChanged(object sender, EventArgs e)
        {
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

        private void dateTimePicker_Changed(object sender, EventArgs e)
        {
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
                    time = Convert.ToDateTime(this.dateTimePicker3.Value.Year.ToString() + "." + this.dateTimePicker3.Value.Month.ToString() + ".01");
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
                this.szamlaadatok = this.Fak.Adatoktolt("C", this.Fak.Aktintervallum, this.szamlaadatok, " where VS='" + this.jel + "' AND DATUM between cast('" + this.dateTimePicker3.Value.ToShortDateString() + "' as datetime) AND cast('" + this.dateTimePicker4.Value.ToShortDateString() + "' as datetime) and fizetve = 'N'", false);
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

        private void dateSzamla_Changed(object sender, EventArgs e)
        {
            bool igazivaltozas = this.igazivaltozas;
            DateTimePicker picker = (DateTimePicker)sender;
            if (this.igazivaltozas)
            {
                this.igazivaltozas = false;
                if (picker == this.dateSzamla)
                {
                    this.dateTimePicker1.MinDate = this.tol;
                    if (this.dateTimePicker1.Value.CompareTo(this.dateSzamla.Value) < 0)
                    {
                        this.dateTimePicker1.Value = this.dateSzamla.Value;
                    }
                    this.dateTimePicker1.MinDate = this.dateSzamla.Value;
                }
                this.dateTeljesit.MinDate = this.tol;
                if (this.dateTeljesit.Value.CompareTo(this.dateTimePicker1.Value) < 0)
                {
                    this.dateTeljesit.Value = this.dateTimePicker1.Value;
                }
                this.dateTeljesit.MinDate = this.dateTimePicker1.Value;
            }
            this.igazivaltozas = igazivaltozas;
        }

        private void buttonNyomtat_Click(object sender, EventArgs e)
        {
            /*
            this.nyomtat = new PrintForm();
            string[] parName = new string[] { "cim", "DATUM_TOL", "DATUM_IG" };
            string[] parValue = new string[] { "", this.dateTimePicker3.Value.ToShortDateString(), this.dateTimePicker3.Value.ToShortDateString() };
            string[] parTyp = new string[] { "string", "string", "string" };
            if (this.jel == "V")
            {
                parValue[0] = "Vevői kintlevőség lista";
            }
            else
            {
                parValue[0] = "Szállító tartozások lista";
            }
            DataSet dataSet = new MainProgramm.virDataSet();
            string str = "0";
            dataSet.Tables["Kintlevőseg"].Clear();
            for (int i = 0; i < this.dataViewSzamla.Count; i++)
            {
                this.Szamlaaktsorindex = this.SzamlaAdattabla.Rows.IndexOf(this.dataViewSzamla[i].Row);
                this.Szamlainfo.Aktsorindex = this.Szamlaaktsorindex;
                this.Szamlaaktid = Convert.ToInt32(this.SzamlaAdattabla.Rows[this.Szamlaaktsorindex][this.Szamlaidcol].ToString());
                this.Szamlainfo.Aktidentity = this.Szamlaaktid;
                DataRow row = dataSet.Tables["Kintlevőseg"].NewRow();
                row["azonosito"] = this.dataViewSzamla[i]["azonosito"];
                row["partner"] = this.dataViewSzamla[i]["pid_k"];
                row["datum_telj"] = this.dataViewSzamla[i]["datum_telj"];
                row["datum_fiz"] = this.dataViewSzamla[i]["datum_fiz"];
                row["datum"] = this.dataViewSzamla[i]["datum"];
                str = "0";
                this.teteladatok = this.Fak.Adatoktolt("T", this.Fak.Aktintervallum, this.teteladatok, "", false);
                for (int j = 0; j < this.SzamlatetelAdattabla.Rows.Count; j++)
                {
                    str = Convert.ToString((decimal)(Convert.ToDecimal(str) + Convert.ToDecimal(this.SzamlatetelAdattabla.Rows[j]["brutto"].ToString())));
                }
                row["osszeg"] = str;
                dataSet.Tables["Kintlevőseg"].Rows.Add(row);
            }
            this.nyomtat.PrintParams(parName, parValue, parTyp);
            this.kintlevösegLista.SetDataSource(dataSet);
            this.kintlevösegLista.SetParameterValue("cim", parValue[0]);
            this.kintlevösegLista.SetParameterValue("DATUM_TOL", parValue[1]);
            this.kintlevösegLista.SetParameterValue("DATUM_IG", parValue[2]);
            this.nyomtat.reportSource = this.kintlevösegLista;
            this.nyomtat.DoPreview(this.mainForm.defPageSettings);
            */
            this.nyomtat = new PrintForm();
            string[] shortDateString = new string[] { "cim", "DATUM_TOL", "DATUM_IG" };
            string[] strArrays = shortDateString;
            shortDateString = new string[] { "", null, null };
            DateTime value = this.dateTimePicker3.Value;
            shortDateString[1] = value.ToShortDateString();
            value = this.dateTimePicker3.Value;
            shortDateString[2] = value.ToShortDateString();
            string[] strArrays1 = shortDateString;
            shortDateString = new string[] { "string", "string", "string" };
            string[] strArrays2 = shortDateString;
            if (!(this.jel == "V"))
            {
                strArrays1[0] = "Szállító tartozások lista";
            }
            else
            {
                strArrays1[0] = "Vevöi kintlevőség lista";
            }
            DataSet _virDataSet = new virDataSet();
            string str = "0";
            _virDataSet.Tables["Kintlevöseg"].Clear();
            for (int i = 0; i < this.dataViewSzamla.Count; i++)
            {
                this.Szamlaaktsorindex = this.SzamlaAdattabla.Rows.IndexOf(this.dataViewSzamla[i].Row);
                this.Szamlainfo.Aktsorindex = this.Szamlaaktsorindex;
                this.Szamlaaktid = Convert.ToInt32(this.SzamlaAdattabla.Rows[this.Szamlaaktsorindex][this.Szamlaidcol].ToString());
                this.Szamlainfo.Aktidentity = this.Szamlaaktid;
                DataRow item = _virDataSet.Tables["Kintlevöseg"].NewRow();
                item["azonosito"] = this.dataViewSzamla[i]["azonosito"];
                item["partner"] = this.dataViewSzamla[i]["pid_k"];
                item["datum_telj"] = this.dataViewSzamla[i]["datum_telj"];
                item["datum_fiz"] = this.dataViewSzamla[i]["datum_fiz"];
                item["datum"] = this.dataViewSzamla[i]["datum"];
                str = "0";
                this.teteladatok = this.Fak.Adatoktolt("T", this.Fak.Aktintervallum, this.teteladatok, "", false);
                for (int j = 0; j < this.SzamlatetelAdattabla.Rows.Count; j++)
                {
                    str = Convert.ToString(Convert.ToDecimal(str) + Convert.ToDecimal(this.SzamlatetelAdattabla.Rows[j]["brutto"].ToString()));
                }
                item["osszeg"] = str;
                _virDataSet.Tables["Kintlevöseg"].Rows.Add(item);
            }
            this.nyomtat.PrintParams(strArrays, strArrays1, strArrays2);
            this.kintlevösegLista.SetDataSource(_virDataSet);
            this.kintlevösegLista.SetParameterValue("cim", strArrays1[0]);
            this.kintlevösegLista.SetParameterValue("DATUM_TOL", strArrays1[1]);
            this.kintlevösegLista.SetParameterValue("DATUM_IG", strArrays1[2]);
            this.nyomtat.reportSource = this.kintlevösegLista;
            this.nyomtat.DoPreview(this.mainForm.defPageSettings);

        }
    }
}
