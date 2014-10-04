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
    public partial class SemaMegtekint : Form
    {
        private DataView KtgView = new DataView();
        private DataTable KtgfelosztasAdattabla = new DataTable();
        private DataSet ds = new DataSet();
        private SqlDataAdapter da;

        public SemaMegtekint(Fak fak, string koltseg_id, string myconn)
        {
            InitializeComponent();

            da = new SqlDataAdapter("select k.szoveg as termek_id_k, szazalek " +
                                    "from ktgfelosztas a, kodtab k " +
                                    "where a.termek_id = k.sorszam and a.koltseg_id = " + koltseg_id, myconn);
            da.Fill(ds, "KtgfelosztasAdattabla");
            KtgfelosztasAdattabla = ds.Tables["KtgfelosztasAdattabla"];

            KtgView.Table = KtgfelosztasAdattabla;
            dataGV.DataSource = KtgView;
        }

        public void SemaMegtekintInit()
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }
    }
}