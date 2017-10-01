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
    public partial class Eredmeny : UserControl
    {
        private VIR.MainForm mainForm;
        private SqlConnection myconn = new SqlConnection();
        private DataSet ds = new DataSet();
        private SqlDataAdapter da;

        private PrintForm nyomtat = new PrintForm();
        private EredmenyLista eredmenyLista = new EredmenyLista();

        private DataTable tableEredmeny   = new DataTable();
        private DataView  viewEredmeny    = new DataView();
        private DataTable tableBevetel    = new DataTable();
        private DataTable tableKiadas     = new DataTable();
        private DataTable tableKiadasTermek = new DataTable();
        private DataView viewKiadasTermek = new DataView();
        private DataTable tableEvek = new DataTable();
        private DataTable tableBevetelek = new DataTable();
        private DataTable tableKiadasok = new DataTable();
        private DataView viewBevetelek = new DataView();
        private DataView viewKiadasok = new DataView();
        private DataTable tableFelgyujt = new DataTable();
        private DataTable tableRendszer = new DataTable();

        private MyTag ktgfelosztastag;
        private DataTable ktgfelosztastabla;
        private MyTag termekfotag;
        private MyTag termekaltag;
        private MyTag termektag;
        private DataTable termekfotabla;
        private DataTable termekaltabla;
        private DataTable termektabla;
        private MyTag koltsegfotag;
        private MyTag koltsegaltag;
        private MyTag koltsegtag;
        private DataTable koltsegfotabla;
        private DataTable koltsegaltabla;
        private DataTable koltsegtabla;

        private string Termekkod = "";
        private string eredmeny_szures = "";


        public Eredmeny(string szoveg, object[] obj)
        {
            MyTag[] tagArray = (MyTag[])obj[1];
            this.ktgfelosztastag = tagArray[0];
            this.termektag = tagArray[1];
            this.koltsegtag = tagArray[2];
            this.termekfotag = tagArray[3];
            this.termekaltag = tagArray[4];
            this.koltsegfotag = tagArray[5];
            this.koltsegaltag = tagArray[6];
            this.InitializeComponent();
        }

        private void Eredmeny_Load(object sender, EventArgs e)
        {
            int num;
            string selectCommandText = "";
            this.mainForm = (VIR.MainForm)base.ParentForm;
            this.myconn = this.mainForm.MyConn;
            this.ktgfelosztastabla = this.ktgfelosztastag.AdatTablainfo.Adattabla;
            this.termekfotabla = this.termekfotag.AdatTablainfo.Adattabla;
            this.termekaltabla = this.termekaltag.AdatTablainfo.Adattabla;
            this.termektabla = this.termektag.AdatTablainfo.Adattabla;
            this.koltsegtabla = this.koltsegtag.AdatTablainfo.Adattabla;
            this.koltsegfotabla = this.koltsegfotag.AdatTablainfo.Adattabla;
            this.koltsegaltabla = this.koltsegaltag.AdatTablainfo.Adattabla;
            selectCommandText = "select '' as TK,'' as Termekkod, '' as Megnevezes, 0 as Januar, 0 as Februar, 0 as Marcius, 0 as Aprilis, 0 as Majus, 0 as Junius, 0 as Julius, 0 as Augusztus, 0 as Szeptember, 0 as Oktober, 0 as November, 0 as December, 0 as Osszesen ";
            this.da = new SqlDataAdapter(selectCommandText, this.myconn);
            this.da.Fill(this.ds, "tableEredmeny");
            this.tableEredmeny = this.ds.Tables["tableEredmeny"];
            this.tableEredmeny.Clear();
            this.viewEredmeny.Table = this.tableEredmeny;
            this.viewEredmeny.Sort = "tk desc,termekkod";
            this.dataGV.DataSource = this.viewEredmeny;
            this.dataGV.Columns[0].Visible = false;
            this.dataGV.Columns[0].Frozen = true;
            this.dataGV.Columns[1].Frozen = true;
            this.dataGV.Columns[2].Frozen = true;
            for (num = 3; num < this.dataGV.Columns.Count; num++)
            {
                this.dataGV.Columns[num].DefaultCellStyle.Format = "N0";
                this.dataGV.Columns[num].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            this.da = new SqlDataAdapter("select distinct year(datum_telj) as ev from szamla", this.myconn);
            this.da.Fill(this.ds, "tableEvek");
            this.tableEvek = this.ds.Tables["tableEvek"];
            for (num = 0; num < this.tableEvek.Rows.Count; num++)
            {
                this.ev.Items.Add(this.tableEvek.Rows[num]["ev"].ToString());
            }
            this.ev.Text = this.ev.Items[0].ToString();
            this.da = new SqlDataAdapter("select * from rendszer", this.myconn);
            this.da.Fill(this.ds, "tableRendszer");
            this.tableRendszer = this.ds.Tables["tableRendszer"];
            string str2 = "";
            this.eredmeny_szures = "'";
            if (this.tableRendszer.Rows.Count <= 0)
            {
                this.eredmeny_szures = "''";
            }
            else
            {
                str2 = this.tableRendszer.Rows[0]["eredmeny_szures"].ToString();
                for (num = 0; num < str2.Length; num++)
                {
                    if (str2.Substring(num, 1) == ",")
                    {
                        if (this.eredmeny_szures.Substring(this.eredmeny_szures.Length - 1, 1) != "'")
                        {
                            this.eredmeny_szures = this.eredmeny_szures + "'";
                        }
                        if ((num + 1) > str2.Length)
                        {
                            break;
                        }
                        this.eredmeny_szures = this.eredmeny_szures + ",'";
                    }
                    else
                    {
                        this.eredmeny_szures = this.eredmeny_szures + str2.Substring(num, 1);
                    }
                }
                this.eredmeny_szures = this.eredmeny_szures + "'";
            }
            this.tableBevetelek = this.tableEredmeny.Clone();
            this.tableKiadasok = this.tableEredmeny.Clone();
            this.tableKiadasok.PrimaryKey = new DataColumn[] { this.tableKiadasok.Columns["Termekkod"] };
            this.viewBevetelek.Table = this.tableBevetelek;
            this.viewKiadasok.Table = this.tableKiadasok;
            this.viewBevetelek.Sort = "tk desc,termekkod";
            this.viewKiadasok.Sort = "tk desc,termekkod";
            this.dataGVBev.DataSource = this.viewBevetelek;
            this.dataGVKiad.DataSource = this.viewKiadasok;
            this.dataGVBev.Columns[0].Visible = false;
            this.dataGVKiad.Columns[0].Visible = false;
            this.dataGVBev.Columns[0].Frozen = true;
            this.dataGVKiad.Columns[0].Frozen = true;
            this.dataGVBev.Columns[1].Frozen = true;
            this.dataGVKiad.Columns[1].Frozen = true;
            this.dataGVBev.Columns[2].Frozen = true;
            this.dataGVKiad.Columns[2].Frozen = true;
            for (num = 3; num < this.dataGVBev.Columns.Count; num++)
            {
                this.dataGVBev.Columns[num].DefaultCellStyle.Format = "N0";
                this.dataGVBev.Columns[num].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dataGVKiad.Columns[num].DefaultCellStyle.Format = "N0";
                this.dataGVKiad.Columns[num].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            this.tableFelgyujt = this.tableEredmeny.Clone();
        }

        private void havi_CheckedChanged(object sender, EventArgs e)
        {
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
            string selectCommandText = "";
            string str2 = this.honap.Value.ToShortDateString();
            string str3 = this.honapig.Value.ToShortDateString();
            this.tableBevetel.Clear();
            selectCommandText = "select b.megnid as termek_id, c.kod as termekkod, c.szoveg as Megnevezes, sum(b.netto) as Netto, month(datum_telj) as ho from szamla a, szamla_tetel b, kodtab c  where a.vs='V'   and c.kod not in(" + this.eredmeny_szures + ") ";
            if (this.havi.Checked)
            {
                selectCommandText = selectCommandText + "and cast(datum_telj as datetime) >= cast('" + str2 + "' as datetime) and cast(datum_telj as datetime) <= cast('" + str3 + "' as datetime) ";
            }
            else
            {
                selectCommandText = selectCommandText + "and year(datum_telj) = '" + this.ev.Text + "' ";
            }
            selectCommandText = selectCommandText + "and a.id=b.id and b.megnid = c.sorszam group by b.megnid,c.kod,c.szoveg,month(datum_telj)";
            this.da = new SqlDataAdapter(selectCommandText, this.myconn);
            this.da.Fill(this.ds, "tableBevetel");
            this.tableBevetel = this.ds.Tables["tableBevetel"];
            this.tableKiadas.Clear();
            selectCommandText = "select b.megnid as koltseg_id, c.kod as koltsegkod, c.szoveg as Megnevezes, sum(b.netto) as Netto, month(datum_telj) as ho from szamla a, szamla_tetel b, kodtab c  where a.vs='S'   and c.kod not in(" + this.eredmeny_szures + ") ";
            if (this.havi.Checked)
            {
                selectCommandText = selectCommandText + "and cast(datum_telj as datetime) >= cast('" + str2 + "' as datetime) and cast(datum_telj as datetime) <= cast('" + str3 + "' as datetime) ";
            }
            else
            {
                selectCommandText = selectCommandText + "and year(datum_telj) = '" + this.ev.Text + "' ";
            }
            selectCommandText = selectCommandText + "and a.id=b.id and b.megnid = c.sorszam group by b.megnid,c.kod,c.szoveg,month(datum_telj)";
            this.da = new SqlDataAdapter(selectCommandText, this.myconn);
            this.da.Fill(this.ds, "tableKiadas");
            this.tableKiadas = this.ds.Tables["tableKiadas"];
            this.da = new SqlDataAdapter("select 0 as termek_id, 0 as ho, 0.00 as ft, '' as TK ", this.myconn);
            this.da.Fill(this.ds, "tableKiadasTermek");
            this.tableKiadasTermek = this.ds.Tables["tableKiadasTermek"];
            this.tableKiadasTermek.Clear();
            this.viewKiadasTermek.Table = this.tableKiadasTermek;
            this.viewKiadasTermek.Sort = "termek_id, ho";
            this.koltsegFelosztas();
            this.tableKiadasokTolt();
            this.tableBevetelekTolt();
            this.eredmenySzamitas();
        }

        private void tablFelgyujtTolt(DataView view)
        {
            int num;
            DataRow row;
            int num2;
            string str;
            DataRow[] rowArray;
            decimal[] numArray;
            decimal[] numArray2;
            this.tableFelgyujt.Clear();
            if (this.comboCsoport.Text == "1. Főcsoport")
            {
                num = 0;
                while (num < view.Count)
                {
                    row = this.tableFelgyujt.NewRow();
                    row["tk"] = view[num]["tk"];
                    if (view[num]["tk"].ToString() == "A")
                    {
                        row["termekkod"] = view[num]["termekkod"];
                        row["megnevezes"] = view[num]["megnevezes"];
                        num2 = 3;
                        while (num2 < view.Table.Columns.Count)
                        {
                            row[num2] = Convert.ToDecimal(view[num][num2].ToString());
                            num2++;
                        }
                        num++;
                    }
                    else if (view[num]["tk"].ToString() == "K")
                    {
                        str = view[num]["termekkod"].ToString().Substring(0, 1);
                        rowArray = this.koltsegfotabla.Select("kod = '" + str + "'");
                        row["termekkod"] = str;
                        row["megnevezes"] = rowArray[0]["szoveg"];
                        numArray2 = new decimal[13];
                        numArray = numArray2;
                        while ((num < view.Count) && (str == view[num]["termekkod"].ToString().Substring(0, 1)))
                        {
                            num2 = 3;
                            while (num2 < view.Table.Columns.Count)
                            {
                                numArray[num2 - 3] += Convert.ToDecimal(view[num][num2].ToString());
                                num2++;
                            }
                            num++;
                        }
                        num2 = 0;
                        while (num2 < numArray.Length)
                        {
                            row[num2 + 3] = numArray[num2];
                            num2++;
                        }
                    }
                    else
                    {
                        str = view[num]["termekkod"].ToString().Substring(0, 1);
                        rowArray = this.termekfotabla.Select("kod = '" + str + "'");
                        row["termekkod"] = str;
                        row["megnevezes"] = rowArray[0]["szoveg"];
                        numArray2 = new decimal[13];
                        numArray = numArray2;
                        while ((num < view.Count) && (str == view[num]["termekkod"].ToString().Substring(0, 1)))
                        {
                            num2 = 3;
                            while (num2 < view.Table.Columns.Count)
                            {
                                numArray[num2 - 3] += Convert.ToDecimal(view[num][num2].ToString());
                                num2++;
                            }
                            num++;
                        }
                        num2 = 0;
                        while (num2 < numArray.Length)
                        {
                            row[num2 + 3] = numArray[num2];
                            num2++;
                        }
                    }
                    this.tableFelgyujt.Rows.Add(row);
                }
            }
            else if (this.comboCsoport.Text == "2. Alcsoport")
            {
                num = 0;
                while (num < view.Count)
                {
                    row = this.tableFelgyujt.NewRow();
                    row["tk"] = view[num]["tk"];
                    if (view[num]["tk"].ToString() == "A")
                    {
                        row["termekkod"] = view[num]["termekkod"];
                        row["megnevezes"] = view[num]["megnevezes"];
                        num2 = 3;
                        while (num2 < view.Table.Columns.Count)
                        {
                            row[num2] = Convert.ToDecimal(view[num][num2].ToString());
                            num2++;
                        }
                        num++;
                    }
                    else if (view[num]["tk"].ToString() == "K")
                    {
                        str = view[num]["termekkod"].ToString().Substring(0, 2);
                        rowArray = this.koltsegaltabla.Select("kod = '" + str + "'");
                        row["termekkod"] = str;
                        row["megnevezes"] = rowArray[0]["szoveg"];
                        numArray2 = new decimal[13];
                        numArray = numArray2;
                        while ((num < view.Count) && (str == view[num]["termekkod"].ToString().Substring(0, 2)))
                        {
                            num2 = 3;
                            while (num2 < view.Table.Columns.Count)
                            {
                                numArray[num2 - 3] += Convert.ToDecimal(view[num][num2].ToString());
                                num2++;
                            }
                            num++;
                        }
                        num2 = 0;
                        while (num2 < numArray.Length)
                        {
                            row[num2 + 3] = numArray[num2];
                            num2++;
                        }
                    }
                    else
                    {
                        str = view[num]["termekkod"].ToString().Substring(0, 2);
                        rowArray = this.termekaltabla.Select("kod = '" + str + "'");
                        row["termekkod"] = str;
                        row["megnevezes"] = rowArray[0]["szoveg"];
                        numArray2 = new decimal[13];
                        numArray = numArray2;
                        while ((num < view.Count) && (str == view[num]["termekkod"].ToString().Substring(0, 2)))
                        {
                            num2 = 3;
                            while (num2 < view.Table.Columns.Count)
                            {
                                numArray[num2 - 3] += Convert.ToDecimal(view[num][num2].ToString());
                                num2++;
                            }
                            num++;
                        }
                        for (num2 = 0; num2 < numArray.Length; num2++)
                        {
                            row[num2 + 3] = numArray[num2];
                        }
                    }
                    this.tableFelgyujt.Rows.Add(row);
                }
            }
        }

        private void koltsegFelosztas()
        {
            this.tableKiadasTermek.Clear();
            for (int i = 0; i < this.tableKiadas.Rows.Count; i++)
            {
                DataRow row;
                decimal num2 = Convert.ToDecimal(this.tableKiadas.Rows[i]["Netto"].ToString());
                DataRow[] rowArray = this.ktgfelosztastabla.Select("koltseg_id = " + this.tableKiadas.Rows[i]["koltseg_id"].ToString());
                if (rowArray.Length > 0)
                {
                    for (int j = 0; j < rowArray.Length; j++)
                    {
                        decimal num4 = Convert.ToDecimal(rowArray[j]["szazalek"].ToString());
                        decimal num5 = (num2 * num4) / 100M;
                        row = this.tableKiadasTermek.NewRow();
                        row["tk"] = "T";
                        row["termek_id"] = rowArray[j]["termek_id"];
                        row["ho"] = this.tableKiadas.Rows[i]["ho"];
                        row["ft"] = num5;
                        this.tableKiadasTermek.Rows.Add(row);
                    }
                }
                else
                {
                    row = this.tableKiadasTermek.NewRow();
                    row["tk"] = "K";
                    row["termek_id"] = this.tableKiadas.Rows[i]["koltseg_id"];
                    row["ho"] = this.tableKiadas.Rows[i]["ho"];
                    row["ft"] = num2;
                    this.tableKiadasTermek.Rows.Add(row);
                }
            }
        }

        private void tableKiadasokTolt()
        {
            DataRow row;
            string columnName;
            this.viewKiadasok.BeginInit();
            this.tableKiadasok.Clear();
            int num = 0;
            while (num < this.viewKiadasTermek.Count)
            {
                string str;
                string str2;
                int num2 = Convert.ToInt32(this.viewKiadasTermek[num]["termek_id"].ToString());
                DataRow[] rowArray = this.termektabla.Select("sorszam = " + this.viewKiadasTermek[num]["termek_id"].ToString());
                if (rowArray.Length > 0)
                {
                    str = rowArray[0]["kod"].ToString();
                    str2 = rowArray[0]["szoveg"].ToString();
                }
                else
                {
                    DataRow[] rowArray2 = this.koltsegtabla.Select("sorszam = " + this.viewKiadasTermek[num]["termek_id"].ToString());
                    str = rowArray2[0]["kod"].ToString();
                    str2 = rowArray2[0]["szoveg"].ToString() + " (felosztatlan k\x00f6lts\x00e9g)";
                }
                row = this.tableKiadasok.NewRow();
                row["tk"] = this.viewKiadasTermek[num]["tk"];
                row["Termekkod"] = str;
                row["megnevezes"] = str2;
                for (int i = 3; i < this.tableKiadasok.Columns.Count; i++)
                {
                    row[i] = 0;
                }
                while ((num < this.viewKiadasTermek.Count) && (num2 == Convert.ToInt32(this.viewKiadasTermek[num]["termek_id"].ToString())))
                {
                    string honap = this.viewKiadasTermek[num]["ho"].ToString();
                    columnName = this.honapNev(honap);
                    row[columnName] = 0;
                    while (((num < this.viewKiadasTermek.Count) && (num2 == Convert.ToInt32(this.viewKiadasTermek[num]["termek_id"].ToString()))) && (honap == this.viewKiadasTermek[num]["ho"].ToString()))
                    {
                        decimal num4 = Convert.ToDecimal(this.viewKiadasTermek[num]["ft"].ToString());
                        row[columnName] = Convert.ToDecimal(row[columnName].ToString()) + num4;
                        num++;
                    }
                }
                row["osszesen"] = this.termekÖsszesen(row);
                this.tableKiadasok.Rows.Add(row);
            }
            row = this.tableKiadasok.NewRow();
            row["tk"] = "A";
            row["Termekkod"] = "\x00d6sszesen:";
            row["megnevezes"] = "";
            for (num = 3; num < this.tableKiadasok.Columns.Count; num++)
            {
                columnName = this.tableKiadasok.Columns[num].ColumnName;
                decimal num5 = 0M;
                for (int j = 0; j < this.tableKiadasok.Rows.Count; j++)
                {
                    num5 += Convert.ToDecimal(this.tableKiadasok.Rows[j][columnName].ToString());
                }
                row[columnName] = num5;
            }
            this.tableKiadasok.Rows.Add(row);
            if ((this.comboCsoport.Text == "1. Főcsoport") || (this.comboCsoport.Text == "2. Alcsoport"))
            {
                this.tablFelgyujtTolt(this.viewKiadasok);
                this.tableKiadasok.Clear();
                this.tableKiadasok = this.tableFelgyujt.Copy();
                this.viewKiadasok.Table = this.tableKiadasok;
                this.viewKiadasok.Sort = "tk desc,termekkod";
                this.dataGVKiad.DataSource = this.viewKiadasok;
            }
            this.viewKiadasok.EndInit();
        }

        private void tableBevetelekTolt()
        {
            DataRow row;
            string columnName;
            this.viewBevetelek.BeginInit();
            this.tableBevetelek.Clear();
            int num = 0;
            while (num < this.tableBevetel.Rows.Count)
            {
                int num2 = Convert.ToInt32(this.tableBevetel.Rows[num]["termek_id"].ToString());
                DataRow[] rowArray = this.termektabla.Select("sorszam = " + this.tableBevetel.Rows[num]["termek_id"].ToString());
                string str = rowArray[0]["kod"].ToString();
                string str2 = rowArray[0]["szoveg"].ToString();
                row = this.tableBevetelek.NewRow();
                row["tk"] = "T";
                row["Termekkod"] = str;
                row["megnevezes"] = str2;
                for (int i = 3; i < this.tableBevetelek.Columns.Count; i++)
                {
                    row[i] = 0;
                }
                while ((num < this.tableBevetel.Rows.Count) && (num2 == Convert.ToInt32(this.tableBevetel.Rows[num]["termek_id"].ToString())))
                {
                    string honap = this.tableBevetel.Rows[num]["ho"].ToString();
                    columnName = this.honapNev(honap);
                    row[columnName] = 0;
                    while (((num < this.tableBevetel.Rows.Count) && (num2 == Convert.ToInt32(this.tableBevetel.Rows[num]["termek_id"].ToString()))) && (honap == this.tableBevetel.Rows[num]["ho"].ToString()))
                    {
                        decimal num4 = Convert.ToDecimal(this.tableBevetel.Rows[num]["netto"].ToString());
                        row[columnName] = Convert.ToDecimal(row[columnName].ToString()) + num4;
                        num++;
                    }
                }
                row["osszesen"] = this.termekÖsszesen(row);
                this.tableBevetelek.Rows.Add(row);
            }
            row = this.tableBevetelek.NewRow();
            row["tk"] = "A";
            row["Termekkod"] = "\x00d6sszesen:";
            row["megnevezes"] = "";
            for (num = 3; num < this.tableBevetelek.Columns.Count; num++)
            {
                columnName = this.tableBevetelek.Columns[num].ColumnName;
                decimal num5 = 0M;
                for (int j = 0; j < this.tableBevetelek.Rows.Count; j++)
                {
                    num5 += Convert.ToDecimal(this.tableBevetelek.Rows[j][columnName].ToString());
                }
                row[columnName] = num5;
            }
            this.tableBevetelek.Rows.Add(row);
            if ((this.comboCsoport.Text == "1. Főcsoport") || (this.comboCsoport.Text == "2. Alcsoport"))
            {
                this.tablFelgyujtTolt(this.viewBevetelek);
                this.tableBevetelek.Clear();
                this.tableBevetelek = this.tableFelgyujt.Copy();
                this.viewBevetelek.Table = this.tableBevetelek;
                this.viewBevetelek.Sort = "tk desc,termekkod";
                this.dataGVBev.DataSource = this.viewBevetelek;
            }
            this.viewBevetelek.EndInit();
        }

        private void eredmenySzamitas()
        {
            DataRow row;
            DataRow[] rowArray;
            int num2;
            int num3;
            decimal num = 0M;
            this.tableEredmeny.Clear();
            for (num2 = 0; num2 < this.tableBevetelek.Rows.Count; num2++)
            {
                row = this.tableEredmeny.NewRow();
                row["tk"] = this.tableBevetelek.Rows[num2]["tk"];
                row["Termekkod"] = this.tableBevetelek.Rows[num2]["termekkod"];
                row["megnevezes"] = this.tableBevetelek.Rows[num2]["megnevezes"];
                if (row["tk"].ToString() == "T")
                {
                    rowArray = this.tableKiadasok.Select("tk = 'K' and termekkod = '" + this.tableBevetelek.Rows[num2]["termekkod"].ToString() + "'");
                }
                else
                {
                    rowArray = this.tableKiadasok.Select("tk = 'A' and termekkod = '" + this.tableBevetelek.Rows[num2]["termekkod"].ToString() + "'");
                }
                num3 = 3;
                while (num3 < this.tableBevetelek.Columns.Count)
                {
                    if (this.tableBevetelek.Rows[num2][num3].ToString() == "")
                    {
                        num = 0M;
                    }
                    else
                    {
                        num = Convert.ToDecimal(this.tableBevetelek.Rows[num2][num3].ToString());
                    }
                    if (rowArray.Length > 0)
                    {
                        if (rowArray[0][num3].ToString() == "")
                        {
                            row[num3] = num;
                        }
                        else
                        {
                            row[num3] = num - Convert.ToDecimal(rowArray[0][num3].ToString());
                        }
                    }
                    else
                    {
                        row[num3] = num;
                    }
                    num3++;
                }
                this.tableEredmeny.Rows.Add(row);
            }
            for (num2 = 0; num2 < this.tableKiadasok.Rows.Count; num2++)
            {
                if (this.tableKiadasok.Rows[num2]["tk"].ToString() == "K")
                {
                    rowArray = this.tableBevetelek.Select("tk = 'T' and termekkod = '" + this.tableKiadasok.Rows[num2]["termekkod"].ToString() + "'");
                }
                else
                {
                    rowArray = this.tableBevetelek.Select("tk = 'A' and termekkod = '" + this.tableKiadasok.Rows[num2]["termekkod"].ToString() + "'");
                }
                if (rowArray.Length == 0)
                {
                    row = this.tableEredmeny.NewRow();
                    row["tk"] = this.tableKiadasok.Rows[num2]["tk"];
                    row["Termekkod"] = this.tableKiadasok.Rows[num2]["termekkod"];
                    row["megnevezes"] = this.tableKiadasok.Rows[num2]["megnevezes"];
                    for (num3 = 3; num3 < this.tableKiadasok.Columns.Count; num3++)
                    {
                        if (this.tableKiadasok.Rows[num2][num3].ToString() == "")
                        {
                            row[num3] = 0;
                        }
                        else
                        {
                            row[num3] = 0M - Convert.ToDecimal(this.tableKiadasok.Rows[num2][num3].ToString());
                        }
                    }
                    this.tableEredmeny.Rows.Add(row);
                }
            }
        }

        private decimal termekÖsszesen(DataRow r)
        {
            decimal num = 0M;
            for (int i = 3; i < r.Table.Columns.Count; i++)
            {
                num += Convert.ToDecimal(r[i].ToString());
            }
            return num;
        }

        private void dataGV_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                this.Termekkod = this.dataGV.Rows[e.RowIndex].Cells["termekkod"].Value.ToString();
            }
        }

        private void eves_CheckedChanged(object sender, EventArgs e)
        {
            this.tableEredmeny.Clear();
            this.tableKiadasok.Clear();
            this.tableBevetelek.Clear();
        }

        private void buttonNyomtat_Click(object sender, EventArgs e)
        {
            DataRow row;
            int num;
            int num2;
            string columnName;
            this.nyomtat = new PrintForm();
            string[] parName = new string[] { "SZURES" };
            string[] parValue = new string[] { "" };
            if (this.havi.Checked)
            {
                parValue[0] = "Havi: " + this.honap.Value.ToShortDateString();
            }
            else
            {
                parValue[0] = "\x00c9ves: " + this.ev.Text.ToString();
            }
            string[] parTyp = new string[] { "string" };
            DataSet dataSet = new MainProgramm.virDataSet();
            for (num = 0; num < this.viewBevetelek.Count; num++)
            {
                if (this.viewBevetelek[num]["tk"].ToString() != "A")
                {
                    row = dataSet.Tables["Eredmeny"].NewRow();
                    row["bke"] = "1B";
                    num2 = 0;
                    while (num2 < this.tableBevetelek.Columns.Count)
                    {
                        columnName = this.tableBevetelek.Columns[num2].ColumnName;
                        if (row.Table.Columns.IndexOf(columnName) > -1)
                        {
                            row[columnName] = this.viewBevetelek[num][columnName];
                        }
                        num2++;
                    }
                    dataSet.Tables["Eredmeny"].Rows.Add(row);
                }
            }
            for (num = 0; num < this.viewKiadasok.Count; num++)
            {
                if (this.viewKiadasok[num]["tk"].ToString() != "A")
                {
                    row = dataSet.Tables["Eredmeny"].NewRow();
                    row["bke"] = "2K";
                    num2 = 0;
                    while (num2 < this.tableKiadasok.Columns.Count)
                    {
                        columnName = this.tableKiadasok.Columns[num2].ColumnName;
                        if (row.Table.Columns.IndexOf(columnName) > -1)
                        {
                            row[columnName] = this.viewKiadasok[num][columnName];
                        }
                        num2++;
                    }
                    dataSet.Tables["Eredmeny"].Rows.Add(row);
                }
            }
            for (num = 0; num < this.viewEredmeny.Count; num++)
            {
                if (this.viewEredmeny[num]["tk"].ToString() != "A")
                {
                    row = dataSet.Tables["Eredmeny"].NewRow();
                    row["bke"] = "3E";
                    for (num2 = 0; num2 < this.tableEredmeny.Columns.Count; num2++)
                    {
                        columnName = this.tableEredmeny.Columns[num2].ColumnName;
                        if (row.Table.Columns.IndexOf(columnName) > -1)
                        {
                            row[columnName] = this.viewEredmeny[num][columnName];
                        }
                    }
                    dataSet.Tables["Eredmeny"].Rows.Add(row);
                }
            }
            this.nyomtat.PrintParams(parName, parValue, parTyp);
            this.eredmenyLista.SetDataSource(dataSet);
            this.eredmenyLista.SetParameterValue("SZURES", parValue[0]);
            this.nyomtat.reportSource = this.eredmenyLista;
            this.nyomtat.DoPreview(this.mainForm.defPageSettings);
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

        private void dataGV_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if ((e.RowIndex >= 0) && (this.dataGV.Rows[e.RowIndex].Cells["tk"].Value.ToString() == "A"))
            {
                e.CellStyle.BackColor = Color.LightGreen;
            }
        }

    }


}