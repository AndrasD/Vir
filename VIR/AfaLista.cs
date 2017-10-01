using FormattedTextBox;
using Könyvtar.Printlib;
using MainProgramm;
using MainProgramm.Listák;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using TableInfo;
using VIR;

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
        private string szint;

        public AfaLista(string szoveg, object[] obj)
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
            this.szint = obj[2].ToString();
            string[] strArray = this.partnerinfo.FindIdentityArray(new string[] { "SAJAT" }, new string[] { "I" });
            string[] strArray2 = this.partnerinfo.FindIdentityArray(new string[] { "SAJAT" }, new string[] { "N" });
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
            }
        }

        private void AfaLista_Load(object sender, EventArgs e)
        {
            this.mainForm = (MainForm)base.ParentForm;
            this.myconn = this.mainForm.MyConn;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string str = this.dateTol.Value.ToShortDateString().Substring(0, 10);
            string str2 = this.dateIg.Value.ToShortDateString().Substring(0, 10);
            this.tableSzamla.Clear();
            if (this.radioVevo.Checked)
            {
                this.sel = "select a.azonosito, a.datum, a.datum_telj, a.datum_fiz, 'Vev\x00f6' as jel, b.brutto as osszeg, d.azonosito as partner, a.megjegyzes, b.afa, b.afakulcs from szamla a inner join szamla_tetel b on a.id=b.id left outer join partner d on a.pid = d.pid ";
                this.sel = this.sel + "where a.vs='V' and cast(datum_telj as datetime) >= cast('" + str + "' as datetime) and cast(datum_telj as datetime) <= cast('" + str2 + "' as datetime) ";
                if (this.szint != "1")
                {
                    this.sel = this.sel + " AND H_PENZTAR_ID <> 407";
                }
                this.da = new SqlDataAdapter(this.sel, this.myconn);
                this.da.Fill(this.ds, "tableSzamla");
            }
            if (this.radioSzallito.Checked)
            {
                this.sel = "select a.azonosito, a.datum, a.datum_telj, a.datum_fiz, 'Sz\x00e1ll\x00edt\x00f3' as jel, b.brutto as osszeg, d.azonosito as partner, a.megjegyzes, b.afa, b.afakulcs  from szamla a inner join szamla_tetel b on a.id=b.id left outer join partner d on a.pid = d.pid ";
                this.sel = this.sel + "where a.vs='S' and cast(datum_telj as datetime) >= cast('" + str + "' as datetime) and cast(datum_telj as datetime) <= cast('" + str2 + "' as datetime) ";
                if (this.szint != "1")
                {
                    this.sel = this.sel + " AND H_PENZTAR_ID <> 407";
                }
                this.da = new SqlDataAdapter(this.sel, this.myconn);
                this.da.Fill(this.ds, "tableSzamla");
            }
            this.tableSzamla = this.ds.Tables["tableSzamla"];
            this.viewEredmeny.BeginInit();
            this.viewEredmeny.Table = this.tableSzamla;
            this.viewEredmeny.EndInit();
            this.dataGV.DataSource = this.viewEredmeny;
            decimal num = 0M;
            for (int i = 0; i < this.tableSzamla.Rows.Count; i++)
            {
                num += Convert.ToDecimal(this.tableSzamla.Rows[i]["afa"].ToString());
            }
            this.osszesen.Text = $"{Convert.ToInt32(num):N}";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.tableSzamla.Rows.Count > 0)
            {
                this.nyomtat = new PrintForm();
                string[] parName = new string[] { "DATUM_TOL", "DATUM_IG" };
                string[] parValue = new string[] { this.dateTol.Value.ToShortDateString(), this.dateIg.Value.ToShortDateString() };
                string[] parTyp = new string[] { "string", "string" };
                DataSet dataSet = new virDataSet();
                dataSet.Tables["AfaLista"].Clear();
                for (int i = 0; i < this.tableSzamla.Rows.Count; i++)
                {
                    DataRow row = dataSet.Tables["AfaLista"].NewRow();
                    row["azonosito"] = this.tableSzamla.Rows[i]["azonosito"];
                    row["datum"] = this.tableSzamla.Rows[i]["datum_telj"];
                    row["jel"] = this.tableSzamla.Rows[i]["jel"];
                    row["osszeg"] = Convert.ToDecimal(this.tableSzamla.Rows[i]["osszeg"].ToString().Replace('.', ','));
                    if (this.tableSzamla.Rows[i]["partner"].ToString() == "")
                    {
                        row["partner"] = " ";
                    }
                    else
                    {
                        row["partner"] = this.tableSzamla.Rows[i]["partner"];
                    }
                    row["megjegyzes"] = this.tableSzamla.Rows[i]["megjegyzes"];
                    row["afa"] = Convert.ToDecimal(this.tableSzamla.Rows[i]["afa"].ToString().Replace('.', ','));
                    row["afakulcs"] = Convert.ToDecimal(this.tableSzamla.Rows[i]["afakulcs"].ToString().Replace('.', ','));
                    dataSet.Tables["AfaLista"].Rows.Add(row);
                }
                this.nyomtat.PrintParams(parName, parValue, parTyp);
                this.afaListaLista.SetDataSource(dataSet);
                this.afaListaLista.SetParameterValue("DATUM_TOL", this.dateTol.Value.ToShortDateString());
                this.afaListaLista.SetParameterValue("DATUM_IG", this.dateIg.Value.ToShortDateString());
                this.nyomtat.reportSource = this.afaListaLista;
                this.nyomtat.DoPreview(this.mainForm.defPageSettings);
            }
        }

    }
}
