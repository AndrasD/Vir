using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Könyvtar.ClassGyujtemeny;
using System.Data.SqlClient;
using TableInfo;

namespace Törzsadatok
{
    public partial class Ktgfelosztas : UserControl
    {
        private VIR.MainForm mainForm;

        private MyTag ktgfelosztastag;
        private Tablainfo Ktgfelosztasinfo;
        private DataTable KtgfelosztasAdattabla;
        private MyTag koltsegtag;
        private MyTag termekfotag;
        private MyTag termekaltag;
        private MyTag termektag;
        private DataTable koltsegtabla;
        private DataTable termekfotabla;
        private DataTable termekaltabla;
        private DataTable termektabla;
        private DataView dataViewFelosztas = new DataView();

        private Fak Fak;

        private TreeNode aktnode;
        private string koltseg_id;
        private int rowIndex;
        private bool igazivaltozas = false;
        private Egyallapotinfo egyallapot;

        private SqlConnection myconn = new SqlConnection();
        private DataSet ds = new DataSet();
        private SqlDataAdapter da;

        public Ktgfelosztas(string szoveg, object[] obj)
        {
            MyTag[] myTag = (MyTag[])obj[1];
            ktgfelosztastag = myTag[0];
            Ktgfelosztasinfo = ktgfelosztastag.AdatTablainfo;

            termekfotag = myTag[2];
            termekaltag = myTag[3];
            termektag = myTag[4];

            InitializeComponent();
            TreeViewIni();

            Fak = ktgfelosztastag.Fak;

            koltsegtag = myTag[1];
            //Ktgfelosztasinfo.AktCombotolt("KOLTSEG_ID", koltsegtag);

            koltsegtabla = koltsegtag.AdatTablainfo.Adattabla;
            Fak.ControlTagokTolt(this,(Control)groupBox2);

            cbSema.Items.Clear();
            da = new SqlDataAdapter("select sorszam, kod+' '+szoveg as nev from kodtab where kodtipus = 'Koltalcsop'", ktgfelosztastag.AdatTablainfo.Adatconn);
            da.Fill(ds, "koltsegtabla");
            koltsegtabla = ds.Tables["koltsegtabla"];
            //for (int i = 0; i < koltsegtabla.Rows.Count; i++)
            //    cbSema.Items.Add(koltsegtabla.Rows[i]["nev"].ToString());

            cbSema.DataSource = koltsegtabla;
            if (cbSema.Items.Count > 0) cbSema.SelectedIndex = 0;

            cbSema.DisplayMember = "nev";
            cbSema.ValueMember = "sorszam";

            egyallapot = Ktgfelosztasinfo.GetEgyallapotinfo(this);
            dataGVFelo.AutoGenerateColumns = false;
            DataGridViewColumn[] kalkgridcols = Ktgfelosztasinfo.GetGridColumns(false);
            dataGVFelo.Columns.AddRange(kalkgridcols);
            dataGVFelo.Columns[0].Visible = false;  ///termek_id
            KtgfelosztasAdattabla = ((Adattablak)Ktgfelosztasinfo.Initselinfo.Adattablak[Ktgfelosztasinfo.Initselinfo.Aktualadattablaindex]).Adattabla;

            //da = new SqlDataAdapter("select a.*, '' as koltseg_id_k, k.szoveg as termek_id_k from ktgfelosztas a, " +
            //                        " kodtab k where a.termek_id = k.sorszam", ktgfelosztastag.AdatTablainfo.Adatconn);
            //da.Fill(ds, "KtgfelosztasAdattabla");
            //KtgfelosztasAdattabla = ds.Tables["KtgfelosztasAdattabla"];

            for (int i=0; i<KtgfelosztasAdattabla.Rows.Count; i++)
            {
                DataRow[] r = termektabla.Select("sorszam = " + KtgfelosztasAdattabla.Rows[i]["termek_id"].ToString());
                if (r.Length > 0)
                    KtgfelosztasAdattabla.Rows[i]["termek_id_k"] = r[0]["kod"].ToString() + " " + r[0]["szoveg"].ToString();
            }

            dataViewFelosztas.Table = KtgfelosztasAdattabla;
            dataViewFelosztas.Sort = Ktgfelosztasinfo.Sort;
            koltseg_id = cbSema.SelectedValue.ToString();
            //                DataRow[] dr = termektabla.Select("szoveg ='" + comboBox1.Text + "'");
            //               termek_id = dr[0]["sorszam"].ToString();
            if (koltseg_id != "")
                dataViewFelosztas.RowFilter = "koltseg_id = " + koltseg_id;
            if (dataViewFelosztas.Count == 0)
                Ktgfelosztasinfo.Aktsorindex = -1;
            else
                Ktgfelosztasinfo.Aktsorindex = 0;

 //           dataViewKalkulacio.RowFilter = "termek_id is null";
            dataGVFelo.DataSource = dataViewFelosztas;
            igazivaltozas = true;
        }

        private void KtgFelosztas_Load(object sender, EventArgs e)
        {
            buttonMentes_Click(sender, e);
        }

        private void TreeViewIni()
        {
            termekfotabla = termekfotag.AdatTablainfo.Adattabla;
            termekaltabla = termekaltag.AdatTablainfo.Adattabla;
            termektabla = termektag.AdatTablainfo.Adattabla;

            TreeNode fonode;
            TreeNode alnode;
            TreeNode node;

            for (int i = 0; i < termekfotabla.Rows.Count; i++)
            {
                fonode = new TreeNode(termekfotabla.Rows[i]["KOD"].ToString() + " " + termekfotabla.Rows[i]["SZOVEG"].ToString());
                fonode.Tag = termekfotabla.Rows[i]["sorszam"].ToString().Trim();
                treeView.Nodes.Add(fonode);

                DataRow[] altab = termekaltabla.Select("substring(kod,1,1)='" + termekfotabla.Rows[i]["kod"] + "'");
                for (int j = 0; j < altab.Length; j++)
                {
                    alnode = new TreeNode(altab[j]["KOD"].ToString()+" "+altab[j]["SZOVEG"].ToString());
                    alnode.Tag = altab[j]["sorszam"].ToString().Trim();
                    fonode.Nodes.Add(alnode);

                    DataRow[] tab = termektabla.Select("substring(kod,1,2)='" + altab[j]["kod"] + "'");
                    for (int k = 0; k < tab.Length; k++)
                    {
                        node = new TreeNode(tab[k]["KOD"].ToString() + " " + tab[k]["SZOVEG"].ToString());
                        node.Tag = tab[k]["sorszam"].ToString().Trim();
                        alnode.Nodes.Add(node);
                    }
                }
            }
        }

        private void treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeViewHitTestInfo hittest = treeView.HitTest(e.X, e.Y);
            aktnode = hittest.Node;
        }

        private void buttonMentes_Click(object sender, EventArgs e)
        {
            if (felosztva.Text != "100 %")
            {
                MessageBox.Show("   Ez nem 100 %!   ","",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else
            {
                ArrayList list = new ArrayList();
                igazivaltozas = false;
                string combotext = cbSema.Text;
                int combosel = Convert.ToInt32(cbSema.SelectedValue);
                int cbSI = cbSema.SelectedIndex;
                list.Add(Ktgfelosztasinfo);
                ((Adattablak)Ktgfelosztasinfo.Initselinfo.Adattablak[Ktgfelosztasinfo.Initselinfo.Aktualadattablaindex]).Added = true;
                if (!Fak.UpdateTransaction(list))
                {
                    this.Enabled = true;
                    this.Visible = false;
                }
                else
                {
                    KtgfelosztasAdattabla = ((Adattablak)Ktgfelosztasinfo.Initselinfo.Adattablak[Ktgfelosztasinfo.Initselinfo.Aktualadattablaindex]).Adattabla;
                    for (int i = 0; i < KtgfelosztasAdattabla.Rows.Count; i++)
                    {
                        DataRow[] r = termektabla.Select("sorszam = " + KtgfelosztasAdattabla.Rows[i]["termek_id"].ToString());
                        if (r.Length > 0)
                            KtgfelosztasAdattabla.Rows[i]["termek_id_k"] = r[0]["kod"].ToString() + " " + r[0]["szoveg"].ToString();
                    }
                    dataViewFelosztas.Table = KtgfelosztasAdattabla;
                    dataViewFelosztas.Sort = Ktgfelosztasinfo.Sort;
                    cbSema.Text = combotext;
                    cbSema.SelectedIndex = cbSI;
                    dataViewFelosztas.RowFilter = "koltseg_id = " + koltseg_id;
                }
                igazivaltozas = true;
            }
        }

        private void button_be_Click(object sender, EventArgs e)
        {
            if (aktnode != null)
            {
                if (aktnode.Level == 0)
                {
                    for (int i = 0; i < aktnode.Nodes.Count; i++)
                        for (int j = 0; j < aktnode.Nodes[i].Nodes.Count; j++)
                        {
                            TreeNode tn = aktnode.Nodes[i].Nodes[j];
                            if (! termekKeres(tn.Tag))
                            {
                                int szaz = 100;
                                for (int s = 0; s < dataViewFelosztas.Count; s++)
                                    szaz = szaz - Convert.ToInt32(dataViewFelosztas[s]["szazalek"].ToString());

                                KtgfelosztasAdattabla = Ktgfelosztasinfo.Ujsor();
                                int aktrow = KtgfelosztasAdattabla.Rows.Count - 1;
                                //Elokalkulacioinfo.Adatsortolt(aktrow);
                                KtgfelosztasAdattabla.Rows[aktrow]["koltseg_id"] = koltseg_id;
                                KtgfelosztasAdattabla.Rows[aktrow]["termek_id"] = tn.Tag;
                                KtgfelosztasAdattabla.Rows[aktrow]["termek_id_k"] = tn.Text.Substring(5);
                                KtgfelosztasAdattabla.Rows[aktrow]["szazalek"] = szaz;
                            }
                        }
                }

                if (aktnode.Level == 1)
                {
                    for (int i = 0; i < aktnode.Nodes.Count; i++)
                    {
                        TreeNode tn = aktnode.Nodes[i];
                        if (!termekKeres(tn.Tag))
                        {
                            int szaz = 100;
                            for (int s = 0; s < dataViewFelosztas.Count; s++)
                                szaz = szaz - Convert.ToInt32(dataViewFelosztas[s]["szazalek"].ToString());

                            KtgfelosztasAdattabla = Ktgfelosztasinfo.Ujsor();
                            int aktrow = KtgfelosztasAdattabla.Rows.Count - 1;
                            //Elokalkulacioinfo.Adatsortolt(aktrow);
                            KtgfelosztasAdattabla.Rows[aktrow]["koltseg_id"] = koltseg_id;
                            KtgfelosztasAdattabla.Rows[aktrow]["termek_id"] = tn.Tag;
                            KtgfelosztasAdattabla.Rows[aktrow]["termek_id_k"] = tn.Text.Substring(5);
                            KtgfelosztasAdattabla.Rows[aktrow]["szazalek"] = szaz;
                        }
                    }
                }

                if (aktnode.Level == 2)
                {
                    if (!termekKeres(aktnode.Tag))
                    {
                        int szaz = 100;
                        for (int s = 0; s < dataViewFelosztas.Count; s++)
                            szaz = szaz - Convert.ToInt32(dataViewFelosztas[s]["szazalek"].ToString());

                        KtgfelosztasAdattabla = Ktgfelosztasinfo.Ujsor();
                        int aktrow = KtgfelosztasAdattabla.Rows.Count - 1;
                        //Elokalkulacioinfo.Adatsortolt(aktrow);
                        KtgfelosztasAdattabla.Rows[aktrow]["koltseg_id"] = koltseg_id;
                        KtgfelosztasAdattabla.Rows[aktrow]["termek_id"] = aktnode.Tag;
                        KtgfelosztasAdattabla.Rows[aktrow]["termek_id_k"] = aktnode.Text.Substring(5);
                        KtgfelosztasAdattabla.Rows[aktrow]["szazalek"] = szaz;
                    }
                }

                Felosztva();
            }
        }

        private bool termekKeres(object tid)
        {
            DataRow[] r = KtgfelosztasAdattabla.Select("koltseg_id = " + koltseg_id + " and termek_id = " + tid);
            if (r.Length == 0)
                return false;
            else
                return true;
        }

        private void Felosztva()
        {
            int szaz = 0;
            for (int s = 0; s < dataViewFelosztas.Count; s++)
                szaz = szaz + Convert.ToInt32(dataViewFelosztas[s]["szazalek"].ToString());
            felosztva.Text = szaz.ToString() + " %";
        }

        private void button_ki_Click(object sender, EventArgs e)
        {
            if (dataGVFelo.SelectedRows.Count > 0)
                dataViewFelosztas[dataGVFelo.CurrentRow.Index].Row.Delete();
             //   ElokalkulacioAdattabla = Elokalkulacioinfo.Adatsortorol(rowIndex);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (igazivaltozas && cbSema.Text != "")
            {
                koltseg_id = cbSema.SelectedValue.ToString();
                dataViewFelosztas.RowFilter = "koltseg_id = " + koltseg_id;
                Felosztva();
            }
        }

        private void dataGVFelo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            rowIndex = e.RowIndex;
        }

        private void buttonVissza_Click(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                Adattablak at = (Adattablak)Ktgfelosztasinfo.Initselinfo.Adattablak[Ktgfelosztasinfo.Initselinfo.Aktualadattablaindex];
                if (!at.Added && !at.Deleted && !at.Modified && !at.Rowadded ||
                    MessageBox.Show("A valtozasok elvesszenek?", "", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    Fak.ForceAdattolt(Ktgfelosztasinfo);
                    this.Visible = false;
                }
            }
        }

        private void dataGVFelo_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            int r = e.RowIndex;
            int c = e.ColumnIndex;

            if (dataGVFelo.Columns[c].HeaderText == "Százalék")
            {
                int s = Convert.ToInt32(dataGVFelo.Rows[r].Cells[c].Value.ToString());
                if (s < 0 || s > 100)
                {
                    MessageBox.Show("A százalék csak 0 és 100 között lehet", "", MessageBoxButtons.OK);
                }
                Felosztva();
            }
        }

        private void dataGVFelo_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            Felosztva();
        }

        private void dataGVFelo_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            Felosztva();
        }

        private void dataGVFelo_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            Felosztva();
        }

    }
}