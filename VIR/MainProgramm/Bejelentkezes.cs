using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Könyvtar.ClassGyujtemeny;
using MainProgramm.Properties;
using System.Data.SqlClient;
using System.IO;

namespace MainProgramm.Bejelentkezes    
{
    public partial class Bejelentkezes : Form
    {
        private DataTable conncetions = new DataTable();
        private SqlConnection connection;
        private SqlDataAdapter da;
        private DataSet dataSet = new DataSet();
        private Gyujtemeny gyujt = new Gyujtemeny();
        private string prg;
        private string szint;
        private DataTable tableUser_info = new DataTable();

        public Bejelentkezes()
        {
            InitializeComponent();
        }
        private void Bejelentkezes_Activated(object sender, EventArgs e)
        {
            this.textUser.Focus();
        }

        private void Bejelentkezes_Load(object sender, EventArgs e)
        {
            int index = -1;
            int startIndex = -1;
            string str = "";
            this.textUser.Focus();
            this.da = new SqlDataAdapter("select '' as year, '' as connstring from kod where id = 0", this.connection);
            this.da.Fill(this.conncetions);
            StreamReader reader = File.OpenText(Directory.GetCurrentDirectory() + @"\sxs_config.ini");
            string str3 = "[SysConnString] := ";
            while (!reader.EndOfStream)
            {
                str = reader.ReadLine();
                index = str.IndexOf(str3);
                startIndex = index + str3.Length;
                if (index > 0)
                {
                    DataRow row = this.conncetions.NewRow();
                    row["year"] = str.Substring(0, 4);
                    row["connstring"] = str.Substring(startIndex);
                    this.conncetions.Rows.Add(row);
                }
            }
            reader.Close();
            this.comboConn.DataSource = this.conncetions;
            this.comboConn.SelectedIndex = 0;
            if (this.conncetions.Rows.Count == 1)
            {
                this.comboConn.Visible = false;
                this.label3.Visible = false;
            }
        }

        private void rendben_Click(object sender, EventArgs e)
        {
            if (this.textUser.Text == string.Empty)
            {
                this.textUser.Focus();
                this.errorProvider.SetError(this.textUser, "Adja meg a dolgoz\x00f3 azonos\x00edt\x00f3j\x00e1t!");
            }
            else if (this.textUser.Text != string.Empty)
            {
                this.errorProvider.SetError(this.textUser, "");
                if (this.textPWD.Text == string.Empty)
                {
                    this.textPWD.Focus();
                    this.errorProvider.SetError(this.textPWD, "Adja meg a jelszav\x00e1t!");
                }
                else if (this.textPWD.Text != string.Empty)
                {
                    this.errorProvider.SetError(this.textPWD, "");
                    if ((this.textUser.Text == "Supervisor") && (this.gyujt.passwordUnCrypt(this.textPWD.Text) == "Supervisor1963"))
                    {
                        this.szint = "1";
                        base.DialogResult = DialogResult.OK;
                        base.Close();
                    }
                    else if (this.userLoad() && (this.tableUser_info.Rows[0]["programok"].ToString().Substring(0, 1) == "1"))
                    {
                        this.szint = this.tableUser_info.Rows[0]["szint"].ToString();
                        this.prg = this.tableUser_info.Rows[0]["programok"].ToString();
                        base.DialogResult = DialogResult.OK;
                        base.Close();
                    }
                    else
                    {
                        MessageBox.Show("Hib\x00e1s Dolgoz\x00f3/Jelsz\x00f3.\r\n");
                    }
                }
            }
        }

        private void textPWD_Leave(object sender, EventArgs e)
        {
        }

        private bool userLoad()
        {
            this.textPWD.Text = this.gyujt.passwordCrypt(this.textPWD.Text);
            this.da = new SqlDataAdapter("select * from dolgozo where azonosito = '" + this.textUser.Text + "' and jelszo = '" + this.textPWD.Text + "'", this.connection);
            this.da.Fill(this.dataSet, "tableUser_info");
            this.tableUser_info = this.dataSet.Tables["tableUser_info"];
            if (this.tableUser_info.Rows.Count == 0)
            {
                return false;
            }
            return true;
        }

        public SqlConnection Connection
        {
            set
            {
                this.connection = value;
            }
        }

        public string ConnectionText =>
            this.comboConn.Text;

        public string ConnectionValue =>
            this.comboConn.SelectedValue.ToString();

        public string Jogosultsag =>
            this.szint;

        public string Programok =>
            this.prg;

        public string Pwd =>
            this.textPWD.Text;

        public string User =>
            this.textUser.Text;

        public DataTable UserTable =>
            this.tableUser_info;

    }
}
