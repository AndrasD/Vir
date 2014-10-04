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
using TableInfo;
using Könyvtar.Printlib;
using MainProgramm.Listák;

namespace Adatbevitel
{
    public partial class Kiegyenlites : UserControl
    {
        private VIR.MainForm mainForm;

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

        private PrintForm nyomtat = new PrintForm();
        private Kintlevöseg kintlevösegLista = new Kintlevöseg();

        public Kiegyenlites(string szoveg, object[] obj)
        {
            szamlatag = ((MyTag[])obj[1])[0];
            Fak = szamlatag.Fak;
            partnerinfo = Fak.GetTablaInfo("C", "PARTNER");
            folyoszlainfo = Fak.GetTablaInfo("C", "PARTNER_FOLYOSZ");
            partneradatok = new Tablainfo[] { partnerinfo };
            partneradatok = Fak.Adatoktolt("C", Fak.Aktintervallum, partneradatok, "", false);
            folyoszlainfoadatok = new Tablainfo[] { folyoszlainfo };
            folyoszlainfoadatok = Fak.Adatoktolt("C", Fak.Aktintervallum, folyoszlainfoadatok, "", false);
            Szamlainfo = szamlatag.AdatTablainfo;
            szamlateteltag = ((MyTag[])obj[1])[1];
            Szamlatetelinfo = szamlateteltag.AdatTablainfo;
            szamlaadatok = new Tablainfo[] { Szamlainfo};
            SzamlaAdattabla = Szamlainfo.Adattabla;
            teteladatok = new Tablainfo[] { Szamlatetelinfo };
            SzamlatetelAdattabla = Szamlatetelinfo.Adattabla;
            jel = obj[2].ToString();
            InitializeComponent();
            if (jel == "V")
            {
                comboBox2.Visible = false;
                label12.Visible = false;
                ((Cols)Szamlainfo.TablaColumns[Szamlainfo.GetTablaColIndex("FSZ_ID")]).ReadOnly=true;
                ((Cols)Szamlainfo.KiegColumns[Szamlainfo.GetKiegcolIndex("FSZ_ID_K")]).Lathato = false;
                kodtag = Fak.GetKodtab("C", "Term");
            }
            else
            {
                ((Cols)Szamlainfo.TablaColumns[Szamlainfo.GetTablaColIndex("FSZ_ID")]).ReadOnly=false;
                ((Cols)Szamlainfo.KiegColumns[Szamlainfo.GetKiegcolIndex("FSZ_ID_K")]).Lathato = true;
                kodtag = Fak.GetKodtab("C", "Kolt");
            }
            if (!Szamlatetelinfo.AktCombotolt("MEGNID", kodtag))
            {
                MessageBox.Show("Nincs termék- vagy költség kód!");
                this.Visible = false;
            }
            string[] pidN = partnerinfo.FindIdentityArray(new string[] { "SAJAT" }, new string[] { "N" });
            string[] pidI = partnerinfo.FindIdentityArray(new string[] { "SAJAT" }, new string[] { "I" });
            if (pidN == null)
            {
                MessageBox.Show("Csak 'saját' jelü partner van!");
                this.Visible = false;
            }
            else if (pidI == null)
            {
                MessageBox.Show("Nincs 'sajat' jelu partner!");
                this.Visible = false;
            }
            if (this.Visible)
            {
                Fak.ControlTagokTolt(this, groupBox2);
                Szamlaegyallapot = Szamlainfo.GetEgyallapotinfo(this);
                dateTimePicker3.Value = Convert.ToDateTime(dateTimePicker3.Value.Year.ToString() + "." + dateTimePicker3.Value.Month.ToString() + ".01");
                DateTime dat = Convert.ToDateTime(dateTimePicker4.Value.Year.ToString() + "." + dateTimePicker4.Value.Month.ToString() + ".01");
                dateTimePicker4.Value = Convert.ToDateTime(dat.AddMonths(1).AddDays(-1));
                dateSzamla.MinDate = dateTimePicker3.Value;
                dateSzamla.MaxDate = dateTimePicker4.Value;
                dateSzamla.Value = dateTimePicker3.Value;
                dateTimePicker1.Value = dateTimePicker3.Value;
                dateTeljesit.Value = dateTimePicker3.Value;
                Szamlainfo.Comboinfoszures(comboSzallito, pidN);
                string[] fszid=folyoszlainfo.FindIdentityArray(new string[]{"PID"},pidI);
                Szamlainfo.Comboinfoszures(comboBox4,fszid);
                szamlaegycont=Szamlainfo.GetEgycontrolinfo(this);
                if (comboSzallito.Items.Count == 0)
                {
                    MessageBox.Show("Elöbb vezesse fel a partnereit!");
                    this.Visible = false;
                } 
                if (this.Visible)
                {
                    tetelegycont = Szamlatetelinfo.GetEgycontrolinfo(this);
                    Tetelegyallapot = Szamlatetelinfo.GetEgyallapotinfo(this);
                    dataGVSzla.AutoGenerateColumns = false;
                    DataGridViewColumn[] szlagridcols = Szamlainfo.GetGridColumns();
                    DataGridViewColumn tempCol = szlagridcols[szlagridcols.Length - 1];
                    dataGVSzla.Columns.AddRange(szlagridcols);
                    dataGVSzla.Columns.Remove("FIZETVE");
                    tempCol.DisplayIndex = 0;
                    tempCol.ReadOnly = false;
  //                  dataGVSzla.Columns.Insert(0, tempCol);
                    dataGVSzla.Columns[0].Frozen = true;
                    dataGVSzla.Columns[1].Frozen = true;
                    dataGVSzla.Columns[2].SortMode = DataGridViewColumnSortMode.Automatic;
                    Szamlaidcol = Szamlainfo.Identitycol;
                    szamlaadatok = Fak.Adatoktolt("C", Fak.Aktintervallum, szamlaadatok, 
                        " where VS='" + jel + "'" +
                        " AND DATUM between cast('" + dateSzamla.MinDate.ToShortDateString()+"' as datetime)"+
                        " AND cast('" + dateSzamla.MaxDate.ToShortDateString()+"' as datetime)"+
                        " and fizetve = 'N'",false);
                    SzamlaAdattabla = Szamlainfo.Adattabla;
                    Szamlatetelidcol = Szamlatetelinfo.Identitycol;
                    dataViewSzamla.Table = SzamlaAdattabla;
                    dataViewSzamla.Sort = Szamlainfo.Sort;
                    if (dataViewSzamla.Count == 0)
                        Szamlaaktgridrowind = -1;
                    else
                        Szamlaaktgridrowind = 0;
                    dataGVSzla.DataSource = dataViewSzamla;
                    dataViewTetel.Table = SzamlatetelAdattabla;
                    dataViewTetel.Sort = Szamlatetelinfo.Sort;
                    //dataGVSzlatetel.DataSource = dataViewTetel;
                    groupBox2.Enabled = false;
                    Szamla_beallit(Szamlaaktgridrowind);
                }
            }
        }

        private void Szamla_beallit(int gridind)
        {
            igazivaltozas = false;
            dateSzamla.MinDate = tol;
            dateSzamla.MaxDate = ig;
            dateSzamla.MinDate = dateTimePicker3.Value;
            dateSzamla.MaxDate = dateTimePicker4.Value;
            dateSzamla.Value = dateTimePicker3.Value;
            buttonMentes.Enabled = false;
            Szamlaegyallapot.Modositott = false;
            //Tetelegyallapot.Modositott = false;
            for (int i = 0; i < dataViewSzamla.Count; i++)
                dataGVSzla.Rows[i].Selected = false;
            Szamlaaktgridrowind = gridind;
            Lastszamlaaktgridindex = gridind;
            if (Szamlaaktgridrowind != -1)
            {
                Szamlaaktsorindex=SzamlaAdattabla.Rows.IndexOf(dataViewSzamla[Szamlaaktgridrowind].Row);
                Szamlainfo.Aktsorindex = Szamlaaktsorindex;
                dataGVSzla.Rows[Szamlaaktgridrowind].Selected = true;
                Szamlaaktid = Convert.ToInt32(SzamlaAdattabla.Rows[Szamlaaktsorindex][Szamlaidcol].ToString());
                Szamlainfo.Aktidentity=Szamlaaktid;
            }
            else
            {
                Szamlaaktid = -1;
                Szamlaaktsorindex=-1;
                Szamlainfo.Aktsorindex = -1;
            }
            dateTimePicker1.MinDate = tol;
            if(dateTimePicker1.Value.CompareTo(dateSzamla.Value) < 0 || Szamlaaktid==-1)
               dateTimePicker1.Value = dateSzamla.Value;
            dateTimePicker1.MinDate = dateSzamla.Value;
            dateTeljesit.MinDate = tol;
            if(dateTeljesit.Value.CompareTo(dateTimePicker1.Value) < 0 || Szamlaaktid==-1)
              dateTeljesit.Value = dateTimePicker1.Value;
            dateTeljesit.MinDate = dateTimePicker1.Value;
            if (comboBox2.Visible)
            {
                string pid = Szamlainfo.GetActualCombofileinfo(comboSzallito);
                partnerinfo.Aktidentity = Convert.ToInt32(pid);
                Tablainfo[] adatok = new Tablainfo[] { folyoszlainfo };
                adatok = Fak.Adatoktolt("C", Fak.Aktintervallum, adatok, "", false);
                Szamlainfo.Aktsorindex = Szamlaaktsorindex;
                string[] fszid = folyoszlainfo.FindIdentityArray(new string[] { "PID" }, new string[] { pid });
                if (fszid != null)
                    Szamlainfo.Comboinfoszures(comboBox2, fszid);
            }
            groupBox1.Enabled = true;
            osszesen.Text = "0";
            teteladatok = Fak.Adatoktolt("T",Fak.Aktintervallum,teteladatok,"",false);
            for (int i = 0; i < dataViewTetel.Count; i++)
                osszesen.Text = Convert.ToString(Convert.ToDecimal(osszesen.Text) + Convert.ToDecimal(dataViewTetel[i].Row["brutto"].ToString()));
            if (dataViewTetel.Count == 0)
                Szamlatetelaktgridrowind = -1;
            else
                Szamlatetelaktgridrowind = 0;
            RadiobuttonBeallitasok();
            igazivaltozas = true;
        }

        private void Szamla_ok()
        {
            string hiba = Fak.Hibavizsg(this,Szamlainfo, null);
            if (hiba == "")
            {
                hiba = Validalas(comboSzallito);
                if (hiba == "")
                {
                    groupBox1.Enabled = false;
                    groupBox2.Enabled = false;
                    if (Szamlaaktgridrowind == -1)
                    {
                        SzamlaAdattabla=Szamlainfo.Ujsor();
                        Szamlaaktsorindex = SzamlaAdattabla.Rows.Count - 1;
                        Szamlaaktgridrowind = 0;
                        Szamlatetelaktgridrowind = -1;
                    }
                    DataRow dr = SzamlaAdattabla.Rows[Szamlaaktsorindex];
                    dr = Szamlainfo.Adatsortolt(Szamlaaktsorindex);
                    dr["VS"] = jel;
                    dataViewSzamla.Table=SzamlaAdattabla;
                    dataViewSzamla.Sort = Szamlainfo.Sort;
                }
            }
        }

        private string Validalas(Control cont)
        {
            string hibaszov="";
            string tartal = cont.Text.Trim();
            Taggyart tg = (Taggyart)cont.Tag;
            switch (tg.Valid)
            {
                case 2:
                    string pid = "";
                    DataRow dr;
                    if (comboSzallito.Text != "")
                    {
                        pid = Szamlainfo.GetActualCombofileinfo(comboSzallito);//, comboSzallito.Text.Trim());
                        if(jel=="S")
                        {
 //                           string[] fszid = folyoszlainfo.FindIdentityArray(new string[] { "PID" }, new string[] { pid });
                            //                   for (int i = 0; i < fszid.Length; i++)
                            //                       fszid[i] = arr[i]["FSZ_ID"].ToString().Trim();
 //                           Szamlainfo.Aktsorindex=Szamlaaktsorindex;
                        }
                    }
                    if (jel == "S")
                    {
                        if (comboSzallito.Text != "" && szamlaszam.Text != "")
                        {
                            dr = Szamlainfo.FindRow(new string[] { "VS", "PID", "AZONOSITO" }, new string[] { jel, pid, szamlaszam.Text.Trim() }, Szamlaaktsorindex);
                            if (dr != null)
                                hibaszov = comboSzallito.Text.Trim() + "- nak mar van ilyen szamlaszama!";
                        }
                    }
                    else if (szamlaszam.Text != "")
                    {
                        dr = Szamlainfo.FindRow(new string[] { "VS", "AZONOSITO" }, new string[] { jel, szamlaszam.Text.Trim() }, Szamlaaktsorindex);
                        if (dr != null)
                            hibaszov = "Mar van ilyen szamu vevoszamla!";
                    }
                    break;
                case 3: 
                    string fsz = Szamlainfo.GetActualCombofileinfo(cont);
                    break;
            }
            Fak.ErrorProvider.SetError(cont, hibaszov);
            return hibaszov;
        }

        private void Mentes(object sender, EventArgs e)
        {
            Szamla_ok();

            ArrayList list = new ArrayList();
            list.Add(Szamlainfo);
            list.Add(Szamlatetelinfo);
            igazivaltozas = false;
            if (!Fak.UpdateTransaction(list))
            {
                this.Enabled = true;
                this.Visible = false;
            }
            if (this.Visible)
            {
                if (dataViewSzamla.Count == 0)
                    Szamlaaktgridrowind = -1;
                else
                    Szamlaaktgridrowind = 0;
                Szamla_beallit(Szamlaaktgridrowind);
                buttonMentes.Enabled = false;
                Szamlaegyallapot.Modositott = false;
                Szamlaegyallapot.Mentett = true;
            }

        }

        private void Vissza(object sender, EventArgs e)
        {
            if (!buttonMentes.Enabled || MessageBox.Show("A változások elvesszenek?", "", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                igazivaltozas = false;
                Fak.ForceAdattolt(Szamlainfo);
                Fak.ForceAdattolt(Szamlatetelinfo);
                Szamlaegyallapot.Modositott = false;
                //Tetelegyallapot.Modositott = false;
                this.Visible = false;
            }
        }

        private void Elem_Validated(object sender, EventArgs e)
        {
            if (igazivaltozas)
            {
                Taggyart tg = (Taggyart)((Control)sender).Tag;
                string hibaszov = Fak.Hibavizsg(this, tg.Tabinfo, (Control)sender);
                if (hibaszov == "")
                    Validalas((Control)sender);
            }
        }

        private void dataGVSzla_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string n = dataGVSzla.Columns[e.ColumnIndex].Name;

            if (e.Clicks == 1 && e.RowIndex > -1) 
            {
                if (n == "FIZETVE" && dataGVSzla.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "N")
                {
                    dataGVSzla.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "I";
                }
                else if (n == "FIZETVE" && dataGVSzla.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "I")
                {
                    dataGVSzla.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "N";
                }
                Szamla_beallit(e.RowIndex);
            }
        }

        private void Elolrol(object sender, EventArgs e)
        {
            igazivaltozas = false;
            SzamlaAdattabla = Fak.ForceAdattolt(Szamlainfo);
            SzamlatetelAdattabla = Fak.ForceAdattolt(Szamlatetelinfo);
            if (dataViewSzamla.Count == 0)
                Szamlaaktgridrowind = -1;
            else
                Szamlaaktgridrowind = 0;
            Szamla_beallit(Szamlaaktgridrowind);
            igazivaltozas = true;
        }

        private void rb_CheckChanged(object sender, EventArgs e)
        {
            if (igazivaltozas)
            {
                if (rBPenztar == (RadioButton)sender)
                {
                    if (rBPenztar.Checked)
                    {
                        checkBox1.Checked = true;
                        checkBox1.Enabled = false;
                    }
                    else
                        checkBox1.Enabled = true;
                }
            }

            RadiobuttonBeallitasok();
        }

        private void RadiobuttonBeallitasok()
        {
            bool elozoigazi = igazivaltozas;
            igazivaltozas = false;

            if (rBBank.Checked)
            {
                comboBox4.Enabled = true;
                Fak.SetCombodef(comboBox4);
                comboBox6.Enabled = false;
                Fak.ClearCombodef(comboBox6);
   //             comboBox6.Text = "";
            }
            if (rBPenztar.Checked)
            {
                comboBox4.Enabled = false;
                Fak.ClearCombodef(comboBox4);
 //               comboBox4.Text = "";
                comboBox6.Enabled = true;
                Fak.SetCombodef(comboBox6);
            }

            igazivaltozas = elozoigazi;
        }

        private void dateTimePicker_Changed(object sender, EventArgs e)
        {
            bool elozoigazi = igazivaltozas;
            if(igazivaltozas)
            {
                DateTime dat;
                igazivaltozas = false;
                DateTimePicker kuldo = (DateTimePicker)sender;
                Fak.ErrorProvider.SetError(kuldo, "");
                if (kuldo == dateTimePicker3)
                {
                    dateTimePicker3.Value = Convert.ToDateTime(dateTimePicker3.Value.Year.ToString() + "." + dateTimePicker3.Value.Month.ToString() + ".01");
                    dat = Convert.ToDateTime(dateTimePicker3.Value.Year.ToString() + "." + dateTimePicker3.Value.Month.ToString() + ".01");
                    dateTimePicker4.Value = Convert.ToDateTime(dateTimePicker3.Value.AddMonths(1).AddDays(-1));
                }
                else
                {
                    dat = Convert.ToDateTime(dateTimePicker4.Value.Year.ToString() + "." + dateTimePicker4.Value.Month.ToString() + ".01");
                    dateTimePicker4.Value = Convert.ToDateTime(dat.AddMonths(1).AddDays(-1));
                    if (dateTimePicker3.Value.CompareTo(dateTimePicker4.Value) > 0)
                        dateTimePicker3.Value=Convert.ToDateTime(dateTimePicker4.Value.Year.ToString()+"."+dateTimePicker4.Value.Month.ToString()+".01");
                }
                //dat = Convert.ToDateTime(dateTimePicker4.Value.Year.ToString() + "." + dateTimePicker4.Value.Month.ToString() + ".01");
                //dateTimePicker4.Value=Convert.ToDateTime(dat.AddMonths(1).AddDays(-1));
                //dateSzamla.MinDate = dateTimePicker3.Value;
                //dateSzamla.MaxDate = dateTimePicker4.Value;
                szamlaadatok = Fak.Adatoktolt("C", Fak.Aktintervallum, szamlaadatok,
                                " where VS='" + jel + "'" +
                                " AND DATUM between cast('" + dateTimePicker3.Value.ToShortDateString() + "' as datetime)" +
                                " AND cast('" + dateTimePicker4.Value.ToShortDateString() + "' as datetime)" +
                                " and fizetve = 'N'", false);
                if (dateTimePicker3.Value.Year != dateTimePicker4.Value.Year || dateTimePicker3.Value.Year == dateTimePicker4.Value.Year &&
                    dateTimePicker3.Value.Month != dateTimePicker4.Value.Month)
                    tobbhonap = true;
                else
                    tobbhonap = false;
                if (dataViewSzamla.Count == 0)
                     Szamlaaktgridrowind = -1;
                else
                     Szamlaaktgridrowind = 0;
                Szamla_beallit(Szamlaaktgridrowind);
            }
            igazivaltozas = elozoigazi;
        }

        private void dateSzamla_Changed(object sender, EventArgs e)
        {
            bool elozoigazi = igazivaltozas;
            DateTimePicker kuldo = (DateTimePicker)sender;
            if (igazivaltozas)
            {
                igazivaltozas = false;
                if (kuldo == dateSzamla)
                {
                    dateTimePicker1.MinDate = tol;
                    if (dateTimePicker1.Value.CompareTo(dateSzamla.Value) < 0)
                        dateTimePicker1.Value = dateSzamla.Value;
                    dateTimePicker1.MinDate = dateSzamla.Value;
                }
                dateTeljesit.MinDate = tol;
                if (dateTeljesit.Value.CompareTo(dateTimePicker1.Value) < 0)
                    dateTeljesit.Value = dateTimePicker1.Value;
                dateTeljesit.MinDate = dateTimePicker1.Value;
            }

            igazivaltozas = elozoigazi;
        }
        private void buttonNyomtat_Click(object sender, EventArgs e)
        {
            nyomtat = new PrintForm();
            string[] parNev = { "cim", "DATUM_TOL", "DATUM_IG" };
            string[] parVal = { "", dateTimePicker3.Value.ToShortDateString(), dateTimePicker3.Value.ToShortDateString() };
            string[] parTip = { "string", "string", "string" };

            if (jel == "V")
                parVal[0] = "Vevöi kintlevőség lista";
            else
                parVal[0] = "Szállító tartozások lista";

            DataSet dS = new MainProgramm.virDataSet();
            DataRow r;
            string osszeg = "0";    
            dS.Tables["Kintlevöseg"].Clear();
            for (int i = 0; i < dataViewSzamla.Count; i++)
            {
                Szamlaaktsorindex = SzamlaAdattabla.Rows.IndexOf(dataViewSzamla[i].Row);
                Szamlainfo.Aktsorindex = Szamlaaktsorindex;
                Szamlaaktid = Convert.ToInt32(SzamlaAdattabla.Rows[Szamlaaktsorindex][Szamlaidcol].ToString());
                Szamlainfo.Aktidentity = Szamlaaktid;
                r = dS.Tables["Kintlevöseg"].NewRow();
                r["azonosito"] = dataViewSzamla[i]["azonosito"];
                r["partner"] = dataViewSzamla[i]["pid_k"];
                r["datum_telj"] = dataViewSzamla[i]["datum_telj"];
                r["datum_fiz"] = dataViewSzamla[i]["datum_fiz"];
                r["datum"] = dataViewSzamla[i]["datum"];
                osszeg = "0";
                teteladatok = Fak.Adatoktolt("T", Fak.Aktintervallum, teteladatok, "", false);
                for (int j = 0; j < this.SzamlatetelAdattabla.Rows.Count; j++)
                    osszeg = Convert.ToString(Convert.ToDecimal(osszeg) + Convert.ToDecimal(SzamlatetelAdattabla.Rows[j]["brutto"].ToString()));
                r["osszeg"] = osszeg;
                dS.Tables["Kintlevöseg"].Rows.Add(r);
            }

            nyomtat.PrintParams(parNev, parVal, parTip);
            kintlevösegLista.SetDataSource(dS);
            this.kintlevösegLista.SetParameterValue("cim", parVal[0]);
            this.kintlevösegLista.SetParameterValue("DATUM_TOL", parVal[1]);
            this.kintlevösegLista.SetParameterValue("DATUM_IG", parVal[2]);

            nyomtat.reportSource = kintlevösegLista;
            nyomtat.DoPreview(mainForm.defPageSettings);
        }

        private void Kiegyenlites_Load(object sender, EventArgs e)
        {
            mainForm = (VIR.MainForm)this.ParentForm;
        }
    }
}
