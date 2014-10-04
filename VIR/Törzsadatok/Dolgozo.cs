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
    public partial class Dolgozo : UserControl
    {
        private VIR.MainForm mainForm;
        private Gyujtemeny gyujt = new Gyujtemeny();

        private DataSet dataSet = new DataSet();
        private DataTable tableDolgozo = new DataTable();
        private DataView viewDolgozo = new DataView();

        private SqlConnection myconn = new SqlConnection();

        private SqlDataAdapter da;
        private SqlCommandBuilder cb;

        private MyTag cegektag;
        private DataTable cegektabla;


        public Dolgozo(string szoveg, object[] obj)
        {
            MyTag[] myTag = (MyTag[])obj[1];

            cegektag = myTag[0];

            InitializeComponent();
        }

        private void Dolgozo_Load(object sender, EventArgs e)
        {
            mainForm = (VIR.MainForm)this.ParentForm;
            myconn = mainForm.MyConn;

            cegektabla = cegektag.AdatTablainfo.Adattabla;

            //for (int i = 0; i < cegektabla.Rows.Count; i++)
            //    ev.Items.Add(cegek.Rows[i]["ev"].ToString());

            this.dolgozoLoad();

            if (tableDolgozo.Rows.Count == 0)
            {
                this.buttonUj_Click(sender, e);
                textAzonosito.Focus();
            }


        }

        private void dolgozoLoad()
        {
            da = new SqlDataAdapter("SELECT * FROM dolgozo order by azonosito", myconn);
            da.Fill(dataSet, "tableDolgozo");
            tableDolgozo = dataSet.Tables["tableDolgozo"];
            tableDolgozo.PrimaryKey = new DataColumn[] { tableDolgozo.Columns["azonosito"] };

            this.viewDolgozo.BeginInit();
            this.viewDolgozo.Table = tableDolgozo;
            this.viewDolgozo.EndInit();

            dataGV.DataSource = this.viewDolgozo;

            cb = new SqlCommandBuilder(da);
        }

        private void buttonUj_Click(object sender, EventArgs e)
        {
            this.textAzonosito.ReadOnly = false;
            variabelClear(this.Controls);
            buttonUj.Enabled = false;
            buttonTorol.Enabled = false;
            buttonMentes.Enabled = false;
            textAzonosito.Focus();
        }

        private void variabelLoad(Control.ControlCollection con, DataRow[] row)
        {
            for (int i = 0; i < con.Count; i++)
            {
                if (con[i].GetType().Name == "GroupBox")
                    variabelLoad(con[i].Controls, row);
                if (con[i].Tag != null)
                    con[i].Text = row[0][(string)con[i].Tag].ToString();
            }
        }

        private void variabelClear(Control.ControlCollection con)
        {
            errorProvider.SetError(textMegnevezes, "");
            for (int i = 0; i < con.Count; i++)
            {
                if (con[i].GetType().Name == "GroupBox")
                    variabelClear(con[i].Controls);
                if (con[i].Tag != null)
                    con[i].Text = String.Empty;
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            //this.rendben();
            try
            {
                da.Update(tableDolgozo);
                tableDolgozo.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba az adatbázis aktualizálásánál\r\n" + ex.Message);
            }
        }

        private void rendben()
        {
            if (mezoValidalas("azonosito") && mezoValidalas("nev") && mezoValidalas("jelszo") && mezoValidalas("szint"))
            {
                if (!textAzonosito.ReadOnly)
                {
                    DataRow row = tableDolgozo.NewRow();
                    variabelSave(this.Controls, row);
                    tableDolgozo.Rows.Add(row);
                    variabelClear(this.Controls);
                }
                else
                {
                   DataRow findDolgozo = tableDolgozo.Rows.Find(dataGV.CurrentRow.Cells["azonosito"].Value);
                   variabelSave(this.Controls, findDolgozo);
                   buttonUj.Enabled = true;
                   buttonTorol.Enabled = true;
                   buttonMentes.Enabled = true;
               }
            }
        }

        private void variabelSave(Control.ControlCollection con, DataRow row)
        {
            for (int i = 0; i < con.Count; i++)
            {
                if (con[i].GetType().Name == "GroupBox")
                    variabelSave(con[i].Controls, row);
                if (con[i].Tag != null)
                    row[(string)con[i].Tag] = con[i].Text;
            }
        }

        private bool mezoValidalas(string mezo)
        {
            bool ret = false;

            if (mezo == "azonosito")
            {
                if ( textAzonosito.ReadOnly)
                    ret = true;
                else
                {
                    DataRow r =tableDolgozo.Rows.Find(textAzonosito.Text);
                    if (textAzonosito.Text == String.Empty)
                    {
                        textAzonosito.Focus();
                        errorProvider.SetError(textAzonosito, "Az azonsitót legyen szíves megadni.");
                        ret = false;
                    }
                    else if( r != null)
                    {
                        textAzonosito.Focus();
                        errorProvider.SetError(textAzonosito, "Már létezik!");
                        ret = false;
                    }
                    else
                    {
                        errorProvider.SetError(textAzonosito, "");
                        ret = true;
                    }
                }
            }

            if (mezo == "nev")
            {
                if (textMegnevezes.Text == String.Empty)
                {
                    textMegnevezes.Focus();
                    errorProvider.SetError(textMegnevezes, "A nevet legyen szíves megadni.");
                    ret = false;
                }
                else
                {
                    errorProvider.SetError(textMegnevezes, "");
                    ret = true;
                }
            }

            if (mezo == "jelszo")
            {
                if (textJelszo.Text == String.Empty)
                {
                    textJelszo.Focus();
                    errorProvider.SetError(textJelszo, "A jelszót legyen szíves megadni.");
                    ret = false;
                }
                else
                {
                    errorProvider.SetError(textJelszo, "");
                    ret = true;
                }
            }

            if (mezo == "szint")
            {
                if (comboSzint.Text == String.Empty)
                {
                    comboSzint.Focus();
                    errorProvider.SetError(comboSzint, "A szintet legyen szíves megadni.");
                    ret = false;
                }
                else if (Convert.ToInt32(comboSzint.Text) < 1 || Convert.ToInt32(comboSzint.Text) > 3)
                {
                    comboSzint.Focus();
                    errorProvider.SetError(comboSzint, "A szint csak 1 és 3 között lehet.");
                    ret = false;
                }
                else
                {
                    errorProvider.SetError(comboSzint, "");
                    ret = true;
                }
            }

            return ret;
        }

        private void textAzonosito_Validating(object sender, CancelEventArgs e)
        {
            mezoValidalas("azonsoito");
        }

        private void textMegnevezes_Validating(object sender, CancelEventArgs e)
        {
            mezoValidalas("nev");
        }

        private void textJelszo_Validating(object sender, CancelEventArgs e)
        {
            if (mezoValidalas("jelszo"))
                textJelszo.Text = gyujt.passwordCrypt(textJelszo.Text);
        }

        private void dataGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                textAzonosito.ReadOnly = true;
                DataRow[] findDolgozo = tableDolgozo.Select("azonosito = '"+dataGV.Rows[e.RowIndex].Cells["azonosito"].Value+"'");
                variabelLoad(this.Controls, findDolgozo);
                buttonUj.Enabled = true;
                buttonTorol.Enabled = true;
                buttonMentes.Enabled = true;
                errorProvider.SetError(textAzonosito, "");
                errorProvider.SetError(textMegnevezes, "");
            }
        }

        private void buttonTorol_Click(object sender, EventArgs e)
        {
            variabelClear(this.Controls);
            if (dataGV.SelectedRows.Count > -1)
            {
                DataRow[] row = tableDolgozo.Select("dolgozo_nev ='" + dataGV.CurrentRow.Cells["dolgozo_nev"].Value + "'");
                row[0].Delete();
            }
        }

        private void comboSzint_Leave(object sender, EventArgs e)
        {
            if (comboSzint.Text == String.Empty)
                comboSzint.Text = "1";
            this.rendben();
        }

        private void comboSzint_Validating(object sender, CancelEventArgs e)
        {
            mezoValidalas("szint");
        }
     }
}
