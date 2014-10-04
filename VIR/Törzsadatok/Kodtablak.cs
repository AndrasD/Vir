using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using TableInfo;

namespace Törzsadatok
{
    public partial class Kodtablak : UserControl
    {
        private Tablainfo Tabinfo;
        private DataTable Adattabla;
        private Fak Fak;
        private ArrayList TablaColumns;
 //       private ArrayList KiegColumns;
        private int azonositocol;
        private int sorrendcol = -1;
        private int szovegcol = -1;
        private int identitycol = -1;
        private int tablanevcol;
        private int kodtipuscol;
        private int azontipcol;
        private string aktkodlen="1";
        private string aktkod="";
        private int aktualtablerowindex;
        private int aktsorrend;
        private string aktazon;
        private int aktualgridrowindex;
        private string funkcio;
        private DateTime Aktdatumkezd;
        private DateTime Aktdatumveg;
        private DateTime lastversiondate;
        private Cols sorrendegycol = null;
        private int maxsorrend = 0;
        private int maxsorrrowind=-1;
        private MyTag Alcsoptag = null;
        private Tablainfo Alcsopinfo=null;
        private DataTable Alcsoptabla=null;
        private Egycontrolinfo egycont;
        private Egyallapotinfo egyallapot;

        public Kodtablak(string szoveg,object[] obj)
        {
            MyTag[] tagok=(MyTag[])obj[1];
            MyTag tag = tagok[0];
 //           string azon = obj[2].ToString();
            if (tagok.Length > 1)
            {
                Alcsoptag = tagok[1];
                Alcsopinfo=Alcsoptag.AdatTablainfo;
                Alcsoptabla=Alcsopinfo.Adattabla;
            }
            Kodtablakkezd(szoveg,tag);
        }

        public Kodtablak(string szoveg,MyTag tag)
        {
            Kodtablakkezd(szoveg,tag);
        }

        public void Kodtablakkezd(string szoveg,MyTag tag)
        {
            InitializeComponent();
            Fak = tag.Fak;
            Tabinfo = tag.AdatTablainfo;
            lastversiondate = Fak.LastversionDate;
            Aktdatumkezd = Tabinfo.Initselinfo.AktDatumkezd;
            Aktdatumveg = Tabinfo.Initselinfo.AktDatumveg;
            groupBox2.Text = szoveg;
            egyallapot = Tabinfo.CreateEgyallapotinfo(this);
            TablaColumns = Tabinfo.TablaColumns;
//            KiegColumns = Tabinfo.KiegColumns;
            if (Alcsoptag != null)
            {
                dataView2.Table = Alcsoptabla;
                dataView2.Sort = Alcsopinfo.Sort;
            }
            int egyci=Tabinfo.GetEgycontrolindex(this);
            if (egyci == -1)
                egycont = Tabinfo.CreateControlinfo(this);
            else
                egycont = Tabinfo.GetEgycontrolinfo(this);
            egycont.InputelemArray.Clear();
            for (int i = 0; i < this.Controls.Count; i++)
            {
                Control egycontrol = this.Controls[i];
                if (egycontrol.GetType().FullName == "System.Windows.Forms.TextBox")
                {
                    object otag = (object)egycontrol.Tag;
                    if (otag != null)
                    {
                        Taggyart tg = new Taggyart(Tabinfo, otag.ToString());
                        egycontrol.Tag = (object)tg;
                        tg.Control = egycontrol;
                        tg.Controltipus = "TextBox";
                        ((TextBox)egycontrol).MaxLength = tg.MaxLength;
                        egycont.InputelemArray.Add(tg);
                        if ((TextBox)egycontrol == textAzonosito)
                        {
                            if (tag.Kodtipus == "Bank"||tag.Kodtipus=="AFA")
                                tg.Valid = 1;
                        }
                    }
                }
            }
//            Tabinfo.InputelemInfok = new Taggyart[Tabinfo.InputelemArray.Count];
            egycont.Inputeleminfok = new Taggyart[egycont.InputelemArray.Count];
            for (int i = 0; i < egycont.InputelemArray.Count; i++)
                egycont.Inputeleminfok[i] = (Taggyart)egycont.InputelemArray[i];
            Tabinfo.Aktcontinfo = egycont;
            dataGridView1.AutoGenerateColumns = false;
            azonositocol = Tabinfo.Azonositocol;
            sorrendcol = Tabinfo.Sorrendcol;
            sorrendegycol = Tabinfo.SorrendColumn;
            if (sorrendegycol != null)
                sorrendcol = Tabinfo.Sorrendcolcol;
            identitycol = Tabinfo.Identitycol;
            if (identitycol == -1)
                identitycol = azonositocol;
            szovegcol = Tabinfo.Szovegcol;
            tablanevcol = Tabinfo.Tablanevcol;
            kodtipuscol = Tabinfo.Kodtipuscol;
            azontipcol = Tabinfo.Azontipcol;
            aktkodlen=Tabinfo.Kodhossz.ToString();
            DataGridViewColumn[] gridcols = Tabinfo.GetGridColumns();
            dataGridView1.Columns.AddRange(gridcols);
            Adattabla = ((Adattablak)Tabinfo.Initselinfo.Adattablak[Tabinfo.Initselinfo.Aktualadattablaindex]).Adattabla;
            dataView1.Table = Adattabla;
            dataView1.Sort = Tabinfo.Sort;
            dataGridView1.DataSource = dataView1;
            aktualgridrowindex = 0;
            if (dataView1.Count != 0)
            {
                aktkod=dataView1[0]["KOD"].ToString();
                aktazon = dataView1[0][identitycol].ToString().Trim();
                aktsorrend = Convert.ToInt32(dataView1[0][sorrendcol].ToString());
                aktualtablerowindex = Adattabla.Rows.IndexOf(dataView1[aktualgridrowindex].Row);
                Maxsorfind();
                funkcio="Modosit";
                textAzonosito.ReadOnly=true;
            }
            else
            {
                aktkod="";
                aktsorrend = 100;
                aktualtablerowindex = -1;
                funkcio="Beszur";
                textAzonosito.ReadOnly = false;
                buttonUj.Enabled = false;
                buttonTöröl.Enabled = false;
                buttonMentes.Enabled = false;
                egyallapot.Modositott = false;
            }
            Tabinfo.Aktsorindex = aktualtablerowindex;

        }

        private void Maxsorfind()
        {
            maxsorrend=0;
            maxsorrrowind=-1;
            for( int i=0;i<Adattabla.Rows.Count;i++)
            {
                DataRow dr=Adattabla.Rows[i];
                if(dr.RowState!=DataRowState.Deleted)
                {
                   if(Convert.ToInt32(dr[sorrendcol].ToString())>maxsorrend)
                   {
                       maxsorrend=Convert.ToInt32(dr[sorrendcol].ToString());
                       maxsorrrowind=i;
                   }
                }
            }
        }

        private void buttonUj_Click(object sender, EventArgs e)
        {
            if (funkcio != "Beszur")
            {
                funkcio = "Beszur";
                if (Adattabla.Rows.Count == 0)
                    aktsorrend = 100;
                else
                    aktsorrend = maxsorrend + 100;
                textAzonosito.ReadOnly = false;
                aktualtablerowindex = -1;
                Tabinfo.Aktsorindex = -1;
            }
            buttonUj.Enabled = false;
            buttonTöröl.Enabled = false;
 //           buttonMentes.Enabled = false;
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
            bool hiba=false;
            if (Alcsoptag != null)
            {
                dataView2.RowFilter = "substring(KOD,1," + aktkodlen + ")=" + aktkod;
                if (dataView2.Count != 0)
                {
                    hiba = true;
                    MessageBox.Show("Eloszor az alsobb szint(ek) torlendoek!");
                }
            }
            if (!hiba)
            {
                Errortorol();
//                if (Adattabla.Rows[aktualtablerowindex].RowState == DataRowState.Added)
 //                   Adattabla.Rows[aktualtablerowindex].Delete();
//                else
                    Tabinfo.Adatsortorol(Adattabla.Rows.IndexOf(dataView1[aktualgridrowindex].Row));
                //            if(aktualtablerowindex==maxsorrrowind)
                Maxsorfind();
                if (dataView1.Count == 0)
                {
                    funkcio = "Beszur";
                    if (Adattabla.Rows.Count == 0)
                        aktsorrend = 100;
                    else
                        aktsorrend = maxsorrend + 100;
                    textAzonosito.ReadOnly = false;
                    aktualtablerowindex = -1;
                    aktualgridrowindex = -1;
                    Tabinfo.Aktsorindex = -1;
                    buttonTöröl.Enabled = false;
                    buttonMentes.Enabled = true;
                    egyallapot.Modositott = true;
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
                    aktsorrend = Convert.ToInt32(dataView1[0][sorrendcol].ToString());
                    aktualtablerowindex = Adattabla.Rows.IndexOf(dataView1[aktualgridrowindex].Row);
                    funkcio = "Modosit";
                    Tabinfo.Aktsorindex = aktualtablerowindex;
                    textAzonosito.ReadOnly = true;
                    buttonMentes.Enabled = true;
                    buttonUj.Enabled = true;
                    egyallapot.Modositott = true;
                }
            }
        }
        private void buttonMentes_Click(object sender, EventArgs e)
        {
            string hibaszov = "";
            string aktualkod = "";
            if (buttonUj.Enabled)
            {
                hibaszov = Fak.Hibavizsg(this, Tabinfo, null);
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
                if (hibaszov == "")
                    aktualkod = textAzonosito.Text;
            }
            if (hibaszov == "")
            {

                //                   if (funkcio == "Beszur")
                //                       aktualkod = Alcsoptabla.Rows[Alcsoptabla.Rows.Count - 1]["KOD"].ToString();
                //                   else
                //                       aktualkod = Alcsoptabla.Rows[aktualtablerowindex]["KOD"].ToString();
                //               }

                ArrayList ar = new ArrayList();
                //                ar.Add(Focsopinfo);
                ar.Add(Tabinfo);
                Fak.UpdateTransaction(ar);
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
                    aktualtablerowindex = Adattabla.Rows.IndexOf(dataView1[aktualgridrowindex].Row);
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        dataGridView1.Rows[i].Selected = false;
                    dataGridView1.Rows[aktualgridrowindex].Selected = true;
                    textAzonosito.ReadOnly = true;
                    funkcio = "Modosit";
                    Tabinfo.Aktsorindex = aktualtablerowindex;
                }
                else if (funkcio == "Beszur")
                {
                    Tabinfo.Aktsorindex = -1;
                    aktualtablerowindex = -1;
                }
                else
                {
                    aktualkod = dataView1[0]["KOD"].ToString();
                    aktualgridrowindex = 0;
                    aktualtablerowindex = Adattabla.Rows.IndexOf(dataView1[0].Row);
                    Tabinfo.Aktsorindex = aktualtablerowindex;
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
                    //                       textAzonosito.Text = foaktkod;
                    buttonTöröl.Enabled = false;
                    buttonUj.Enabled = false;
                }
                buttonMentes.Enabled = false;
                egyallapot.Modositott = false;
                egyallapot.Mentett = true;
            }
        }
        

        private void buttonVissza_Click(object sender, EventArgs e)
        {
            if (!buttonMentes.Enabled ||
                MessageBox.Show("A valtozasok elvesszenek?", "", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                Fak.ForceAdattolt(Tabinfo);
                this.Visible = false;
            }
        }

        private void Control_Validated(object sender, EventArgs e)
        {
            Taggyart tg = (Taggyart)((Control)sender).Tag;
            string hibaszov = Fak.Hibavizsg(this, Tabinfo, (Control)sender);
            if (hibaszov == "")
            {
                hibaszov += EgyediValidalas((TextBox)sender);
                Fak.ErrorProvider.SetError((Control)sender, hibaszov);
            }
            Fak.ErrorProvider.SetError((Control)sender, hibaszov);
            if (hibaszov == "")
            {
                for (int i = 0; i < egycont.Inputeleminfok.Length; i++)
                {
                    TextBox egytext = (TextBox)((Taggyart)egycont.Inputeleminfok[i]).Control;
                    if (egytext != (TextBox)sender)
                    {
                        hibaszov += Fak.Hibavizsg(this, Tabinfo, egytext);
                        Fak.ErrorProvider.SetError(egytext, "");
                        if (hibaszov == "")
                        {
                            hibaszov += EgyediValidalas(egytext);
                            Fak.ErrorProvider.SetError(egytext, hibaszov);
                            //                               Fak.ErrorProvider.SetError(egytext, hibaszov);
                        }
                    }
                }
                if (hibaszov == "")
                {
                    buttonTöröl.Enabled = true;
                    if (funkcio == "Beszur")
                    {
                        Tabinfo.Ujsor();
                        aktualtablerowindex = Adattabla.Rows.Count - 1;
                        maxsorrend = aktsorrend;
                        maxsorrrowind = aktualtablerowindex;
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                            dataGridView1.Rows[i].Selected = false;
                    }
                    Tabinfo.Adatsortolt(aktualtablerowindex);
                    string aktualkod = Adattabla.Rows[aktualtablerowindex]["KOD"].ToString();
                    Adattabla.Rows[aktualtablerowindex][sorrendcol] = aktsorrend;
                    if (funkcio == "Beszur")
                    {
                        aktualgridrowindex = dataView1.Find((object)aktualkod);
                        dataGridView1.Rows[aktualgridrowindex].Selected = true;
                        aktsorrend = maxsorrend + 100;
                        textAzonosito.ReadOnly = false;
                        aktualtablerowindex = -1;
                        Tabinfo.Aktsorindex = -1;
                    }
                    else
                    {
                        buttonUj.Enabled = true;
                        buttonTöröl.Enabled = true;
                    }
                    buttonMentes.Enabled = true;
                    egyallapot.Modositott = true;
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
                        Cols egycol = (Cols)Tabinfo.TablaColumns[tg.egycolind];
                        if ((tb.Text.Trim().Length != tg.MaxLength)&&tg.Tabinfo.Tablatag.Kodtipus!="AFA")
                            hibaszov = egycol.Sorszov + " kotelezoen " + tg.MaxLength.ToString() + " jegyu!\n";
                        else
                        {
                            try
                            {
                                Convert.ToInt64(tb.Text);
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
         

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                funkcio = "Modosit";
                textAzonosito.ReadOnly = true;
                aktualgridrowindex = e.RowIndex;
                aktualtablerowindex = Adattabla.Rows.IndexOf(dataView1[aktualgridrowindex].Row);
                Tabinfo.Aktsorindex = aktualtablerowindex;
                aktkod=dataView1[e.RowIndex]["KOD"].ToString();
 //               buttonMentes.Enabled = false;
                buttonTöröl.Enabled = true;
                buttonUj.Enabled = true;
                Errortorol();
            }
        }

    }
}
