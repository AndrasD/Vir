using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//using MySql.Data.MySqlClient;
using Könyvtar.ClassGyujtemeny;
using System.IO;

namespace MainProgramm.Bejelentkezes
{
    public partial class Bejelentkezes : Form
    {
        private Gyujtemeny gyujt = new Gyujtemeny();
        private SqlConnection connection;
        private DataSet dataSet = new DataSet();
        private DataTable tableUser_info = new DataTable();
        private SqlDataAdapter da;
        private DataTable conncetions = new DataTable();

        private string szint;

        public Bejelentkezes()
        {
            InitializeComponent();
        }

        #region Tulajdonságok (Get és Set)

        public SqlConnection Connection
        {
            set {this.connection = value;}
        }

        public string User
        {
            get { return this.textUser.Text; }
        }

        public string Pwd
        {
            get { return this.textPWD.Text; }
        }

        public DataTable UserTable
        {
            get { return this.tableUser_info; }
        }

        public string Jogosultsag
        {
            get { return this.szint; }
        }

        public string ConnectionValue
        {
            get { return this.comboConn.SelectedValue.ToString(); }
        }

        public string ConnectionText
        {
            get { return this.comboConn.Text; }
        }

        #endregion

        private void Bejelentkezes_Load(object sender, EventArgs e)
        {
            int pozicio = -1;
            int pozCR = -1;
            int start = -1;
            int hossz = -1;
            string line = "";

            this.textUser.Focus();
            da = new SqlDataAdapter("select '' as year, '' as connstring from kod where id = 0", connection);
            da.Fill(conncetions);
            DataRow r;

            string sourceFile = Directory.GetCurrentDirectory() + "\\sxs_config.ini";
            StreamReader sr = File.OpenText(sourceFile);

            string s = "[SysConnString] := ";

            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                pozicio = line.IndexOf(s);
                start = pozicio + s.Length;

                if (pozicio > 0)
                {
                    r = conncetions.NewRow();
                    r["year"] = line.Substring(0, 4);
                    r["connstring"] = line.Substring(start);
                    conncetions.Rows.Add(r);
                }
            }
            sr.Close();

            comboConn.DataSource = conncetions;
            comboConn.SelectedIndex = 0;

            if (conncetions.Rows.Count == 1)
            {
                comboConn.Visible = false;
                label3.Visible = false;
            }
        }

        private void rendben_Click(object sender, EventArgs e)
        {
            if (textUser.Text == String.Empty)
            {
                this.textUser.Focus();
                errorProvider.SetError(textUser, "Adja meg a dolgozó azonosítoját!");
            }
            else if (textUser.Text != String.Empty)
            {
                errorProvider.SetError(textUser, "");

                if (textPWD.Text == String.Empty)
                {
                    this.textPWD.Focus();
                    errorProvider.SetError(textPWD, "Adja meg a jelszavát!");
                }
                else if (textPWD.Text != String.Empty)
                {
                    errorProvider.SetError(textPWD, "");
                    if (textUser.Text == "Supervisor" && gyujt.passwordUnCrypt(textPWD.Text) == "Supervisor1963")
                    {
                        szint = "1";
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else if (userLoad())
                    {
                        szint = tableUser_info.Rows[0]["szint"].ToString();
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                        MessageBox.Show("Hibás Dolgozó/Jelszó.\r\n" );
                }
            }
        }

        private bool userLoad()
        {
            textPWD.Text = gyujt.passwordCrypt(textPWD.Text);

            da = new SqlDataAdapter("select * from dolgozo where azonosito = '" + textUser.Text + "' and jelszo = '"+textPWD.Text+"'" , connection);
            da.Fill(dataSet, "tableUser_info");
            tableUser_info = dataSet.Tables["tableUser_info"];

            if (tableUser_info.Rows.Count == 0)
                return false;
            else
                return true;
        }

        private void textPWD_Leave(object sender, EventArgs e)
        {
            //textPWD.Text = gyujt.passwordCrypt(textPWD.Text);
        }

        private void Bejelentkezes_Activated(object sender, EventArgs e)
        {
            textUser.Focus();
        }

    }
}