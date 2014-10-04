using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using TableInfo;

namespace Adatbevitel
{
    public partial class Partnerform : UserControl
    {

        private Tablainfo Partnerinfo;
        private Tablainfo Partnertetelinfo;
        private DataTable PartnerAdattabla;
        private DataTable PartnertetelAdattabla;
        private int Partneraktsorindex = -1;
        private int Lastpartneraktgridindex=-1;
        private int Partneraktgridrowind=-1;
        private int Partneraktid = -1;
        private int Partnertetelaktsorindex = -1;
        private int Lasttetelaktgridindex=-1;
        private int Partnertetelaktgridrowind=-1;
        private int Partnertetelaktid = -1;
        private int Partneridcol = -1;
        private int Partnertetelidcol = -1;
        private Fak Fak;
        private MyTag partnertag;
        private MyTag partnerteteltag;
        private bool tetelmod = false;
        private bool igazivaltozas = false;
        private Egycontrolinfo egypartn;
        private Egycontrolinfo egytet;
        private string sajatid="";


        public Partnerform(string szoveg, object[] obj)
        {
            partnertag = ((MyTag[])obj[1])[0];
            Partnerinfo = partnertag.AdatTablainfo;
            partnerteteltag = ((MyTag[])obj[1])[1];
            Partnertetelinfo = partnerteteltag.AdatTablainfo;
            InitializeComponent();
            Fak = partnertag.Fak;
            Fak.ControlTagokTolt(this,groupBox2);
            try
            {
                Fak.ControlTagokTolt(this, groupBox4);
            }
            catch
            {
                MessageBox.Show("Nincs még pénzintézet megadva.");
            }
            egypartn = Partnerinfo.GetEgycontrolinfo(this);
            egytet = Partnertetelinfo.GetEgycontrolinfo(this);
            dataGVPartn.AutoGenerateColumns = false;
            DataGridViewColumn[] partnergridcols = Partnerinfo.GetGridColumns();
            dataGVPartn.Columns.AddRange(partnergridcols);
            dataGVPartntetel.AutoGenerateColumns = false;
            DataGridViewColumn[] partnertetelgridcols = Partnertetelinfo.GetGridColumns();
            dataGVPartntetel.Columns.AddRange(partnertetelgridcols);
            Partneridcol = Partnerinfo.Identitycol;
            PartnerAdattabla = ((Adattablak)Partnerinfo.Initselinfo.Adattablak[Partnerinfo.Initselinfo.Aktualadattablaindex]).Adattabla;
            if (PartnerAdattabla.Rows.Count != 0)
                Partneraktsorindex = 0;
            else
                Partneraktsorindex = -1;
            Partnertetelidcol = Partnertetelinfo.Identitycol;
            dataViewPartner.Table = PartnerAdattabla;
            dataViewPartner.Sort = Partnerinfo.Sort;
            dataGVPartn.DataSource = dataViewPartner;
            if (dataViewPartner.Count == 0)
                Partneraktgridrowind = -1;
            else
                Partneraktgridrowind = 0;
            PartnertetelAdattabla = ((Adattablak)Partnertetelinfo.Initselinfo.Adattablak[Partnertetelinfo.Initselinfo.Aktualadattablaindex]).Adattabla; ;
            dataViewTetel.Table = PartnertetelAdattabla;
            dataViewTetel.Sort = Partnertetelinfo.Sort;
            dataGVPartntetel.DataSource = dataViewTetel;
            Partner_beallit(Partneraktgridrowind);
        }

        private void Partner_beallit(int gridind)
        {
            igazivaltozas = false;
            for (int i = 0; i < dataViewPartner.Count; i++)
                dataGVPartn.Rows[i].Selected = false;
            Partneraktgridrowind = gridind;
            Lastpartneraktgridindex = gridind;
            if (Partneraktgridrowind != -1)
            {
                Partneraktsorindex=PartnerAdattabla.Rows.IndexOf(dataViewPartner[Partneraktgridrowind].Row);
                Partneraktid = Convert.ToInt32(PartnerAdattabla.Rows[Partneraktsorindex][Partneridcol].ToString());
                dataGVPartn.Rows[Partneraktgridrowind].Selected = true;
                DataRow dr = Partnerinfo.FindRow("SAJAT", "I");
                if (dr != null)
                    sajatid = dr["PID"].ToString();
                else
                    sajatid = "";
                buttonUj.Enabled = true;
                buttonTöröl.Enabled = true;
                buttonMentes.Enabled = false;
            }
            else
            {
 //               sajatid = "";
                if (PartnerAdattabla.Rows.Count == 0)
                    sajatid = "";
                Partneraktid = -1;
                Partneraktsorindex=-1;
                buttonUj.Enabled = false;
                buttonTöröl.Enabled = false;
                buttonMentes.Enabled = false;
            }
            Fak.ErrorProvider.SetError(button1, "");
            groupBox3.Enabled = false;
            groupBox4.Enabled = false;
            groupBox1.Enabled = true;
            if (Partneraktid!=-1)
                groupBox2.Enabled = false;
            else
                groupBox2.Enabled = true;
            Partnerinfo.Aktsorindex = Partneraktsorindex;
            dataViewTetel.RowFilter = "PID=" + Partneraktid.ToString() + " OR PID=0";
            if (dataViewTetel.Count == 0)
                Partnertetelaktgridrowind = -1;
            else
                Partnertetelaktgridrowind = 0;
            Tetel_beallit(Partnertetelaktgridrowind);
            igazivaltozas = true;
        }


        private void Partner_ok(object sender, EventArgs e)
        {
            igazivaltozas = false;
            string hiba = Fak.Hibavizsg(this,Partnerinfo, null);
            if (hiba == "")
            {
                if (sajatid != "")
                {
                    if (checkBox1.Checked)
                    {
                        if (Partneraktsorindex == -1 || PartnerAdattabla.Rows[Partneraktsorindex]["PID"].ToString() != sajatid)
                            hiba = "Van már Saját!";
                    }
                    else
                    {
                        if (Partneraktsorindex != -1 && PartnerAdattabla.Rows[Partneraktsorindex]["PID"].ToString() == sajatid)
                            sajatid = "";
                    }
                }
                Fak.ErrorProvider.SetError((Control)sender, hiba);
                if (hiba == "")
                {
                    buttonUj.Enabled = false;
                    buttonTöröl.Enabled = true;
                    groupBox1.Enabled = false;
                    groupBox2.Enabled = false;
                    groupBox3.Enabled = true;
                    groupBox4.Enabled = true;
                    if (Partneraktgridrowind == -1)
                    {
                        PartnerAdattabla = Partnerinfo.Ujsor();
                        Partneraktgridrowind = 0;
                        Partnertetelaktgridrowind = -1;
                    }
                    Partneraktsorindex = PartnerAdattabla.Rows.IndexOf(dataViewPartner[Partneraktgridrowind].Row);
                    DataRow dr = Partnerinfo.Adatsortolt(Partneraktsorindex);
                }
            }
            igazivaltozas = true;
        }

        private void Tetel_ok(object sender, EventArgs e)
        {
            string hiba = Fak.Hibavizsg(this,Partnertetelinfo, null);
            if (hiba == "")
            {
                hiba=Validalas(textBox4);
                if(hiba=="")
                {
                    igazivaltozas = false;
                    Lasttetelaktgridindex = Partnertetelaktgridrowind;
                    if (Partnertetelaktgridrowind == -1)
                    {
                        PartnertetelAdattabla = Partnertetelinfo.Ujsor();
                        Partnertetelaktsorindex = PartnertetelAdattabla.Rows.Count - 1;
                    }
                    DataRow dr = PartnertetelAdattabla.Rows[Partnertetelaktsorindex];
                    dr = Partnertetelinfo.Adatsortolt(Partnertetelaktsorindex);
                    if (Lasttetelaktgridindex == -1)
                    {
                        Partnertetelaktsorindex = -1;
                        Partnertetelinfo.Aktsorindex = -1;
                    }
                    buttonMentes.Enabled = true;
                    buttonTeteltorol.Enabled = true;
                    igazivaltozas = true;
                }
            }
        }

        private void Uj_Partner(object sender, EventArgs e)
        {
            Partner_beallit(-1);
        }

        private void Partner_torol(object sender, EventArgs e)
        {
            igazivaltozas = false;
            dataViewTetel.RowStateFilter=DataViewRowState.Added|DataViewRowState.Deleted|DataViewRowState.ModifiedCurrent|DataViewRowState.Unchanged;
            for (int i = 0; i < dataViewTetel.Count; i++)
            {
                DataRow dr = dataViewTetel[i].Row;
                PartnertetelAdattabla = Partnertetelinfo.Adatsortorol(PartnertetelAdattabla.Rows.IndexOf(dr));
            }
            dataViewTetel.RowStateFilter = DataViewRowState.Added | DataViewRowState.ModifiedCurrent | DataViewRowState.Unchanged;
            for (int i = 0; i < dataGVPartn.Rows.Count; i++)
            {
                if (dataGVPartn.Rows[i].Selected)
                {
                    Partneraktgridrowind = i;
                    dataGVPartn.Rows[i].Selected = false;
                    Partneraktsorindex = PartnerAdattabla.Rows.IndexOf(dataViewPartner[Partneraktgridrowind].Row);
                    break;
                }
            }
            PartnerAdattabla = Partnerinfo.Adatsortorol(Partneraktsorindex);
            if (dataViewPartner.Count == 0)
                Partner_beallit(-1);
            else
            {
                if (Partneraktgridrowind > dataViewPartner.Count - 1)
                    Partneraktgridrowind--;
                dataGVPartn.Rows[Partneraktgridrowind].Selected = true;
                Partneraktsorindex = PartnerAdattabla.Rows.IndexOf(dataViewPartner[Partneraktgridrowind].Row);
                Partner_beallit(Partneraktgridrowind);
            }
            buttonMentes.Enabled = true;
            igazivaltozas = true;
        }

        private void Mentes(object sender, EventArgs e)
        {
            ArrayList list = new ArrayList();
            list.Add(Partnerinfo);
            list.Add(Partnertetelinfo);
            igazivaltozas = false;
            if (!Fak.UpdateTransaction(list))
            {
                    this.Enabled = true;
                    this.Visible = false;
            }
            if (dataViewPartner.Count == 0)
                Partneraktgridrowind = -1;
            else
                Partneraktgridrowind = 0;
            Partner_beallit(Partneraktgridrowind);
            buttonMentes.Enabled = false;
            igazivaltozas = true;

        }

        private void Vissza(object sender, EventArgs e)
        {
            Fak.ForceAdattolt(Partnerinfo);
            Fak.ForceAdattolt(Partnertetelinfo);
            this.Visible = false;
            
        }
        private void Tetel_beallit(int gridindex)
        {
            igazivaltozas = false;
            Partnertetelaktgridrowind = gridindex;
            if (Partnertetelaktgridrowind != -1)
            {
                Partnertetelaktsorindex = PartnertetelAdattabla.Rows.IndexOf(dataViewTetel[Partnertetelaktgridrowind].Row);
                if (PartnertetelAdattabla.Rows[Partnertetelaktsorindex].RowState != DataRowState.Added)
                    Partnertetelaktid = Convert.ToInt32(PartnertetelAdattabla.Rows[Partnertetelaktsorindex][Partnertetelidcol].ToString());
                else
                    Partnertetelaktid = -1;
                buttonUjtetel.Enabled = true;
            }
            else
            {
                Partnertetelaktid = -1;
                Partnertetelaktsorindex = -1;
                buttonUjtetel.Enabled = false;
            }
            Lasttetelaktgridindex = gridindex;
            Partnertetelinfo.Aktsorindex = Partnertetelaktsorindex;
            if (dataViewTetel.Count == 0)
            {
                buttonTeteltorol.Enabled = false;
            }
            else
            {
                buttonTeteltorol.Enabled = true;
            }
            igazivaltozas = true;
        }
        private void Uj_tetel(object sender, EventArgs e)
        {
            Tetel_beallit(-1);
            groupBox1.Enabled = false;
            groupBox2.Enabled = false;
            buttonUj.Enabled = false;
            buttonTöröl.Enabled = false;
        }

        private void Tetel_torol(object sender, EventArgs e)
        {
            buttonUj.Enabled = false;
            buttonTöröl.Enabled = false;
            for(int i=0;i<dataViewTetel.Count;i++)
            {
                if (dataGVPartntetel.Rows[i].Selected)
                {
                    Partnertetelaktgridrowind = i;
                    break;
                }
            }
            Partnertetelaktsorindex = PartnertetelAdattabla.Rows.IndexOf(dataViewTetel[Partnertetelaktgridrowind].Row);
            PartnertetelAdattabla = Partnertetelinfo.Adatsortorol(Partnertetelaktsorindex);
            if (dataViewTetel.Count == 0)
                Tetel_beallit(-1);
            else
            {
                if (Partnertetelaktgridrowind > dataViewTetel.Count - 1)
                    Partnertetelaktgridrowind--;
                if (Partnertetelaktgridrowind != -1)
                    dataGVPartntetel.Rows[Partnertetelaktgridrowind].Selected = true;
                Tetel_beallit(Partnertetelaktgridrowind);
            }
            
        }

        private void Elem_Validated(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                Control cont = (Control)sender;
                if (cont.Tag != null && igazivaltozas)
                {
                    Taggyart tg = (Taggyart)((Control)sender).Tag;
                    string hibaszov = Fak.Hibavizsg(this,tg.Tabinfo, (Control)sender);
                    if (hibaszov == "" && tg.Valid != -1)
                        hibaszov = Validalas(cont);
                }
            }
        }
        private string Validalas(Control cont)
        {
            string hibaszov="";
            string tartal = cont.Text.Trim();
            Taggyart tg = (Taggyart)cont.Tag;
            switch (tg.Valid)
            {
                case 1:
                    for (int i = 0; i < PartnerAdattabla.Rows.Count; i++)
                    {
                        DataRow dr = PartnerAdattabla.Rows[i];
                        if (dr.RowState != DataRowState.Deleted)
                        {
                            if (dr[Partnerinfo.Sort].ToString().Trim() == tartal)
                            {
                                hibaszov = "Van mar ilyen azonosito";
                                break;
                            }
                        }
                    }
                    break;
                case 2:
                    if (cont == (Control)textBox4)
                    {
                        textBox4.RemoveFormatCharacters();
                        tartal = textBox4.Text.Trim();
                        if (tartal != "")
                        {
                            try
                            {
                                Convert.ToDecimal(tartal);
                            }
                            catch
                            {
                                hibaszov = "Folyoszamlaszam csak numerikus lehet!";
                            }
                            if (hibaszov == "")
                            {
                                if (tartal.Length != 8 && tartal.Length != 16)
                                    hibaszov = "Folyoszamlaszam csak 8 vagy 16 jegyu lehet!";
                            }
                        }
                    }

                    string bszla = "";
                    string fszla = textBox4.Text.Trim();
                    string pid = PartnerAdattabla.Rows[Partneraktsorindex][Partneridcol].ToString();
                    string azon = PartnerAdattabla.Rows[Partneraktsorindex]["AZONOSITO"].ToString();
                    if (comboBox3.Text != "" && textBox4.Text != "")
                    {
                        bszla = Partnertetelinfo.GetActualCombofileinfo(comboBox3);
                        DataRow dr = Partnertetelinfo.FindRow(new string[] {"BANKSZLA", "FOLYOSZLASZAM" }, new string[] {bszla, fszla }, Partnertetelaktsorindex);
                        if (dr != null)
                        {
                            pid = dr["PID"].ToString();
                            dr= Partnerinfo.FindRow("PID", pid);

                            hibaszov = "Ez a " + dr["AZONOSITO"].ToString() + " partner folyoszamlaszama!";
                        }
                    }
                    break;
            }
            Fak.ErrorProvider.SetError(cont, hibaszov);
            return hibaszov;
        }
        private void dataGVPartn_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
                Partner_beallit(e.RowIndex);
        }

        private void dataGVPartn_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                Partner_beallit(e.RowIndex);
                groupBox2.Enabled = true;
            }
        }

        private void dataGVPartntetel_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                Partnertetelaktgridrowind = e.RowIndex;
                Tetel_beallit(e.RowIndex);
            }
        }

        private void Elolrol(object sender, EventArgs e)
        {
            igazivaltozas = false;
            PartnerAdattabla = Fak.ForceAdattolt(Partnerinfo,true);
            PartnertetelAdattabla = Fak.ForceAdattolt(Partnertetelinfo,true);
            if (PartnerAdattabla.Rows.Count == 0)
                Partneraktgridrowind = -1;
            else
                Partneraktgridrowind = 0;
            Partner_beallit(Partneraktgridrowind);
            buttonMentes.Enabled = false;
            igazivaltozas = true;
        }

    }

}
