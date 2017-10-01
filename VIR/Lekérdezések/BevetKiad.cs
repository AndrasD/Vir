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
            int num;
            this.mainForm = (VIR.MainForm)base.ParentForm;
            this.myconn = this.mainForm.MyConn;
            this.da = new SqlDataAdapter("select distinct year(datum_telj) as ev from szamla", this.myconn);
            this.da.Fill(this.ds, "tableEvek");
            this.tableEvek = this.ds.Tables["tableEvek"];
            for (num = 0; num < this.tableEvek.Rows.Count; num++)
            {
                this.ev.Items.Add(this.tableEvek.Rows[num]["ev"].ToString());
            }
            string str = this.honap.Value.ToShortDateString();
            string str2 = this.honapig.Value.ToShortDateString();
            this.da = new SqlDataAdapter("select distinct partner.azonosito, partner.pid from szamla, partner where szamla.pid = partner.pid and cast(datum_telj as datetime) >= cast('" + str + "' as datetime) and cast(datum_telj as datetime) <= cast('" + str2 + "' as datetime) order by azonosito ", this.myconn);
            this.da.Fill(this.ds, "tablePartner");
            this.tablePartner = this.ds.Tables["tablePartner"];
            for (num = 0; num < this.tablePartner.Rows.Count; num++)
            {
                this.comboPartner.Items.Add(this.tablePartner.Rows[num]["azonosito"].ToString());
            }
            this.tablePartner.PrimaryKey = new DataColumn[] { this.tablePartner.Columns["azonosito"] };
            this.da = new SqlDataAdapter("select distinct szoveg from szamla a, szamla_tetel b where a.id = b.id and cast(datum_telj as datetime) >= cast('" + str + "' as datetime) and cast(datum_telj as datetime) <= cast('" + str2 + "' as datetime) order by szoveg", this.myconn);
            this.da.Fill(this.ds, "tableSzoveg");
            this.tableSzoveg = this.ds.Tables["tableSzoveg"];
            for (num = 0; num < this.tableSzoveg.Rows.Count; num++)
            {
                this.comboSzoveg.Items.Add(this.tableSzoveg.Rows[num]["szoveg"].ToString());
            }
        }

        private void havi_CheckedChanged(object sender, EventArgs e)
        {
            this.tableBevet.Clear();
            if (this.havi.Checked)
            {
                this.honap.Enabled = true;
                this.honapig.Enabled = true;
                this.ev.Enabled = false;
            }
            else
            {
                this.honap.Enabled = false;
                this.honapig.Enabled = false;
                this.ev.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int num;
            this.osszeg1.Text = "0";
            this.osszeg2.Text = "0";
            string sel = "";
            string str2 = this.honap.Value.ToShortDateString();
            string str3 = this.honapig.Value.ToShortDateString();
            this.tableBevet.Clear();
            sel = "select kodtab.kod, partner.azonosito as Partner, datum_telj as TeljDatum, netto, brutto, szamla_tetel.szoveg ";
            sel = sel + "from szamla , szamla_tetel , partner, kodtab where szamla.vs='V' ";
            if (this.textBevetelkod.Text != string.Empty)
            {
                sel = this.eredmeny_szuro(this.textBevetelkod, sel);
            }
            if (this.havi.Checked)
            {
                sel = sel + "and cast(datum_telj as datetime) >= cast('" + str2 + "' as datetime) and cast(datum_telj as datetime) <= cast('" + str3 + "' as datetime) ";
            }
            else
            {
                sel = sel + "and year(datum_telj) = '" + this.ev.Text + "' ";
            }
            sel = sel + "and szamla.id=szamla_tetel.id and szamla.pid = partner.pid and szamla_tetel.megnid = kodtab.sorszam";
            if ((this.comboPartner.Text != "") && (this.partner_id != null))
            {
                sel = sel + " and partner.pid = " + this.partner_id.ToString().Trim();
            }
            if (this.comboSzoveg.Text != "")
            {
                sel = sel + " and szamla_tetel.szoveg = '" + this.comboSzoveg.Text + "'";
            }
            this.da = new SqlDataAdapter(sel, this.myconn);
            try
            {
                this.da.Fill(this.ds, "tableBevet");
                this.tableBevet = this.ds.Tables["tableBevet"];
                this.viewBevet.BeginInit();
                this.viewBevet.Table = this.tableBevet;
                this.viewBevet.EndInit();
                this.viewBevet.Sort = "kod, Partner, TeljDatum";
                this.dataGV.DataSource = this.viewBevet;
                this.dataGV.Columns[0].Frozen = true;
                this.dataGV.Columns["netto"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dataGV.Columns["brutto"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dataGV.Columns["netto"].DefaultCellStyle.Format = "N2";
                this.dataGV.Columns["brutto"].DefaultCellStyle.Format = "N2";
            }
            catch
            {
                MessageBox.Show("Hib\x00e1s szűr\x00e9s megad\x00e1s!");
            }
            this.tableKiad.Clear();
            sel = "select kodtab.kod, partner.azonosito as Partner, datum_telj as TeljDatum, netto, brutto, szamla_tetel.szoveg ";
            sel = sel + "from szamla , szamla_tetel , partner, kodtab where szamla.vs='S' ";
            if (this.textBevetelkod.Text != string.Empty)
            {
                sel = this.eredmeny_szuro(this.textKoltsegkod, sel);
            }
            if (this.havi.Checked)
            {
                sel = sel + "and cast(datum_telj as datetime) >= cast('" + str2 + "' as datetime) and cast(datum_telj as datetime) <= cast('" + str3 + "' as datetime) ";
            }
            else
            {
                sel = sel + "and year(datum_telj) = '" + this.ev.Text + "' ";
            }
            sel = sel + "and szamla.id=szamla_tetel.id and szamla.pid = partner.pid and szamla_tetel.megnid = kodtab.sorszam";
            if ((this.comboPartner.Text != "") && (this.partner_id != null))
            {
                sel = sel + " and partner.pid = " + this.partner_id.ToString().Trim();
            }
            if (this.comboSzoveg.Text != "")
            {
                sel = sel + " and szamla_tetel.szoveg = '" + this.comboSzoveg.Text + "'";
            }
            this.da = new SqlDataAdapter(sel, this.myconn);
            try
            {
                this.da.Fill(this.ds, "tableKiad");
                this.tableKiad = this.ds.Tables["tableKiad"];
                this.viewKiad.BeginInit();
                this.viewKiad.Table = this.tableKiad;
                this.viewKiad.EndInit();
                this.viewKiad.Sort = "kod, Partner, TeljDatum";
                this.dataGV2.DataSource = this.viewKiad;
                this.dataGV2.Columns[0].Frozen = true;
                this.dataGV2.Columns["netto"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dataGV2.Columns["brutto"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dataGV2.Columns["netto"].DefaultCellStyle.Format = "N2";
                this.dataGV2.Columns["brutto"].DefaultCellStyle.Format = "N2";
            }
            catch
            {
                MessageBox.Show("Hib\x00e1s szűr\x00e9s megad\x00e1s!");
            }
            for (num = 0; num < this.tableBevet.Rows.Count; num++)
            {
                this.osszeg1.Text = Convert.ToString((int)(Convert.ToInt32(this.osszeg1.Text) + Convert.ToInt32(this.tableBevet.Rows[num]["netto"].ToString())));
            }
            for (num = 0; num < this.tableKiad.Rows.Count; num++)
            {
                this.osszeg2.Text = Convert.ToString((int)(Convert.ToInt32(this.osszeg2.Text) + Convert.ToInt32(this.tableKiad.Rows[num]["netto"].ToString())));
            }
            this.egyenleg.Text = $"{Convert.ToInt32(this.osszeg1.Text) - Convert.ToInt32(this.osszeg2.Text):N}";
            this.osszeg1.Text = $"{Convert.ToInt32(this.osszeg1.Text):N}";
            this.osszeg2.Text = $"{Convert.ToInt32(this.osszeg2.Text):N}";
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
            int num;
            this.tablePartner.Clear();
            this.tableSzoveg.Clear();
            this.comboPartner.Items.Clear();
            this.comboSzoveg.Items.Clear();
            this.da = new SqlDataAdapter("select distinct partner.azonosito, partner.pid from szamla, partner where szamla.pid = partner.pid and vs='V' and year(datum_telj) = '" + this.ev.Text + "' order by azonosito ", this.myconn);
            this.da.Fill(this.ds, "tablePartner");
            this.tablePartner = this.ds.Tables["tablePartner"];
            for (num = 0; num < this.tablePartner.Rows.Count; num++)
            {
                this.comboPartner.Items.Add(this.tablePartner.Rows[num]["azonosito"].ToString());
            }
            this.tablePartner.PrimaryKey = new DataColumn[] { this.tablePartner.Columns["azonosito"] };
            this.da = new SqlDataAdapter("select distinct szoveg from szamla a, szamla_tetel b where a.id = b.id and vs='V' and year(datum_telj) = '" + this.ev.Text + "' order by szoveg", this.myconn);
            this.da.Fill(this.ds, "tableSzoveg");
            this.tableSzoveg = this.ds.Tables["tableSzoveg"];
            for (num = 0; num < this.tableSzoveg.Rows.Count; num++)
            {
                this.comboSzoveg.Items.Add(this.tableSzoveg.Rows[num]["szoveg"].ToString());
            }
        }

        private void honap_ValueChanged(object sender, EventArgs e)
        {
            int num;
            this.tablePartner.Clear();
            this.tableSzoveg.Clear();
            this.comboPartner.Items.Clear();
            this.comboSzoveg.Items.Clear();
            string str = this.honap.Value.ToShortDateString();
            string str2 = this.honapig.Value.ToShortDateString();
            this.da = new SqlDataAdapter("select distinct partner.azonosito, partner.pid from szamla, partner where szamla.pid = partner.pid and vs='V' and cast(datum_telj as datetime) >= cast('" + str + "' as datetime) and cast(datum_telj as datetime) <= cast('" + str2 + "' as datetime) order by azonosito ", this.myconn);
            this.da.Fill(this.ds, "tablePartner");
            this.tablePartner = this.ds.Tables["tablePartner"];
            for (num = 0; num < this.tablePartner.Rows.Count; num++)
            {
                this.comboPartner.Items.Add(this.tablePartner.Rows[num]["azonosito"].ToString());
            }
            this.tablePartner.PrimaryKey = new DataColumn[] { this.tablePartner.Columns["azonosito"] };
            this.da = new SqlDataAdapter("select distinct szoveg from szamla a, szamla_tetel b where a.id = b.id and vs='V' and cast(datum_telj as datetime) >= cast('" + str + "' as datetime) and cast(datum_telj as datetime) <= cast('" + str2 + "' as datetime) order by szoveg", this.myconn);
            this.da.Fill(this.ds, "tableSzoveg");
            this.tableSzoveg = this.ds.Tables["tableSzoveg"];
            for (num = 0; num < this.tableSzoveg.Rows.Count; num++)
            {
                this.comboSzoveg.Items.Add(this.tableSzoveg.Rows[num]["szoveg"].ToString());
            }
        }

        private void honapig_ValueChanged(object sender, EventArgs e)
        {
            int num;
            this.tablePartner.Clear();
            this.tableSzoveg.Clear();
            this.comboPartner.Items.Clear();
            this.comboSzoveg.Items.Clear();
            string str = this.honap.Value.ToShortDateString();
            string str2 = this.honapig.Value.ToShortDateString();
            this.da = new SqlDataAdapter("select distinct partner.azonosito, partner.pid from szamla, partner where szamla.pid = partner.pid and vs='V' and cast(datum_telj as datetime) >= cast('" + str + "' as datetime) and cast(datum_telj as datetime) <= cast('" + str2 + "' as datetime) order by azonosito ", this.myconn);
            this.da.Fill(this.ds, "tablePartner");
            this.tablePartner = this.ds.Tables["tablePartner"];
            for (num = 0; num < this.tablePartner.Rows.Count; num++)
            {
                this.comboPartner.Items.Add(this.tablePartner.Rows[num]["azonosito"].ToString());
            }
            this.tablePartner.PrimaryKey = new DataColumn[] { this.tablePartner.Columns["azonosito"] };
            this.da = new SqlDataAdapter("select distinct szoveg from szamla a, szamla_tetel b where a.id = b.id and vs='V' and cast(datum_telj as datetime) >= cast('" + str + "' as datetime) and cast(datum_telj as datetime) <= cast('" + str2 + "' as datetime) order by szoveg", this.myconn);
            this.da.Fill(this.ds, "tableSzoveg");
            this.tableSzoveg = this.ds.Tables["tableSzoveg"];
            for (num = 0; num < this.tableSzoveg.Rows.Count; num++)
            {
                this.comboSzoveg.Items.Add(this.tableSzoveg.Rows[num]["szoveg"].ToString());
            }
        }

        private string eredmeny_szuro(TextBox tx, string sel)
        {
            string str = "";
            int length = 0;
            bool flag = false;
            string str2 = "";
            string str3 = "";
            string str4 = "";
            string str5 = "";
            string str6 = "";
            string str7 = "";
            string str8 = "";
            bool flag2 = false;
            bool flag3 = false;
            if (tx.Text.Length > 0)
            {
                str = tx.Text.Trim();
                if (tx.Text.IndexOf(",") > -1)
                {
                    for (int i = 0; i < str.Length; i++)
                    {
                        if (str.Substring(i, 1) == "-")
                        {
                            flag2 = true;
                        }
                        else if ((str.Substring(i, 1) != ",") || (i == (str.Length - 1)))
                        {
                            str8 = str8 + str.Substring(i, 1);
                        }
                        if ((str.Substring(i, 1) == ",") || (i == (str.Length - 1)))
                        {
                            if (str8.Length == 1)
                            {
                                if (flag2)
                                {
                                    if (str5.Length > 0)
                                    {
                                        str5 = str5 + ",";
                                    }
                                    str5 = str5 + "'" + str8 + "'";
                                }
                                else
                                {
                                    if (str2.Length > 0)
                                    {
                                        str2 = str2 + ",";
                                    }
                                    str2 = str2 + "'" + str8 + "'";
                                }
                            }
                            if (str8.Length == 2)
                            {
                                if (flag2)
                                {
                                    if (str6.Length > 0)
                                    {
                                        str6 = str6 + ",";
                                    }
                                    str6 = str6 + "'" + str8 + "'";
                                }
                                else
                                {
                                    if (str3.Length > 0)
                                    {
                                        str3 = str3 + ",";
                                    }
                                    str3 = str3 + "'" + str8 + "'";
                                }
                            }
                            if (str8.Length == 4)
                            {
                                if (flag2)
                                {
                                    if (str7.Length > 0)
                                    {
                                        str7 = str7 + ",";
                                    }
                                    str7 = str7 + "'" + str8 + "'";
                                }
                                else
                                {
                                    if (str4.Length > 0)
                                    {
                                        str4 = str4 + ",";
                                    }
                                    str4 = str4 + "'" + str8 + "'";
                                }
                            }
                            str8 = "";
                            flag2 = false;
                            flag3 = false;
                        }
                    }
                    if (((str2.Length > 0) || (str3.Length > 0)) || (str4.Length > 0))
                    {
                        flag = false;
                        sel = sel + " and (";
                        if (str2.Length > 0)
                        {
                            length = 1;
                            flag = true;
                            sel = sel + "substring(kodtab.kod,1," + length.ToString().Trim() + ") in(" + str2 + ")";
                        }
                        if (str3.Length > 0)
                        {
                            length = 2;
                            if (flag)
                            {
                                sel = sel + " or ";
                            }
                            flag = true;
                            sel = sel + "substring(kodtab.kod,1," + length.ToString().Trim() + ") in(" + str3 + ")";
                        }
                        if (str4.Length > 0)
                        {
                            length = 4;
                            if (flag)
                            {
                                sel = sel + " or ";
                            }
                            flag = true;
                            sel = sel + "kodtab.kod in(" + str4 + ")";
                        }
                        sel = sel + ") ";
                    }
                    if (((str5.Length > 0) || (str6.Length > 0)) || (str7.Length > 0))
                    {
                        flag = false;
                        sel = sel + " and (";
                        if (str5.Length > 0)
                        {
                            length = 1;
                            flag = true;
                            sel = sel + "substring(kodtab.kod,1," + length.ToString().Trim() + ") not in(" + str5 + ")";
                        }
                        if (str6.Length > 0)
                        {
                            length = 2;
                            if (flag)
                            {
                                sel = sel + " or ";
                            }
                            flag = true;
                            sel = sel + "substring(kodtab.kod,1," + length.ToString().Trim() + ") not in(" + str6 + ")";
                        }
                        if (str7.Length > 0)
                        {
                            length = 4;
                            if (flag)
                            {
                                sel = sel + " or ";
                            }
                            flag = true;
                            sel = sel + "kodtab.kod not in(" + str7 + ")";
                        }
                        sel = sel + ") ";
                    }
                    return sel;
                }
                if ((tx.Text.IndexOf("_") > -1) || (tx.Text.IndexOf("%") > -1))
                {
                    sel = sel + " and kodtab.kod like '" + str + "' ";
                    return sel;
                }
                if (str.Substring(0, 1) == "-")
                {
                    length = str.Substring(1).Length;
                    if (length == 4)
                    {
                        sel = sel + " and kodtab.kod not in('" + str.Substring(1) + "') ";
                        return sel;
                    }
                    sel = sel + " and substring(kodtab.kod,1," + length.ToString().Trim() + ") not in('" + str.Substring(1) + "') ";
                    return sel;
                }
                length = str.Length;
                if (length == 4)
                {
                    sel = sel + " and kodtab.kod in('" + str + "') ";
                    return sel;
                }
                sel = sel + " and substring(kodtab.kod,1," + length.ToString().Trim() + ") in('" + str + "') ";
            }
            return sel;
        }

        private void buttonNyomtat_Click_1(object sender, EventArgs e)
        {
            this.nyomtat = new PrintForm();
            string[] parName = new string[] { "SZOVEG2" };
            string[] parValue = new string[] { "Bev\x00e9tel" };
            string[] parTyp = new string[] { "string" };
            this.nyomtat.PrintParams(parName, parValue, parTyp);
            this.bevetkiadLista.SetDataSource(this.tableBevet);
            this.bevetkiadLista.SetParameterValue("SZOVEG2", parValue[0]);
            this.nyomtat.reportSource = this.bevetkiadLista;
            this.nyomtat.DoPreview(this.mainForm.defPageSettings);
            this.nyomtat = new PrintForm();
            this.nyomtat.PrintParams(parName, parValue, parTyp);
            this.bevetkiadLista.SetDataSource(this.tableKiad);
            parValue[0] = "Kiad\x00e1s";
            this.bevetkiadLista.SetParameterValue("SZOVEG2", parValue[0]);
            this.nyomtat.reportSource = this.bevetkiadLista;
            this.nyomtat.DoPreview(this.mainForm.defPageSettings);
        }

        private void buttonNyomtat_Click(object sender, EventArgs e)
        {
            this.nyomtat = new PrintForm();
            string[] parName = new string[] { "SZOVEG", "EV" };
            string[] parValue = new string[] { "", this.ev.Text };
            string[] parTyp = new string[] { "string", "string" };
            DataSet dataSet = new MainProgramm.virDataSet();
            for (int i = 0; i < this.viewBevet.Count; i++)
            {
                DataRow row = dataSet.Tables["HaviBontas"].NewRow();
                if (this.viewBevet[i]["bonthato"].ToString() == "F")
                {
                    row["kod"] = this.viewBevet[i]["kod"].ToString();
                }
                if (this.viewBevet[i]["bonthato"].ToString() == "A")
                {
                    row["kod"] = " " + this.viewBevet[i]["kod"].ToString();
                }
                if (this.viewBevet[i]["bonthato"].ToString() == "T")
                {
                    row["kod"] = "  " + this.viewBevet[i]["kod"].ToString();
                }
                row["megnevezes"] = this.viewBevet[i]["megnevezes"];
                if (this.havi.Checked)
                {
                    for (int j = 2; j < dataSet.Tables["HaviBontas"].Columns.Count; j++)
                    {
                        row[j] = 0;
                    }
                    row[this.honap.Value.Month + 1] = this.viewBevet[i]["netto"];
                }
                else
                {
                    row["januar"] = this.viewBevet[i]["januar"];
                    row["februar"] = this.viewBevet[i]["februar"];
                    row["marcius"] = this.viewBevet[i]["marcius"];
                    row["aprilis"] = this.viewBevet[i]["aprilis"];
                    row["majus"] = this.viewBevet[i]["majus"];
                    row["junius"] = this.viewBevet[i]["junius"];
                    row["julius"] = this.viewBevet[i]["julius"];
                    row["augusztus"] = this.viewBevet[i]["augusztus"];
                    row["szeptember"] = this.viewBevet[i]["szeptember"];
                    row["oktober"] = this.viewBevet[i]["oktober"];
                    row["november"] = this.viewBevet[i]["november"];
                    row["december"] = this.viewBevet[i]["december"];
                }
                dataSet.Tables["HaviBontas"].Rows.Add(row);
            }
            this.nyomtat.PrintParams(parName, parValue, parTyp);
            this.bevetelkiadasLista.SetDataSource(dataSet);
            this.bevetelkiadasLista.SetParameterValue("SZOVEG", parValue[0]);
            this.bevetelkiadasLista.SetParameterValue("EV", parValue[1]);
            this.nyomtat.reportSource = this.bevetelkiadasLista;
            this.nyomtat.DoPreview(this.mainForm.defPageSettings);
        }
    }
}