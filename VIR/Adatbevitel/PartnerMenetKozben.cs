using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TableInfo;

namespace Adatbevitel
{
    public partial class PartnerMenetKozben : Form
    {
        private DataSet dataSet = new DataSet();
        private Fak Fak;
        private Tablainfo partnerinfo;
        private Tablainfo Szamlainfo;
        private DataTable tablePartner = new DataTable();
        private DataView dataViewPartner = new DataView();
        private DataTable tableIrsz = new DataTable();
        private DataTable tableKoztj = new DataTable();
        private string hibaszov = "";
        private SqlConnection myconn = new SqlConnection();
        private ComboBox Combo;
        private string Text1;

        private int Partneraktgridrowind = -1;

        public PartnerMenetKozben(Tablainfo szamlainfo, Tablainfo info,ComboBox combo, Fak fak)
        {
            partnerinfo = info;
            Szamlainfo = szamlainfo;
            Combo = combo;
            Fak = fak;
            tablePartner = ((Adattablak)partnerinfo.Initselinfo.Adattablak[partnerinfo.Initselinfo.Aktualadattablaindex]).Adattabla;
            tablePartner.PrimaryKey = new DataColumn[] { tablePartner.Columns["azonosito"] };

            dataViewPartner.Table = tablePartner;
            dataViewPartner.Sort = partnerinfo.Sort;
            InitializeComponent();
            Fak.ControlTagokTolt(this, this);
            PartnerMenetKozbenInit();
        }

        public void PartnerMenetKozbenInit()
        {
            partnerinfo.Aktsorindex = -1;
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            hibaszov = Fak.Hibavizsg(this,partnerinfo,null);
            if (hibaszov == "")
            {
                Text1 = textBox1.Text;
                button1.Visible = true;
                button2.Visible = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ArrayList list = new ArrayList();
            partnerinfo.Ujsor();
            Partneraktgridrowind = 0;
            partnerinfo.Adatsortolt(tablePartner.Rows.IndexOf(dataViewPartner[Partneraktgridrowind].Row));
            list.Add(partnerinfo);
            if (!Fak.UpdateTransaction(list))
            {

            }
            else
            {
                string[] pidN = partnerinfo.FindIdentityArray(new string[] { "SAJAT" }, new string[] { "N" });
                Szamlainfo.Comboinfoszures(Combo, pidN);
                Combo.SelectedIndex= Combo.Items.IndexOf(Text1);
            }
        }
    }
}