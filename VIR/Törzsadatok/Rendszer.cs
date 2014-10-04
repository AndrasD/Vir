using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
//using MySql.Data.MySqlClient;
//using MySql.Data.Types;
using System.Data.SqlClient;
using Könyvtar.ClassGyujtemeny;
using TableInfo;

namespace Törzsadatok
{
    public partial class Rendszer : UserControl
    {
        private VIR.MainForm mainForm;
        private Gyujtemeny gyujt = new Gyujtemeny();

        private DataSet dataSet = new DataSet();
        private DataTable tableRendszer = new DataTable();

        private SqlConnection myconn = new SqlConnection();

        private SqlDataAdapter da;
        private SqlCommandBuilder cb;

        public Rendszer(string szoveg, object[] obj)
        {
            InitializeComponent();
        }

        private void Rendszer_Load(object sender, EventArgs e)
        {
            mainForm = (VIR.MainForm)this.ParentForm;
            myconn = mainForm.MyConn;

            this.rendszerLoad();
        }

        private void rendszerLoad()
        {
            da = new SqlDataAdapter("SELECT * FROM rendszer", myconn);
            da.Fill(dataSet, "tableRendszer");
            tableRendszer = dataSet.Tables["tableRendszer"];

            cb = new SqlCommandBuilder(da);

            if (tableRendszer.Rows.Count > 0)
            {
                eredmeny_szures.Text = tableRendszer.Rows[0]["eredmeny_szures"].ToString();
                vegzo_sorszam.Text = tableRendszer.Rows[0]["vegzo_sorszam"].ToString();
            }

        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (mezoValidalas("eredmeny_szures"))
            {
                if (tableRendszer.Rows.Count == 0)
                {
                    DataRow row = tableRendszer.NewRow();
                    row["eredmeny_szures"] = eredmeny_szures.Text;
                    row["vegzo_sorszam"] = vegzo_sorszam.Text;
                    tableRendszer.Rows.Add(row);
                }
                else
                {
                    tableRendszer.Rows[0]["eredmeny_szures"] = eredmeny_szures.Text;
                    tableRendszer.Rows[0]["vegzo_sorszam"] = vegzo_sorszam.Text;
                }

                try
                {
                    da.Update(tableRendszer);
                    tableRendszer.AcceptChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hiba az adatbázis aktualizálásánál\r\n" + ex.Message);
                }
            }
        }

        private bool mezoValidalas(string mezo)
        {
            bool ret = false;

            if (mezo == "eredmeny_szures")
            {
                if (eredmeny_szures.Text == String.Empty)
                {
                    eredmeny_szures.Focus();
                    errorProvider.SetError(eredmeny_szures, "Adja meg legyen szíves a szűrést.");
                    ret = false;
                }
                else
                {
                    errorProvider.SetError(eredmeny_szures, "");
                    ret = true;
                }
            }

            return ret;
        }
     }
}
