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
                this.szamlainfo.Comboinfoszures(this.comboSzallito, kellfileinfo);
                this.igazivaltozas = true;
                this.comboSzallito.Text = "";
                this.folyszla_id = "-1";
                this.penztar_id = "-1";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string str = this.dateTol.Value.ToShortDateString().Substring(0, 10);
            string str2 = this.dateIg.Value.ToShortDateString().Substring(0, 10);
            if ((this.rbVevo.Checked || this.rbSzallito.Checked) || this.rbPenzm.Checked)
            {
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
                if (this.rbVevo.Checked)
                {
                    this.sel = "select a.id, a.azonosito, a.datum, 'Vev\x00f6' as jel, sum(b.netto) as netto, sum(b.afa) as afa, sum(b.brutto) as brutto, d.azonosito as partner, a.megjegyzes, 0 as fsz_id, a.h_fsz_id, 0 as penztar_id, a.h_penztar_id,  case when fizetve = 'I' then a.datum_fizetve else cast('' as datetime) end as dat_fiz from szamla a inner join szamla_tetel b on a.id = b.id left outer join partner d on a.pid = d.pid ";
                    this.sel = this.sel + "where a.vs='V' and cast(datum as datetime) >= cast('" + str + "' as datetime) and cast(datum as datetime) <= cast('" + str2 + "' as datetime) ";
                    if (this.rBBank.Checked)
                    {
                        this.sel = this.sel + "and a.h_fsz_id > 0 and a.h_penztar_id = 0 ";
                    }
                    else if (this.rBPenztar.Checked)
                    {
                        this.sel = this.sel + "and a.h_penztar_id > 0 ";
                    }
                    if (this.comboSzallito.Text != "")
                    {
                        this.sel = this.sel + "and a.pid = " + this.partner_id + " ";
                    }
                    this.sel = this.sel + " group by a.id, a.azonosito, a.datum, d.azonosito, a.megjegyzes, a.h_fsz_id, a.h_penztar_id, fizetve, datum_fizetve ";
                    this.da = new SqlDataAdapter(this.sel, this.myconn);
                    this.da.Fill(this.ds, "tableSzamla");
                }
                if (this.rbSzallito.Checked)
                {
                    this.sel = "select a.id, a.azonosito, a.datum, 'Sz\x00e1ll\x00edt\x00f3' as jel, sum(b.netto) as netto, sum(b.afa) as afa, sum(b.brutto) as brutto, d.azonosito as partner, a.megjegyzes, 0 as fsz_id, a.h_fsz_id, 0 as penztar_id, a.h_penztar_id,  case when fizetve = 'I' then a.datum_fizetve else cast('' as datetime) end as dat_fiz from szamla a inner join szamla_tetel b on a.id=b.id left outer join partner d on a.pid = d.pid ";
                    this.sel = this.sel + "where a.vs='S' and cast(datum as datetime) >= cast('" + str + "' as datetime) and cast(datum as datetime) <= cast('" + str2 + "' as datetime) ";
                    if (this.rBBank.Checked)
                    {
                        this.sel = this.sel + "and a.h_fsz_id > 0 and a.h_penztar_id = 0 ";
                    }
                    else if (this.rBPenztar.Checked)
                    {
                        this.sel = this.sel + "and a.h_penztar_id > 0 ";
                    }
                    if (this.comboSzallito.Text != "")
                    {
                        this.sel = this.sel + "and a.pid = " + this.partner_id + " ";
                    }
                    this.sel = this.sel + " group by a.id, a.azonosito, a.datum, d.azonosito, a.megjegyzes, a.h_fsz_id, a.h_penztar_id, fizetve, datum_fizetve ";
                    this.da = new SqlDataAdapter(this.sel, this.myconn);
                    this.da.Fill(this.ds, "tableSzamla");
                }
                if (this.rbPenzm.Checked)
                {
                    this.sel = "select cast(id as char) as azonosito, datum, case when (a.h_fsz_id > 0 or a.h_penztar_id > 0) and (a.fsz_id > 0 or a.penztar_id > 0) then 'Belső-P\x00e9nzmozg\x00e1s' else 'K\x00fclső-P\x00e9nzmozg\x00e1s' end as jel, 0.00 as netto, 0.00 as afa, ft as brutto, '' as partner, a.megjegyzes, a.fsz_id, a.h_fsz_id, a.penztar_id, a.h_penztar_id, cast('' as datetime) as dat_fiz from penzmozg a ";
                    this.sel = this.sel + "where cast(datum as datetime) >= cast('" + str + "' as datetime) and cast(datum as datetime) <= cast('" + str2 + "' as datetime) ";
                    if (this.rBBank.Checked)
                    {
                        this.sel = this.sel + "and (a.h_fsz_id > 0 or a.fsz_id > 0 ) ";
                    }
                    else if (this.rBPenztar.Checked)
                    {
                        this.sel = this.sel + "and (a.h_penztar_id > 0 or a.penztar_id > 0)";
                    }
                    this.da = new SqlDataAdapter(this.sel, this.myconn);
                    this.da.Fill(this.ds, "tableSzamla");
                }
                this.tableSzamla = this.ds.Tables["tableSzamla"];
                this.viewEredmeny.BeginInit();
                this.viewEredmeny.Table = this.tableSzamla;
                this.viewEredmeny.EndInit();
                this.viewEredmeny.Sort = "datum";
                this.dataGV.DataSource = this.viewEredmeny;
                decimal num = 0M;
                decimal num2 = 0M;
                decimal num3 = 0M;
                for (int i = 0; i < this.tableSzamla.Rows.Count; i++)
                {
                    if (this.tableSzamla.Rows[i]["jel"].ToString() == "Vev\x00f6")
                    {
                        num += Convert.ToDecimal(this.tableSzamla.Rows[i]["netto"].ToString());
                        num2 += Convert.ToDecimal(this.tableSzamla.Rows[i]["afa"].ToString());
                        num3 += Convert.ToDecimal(this.tableSzamla.Rows[i]["brutto"].ToString());
                    }
                    else if (this.tableSzamla.Rows[i]["jel"].ToString() == "Sz\x00e1ll\x00edt\x00f3")
                    {
                        num -= Convert.ToDecimal(this.tableSzamla.Rows[i]["netto"].ToString());
                        num2 -= Convert.ToDecimal(this.tableSzamla.Rows[i]["afa"].ToString());
                        num3 -= Convert.ToDecimal(this.tableSzamla.Rows[i]["brutto"].ToString());
                    }
                    else
                    {
                        if (Convert.ToInt32(this.tableSzamla.Rows[i]["fsz_id"].ToString()) > 0)
                        {
                            num -= Convert.ToDecimal(this.tableSzamla.Rows[i]["netto"].ToString());
                            num2 -= Convert.ToDecimal(this.tableSzamla.Rows[i]["afa"].ToString());
                            num3 -= Convert.ToDecimal(this.tableSzamla.Rows[i]["brutto"].ToString());
                        }
                        if (Convert.ToInt32(this.tableSzamla.Rows[i]["h_fsz_id"].ToString()) > 0)
                        {
                            num += Convert.ToDecimal(this.tableSzamla.Rows[i]["netto"].ToString());
                            num2 += Convert.ToDecimal(this.tableSzamla.Rows[i]["afa"].ToString());
                            num3 += Convert.ToDecimal(this.tableSzamla.Rows[i]["brutto"].ToString());
                        }
                        if (Convert.ToInt32(this.tableSzamla.Rows[i]["penztar_id"].ToString()) > 0)
                        {
                            num -= Convert.ToDecimal(this.tableSzamla.Rows[i]["netto"].ToString());
                            num2 -= Convert.ToDecimal(this.tableSzamla.Rows[i]["afa"].ToString());
                            num3 -= Convert.ToDecimal(this.tableSzamla.Rows[i]["brutto"].ToString());
                        }
                        if (Convert.ToInt32(this.tableSzamla.Rows[i]["h_penztar_id"].ToString()) > 0)
                        {
                            num += Convert.ToDecimal(this.tableSzamla.Rows[i]["netto"].ToString());
                            num2 += Convert.ToDecimal(this.tableSzamla.Rows[i]["afa"].ToString());
                            num3 += Convert.ToDecimal(this.tableSzamla.Rows[i]["brutto"].ToString());
                        }
                    }
                }
                this.nettoForgalom.Text = $"{Convert.ToInt32(num):N}";
                this.afaForgalom.Text = $"{Convert.ToInt32(num2):N}";
                this.bruttoForgalom.Text = $"{Convert.ToInt32(num3):N}";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.tableSzamla.Rows.Count > 0)
            {
                this.nyomtat = new PrintForm();
                string[] parName = new string[] { "BSZLA", "PENZTAR", "DATUM_TOL", "DATUM_IG", "NETTOSUM", "AFASUM", "BRUTTOSUM" };
                string[] parValue = new string[] { "", "", this.dateTol.Value.ToShortDateString(), this.dateIg.Value.ToShortDateString(), this.nettoForgalom.Text, this.afaForgalom.Text, this.bruttoForgalom.Text };
                string[] parTyp = new string[] { "string", "string", "string", "string", "number", "number", "number" };
                DataSet dataSet = new MainProgramm.virDataSet();
                dataSet.Tables["Bizonylatok"].Clear();
                for (int i = 0; i < this.tableSzamla.Rows.Count; i++)
                {
                    DataRow row = dataSet.Tables["Bizonylatok"].NewRow();
                    row["azonosito"] = this.tableSzamla.Rows[i]["azonosito"];
                    row["datum"] = this.tableSzamla.Rows[i]["datum"];
                    if ((this.tableSzamla.Rows[i]["jel"].ToString() == "Vev\x00f6") || (this.tableSzamla.Rows[i]["jel"].ToString() == "Sz\x00e1ll\x00edt\x00f3"))
                    {
                        row["jel"] = this.tableSzamla.Rows[i]["jel"].ToString().Substring(0, 1);
                    }
                    else
                    {
                        row["jel"] = "P";
                    }
                    row["netto"] = Convert.ToDecimal(this.tableSzamla.Rows[i]["netto"].ToString().Replace('.', ','));
                    row["afa"] = Convert.ToDecimal(this.tableSzamla.Rows[i]["afa"].ToString().Replace('.', ','));
                    row["brutto"] = Convert.ToDecimal(this.tableSzamla.Rows[i]["brutto"].ToString().Replace('.', ','));
                    if (this.tableSzamla.Rows[i]["partner"].ToString() == "")
                    {
                        row["partner"] = " ";
                    }
                    else
                    {
                        row["partner"] = this.tableSzamla.Rows[i]["partner"];
                    }
                    row["megjegyzes"] = this.tableSzamla.Rows[i]["megjegyzes"];
                    dataSet.Tables["Bizonylatok"].Rows.Add(row);
                }
                this.nyomtat.PrintParams(parName, parValue, parTyp);
                this.bizonylatokLista.SetDataSource(dataSet);
                this.bizonylatokLista.SetParameterValue("BSZLA", "");
                this.bizonylatokLista.SetParameterValue("PENZTAR", "");
                this.bizonylatokLista.SetParameterValue("DATUM_TOL", this.dateTol.Value.ToShortDateString());
                this.bizonylatokLista.SetParameterValue("DATUM_IG", this.dateIg.Value.ToShortDateString());
                this.bizonylatokLista.SetParameterValue("NETTOSUM", Convert.ToDecimal(this.nettoForgalom.Text));
                this.bizonylatokLista.SetParameterValue("AFASUM", Convert.ToDecimal(this.afaForgalom.Text));
                this.bizonylatokLista.SetParameterValue("BRUTTOSUM", Convert.ToDecimal(this.bruttoForgalom.Text));
                this.nyomtat.reportSource = this.bizonylatokLista;
                this.nyomtat.DoPreview(this.mainForm.defPageSettings);
            }
        }
        private void Penzforgalom_Load(object sender, EventArgs e)
        {
            this.mainForm = (VIR.MainForm)base.ParentForm;
            this.myconn = this.mainForm.MyConn;
            this.comboSzallito.Text = "";
        }

        private void rBBank_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rBBank.Checked)
            {
            }
        }

    }
}