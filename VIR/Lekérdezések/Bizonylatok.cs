using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Könyvtar.ClassGyujtemeny;
using Könyvtar.Printlib;
using TableInfo;
//using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using MainProgramm.Listák;

namespace Lekerdezesek
{
    public partial class Bizonylatok : UserControl
    {
        private VIR.MainForm mainForm;
        private SqlConnection myconn = new SqlConnection();
        private DataSet ds = new DataSet();
        private SqlDataAdapter da;

        private PrintForm nyomtat = new PrintForm();
        private BizonylatokLista bizonylatokLista = new BizonylatokLista();

        private Fak Fak;

        private DataView  viewEredmeny = new DataView();
        private DataTable tableSzamla    = new DataTable();
        private DataTable tableÖsszesen = new DataTable();
        private DataTable tableRendszer = new DataTable();

        private string sel = "";

        private MyTag     szamlatag;
        private MyTag     szamlateteltag;
        private MyTag     penzmozgastag;
        private Tablainfo szamlainfo;
        private Tablainfo szamlatetelinfo;
        private Tablainfo penzmozgasinfo;
        private Tablainfo partnerinfo;
        private Tablainfo partnertetelinfo;
        private string vegzo_sorszam = "";

        private string[] fszids;

        private string folyszla_id;
        private string penztar_id;
        private string partner_id;
        private bool igazivaltozas = false;

        public Bizonylatok(string szoveg, object[] obj)
        {
            MyTag[] myTag = (MyTag[])obj[1];

            szamlatag = myTag[0];
            szamlainfo = szamlatag.AdatTablainfo;
            szamlateteltag = myTag[1];
            szamlatetelinfo = szamlateteltag.AdatTablainfo;
            penzmozgastag = myTag[2];
            penzmozgasinfo = penzmozgastag.AdatTablainfo;
            partnerinfo = myTag[3].AdatTablainfo;
            partnertetelinfo = myTag[4].AdatTablainfo;

            string[] pidt = partnerinfo.FindIdentityArray(new string[] { "SAJAT" }, new string[] { "I" });
            string[] pidN = partnerinfo.FindIdentityArray(new string[] { "SAJAT" }, new string[] { "N" });

            if (pidt == null)
            {
                MessageBox.Show("Nincs 'sajat' jelu partner!");
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

                Fak = myTag[0].Fak;
                //PenzMozgasAdattabla = ((Adattablak)penzmozgasinfo.Initselinfo.Adattablak[penzmozgasinfo.Initselinfo.Aktualadattablaindex]).Adattabla;
                //SzamlaAdattabla = ((Adattablak)szamlainfo.Initselinfo.Adattablak[szamlainfo.Initselinfo.Aktualadattablaindex]).Adattabla;

                Fak.ControlTagokTolt(this, new Control[] { this });
                //szamlainfo.Comboinfoszures(cbBank, fszids);
                szamlainfo.Comboinfoszures(comboSzallito, pidN);

                igazivaltozas = true;
                //cbBank.Text = "";
                //cbPenztar.Text = "";
                comboSzallito.Text = "";
                folyszla_id = "-1";
                penztar_id = "-1";
            }
        }

        private void Penzforgalom_Load(object sender, EventArgs e)
        {
            mainForm = (VIR.MainForm)this.ParentForm;
            myconn = mainForm.MyConn;
            //cbBank.Text = "";
            //cbPenztar.Text = "";
            comboSzallito.Text = "";
        }

        private void rBBank_CheckedChanged(object sender, EventArgs e)
        {
            if (rBBank.Checked)
            {
                //cbBank.Enabled = true;
                //cbPenztar.Enabled = false;
                //cbPenztar.Text = "";
            }
            else
            {
                //cbBank.Enabled = false;
                //cbPenztar.Enabled = true;
                //cbBank.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string datTol = dateTol.Value.ToShortDateString().Substring(0,10);
            string datIg = dateIg.Value.ToShortDateString().Substring(0, 10);

            if (rbVevo.Checked || rbSzallito.Checked || rbPenzm.Checked)
            {
                //if (rBBank.Checked)
                //    folyszla_id = szamlainfo.GetActualCombofileinfo(cbBank);
                //else
                //    penztar_id = szamlainfo.GetActualCombofileinfo(cbPenztar);
                if (comboSzallito.Text != "")
                    partner_id = szamlainfo.GetActualCombofileinfo(comboSzallito);
                if (folyszla_id == "")
                    folyszla_id = "-1";
                if (penztar_id == "")
                    penztar_id = "-1";
                if (partner_id == "")
                    partner_id = "-1";

                tableSzamla.Clear();

                if (rbVevo.Checked)
                {
                    sel = "select a.id, a.azonosito, a.datum, 'Vevö' as jel, sum(b.netto) as netto, sum(b.afa) as afa, sum(b.brutto) as brutto, d.azonosito as partner, a.megjegyzes, " +
                          "0 as fsz_id, a.h_fsz_id, 0 as penztar_id, a.h_penztar_id " +
                          "from szamla a " +
                          "inner join szamla_tetel b on a.id = b.id " +
                          "left outer join partner d on a.pid = d.pid ";
                    //if (rBBank.Checked /*&& cbBank.Text != "" */)
                    //    sel = sel + "left outer join partner_folyosz c on a.h_fsz_id=c.fsz_id ";
                    //else if (rBPenztar.Checked /*&& cbPenztar.Text != ""*/)
                    //    sel = sel + "left outer join kodtab c on a.h_penztar_id=c.sorszam ";
                    sel = sel + "where a.vs='V' " +
                            "and cast(datum as datetime) >= cast('" + datTol + "' as datetime) and cast(datum as datetime) <= cast('" + datIg + "' as datetime) ";
                    if (rBBank.Checked /*&& cbBank.Text != ""*/)
                        sel = sel + "and a.h_fsz_id > 0 ";
                    else if (rBPenztar.Checked /*&& cbPenztar.Text != ""*/)
                        sel = sel + "and a.h_penztar_id > 0 ";
                    if (comboSzallito.Text != "")
                        sel = sel + "and a.pid = " + partner_id + " ";
                    sel = sel + " group by a.id, a.azonosito, a.datum, d.azonosito, a.megjegyzes, a.h_fsz_id, a.h_penztar_id ";
                    da = new SqlDataAdapter(sel, myconn);
                    da.Fill(ds, "tableSzamla");
                }

                if (rbSzallito.Checked)
                {
                    sel = "select a.id, a.azonosito, a.datum, 'Szállító' as jel, sum(b.netto) as netto, sum(b.afa) as afa, sum(b.brutto) as brutto, d.azonosito as partner, a.megjegyzes, " +
                          "0 as fsz_id, a.h_fsz_id, 0 as penztar_id, a.h_penztar_id " +
                          "from szamla a " +
                          "inner join szamla_tetel b on a.id=b.id " +
                          "left outer join partner d on a.pid = d.pid ";
                    //if (rBBank.Checked /*&& cbBank.Text != ""*/)
                    //    sel = sel + "left outer join partner_folyosz c on a.h_fsz_id=c.fsz_id ";
                    //else if (rBPenztar.Checked /*&& cbPenztar.Text != ""*/)
                    //    sel = sel + "left outer join kodtab c on a.h_penztar_id=c.sorszam ";
                    sel = sel + "where a.vs='S' " +
                                "and cast(datum as datetime) >= cast('" + datTol + "' as datetime) and cast(datum as datetime) <= cast('" + datIg + "' as datetime) ";
                    if (rBBank.Checked /*&& cbBank.Text != ""*/)
                        sel = sel + "and a.h_fsz_id > 0 ";
                    else if (rBPenztar.Checked /*&& cbPenztar.Text != ""*/)
                        sel = sel + "and a.h_penztar_id > 0 ";
                    if (comboSzallito.Text != "")
                        sel = sel + "and a.pid = " + partner_id + " ";
                    sel = sel + " group by a.id, a.azonosito, a.datum, d.azonosito, a.megjegyzes, a.h_fsz_id, a.h_penztar_id ";
                    da = new SqlDataAdapter(sel, myconn);
                    da.Fill(ds, "tableSzamla");
                }

                if (rbPenzm.Checked)
                {
                    sel = "select cast(id as char) as azonosito, datum, " +
                          "case when (a.h_fsz_id > 0 or a.h_penztar_id > 0) and (a.fsz_id > 0 or a.penztar_id > 0) then 'Belső-Pénzmozgás' else 'Külső-Pénzmozgás' end as jel, " +
                            "0.00 as netto, 0.00 as afa, ft as brutto, " +
                            "'' as partner, a.megjegyzes, a.fsz_id, a.h_fsz_id, a.penztar_id, a.h_penztar_id " +
                            "from penzmozg a ";
                    sel = sel + "where cast(datum as datetime) >= cast('" + datTol + "' as datetime) and cast(datum as datetime) <= cast('" + datIg + "' as datetime) ";
                    if (rBBank.Checked /*&& cbBank.Text != ""*/)
                        sel = sel + "and (a.h_fsz_id > 0 or a.fsz_id > 0 ) ";
                    else if (rBPenztar.Checked /*&& cbPenztar.Text != ""*/)
                        sel = sel + "and (a.h_penztar_id > 0 or a.penztar_id > 0)";
                    da = new SqlDataAdapter(sel, myconn);
                    da.Fill(ds, "tableSzamla");
                }

                tableSzamla = ds.Tables["tableSzamla"];
                this.viewEredmeny.BeginInit();
                this.viewEredmeny.Table = tableSzamla;
                this.viewEredmeny.EndInit();
                dataGV.DataSource = this.viewEredmeny;

                decimal beN = 0;
                decimal beA = 0;
                decimal beB = 0;
                for (int i = 0; i < tableSzamla.Rows.Count; i++)
                {
                    if (tableSzamla.Rows[i]["jel"].ToString() == "Vevö")
                    {
                        beN = beN + Convert.ToDecimal(tableSzamla.Rows[i]["netto"].ToString());
                        beA = beA + Convert.ToDecimal(tableSzamla.Rows[i]["afa"].ToString());
                        beB = beB + Convert.ToDecimal(tableSzamla.Rows[i]["brutto"].ToString());
                    }
                    if (tableSzamla.Rows[i]["jel"].ToString() == "Szállító")
                    {
                        beN = beN - Convert.ToDecimal(tableSzamla.Rows[i]["netto"].ToString());
                        beA = beA - Convert.ToDecimal(tableSzamla.Rows[i]["afa"].ToString());
                        beB = beB - Convert.ToDecimal(tableSzamla.Rows[i]["brutto"].ToString());
                    }
                    else
                    {
                        if (Convert.ToInt32(tableSzamla.Rows[i]["fsz_id"].ToString()) > 0)
                        {
                            beN = beN - Convert.ToDecimal(tableSzamla.Rows[i]["netto"].ToString());
                            beA = beA - Convert.ToDecimal(tableSzamla.Rows[i]["afa"].ToString());
                            beB = beB - Convert.ToDecimal(tableSzamla.Rows[i]["brutto"].ToString());
                        }
                        if (Convert.ToInt32(tableSzamla.Rows[i]["h_fsz_id"].ToString()) > 0)
                        {
                            beN = beN + Convert.ToDecimal(tableSzamla.Rows[i]["netto"].ToString());
                            beA = beA + Convert.ToDecimal(tableSzamla.Rows[i]["afa"].ToString());
                            beB = beB + Convert.ToDecimal(tableSzamla.Rows[i]["brutto"].ToString());
                        }
                        if (Convert.ToInt32(tableSzamla.Rows[i]["penztar_id"].ToString()) > 0)
                        {
                            beN = beN - Convert.ToDecimal(tableSzamla.Rows[i]["netto"].ToString());
                            beA = beA - Convert.ToDecimal(tableSzamla.Rows[i]["afa"].ToString());
                            beB = beB - Convert.ToDecimal(tableSzamla.Rows[i]["brutto"].ToString());
                        }
                        if (Convert.ToInt32(tableSzamla.Rows[i]["h_penztar_id"].ToString()) > 0)
                        {
                            beN = beN + Convert.ToDecimal(tableSzamla.Rows[i]["netto"].ToString());
                            beA = beA + Convert.ToDecimal(tableSzamla.Rows[i]["afa"].ToString());
                            beB = beB + Convert.ToDecimal(tableSzamla.Rows[i]["brutto"].ToString());
                        }
                    }
                }
                nettoForgalom.Text = string.Format("{0:N}", Convert.ToInt32(beN));
                afaForgalom.Text = string.Format("{0:N}", Convert.ToInt32(beA));
                bruttoForgalom.Text = string.Format("{0:N}", Convert.ToInt32(beB));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (tableSzamla.Rows.Count > 0)
            {
                nyomtat = new PrintForm();
                string[] parNev = { "BSZLA","PENZTAR","DATUM_TOL","DATUM_IG","NETTOSUM","AFASUM","BRUTTOSUM" };
                string[] parVal = { "", "", dateTol.Value.ToShortDateString(), dateIg.Value.ToShortDateString(), nettoForgalom.Text,afaForgalom.Text, bruttoForgalom.Text };
                string[] parTip = { "string", "string", "string", "string", "number", "number", "number" };


                DataSet dS = new MainProgramm.virDataSet();
                DataRow r;
                dS.Tables["Bizonylatok"].Clear();
                for (int i = 0; i < tableSzamla.Rows.Count; i++)
                {
                    r = dS.Tables["Bizonylatok"].NewRow();
                    r["azonosito"] = tableSzamla.Rows[i]["azonosito"];
                    r["datum"] = tableSzamla.Rows[i]["datum"];
                    if (tableSzamla.Rows[i]["jel"].ToString() == "Vevö" || tableSzamla.Rows[i]["jel"].ToString() == "Szállító") 
                        r["jel"] = tableSzamla.Rows[i]["jel"].ToString().Substring(0,1);
                    else
                        r["jel"] = "P"; // Pénzmozgás
                    r["netto"] = Convert.ToDecimal(tableSzamla.Rows[i]["netto"].ToString().Replace('.', ','));
                    r["afa"] = Convert.ToDecimal(tableSzamla.Rows[i]["afa"].ToString().Replace('.',','));
                    r["brutto"] = Convert.ToDecimal(tableSzamla.Rows[i]["brutto"].ToString().Replace('.', ','));
                    if (tableSzamla.Rows[i]["partner"].ToString() == "")
                        r["partner"] = " ";
                    else
                        r["partner"] = tableSzamla.Rows[i]["partner"];
                    r["megjegyzes"] = tableSzamla.Rows[i]["megjegyzes"];
                    dS.Tables["Bizonylatok"].Rows.Add(r);
                }

                nyomtat.PrintParams(parNev, parVal, parTip);
                this.bizonylatokLista.SetDataSource(dS);
                this.bizonylatokLista.SetParameterValue("BSZLA", "");
                this.bizonylatokLista.SetParameterValue("PENZTAR", "");
                this.bizonylatokLista.SetParameterValue("DATUM_TOL", dateTol.Value.ToShortDateString());
                this.bizonylatokLista.SetParameterValue("DATUM_IG", dateIg.Value.ToShortDateString());
                this.bizonylatokLista.SetParameterValue("NETTOSUM", Convert.ToDecimal(nettoForgalom.Text));
                this.bizonylatokLista.SetParameterValue("AFASUM", Convert.ToDecimal(afaForgalom.Text));
                this.bizonylatokLista.SetParameterValue("BRUTTOSUM", Convert.ToDecimal(bruttoForgalom.Text));

                nyomtat.reportSource = bizonylatokLista;
                nyomtat.DoPreview(mainForm.defPageSettings);
            }
        }
    }
}