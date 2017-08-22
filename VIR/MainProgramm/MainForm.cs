#define release

using System;
using System.IO;
using System.Drawing;
using System.Drawing.Printing;
using System.Collections;
using System.Globalization;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

using MainProgramm.SajatMenu;
using MainProgramm.Bejelentkezes;
using MainProgramm.Info;
using Könyvtar.ClassGyujtemeny;
using Törzsadatok;
using Adatbevitel;
using Lekerdezesek;

using TableInfo;
using Könyvtar.Printlib;
using SqlInterface;
using SqlServers;
//using Microsoft.SqlServer.Management.Smo;

namespace VIR
{
    public partial class MainForm : Form
    {
        private string jogosultsag = "1";
        private Gyujtemeny Gyujt = new Gyujtemeny();
        private Bejelentkezes bejelentkezes = new Bejelentkezes();
        private AboutBox   aboutBox = new AboutBox();

        private DataSet dataSet           = new DataSet();
        private DataTable tableUser_info  = new DataTable();
        private DataTable tableUser       = new DataTable();
        private SqlConnection myconn      = new SqlConnection();
        private SqlConnection mysqlconn   = new SqlConnection();

        private object[] menuelemek;
        private string[] menubutton;
//        private MyMenu   mM=new MyMenu();
        private MyMenuArray MyMenuArray;
        private MyMenu[] menuArr;
        private int menuElem;
 //       public  string   mdiChildSearchString;
        private Control aktualControl = null;
        private string   user;
        private string   pwd;
        private string programok = "";
        private string szint;

        //public  Server svr = new Server();

        private Fak fak;
        private MyTag tagbank;// = fak.GetKodtab("R", "Bank");
        private MyTag tagafa;// = fak.GetKodtab("C", "AFA");
        private MyTag tagpenztar;// = fak.GetKodtab("R", "Penztar");
        private MyTag tagtermekfo;// = fak.GetTablaTag("C", "TERMEKFOCSOPORT");
        private MyTag tagtermekal;// = fak.GetTablaTag("C", "TERMEKAL1");
        private MyTag tagtermek;// = fak.GetTablaTag("C", "TERMEK");
        private MyTag tagkoltsegfo;// = tagtermekfo;
        private MyTag tagkoltsegal; //= tagtermekal;
        private MyTag tagkoltseg;// = tagtermek;
        private MyTag tagpartner;
        private MyTag tagpartnertetel;
        private MyTag tagszamla;
        private MyTag tagszlatetel;
        private MyTag tagpenzmozg;// = fak.GetKodtab("R", "PENZMOZG");
        private MyTag tagelokalkulacio;
        private MyTag tagktgfelosztas;
        private MyTag tagcegek;

        //private string rendszerconn = "database=vir;server=localhost;uid=root;pwd=password;";
        private string rendszerconn = "Data Source=ANDRAS-NB;Initial Catalog=vir;Integrated Security=True;Pooling=false;";
        //private string cegconn = "Data Source=ANDRASPC;Initial Catalog=vir;Integrated Security=True;Pooling=false;";
        private string adatbazisfajta = "Sql";
        private DateTime modkezdetedatum;
        private bool elso = true;

        private MyTag[] aktualTag = null; 
        private bool useraktiv = false;
        private ListViewItem AktlistviewItem = null;
        private PrintForm nyomtat = new PrintForm();
        public  PageSettings defPageSettings = new PageSettings();

        public MainForm()
        {
            Screen primaryScreen = Screen.PrimaryScreen;
            if ((primaryScreen.Bounds.Size.Width == 0x400) && (primaryScreen.Bounds.Size.Height == 0x300))
            {
                base.WindowState = FormWindowState.Maximized;
            }
            this.InitializeComponent();
        }

        /// <summary>
        /// Amikor a MainForm betöltödik
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VIR_Load(object sender, EventArgs e)
        {
            if (this.ConfigFileRead())
            {
                this.createConnection();
            }
            else
            {
                base.Close();
            }
            this.bejelentkezes.Connection = this.myconn;
            if (this.bejelentkezes.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
            {
                base.Close();
            }
            else
            {
                this.user = this.bejelentkezes.User;
                this.pwd = this.bejelentkezes.Pwd;
                this.jogosultsag = this.bejelentkezes.Jogosultsag;
                this.programok = this.bejelentkezes.Programok;
                this.myconn.Close();
                this.myconn.ConnectionString = this.bejelentkezes.ConnectionValue;
                this.rendszerconn = this.myconn.ConnectionString;
                this.createConnection();
                this.adatEv.Text = this.adatEv.Text + this.bejelentkezes.ConnectionText;
                this.userLoad();
                this.menuBeallit();
                if (this.jogosultsag != "1")
                {
                    this.import.Enabled = false;
                }
            }
            this.myconn.Close();
        }

        #region Tulajdonságok (Get és Set)

        public SqlConnection MyConn
        {
            get { return this.myconn; }
            set { this.myconn = value; }
        }

        public SqlConnection MySqlConn
        {
            get { return this.mysqlconn; }
            set { this.mysqlconn = value; }
        }

        public string UserId
        {
            get { return this.user; }
            set { this.user = value; }
        }

        public string Password
        {
            get { return this.pwd; }
            set { this.pwd = value; }
        }

        #endregion

        #region Nyilvános (public) methodusok (függvények)
        /// <summary>
        /// A MainFormban található progressbar léptetése
        /// </summary>
        public void pBSkip()
        {
            this.progressBar.PerformStep();
        }

        /// <summary>
        /// A MainFormban található proggressbar uzenet megadása és Refresh()
        /// </summary>
        /// <param name="uz">A kiirandó üzenet</param>
        public void pbUzenet(string uz)
        {
            this.labelUzenetek.Text = uz;
            this.labelUzenetek.Refresh();
        }

        /// <summary>
        /// A MainFormban található proggressbar Maximumának és Lépésközeinek megadás
        /// </summary>
        /// <param name="max">Maximum érték</param>
        /// <param name="step">Lépésköz</param>
        public void pbParameters(int max, int step)
        {
            this.progressBar.Visible = true;
            this.progressBar.Maximum = max;
            this.progressBar.Step = step;
        }

        /// <summary>
        /// Ha a MainFormban található proggressbar lefutott akkor az uzenet kinullázása és Value = 0
        /// </summary>
        /// <param name="uz"></param>
        /// <param name="val"></param>
        public void pbLezaras(int val)
        {
            this.progressBar.Visible = false;
            this.pbUzenet(string.Empty);
            this.progressBar.Value = val;
        }
        #endregion

        #region Saját (private) methodusok (függvények)

        private bool ConfigFileRead()
        {
            try
            {
                string path = Directory.GetCurrentDirectory() + @"\sxs_config.ini";
                if (!(File.Exists(path) && (File.ReadAllText(path) != string.Empty)))
                {
                    this.myconn.ConnectionString = this.rendszerconn;
                    StreamWriter writer = File.CreateText(path);
                    writer.WriteLine("[SysConnString] := " + this.myconn.ConnectionString);
                    writer.Close();
                    return true;
                }
                this.myconn.ConnectionString = this.Gyujt.StringFromConfig("[SysConnString]");
                this.rendszerconn = this.myconn.ConnectionString;
                return true;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString(), "Hiba a path megadásánál.");
                return false;
            }
        }

        private void createConnection()
        {
            try
            {
                this.myconn.Open();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Adatbázis kapcsolódási hiba.\r\n Hibás Dolgozó/Jelszó.\r\n" + this.myconn.ConnectionString + "\r\n" + exception.Message);
                base.Close();
            }
        }

        private void userLoad()
        {
            this.tableUser_info = this.bejelentkezes.UserTable;
            if (this.tableUser_info.Rows.Count > 0)
            {
                this.labelUser.Text = this.tableUser_info.Rows[0]["dolgozo_nev"].ToString();
                this.labelPrg.Text = this.tableUser_info.Rows[0]["programok"].ToString();
                this.szint = this.tableUser_info.Rows[0]["szint"].ToString();
            }
        }

        private void menuBeallit()
        {
            this.modkezdetedatum = Convert.ToDateTime(DateTime.Today.ToShortDateString().Substring(0, 8) + ".01");
            this.fak = new Fak(this, this.rendszerconn, this.panel4, "Sql");
            this.fak.Cegadatok(this.rendszerconn, this.modkezdetedatum);
            this.fak.Formfaclose();
            this.panel1.Visible = false;
            this.MenuInicializalas();
            this.panel1.Visible = true;
        }

        /// <summary>
        /// A fömenü inicializálása.
        /// A billentyük megadása. A billentyükhöz tartozó almenük (TreeView) berendezése.
        /// Új menüpontot a SajatMenü programban a MyMenu osztályban (Class) kell felvenni.
        /// Utána minden müködik automatikusan.
        /// </summary>
        private void MenuInicializalas()
        {
            int num;
            this.tagbank = this.fak.GetKodtab("R", "Bank");
            this.tagpenztar = this.fak.GetKodtab("C", "PENZT");
            this.tagtermekfo = this.fak.GetKodtab("C", "Termfocsop");
            this.tagtermekal = this.fak.GetKodtab("C", "Termalcsop");
            this.tagtermek = this.fak.GetKodtab("C", "Term");
            this.tagkoltsegfo = this.fak.GetKodtab("C", "Koltfocsop");
            this.tagkoltsegal = this.fak.GetKodtab("C", "Koltalcsop");
            this.tagkoltseg = this.fak.GetKodtab("C", "Kolt");
            this.tagafa = this.fak.GetKodtab("C", "AFA");
            this.tagpartner = this.fak.GetTablaTag("C", "PARTNER");
            this.tagpartnertetel = this.fak.GetTablaTag("C", "PARTNER_FOLYOSZ");
            this.tagszamla = this.fak.GetTablaTag("C", "SZAMLA");
            this.tagszlatetel = this.fak.GetTablaTag("T", "SZAMLA_TETEL");
            this.tagpenzmozg = this.fak.GetTablaTag("C", "PENZMOZG");
            this.tagelokalkulacio = this.fak.GetTablaTag("C", "ELOKALKULACIO");
            this.tagktgfelosztas = this.fak.GetTablaTag("C", "KTGFELOSZTAS");
            this.tagcegek = this.fak.GetKodtab("C", "Ceg");
            Osszefinfo osszefinfo = new Osszefinfo();
            Tablainfo adatTablainfo = this.fak.GetKodtab("R", "Jog").AdatTablainfo;
            string str = "";
            for (num = 0; num < adatTablainfo.Adattabla.Rows.Count; num++)
            {
                DataRow row = adatTablainfo.Adattabla.Rows[num];
                if (row["KOD"].ToString() == this.jogosultsag)
                {
                    str = row["SORSZAM"].ToString();
                    break;
                }
            }
            Tablainfo tablainfo2 = this.fak.GetOsszef("R", "Fommjog").AdatTablainfo;
            osszefinfo.Osszefinfotolt(tablainfo2.Tablatag, this.fak);
            object[] idparamok = new object[] { new object[] { "", "" }, str };
            TreeView aktualosszef = osszefinfo.GetAktualosszef(idparamok);
            this.menubutton = new string[aktualosszef.Nodes.Count];
            this.menuElem = this.menubutton.Length;
            this.menuelemek = new object[this.menuElem];
            for (num = 0; num < this.menubutton.Length; num++)
            {
                this.menubutton[num] = aktualosszef.Nodes[num].Text;
                this.menuelemek[num] = new object[aktualosszef.Nodes[num].Nodes.Count];
                for (int i = 0; i < aktualosszef.Nodes[num].Nodes.Count; i++)
                {
                    TreeNode node = aktualosszef.Nodes[num].Nodes[i];
                    string text = node.Text;
                    switch (text)
                    {
                        case "Bevétel - Vevö":
                            ((object[])this.menuelemek[num])[i] = new object[] { text, new object[] { "Szamla", new MyTag[] { this.tagszamla, this.tagszlatetel }, "V", this.szint } };
                            break;

                        case "Kiadás - Szállitó":
                            ((object[])this.menuelemek[num])[i] = new object[] { text, new object[] { "Szamla", new MyTag[] { this.tagszamla, this.tagszlatetel }, "S", this.szint } };
                            break;

                        case "Pénzmozgás":
                            ((object[])this.menuelemek[num])[i] = new object[] { text, new object[] { "PenzMozgas", new MyTag[] { this.tagpenzmozg, this.tagpartner, this.tagpartnertetel, this.tagpenztar }, this.szint } };
                            break;

                        case "Kiegyenlítés - Vevö":
                            ((object[])this.menuelemek[num])[i] = new object[] { text, new object[] { "Kiegyenlites", new MyTag[] { this.tagszamla, this.tagszlatetel }, "V" } };
                            break;

                        case "Kiegyenlítés - Szállító":
                            ((object[])this.menuelemek[num])[i] = new object[] { text, new object[] { "Kiegyenlites", new MyTag[] { this.tagszamla, this.tagszlatetel }, "S" } };
                            break;

                        case "Bevételek":
                            ((object[])this.menuelemek[num])[i] = new object[] { text, new object[] { "Szamlak", new MyTag[] { this.tagtermekfo, this.tagtermekal, this.tagtermek }, "V" } };
                            break;

                        case "Kiadások":
                            ((object[])this.menuelemek[num])[i] = new object[] { text, new object[] { "Szamlak", new MyTag[] { this.tagkoltsegfo, this.tagkoltsegal, this.tagkoltseg }, "S" } };
                            break;

                        case "Bevétel-Kiadás":
                            ((object[])this.menuelemek[num])[i] = new object[] { text, new object[] { "BevetKiad", new MyTag[] { this.tagtermekfo, this.tagtermekal, this.tagtermek } } };
                            break;

                        case "Eredmény":
                            ((object[])this.menuelemek[num])[i] = new object[] { text, new object[] { "Eredmeny", new MyTag[] { this.tagktgfelosztas, this.tagtermek, this.tagkoltseg, this.tagtermekfo, this.tagtermekal, this.tagkoltsegfo, this.tagkoltsegal } } };
                            break;

                        case "Pénzforgalom":
                            ((object[])this.menuelemek[num])[i] = new object[] { text, new object[] { "Penzforgalom", new MyTag[] { this.tagszamla, this.tagszlatetel, this.tagpenzmozg, this.tagpartner, this.tagpartnertetel }, this.szint } };
                            break;

                        case "Áfalista":
                            ((object[])this.menuelemek[num])[i] = new object[] { text, new object[] { "AfaLista", new MyTag[] { this.tagszamla, this.tagszlatetel, this.tagpenzmozg, this.tagpartner, this.tagpartnertetel }, this.szint } };
                            break;

                        case "Bizonylatok":
                            ((object[])this.menuelemek[num])[i] = new object[] { text, new object[] { "Bizonylatok", new MyTag[] { this.tagszamla, this.tagszlatetel, this.tagpenzmozg, this.tagpartner, this.tagpartnertetel } } };
                            break;

                        case "Pénzintézetek":
                            ((object[])this.menuelemek[num])[i] = new object[] { text, new object[] { "Kodtablak", new MyTag[] { this.tagbank } } };
                            break;

                        case "Pénztárak":
                            ((object[])this.menuelemek[num])[i] = new object[] { text, new object[] { "Kodtablak", new MyTag[] { this.tagpenztar } } };
                            break;

                        case "Termékföcsoportok":
                            ((object[])this.menuelemek[num])[i] = new object[] { text, new object[] { "Kodtablak", new MyTag[] { this.tagtermekfo, this.tagtermekal } } };
                            break;

                        case "Termékalcsoportok":
                            ((object[])this.menuelemek[num])[i] = new object[] { text, new object[] { "Kodform", new MyTag[] { this.tagtermekfo, this.tagtermekal, this.tagtermek } } };
                            break;

                        case "Termékek":
                            ((object[])this.menuelemek[num])[i] = new object[] { text, new object[] { "Kodform", new MyTag[] { this.tagtermekal, this.tagtermek } } };
                            break;

                        case "Költségföcsoportok":
                            ((object[])this.menuelemek[num])[i] = new object[] { text, new object[] { "Kodtablak", new MyTag[] { this.tagkoltsegfo, this.tagkoltsegal } } };
                            break;

                        case "Költségalcsoportok":
                            ((object[])this.menuelemek[num])[i] = new object[] { text, new object[] { "Kodform", new MyTag[] { this.tagkoltsegfo, this.tagkoltsegal, this.tagkoltseg } } };
                            break;

                        case "Költségek":
                            ((object[])this.menuelemek[num])[i] = new object[] { text, new object[] { "Kodform", new MyTag[] { this.tagkoltsegal, this.tagkoltseg } } };
                            break;

                        case "Partnerek":
                            ((object[])this.menuelemek[num])[i] = new object[] { text, new object[] { "Partnerform", new MyTag[] { this.tagpartner, this.tagpartnertetel } } };
                            break;

                        case "ÁFA kulcsok":
                            ((object[])this.menuelemek[num])[i] = new object[] { text, new object[] { "Kodtablak", new MyTag[] { this.tagafa } } };
                            break;

                        case "Elökalkuláció":
                            ((object[])this.menuelemek[num])[i] = new object[] { text, new object[] { "Elokalkulacio", new MyTag[] { this.tagelokalkulacio, this.tagtermek, this.tagkoltsegfo, this.tagkoltsegal, this.tagkoltseg } } };
                            break;

                        case "Költségfelosztás":
                            ((object[])this.menuelemek[num])[i] = new object[] { text, new object[] { "KtgFelosztas", new MyTag[] { this.tagktgfelosztas, this.tagkoltsegal, this.tagkoltsegfo, this.tagkoltsegal, this.tagkoltseg } } };
                            break;

                        case "Dolgozók":
                            ((object[])this.menuelemek[num])[i] = new object[] { text, new object[] { "Dolgozo", new MyTag[] { this.tagcegek } } };
                            break;

                        case "Rendszer":
                            ((object[])this.menuelemek[num])[i] = new object[] { text, new object[] { "Rendszer", new MyTag[] { this.tagcegek } } };
                            break;
                    }
                }
            }
            this.MyMenuArray = new MainProgramm.SajatMenu.MyMenuArray(this.menubutton, this.menuelemek, this.panel1);
            this.menuArr = this.MyMenuArray.menuArr;
            for (num = 0; num < this.menuElem; num++)
            {
                MyMenu menu = this.menuArr[num];
                menu.list.Click += new EventHandler(this.listViewMenu_Click);
            }
        }

        private bool Userabortkerdes()
        {
            if (this.useraktiv)
            {
                int num;
                string str = "";
                for (num = 0; num < this.aktualTag.Length; num++)
                {
                    Egyallapotinfo egyallapotinfo = this.aktualTag[num].AdatTablainfo.GetEgyallapotinfo(this.aktualControl);
                    if (egyallapotinfo != null)
                    {
                        if (egyallapotinfo.Modositott)
                        {
                            str = str + this.aktualTag[num].Szoveg + "\n";
                        }
                    }
                    else
                    {
                        Adattablak adattablak = (Adattablak)this.aktualTag[num].AdatTablainfo.Initselinfo.Adattablak[0];
                        if (((adattablak.Added || adattablak.Deleted) || adattablak.Modified) || adattablak.Rowadded)
                        {
                            str = str + this.aktualTag[num].Szoveg + "\n";
                        }
                    }
                }
                if (str == "")
                {
                    this.panel6.Controls[0].VisibleChanged -= new EventHandler(this.usercontrol_VisibleChanged);
                    this.panel6.Controls.Clear();
                    return false;
                }
                for (num = 0; num < this.aktualTag.Length; num++)
                {
                    this.fak.ForceAdattolt(this.aktualTag[num].AdatTablainfo);
                    Egyallapotinfo egyallapotinfo2 = this.aktualTag[num].AdatTablainfo.GetEgyallapotinfo(this.aktualControl);
                    if (egyallapotinfo2 != null)
                    {
                        egyallapotinfo2.Modositott = false;
                    }
                }
                this.panel6.Controls[0].VisibleChanged -= new EventHandler(this.usercontrol_VisibleChanged);
                this.panel6.Controls.Clear();
                this.useraktiv = false;
                this.aktualTag = null;
                this.AktlistviewItem = null;
                return false;
            }
            return this.useraktiv;
        }

        #endregion

        #region Events
        /// <summary>
        ///egy Button megnyomása a "MENÜ"-ben kinyitja a hozzátartozo LISTVIEW-t
        /// és fel- vagy lecsuszik
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        /// <summary>
        /// Egy menüelem kiválasztása a MainForm-ban
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listViewMenu_Click(object sender, EventArgs e)
        {
            ListView view = (ListView)sender;
            MyMenu tag = (MyMenu)view.Tag;
            object[] objArray = (object[])view.FocusedItem.Tag;
            this.Text = "SIXIS-VIR [" + view.FocusedItem.Text + "]";
            if (((view.FocusedItem != this.AktlistviewItem) && (objArray.Length != 1)) && !this.Userabortkerdes())
            {
                string text = view.FocusedItem.Text;
                switch (objArray[0].ToString())
                {
                    case "Kodtablak":
                        this.aktualControl = new Kodtablak(text, objArray);
                        break;

                    case "Kodform":
                        this.aktualControl = new KodForm(text, objArray);
                        break;

                    case "Szamla":
                        this.aktualControl = new Szamla(text, objArray);
                        break;

                    case "PenzMozgas":
                        this.aktualControl = new PenzMozgas(text, objArray);
                        break;

                    case "Kiegyenlites":
                        this.aktualControl = new Kiegyenlites(text, objArray);
                        break;

                    case "Partnerform":
                        this.aktualControl = new Partnerform(text, objArray);
                        break;

                    case "KtgFelosztas":
                        this.aktualControl = new Ktgfelosztas(text, objArray);
                        break;

                    case "Szamlak":
                        this.aktualControl = new Szamlak(text, objArray);
                        break;

                    case "BevetKiad":
                        this.aktualControl = new BevetKiad(text, objArray);
                        break;

                    case "Eredmeny":
                        this.aktualControl = new Eredmeny(text, objArray);
                        break;

                    case "Penzforgalom":
                        this.aktualControl = new Penzforgalom(text, objArray);
                        break;

                    case "Bizonylatok":
                        this.aktualControl = new Bizonylatok(text, objArray);
                        break;

                    case "Dolgozo":
                        this.aktualControl = new Dolgozo(text, objArray);
                        break;

                    case "Rendszer":
                        this.aktualControl = new Rendszer(text, objArray);
                        break;

                    case "AfaLista":
                        this.aktualControl = new AfaLista(text, objArray);
                        break;
                }
                if (this.aktualControl != null)
                {
                    this.panel6.Visible = true;
                    this.panel6.Controls.Add(this.aktualControl);
                    this.aktualControl.Dock = DockStyle.Fill;
                    this.panel6.Controls[this.panel6.Controls.Count - 1].VisibleChanged += new EventHandler(this.usercontrol_VisibleChanged);
                    this.AktlistviewItem = view.FocusedItem;
                    this.aktualTag = (MyTag[])objArray[1];
                    this.useraktiv = true;
                }
            }
        }

        private void usercontrol_VisibleChanged(object sender, EventArgs e)
        {
            object obj2 = new object();
            lock (obj2)
            {
                Control control = (Control)sender;
                if (!control.Visible)
                {
                    if (!control.Enabled)
                    {
                        base.Close();
                    }
                    if (this.panel6.Controls.Count != 0)
                    {
                        this.panel6.Controls.Remove((Control)sender);
                    }
                    this.panel6.Visible = false;
                    this.AktlistviewItem = null;
                    this.aktualTag = null;
                    this.aktualControl = null;
                    this.useraktiv = false;
                }
            }
        }


        private void kilépésToolStripMenuItem_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aboutBox.ShowDialog();
        }

        private void nyomtatoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.nyomtat.pageSetupDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.defPageSettings = this.nyomtat.pageSetupDialog.PageSettings;
            }
        }

        private void export_Click(object sender, EventArgs e)
        {
            string str2 = Directory.GetCurrentDirectory() + @"\backup.sql";
            string str3 = DateTime.Now.ToShortDateString().Replace('.', '_') + DateTime.Now.ToShortTimeString().Replace(':', '_');
            string str4 = "vir_" + str3 + ".bak";
        }

        private void import_Click(object sender, EventArgs e)
        {
        }

        private void VIR_Activated(object sender, EventArgs e)
        {
            if (this.elso)
            {
                this.Refresh();
                this.elso = false;
            }
        }

        #endregion

    }
}