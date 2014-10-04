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
using System.Data.Odbc;
using MainProgramm.Listák;

namespace Törzsadatok
{
    public partial class KodForm : UserControl
    {
        private VIR.MainForm mainForm;
        private Tablainfo Focsopinfo;
        private DataTable Focsoptabla;
        private Tablainfo Alcsopinfo;
        private DataTable Alcsoptabla;
        private Fak Fak;
        private ArrayList FocsopTablaColumns;
        private ArrayList AlcsopTablaColumns;
        private MyTag Focsoptag;
        private MyTag Alcsoptag;
        private TreeNode aktnode;
        private bool buttonvisszavolt = false;

        private PrintForm nyomtat = new PrintForm();
        private EredmenyLista eredmenyLista = new EredmenyLista();

        //       private ArrayList KiegColumns;
        private int sorrendcol = -1;
        private int szovegcol = -1;
        private int identitycol = -1;
        private int azonositocol = -1;
        private int fokodcol = -1;
        private int tablanevcol;
        private int kodtipuscol;
        private int azontipcol;
        private int aktualtablerowindex;
        private int aktsorrend;
        private string aktazon;
        private int aktualgridrowindex;
        private string funkcio;
        private Cols sorrendegycol = null;
        private int maxsorrend = 0;
        private int maxsorrrowind = -1;
        private string foaktkod = "";
        private string foaktkodlen = "1";
        private string Alaktkod = "";
        private string Alaktkodlen = "1";
        private MyTag Alalcsoptag = null;
        private Tablainfo Alalcsopinfo = null;
        private DataTable Alalcsoptabla = null;
        private Egycontrolinfo egycont;
        private Egyallapotinfo egyallapot;
        public bool Modositott
        {
            get { return buttonMentes.Enabled; }
        }


        public KodForm(string szoveg,object[] obj)
        {
            InitializeComponent();
            MyTag[] tagok = (MyTag[])obj[1];
            Focsoptag = tagok[0];
            Fak = Focsoptag.Fak;
            Focsopinfo = Focsoptag.AdatTablainfo;
 //           aktinfo=Focsopinfo;
//            Focsoptag.AdatSelWhere = " where JEL='" + Jel + "'";
            groupBox1.Text =Focsoptag.Szoveg;
            Focsoptabla = Focsopinfo.Adattabla;
            if(Focsoptabla.Rows.Count==0)
                this.Visible=false;
            if(this.Visible)
            {
                FocsopTablaColumns = Focsopinfo.TablaColumns;
                fokodcol = Focsopinfo.Kodcol;
                Alcsoptag = tagok[1];
                Alcsopinfo = Alcsoptag.AdatTablainfo;
                egyallapot = Alcsopinfo.CreateEgyallapotinfo(this);
                Alcsoptabla = Alcsopinfo.Adattabla;
                Alaktkodlen = Alcsopinfo.Kodhossz.ToString();
                AlcsopTablaColumns = Alcsopinfo.TablaColumns;
                treeView1 = TreeViewini(treeView1);
                treeView1.SelectedNode = treeView1.Nodes[0];
                aktnode=treeView1.SelectedNode;
                Controlini(Alcsopinfo);
                DataGridViewini(true);
                if (tagok.Length > 2)
                {
                    Alalcsoptag = tagok[2];
                    Alalcsopinfo = Alalcsoptag.AdatTablainfo;
                    Alalcsoptabla = Alalcsopinfo.Adattabla;
                    dataView2.Table = Alalcsoptabla;
                    dataView2.Sort = Alalcsopinfo.Sort;
                }
            }
        }

        private TreeView TreeViewini(TreeView tv)
        {
            TreeNode fonode;
            DataTable dt=Focsopinfo.Adattabla;
            DataTable dta=Alcsopinfo.Adattabla;
            string foaktkod = "";
 //           string foidentname=Focsopinfo.Identity;
            for (int i=0;i<dt.Rows.Count;i++)
            {
                DataRow dr=dt.Rows[i];
                fonode=new TreeNode(dr["KOD"].ToString()+" "+dr["SZOVEG"].ToString());
                foaktkod=dr[fokodcol].ToString().Trim();
                fonode.Tag=foaktkod;
                tv.Nodes.Add(fonode);
            }
            foaktkodlen = foaktkod.Length.ToString();
            return tv;
        }

        private void Controlini(Tablainfo tabinfo)
        {
            int egyci = tabinfo.GetEgycontrolindex(this);
            if (egyci == -1)
                egycont = tabinfo.CreateControlinfo(this);
            else
                egycont = tabinfo.GetEgycontrolinfo(this);
            egycont.InputelemArray.Clear();
            for (int i = 0; i < this.Controls.Count; i++)
            {
                Control egycontrol = this.Controls[i];
                if (egycontrol.GetType().FullName == "System.Windows.Forms.TextBox")
                {
                    object otag = (object)egycontrol.Tag;
                    if (otag != null)
                    {
                        Taggyart tg = new Taggyart(tabinfo, otag.ToString());
                        egycontrol.Tag = tg;
                        tg.Control = egycontrol;
                        tg.Controltipus = "TextBox";
                        ((TextBox)egycontrol).MaxLength = tg.MaxLength;
                        egycont.InputelemArray.Add(tg);
                    }
                }
            }
            egycont.Inputeleminfok=new Taggyart[egycont.InputelemArray.Count];
            for (int i = 0; i < egycont.InputelemArray.Count; i++)
                egycont.Inputeleminfok[i] = (Taggyart)egycont.InputelemArray[i];
            tabinfo.Aktcontinfo = egycont;
        }

        private void DataGridViewini(bool elso)
        {
            foaktkod =aktnode.Tag.ToString();
            if (elso)
            {
                dataGridView1.AutoGenerateColumns = false;
                azonositocol = Alcsopinfo.Azonositocol;
                sorrendcol = Alcsopinfo.Sorrendcol;
                sorrendegycol = Alcsopinfo.SorrendColumn;
                if (sorrendegycol != null)
                    sorrendcol = Alcsopinfo.Sorrendcolcol;
                identitycol = Alcsopinfo.Identitycol;
                if (identitycol == -1)
                    identitycol = azonositocol;
                szovegcol = Alcsopinfo.Szovegcol;
                tablanevcol = Alcsopinfo.Tablanevcol;
                kodtipuscol = Alcsopinfo.Kodtipuscol;
                azontipcol = Alcsopinfo.Azontipcol;
                DataGridViewColumn[] gridcols = Alcsopinfo.GetGridColumns();
                dataGridView1.Columns.AddRange(gridcols);
                Alcsoptabla = ((Adattablak)Alcsopinfo.Initselinfo.Adattablak[Alcsopinfo.Initselinfo.Aktualadattablaindex]).Adattabla;
                dataView1.Table = Alcsoptabla;
                dataView1.Sort = Alcsopinfo.Sort;
            }
            dataView1.RowFilter = "substring(KOD,1,"+foaktkodlen+")="+ foaktkod;
            dataGridView1.DataSource = dataView1;
            aktualgridrowindex = 0;
            groupBox2.Text = aktnode.Text;
//            if (Alcsopinfo.Tablatag.Szoveg == "alcsoportok")
//                groupBox2.Text += " alcsoportok";
//            else if(Jel=="T")
//                groupBox2.Text+=" termékek";
 //           else
//                groupBox2.Text+=" költségek";
            groupBox2.Text += " " + Alcsoptag.Szoveg;
//            Cols egycol = (Cols)Alcsopinfo.TablaColumns[Alcsopinfo.GetTablaColIndex(Focsopidentity)];
//            egycol.Tartalom = foidentity;
//            egycol.Defert = foidentity;
//            Focsopinfo.Aktidentity = Convert.ToInt32(foidentity);
            Maxsorfind();
            if (dataView1.Count != 0)
            {
                aktazon = dataView1[0][identitycol].ToString().Trim();
                aktualtablerowindex = Alcsoptabla.Rows.IndexOf(dataView1[aktualgridrowindex].Row);
                aktsorrend = Convert.ToInt32(Alcsoptabla.Rows[aktualtablerowindex][sorrendcol].ToString());
                Maxsorfind();
                funkcio = "Modosit";
                textAzonosito.ReadOnly = true;
                buttonUj.Enabled = true;
                buttonTöröl.Enabled = true;
            }
            else
            {
                aktualtablerowindex = -1;
                aktsorrend = maxsorrend + 100;
                funkcio = "Beszur";
                textAzonosito.ReadOnly = false;
                buttonUj.Enabled = false;
                buttonTöröl.Enabled = false;
                buttonMentes.Enabled = false;
                egyallapot.Modositott = false;
            }

            Alcsopinfo.Aktsorindex = aktualtablerowindex;
            if(funkcio=="Beszur")
                textAzonosito.Text = foaktkod;
        }

        private void Control_Validated(object sender, EventArgs e)
        {
            if (Focsoptabla.Rows.Count == 0)
                this.Visible = false;
            if (this.Visible)
            {
                Taggyart tg = (Taggyart)((Control)sender).Tag;
                string hibaszov = Fak.Hibavizsg(this, Alcsopinfo, (Control)sender);
                if (hibaszov == "")
                {
                    hibaszov += EgyediValidalas((TextBox)sender);
                    Fak.ErrorProvider.SetError((Control)sender, hibaszov);
                }
                if (hibaszov == "")
                {
                    for (int i = 0; i < egycont.Inputeleminfok.Length; i++)
                    {
                        TextBox egytext = (TextBox)((Taggyart)egycont.Inputeleminfok[i]).Control;
                        if (egytext != (TextBox)sender)
                        {
                            hibaszov += Fak.Hibavizsg(this, Alcsopinfo, egytext);
                            Fak.ErrorProvider.SetError(egytext, "");
                            if (hibaszov == "")
                            {
                                hibaszov += EgyediValidalas(egytext);
                                //                               Fak.ErrorProvider.SetError(egytext, hibaszov);
                            }
                        }
                    }
                    if (hibaszov == "")
                    {
                        if (funkcio == "Beszur")
                        {
                            dataView1.RowFilter = "";
                            dataView1.Sort = "";
                            Alcsopinfo.Ujsor();
                            aktualtablerowindex = Alcsoptabla.Rows.Count - 1;
                            maxsorrend = aktsorrend;
                            maxsorrrowind = aktualtablerowindex;
                        }
                        Alcsopinfo.Adatsortolt(aktualtablerowindex);
                        Alcsoptabla.Rows[aktualtablerowindex][sorrendcol] = aktsorrend;
                        if (funkcio == "Beszur")
                        {
                            aktsorrend = maxsorrend + 100;
                            textAzonosito.ReadOnly = false;
                            aktualtablerowindex = -1;
                            Alcsopinfo.Aktsorindex = -1;
                            textAzonosito.ReadOnly = false;
                            textAzonosito.Text = foaktkod;
                            dataView1.RowFilter = "substring(KOD,1," + foaktkodlen + ")=" + foaktkod;
                            dataView1.Sort = Alcsopinfo.Sort;
                            buttonTöröl.Enabled = false;
                            buttonUj.Enabled = false;
                        }
                        else
                        {
                            buttonUj.Enabled = true;
                            buttonTöröl.Enabled = true;
                        }
                        buttonMentes.Enabled = true;
                        egyallapot.Modositott = true;
                    }
                    else
                    {
                        buttonMentes.Enabled = false;
                        egyallapot.Modositott = false;
                    }
                }
            }
        }
        private string EgyediValidalas(TextBox tb)
        {
            string hibaszov = "";
            Taggyart tg = (Taggyart)tb.Tag;
            if (tg != null && tg.Valid != -1)
            {
                switch (tg.Valid)
                {
                    case 1:
                        Cols egycol = (Cols)Alcsopinfo.TablaColumns[tg.egycolind];
                        if (tb.Text.Trim().Length != tg.MaxLength)
                            hibaszov = egycol.Sorszov + " kotelezoen " + tg.MaxLength.ToString() + " jegyu!\n";
                        else
                        {
                            try
                            {
                                Convert.ToInt64(tb.Text);
                                if (tb.Text.Trim().Substring(0, foaktkod.Length) != foaktkod)
                                    hibaszov += egycol.Sorszov + " elso " + foaktkod.Length + " jegye kotelezoen " + foaktkod;
                            }
                            catch
                            {
                                hibaszov += egycol.Sorszov + " kotelezoen numerikus!\n";
                            }
                        }
                        break;
                }
            }
            return hibaszov;
        }
        private void buttonUj_Click(object sender, EventArgs e)
        {
            if (Focsoptabla.Rows.Count == 0)
                this.Visible = false;
            if (this.Visible)
            {
                buttonTöröl.Enabled = false;
                if (funkcio != "Beszur")
                {
                    funkcio = "Beszur";
                    if (Alcsoptabla.Rows.Count == 0)
                        aktsorrend = 100;
                    else
                        aktsorrend = maxsorrend + 100;
                }
                textAzonosito.ReadOnly = false;
                aktualtablerowindex = -1;
                Alcsopinfo.Aktsorindex = -1;
                textAzonosito.Text = foaktkod;
                buttonUj.Enabled = false;
            }
        }
        private void Errortorol()
        {
            for (int i = 0; i < egycont.Inputeleminfok.Length; i++)
            {
                TextBox tb = (TextBox)((Taggyart)egycont.Inputeleminfok[i]).Control;
                Fak.ErrorProvider.SetError(tb, "");
            }
        }
        private void buttonTorol_Click(object sender, EventArgs e)
        {
            if (Focsoptabla.Rows.Count == 0)
                this.Visible = false;
            if (this.Visible)
            {
                bool hiba = false;
                if (Alalcsoptag != null)
                {
                    Alaktkod = dataView1[aktualgridrowindex]["KOD"].ToString();
                    dataView2.RowFilter = "substring(KOD,1," + Alaktkodlen + ")=" + Alaktkod;
                    if (dataView2.Count != 0)
                    {
                        hiba = true;
                        MessageBox.Show("Eloszor az alsobb szint(ek) torlendoek!");
                    }
                }
                if (!hiba)
                {
                    Errortorol();
                    Alcsopinfo.Adatsortorol(aktualtablerowindex);
                    //            if(aktualtablerowindex==maxsorrrowind)
                    Maxsorfind();
                    if (dataView1.Count == 0)
                    {
                        funkcio = "Beszur";
                        if (Alcsoptabla.Rows.Count == 0)
                            aktsorrend = 100;
                        else
                            aktsorrend = maxsorrend + 100;
                        aktualtablerowindex = -1;
                        Alcsopinfo.Aktsorindex = -1;
                        buttonTöröl.Enabled = false;
                        buttonUj.Enabled = false;
                        //                    buttonMentes.Enabled = false;
                        textAzonosito.ReadOnly = false;
                        textAzonosito.Text = foaktkod;
                        buttonMentes.Enabled = true;
                        egyallapot.Modositott=true;

                    }
                    else
                    {
                        if (aktualgridrowindex > dataView1.Count - 1)
                        {
                            aktualgridrowindex--;
                            DataGridViewSelectedRowCollection drr = dataGridView1.SelectedRows;
                            DataGridViewRow drv;
                            if (drr.Count != 0)
                            {
                                for (int i = 0; i < drr.Count; i++)
                                {
                                    drv = drr[i];
                                    drv.Selected = false;
                                }
                            }

                            dataGridView1.Rows[aktualgridrowindex].Selected = true;
                        }
                        aktualtablerowindex = Alcsoptabla.Rows.IndexOf(dataView1[aktualgridrowindex].Row);
                        aktsorrend = Convert.ToInt32(Alcsoptabla.Rows[aktualtablerowindex][sorrendcol].ToString());
                        funkcio = "Modosit";
                        Alcsopinfo.Aktsorindex = aktualtablerowindex;
                        textAzonosito.ReadOnly = true;
                        buttonUj.Enabled = true;
                        buttonTöröl.Enabled = true;
                        buttonMentes.Enabled = true;
                        egyallapot.Modositott=true;
                    }
                }
            }
        }
        private void Maxsorfind()
        {
            maxsorrend = 0;
            maxsorrrowind = -1;
            for (int i = 0; i < Alcsoptabla.Rows.Count; i++)
            {
                DataRow dr = Alcsoptabla.Rows[i];
                if (dr.RowState != DataRowState.Deleted)
                {
                    if (Convert.ToInt32(dr[sorrendcol].ToString()) > maxsorrend)
                    {
                        maxsorrend = Convert.ToInt32(dr[sorrendcol].ToString());
                        maxsorrrowind = i;
                    }
                }
            }
        }

        private void buttonMentes_Click(object sender, EventArgs e)
        {
            if (Focsoptabla.Rows.Count == 0)
                this.Visible = false;
            if (this.Visible)
            {
                string hibaszov="";
                string aktualkod = "";
                if (buttonUj.Enabled)
                {
                    hibaszov = Fak.Hibavizsg(this, Alcsopinfo, null);
                    if (hibaszov == "")
                    {
                        hibaszov = EgyediValidalas(textAzonosito);
                        Fak.ErrorProvider.SetError(textAzonosito, hibaszov);
                        if (hibaszov == "")
                        {
                            hibaszov = EgyediValidalas(textMegnevezes);
                            Fak.ErrorProvider.SetError(textMegnevezes, hibaszov);
                        }
                    }
                    if(hibaszov=="")
                        aktualkod=textAzonosito.Text;
                }
                if(hibaszov=="")
                {
                    //                   if (funkcio == "Beszur")
                    //                       aktualkod = Alcsoptabla.Rows[Alcsoptabla.Rows.Count - 1]["KOD"].ToString();
                    //                   else
                    //                       aktualkod = Alcsoptabla.Rows[aktualtablerowindex]["KOD"].ToString();
                    //               }

                    ArrayList ar = new ArrayList();
                    //                ar.Add(Focsopinfo);
                    ar.Add(Alcsopinfo);
                    Fak.UpdateTransaction(ar);
                    string rowfilt = dataView1.RowFilter;
                    Alcsoptabla = Alcsopinfo.Adattabla;
                    dataView1.Table = Alcsoptabla;
                    dataView1.Sort = Alcsopinfo.Sort;
                    dataView1.RowFilter = rowfilt;
                    if (funkcio == "Beszur" && dataView1.Count != 0)
                    {
                        //                   string find1=Focsopidentity+"='"+aktnode.Tag.ToString()+"'";
                        //                   string find2="KOD='"+aktualkod+"'";
                        //                   aktualgridrowindex = dataView1.Find((object)aktualkod);
                        //                   aktualtablerowindex = Alcsoptabla.Rows.IndexOf(dataView1[aktualgridrowindex].Row);
                        //                  textAzonosito.ReadOnly = true;
                        funkcio = "Modosit";
                        //                   Alcsopinfo.Aktsorindex = aktualtablerowindex;
                    }
                    if (aktualkod != "")
                    {
                        aktualgridrowindex = dataView1.Find((object)aktualkod);
                        aktualtablerowindex = Alcsoptabla.Rows.IndexOf(dataView1[aktualgridrowindex].Row);
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                            dataGridView1.Rows[i].Selected = false;
                        dataGridView1.Rows[aktualgridrowindex].Selected = true;
                        textAzonosito.ReadOnly = true;
                        funkcio = "Modosit";
                        Alcsopinfo.Aktsorindex = aktualtablerowindex;
                    }
                    else if (funkcio == "Beszur")
                    {
                        Alcsopinfo.Aktsorindex = -1;
                        aktualtablerowindex = -1;
                    }
                    else
                    {
                        aktualkod = dataView1[0]["KOD"].ToString();
                        aktualgridrowindex = 0;
                        aktualtablerowindex = Alcsoptabla.Rows.IndexOf(dataView1[0].Row);
                        Alcsopinfo.Aktsorindex = aktualtablerowindex;
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                            dataGridView1.Rows[i].Selected = false;
                        dataGridView1.Rows[aktualgridrowindex].Selected = true;
                    }
                    if (dataView1.Count != 0)
                    {
                        buttonTöröl.Enabled = true;
                        buttonUj.Enabled = true;
                    }
                    else
                    {
                        textAzonosito.Text = foaktkod;
                        buttonTöröl.Enabled = false;
                        buttonUj.Enabled = false;
                    }
                    buttonMentes.Enabled = false;
                    egyallapot.Modositott=false;
                    egyallapot.Mentett = true;
                }
            }            
        }

        private void buttonVissza_Click(object sender, EventArgs e)
        {
            if (Focsoptabla.Rows.Count == 0)
                this.Visible = false;
            if (this.Visible)
            {
                buttonvisszavolt = true;
//                if(buttonMentes.Enabled)
//                Adattablak at = (Adattablak)Alcsopinfo.Initselinfo.Adattablak[Alcsopinfo.Initselinfo.Aktualadattablaindex];
                if (!buttonMentes.Enabled  ||
                    MessageBox.Show("A valtozasok elvesszenek?", "", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    Fak.ForceAdattolt(Alcsopinfo);
                    this.Visible = false;
                }
            }
        }

        private void Kodform_VisibleChanged(object sender, EventArgs e)
        {
            if(!this.Visible&&!buttonvisszavolt&&Focsoptabla.Rows.Count==0)
                 MessageBox.Show(groupBox1.Text + " nincs adat");
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeViewHitTestInfo hittest = treeView1.HitTest(e.X, e.Y);
            aktnode=hittest.Node;
            DataGridViewini(false);

        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                funkcio = "Modosit";
                textAzonosito.ReadOnly = true;
                aktualgridrowindex = e.RowIndex;
                aktualtablerowindex = Alcsoptabla.Rows.IndexOf(dataView1[aktualgridrowindex].Row);
                Alcsopinfo.Aktsorindex = aktualtablerowindex;
                buttonTöröl.Enabled = true;
                buttonUj.Enabled = true;
                Errortorol();
            }
        }

        private void buttonNyomtat_Click(object sender, EventArgs e)
        {
            nyomtat = new PrintForm();

            //eredmenyLista.SetDataSource(dS);

            nyomtat.reportSource = eredmenyLista;
            nyomtat.DoPreview(mainForm.defPageSettings);

        }
    }
}