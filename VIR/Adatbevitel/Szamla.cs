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

        public Szamla(string szoveg, object[] obj)
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
                label18.Visible = false;
                comboSEMA.Visible = false;
                button4.Visible = false;
                ((Cols)Szamlainfo.TablaColumns[Szamlainfo.GetTablaColIndex("FSZ_ID")]).ReadOnly = true;
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
                dateFizetes.Value = dateTimePicker3.Value;
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

//                    ArrayList fszidk = new ArrayList();
 //                   for (int i = 0; i < pidt.Length; i++)
 //                   {
 //                       string[] egyst = folyoszlainfo.FindIdentityArray(new string[] { "PID" }, new string[] { pidt[i] });
//                        for (int j = 0; j < egyst.Length; j++)
 //                           fszidk.Add(egyst[j]);
 //                   }
 //                   fszids = new string[fszidk.Count];
 //                   for (int i = 0; i < fszids.Length; i++)
 //                       fszids[i] = fszidk[i].ToString();

                    Fak.ControlTagokTolt(this,groupBox4);
                    tetelegycont = Szamlatetelinfo.GetEgycontrolinfo(this);
                    Tetelegyallapot = Szamlatetelinfo.GetEgyallapotinfo(this);
                    dataGVSzla.AutoGenerateColumns = false;
                    DataGridViewColumn[] szlagridcols = Szamlainfo.GetGridColumns();
                    dataGVSzla.Columns.AddRange(szlagridcols);
                    dataGVSzlatetel.AutoGenerateColumns = false;
                    szlatetelgridcols = Szamlatetelinfo.GetGridColumns();
                    dataGVSzlatetel.Columns.AddRange(szlatetelgridcols);
                    Szamlaidcol = Szamlainfo.Identitycol;
                    szamlaadatok = Fak.Adatoktolt("C", Fak.Aktintervallum, szamlaadatok, " where VS='" + jel + 
                        "' AND DATUM between cast('" + dateSzamla.MinDate.ToShortDateString()+"' as datetime) AND cast('" + dateSzamla.MaxDate.ToShortDateString()+"' as datetime)",false);
                    SzamlaAdattabla =Szamlainfo.Adattabla;
                    Szamlatetelidcol = Szamlatetelinfo.Identitycol;
                    dataViewSzamla.Table = SzamlaAdattabla;
                    dataViewSzamla.Sort = Szamlainfo.Sort;
                    SzamlaOriginSort = Szamlainfo.Sort;
                    if (dataViewSzamla.Count == 0)
                        Szamlaaktgridrowind = -1;
                    else
                        Szamlaaktgridrowind = 0;
                    dataGVSzla.DataSource = dataViewSzamla;
                    dataViewTetel.Table = SzamlatetelAdattabla;
                    dataViewTetel.Sort = Szamlatetelinfo.Sort;
                    dataGVSzlatetel.DataSource = dataViewTetel;
                    groupBox2.Enabled = false;
                    Szamla_beallit(Szamlaaktgridrowind);

                    da = new SqlDataAdapter("select sorszam, szoveg, kod from kodtab where kodtipus = 'AFA' order by kod ", szamlatag.AdatTablainfo.Adatconn);
                    da.Fill(ds, "afaKulcs");
                    afaKulcs = ds.Tables["afaKulcs"];
                    comboAFA.DataSource = afaKulcs;
                    if (comboAFA.Items.Count > 0) comboAFA.SelectedIndex = 0;
                    comboAFA.DisplayMember = "szoveg";
                    comboAFA.ValueMember = "kod";
                    comboAFA.SelectedIndex = comboAFA.FindString("25%");

                    da = new SqlDataAdapter("select distinct 0, '' as nev from ktgfelosztas ", szamlatag.AdatTablainfo.Adatconn);
                    da.Fill(ds, "sema");
                    da = new SqlDataAdapter("select distinct koltseg_id, k.kod+' '+k.szoveg as nev from ktgfelosztas a, kodtab k where a.koltseg_id = k.sorszam ", szamlatag.AdatTablainfo.Adatconn);
                    da.Fill(ds, "sema");
                    sema = ds.Tables["sema"];
                    comboSEMA.DataSource = sema;
                    if (comboSEMA.Items.Count > 0) comboSEMA.SelectedIndex = 0;
                    comboSEMA.DisplayMember = "nev";
                    comboSEMA.ValueMember = "koltseg_id";

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
            Tetelegyallapot.Modositott = false;
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
                buttonUj.Enabled = true;
                buttonTöröl.Enabled = true;
                panelControl.Visible = false;
            }
            else
            {
                Szamlaaktid = -1;
                Szamlaaktsorindex=-1;
                Szamlainfo.Aktsorindex = -1;
                buttonUj.Enabled = false;
                buttonTöröl.Enabled = false;
                panelControl.Visible = true;
            }
            dateFizetes.MinDate = tol;
            if (dateFizetes.Value.CompareTo(dateSzamla.Value) < 0 || Szamlaaktid == -1)
                dateFizetes.Value = dateSzamla.Value;
            dateFizetes.MinDate = dateSzamla.Value;
            dateTeljesit.MinDate = tol;
            if (dateTeljesit.Value.CompareTo(dateFizetes.Value) < 0 || Szamlaaktid == -1)
                dateTeljesit.Value = dateFizetes.Value;
            dateTeljesit.MinDate = dateFizetes.Value;
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
            if (Szamlaaktid == -1)
                groupBox2.Enabled = true;
            groupBox3.Enabled = false;
            groupBox4.Enabled = false;
            groupBox1.Enabled = true;
            teteladatok=Fak.Adatoktolt("T",Fak.Aktintervallum,teteladatok,"",false);
            if (dataViewTetel.Count == 0)
                Szamlatetelaktgridrowind = -1;
            else
                Szamlatetelaktgridrowind = 0;
            Tetel_beallit(Szamlatetelaktgridrowind);
            RadiobuttonBeallitasok();
            igazivaltozas = true;
        }

        private void Szamla_ok(object sender, EventArgs e)
        {
            if (rBPenztar.Checked)
                checkBox1.Checked = true;

            string hiba = Fak.Hibavizsg(this,Szamlainfo, null);
            if (hiba == "")
            {
                hiba = Validalas(comboSzallito);
                if (hiba == "")
                {
                    buttonUj.Enabled = false;
                    buttonTöröl.Enabled = true;
                    buttonMentes.Enabled = false;
                    groupBox1.Enabled = false;
                    groupBox2.Enabled = false;
                    groupBox3.Enabled = true;
                    groupBox4.Enabled = true;
                    if (Szamlaaktgridrowind == -1)
                    {
                        SzamlaAdattabla = Szamlainfo.Ujsor();
                        Szamlaaktsorindex = SzamlaAdattabla.Rows.Count - 1;
                        Szamlaaktgridrowind = 0;
                        Szamlatetelaktgridrowind = -1;
                    }
                    if (!modositas)
                        Szamlaaktgridrowind = dataViewSzamla.Count + 1;

                    DataRow dr = SzamlaAdattabla.Rows[Szamlaaktsorindex];
                    dr = Szamlainfo.Adatsortolt(Szamlaaktsorindex);
                    dr["VS"] = jel;
                    dataViewSzamla.Table= SzamlaAdattabla;
                    dataViewSzamla.Sort = Szamlainfo.Sort;
                }
            }
            if (rBPenztar.Checked)
            {
                textBox3.ReadOnly = true;
                textBox6.ReadOnly = false;
            }
            else
            {
                textBox6.ReadOnly = true;
                textBox3.ReadOnly = false;
            }

            if (panelControl.Visible 
                && Convert.ToDecimal(controlOsszeg.Text) > 0 
                && comboSEMA.SelectedIndex > 0 
                && comboSEMA.SelectedValue.ToString() != "" 
                && osszeg_felosztasa(sender,e))
            {
            }

        }

        private bool osszeg_felosztasa(object sender, EventArgs e)
        {
            bool ret = false;
            Decimal arany = 100;
            Decimal afa = Convert.ToDecimal(comboAFA.SelectedValue);
            Decimal netto = Convert.ToDecimal(controlOsszeg.Text) / ((afa+100) / 100);
            Decimal resz = 0;

            da = new SqlDataAdapter("select a.*, k.kod + ' ' + k.szoveg as nev " +
                        "from ktgfelosztas a, kodtab k " +
                        "where a.termek_id = k.sorszam and a.koltseg_id = " + comboSEMA.SelectedValue.ToString(), szamlatag.AdatTablainfo.Adatconn);
            da.Fill(ds, "KtgfelosztasAdattabla");
            KtgfelosztasAdattabla = ds.Tables["KtgfelosztasAdattabla"];

            if (KtgfelosztasAdattabla.Rows.Count > 0)
            {
                for (int i = 0; i < KtgfelosztasAdattabla.Rows.Count; i++)
                {
                    if (textBox1.Text == "")
                        textBox2.Text = "Séma:" + comboSEMA.Text;
                    else
                        textBox2.Text = textBox1.Text;
                    comboBox5.SelectedIndex = comboBox5.FindString(KtgfelosztasAdattabla.Rows[i]["nev"].ToString());
                    comboBox1.SelectedIndex = comboAFA.SelectedIndex;
                    arany = Convert.ToDecimal(KtgfelosztasAdattabla.Rows[i]["szazalek"].ToString());
                    resz = netto * arany / 100;
                    //if (i == KtgfelosztasAdattabla.Rows.Count - 1) // utolsó tételre terheljük a kerekítés különbözetet
                    //{
                    //    if (resz + Convert.ToDecimal(textBox4.Text) != netto)
                    //    {
                    //        resz = resz + (netto - resz + Convert.ToDecimal(textBox4.Text));
                    //    }
                    //}
                    textBox3.Text = Convert.ToString(resz);
                    textBox5.Text = Convert.ToString(resz * afa / 100);
                    textBox6.Text = Convert.ToString(Convert.ToDecimal(textBox3.Text) + Convert.ToDecimal(textBox5.Text));
                    Tetel_ok(sender, e);
                }
            }

            return ret;
        }

        private string Validalas(Control cont)
        {
            string hibaszov="";
            string tartal = cont.Text.Trim();
            Taggyart tg = (Taggyart)cont.Tag;
            switch (tg.Valid)
            {
                case 1:
                    if (comboBox1.Text != "" && (!rBPenztar.Checked && textBox3.Text != "" || rBPenztar.Checked && textBox6.Text!=""))
                    {
                        tetelOsszegSzamitas();
                    }
                    break;
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

        private void Tetel_ok(object sender, EventArgs e)
        {
            igazivaltozas = false;
            string hiba = Fak.Hibavizsg(this,Szamlatetelinfo, null);
            if (hiba == "")
            {
                //Egyinputinfo egyinp = (Egyinputinfo)Szamlatetelinfo.Inputinfo[Szamlatetelinfo.GetInputColIndex("AFAKULCS")];
                //decimal afa = Convert.ToDecimal(egyinp.Comboaktfileba);
                //decimal netto = Convert.ToDecimal(textBox3.Text);
                //textBox5.Text = (afa * netto / 100).ToString();
                //textBox6.Text = (netto + afa * netto / 100).ToString();
                tetelOsszegSzamitas();
                Fak.Hibavizsg(this, Szamlatetelinfo, null);
                Lasttetelaktgridindex = Szamlatetelaktgridrowind;
                if (Szamlatetelaktgridrowind == -1)
                {
                    SzamlatetelAdattabla = Szamlatetelinfo.Ujsor();
                    Szamlatetelaktsorindex = SzamlatetelAdattabla.Rows.Count - 1;
                }
                DataRow dr = SzamlatetelAdattabla.Rows[Szamlatetelaktsorindex];
                dr = Szamlatetelinfo.Adatsortolt(Szamlatetelaktsorindex);
                dataViewTetel.Table = SzamlatetelAdattabla;
                dataViewTetel.Sort = Szamlatetelinfo.Sort;
                Summaz();
                //               Partnertetelaktsorindex = PartnertetelAdattabla.Rows.IndexOf(dataViewTetel[Partnertetelaktgridrowind].Row);

                if (Lasttetelaktgridindex == -1)
                {
                    Szamlatetelaktsorindex = -1;
                    Szamlatetelinfo.Aktsorindex = -1;
                    textBox5.Text = "";
                    textBox6.Text = "";
                }
                buttonMentes.Enabled = true;
                Tetelegyallapot.Modositott = true;
                buttonTeteltorol.Enabled = true;
                igazivaltozas = true;
            }
        }

        private void Summaz()
        {
            decimal sumafa = 0;
            decimal sumnetto = 0;
            decimal sumbrutto = 0;
            DataRow dr;
            for (int i = 0; i < dataViewTetel.Count; i++)
            {
                dr = dataViewTetel[i].Row; ;
                sumafa += Convert.ToDecimal(dr["AFA"].ToString());
                sumnetto += Convert.ToDecimal(dr["NETTO"].ToString());
                sumbrutto += Convert.ToDecimal(dr["BRUTTO"].ToString());
            }
            textBox4.Text = sumnetto.ToString();
            textBox4.InsertFormatCharacters();
            textBox7.Text = sumafa.ToString();
            textBox7.InsertFormatCharacters();
            textBox8.Text = sumbrutto.ToString();
            textBox8.InsertFormatCharacters();
        }
  
        private void Uj_szamla(object sender, EventArgs e)
        {
            Szamla_beallit(-1);
        }

        private void Szamla_torol(object sender, EventArgs e)
        {
            dataViewTetel.RowStateFilter=DataViewRowState.Added|DataViewRowState.Deleted|DataViewRowState.ModifiedCurrent|DataViewRowState.Unchanged;
            for (int i = 0; i < dataViewTetel.Count; i++)
            {
                DataRow dr = dataViewTetel[i].Row;
                SzamlatetelAdattabla = Szamlatetelinfo.Adatsortorol(SzamlatetelAdattabla.Rows.IndexOf(dr));
            }
            dataViewTetel.RowStateFilter = DataViewRowState.Added | DataViewRowState.ModifiedCurrent | DataViewRowState.Unchanged;

            for (int i = 0; i < dataGVSzla.Rows.Count; i++)
            {
                if (dataGVSzla.Rows[i].Selected)
                {
                    Szamlaaktgridrowind = i;
                    dataGVSzla.Rows[i].Selected = false;
                    Szamlaaktsorindex = SzamlaAdattabla.Rows.IndexOf(dataViewSzamla[Szamlaaktgridrowind].Row);
                    break;
                }
            }
            SzamlaAdattabla = Szamlainfo.Adatsortorol(Szamlaaktsorindex);
            if (dataViewSzamla.Count == 0)
                Szamla_beallit(-1);
            else
            {
                if (Szamlaaktgridrowind > dataViewSzamla.Count - 1)
                    Szamlaaktgridrowind--;
                dataGVSzla.Rows[Szamlaaktgridrowind].Selected = true;
                Szamlaaktsorindex = SzamlaAdattabla.Rows.IndexOf(dataViewSzamla[Szamlaaktgridrowind].Row);
                Szamla_beallit(Szamlaaktgridrowind);
            }
            buttonMentes.Enabled = true;
            Szamlaegyallapot.Modositott = true;
            Tetelegyallapot.Modositott = true;
        }

        private void Mentes(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(textBox8.Text) != Convert.ToDecimal(controlOsszeg.Text))
            {
                MessageBox.Show("Még van maradék vagy túl sok a tételek összege!", "", MessageBoxButtons.OKCancel);
            }
            else
            {
                dataViewSzamla.Sort = SzamlaOriginSort;
                ArrayList list = new ArrayList();
                list.Add(Szamlainfo);
                list.Add(Szamlatetelinfo);
                igazivaltozas = false;
                if (dataViewSzamla.Count > 0 && dataViewSzamla[0].Row.RowState == DataRowState.Added)
                    Szamlaaktgridrowind = dataViewSzamla.Count - 1;
                if (!Fak.UpdateTransaction(list))
                {
                    this.Enabled = true;
                    this.Visible = false;
                    modositas = false;
                }
                if (this.Visible)
                {
                    //                dataViewSzamla.Table = SzamlaAdattabla;
                    //                dataViewSzamla.Sort = Szamlainfo.Sort;
                    //                dataViewTetel.Table = SzamlatetelAdattabla;
                    //                dataViewTetel.Sort = Szamlatetelinfo.Sort;
                    //                Szamlatetelinfo.AktCombotolt("KONTID", konttag);
                    if (dataViewSzamla.Count == 0)
                        Szamlaaktgridrowind = -1;
                    //else
                    //Szamlaaktgridrowind = 0;
                    Szamla_beallit(Szamlaaktgridrowind);
                    if (Szamlaaktgridrowind != -1)
                        dataGVSzla.FirstDisplayedScrollingRowIndex = Szamlaaktgridrowind;
                    buttonMentes.Enabled = false;
                    Szamlaegyallapot.Modositott = false;
                    Tetelegyallapot.Modositott = false;
                    Szamlaegyallapot.Mentett = true;
                    Tetelegyallapot.Mentett = true;
                }
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
                Tetelegyallapot.Modositott = false;
                this.Visible = false;
            }
        }

        private void Tetel_beallit(int gridindex)
        {
            igazivaltozas = false;
            Szamlatetelaktgridrowind = gridindex;
            if (Szamlatetelaktgridrowind != -1)
            {
                Szamlatetelaktsorindex = SzamlatetelAdattabla.Rows.IndexOf(dataViewTetel[Szamlatetelaktgridrowind].Row);
                if (SzamlatetelAdattabla.Rows[Szamlatetelaktsorindex].RowState != DataRowState.Added)
                    Szamlatetelaktid = Convert.ToInt32(SzamlatetelAdattabla.Rows[Szamlatetelaktsorindex][Szamlatetelidcol].ToString());
                else
                    Szamlatetelaktid = -1;
                buttonUjtetel.Enabled = true;
            }
            else
            {
                Szamlatetelaktid = -1;
                Szamlatetelaktsorindex = -1;
                buttonUjtetel.Enabled = false;
            }
            Lasttetelaktgridindex = gridindex;
            Szamlatetelinfo.Aktsorindex = Szamlatetelaktsorindex;
            if (dataViewTetel.Count == 0)
                buttonTeteltorol.Enabled = false;
            else
                buttonTeteltorol.Enabled = true;
            Summaz();
            igazivaltozas = true;
        }

        private void Uj_tetel(object sender, EventArgs e)
        {
            Tetel_beallit(-1);
            groupBox1.Enabled = false;
            groupBox2.Enabled = false;
            buttonUj.Enabled = false;
            buttonTöröl.Enabled = false;
            if (rBPenztar.Checked)
            {
                textBox3.ReadOnly = true;
                textBox6.ReadOnly = false;
            }
            else
            {
                textBox6.ReadOnly = true;
                textBox3.ReadOnly = false;
            }
        }

        private void Tetel_torol(object sender, EventArgs e)
        {
            if (dataViewTetel.Count > 0)
            {
                buttonUj.Enabled = false;
                buttonTöröl.Enabled = false;
                for (int i = 0; i < dataViewTetel.Count; i++)
                {
                    if (dataGVSzlatetel.Rows[i].Selected)
                    {
                        Szamlatetelaktgridrowind = i;
                        break;
                    }
                }
                Szamlatetelaktsorindex = SzamlatetelAdattabla.Rows.IndexOf(dataViewTetel[Szamlatetelaktgridrowind].Row);
                SzamlatetelAdattabla = Szamlatetelinfo.Adatsortorol(Szamlatetelaktsorindex);
                dataViewTetel.Table = SzamlatetelAdattabla;
                Summaz();
                if (dataViewTetel.Count == 0)
                {
                    Tetel_beallit(-1);
                    buttonMentes.Enabled = false;
                }
                else
                {
                    if (Szamlatetelaktgridrowind > dataViewTetel.Count - 1)
                        Szamlatetelaktgridrowind--;
                    if (Szamlatetelaktgridrowind != -1)
                        dataGVSzlatetel.Rows[Szamlatetelaktgridrowind].Selected = true;
                    Tetel_beallit(Szamlatetelaktgridrowind);
                }
                Tetelegyallapot.Modositott = true;
            }
        }

        private void Elem_Validated(object sender, EventArgs e)
        {
            //if (igazivaltozas && this.Focused)
            if (igazivaltozas)
            {
                Taggyart tg = (Taggyart)((Control)sender).Tag;
                string hibaszov = Fak.Hibavizsg(this, tg.Tabinfo, (Control)sender);
                if (hibaszov == "")
                    Validalas((Control)sender);
                tetelOsszegSzamitas();
            }
        }

        private void comboSzallitoTextChanged(object sender, EventArgs e)
        {
            if (comboSzallito.Text != "" && igazivaltozas && comboBox2.Visible)
            {
                igazivaltozas = false;
                string pid = Szamlainfo.GetActualCombofileinfo(comboSzallito);
                if (pid != "")
                {
                    partnerinfo.Aktidentity = Convert.ToInt32(pid);
                    string[] fszid = folyoszlainfo.FindIdentityArray(new string[] { "PID" }, new string[] { pid });
                    if (fszid != null)
                        Szamlainfo.Comboinfoszures(comboBox2, fszid);
                    igazivaltozas = true;
                }
            }
        }

        private void dataGVSzla_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Clicks == 1 )
            {
                groupBox2.Enabled = false;
                if (e.RowIndex != -1)
                    Szamla_beallit(e.RowIndex);
            }
        }

        private void dataGVSzla_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)   
            {
                modositas = true;
                //                Partneraktsorindex = PartnerAdattabla.Rows.IndexOf(dataViewPartner[Partneraktgridrowind].Row);
                Szamla_beallit(e.RowIndex);
                if (!tobbhonap)
                    groupBox2.Enabled = true;
            }
        }

        private void dataGVSzlatetel_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                Szamlatetelaktgridrowind = e.RowIndex;
                //                Partnertetelaktsorindex = PartnertetelAdattabla.Rows.IndexOf(dataViewTetel[e.RowIndex].Row);
                Tetel_beallit(e.RowIndex);
            }
        }

        private void Elolrol(object sender, EventArgs e)
        {
            igazivaltozas = false;
            SzamlaAdattabla = Fak.ForceAdattolt(Szamlainfo);
            SzamlatetelAdattabla = Fak.ForceAdattolt(Szamlatetelinfo);
            Rectangle rc = dataGVSzlatetel.GetColumnDisplayRectangle(5, false);

   //         Szamlatetelinfo.AktCombotolt("KONTID", konttag);
   //         dataViewSzamla.Table = SzamlaAdattabla;
   //         dataViewSzamla.Sort = Szamlainfo.Sort;
   //         dataViewTetel.Table = SzamlatetelAdattabla;
   //         dataViewTetel.Sort = Szamlatetelinfo.Sort;
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
 //                   dat = Convert.ToDateTime(dateTimePicker3.Value.Year.ToString() + "." + dateTimePicker3.Value.Month.ToString() + ".01");
                    dateTimePicker4.Value = Convert.ToDateTime(dateTimePicker3.Value.AddMonths(1).AddDays(-1));
                }
                else
                {
                    dat = Convert.ToDateTime(dateTimePicker4.Value.Year.ToString() + "." + dateTimePicker4.Value.Month.ToString() + ".01");
                    dateTimePicker4.Value = Convert.ToDateTime(dat.AddMonths(1).AddDays(-1));
                    if (dateTimePicker3.Value.CompareTo(dateTimePicker4.Value) > 0)
                        dateTimePicker3.Value=Convert.ToDateTime(dateTimePicker4.Value.Year.ToString()+"."+dateTimePicker4.Value.Month.ToString()+".01");
                }
                dat = Convert.ToDateTime(dateTimePicker4.Value.Year.ToString() + "." + dateTimePicker4.Value.Month.ToString() + ".01");
                dateTimePicker4.Value=Convert.ToDateTime(dat.AddMonths(1).AddDays(-1));
                szamlaadatok = Fak.Adatoktolt("C", Fak.Aktintervallum, szamlaadatok, " where VS='" + jel +
                        "' AND DATUM between cast('" + dateTimePicker3.Value.ToShortDateString() + "' as datetime) AND cast('" + dateTimePicker4.Value.ToShortDateString() + "' as datetime)", false);
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
            //DateTimePicker kuldo = (DateTimePicker)sender;
            //if (igazivaltozas)
            //{
            //    igazivaltozas = false;
            //    if (kuldo == dateSzamla)
            //    {
            //        dateFizetes.MinDate = tol;
            //        if (dateFizetes.Value.CompareTo(dateSzamla.Value) < 0)
            //            dateFizetes.Value = dateSzamla.Value;
            //        //dateFizetes.MinDate = dateSzamla.Value;
            //    }
            //    dateTeljesit.MinDate = tol;
            //    if (dateTeljesit.Value.CompareTo(dateFizetes.Value) < 0)
            //        dateTeljesit.Value = dateFizetes.Value;
            //    //dateTeljesit.MinDate = dateFizetes.Value;
            //}

            igazivaltozas = elozoigazi;
        }

        private void Szamla_Load(object sender, EventArgs e)
        {
            Rectangle nettorec = new Rectangle();
            Rectangle afarec = new Rectangle();
            Rectangle bruttorec = new Rectangle();
            for(int i=0;i<szlatetelgridcols.Length;i++)
            {
                DataGridViewColumn egycol=szlatetelgridcols[i];
                switch (egycol.DataPropertyName)
                {
                    case "NETTO":
                        nettorec=dataGVSzlatetel.GetColumnDisplayRectangle(i,false);
                        break;
                    case "AFA":
                        afarec=dataGVSzlatetel.GetColumnDisplayRectangle(i,false);
                        break;
                    case "BRUTTO":
                        bruttorec=dataGVSzlatetel.GetColumnDisplayRectangle(i,false);
                        break;
                }
            }
            textBox4.Bounds = new Rectangle(nettorec.X+dataGVSzlatetel.Location.X,textBox4.Location.Y,nettorec.Width,textBox4.Height);
            textBox7.Bounds = new Rectangle(afarec.X + dataGVSzlatetel.Location.X, textBox7.Location.Y, afarec.Width, textBox7.Height);
            textBox8.Bounds = new Rectangle(bruttorec.X + dataGVSzlatetel.Location.X, textBox8.Location.Y, bruttorec.Width, textBox8.Height);
            textBox3.Bounds = new Rectangle(bruttorec.X + dataGVSzlatetel.Location.X, textBox3.Location.Y, bruttorec.Width, textBox3.Height);
            textBox5.Bounds = new Rectangle(bruttorec.X + dataGVSzlatetel.Location.X, textBox5.Location.Y, bruttorec.Width, textBox5.Height);
            textBox6.Bounds = new Rectangle(bruttorec.X + dataGVSzlatetel.Location.X, textBox6.Location.Y, bruttorec.Width, textBox6.Height);
            //label7.Bounds = new Rectangle(bruttorec.X + dataGVSzlatetel.Location.X, label7.Location.Y, bruttorec.Width, label7.Height);
        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            tetelOsszegSzamitas();
        }

        private void tetelOsszegSzamitas()
        {
            Egyinputinfo egyinp = (Egyinputinfo)Szamlatetelinfo.Inputinfo[Szamlatetelinfo.GetInputColIndex("AFAKULCS")];
            decimal afa = Convert.ToDecimal(egyinp.Comboaktfileba);
            decimal netto;
            decimal brutto;
            decimal afaFt;

            if (rBPenztar.Checked)
            {
                brutto = Convert.ToDecimal(textBox6.Text.PadRight(1,'0'));
                netto = brutto / (1 + afa / 100);
                netto = Decimal.Round(netto, 2);
                textBox3.Text = netto.ToString();
                afaFt = brutto - netto;
                textBox5.Text = afaFt.ToString();
            }
            else
            {
                netto = Convert.ToDecimal(textBox3.Text.PadRight(1, '0'));
                textBox5.Text = (afa * netto / 100).ToString();
                textBox6.Text = (netto + afa * netto / 100).ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (PartnerMenetKozben == null)
                PartnerMenetKozben = new PartnerMenetKozben(Szamlainfo, partnerinfo, comboSzallito, Fak);
            else
                PartnerMenetKozben.PartnerMenetKozbenInit();
            if (PartnerMenetKozben.ShowDialog() == DialogResult.OK)
            {
            }
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Text == "0")
                textBox3.Text = "";
        }

        private void textBox6_Enter(object sender, EventArgs e)
        {
            if (textBox6.Text == "0")
                textBox6.Text = "";
        }

        private void DatumSzerint_Click(object sender, EventArgs e)
        {
            dataViewSzamla.Sort = "DATUM";
            Szamla_beallit(Szamlaaktgridrowind);
        }

        private void PartnerSzerint_Click(object sender, EventArgs e)
        {
            dataViewSzamla.Sort = "PID_K";
            Szamla_beallit(Szamlaaktgridrowind);
        }

        private void FelvitelSzerint_Click(object sender, EventArgs e)
        {
            dataViewSzamla.Sort = "ID";
            Szamla_beallit(Szamlaaktgridrowind);
        }

        private void comboAFA_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = comboAFA.SelectedIndex;
        }

        private void comboSEMA_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (comboSEMA.SelectedValue.ToString() != "")
            {
                SemaMegtekint = new SemaMegtekint(Fak, comboSEMA.SelectedValue.ToString(), szamlatag.AdatTablainfo.Adatconn);
                if (SemaMegtekint.ShowDialog() == DialogResult.OK)
                {
                }
            }
        }

        private void controlOsszeg_Leave(object sender, EventArgs e)
        {
            if (controlOsszeg.Text != "")
               controlOsszeg.Text = string.Format("{0:N}", Convert.ToInt32(controlOsszeg.Text));
        }

    }
}
