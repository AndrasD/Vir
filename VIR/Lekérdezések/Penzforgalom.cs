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
    public partial class Penzforgalom : UserControl
    {
        private VIR.MainForm mainForm;
        private SqlConnection myconn = new SqlConnection();
        private DataSet ds = new DataSet();
        private SqlDataAdapter da;

        private PrintForm nyomtat = new PrintForm();
        private PenzForgalomLista penzforgalomLista = new PenzForgalomLista();

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

        public Penzforgalom(string szoveg, object[] obj)
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
                szamlainfo.Comboinfoszures(cbBank, fszids);
                szamlainfo.Comboinfoszures(comboSzallito, pidN);

                igazivaltozas = true;
                cbBank.Text = "";
                cbPenztar.Text = "";
                comboSzallito.Text = "";
                folyszla_id = "-1";
                penztar_id = "-1";
            }
        }

        private void Penzforgalom_Load(object sender, EventArgs e)
        {
            mainForm = (VIR.MainForm)this.ParentForm;
            myconn = mainForm.MyConn;
            cbBank.Text = "";
            cbPenztar.Text = "";
            comboSzallito.Text = "";
        }

        private void rBBank_CheckedChanged(object sender, EventArgs e)
        {
            if (rBBank.Checked)
            {
                cbBank.Enabled = true;
                cbPenztar.Enabled = false;
                cbPenztar.Text = "";
            }
            else
            {
                cbBank.Enabled = false;
                cbPenztar.Enabled = true;
                cbBank.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string datTol = dateTol.Value.ToShortDateString().Substring(0,10);
            string datIg = dateIg.Value.ToShortDateString().Substring(0, 10);

            da = new SqlDataAdapter("select * from rendszer", myconn);
            da.Fill(ds, "tableRendszer");
            tableRendszer = ds.Tables["tableRendszer"];
            if (tableRendszer.Rows.Count > 0)
                vegzo_sorszam = tableRendszer.Rows[0]["vegzo_sorszam"].ToString();

            if (rBBank.Checked)
                folyszla_id = szamlainfo.GetActualCombofileinfo(cbBank);
            else
                penztar_id = szamlainfo.GetActualCombofileinfo(cbPenztar);
            if (comboSzallito.Text != "")
                partner_id = szamlainfo.GetActualCombofileinfo(comboSzallito);
            if (folyszla_id == "")
                folyszla_id = "-1";
            if (penztar_id == "")
                penztar_id = "-1";
            if (partner_id == "")
                partner_id = "-1";

            tableSzamla.Clear();

            sel = "select a.id, a.azonosito, a.datum, 'Vevö' as jel, sum(b.brutto) as brutton, 0.00 as bruttoc, d.azonosito as partner, a.megjegyzes " +
                  "from szamla a " +
                  "inner join szamla_tetel b on a.id = b.id " +
                  "left outer join partner d on a.pid = d.pid ";
            if (rBBank.Checked && cbBank.Text != "")
                sel=sel+"left outer join partner_folyosz c on a.h_fsz_id=c.fsz_id ";
            else if (rBPenztar.Checked && cbPenztar.Text != "")
                sel=sel+"left outer join kodtab c on a.h_penztar_id=c.sorszam ";
            sel=sel+"where a.vs='V' " +
                    "and cast(datum as datetime) >= cast('" + datTol + "' as datetime) and cast(datum as datetime) <= cast('" + datIg + "' as datetime) ";
            if (rBBank.Checked && cbBank.Text != "")
                sel = sel + "and a.h_fsz_id = " + folyszla_id + " ";
            else if (rBPenztar.Checked && cbPenztar.Text != "")
                sel=sel+"and a.h_penztar_id = " + penztar_id + " ";
            if (comboSzallito.Text != "")
                sel = sel + "and a.pid = " + partner_id + " ";
            sel = sel + " group by a.id, a.azonosito, a.datum, d.azonosito, a.megjegyzes";
            da = new SqlDataAdapter(sel, myconn);
            da.Fill(ds, "tableSzamla");

            sel = "select a.id, a.azonosito, a.datum, 'Szállító' as jel, 0.00 as brutton, sum(b.brutto) as bruttoc, d.azonosito as partner, a.megjegyzes " +
                        "from szamla a " +
                        "inner join szamla_tetel b on a.id=b.id " +
                        "left outer join partner d on a.pid = d.pid ";
            if (rBBank.Checked && cbBank.Text != "")
                sel = sel + "left outer join partner_folyosz c on a.h_fsz_id=c.fsz_id ";
            else if (rBPenztar.Checked && cbPenztar.Text != "")
                sel = sel + "left outer join kodtab c on a.h_penztar_id=c.sorszam ";
            sel = sel + "where a.vs='S' " +
                        "and cast(datum as datetime) >= cast('" + datTol + "' as datetime) and cast(datum as datetime) <= cast('" + datIg + "' as datetime) ";
            if (rBBank.Checked && cbBank.Text != "")
                sel = sel + "and a.h_fsz_id = " + folyszla_id + " ";
            else if (rBPenztar.Checked && cbPenztar.Text != "")
                sel = sel + "and a.h_penztar_id = " + penztar_id + " ";
            if (comboSzallito.Text != "")
                sel = sel + "and a.pid = " + partner_id + " ";
            sel = sel + " group by a.id, a.azonosito, a.datum, d.azonosito, a.megjegyzes";
            da = new SqlDataAdapter(sel, myconn);
            da.Fill(ds, "tableSzamla");

            sel = "select cast(id as char) as azonosito, datum, " +
                  "case when (a.h_fsz_id > 0 or a.h_penztar_id > 0) and (a.fsz_id > 0 or a.penztar_id > 0) then 'Belső-Pénzmozgás' else 'Külső-Pénzmozgás' end as jel, " +
                    "case when a.h_fsz_id > 0 or a.h_penztar_id > 0 then ft else 0.00 end as brutton, "+
                    "case when a.fsz_id > 0 or a.penztar_id > 0 then ft else 0.00 end as bruttoc, '' as partner, a.megjegyzes " +
                    "from penzmozg a ";
            sel = sel + "where cast(datum as datetime) >= cast('" + datTol + "' as datetime) and cast(datum as datetime) <= cast('" + datIg + "' as datetime) ";
            if (rBBank.Checked && cbBank.Text != "")
                sel = sel + "and (a.h_fsz_id = " + folyszla_id + " or a.fsz_id = " + folyszla_id +")";
            else if (rBPenztar.Checked && cbPenztar.Text != "")
                sel = sel + "and (a.h_penztar_id = " + penztar_id + " or a.penztar_id = " + penztar_id + ")";
            da = new SqlDataAdapter(sel, myconn);
            da.Fill(ds, "tableSzamla");

            tableSzamla = ds.Tables["tableSzamla"];
            this.viewEredmeny.BeginInit();
            this.viewEredmeny.Table = tableSzamla;
            this.viewEredmeny.EndInit();
            dataGV.DataSource = this.viewEredmeny;

            tableÖsszesen.Clear();
            //sel = "select a.azonosito, b.brutto as brutton, 0.00 as bruttoc, a.datum  " +
            //      "from szamla a " +
            //      "inner join szamla_tetel b on a.id=b.id ";
            sel = "select sum(b.brutto) as brutton, 0.00 as bruttoc, a.datum from szamla a inner join szamla_tetel b on a.id = b.id ";
            sel = sel + "where a.vs='V'";
            if (rBBank.Checked && cbBank.Text != "")
                sel = sel + " and a.h_fsz_id = " + folyszla_id + " ";
            else if (rBPenztar.Checked && cbPenztar.Text != "")
                sel = sel + " and a.h_penztar_id = " + penztar_id + " ";
            sel = sel + " and cast(datum as datetime) <= cast('" + datIg + "' as datetime) " +
                        " group by a.datum ";
            da = new SqlDataAdapter(sel, myconn);
            da.Fill(ds, "tableÖsszesen");

            //sel = "select a.azonosito, 0.00 as brutton, b.brutto as bruttoc, a.datum " +
            //      "from szamla a " +
            //      "inner join szamla_tetel b on a.id=b.id ";
            sel = "select 0.00 as brutton, sum(b.brutto) as bruttoc, a.datum from szamla a inner join szamla_tetel b on a.id = b.id ";
            sel = sel + "where a.vs='S'";
            if (rBBank.Checked && cbBank.Text != "")
                sel = sel + " and a.h_fsz_id = " + folyszla_id + " ";
            else if (rBPenztar.Checked && cbPenztar.Text != "")
                sel = sel + " and a.h_penztar_id = " + penztar_id + " ";
            sel = sel + " and cast(datum as datetime) <= cast('" + datIg + "' as datetime) " +
                        " group by a.datum ";
            da = new SqlDataAdapter(sel, myconn);
            da.Fill(ds, "tableÖsszesen");

            //sel = "select cast(id as char), ft as brutton, 0.00 as bruttoc, a.datum " +
            //      "from penzmozg a " +
            sel = "select case when a.h_fsz_id > 0 or a.h_penztar_id > 0 then ft else 0.00 end as brutton, " +
                  "       case when a.fsz_id > 0 or a.penztar_id > 0 then ft else 0.00 end as bruttoc, datum " +
                  " from penzmozg a " +
                  " where cast(datum as datetime) <= cast('" + datIg + "' as datetime) ";
            if (rBBank.Checked && cbBank.Text != "")
                sel = sel + "and (a.h_fsz_id = " + folyszla_id + " or a.fsz_id = " + folyszla_id + ")";
            else if (rBPenztar.Checked && cbPenztar.Text != "")
                sel = sel + "and (a.h_penztar_id = " + penztar_id + " or a.penztar_id = " + penztar_id + ")";
            da = new SqlDataAdapter(sel, myconn);
            da.Fill(ds, "tableÖsszesen");

            tableÖsszesen = ds.Tables["tableÖsszesen"];

            decimal be = 0;
            decimal ki = 0;
            decimal beid = 0;
            decimal kiid = 0;
            DateTime dt1;
            DateTime dt2;
            for (int i = 0; i < tableÖsszesen.Rows.Count; i++)
            {
                beid = beid + Convert.ToDecimal(tableÖsszesen.Rows[i]["brutton"].ToString());
                kiid = kiid + Convert.ToDecimal(tableÖsszesen.Rows[i]["bruttoc"].ToString());
                dt1 = Convert.ToDateTime(Convert.ToDateTime(tableÖsszesen.Rows[i]["datum"].ToString()).ToShortDateString());
                dt2 = Convert.ToDateTime(dateTol.Value.ToShortDateString());
                if (dt1.CompareTo(dt2) < 0)
                {
                    be = be + Convert.ToDecimal(tableÖsszesen.Rows[i]["brutton"].ToString());
                    ki = ki + Convert.ToDecimal(tableÖsszesen.Rows[i]["bruttoc"].ToString());
                }
            }
            osszesen.Text = string.Format("{0:N}", Convert.ToInt32(be - ki));
            idoszaki.Text = string.Format("{0:N}", Convert.ToInt32(beid - kiid));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (tableSzamla.Rows.Count > 0)
            {
                nyomtat = new PrintForm();
                string[] parNev = { "BSZLA","PENZTAR","DATUM_TOL","DATUM_IG","OSSZESEN" };
                string[] parVal = { cbBank.Text, cbPenztar.Text, dateTol.Value.ToShortDateString(), dateIg.Value.ToShortDateString(), osszesen.Text };
                string[] parTip = { "string", "string", "string", "string", "number" };


                DataSet dS = new MainProgramm.virDataSet();
                DataRow r;
                dS.Tables["PenzForgalom"].Clear();
                for (int i = 0; i < tableSzamla.Rows.Count; i++)
                {
                    r = dS.Tables["PenzForgalom"].NewRow();
                    r["azonosito"] = tableSzamla.Rows[i]["azonosito"];
                    r["datum"] = tableSzamla.Rows[i]["datum"];
                    r["jel"] = tableSzamla.Rows[i]["jel"];
                    r["brutton"] = Convert.ToDecimal(tableSzamla.Rows[i]["brutton"].ToString().Replace('.',','));
                    r["bruttoc"] = Convert.ToDecimal(tableSzamla.Rows[i]["bruttoc"].ToString().Replace('.',','));
                    if (tableSzamla.Rows[i]["partner"].ToString() == "")
                        r["partner"] = " ";
                    else
                        r["partner"] = tableSzamla.Rows[i]["partner"];
                    r["megjegyzes"] = tableSzamla.Rows[i]["megjegyzes"];
                    dS.Tables["PenzForgalom"].Rows.Add(r);
                }

                nyomtat.PrintParams(parNev, parVal, parTip);
                this.penzforgalomLista.SetDataSource(dS);
                this.penzforgalomLista.SetParameterValue("BSZLA", cbBank.Text);
                this.penzforgalomLista.SetParameterValue("PENZTAR", cbPenztar.Text);
                this.penzforgalomLista.SetParameterValue("DATUM_TOL", dateTol.Value.ToShortDateString());
                this.penzforgalomLista.SetParameterValue("DATUM_IG", dateIg.Value.ToShortDateString());
                this.penzforgalomLista.SetParameterValue("OSSZESEN", Convert.ToDecimal(osszesen.Text));

                nyomtat.reportSource = penzforgalomLista;
                nyomtat.DoPreview(mainForm.defPageSettings);
            }
        }
    }
}