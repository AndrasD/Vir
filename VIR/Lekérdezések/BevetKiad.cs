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
    public partial class BevetKiad : UserControl
    {
        private VIR.MainForm mainForm;

        private SqlConnection myconn = new SqlConnection();
        private DataSet ds = new DataSet();
        private SqlDataAdapter da;

        private PrintForm nyomtat = new PrintForm();
        private BevetelKiadasLista bevetelkiadasLista = new BevetelKiadasLista();
        private BevetKiadLista bevetkiadLista = new BevetKiadLista();

        private DataTable tableEvek = new DataTable();
        private DataTable tableBevet = new DataTable();
        private DataView  viewBevet  = new DataView();
        private DataTable tableKiad = new DataTable();
        private DataView  viewKiad = new DataView();

        private DataTable tableTooltip = new DataTable();
        private DataTable tablePartner = new DataTable();
        private DataTable tableSzoveg = new DataTable();

        private string partner_id = "";
        private string eredmeny_szures = "";
        

        public BevetKiad(string szoveg, object[] obj)
        {
            MyTag[] myTag = (MyTag[])obj[1];

            InitializeComponent();

            osszeg1.Text = "0";
            osszeg2.Text = "0";
        }

        private void Szamlak_Load(object sender, EventArgs e)
        {
            mainForm = (VIR.MainForm)this.ParentForm;
            myconn = mainForm.MyConn;

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
            tableBevet.Clear();
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
            osszeg1.Text = "0";
            osszeg2.Text = "0";
            string sel = "";
            string dat = honap.Value.ToShortDateString();

            tableBevet.Clear();
            sel = "select kodtab.kod, partner.azonosito as Partner, datum_telj as TeljDatum, netto, brutto, szamla_tetel.szoveg ";
            sel = sel + "from szamla , szamla_tetel , partner, kodtab " +
                        "where szamla.vs='V' ";
            if (textBevetelkod.Text != String.Empty)
                sel = eredmeny_szuro(textBevetelkod,sel);

            if (havi.Checked)
                sel = sel + "and year(datum_telj) = year('" + dat + "') and month(datum_telj) = month('" + dat + "') ";
            else
                sel = sel + "and year(datum_telj) = '" + ev.Text + "' ";

            sel = sel + "and szamla.id=szamla_tetel.id " +
                        "and szamla.pid = partner.pid " +
                        "and szamla_tetel.megnid = kodtab.sorszam";

            if (comboPartner.Text != "" && partner_id != null)
                sel = sel + " and partner.pid = " + partner_id.ToString().Trim();
            if (comboSzoveg.Text != "")
                sel = sel + " and szamla_tetel.szoveg = '" + comboSzoveg.Text+"'";

            da = new SqlDataAdapter(sel, myconn);
            try
            {
                da.Fill(ds, "tableBevet");
                tableBevet = ds.Tables["tableBevet"];
                this.viewBevet.BeginInit();
                this.viewBevet.Table = tableBevet;
                this.viewBevet.EndInit();
                viewBevet.Sort = "kod, Partner, TeljDatum";
                dataGV.DataSource = this.viewBevet;
                dataGV.Columns[0].Frozen = true;
                dataGV.Columns["netto"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGV.Columns["brutto"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGV.Columns["netto"].DefaultCellStyle.Format = "N2";
                dataGV.Columns["brutto"].DefaultCellStyle.Format = "N2";
            }
            catch
            {
                MessageBox.Show("Hibás szűrés megadás!");
            }

            tableKiad.Clear();
            sel = "select kodtab.kod, partner.azonosito as Partner, datum_telj as TeljDatum, netto, brutto, szamla_tetel.szoveg ";
            sel = sel + "from szamla , szamla_tetel , partner, kodtab " +
                        "where szamla.vs='S' ";
            if (textBevetelkod.Text != String.Empty)
                sel = eredmeny_szuro(textKoltsegkod,sel);

            if (havi.Checked)
                sel = sel + "and year(datum_telj) = year('" + dat + "') and month(datum_telj) = month('" + dat + "') ";
            else
                sel = sel + "and year(datum_telj) = '" + ev.Text + "' ";

            sel = sel + "and szamla.id=szamla_tetel.id " +
                        "and szamla.pid = partner.pid "+
                        "and szamla_tetel.megnid = kodtab.sorszam";

            if (comboPartner.Text != "" && partner_id != null)
                sel = sel + " and partner.pid = " + partner_id.ToString().Trim();
            if (comboSzoveg.Text != "")
                sel = sel + " and szamla_tetel.szoveg = '" + comboSzoveg.Text+"'";

            da = new SqlDataAdapter(sel, myconn);
            try
            {
                da.Fill(ds, "tableKiad");
                tableKiad = ds.Tables["tableKiad"];
                this.viewKiad.BeginInit();
                this.viewKiad.Table = tableKiad;
                this.viewKiad.EndInit();
                viewKiad.Sort = "kod, Partner, TeljDatum";
                dataGV2.DataSource = this.viewKiad;
                dataGV2.Columns[0].Frozen = true;
                dataGV2.Columns["netto"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGV2.Columns["brutto"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGV2.Columns["netto"].DefaultCellStyle.Format = "N2";
                dataGV2.Columns["brutto"].DefaultCellStyle.Format = "N2";
            }
            catch
            {
                MessageBox.Show("Hibás szűrés megadás!");
            }

            for (int i = 0; i < tableBevet.Rows.Count; i++)
                osszeg1.Text = Convert.ToString(Convert.ToInt32(osszeg1.Text) + Convert.ToInt32(tableBevet.Rows[i]["netto"].ToString()));
            for (int i = 0; i < tableKiad.Rows.Count; i++)
                osszeg2.Text = Convert.ToString(Convert.ToInt32(osszeg2.Text) + Convert.ToInt32(tableKiad.Rows[i]["netto"].ToString()));
            egyenleg.Text = string.Format("{0:N}", Convert.ToInt32(osszeg1.Text) - Convert.ToInt32(osszeg2.Text));
            osszeg1.Text = string.Format("{0:N}", Convert.ToInt32(osszeg1.Text));
            osszeg2.Text = string.Format("{0:N}", Convert.ToInt32(osszeg2.Text));
        }

        private void dataGV_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                //tableTooltip.Clear();
                //string dat = honap.Value.ToShortDateString();
                //string tKod = dataGV.Rows[e.RowIndex].Cells["kod"].Value.ToString();
                //string sel = "select a.azonosito, d.azonosito as partner, a.megjegyzes, b.netto, b.brutto, a.fizetve, b.szoveg " +
                //             "from szamla a " +
                //             "left outer join partner d on a.pid =d.pid, " +
                //             "szamla_tetel b, kodtab c " +
                //             "where a.id = b.id and a.vs='V' ";

                //if (comboPartner.SelectedItem != null && partner_id != String.Empty)
                //    sel = sel + "and a.pid=" + partner_id + " ";
                //if (comboSzoveg.SelectedItem != null && comboSzoveg.SelectedItem.ToString() != String.Empty)
                //    sel = sel + "and b.szoveg='" + comboSzoveg.SelectedItem.ToString() + "' ";

                //if (havi.Checked)
                //    sel = sel + "and year(datum_telj) = year('" + dat + "') and month(datum_telj) = month('" + dat + "') ";
                //else
                //    sel = sel + "and year(datum_telj) = '" + ev.Text + "' ";

                //sel = sel + "  and b.megnid = c.sorszam " +
                //            "  and c.kod = '" + tKod + "' ";

                //da = new SqlDataAdapter(sel, myconn);
                //da.Fill(ds, "tableTooltip");
                //tableTooltip = ds.Tables["tableTooltip"];

                //alSzamlak = new AlSzamlak(tableTooltip, tKod);
                //alSzamlak.ShowDialog();
            }
        }

        private void eves_CheckedChanged(object sender, EventArgs e)
        {
            tableBevet.Clear();
        }

        private void dataGV_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                //if (dataGV.Rows[e.RowIndex].Cells["bonthato"].Value.ToString() == "A")
                //{
                //    e.CellStyle.BackColor = Color.LightGreen;
                //}
                //if (dataGV.Rows[e.RowIndex].Cells["bonthato"].Value.ToString() == "T")
                //{
                //    e.CellStyle.BackColor = Color.GreenYellow;
                //}
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
                                    "where szamla.pid = partner.pid and vs='V' " +
                                    "and year(datum_telj) = '" + ev.Text + "' " +
                                    "order by azonosito "
                                   , myconn);
            da.Fill(ds, "tablePartner");
            tablePartner = ds.Tables["tablePartner"];
            for (int i = 0; i < tablePartner.Rows.Count; i++)
                comboPartner.Items.Add(tablePartner.Rows[i]["azonosito"].ToString());
            tablePartner.PrimaryKey = new DataColumn[] { tablePartner.Columns["azonosito"] };

            da = new SqlDataAdapter("select distinct szoveg from szamla a, szamla_tetel b " +
                                    "where a.id = b.id and vs='V' " +
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
                                    "where szamla.pid = partner.pid and vs='V' " +
                                    "and year(datum_telj) = year('" + dat + "') and month(datum_telj) = month('" + dat + "') " +
                                    "order by azonosito "
                                    , myconn);
            da.Fill(ds, "tablePartner");
            tablePartner = ds.Tables["tablePartner"];
            for (int i = 0; i < tablePartner.Rows.Count; i++)
                comboPartner.Items.Add(tablePartner.Rows[i]["azonosito"].ToString());
            tablePartner.PrimaryKey = new DataColumn[] { tablePartner.Columns["azonosito"] };

            da = new SqlDataAdapter("select distinct szoveg from szamla a, szamla_tetel b " +
                                    "where a.id = b.id and vs='V' " +
                                    "and year(datum_telj) = year('" + dat + "') and month(datum_telj) = month('" + dat + "') " +
                                    "order by szoveg"
                                    , myconn);
            da.Fill(ds, "tableSzoveg");
            tableSzoveg = ds.Tables["tableSzoveg"];
            for (int i = 0; i < tableSzoveg.Rows.Count; i++)
                comboSzoveg.Items.Add(tableSzoveg.Rows[i]["szoveg"].ToString());

        }

        private string eredmeny_szuro(TextBox tx, String sel)
        {
            string s = "";
            int hossz = 0;
            bool talalt = false;
            string egyesek = "";
            string kettesek = "";
            string negyesek = "";
            string k_egyesek = "";
            string k_kettesek = "";
            string k_negyesek = "";
            string kod = "";
            bool kiveve = false;
            bool like = false;
            string l_like = "";

            if (tx.Text.Length > 0)
            {
                s = tx.Text.Trim();
                if (tx.Text.IndexOf(",") > -1) // van benne vessző, azaz felsorolás
                {
                    for (int i = 0; i < s.Length; i++)
                    {
                        if (s.Substring(i, 1) == "-")
                            kiveve = true;
                        else if (s.Substring(i, 1) != "," || i == s.Length - 1)
                            kod = kod + s.Substring(i, 1);

                        if (s.Substring(i, 1) == "," || i == s.Length - 1)
                        {
                            if (kod.Length == 1)
                            {
                                if (kiveve)
                                {
                                    if (k_egyesek.Length > 0)
                                        k_egyesek = k_egyesek + ",";
                                    k_egyesek = k_egyesek + "'" + kod + "'";

                                }
                                else
                                {
                                    if (egyesek.Length > 0)
                                        egyesek = egyesek + ",";
                                    egyesek = egyesek + "'" + kod + "'";
                                }
                            }

                            if (kod.Length == 2)
                            {
                                if (kiveve)
                                {
                                    if (k_kettesek.Length > 0)
                                        k_kettesek = k_kettesek + ",";
                                    k_kettesek = k_kettesek + "'" + kod + "'";
                                }
                                else
                                {
                                    if (kettesek.Length > 0)
                                        kettesek = kettesek + ",";
                                    kettesek = kettesek + "'" + kod + "'";
                                }
                            }

                            if (kod.Length == 4)
                            {
                                if (kiveve)
                                {
                                    if (k_negyesek.Length > 0)
                                        k_negyesek = k_negyesek + ",";
                                    k_negyesek = k_negyesek + "'" + kod + "'";
                                }
                                else
                                {
                                    if (negyesek.Length > 0)
                                        negyesek = negyesek + ",";
                                    negyesek = negyesek + "'" + kod + "'";
                                }
                            }
                            kod = "";
                            kiveve = false;
                            like = false;
                        }
                    }

                    if (egyesek.Length > 0 || kettesek.Length > 0 || negyesek.Length > 0)
                    {
                        talalt = false;
                        sel = sel + " and (";
                        if (egyesek.Length > 0)
                        {
                            hossz = 1;
                            talalt = true;
                            sel = sel + "substring(kodtab.kod,1," + hossz.ToString().Trim() + ") in(" + egyesek + ")";
                        }
                        if (kettesek.Length > 0)
                        {
                            hossz = 2;
                            if (talalt)
                                sel = sel + " or ";
                            talalt = true;
                            sel = sel + "substring(kodtab.kod,1," + hossz.ToString().Trim() + ") in(" + kettesek + ")";
                        }
                        if (negyesek.Length > 0)
                        {
                            hossz = 4;
                            if (talalt)
                                sel = sel + " or ";
                            talalt = true;
                            sel = sel + "kodtab.kod in(" + negyesek + ")";
                        }
                        sel = sel + ") ";
                    }

                    if (k_egyesek.Length > 0 || k_kettesek.Length > 0 || k_negyesek.Length > 0)
                    {
                        talalt = false;
                        sel = sel + " and (";
                        if (k_egyesek.Length > 0)
                        {
                            hossz = 1;
                            talalt = true;
                            sel = sel + "substring(kodtab.kod,1," + hossz.ToString().Trim() + ") not in(" + k_egyesek + ")";
                        }
                        if (k_kettesek.Length > 0)
                        {
                            hossz = 2;
                            if (talalt)
                                sel = sel + " or ";
                            talalt = true;
                            sel = sel + "substring(kodtab.kod,1," + hossz.ToString().Trim() + ") not in(" + k_kettesek + ")";
                        }
                        if (k_negyesek.Length > 0)
                        {
                            hossz = 4;
                            if (talalt)
                                sel = sel + " or ";
                            talalt = true;
                            sel = sel + "kodtab.kod not in(" + k_negyesek + ")";
                        }
                        sel = sel + ") ";
                    }
                }
                else // nincs vessző
                {
                    if (tx.Text.IndexOf("_") > -1 || tx.Text.IndexOf("%") > -1)
                    {
                        sel = sel + " and kodtab.kod like '" + s + "' ";
                    }
                    else
                    {
                        if (s.Substring(0, 1) == "-")
                        {
                            hossz = s.Substring(1).Length;
                            if (hossz == 4)
                                sel = sel + " and kodtab.kod not in('" + s.Substring(1) + "') ";
                            else
                                sel = sel + " and substring(kodtab.kod,1," + hossz.ToString().Trim() + ") not in('" + s.Substring(1) + "') ";
                        }
                        else
                        {
                            hossz = s.Length;
                            if (hossz == 4)
                                sel = sel + " and kodtab.kod in('" + s + "') ";
                            else
                                sel = sel + " and substring(kodtab.kod,1," + hossz.ToString().Trim() + ") in('" + s + "') ";
                        }
                    }
                }
            }
            return sel;
        }

        private void buttonNyomtat_Click_1(object sender, EventArgs e)
        {
            nyomtat = new PrintForm();
            string[] parNev = { "SZOVEG2" };
            string[] parVal = { "Bevétel" };
            string[] parTip = { "string" };

            nyomtat.PrintParams(parNev, parVal, parTip);
            bevetkiadLista.SetDataSource(tableBevet);
            bevetkiadLista.SetParameterValue("SZOVEG2", parVal[0]);
            nyomtat.reportSource = bevetkiadLista;
            nyomtat.DoPreview(mainForm.defPageSettings);

            nyomtat = new PrintForm();
            nyomtat.PrintParams(parNev, parVal, parTip);
            bevetkiadLista.SetDataSource(tableKiad);
            parVal[0] = "Kiadás";
            bevetkiadLista.SetParameterValue("SZOVEG2", parVal[0]);
            nyomtat.reportSource = bevetkiadLista;
            nyomtat.DoPreview(mainForm.defPageSettings);

        }

        private void buttonNyomtat_Click(object sender, EventArgs e)
        {
            nyomtat = new PrintForm();
            string[] parNev = { "SZOVEG", "EV" };
            string[] parVal = { "", ev.Text };
            string[] parTip = { "string", "string" };


            DataSet dS = new MainProgramm.virDataSet();
            DataRow r;
            for (int i = 0; i < viewBevet.Count; i++)
            {
                r = dS.Tables["HaviBontas"].NewRow();
                if (viewBevet[i]["bonthato"].ToString() == "F")
                    r["kod"] = viewBevet[i]["kod"].ToString();
                if (viewBevet[i]["bonthato"].ToString() == "A")
                    r["kod"] = " " + viewBevet[i]["kod"].ToString();
                if (viewBevet[i]["bonthato"].ToString() == "T")
                    r["kod"] = "  " + viewBevet[i]["kod"].ToString();

                r["megnevezes"] = viewBevet[i]["megnevezes"];
                if (havi.Checked)
                {
                    for (int f = 2; f < dS.Tables["HaviBontas"].Columns.Count; f++)
                        r[f] = 0;
                    r[honap.Value.Month + 1] = viewBevet[i]["netto"];
                }
                else
                {
                    r["januar"] = viewBevet[i]["januar"];
                    r["februar"] = viewBevet[i]["februar"];
                    r["marcius"] = viewBevet[i]["marcius"];
                    r["aprilis"] = viewBevet[i]["aprilis"];
                    r["majus"] = viewBevet[i]["majus"];
                    r["junius"] = viewBevet[i]["junius"];
                    r["julius"] = viewBevet[i]["julius"];
                    r["augusztus"] = viewBevet[i]["augusztus"];
                    r["szeptember"] = viewBevet[i]["szeptember"];
                    r["oktober"] = viewBevet[i]["oktober"];
                    r["november"] = viewBevet[i]["november"];
                    r["december"] = viewBevet[i]["december"];
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

    }
}