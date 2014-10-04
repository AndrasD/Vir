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
            MyTag[] myTag = (MyTag[])obj[1];

            ktgfelosztastag = myTag[0];
            termektag = myTag[1];
            koltsegtag = myTag[2];
            termekfotag = myTag[3];
            termekaltag = myTag[4];
            koltsegfotag = myTag[5];
            koltsegaltag = myTag[6];

            InitializeComponent();
        }

        private void Eredmeny_Load(object sender, EventArgs e)
        {
            string sel = "";

            mainForm = (VIR.MainForm)this.ParentForm;
            myconn = mainForm.MyConn;

            ktgfelosztastabla = ktgfelosztastag.AdatTablainfo.Adattabla;
            termekfotabla = termekfotag.AdatTablainfo.Adattabla;
            termekaltabla = termekaltag.AdatTablainfo.Adattabla;
            termektabla = termektag.AdatTablainfo.Adattabla;
            koltsegtabla = koltsegtag.AdatTablainfo.Adattabla;
            koltsegfotabla = koltsegfotag.AdatTablainfo.Adattabla;
            koltsegaltabla = koltsegaltag.AdatTablainfo.Adattabla;

            sel = "select '' as TK,'' as Termekkod, '' as Megnevezes, " +
                  "0 as Januar, " +
                  "0 as Februar, " +
                  "0 as Marcius, " +
                  "0 as Aprilis, " +
                  "0 as Majus, " +
                  "0 as Junius, " +
                  "0 as Julius, " +
                  "0 as Augusztus, " +
                  "0 as Szeptember, " +
                  "0 as Oktober, " +
                  "0 as November, " +
                  "0 as December, " +
                  "0 as Osszesen ";
                  //"from dual";
            da = new SqlDataAdapter(sel, myconn);
            da.Fill(ds, "tableEredmeny");
            tableEredmeny = ds.Tables["tableEredmeny"];
            tableEredmeny.Clear();
            viewEredmeny.Table = tableEredmeny;
            viewEredmeny.Sort = "tk desc,termekkod";
            dataGV.DataSource = this.viewEredmeny;
            dataGV.Columns[0].Visible = false;
            dataGV.Columns[0].Frozen = true;
            dataGV.Columns[1].Frozen = true;
            dataGV.Columns[2].Frozen = true;
            for (int i = 3; i < dataGV.Columns.Count; i++) 
            {
                dataGV.Columns[i].DefaultCellStyle.Format = "N0";
                dataGV.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            da = new SqlDataAdapter("select distinct year(datum_telj) as ev from szamla", myconn);
            da.Fill(ds, "tableEvek");
            tableEvek = ds.Tables["tableEvek"];

            for (int i = 0; i < tableEvek.Rows.Count; i++)
                ev.Items.Add(tableEvek.Rows[i]["ev"].ToString());

            ev.Text = ev.Items[0].ToString();

            da = new SqlDataAdapter("select * from rendszer", myconn);
            da.Fill(ds, "tableRendszer");
            tableRendszer = ds.Tables["tableRendszer"];

            string s = "";
            eredmeny_szures = "'";
            if (tableRendszer.Rows.Count > 0)
            {
                s = tableRendszer.Rows[0]["eredmeny_szures"].ToString();
                for (int i = 0; i < s.Length; i++)
                {
                    if (s.Substring(i, 1) == ",")
                    {
                        if (eredmeny_szures.Substring(eredmeny_szures.Length - 1, 1) != "'")
                            eredmeny_szures = eredmeny_szures + "'";
                        if (i + 1 > s.Length)
                            break;
                        else
                            eredmeny_szures = eredmeny_szures + ",'";
                    }
                    else
                        eredmeny_szures = eredmeny_szures + s.Substring(i, 1);
                }
                eredmeny_szures = eredmeny_szures + "'";
            }
            else
                eredmeny_szures = "''";


            tableBevetelek = tableEredmeny.Clone();
            tableKiadasok = tableEredmeny.Clone();
            tableKiadasok.PrimaryKey = new DataColumn[] {tableKiadasok.Columns["Termekkod"]};
            viewBevetelek.Table = tableBevetelek;
            viewKiadasok.Table = tableKiadasok;
            viewBevetelek.Sort = "tk desc,termekkod";
            viewKiadasok.Sort = "tk desc,termekkod";
            dataGVBev.DataSource = viewBevetelek;
            dataGVKiad.DataSource = viewKiadasok;
            dataGVBev.Columns[0].Visible = false;
            dataGVKiad.Columns[0].Visible = false;
            dataGVBev.Columns[0].Frozen = true;
            dataGVKiad.Columns[0].Frozen = true;
            dataGVBev.Columns[1].Frozen = true;
            dataGVKiad.Columns[1].Frozen = true;
            dataGVBev.Columns[2].Frozen = true;
            dataGVKiad.Columns[2].Frozen = true;
          
            for (int i = 3; i < dataGVBev.Columns.Count; i++)
            {
                dataGVBev.Columns[i].DefaultCellStyle.Format = "N0";
                dataGVBev.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGVKiad.Columns[i].DefaultCellStyle.Format = "N0";
                dataGVKiad.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            tableFelgyujt = tableEredmeny.Clone();
        }

        private void havi_CheckedChanged(object sender, EventArgs e)
        {
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

            tableBevetel.Clear();
            sel = "select b.megnid as termek_id, c.kod as termekkod, c.szoveg as Megnevezes, sum(b.netto) as Netto, month(datum_telj) as ho " +
                  "from szamla a, szamla_tetel b, kodtab c  " +
                  "where a.vs='V' "+
                  "  and c.kod not in("+eredmeny_szures+") ";
            if (havi.Checked)
                sel = sel + "and year(datum_telj) = year('" + dat + "') and month(datum_telj) = month('" + dat + "') ";
            else
                sel = sel + "and year(datum_telj) = '" + ev.Text + "' ";
            sel = sel + "and a.id=b.id " +
                        "and b.megnid = c.sorszam " +
                        "group by b.megnid,c.kod,c.szoveg,month(datum_telj)";
            da = new SqlDataAdapter(sel, myconn);
            da.Fill(ds, "tableBevetel");
            tableBevetel = ds.Tables["tableBevetel"];

            tableKiadas.Clear();
            sel = "select b.megnid as koltseg_id, c.kod as koltsegkod, c.szoveg as Megnevezes, sum(b.netto) as Netto, month(datum_telj) as ho " +
                  "from szamla a, szamla_tetel b, kodtab c  " +
                  "where a.vs='S' " +
                  "  and c.kod not in(" + eredmeny_szures + ") ";
            if (havi.Checked)
                sel = sel + "and year(datum_telj) = year('" + dat + "') and month(datum_telj) = month('" + dat + "') ";
            else
                sel = sel + "and year(datum_telj) = '" + ev.Text + "' ";
            sel = sel + "and a.id=b.id " +
                        "and b.megnid = c.sorszam " +
                        "group by b.megnid,c.kod,c.szoveg,month(datum_telj)";
            da = new SqlDataAdapter(sel, myconn);
            da.Fill(ds, "tableKiadas");
            tableKiadas = ds.Tables["tableKiadas"];

            da = new SqlDataAdapter("select 0 as termek_id, 0 as ho, 0.00 as ft, '' as TK ", myconn);
            da.Fill(ds, "tableKiadasTermek");
            tableKiadasTermek = ds.Tables["tableKiadasTermek"];
            tableKiadasTermek.Clear();
            viewKiadasTermek.Table = tableKiadasTermek;
            viewKiadasTermek.Sort = "termek_id, ho";

            koltsegFelosztas();
            tableKiadasokTolt();
            tableBevetelekTolt();
            eredmenySzamitas();

            //viewEredmeny.RowFilter = "koltsegnev = ''";
        }

        private void tablFelgyujtTolt(DataView view)
        {
            tableFelgyujt.Clear();
            if (comboCsoport.Text == "1. Főcsoport")
            {
                int i = 0;
                while (i < view.Count)
                {
                    DataRow ujsor = tableFelgyujt.NewRow();
                    ujsor["tk"] = view[i]["tk"];
                    if (view[i]["tk"].ToString() == "A" )
                    {
                        ujsor["termekkod"] = view[i]["termekkod"];
                        ujsor["megnevezes"] = view[i]["megnevezes"]; ;
                        for (int h = 3; h < view.Table.Columns.Count; h++)
                            ujsor[h] = Convert.ToDecimal(view[i][h].ToString());
                        i++;
                    }
                    else if (view[i]["tk"].ToString() == "K")
                    {
                        string k = view[i]["termekkod"].ToString().Substring(0, 1);
                        DataRow[] t = koltsegfotabla.Select("kod = '" + k + "'");

                        ujsor["termekkod"] = k;
                        ujsor["megnevezes"] = t[0]["szoveg"];
                        decimal[] tomb = new decimal[13] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                        while (i < view.Count && k == view[i]["termekkod"].ToString().Substring(0, 1))
                        {
                            for (int h = 3; h < view.Table.Columns.Count; h++)
                                tomb[h - 3] = tomb[h - 3] + Convert.ToDecimal(view[i][h].ToString());
                            i++;
                        }
                        for (int h = 0; h < tomb.Length; h++)
                            ujsor[h + 3] = tomb[h];
                    }
                    else
                    {
                        string k = view[i]["termekkod"].ToString().Substring(0, 1);
                        DataRow[] t = termekfotabla.Select("kod = '" + k + "'");

                        ujsor["termekkod"] = k;
                        ujsor["megnevezes"] = t[0]["szoveg"];
                        decimal[] tomb = new decimal[13] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                        while (i < view.Count && k == view[i]["termekkod"].ToString().Substring(0, 1))
                        {
                            for (int h = 3; h < view.Table.Columns.Count; h++)
                                tomb[h - 3] = tomb[h - 3] + Convert.ToDecimal(view[i][h].ToString());
                            i++;
                        }
                        for (int h = 0; h < tomb.Length; h++)
                            ujsor[h + 3] = tomb[h];
                    }
                    tableFelgyujt.Rows.Add(ujsor);
                }
            }
            else if (comboCsoport.Text == "2. Alcsoport")
            {
                int i = 0;
                while (i < view.Count)
                {
                    DataRow ujsor = tableFelgyujt.NewRow();
                    ujsor["tk"] = view[i]["tk"];
                    if (view[i]["tk"].ToString() == "A")
                    {
                        ujsor["termekkod"] = view[i]["termekkod"];
                        ujsor["megnevezes"] = view[i]["megnevezes"]; ;
                        for (int h = 3; h < view.Table.Columns.Count; h++)
                            ujsor[h] = Convert.ToDecimal(view[i][h].ToString());
                        i++;
                    }
                    else if (view[i]["tk"].ToString() == "K")
                    {
                        string k = view[i]["termekkod"].ToString().Substring(0, 2);
                        DataRow[] t = koltsegaltabla.Select("kod = '" + k + "'");

                        ujsor["termekkod"] = k;
                        ujsor["megnevezes"] = t[0]["szoveg"];
                        decimal[] tomb = new decimal[13] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                        while (i < view.Count && k == view[i]["termekkod"].ToString().Substring(0, 2))
                        {
                            for (int h = 3; h < view.Table.Columns.Count; h++)
                                tomb[h - 3] = tomb[h - 3] + Convert.ToDecimal(view[i][h].ToString());
                            i++;
                        }
                        for (int h = 0; h < tomb.Length; h++)
                            ujsor[h + 3] = tomb[h];
                    }
                    else
                    {
                        string k = view[i]["termekkod"].ToString().Substring(0, 2);
                        DataRow[] t = termekaltabla.Select("kod = '" + k + "'");

                        ujsor["termekkod"] = k;
                        ujsor["megnevezes"] = t[0]["szoveg"];
                        decimal[] tomb = new decimal[13] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                        while (i < view.Count && k == view[i]["termekkod"].ToString().Substring(0, 2))
                        {
                            for (int h = 3; h < view.Table.Columns.Count; h++)
                                tomb[h - 3] = tomb[h - 3] + Convert.ToDecimal(view[i][h].ToString());
                            i++;
                        }
                        for (int h = 0; h < tomb.Length; h++)
                            ujsor[h + 3] = tomb[h];
                    }
                    tableFelgyujt.Rows.Add(ujsor);
                }
            }
        }

        private void koltsegFelosztas()
        {
            DataRow ujsor;
            tableKiadasTermek.Clear();
            for (int i = 0; i < tableKiadas.Rows.Count; i++)
            {
                decimal n = Convert.ToDecimal(tableKiadas.Rows[i]["Netto"].ToString());

                // megkeresem a ktgfelosztásban az adott ktg-t, majd felosztom a megadott termékekre
                DataRow[] dr = ktgfelosztastabla.Select("koltseg_id = " + tableKiadas.Rows[i]["koltseg_id"].ToString());
                if (dr.Length > 0)
                {
                    for (int j = 0; j < dr.Length; j++)
                    {
                        decimal s = Convert.ToDecimal(dr[j]["szazalek"].ToString());
                        decimal ft = n * s / 100;

                        ujsor = tableKiadasTermek.NewRow();
                        ujsor["tk"] = "T";
                        ujsor["termek_id"] = dr[j]["termek_id"];
                        ujsor["ho"] = tableKiadas.Rows[i]["ho"];
                        ujsor["ft"] = ft;
                        tableKiadasTermek.Rows.Add(ujsor);
                    }
                }
                else // felosztatlan költség
                {
                    ujsor = tableKiadasTermek.NewRow();
                    ujsor["tk"] = "K";
                    ujsor["termek_id"] = tableKiadas.Rows[i]["koltseg_id"];
                    ujsor["ho"] = tableKiadas.Rows[i]["ho"];
                    ujsor["ft"] = n;
                    tableKiadasTermek.Rows.Add(ujsor);
                }
            }
        }

        private void tableKiadasokTolt()
        {
            DataRow ujsor;
            string tKod;
            string tSzoveg;

            viewKiadasok.BeginInit();
            tableKiadasok.Clear();
            int i = 0;
            while (i < viewKiadasTermek.Count)
            {
                int id = Convert.ToInt32(viewKiadasTermek[i]["termek_id"].ToString());
                DataRow[] t = termektabla.Select("sorszam = " + viewKiadasTermek[i]["termek_id"].ToString());
                if (t.Length > 0)
                {
                    tKod = t[0]["kod"].ToString();
                    tSzoveg = t[0]["szoveg"].ToString();
                }
                else
                {
                    DataRow[] k = koltsegtabla.Select("sorszam = " + viewKiadasTermek[i]["termek_id"].ToString());
                    tKod = k[0]["kod"].ToString(); ;
                    tSzoveg = k[0]["szoveg"].ToString() + " (felosztatlan költség)";
                }

                ujsor = tableKiadasok.NewRow();
                ujsor["tk"] = viewKiadasTermek[i]["tk"];
                ujsor["Termekkod"] = tKod;
                ujsor["megnevezes"] = tSzoveg;

                for (int m = 3; m < tableKiadasok.Columns.Count; m++)
                    ujsor[m] = 0;

                while (i < viewKiadasTermek.Count && id == Convert.ToInt32(viewKiadasTermek[i]["termek_id"].ToString()))
                {
                    string hoNap = viewKiadasTermek[i]["ho"].ToString();
                    string hoNev = honapNev(hoNap);
                    ujsor[hoNev] = 0;
                    while (i < viewKiadasTermek.Count && id == Convert.ToInt32(viewKiadasTermek[i]["termek_id"].ToString()) && hoNap == viewKiadasTermek[i]["ho"].ToString())
                    {
                        decimal kiadasN = Convert.ToDecimal(viewKiadasTermek[i]["ft"].ToString());
                        ujsor[hoNev] = Convert.ToDecimal(ujsor[hoNev].ToString()) + kiadasN;
                        i++;
                    }
                }
                ujsor["osszesen"] = termekÖsszesen(ujsor);
                tableKiadasok.Rows.Add(ujsor);
            }

            ujsor = tableKiadasok.NewRow();
            ujsor["tk"] = "A";
            ujsor["Termekkod"] = "Összesen:";
            ujsor["megnevezes"] = "";
            for (i = 3; i < tableKiadasok.Columns.Count; i++)
            {
                string hoNev = tableKiadasok.Columns[i].ColumnName;
                decimal sum = 0;
                for (int j = 0; j < tableKiadasok.Rows.Count; j++)
                    sum = sum + Convert.ToDecimal(tableKiadasok.Rows[j][hoNev].ToString());
                ujsor[hoNev] = sum;
            }
            tableKiadasok.Rows.Add(ujsor);

            if (comboCsoport.Text == "1. Főcsoport" || comboCsoport.Text == "2. Alcsoport")
            {
                tablFelgyujtTolt(viewKiadasok);
                tableKiadasok.Clear();
                tableKiadasok = tableFelgyujt.Copy();
                viewKiadasok.Table = tableKiadasok;
                viewKiadasok.Sort = "tk desc,termekkod";
                dataGVKiad.DataSource = viewKiadasok;
            }

            viewKiadasok.EndInit();
        }

        private void tableBevetelekTolt()
        {
            DataRow ujsor;
            string tKod;
            string tSzoveg;

            viewBevetelek.BeginInit();
            tableBevetelek.Clear();
            int i = 0;
            while (i < tableBevetel.Rows.Count)
            {
                int id = Convert.ToInt32(tableBevetel.Rows[i]["termek_id"].ToString());
                DataRow[] t = termektabla.Select("sorszam = " + tableBevetel.Rows[i]["termek_id"].ToString());
                tKod = t[0]["kod"].ToString();
                tSzoveg = t[0]["szoveg"].ToString();

                ujsor = tableBevetelek.NewRow();
                ujsor["tk"] = "T";
                ujsor["Termekkod"] = tKod;
                ujsor["megnevezes"] = tSzoveg;

                for (int m = 3; m < tableBevetelek.Columns.Count; m++)
                    ujsor[m] = 0;

                while (i < tableBevetel.Rows.Count && id == Convert.ToInt32(tableBevetel.Rows[i]["termek_id"].ToString()))
                {
                    string hoNap = tableBevetel.Rows[i]["ho"].ToString();
                    string hoNev = honapNev(hoNap);
                    ujsor[hoNev] = 0;
                    while (i < tableBevetel.Rows.Count && id == Convert.ToInt32(tableBevetel.Rows[i]["termek_id"].ToString()) && hoNap == tableBevetel.Rows[i]["ho"].ToString())
                    {
                        decimal kiadasN = Convert.ToDecimal(tableBevetel.Rows[i]["netto"].ToString());
                        ujsor[hoNev] = Convert.ToDecimal(ujsor[hoNev].ToString()) + kiadasN;
                        i++;
                    }
                }
                ujsor["osszesen"] = termekÖsszesen(ujsor);
                tableBevetelek.Rows.Add(ujsor);
            }

            ujsor = tableBevetelek.NewRow();
            ujsor["tk"] = "A";
            ujsor["Termekkod"] = "Összesen:";
            ujsor["megnevezes"] = "";
            for (i = 3; i < tableBevetelek.Columns.Count; i++)
            {
                string hoNev = tableBevetelek.Columns[i].ColumnName;
                decimal sum = 0;
                for (int j = 0; j < tableBevetelek.Rows.Count; j++)
                    sum = sum + Convert.ToDecimal(tableBevetelek.Rows[j][hoNev].ToString());
                ujsor[hoNev] = sum;
            }
            tableBevetelek.Rows.Add(ujsor);

            if (comboCsoport.Text == "1. Főcsoport" || comboCsoport.Text == "2. Alcsoport")
            {
                tablFelgyujtTolt(viewBevetelek);
                tableBevetelek.Clear();
                tableBevetelek = tableFelgyujt.Copy();
                viewBevetelek.Table = tableBevetelek;
                viewBevetelek.Sort = "tk desc,termekkod";
                dataGVBev.DataSource = viewBevetelek;

            }
            viewBevetelek.EndInit();
        }

        private void eredmenySzamitas()
        {
            DataRow ujsor;
            DataRow[] r ;
            decimal bevetelN = 0;

            tableEredmeny.Clear();
            for (int i = 0; i < tableBevetelek.Rows.Count; i++ )
            {
                ujsor = tableEredmeny.NewRow();
                ujsor["tk"] = tableBevetelek.Rows[i]["tk"];
                ujsor["Termekkod"] = tableBevetelek.Rows[i]["termekkod"];
                ujsor["megnevezes"] = tableBevetelek.Rows[i]["megnevezes"];

                if (ujsor["tk"].ToString() == "T")
                    r = tableKiadasok.Select("tk = 'K' and termekkod = '" + tableBevetelek.Rows[i]["termekkod"].ToString() +"'");
                else
                    r = tableKiadasok.Select("tk = 'A' and termekkod = '" + tableBevetelek.Rows[i]["termekkod"].ToString() + "'");

                for (int j = 3; j < tableBevetelek.Columns.Count; j++)
                {
                    if (tableBevetelek.Rows[i][j].ToString() == "")
                        bevetelN = 0;
                    else
                        bevetelN = Convert.ToDecimal(tableBevetelek.Rows[i][j].ToString());

                    if (r.Length > 0) // ha van kiadas párja
                        if (r[0][j].ToString() == "")
                            ujsor[j] = bevetelN ;
                        else
                            ujsor[j] = bevetelN - Convert.ToDecimal(r[0][j].ToString());
                    else // nincs kiadas erre a termékre
                        ujsor[j] = bevetelN;
                }

                tableEredmeny.Rows.Add(ujsor);
            }

            for (int i = 0; i < tableKiadasok.Rows.Count; i++)
            {
                if (tableKiadasok.Rows[i]["tk"].ToString() == "K")
                    r = tableBevetelek.Select("tk = 'T' and termekkod = '" + tableKiadasok.Rows[i]["termekkod"].ToString() + "'");
                else
                    r = tableBevetelek.Select("tk = 'A' and termekkod = '" + tableKiadasok.Rows[i]["termekkod"].ToString() + "'");

                if (r.Length == 0)
                {
                    ujsor = tableEredmeny.NewRow();
                    ujsor["tk"] = tableKiadasok.Rows[i]["tk"];
                    ujsor["Termekkod"] = tableKiadasok.Rows[i]["termekkod"];
                    ujsor["megnevezes"] = tableKiadasok.Rows[i]["megnevezes"];
                    for (int j = 3; j < tableKiadasok.Columns.Count; j++)
                        if (tableKiadasok.Rows[i][j].ToString() == "")
                            ujsor[j] = 0;
                        else
                            ujsor[j] = 0 - Convert.ToDecimal(tableKiadasok.Rows[i][j].ToString());
                    tableEredmeny.Rows.Add(ujsor);
                }
            }
        }

        private decimal termekÖsszesen(DataRow r)
        {
            decimal sum = 0;
            for (int i = 3; i < r.Table.Columns.Count; i++)
                sum = sum + Convert.ToDecimal(r[i].ToString());

            return sum;
        }


        private void dataGV_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                Termekkod = dataGV.Rows[e.RowIndex].Cells["termekkod"].Value.ToString();
            }
        }

        private void eves_CheckedChanged(object sender, EventArgs e)
        {
            tableEredmeny.Clear();
            tableKiadasok.Clear();
            tableBevetelek.Clear();
        }

        private void buttonNyomtat_Click(object sender, EventArgs e)
        {
            nyomtat = new PrintForm();
            string[] parNev = { "SZURES"};
            string[] parVal = { ""};
            if (havi.Checked)
                parVal[0] = "Havi: " + honap.Value.ToShortDateString();
            else
                parVal[0] = "Éves: " + ev.Text.ToString();

            string[] parTip = { "string"};

            DataSet dS = new MainProgramm.virDataSet();
            DataRow r;
            for (int i = 0; i < viewBevetelek.Count; i++)
            {
                if (viewBevetelek[i]["tk"].ToString() != "A")
                {
                    r = dS.Tables["Eredmeny"].NewRow();
                    r["bke"] = "1B";
                    for (int j = 0; j < tableBevetelek.Columns.Count; j++)
                    {
                        string colName = tableBevetelek.Columns[j].ColumnName;
                        if (r.Table.Columns.IndexOf(colName) > -1)
                            r[colName] = viewBevetelek[i][colName];
                    }
                    dS.Tables["Eredmeny"].Rows.Add(r);
                }
            }

            for (int i = 0; i < viewKiadasok.Count; i++)
            {
                if (viewKiadasok[i]["tk"].ToString() != "A")
                {
                    r = dS.Tables["Eredmeny"].NewRow();
                    r["bke"] = "2K";
                    for (int j = 0; j < tableKiadasok.Columns.Count; j++)
                    {
                        string colName = tableKiadasok.Columns[j].ColumnName;
                        if (r.Table.Columns.IndexOf(colName) > -1)
                            r[colName] = viewKiadasok[i][colName];
                    }
                    dS.Tables["Eredmeny"].Rows.Add(r);
                }
            }

            for (int i = 0; i < viewEredmeny.Count; i++)
            {
                if (viewEredmeny[i]["tk"].ToString() != "A")
                {
                    r = dS.Tables["Eredmeny"].NewRow();
                    r["bke"] = "3E";
                    for (int j = 0; j < tableEredmeny.Columns.Count; j++)
                    {
                        string colName = tableEredmeny.Columns[j].ColumnName;
                        if (r.Table.Columns.IndexOf(colName) > -1)
                            r[colName] = viewEredmeny[i][colName];
                    }
                    dS.Tables["Eredmeny"].Rows.Add(r);
                }
            }

            nyomtat.PrintParams(parNev, parVal, parTip);
            eredmenyLista.SetDataSource(dS);
            eredmenyLista.SetParameterValue("SZURES", parVal[0]);

            nyomtat.reportSource = eredmenyLista;
            nyomtat.DoPreview(mainForm.defPageSettings);

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
                    ret = "Marcius";
                    break;
                case "4":
                    ret = "Aprilis";
                    break;
                case "5":
                    ret = "Majus";
                    break;
                case "6":
                    ret = "Junius";
                    break;
                case "7":
                    ret = "Julius";
                    break;
                case "8":
                    ret = "Augusztus";
                    break;
                case "9":
                    ret = "Szeptember";
                    break;
                case "10":
                    ret = "Oktober";
                    break;
                case "11":
                    ret = "November";
                    break;
                case "12":
                    ret = "December";
                    break;
            }
            return ret;
        }

        private void dataGV_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (dataGV.Rows[e.RowIndex].Cells["tk"].Value.ToString() == "A")
                {
                    e.CellStyle.BackColor = Color.LightGreen;
                }
            }
        }

    }


}