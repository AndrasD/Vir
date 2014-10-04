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
    public partial class Szamlak : UserControl
    {
        private VIR.MainForm mainForm;
        private AlSzamlak alSzamlak ;

        private SqlConnection myconn = new SqlConnection();
        private DataSet ds = new DataSet();
        private SqlDataAdapter da;

        private PrintForm nyomtat = new PrintForm();
        private BevetelKiadasLista bevetelkiadasLista = new BevetelKiadasLista();

        private DataTable tableEvek = new DataTable();
        private DataTable tableKoztes = new DataTable();
        private DataTable tableHaviBontas = new DataTable();
        private DataTable tableFo = new DataTable();
        private DataView  viewFo  = new DataView();
        private DataTable tableAl = new DataTable();
        //private DataView  viewAl  = new DataView();
        private DataTable table   = new DataTable();
        //private DataView  view    = new DataView();

        private DataTable tableTooltip = new DataTable();
        private DataTable tablePartner = new DataTable();
        private DataTable tableSzoveg = new DataTable();

        private MyTag ktfotag;
        private MyTag ktaltag;
        private MyTag kttag;
        private DataTable ktfotabla;
        private DataTable ktaltabla;
        private DataTable kttabla;

        private string jel;
        private string partner_id = "";


        public Szamlak(string szoveg, object[] obj)
        {
            MyTag[] myTag = (MyTag[])obj[1];

            ktfotag = myTag[0];
            ktaltag = myTag[1];
            kttag = myTag[2];

            jel = obj[2].ToString();

            InitializeComponent();
        }

        private void Szamlak_Load(object sender, EventArgs e)
        {
            mainForm = (VIR.MainForm)this.ParentForm;
            myconn = mainForm.MyConn;

            ktfotabla = ktfotag.AdatTablainfo.Adattabla;
            ktaltabla = ktaltag.AdatTablainfo.Adattabla;
            kttabla = kttag.AdatTablainfo.Adattabla;

            da = new SqlDataAdapter("select '' as Kod, '' as Megnevezes, "+
                                      "0.0 as Januar, " +
                                      "0.0 as Februar, " +
                                      "0.0 as Marcius, " +
                                      "0.0 as Aprilis, " +
                                      "0.0 as Majus, " +
                                      "0.0 as Junius, " +
                                      "0.0 as Julius, " +
                                      "0.0 as Augusztus, " +
                                      "0.0 as Szeptember, " +
                                      "0.0 as Oktober, " +
                                      "0.0 as November, " +
                                      "0.0 as December, " +
                                      "'F' as bonthato, 'N' as bontva " +
                                      "from szamla_tetel where id is null", myconn);
            da.Fill(ds, "tableHaviBontas");
            tableHaviBontas = ds.Tables["tableHaviBontas"];

            da = new SqlDataAdapter("select distinct year(datum_telj) as ev from szamla", myconn);
            da.Fill(ds, "tableEvek");
            tableEvek = ds.Tables["tableEvek"];
            for (int i = 0; i < tableEvek.Rows.Count; i++)
                ev.Items.Add(tableEvek.Rows[i]["ev"].ToString());

            string dat = honap.Value.ToShortDateString();

            da = new SqlDataAdapter("select distinct partner.azonosito, partner.pid from szamla, partner " +
                                    "where szamla.pid = partner.pid "+
                                    "and year(datum_telj) = year('" + dat + "') and month(datum_telj) = month('" + dat + "') "+
                                    "order by azonosito "
                                    , myconn);
            da.Fill(ds, "tablePartner");
            tablePartner = ds.Tables["tablePartner"];
            for (int i = 0; i < tablePartner.Rows.Count; i++)
                comboPartner.Items.Add(tablePartner.Rows[i]["azonosito"].ToString());
            tablePartner.PrimaryKey = new DataColumn[] { tablePartner.Columns["azonosito"] };

            da = new SqlDataAdapter("select distinct szoveg from szamla a, szamla_tetel b " +
                                    "where a.id = b.id "+
                                    "and year(datum_telj) = year('" + dat + "') and month(datum_telj) = month('" + dat + "') "+
                                    "order by szoveg"
                                    , myconn);
            da.Fill(ds, "tableSzoveg");
            tableSzoveg = ds.Tables["tableSzoveg"];
            for (int i = 0; i < tableSzoveg.Rows.Count; i++)
                comboSzoveg.Items.Add(tableSzoveg.Rows[i]["szoveg"].ToString());
        }

        private void havi_CheckedChanged(object sender, EventArgs e)
        {
            tableFo.Clear();
            tableAl.Clear();
            table.Clear();
            if (havi.Checked)
            {
                honap.Enabled = true;
                ev.Enabled = false;
            }
            else
            {
                honap.Enabled = false;
                ev.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sel = "";
            string dat = honap.Value.ToShortDateString();
            tableKoztes.Clear();
            tableFo.Clear();
            tableAl.Clear();
            table.Clear();

            for (int i = 0; i < ktfotabla.Rows.Count; i++)
            {
                string tKod = ktfotabla.Rows[i]["kod"].ToString();
                string tSzoveg = ktfotabla.Rows[i]["szoveg"].ToString();
                string tSorszam = ktfotabla.Rows[i]["sorszam"].ToString();
                if (havi.Checked)
                    sel = "select substring(c.kod,1,1) as Kod, '" + tSzoveg + "' as Megnevezes, sum(b.netto) as Netto ";
                else
                    sel = "select substring(c.kod,1,1) as Kod, month(datum_telj) as ho, '" + tSzoveg + "' as Megnevezes, sum(b.netto) as Netto ";
                sel = sel + ", 'F' as bonthato, 'N' as bontva " +
                            "from szamla a, szamla_tetel b, kodtab c " + 
                            "where a.vs='"+jel+"' ";

                if (comboPartner.SelectedItem != null && partner_id != String.Empty)
                    sel = sel + "and pid=" + partner_id + " ";
                if (comboSzoveg.SelectedItem != null && comboSzoveg.SelectedItem.ToString() != String.Empty)
                    sel = sel + "and b.szoveg='" + comboSzoveg.SelectedItem.ToString() + "' ";

                if (havi.Checked)
                    sel = sel + "and year(datum_telj) = year('" + dat + "') and month(datum_telj) = month('" + dat + "') ";
                else
                    sel = sel + "and year(datum_telj) = '" + ev.Text + "' ";

                sel = sel + "and a.id=b.id " +
                            "and b.megnid = c.sorszam " +
                            "and '" + tKod + "' = substring(c.kod,1,1) ";
                if (havi.Checked)
                    sel = sel + "group by substring(c.kod,1,1)";
                else
                    sel = sel + "group by substring(c.kod,1,1),month(datum_telj)";

                da = new SqlDataAdapter(sel, myconn);
                if (havi.Checked)
                    da.Fill(ds, "tableFo");
                else
                    da.Fill(ds, "tableKoztes");
            }
            if (havi.Checked)
                tableFo = ds.Tables["tableFo"];
            else
                tableFo = koztesTablaAtforditas().Copy();
            this.viewFo.BeginInit();
            this.viewFo.Table = tableFo;
            this.viewFo.EndInit();
            viewFo.Sort = "kod";
            dataGV.DataSource = this.viewFo;
            dataGV.Columns[0].Frozen = true;
            dataGV.Columns[1].Frozen = true;
            for (int i = 2; i < dataGV.ColumnCount-2; i++)
            {
                dataGV.Columns[i].DefaultCellStyle.Format = "N2";
                dataGV.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            dataGV.Columns[dataGV.ColumnCount - 2].Visible = false;
            dataGV.Columns[dataGV.ColumnCount - 1].Visible = false;

            tableKoztes.Clear();
            for (int i = 0; i < ktaltabla.Rows.Count; i++)
            {
                string tKod = ktaltabla.Rows[i]["kod"].ToString();
                string tSzoveg = ktaltabla.Rows[i]["szoveg"].ToString();
                string tSorszam = ktaltabla.Rows[i]["sorszam"].ToString();
                if (havi.Checked)
                    sel = "select substring(c.kod,1,2) as Kod, '" + tSzoveg + "' as Megnevezes, sum(b.netto) as Netto ";
                else
                    sel = "select substring(c.kod,1,2) as Kod, month(datum_telj) as ho, '" + tSzoveg + "' as Megnevezes, sum(b.netto) as Netto ";
                sel = sel + ", 'A' as bonthato, 'N' as bontva " +
                            "from szamla a, szamla_tetel b, kodtab c "+
                            "where a.vs='" + jel + "' ";

                if (comboPartner.SelectedItem != null && partner_id != String.Empty)
                    sel = sel + "and pid=" + partner_id + " ";
                if (comboSzoveg.SelectedItem != null && comboSzoveg.SelectedItem.ToString() != String.Empty)
                    sel = sel + "and b.szoveg='" + comboSzoveg.SelectedItem.ToString() + "' ";

                if (havi.Checked)
                    sel = sel + "and year(datum_telj) = year('" + dat + "') and month(datum_telj) = month('" + dat + "') ";
                else
                    sel = sel + "and year(datum_telj) = '" + ev.Text + "' ";
                sel = sel + "and a.id=b.id " +
                             "and b.megnid = c.sorszam " +
                             "and '" + tKod + "' = substring(c.kod,1,2) ";
                if (havi.Checked)
                    sel = sel + "group by substring(c.kod,1,2)";
                else
                    sel = sel + "group by substring(c.kod,1,2),month(datum_telj)";

                da = new SqlDataAdapter(sel, myconn);
                if (havi.Checked)
                    da.Fill(ds, "tableAl");
                else
                    da.Fill(ds, "tableKoztes");
            }
            if (havi.Checked)
                tableAl = ds.Tables["tableAl"];
            else
                tableAl = koztesTablaAtforditas().Copy();
            //this.viewAl.BeginInit();
            //this.viewAl.Table = tableAl;
            //this.viewAl.EndInit();

            tableKoztes.Clear();
            for (int i = 0; i < kttabla.Rows.Count; i++)
            {
                string tKod = kttabla.Rows[i]["kod"].ToString();
                string tSzoveg = kttabla.Rows[i]["szoveg"].ToString();
                string tSorszam = kttabla.Rows[i]["sorszam"].ToString();
                if (havi.Checked)
                    sel = "select c.kod as Kod, '" + tSzoveg + "' as Megnevezes, sum(b.netto) as Netto ";
                else
                    sel = "select c.kod as Kod, month(datum_telj) as ho, '" + tSzoveg + "' as Megnevezes, sum(b.netto) as Netto ";
                sel = sel + ", 'T' as bonthato, 'N' as bontva " +
                            "from szamla a, szamla_tetel b, kodtab c "+
                            "where a.vs='" + jel + "' ";

                if (comboPartner.SelectedItem != null && partner_id != String.Empty)
                    sel = sel + "and pid=" + partner_id + " ";
                if (comboSzoveg.SelectedItem != null && comboSzoveg.SelectedItem.ToString() != String.Empty)
                    sel = sel + "and b.szoveg='" + comboSzoveg.SelectedItem.ToString() + "' ";

                if (havi.Checked)
                    sel = sel + "and year(datum_telj) = year('" + dat + "') and month(datum_telj) = month('" + dat + "') ";
                else
                    sel = sel + "and year(datum_telj) = '" + ev.Text + "' ";
                sel = sel + "and a.id=b.id " +
                             "and b.megnid = c.sorszam " +
                             "and '" + tKod + "' = c.kod ";
                if (havi.Checked)
                    sel = sel + "group by c.kod";
                else
                    sel = sel + "group by c.kod,month(datum_telj)";

                da = new SqlDataAdapter(sel, myconn);
                if (havi.Checked)
                    da.Fill(ds, "table");
                else
                    da.Fill(ds, "tableKoztes");
            }
            if (havi.Checked)
                table = ds.Tables["table"];
            else
                table = koztesTablaAtforditas().Copy();
            //this.view.BeginInit();
            //this.view.Table = table;
            //this.view.EndInit();
        }

        private DataTable koztesTablaAtforditas()
        {
            tableHaviBontas.Clear();
            tableKoztes = ds.Tables["tableKoztes"];
            int i = 0;
            while (i < tableKoztes.Rows.Count)
            {
                string tKod = tableKoztes.Rows[i]["kod"].ToString();
                DataRow ujsor = tableHaviBontas.NewRow();
                ujsor["kod"] = tKod;
                ujsor["megnevezes"] = tableKoztes.Rows[i]["megnevezes"].ToString();
                ujsor["bonthato"] = tableKoztes.Rows[i]["bonthato"].ToString();
                ujsor["bontva"] = tableKoztes.Rows[i]["bontva"].ToString();
                while (i < tableKoztes.Rows.Count && tKod == tableKoztes.Rows[i]["kod"].ToString())
                {
                    string hoNap = tableKoztes.Rows[i]["ho"].ToString();
                    string hoNev = honapNev(hoNap);
                    while (i < tableKoztes.Rows.Count && tKod == tableKoztes.Rows[i]["kod"].ToString() && hoNap == tableKoztes.Rows[i]["ho"].ToString())
                    {
                        ujsor[hoNev] = tableKoztes.Rows[i]["netto"].ToString();
                        i++;
                    }
                }
                tableHaviBontas.Rows.Add(ujsor);
            }
            return tableHaviBontas;
        }

        private string honapNev(string honap)
        {
            string ret = "Januar";
            switch (honap)
            {
                case "1":
                    ret = "Januar";
                    break;
                case "2":
                    ret = "Februar";
                    break;
                case "3":
                    ret ="Marcius";
                    break;
                case "4":
                    ret = "Aprilis";
                    break;
                case "5":
                    ret ="Majus";
                    break;
                case "6":
                    ret ="Junius";
                    break;
                case "7":
                    ret ="Julius";
                    break;
                case "8":
                    ret ="Augusztus";
                    break;
                case "9":
                    ret ="Szeptember";
                    break;
                case "10":
                    ret ="Oktober";
                    break;
                case "11":
                    ret ="November";
                    break;
                case "12":
                    ret = "December";
                    break;
            }
            return ret;
        }

        private void dataGV_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex > -1 )
            {
                DataRow[] aktRow = tableFo.Select("kod ='"+dataGV.Rows[e.RowIndex].Cells["kod"].Value.ToString()+"'");
                int aktRowIndex = tableFo.Rows.IndexOf(aktRow[0]);
                if (dataGV.Rows[e.RowIndex].Cells["bonthato"].Value.ToString() == "F")
                {
                    if (dataGV.Rows[e.RowIndex].Cells["bontva"].Value.ToString() == "N")
                    {
                        tableFo.Rows[aktRowIndex]["bontva"] = "I";
                        DataRow[] f = tableAl.Select("substring(kod,1,1)='" + dataGV.Rows[e.RowIndex].Cells["kod"].Value.ToString() + "'");
                        for (int i = 0; i < f.Length; i++)
                        {
                            DataRow r = tableFo.NewRow();
                            for (int m = 0; m < tableFo.Columns.Count; m++)
                                r[m] = f[i][m];
                            tableFo.Rows.Add(r);
                        }
                    }
                    else
                    {
                        DataRow[] f = tableFo.Select("substring(kod,1,1)='" + dataGV.Rows[e.RowIndex].Cells["kod"].Value.ToString() + "' and bonthato in('A','T')");
                        for (int i=0;i<f.Length;i++)
                            tableFo.Rows.Remove(f[i]);
                        tableFo.Rows[aktRowIndex]["bontva"] = "N";
                    }
                }
                else if (dataGV.Rows[e.RowIndex].Cells["bonthato"].Value.ToString() == "A")
                {
                    if (dataGV.Rows[e.RowIndex].Cells["bontva"].Value.ToString() == "N")
                    {
                        tableFo.Rows[aktRowIndex]["bontva"] = "I";
                        DataRow[] f = table.Select("substring(kod,1,2)='" + dataGV.Rows[e.RowIndex].Cells["kod"].Value.ToString() + "'");
                        for (int i = 0; i < f.Length; i++)
                        {
                            DataRow r = tableFo.NewRow();
                            for (int m = 0; m < tableFo.Columns.Count; m++)
                                r[m] = f[i][m];
                            tableFo.Rows.Add(r);
                        }
                    }
                    else
                    {
                        DataRow[] f = tableFo.Select("substring(kod,1,2)='" + dataGV.Rows[e.RowIndex].Cells["kod"].Value.ToString() + "' and bonthato = 'T'");
                        for (int i = 0; i < f.Length; i++)
                            tableFo.Rows.Remove(f[i]);
                        tableFo.Rows[aktRowIndex]["bontva"] = "N";
                    }

                }
                else if (dataGV.Rows[e.RowIndex].Cells["bonthato"].Value.ToString() == "T")
                {
                    tableTooltip.Clear();
                    string dat = honap.Value.ToShortDateString();
                    string tKod = dataGV.Rows[e.RowIndex].Cells["kod"].Value.ToString();
                    string sel = "select a.azonosito, d.azonosito as partner, a.megjegyzes, b.netto, b.brutto, a.fizetve, b.szoveg " +
                                 "from szamla a "+
                                 "left outer join partner d on a.pid =d.pid, " +
                                 "szamla_tetel b, kodtab c " +
                                 "where a.id = b.id and a.vs='" + jel + "' ";

                    if (comboPartner.SelectedItem != null && partner_id != String.Empty)
                        sel = sel + "and a.pid=" + partner_id + " ";
                    if (comboSzoveg.SelectedItem != null && comboSzoveg.SelectedItem.ToString() != String.Empty)
                        sel = sel + "and b.szoveg='" + comboSzoveg.SelectedItem.ToString() + "' ";

                    if (havi.Checked)
                        sel = sel + "and year(datum_telj) = year('" + dat + "') and month(datum_telj) = month('" + dat + "') ";
                    else
                        sel = sel + "and year(datum_telj) = '" + ev.Text + "' ";

                    sel = sel + "  and b.megnid = c.sorszam " +
                                "  and c.kod = '" + tKod + "' ";

                    da = new SqlDataAdapter(sel,myconn);
                    da.Fill(ds, "tableTooltip");
                    tableTooltip = ds.Tables["tableTooltip"];

                    alSzamlak = new AlSzamlak(tableTooltip,tKod);
                    alSzamlak.ShowDialog();
                }
            }
        }

        private void eves_CheckedChanged(object sender, EventArgs e)
        {
            tableFo.Clear();
            tableAl.Clear();
            table.Clear();
        }

        private void buttonNyomtat_Click(object sender, EventArgs e)
        {
            nyomtat = new PrintForm();
            string[] parNev = { "SZOVEG","EV"};
            string[] parVal = { "", ev.Text };
            string[] parTip = { "string", "string" };

            if (jel == "V")
                parVal[0] = "Bevételek"; 
            else
                parVal[0] = "Kiadások";
      
            DataSet dS = new MainProgramm.virDataSet();
            DataRow r;
            for (int i = 0; i < viewFo.Count; i++)
            {
                r = dS.Tables["HaviBontas"].NewRow();
                if (viewFo[i]["bonthato"].ToString() == "F")
                    r["kod"] = viewFo[i]["kod"].ToString();
                if (viewFo[i]["bonthato"].ToString() == "A")
                    r["kod"] = " " + viewFo[i]["kod"].ToString();
                if (viewFo[i]["bonthato"].ToString() == "T")
                    r["kod"] = "  " + viewFo[i]["kod"].ToString();

                r["megnevezes"] = viewFo[i]["megnevezes"];
                if (havi.Checked)
                {
                    for (int f = 2; f < dS.Tables["HaviBontas"].Columns.Count; f++)
                        r[f] = 0;
                    r[honap.Value.Month + 1] = viewFo[i]["netto"];
                }
                else
                {
                    r["januar"] = viewFo[i]["januar"];
                    r["februar"] = viewFo[i]["februar"];
                    r["marcius"] = viewFo[i]["marcius"];
                    r["aprilis"] = viewFo[i]["aprilis"];
                    r["majus"] = viewFo[i]["majus"];
                    r["junius"] = viewFo[i]["junius"];
                    r["julius"] = viewFo[i]["julius"];
                    r["augusztus"] = viewFo[i]["augusztus"];
                    r["szeptember"] = viewFo[i]["szeptember"];
                    r["oktober"] = viewFo[i]["oktober"];
                    r["november"] = viewFo[i]["november"];
                    r["december"] = viewFo[i]["december"];
                }
                dS.Tables["HaviBontas"].Rows.Add(r);
            }

            nyomtat.PrintParams(parNev, parVal, parTip);
            bevetelkiadasLista.SetDataSource(dS);
            bevetelkiadasLista.SetParameterValue("SZOVEG", parVal[0]);
            bevetelkiadasLista.SetParameterValue("EV", parVal[1]);

            nyomtat.reportSource = bevetelkiadasLista;
            nyomtat.DoPreview(mainForm.defPageSettings);
        }

        private void dataGV_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (dataGV.Rows[e.RowIndex].Cells["bonthato"].Value.ToString() == "A")
                {
                    e.CellStyle.BackColor = Color.LightGreen;
                }
                if (dataGV.Rows[e.RowIndex].Cells["bonthato"].Value.ToString() == "T")
                {
                    e.CellStyle.BackColor = Color.GreenYellow;
                }
            }
        }

        private void comboPartner_SelectedIndexChanged(object sender, EventArgs e)
        {
            partner_id = tablePartner.Rows.Find(comboPartner.SelectedItem)["pid"].ToString();
        }

        private void ev_SelectedIndexChanged(object sender, EventArgs e)
        {
            tablePartner.Clear();
            tableSzoveg.Clear();
            comboPartner.Items.Clear();
            comboSzoveg.Items.Clear();

            da = new SqlDataAdapter("select distinct partner.azonosito, partner.pid from szamla, partner " +
                                    "where szamla.pid = partner.pid and vs='" + jel + "' " +
                                    "and year(datum_telj) = '" + ev.Text + "' " +
                                    "order by azonosito "
                                   , myconn);
            da.Fill(ds, "tablePartner");
            tablePartner = ds.Tables["tablePartner"];
            for (int i = 0; i < tablePartner.Rows.Count; i++)
                comboPartner.Items.Add(tablePartner.Rows[i]["azonosito"].ToString());
            tablePartner.PrimaryKey = new DataColumn[] { tablePartner.Columns["azonosito"] };

            da = new SqlDataAdapter("select distinct szoveg from szamla a, szamla_tetel b " +
                                    "where a.id = b.id and vs='" + jel + "' " +
                                    "and year(datum_telj) = '" + ev.Text + "' " +
                                    "order by szoveg"
                                    , myconn);
            da.Fill(ds, "tableSzoveg");
            tableSzoveg = ds.Tables["tableSzoveg"];
            for (int i = 0; i < tableSzoveg.Rows.Count; i++)
                comboSzoveg.Items.Add(tableSzoveg.Rows[i]["szoveg"].ToString());

        }

        private void honap_ValueChanged(object sender, EventArgs e)
        {
            tablePartner.Clear();
            tableSzoveg.Clear();
            comboPartner.Items.Clear();
            comboSzoveg.Items.Clear();
            string dat = honap.Value.ToShortDateString();
            da = new SqlDataAdapter("select distinct partner.azonosito, partner.pid from szamla, partner " +
                                    "where szamla.pid = partner.pid and vs='" + jel + "' " +
                                    "and year(datum_telj) = year('" + dat + "') and month(datum_telj) = month('" + dat + "') " +
                                    "order by azonosito "
                                    , myconn);
            da.Fill(ds, "tablePartner");
            tablePartner = ds.Tables["tablePartner"];
            for (int i = 0; i < tablePartner.Rows.Count; i++)
                comboPartner.Items.Add(tablePartner.Rows[i]["azonosito"].ToString());
            tablePartner.PrimaryKey = new DataColumn[] { tablePartner.Columns["azonosito"] };

            da = new SqlDataAdapter("select distinct szoveg from szamla a, szamla_tetel b " +
                                    "where a.id = b.id and vs='" + jel + "' " +
                                    "and year(datum_telj) = year('" + dat + "') and month(datum_telj) = month('" + dat + "') " +
                                    "order by szoveg"
                                    , myconn);
            da.Fill(ds, "tableSzoveg");
            tableSzoveg = ds.Tables["tableSzoveg"];
            for (int i = 0; i < tableSzoveg.Rows.Count; i++)
                comboSzoveg.Items.Add(tableSzoveg.Rows[i]["szoveg"].ToString());

        }

    }
}