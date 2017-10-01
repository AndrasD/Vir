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
            MyTag[] tagArray = (MyTag[])obj[1];
            this.ktfotag = tagArray[0];
            this.ktaltag = tagArray[1];
            this.kttag = tagArray[2];
            this.jel = obj[2].ToString();
            this.InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int num;
            string str3;
            string str4;
            string str5;
            string selectCommandText = "";
            string str2 = this.honap.Value.ToShortDateString();
            this.tableKoztes.Clear();
            this.tableFo.Clear();
            this.tableAl.Clear();
            this.table.Clear();
            for (num = 0; num < this.ktfotabla.Rows.Count; num++)
            {
                str3 = this.ktfotabla.Rows[num]["kod"].ToString();
                str4 = this.ktfotabla.Rows[num]["szoveg"].ToString();
                str5 = this.ktfotabla.Rows[num]["sorszam"].ToString();
                if (this.havi.Checked)
                {
                    selectCommandText = "select substring(c.kod,1,1) as Kod, '" + str4 + "' as Megnevezes, sum(b.netto) as Netto ";
                }
                else
                {
                    selectCommandText = "select substring(c.kod,1,1) as Kod, month(datum_telj) as ho, '" + str4 + "' as Megnevezes, sum(b.netto) as Netto ";
                }
                selectCommandText = selectCommandText + ", 'F' as bonthato, 'N' as bontva from szamla a, szamla_tetel b, kodtab c where a.vs='" + this.jel + "' ";
                if ((this.comboPartner.SelectedItem != null) && (this.partner_id != string.Empty))
                {
                    selectCommandText = selectCommandText + "and pid=" + this.partner_id + " ";
                }
                if ((this.comboSzoveg.SelectedItem != null) && (this.comboSzoveg.SelectedItem.ToString() != string.Empty))
                {
                    selectCommandText = selectCommandText + "and b.szoveg='" + this.comboSzoveg.SelectedItem.ToString() + "' ";
                }
                if (this.havi.Checked)
                {
                    selectCommandText = selectCommandText + "and year(datum_telj) = year('" + str2 + "') and month(datum_telj) = month('" + str2 + "') ";
                }
                else
                {
                    selectCommandText = selectCommandText + "and year(datum_telj) = '" + this.ev.Text + "' ";
                }
                selectCommandText = selectCommandText + "and a.id=b.id and b.megnid = c.sorszam and '" + str3 + "' = substring(c.kod,1,1) ";
                if (this.havi.Checked)
                {
                    selectCommandText = selectCommandText + "group by substring(c.kod,1,1)";
                }
                else
                {
                    selectCommandText = selectCommandText + "group by substring(c.kod,1,1),month(datum_telj)";
                }
                this.da = new SqlDataAdapter(selectCommandText, this.myconn);
                if (this.havi.Checked)
                {
                    this.da.Fill(this.ds, "tableFo");
                }
                else
                {
                    this.da.Fill(this.ds, "tableKoztes");
                }
            }
            if (this.havi.Checked)
            {
                this.tableFo = this.ds.Tables["tableFo"];
            }
            else
            {
                this.tableFo = this.koztesTablaAtforditas().Copy();
            }
            this.viewFo.BeginInit();
            this.viewFo.Table = this.tableFo;
            this.viewFo.EndInit();
            this.viewFo.Sort = "kod";
            this.dataGV.DataSource = this.viewFo;
            this.dataGV.Columns[0].Frozen = true;
            this.dataGV.Columns[1].Frozen = true;
            for (num = 2; num < (this.dataGV.ColumnCount - 2); num++)
            {
                this.dataGV.Columns[num].DefaultCellStyle.Format = "N2";
                this.dataGV.Columns[num].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            this.dataGV.Columns[this.dataGV.ColumnCount - 2].Visible = false;
            this.dataGV.Columns[this.dataGV.ColumnCount - 1].Visible = false;
            this.tableKoztes.Clear();
            for (num = 0; num < this.ktaltabla.Rows.Count; num++)
            {
                str3 = this.ktaltabla.Rows[num]["kod"].ToString();
                str4 = this.ktaltabla.Rows[num]["szoveg"].ToString();
                str5 = this.ktaltabla.Rows[num]["sorszam"].ToString();
                if (this.havi.Checked)
                {
                    selectCommandText = "select substring(c.kod,1,2) as Kod, '" + str4 + "' as Megnevezes, sum(b.netto) as Netto ";
                }
                else
                {
                    selectCommandText = "select substring(c.kod,1,2) as Kod, month(datum_telj) as ho, '" + str4 + "' as Megnevezes, sum(b.netto) as Netto ";
                }
                selectCommandText = selectCommandText + ", 'A' as bonthato, 'N' as bontva from szamla a, szamla_tetel b, kodtab c where a.vs='" + this.jel + "' ";
                if ((this.comboPartner.SelectedItem != null) && (this.partner_id != string.Empty))
                {
                    selectCommandText = selectCommandText + "and pid=" + this.partner_id + " ";
                }
                if ((this.comboSzoveg.SelectedItem != null) && (this.comboSzoveg.SelectedItem.ToString() != string.Empty))
                {
                    selectCommandText = selectCommandText + "and b.szoveg='" + this.comboSzoveg.SelectedItem.ToString() + "' ";
                }
                if (this.havi.Checked)
                {
                    selectCommandText = selectCommandText + "and year(datum_telj) = year('" + str2 + "') and month(datum_telj) = month('" + str2 + "') ";
                }
                else
                {
                    selectCommandText = selectCommandText + "and year(datum_telj) = '" + this.ev.Text + "' ";
                }
                selectCommandText = selectCommandText + "and a.id=b.id and b.megnid = c.sorszam and '" + str3 + "' = substring(c.kod,1,2) ";
                if (this.havi.Checked)
                {
                    selectCommandText = selectCommandText + "group by substring(c.kod,1,2)";
                }
                else
                {
                    selectCommandText = selectCommandText + "group by substring(c.kod,1,2),month(datum_telj)";
                }
                this.da = new SqlDataAdapter(selectCommandText, this.myconn);
                if (this.havi.Checked)
                {
                    this.da.Fill(this.ds, "tableAl");
                }
                else
                {
                    this.da.Fill(this.ds, "tableKoztes");
                }
            }
            if (this.havi.Checked)
            {
                this.tableAl = this.ds.Tables["tableAl"];
            }
            else
            {
                this.tableAl = this.koztesTablaAtforditas().Copy();
            }
            this.tableKoztes.Clear();
            for (num = 0; num < this.kttabla.Rows.Count; num++)
            {
                str3 = this.kttabla.Rows[num]["kod"].ToString();
                str4 = this.kttabla.Rows[num]["szoveg"].ToString();
                str5 = this.kttabla.Rows[num]["sorszam"].ToString();
                if (this.havi.Checked)
                {
                    selectCommandText = "select c.kod as Kod, '" + str4 + "' as Megnevezes, sum(b.netto) as Netto ";
                }
                else
                {
                    selectCommandText = "select c.kod as Kod, month(datum_telj) as ho, '" + str4 + "' as Megnevezes, sum(b.netto) as Netto ";
                }
                selectCommandText = selectCommandText + ", 'T' as bonthato, 'N' as bontva from szamla a, szamla_tetel b, kodtab c where a.vs='" + this.jel + "' ";
                if ((this.comboPartner.SelectedItem != null) && (this.partner_id != string.Empty))
                {
                    selectCommandText = selectCommandText + "and pid=" + this.partner_id + " ";
                }
                if ((this.comboSzoveg.SelectedItem != null) && (this.comboSzoveg.SelectedItem.ToString() != string.Empty))
                {
                    selectCommandText = selectCommandText + "and b.szoveg='" + this.comboSzoveg.SelectedItem.ToString() + "' ";
                }
                if (this.havi.Checked)
                {
                    selectCommandText = selectCommandText + "and year(datum_telj) = year('" + str2 + "') and month(datum_telj) = month('" + str2 + "') ";
                }
                else
                {
                    selectCommandText = selectCommandText + "and year(datum_telj) = '" + this.ev.Text + "' ";
                }
                selectCommandText = selectCommandText + "and a.id=b.id and b.megnid = c.sorszam and '" + str3 + "' = c.kod ";
                if (this.havi.Checked)
                {
                    selectCommandText = selectCommandText + "group by c.kod";
                }
                else
                {
                    selectCommandText = selectCommandText + "group by c.kod,month(datum_telj)";
                }
                this.da = new SqlDataAdapter(selectCommandText, this.myconn);
                if (this.havi.Checked)
                {
                    this.da.Fill(this.ds, "table");
                }
                else
                {
                    this.da.Fill(this.ds, "tableKoztes");
                }
            }
            if (this.havi.Checked)
            {
                this.table = this.ds.Tables["table"];
            }
            else
            {
                this.table = this.koztesTablaAtforditas().Copy();
            }
        }

        private void buttonNyomtat_Click(object sender, EventArgs e)
        {
            this.nyomtat = new PrintForm();
            string[] parName = new string[] { "SZOVEG", "EV" };
            string[] parValue = new string[] { "", this.ev.Text };
            string[] parTyp = new string[] { "string", "string" };
            if (this.jel == "V")
            {
                parValue[0] = "Bev\x00e9telek";
            }
            else
            {
                parValue[0] = "Kiad\x00e1sok";
            }
            DataSet dataSet = new MainProgramm.virDataSet();
            for (int i = 0; i < this.viewFo.Count; i++)
            {
                DataRow row = dataSet.Tables["HaviBontas"].NewRow();
                if (this.viewFo[i]["bonthato"].ToString() == "F")
                {
                    row["kod"] = this.viewFo[i]["kod"].ToString();
                }
                if (this.viewFo[i]["bonthato"].ToString() == "A")
                {
                    row["kod"] = " " + this.viewFo[i]["kod"].ToString();
                }
                if (this.viewFo[i]["bonthato"].ToString() == "T")
                {
                    row["kod"] = "  " + this.viewFo[i]["kod"].ToString();
                }
                row["megnevezes"] = this.viewFo[i]["megnevezes"];
                if (this.havi.Checked)
                {
                    for (int j = 2; j < dataSet.Tables["HaviBontas"].Columns.Count; j++)
                    {
                        row[j] = 0;
                    }
                    row[this.honap.Value.Month + 1] = this.viewFo[i]["netto"];
                }
                else
                {
                    row["januar"] = this.viewFo[i]["januar"];
                    row["februar"] = this.viewFo[i]["februar"];
                    row["marcius"] = this.viewFo[i]["marcius"];
                    row["aprilis"] = this.viewFo[i]["aprilis"];
                    row["majus"] = this.viewFo[i]["majus"];
                    row["junius"] = this.viewFo[i]["junius"];
                    row["julius"] = this.viewFo[i]["julius"];
                    row["augusztus"] = this.viewFo[i]["augusztus"];
                    row["szeptember"] = this.viewFo[i]["szeptember"];
                    row["oktober"] = this.viewFo[i]["oktober"];
                    row["november"] = this.viewFo[i]["november"];
                    row["december"] = this.viewFo[i]["december"];
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

        private void comboPartner_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.partner_id = this.tablePartner.Rows.Find(this.comboPartner.SelectedItem)["pid"].ToString();
        }

        private void dataGV_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                DataRow[] rowArray2;
                int num2;
                DataRow row;
                int num3;
                DataRow[] rowArray = this.tableFo.Select("kod ='" + this.dataGV.Rows[e.RowIndex].Cells["kod"].Value.ToString() + "'");
                int index = this.tableFo.Rows.IndexOf(rowArray[0]);
                if (this.dataGV.Rows[e.RowIndex].Cells["bonthato"].Value.ToString() == "F")
                {
                    if (this.dataGV.Rows[e.RowIndex].Cells["bontva"].Value.ToString() == "N")
                    {
                        this.tableFo.Rows[index]["bontva"] = "I";
                        rowArray2 = this.tableAl.Select("substring(kod,1,1)='" + this.dataGV.Rows[e.RowIndex].Cells["kod"].Value.ToString() + "'");
                        for (num2 = 0; num2 < rowArray2.Length; num2++)
                        {
                            row = this.tableFo.NewRow();
                            num3 = 0;
                            while (num3 < this.tableFo.Columns.Count)
                            {
                                row[num3] = rowArray2[num2][num3];
                                num3++;
                            }
                            this.tableFo.Rows.Add(row);
                        }
                    }
                    else
                    {
                        rowArray2 = this.tableFo.Select("substring(kod,1,1)='" + this.dataGV.Rows[e.RowIndex].Cells["kod"].Value.ToString() + "' and bonthato in('A','T')");
                        for (num2 = 0; num2 < rowArray2.Length; num2++)
                        {
                            this.tableFo.Rows.Remove(rowArray2[num2]);
                        }
                        this.tableFo.Rows[index]["bontva"] = "N";
                    }
                }
                else if (this.dataGV.Rows[e.RowIndex].Cells["bonthato"].Value.ToString() == "A")
                {
                    if (this.dataGV.Rows[e.RowIndex].Cells["bontva"].Value.ToString() == "N")
                    {
                        this.tableFo.Rows[index]["bontva"] = "I";
                        rowArray2 = this.table.Select("substring(kod,1,2)='" + this.dataGV.Rows[e.RowIndex].Cells["kod"].Value.ToString() + "'");
                        for (num2 = 0; num2 < rowArray2.Length; num2++)
                        {
                            row = this.tableFo.NewRow();
                            for (num3 = 0; num3 < this.tableFo.Columns.Count; num3++)
                            {
                                row[num3] = rowArray2[num2][num3];
                            }
                            this.tableFo.Rows.Add(row);
                        }
                    }
                    else
                    {
                        rowArray2 = this.tableFo.Select("substring(kod,1,2)='" + this.dataGV.Rows[e.RowIndex].Cells["kod"].Value.ToString() + "' and bonthato = 'T'");
                        for (num2 = 0; num2 < rowArray2.Length; num2++)
                        {
                            this.tableFo.Rows.Remove(rowArray2[num2]);
                        }
                        this.tableFo.Rows[index]["bontva"] = "N";
                    }
                }
                else if (this.dataGV.Rows[e.RowIndex].Cells["bonthato"].Value.ToString() == "T")
                {
                    this.tableTooltip.Clear();
                    string str = this.honap.Value.ToShortDateString();
                    string kod = this.dataGV.Rows[e.RowIndex].Cells["kod"].Value.ToString();
                    string selectCommandText = "select a.azonosito, d.azonosito as partner, a.megjegyzes, b.netto, b.brutto, a.fizetve, b.szoveg from szamla a left outer join partner d on a.pid =d.pid, szamla_tetel b, kodtab c where a.id = b.id and a.vs='" + this.jel + "' ";
                    if ((this.comboPartner.SelectedItem != null) && (this.partner_id != string.Empty))
                    {
                        selectCommandText = selectCommandText + "and a.pid=" + this.partner_id + " ";
                    }
                    if ((this.comboSzoveg.SelectedItem != null) && (this.comboSzoveg.SelectedItem.ToString() != string.Empty))
                    {
                        selectCommandText = selectCommandText + "and b.szoveg='" + this.comboSzoveg.SelectedItem.ToString() + "' ";
                    }
                    if (this.havi.Checked)
                    {
                        selectCommandText = selectCommandText + "and year(datum_telj) = year('" + str + "') and month(datum_telj) = month('" + str + "') ";
                    }
                    else
                    {
                        selectCommandText = selectCommandText + "and year(datum_telj) = '" + this.ev.Text + "' ";
                    }
                    selectCommandText = selectCommandText + "  and b.megnid = c.sorszam   and c.kod = '" + kod + "' ";
                    this.da = new SqlDataAdapter(selectCommandText, this.myconn);
                    this.da.Fill(this.ds, "tableTooltip");
                    this.tableTooltip = this.ds.Tables["tableTooltip"];
                    this.alSzamlak = new AlSzamlak(this.tableTooltip, kod);
                    this.alSzamlak.ShowDialog();
                }
            }
        }

        private void dataGV_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (this.dataGV.Rows[e.RowIndex].Cells["bonthato"].Value.ToString() == "A")
                {
                    e.CellStyle.BackColor = Color.LightGreen;
                }
                if (this.dataGV.Rows[e.RowIndex].Cells["bonthato"].Value.ToString() == "T")
                {
                    e.CellStyle.BackColor = Color.GreenYellow;
                }
            }
        }

        private void ev_SelectedIndexChanged(object sender, EventArgs e)
        {
            int num;
            this.tablePartner.Clear();
            this.tableSzoveg.Clear();
            this.comboPartner.Items.Clear();
            this.comboSzoveg.Items.Clear();
            this.da = new SqlDataAdapter("select distinct partner.azonosito, partner.pid from szamla, partner where szamla.pid = partner.pid and vs='" + this.jel + "' and year(datum_telj) = '" + this.ev.Text + "' order by azonosito ", this.myconn);
            this.da.Fill(this.ds, "tablePartner");
            this.tablePartner = this.ds.Tables["tablePartner"];
            for (num = 0; num < this.tablePartner.Rows.Count; num++)
            {
                this.comboPartner.Items.Add(this.tablePartner.Rows[num]["azonosito"].ToString());
            }
            this.tablePartner.PrimaryKey = new DataColumn[] { this.tablePartner.Columns["azonosito"] };
            this.da = new SqlDataAdapter("select distinct szoveg from szamla a, szamla_tetel b where a.id = b.id and vs='" + this.jel + "' and year(datum_telj) = '" + this.ev.Text + "' order by szoveg", this.myconn);
            this.da.Fill(this.ds, "tableSzoveg");
            this.tableSzoveg = this.ds.Tables["tableSzoveg"];
            for (num = 0; num < this.tableSzoveg.Rows.Count; num++)
            {
                this.comboSzoveg.Items.Add(this.tableSzoveg.Rows[num]["szoveg"].ToString());
            }
        }

        private void eves_CheckedChanged(object sender, EventArgs e)
        {
            this.tableFo.Clear();
            this.tableAl.Clear();
            this.table.Clear();
        }

        private void havi_CheckedChanged(object sender, EventArgs e)
        {
            this.tableFo.Clear();
            this.tableAl.Clear();
            this.table.Clear();
            if (this.havi.Checked)
            {
                this.honap.Enabled = true;
                this.ev.Enabled = false;
            }
            else
            {
                this.honap.Enabled = false;
                this.ev.Enabled = true;
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
            this.da = new SqlDataAdapter("select distinct partner.azonosito, partner.pid from szamla, partner where szamla.pid = partner.pid and vs='" + this.jel + "' and year(datum_telj) = year('" + str + "') and month(datum_telj) = month('" + str + "') order by azonosito ", this.myconn);
            this.da.Fill(this.ds, "tablePartner");
            this.tablePartner = this.ds.Tables["tablePartner"];
            for (num = 0; num < this.tablePartner.Rows.Count; num++)
            {
                this.comboPartner.Items.Add(this.tablePartner.Rows[num]["azonosito"].ToString());
            }
            this.tablePartner.PrimaryKey = new DataColumn[] { this.tablePartner.Columns["azonosito"] };
            this.da = new SqlDataAdapter("select distinct szoveg from szamla a, szamla_tetel b where a.id = b.id and vs='" + this.jel + "' and year(datum_telj) = year('" + str + "') and month(datum_telj) = month('" + str + "') order by szoveg", this.myconn);
            this.da.Fill(this.ds, "tableSzoveg");
            this.tableSzoveg = this.ds.Tables["tableSzoveg"];
            for (num = 0; num < this.tableSzoveg.Rows.Count; num++)
            {
                this.comboSzoveg.Items.Add(this.tableSzoveg.Rows[num]["szoveg"].ToString());
            }
        }

        private string honapNev(string honap)
        {
            switch (honap)
            {
                case "1":
                    return "Januar";

                case "2":
                    return "Februar";

                case "3":
                    return "Marcius";

                case "4":
                    return "Aprilis";

                case "5":
                    return "Majus";

                case "6":
                    return "Junius";

                case "7":
                    return "Julius";

                case "8":
                    return "Augusztus";

                case "9":
                    return "Szeptember";

                case "10":
                    return "Oktober";

                case "11":
                    return "November";

                case "12":
                    return "December";
            }
            return "Januar";
        }

        private DataTable koztesTablaAtforditas()
        {
            this.tableHaviBontas.Clear();
            this.tableKoztes = this.ds.Tables["tableKoztes"];
            int num = 0;
            while (num < this.tableKoztes.Rows.Count)
            {
                string str = this.tableKoztes.Rows[num]["kod"].ToString();
                DataRow row = this.tableHaviBontas.NewRow();
                row["kod"] = str;
                row["megnevezes"] = this.tableKoztes.Rows[num]["megnevezes"].ToString();
                row["bonthato"] = this.tableKoztes.Rows[num]["bonthato"].ToString();
                row["bontva"] = this.tableKoztes.Rows[num]["bontva"].ToString();
                while ((num < this.tableKoztes.Rows.Count) && (str == this.tableKoztes.Rows[num]["kod"].ToString()))
                {
                    string honap = this.tableKoztes.Rows[num]["ho"].ToString();
                    string str3 = this.honapNev(honap);
                    while (((num < this.tableKoztes.Rows.Count) && (str == this.tableKoztes.Rows[num]["kod"].ToString())) && (honap == this.tableKoztes.Rows[num]["ho"].ToString()))
                    {
                        row[str3] = this.tableKoztes.Rows[num]["netto"].ToString();
                        num++;
                    }
                }
                this.tableHaviBontas.Rows.Add(row);
            }
            return this.tableHaviBontas;
        }

        private void Szamlak_Load(object sender, EventArgs e)
        {
            int num;
            this.mainForm = (VIR.MainForm)base.ParentForm;
            this.myconn = this.mainForm.MyConn;
            this.ktfotabla = this.ktfotag.AdatTablainfo.Adattabla;
            this.ktaltabla = this.ktaltag.AdatTablainfo.Adattabla;
            this.kttabla = this.kttag.AdatTablainfo.Adattabla;
            this.da = new SqlDataAdapter("select '' as Kod, '' as Megnevezes, 0.0 as Januar, 0.0 as Februar, 0.0 as Marcius, 0.0 as Aprilis, 0.0 as Majus, 0.0 as Junius, 0.0 as Julius, 0.0 as Augusztus, 0.0 as Szeptember, 0.0 as Oktober, 0.0 as November, 0.0 as December, 'F' as bonthato, 'N' as bontva from szamla_tetel where id is null", this.myconn);
            this.da.Fill(this.ds, "tableHaviBontas");
            this.tableHaviBontas = this.ds.Tables["tableHaviBontas"];
            this.da = new SqlDataAdapter("select distinct year(datum_telj) as ev from szamla", this.myconn);
            this.da.Fill(this.ds, "tableEvek");
            this.tableEvek = this.ds.Tables["tableEvek"];
            for (num = 0; num < this.tableEvek.Rows.Count; num++)
            {
                this.ev.Items.Add(this.tableEvek.Rows[num]["ev"].ToString());
            }
            string str = this.honap.Value.ToShortDateString();
            this.da = new SqlDataAdapter("select distinct partner.azonosito, partner.pid from szamla, partner where szamla.pid = partner.pid and year(datum_telj) = year('" + str + "') and month(datum_telj) = month('" + str + "') order by azonosito ", this.myconn);
            this.da.Fill(this.ds, "tablePartner");
            this.tablePartner = this.ds.Tables["tablePartner"];
            for (num = 0; num < this.tablePartner.Rows.Count; num++)
            {
                this.comboPartner.Items.Add(this.tablePartner.Rows[num]["azonosito"].ToString());
            }
            this.tablePartner.PrimaryKey = new DataColumn[] { this.tablePartner.Columns["azonosito"] };
            this.da = new SqlDataAdapter("select distinct szoveg from szamla a, szamla_tetel b where a.id = b.id and year(datum_telj) = year('" + str + "') and month(datum_telj) = month('" + str + "') order by szoveg", this.myconn);
            this.da.Fill(this.ds, "tableSzoveg");
            this.tableSzoveg = this.ds.Tables["tableSzoveg"];
            for (num = 0; num < this.tableSzoveg.Rows.Count; num++)
            {
                this.comboSzoveg.Items.Add(this.tableSzoveg.Rows[num]["szoveg"].ToString());
            }
        }

    }
}