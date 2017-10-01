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
        private MyTag     penztar;
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
        private string szint;


        public Penzforgalom(string szoveg, object[] obj)
        {
            MyTag[] tagArray = (MyTag[])obj[1];
            this.szamlatag = tagArray[0];
            this.szamlainfo = this.szamlatag.AdatTablainfo;
            this.szamlateteltag = tagArray[1];
            this.szamlatetelinfo = this.szamlateteltag.AdatTablainfo;
            this.penzmozgastag = tagArray[2];
            this.penzmozgasinfo = this.penzmozgastag.AdatTablainfo;
            this.partnerinfo = tagArray[3].AdatTablainfo;
            this.partnertetelinfo = tagArray[4].AdatTablainfo;
            string[] strArray = this.partnerinfo.FindIdentityArray(new string[] { "SAJAT" }, new string[] { "I" });
            string[] kellfileinfo = this.partnerinfo.FindIdentityArray(new string[] { "SAJAT" }, new string[] { "N" });
            if (strArray == null)
            {
                MessageBox.Show("Nincs 'sajat' jelu partner!");
                base.Visible = false;
            }
            if (base.Visible)
            {
                int num;
                ArrayList list = new ArrayList();
                for (num = 0; num < strArray.Length; num++)
                {
                    string[] strArray3 = this.partnertetelinfo.FindIdentityArray(new string[] { "PID" }, new string[] { strArray[num] });
                    for (int i = 0; i < strArray3.Length; i++)
                    {
                        list.Add(strArray3[i]);
                    }
                }
                this.fszids = new string[list.Count];
                for (num = 0; num < this.fszids.Length; num++)
                {
                    this.fszids[num] = list[num].ToString();
                }
                this.InitializeComponent();
                this.Fak = tagArray[0].Fak;
                this.Fak.ControlTagokTolt(this, new Control[] { this });
                this.szamlainfo.Comboinfoszures(this.cbBank, this.fszids);
                this.szamlainfo.Comboinfoszures(this.comboSzallito, kellfileinfo);
                this.szint = obj[2].ToString();
                this.penztar = this.Fak.GetKodtab("C", "PENZT");
                this.igazivaltozas = true;
                this.cbBank.Text = "";
                this.cbPenztar.Text = "";
                this.comboSzallito.Text = "";
                this.folyszla_id = "-1";
                this.penztar_id = "-1";
            }
        }

        private void Penzforgalom_Load(object sender, EventArgs e)
        {
            this.mainForm = (VIR.MainForm)base.ParentForm;
            this.myconn = this.mainForm.MyConn;
            this.cbBank.Text = "";
            this.cbPenztar.Text = "";
            this.comboSzallito.Text = "";
        }

        private void rBBank_CheckedChanged(object sender, EventArgs e)
        {
            this.toolStripButton1.Enabled = true;
            if (this.rBBank.Checked)
            {
                this.cbBank.Enabled = true;
                this.cbPenztar.Enabled = false;
                this.cbPenztar.Text = "";
            }
            else
            {
                this.cbBank.Enabled = false;
                this.cbPenztar.Enabled = true;
                this.cbBank.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string str = this.dateTol.Value.ToShortDateString().Substring(0, 10);
            string str2 = this.dateIg.Value.ToShortDateString().Substring(0, 10);
            this.da = new SqlDataAdapter("select * from rendszer", this.myconn);
            this.da.Fill(this.ds, "tableRendszer");
            this.tableRendszer = this.ds.Tables["tableRendszer"];
            if (this.tableRendszer.Rows.Count > 0)
            {
                this.vegzo_sorszam = this.tableRendszer.Rows[0]["vegzo_sorszam"].ToString();
            }
            if (this.rBBank.Checked)
            {
                this.folyszla_id = this.szamlainfo.GetActualCombofileinfo(this.cbBank);
            }
            else
            {
                this.penztar_id = this.szamlainfo.GetActualCombofileinfo(this.cbPenztar);
            }
            if (this.comboSzallito.Text != "")
            {
                this.partner_id = this.szamlainfo.GetActualCombofileinfo(this.comboSzallito);
            }
            if (this.folyszla_id == "")
            {
                this.folyszla_id = "-1";
            }
            if (this.penztar_id == "")
            {
                this.penztar_id = "-1";
            }
            if (this.partner_id == "")
            {
                this.partner_id = "-1";
            }
            this.tableSzamla.Clear();
            this.sel = "select a.id, a.azonosito, a.datum, 'Vev\x00f6' as jel, sum(b.brutto) as brutton, 0.00 as bruttoc, d.azonosito as partner, a.megjegyzes from szamla a inner join szamla_tetel b on a.id = b.id left outer join partner d on a.pid = d.pid ";
            if (this.rBBank.Checked && (this.cbBank.Text != ""))
            {
                this.sel = this.sel + "left outer join partner_folyosz c on a.h_fsz_id=c.fsz_id ";
            }
            else if (this.rBPenztar.Checked && (this.cbPenztar.Text != ""))
            {
                this.sel = this.sel + "left outer join kodtab c on a.h_penztar_id=c.sorszam ";
            }
            this.sel = this.sel + "where a.vs='V' and cast(datum as datetime) >= cast('" + str + "' as datetime) and cast(datum as datetime) <= cast('" + str2 + "' as datetime) ";
            if (this.rBBank.Checked && (this.cbBank.Text != ""))
            {
                this.sel = this.sel + "and a.h_fsz_id = " + this.folyszla_id + " ";
            }
            else if (this.rBPenztar.Checked && (this.cbPenztar.Text != ""))
            {
                this.sel = this.sel + "and a.h_penztar_id = " + this.penztar_id + " ";
            }
            if (this.comboSzallito.Text != "")
            {
                this.sel = this.sel + "and a.pid = " + this.partner_id + " ";
            }
            this.sel = this.sel + " group by a.id, a.azonosito, a.datum, d.azonosito, a.megjegyzes order by a.datum";
            this.da = new SqlDataAdapter(this.sel, this.myconn);
            this.da.Fill(this.ds, "tableSzamla");
            this.sel = "select a.id, a.azonosito, a.datum, 'Sz\x00e1ll\x00edt\x00f3' as jel, 0.00 as brutton, sum(b.brutto) as bruttoc, d.azonosito as partner, a.megjegyzes from szamla a inner join szamla_tetel b on a.id=b.id left outer join partner d on a.pid = d.pid ";
            if (this.rBBank.Checked && (this.cbBank.Text != ""))
            {
                this.sel = this.sel + "left outer join partner_folyosz c on a.h_fsz_id=c.fsz_id ";
            }
            else if (this.rBPenztar.Checked && (this.cbPenztar.Text != ""))
            {
                this.sel = this.sel + "left outer join kodtab c on a.h_penztar_id=c.sorszam ";
            }
            this.sel = this.sel + "where a.vs='S' and cast(datum as datetime) >= cast('" + str + "' as datetime) and cast(datum as datetime) <= cast('" + str2 + "' as datetime) ";
            if (this.rBBank.Checked && (this.cbBank.Text != ""))
            {
                this.sel = this.sel + "and a.h_fsz_id = " + this.folyszla_id + " ";
            }
            else if (this.rBPenztar.Checked && (this.cbPenztar.Text != ""))
            {
                this.sel = this.sel + "and a.h_penztar_id = " + this.penztar_id + " ";
            }
            if (this.comboSzallito.Text != "")
            {
                this.sel = this.sel + "and a.pid = " + this.partner_id + " ";
            }
            this.sel = this.sel + " group by a.id, a.azonosito, a.datum, d.azonosito, a.megjegyzes order by a.datum";
            this.da = new SqlDataAdapter(this.sel, this.myconn);
            this.da.Fill(this.ds, "tableSzamla");
            this.sel = "select cast(id as char) as azonosito, datum, case when (a.h_fsz_id > 0 or a.h_penztar_id > 0) and (a.fsz_id > 0 or a.penztar_id > 0) then 'Belső-P\x00e9nzmozg\x00e1s' else 'K\x00fclső-P\x00e9nzmozg\x00e1s' end as jel, ";
            this.sel = this.sel + "case when (a.h_fsz_id > 0 or a.h_penztar_id > 0) ";
            if (this.rBBank.Checked && (this.cbBank.Text != ""))
            {
                this.sel = this.sel + " and a.h_fsz_id = " + this.folyszla_id + " ";
            }
            else if (this.rBPenztar.Checked && (this.cbPenztar.Text != ""))
            {
                this.sel = this.sel + " and a.h_penztar_id = " + this.penztar_id + " ";
            }
            this.sel = this.sel + "then ft else 0.00 end as brutton, ";
            this.sel = this.sel + "case when (a.fsz_id > 0 or a.penztar_id > 0) ";
            if (this.rBBank.Checked && (this.cbBank.Text != ""))
            {
                this.sel = this.sel + " and a.fsz_id = " + this.folyszla_id + " ";
            }
            else if (this.rBPenztar.Checked && (this.cbPenztar.Text != ""))
            {
                this.sel = this.sel + " and a.penztar_id = " + this.penztar_id + " ";
            }
            this.sel = this.sel + "then ft else 0.00 end as bruttoc, '' as partner, a.megjegyzes from penzmozg a ";
            this.sel = this.sel + "where cast(datum as datetime) >= cast('" + str + "' as datetime) and cast(datum as datetime) <= cast('" + str2 + "' as datetime) ";
            if (this.rBBank.Checked && (this.cbBank.Text != ""))
            {
                this.sel = this.sel + "and (a.h_fsz_id = " + this.folyszla_id + " or a.fsz_id = " + this.folyszla_id + ")";
            }
            else if (this.rBPenztar.Checked && (this.cbPenztar.Text != ""))
            {
                this.sel = this.sel + "and (a.h_penztar_id = " + this.penztar_id + " or a.penztar_id = " + this.penztar_id + ")";
            }
            this.sel = this.sel + " order by a.datum";
            this.da = new SqlDataAdapter(this.sel, this.myconn);
            this.da.Fill(this.ds, "tableSzamla");
            this.tableSzamla = this.ds.Tables["tableSzamla"];
            this.viewEredmeny.BeginInit();
            this.viewEredmeny.Table = this.tableSzamla;
            this.viewEredmeny.EndInit();
            this.viewEredmeny.Sort = "datum";
            this.dataGV.DataSource = this.viewEredmeny;
            this.tableÖsszesen.Clear();
            this.sel = "select sum(b.brutto) as brutton, 0.00 as bruttoc, a.datum from szamla a inner join szamla_tetel b on a.id = b.id ";
            this.sel = this.sel + "where a.vs='V'";
            if (this.rBBank.Checked && (this.cbBank.Text != ""))
            {
                this.sel = this.sel + " and a.h_fsz_id = " + this.folyszla_id + " ";
            }
            else if (this.rBPenztar.Checked && (this.cbPenztar.Text != ""))
            {
                this.sel = this.sel + " and a.h_penztar_id = " + this.penztar_id + " ";
            }
            this.sel = this.sel + " and cast(datum as datetime) <= cast('" + str2 + "' as datetime)  group by a.datum ";
            this.da = new SqlDataAdapter(this.sel, this.myconn);
            this.da.Fill(this.ds, "table\x00d6sszesen");
            this.sel = "select 0.00 as brutton, sum(b.brutto) as bruttoc, a.datum from szamla a inner join szamla_tetel b on a.id = b.id ";
            this.sel = this.sel + "where a.vs='S'";
            if (this.rBBank.Checked && (this.cbBank.Text != ""))
            {
                this.sel = this.sel + " and a.h_fsz_id = " + this.folyszla_id + " ";
            }
            else if (this.rBPenztar.Checked && (this.cbPenztar.Text != ""))
            {
                this.sel = this.sel + " and a.h_penztar_id = " + this.penztar_id + " ";
            }
            this.sel = this.sel + " and cast(datum as datetime) <= cast('" + str2 + "' as datetime)  group by a.datum ";
            this.da = new SqlDataAdapter(this.sel, this.myconn);
            this.da.Fill(this.ds, "table\x00d6sszesen");
            this.sel = " select case when (a.h_fsz_id > 0 or a.h_penztar_id > 0) ";
            if (this.rBBank.Checked && (this.cbBank.Text != ""))
            {
                this.sel = this.sel + " and a.h_fsz_id = " + this.folyszla_id + " ";
            }
            else if (this.rBPenztar.Checked && (this.cbPenztar.Text != ""))
            {
                this.sel = this.sel + " and a.h_penztar_id = " + this.penztar_id + " ";
            }
            this.sel = this.sel + "then ft else 0.00 end as brutton, ";
            this.sel = this.sel + "case when (a.fsz_id > 0 or a.penztar_id > 0) ";
            if (this.rBBank.Checked && (this.cbBank.Text != ""))
            {
                this.sel = this.sel + " and a.fsz_id = " + this.folyszla_id + " ";
            }
            else if (this.rBPenztar.Checked && (this.cbPenztar.Text != ""))
            {
                this.sel = this.sel + " and a.penztar_id = " + this.penztar_id + " ";
            }
            this.sel = this.sel + "then ft else 0.00 end as bruttoc, datum  from penzmozg a  where cast(datum as datetime) <= cast('" + str2 + "' as datetime) ";
            if (this.rBBank.Checked && (this.cbBank.Text != ""))
            {
                this.sel = this.sel + "and (a.h_fsz_id = " + this.folyszla_id + " or a.fsz_id = " + this.folyszla_id + ")";
            }
            else if (this.rBPenztar.Checked && (this.cbPenztar.Text != ""))
            {
                this.sel = this.sel + "and (a.h_penztar_id = " + this.penztar_id + " or a.penztar_id = " + this.penztar_id + ")";
            }
            this.da = new SqlDataAdapter(this.sel, this.myconn);
            this.da.Fill(this.ds, "table\x00d6sszesen");
            this.tableÖsszesen = this.ds.Tables["table\x00d6sszesen"];
            decimal num = 0M;
            decimal num2 = 0M;
            decimal num3 = 0M;
            decimal num4 = 0M;
            for (int i = 0; i < this.tableÖsszesen.Rows.Count; i++)
            {
                num3 += Convert.ToDecimal(this.tableÖsszesen.Rows[i]["brutton"].ToString());
                num4 += Convert.ToDecimal(this.tableÖsszesen.Rows[i]["bruttoc"].ToString());
                DateTime time = Convert.ToDateTime(Convert.ToDateTime(this.tableÖsszesen.Rows[i]["datum"].ToString()).ToShortDateString());
                DateTime time2 = Convert.ToDateTime(this.dateTol.Value.ToShortDateString());
                if (time.CompareTo(time2) <= 0)
                {
                    num += Convert.ToDecimal(this.tableÖsszesen.Rows[i]["brutton"].ToString());
                    num2 += Convert.ToDecimal(this.tableÖsszesen.Rows[i]["bruttoc"].ToString());
                }
            }
            this.osszesen.Text = $"{Convert.ToInt32((decimal)(num - num2)):N}";
            this.idoszaki.Text = $"{Convert.ToInt32((decimal)(num3 - num4)):N}";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.tableSzamla.Rows.Count > 0)
            {
                DataRow[] rowArray = this.tableSzamla.Select("", "datum");
                this.nyomtat = new PrintForm();
                string[] parName = new string[] { "BSZLA", "PENZTAR", "DATUM_TOL", "DATUM_IG", "OSSZESEN" };
                string[] parValue = new string[] { this.cbBank.Text, this.cbPenztar.Text, this.dateTol.Value.ToShortDateString(), this.dateIg.Value.ToShortDateString(), this.osszesen.Text };
                string[] parTyp = new string[] { "string", "string", "string", "string", "number" };
                DataSet dataSet = new MainProgramm.virDataSet();
                dataSet.Tables["PenzForgalom"].Clear();
                for (int i = 0; i < rowArray.Length; i++)
                {
                    DataRow row = dataSet.Tables["PenzForgalom"].NewRow();
                    row["azonosito"] = rowArray[i]["azonosito"];
                    row["datum"] = rowArray[i]["datum"];
                    row["jel"] = rowArray[i]["jel"];
                    row["brutton"] = Convert.ToDecimal(rowArray[i]["brutton"].ToString().Replace('.', ','));
                    row["bruttoc"] = Convert.ToDecimal(rowArray[i]["bruttoc"].ToString().Replace('.', ','));
                    if (rowArray[i]["partner"].ToString() == "")
                    {
                        row["partner"] = " ";
                    }
                    else
                    {
                        row["partner"] = rowArray[i]["partner"];
                    }
                    row["megjegyzes"] = rowArray[i]["megjegyzes"];
                    dataSet.Tables["PenzForgalom"].Rows.Add(row);
                }
                this.nyomtat.PrintParams(parName, parValue, parTyp);
                this.penzforgalomLista.SetDataSource(dataSet);
                this.penzforgalomLista.SetParameterValue("BSZLA", this.cbBank.Text);
                this.penzforgalomLista.SetParameterValue("PENZTAR", this.cbPenztar.Text);
                this.penzforgalomLista.SetParameterValue("DATUM_TOL", this.dateTol.Value.ToShortDateString());
                this.penzforgalomLista.SetParameterValue("DATUM_IG", this.dateIg.Value.ToShortDateString());
                this.penzforgalomLista.SetParameterValue("OSSZESEN", Convert.ToDecimal(this.osszesen.Text));
                this.nyomtat.reportSource = this.penzforgalomLista;
                this.nyomtat.DoPreview(this.mainForm.defPageSettings);
            }
        }

        private void cbPenztar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.rBPenztar.Checked)
            {
                this.toolStripButton1.Enabled = true;
                DataRow[] rowArray = this.penztar.AdatTablainfo.Adattabla.Select("szoveg = '" + this.cbPenztar.Text + "'");
                if (((rowArray.Length > 0) && (rowArray[0]["megj"].ToString() != "")) && (rowArray[0]["megj"].ToString() != this.szint))
                {
                    MessageBox.Show("Ezt a pénztárt nincs joga lekérdezni!");
                    this.toolStripButton1.Enabled = false;
                }
            }
        }
    }
}