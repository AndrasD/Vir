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
using System.Data.SqlClient;
using MainProgramm.Listák;

namespace Lekerdezesek
{
    public partial class AfaLista : UserControl
    {
        private VIR.MainForm mainForm;
        private SqlConnection myconn = new SqlConnection();
        private DataSet ds = new DataSet();
        private SqlDataAdapter da;

        private PrintForm nyomtat = new PrintForm();
        private AfaListaLista afaListaLista = new AfaListaLista();

        private Fak Fak;

        private DataView  viewEredmeny = new DataView();
        private DataTable tableSzamla    = new DataTable();
        private DataTable tableÖsszesen = new DataTable();
        private string sel = "";

        private MyTag     szamlatag;
        private MyTag     szamlateteltag;
        private MyTag     penzmozgastag;
        private Tablainfo szamlainfo;
        private Tablainfo szamlatetelinfo;
        private Tablainfo penzmozgasinfo;
        private Tablainfo partnerinfo;
        private Tablainfo partnertetelinfo;

        private string[] fszids;

        public AfaLista(string szoveg, object[] obj)
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

            }
        }

        private void AfaLista_Load(object sender, EventArgs e)
        {
            mainForm = (VIR.MainForm)this.ParentForm;
            myconn = mainForm.MyConn;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string datTol = dateTol.Value.ToShortDateString().Substring(0, 10);
            string datIg = dateIg.Value.ToShortDateString().Substring(0, 10);

            tableSzamla.Clear();

            if (radioVevo.Checked)
            {
                sel = "select a.azonosito, a.datum, a.datum_telj, a.datum_fiz, 'Vevö' as jel, b.brutto as osszeg, d.azonosito as partner, a.megjegyzes, b.afa, b.afakulcs " +
                      "from szamla a " +
                      "inner join szamla_tetel b on a.id=b.id " +
                      "left outer join partner d on a.pid = d.pid ";
                sel = sel + "where a.vs='V' " +
                        "and cast(datum_telj as datetime) >= cast('" + datTol + "' as datetime) and cast(datum_telj as datetime) <= cast('" + datIg + "' as datetime) ";
                da = new SqlDataAdapter(sel, myconn);
                da.Fill(ds, "tableSzamla");
            }

            if (radioSzallito.Checked)
            {
                sel = "select a.azonosito, a.datum, a.datum_telj, a.datum_fiz, 'Szállító' as jel, b.brutto as osszeg, d.azonosito as partner, a.megjegyzes, b.afa, b.afakulcs  " +
                      "from szamla a " +
                      "inner join szamla_tetel b on a.id=b.id " +
                      "left outer join partner d on a.pid = d.pid ";
                sel = sel + "where a.vs='S' " +
                            "and cast(datum_telj as datetime) >= cast('" + datTol + "' as datetime) and cast(datum_telj as datetime) <= cast('" + datIg + "' as datetime) ";
                da = new SqlDataAdapter(sel, myconn);
                da.Fill(ds, "tableSzamla");
            }
            //sel = "select cast(id as char) as azonosito, datum, 'Pénzmozgás' as jel, " +
            //      "case when a.h_fsz_id = " + folyszla_id + " or a.h_penztar_id = " + penztar_id + " then ft else 0.00 end as brutton, " +
            //      "case when a.fsz_id = " + folyszla_id + " or a.penztar_id = " + penztar_id + " then ft else 0.00 end as bruttoc, '' as partner, a.megjegyzes " +
            //      "from penzmozg a ";
            //sel = sel + "where cast(datum as datetime) >= cast('" + datTol + "' as datetime) and cast(datum as datetime) <= cast('" + datIg + "' as datetime) ";
            //da = new SqlDataAdapter(sel, myconn);
            //da.Fill(ds, "tableSzamla");

            tableSzamla = ds.Tables["tableSzamla"];
            this.viewEredmeny.BeginInit();
            this.viewEredmeny.Table = tableSzamla;
            this.viewEredmeny.EndInit();
            dataGV.DataSource = this.viewEredmeny;

            //tableÖsszesen.Clear();
            //sel = "select a.azonosito, b.brutto as brutton, 0.00 as bruttoc " +
            //      "from szamla a " +
            //      "inner join szamla_tetel b on a.id=b.id ";
            //sel = sel + "where a.vs='V' ";
            //if (rBBank.Checked && cbBank.Text != "")
            //    sel = sel + "and a.h_fsz_id = " + folyszla_id + " ";
            //else if (rBPenztar.Checked && cbPenztar.Text != "")
            //    sel = sel + "and a.h_penztar_id = " + penztar_id + " ";
            //da = new SqlDataAdapter(sel, myconn);
            //da.Fill(ds, "tableÖsszesen");

            //sel = "select a.azonosito, 0.00 as brutton, b.brutto as bruttoc " +
            //      "from szamla a " +
            //      "inner join szamla_tetel b on a.id=b.id ";
            //sel = sel + "where a.vs='S' ";
            //if (rBBank.Checked && cbBank.Text != "")
            //    sel = sel + "and a.h_fsz_id = " + folyszla_id + " ";
            //else if (rBPenztar.Checked && cbPenztar.Text != "")
            //    sel = sel + "and a.h_penztar_id = " + penztar_id + " ";
            //da = new SqlDataAdapter(sel, myconn);
            //da.Fill(ds, "tableÖsszesen");

            //sel = "select cast(id as char), ft as brutton, 0.00 as bruttoc " +
            //      "from penzmozg a ";
            //if (rBBank.Checked && cbBank.Text != "")
            //    sel = sel + "where a.h_fsz_id = " + folyszla_id + " ";
            //else if (rBPenztar.Checked && cbPenztar.Text != "")
            //    sel = sel + "where a.h_penztar_id = " + penztar_id + " ";
            //da = new SqlDataAdapter(sel, myconn);
            //da.Fill(ds, "tableÖsszesen");

            //sel = "select cast(id as char), 0.00 as brutton, ft as bruttoc " +
            //      "from penzmozg a ";
            //if (rBBank.Checked && cbBank.Text != "")
            //    sel = sel + "where a.fsz_id = " + folyszla_id + " ";
            //else if (rBPenztar.Checked && cbPenztar.Text != "")
            //    sel = sel + "where a.penztar_id = " + penztar_id + " ";

            //da = new SqlDataAdapter(sel, myconn);
            //da.Fill(ds, "tableÖsszesen");
            //tableÖsszesen = ds.Tables["tableÖsszesen"];

            decimal afa = 0;
            for (int i = 0; i < tableSzamla.Rows.Count; i++)
            {
                afa = afa + Convert.ToDecimal(tableSzamla.Rows[i]["afa"].ToString());
            }
            osszesen.Text = string.Format("{0:N}", Convert.ToInt32(afa));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (tableSzamla.Rows.Count > 0)
            {
                nyomtat = new PrintForm();
                string[] parNev = { "DATUM_TOL", "DATUM_IG" };
                string[] parVal = { dateTol.Value.ToShortDateString(), dateIg.Value.ToShortDateString()};
                string[] parTip = { "string", "string", };


                DataSet dS = new MainProgramm.virDataSet();
                DataRow r;
                dS.Tables["AfaLista"].Clear();
                for (int i = 0; i < tableSzamla.Rows.Count; i++)
                {
                    r = dS.Tables["AfaLista"].NewRow();
                    r["azonosito"] = tableSzamla.Rows[i]["azonosito"];
                    r["datum"] = tableSzamla.Rows[i]["datum_telj"];
                    r["jel"] = tableSzamla.Rows[i]["jel"];
                    r["osszeg"] = Convert.ToDecimal(tableSzamla.Rows[i]["osszeg"].ToString().Replace('.', ','));
                    if (tableSzamla.Rows[i]["partner"].ToString() == "")
                        r["partner"] = " ";
                    else
                        r["partner"] = tableSzamla.Rows[i]["partner"];
                    r["megjegyzes"] = tableSzamla.Rows[i]["megjegyzes"];
                    r["afa"] = Convert.ToDecimal(tableSzamla.Rows[i]["afa"].ToString().Replace('.', ','));
                    r["afakulcs"] = Convert.ToDecimal(tableSzamla.Rows[i]["afakulcs"].ToString().Replace('.', ','));
                    dS.Tables["AfaLista"].Rows.Add(r);
                }

                nyomtat.PrintParams(parNev, parVal, parTip);
                this.afaListaLista.SetDataSource(dS);
                this.afaListaLista.SetParameterValue("DATUM_TOL", dateTol.Value.ToShortDateString());
                this.afaListaLista.SetParameterValue("DATUM_IG", dateIg.Value.ToShortDateString());

                nyomtat.reportSource = afaListaLista;
                nyomtat.DoPreview(mainForm.defPageSettings);
            }
        }

    }
}
