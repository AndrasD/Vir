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
            Screen sc = Screen.PrimaryScreen;
            if (sc.Bounds.Size.Width == 1024 && sc.Bounds.Size.Height == 768)
            {
                this.WindowState = FormWindowState.Maximized;
            }

            InitializeComponent();
        }


        /// <summary>
        /// Amikor a MainForm betöltödik
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VIR_Load(object sender, EventArgs e)
        {
            if (ConfigFileRead())
            {
                createConnection();
            }
            else
                this.Close();
#if debug
            user = "Dallos András";
            pwd = "dallos";
            this.userLoad();
            this.menuBeallit();
#else
            bejelentkezes.Connection = this.myconn;
            if (bejelentkezes.ShowDialog() == DialogResult.Cancel)
                this.Close();
            else
            {
                user = bejelentkezes.User;
                pwd = bejelentkezes.Pwd;
                jogosultsag = bejelentkezes.Jogosultsag;

                myconn.Close();
                myconn.ConnectionString = bejelentkezes.ConnectionValue;
                rendszerconn = myconn.ConnectionString;
                createConnection();
                adatEv.Text = adatEv.Text + bejelentkezes.ConnectionText;

                this.userLoad();
                this.menuBeallit();
                if (jogosultsag != "1")
                    this.import.Enabled = false;
            }
#endif
            myconn.Close();

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
            //for (int i = 0; i < 100000000; i++) ;
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
            this.pbUzenet(String.Empty);
            this.progressBar.Value = val;
        }
        #endregion

        #region Saját (private) methodusok (függvények)

        private bool ConfigFileRead()
        {
            StreamWriter sw;
            bool ret = true;

            try
            {
                string sourceDir = Directory.GetCurrentDirectory();
                string sourceFile = sourceDir + "\\sxs_config.ini";

                if (!File.Exists(sourceFile) || File.ReadAllText(sourceFile) == String.Empty)
                {
                    myconn.ConnectionString = rendszerconn;
                    sw = File.CreateText(sourceFile);
                    sw.WriteLine("[SysConnString] := " + myconn.ConnectionString);
                    sw.Close();
                    ret = true;
                }
                else
                {
                    myconn.ConnectionString = Gyujt.StringFromConfig("[SysConnString]");
                    rendszerconn = myconn.ConnectionString;
                    ret = true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Hiba a path megállapításánál.");
                ret = false;
            }

            return ret;
        }

        private void createConnection()
        {
            try
            {
                myconn.Open();
            }
            catch (Exception err)
            {
                MessageBox.Show("Adatbázis kapcsolódási hiba.\r\n Hibás Dolgozó/Jelszó.\r\n"+myconn.ConnectionString+"\r\n"+ err.Message);
                this.Close();
            }
        }

        private void userLoad()
        {
            tableUser_info = bejelentkezes.UserTable;
            if (tableUser_info.Rows.Count > 0)
                this.labelUser.Text = tableUser_info.Rows[0]["dolgozo_nev"].ToString();
        }

        private void menuBeallit()
        {
            modkezdetedatum = Convert.ToDateTime(DateTime.Today.ToShortDateString().Substring(0, 8) + ".01");
            fak = new Fak(this, rendszerconn, panel4, "Sql");
            fak.Cegadatok(rendszerconn, modkezdetedatum);
            fak.Formfaclose();
            panel1.Visible = false;
            MenuInicializalas();
            panel1.Visible = true;
        }



        /// <summary>
        /// A fömenü inicializálása.
        /// A billentyük megadása. A billentyükhöz tartozó almenük (TreeView) berendezése.
        /// Új menüpontot a SajatMenü programban a MyMenu osztályban (Class) kell felvenni.
        /// Utána minden müködik automatikusan.
        /// </summary>
        private void MenuInicializalas()
        {
            tagbank = fak.GetKodtab("R", "Bank");
            tagpenztar = fak.GetKodtab("C", "PENZT");
            tagtermekfo = fak.GetKodtab("C", "Termfocsop");
            tagtermekal = fak.GetKodtab("C", "Termalcsop");
            tagtermek = fak.GetKodtab("C", "Term");
            tagkoltsegfo = fak.GetKodtab("C", "Koltfocsop");
            tagkoltsegal = fak.GetKodtab("C", "Koltalcsop");
            tagkoltseg = fak.GetKodtab("C", "Kolt");
            tagafa = fak.GetKodtab("C", "AFA");
            tagpartner = fak.GetTablaTag("C", "PARTNER");
            tagpartnertetel = fak.GetTablaTag("C", "PARTNER_FOLYOSZ");
            tagszamla = fak.GetTablaTag("C", "SZAMLA");
            tagszlatetel = fak.GetTablaTag("T", "SZAMLA_TETEL");
            tagpenzmozg = fak.GetTablaTag("C", "PENZMOZG");
            tagelokalkulacio = fak.GetTablaTag("C", "ELOKALKULACIO");
            tagktgfelosztas = fak.GetTablaTag("C", "KTGFELOSZTAS");
            tagcegek = fak.GetKodtab("C", "Ceg");

            Osszefinfo oszefinfo = new Osszefinfo();
            Tablainfo  jogosinfo = ((MyTag)fak.GetKodtab("R", "Jog")).AdatTablainfo;
            string jogid="";
            for (int i=0;i<jogosinfo.Adattabla.Rows.Count;i++)
            {
                DataRow dr=jogosinfo.Adattabla.Rows[i];
                if(dr["KOD"].ToString()==jogosultsag)
                {
                    jogid=dr["SORSZAM"].ToString();
                    break;
                }
            }
//            Tablainfo fomeninfo = ((MyTag)fak.GetKodtab("R", "Fömenü")).AdatTablainfo;
//            Tablainfo meninfo = ((MyTag)fak.GetKodtab("R", "Menü")).AdatTablainfo;
 //           Tablainfo fomminfo = ((MyTag)fak.GetOsszef("R", "Fomm")).AdatTablainfo;
            Tablainfo fommjoginfo = ((MyTag)fak.GetOsszef("R", "Fommjog")).AdatTablainfo;
            oszefinfo.Osszefinfotolt(fommjoginfo.Tablatag, fak);
            object[] ob1 = new object[] { new object[] { "", "" }, jogid };
//            oszefinfo.GetOsszef(ob1);
            TreeView tv = oszefinfo.GetAktualosszef(ob1);
            menubutton=new string[tv.Nodes.Count];
            menuElem = menubutton.Length;
            menuelemek = new object[menuElem];
            for (int i = 0; i < menubutton.Length; i++)
            {
                menubutton[i] = tv.Nodes[i].Text;
                menuelemek[i] = new object[tv.Nodes[i].Nodes.Count];
                for (int k = 0; k < tv.Nodes[i].Nodes.Count; k++)
                {
                    TreeNode tn=tv.Nodes[i].Nodes[k];
                    string menuszov=tn.Text;
                    switch (menuszov)
                    {
                        case "Bevétel - Vevö":
                            ((object[])menuelemek[i])[k] =new object[] { menuszov, new object[] { "Szamla", new MyTag[] { tagszamla, tagszlatetel }, "V" } };
                            break;
                        case "Kiadás - Szállitó":
                            ((object[])menuelemek[i])[k] =new object[] { menuszov, new object[] { "Szamla", new MyTag[] { tagszamla, tagszlatetel }, "S" } };
                            break;
                        case "Pénzmozgás":
                            ((object[])menuelemek[i])[k] = new object[] { menuszov, new object[] { "PenzMozgas", new MyTag[] { tagpenzmozg, tagpartner, tagpartnertetel, tagpenztar } } };
                            break;
                        case "Kiegyenlítés - Vevö":
                            ((object[])menuelemek[i])[k] = new object[] { menuszov, new object[] { "Kiegyenlites", new MyTag[] { tagszamla, tagszlatetel }, "V" } };
                            break;
                        case "Kiegyenlítés - Szállító":
                            ((object[])menuelemek[i])[k] = new object[] { menuszov, new object[] { "Kiegyenlites", new MyTag[] { tagszamla, tagszlatetel }, "S" } };
                            break;
                        case "Bevételek":
                            ((object[])menuelemek[i])[k] = new object[]{menuszov ,new object[]{"Szamlak",new MyTag[]{tagtermekfo,tagtermekal,tagtermek},"V"}};
                            break;
                        case "Kiadások":
                            ((object[])menuelemek[i])[k] = new object[]{menuszov,new object[]{"Szamlak",new MyTag[]{tagkoltsegfo,tagkoltsegal,tagkoltseg},"S"}};
                            break;
                        case "Bevétel-Kiadás":
                            ((object[])menuelemek[i])[k] = new object[] { menuszov, new object[] { "BevetKiad", new MyTag[] { tagtermekfo, tagtermekal, tagtermek }} };
                            break;
                         case "Eredmény":
                            ((object[])menuelemek[i])[k] = new object[]{menuszov,new object[]{"Eredmeny",new MyTag[]{tagktgfelosztas,tagtermek,tagkoltseg,tagtermekfo,tagtermekal,tagkoltsegfo,tagkoltsegal}}};
                            break;
                        //case "Termékenkénti kiadás":
                        //    ((object[])menuelemek[i])[k] = new object[] { menuszov, new object[] { "" } };
                        //    break;
                        //case "Termékenkénti eredmény":
                        //    ((object[])menuelemek[i])[k] = new object[] { menuszov, new object[] { "" } };
                        //    break;
                        case "Pénzforgalom":
                            ((object[])menuelemek[i])[k] = new object[] { menuszov, new object[] { "Penzforgalom", new MyTag[] { tagszamla, tagszlatetel, tagpenzmozg, tagpartner, tagpartnertetel } } };
                            break;
                        case "Áfalista":
                            ((object[])menuelemek[i])[k] = new object[] { menuszov, new object[] { "AfaLista", new MyTag[] { tagszamla, tagszlatetel, tagpenzmozg, tagpartner, tagpartnertetel } } };
                            break;
                        case "Bizonylatok":
                            ((object[])menuelemek[i])[k] = new object[] { menuszov, new object[] { "Bizonylatok", new MyTag[] { tagszamla, tagszlatetel, tagpenzmozg, tagpartner, tagpartnertetel } } };
                            break;
                        case "Pénzintézetek":
                            ((object[])menuelemek[i])[k] = new object[]{menuszov,new object[]{"Kodtablak",new MyTag[]{tagbank}}};
                            break;
                        case "Pénztárak":
                            ((object[])menuelemek[i])[k] = new object[]{menuszov,new object[]{"Kodtablak",new MyTag[]{tagpenztar}}};
                            break;
                        case "Termékföcsoportok":
                            ((object[])menuelemek[i])[k] = new object[]{menuszov,new object[]{"Kodtablak",new MyTag[]{tagtermekfo,tagtermekal}}};
                            break;
                        case "Termékalcsoportok":
                            ((object[])menuelemek[i])[k] = new object[]{menuszov,new object[]{"Kodform",new MyTag[]{tagtermekfo,tagtermekal,tagtermek}}};
                            break;
                        case "Termékek":
                            ((object[])menuelemek[i])[k] = new object[]{menuszov,new object[]{"Kodform",new MyTag[]{tagtermekal,tagtermek}}};
                            break;
                        case "Költségföcsoportok":
                            ((object[])menuelemek[i])[k] = new object[]{menuszov,new object[]{"Kodtablak",new MyTag[]{tagkoltsegfo,tagkoltsegal}}};
                            break;
                        case "Költségalcsoportok":
                            ((object[])menuelemek[i])[k] = new object[]{menuszov,new object[]{"Kodform",new MyTag[]{tagkoltsegfo,tagkoltsegal,tagkoltseg}}};
                            break;
                        case "Költségek":
                            ((object[])menuelemek[i])[k] = new object[]{menuszov,new object[]{"Kodform",new MyTag[]{tagkoltsegal,tagkoltseg}}};
                            break;
                        case "Partnerek":
                            ((object[])menuelemek[i])[k] = new object[]{menuszov,new object[]{"Partnerform",new MyTag[]{tagpartner,tagpartnertetel}}};
                            break;
                        case "ÁFA kulcsok":
                            ((object[])menuelemek[i])[k] = new object[]{menuszov,new object[]{"Kodtablak",new MyTag[]{tagafa}}};
                            break;
                        case "Elökalkuláció":
                            ((object[])menuelemek[i])[k] = new object[]{menuszov,new object[]{"Elokalkulacio", new MyTag[]{tagelokalkulacio,tagtermek,tagkoltsegfo,tagkoltsegal,tagkoltseg}}};
                            break;
                        case "Költségfelosztás":
                            ((object[])menuelemek[i])[k] = new object[] { menuszov, new object[] { "KtgFelosztas", new MyTag[] { tagktgfelosztas, tagkoltsegal, tagkoltsegfo, tagkoltsegal, tagkoltseg } } };
                            break;
                        case "Dolgozók":
                            ((object[])menuelemek[i])[k] = new object[] { menuszov, new object[] { "Dolgozo", new MyTag[] { tagcegek } } };
                            break;
                        case "Rendszer":
                            ((object[])menuelemek[i])[k] = new object[] { menuszov, new object[] { "Rendszer", new MyTag[] { tagcegek } } };
                            break;

                    }        
                }
            }
            MyMenuArray = new MyMenuArray(menubutton, menuelemek, panel1);
            menuArr = MyMenuArray.menuArr;
            for (int i = 0; i < menuElem; i++)
            {
                MyMenu egyelem = menuArr[i];
                egyelem.list.Click += listViewMenu_Click;
            }
        }

        private bool Userabortkerdes()
        {
            if (useraktiv)
            {
                string szoveg="";
                for(int i=0;i<aktualTag.Length;i++)
                {
                    Egyallapotinfo egyall = aktualTag[i].AdatTablainfo.GetEgyallapotinfo(aktualControl);
                    if (egyall != null )
                    {
                        if(egyall.Modositott)
                             szoveg += aktualTag[i].Szoveg + "\n";
//                    if (panel6.Controls[0].Name=="Szamla")
 //                   {
//                        if(((Szamla)panel6.Controls[0]).Modositott)
 //                           szoveg += aktualTag[i].Szoveg + "\n";
                    }
                    else
                    {
                        Adattablak egyadattabla = (Adattablak)aktualTag[i].AdatTablainfo.Initselinfo.Adattablak[0];
                        //Adattablak egyadattabla = (Adattablak)akttabinfo.Initselinfo.Adattablak[akttabinfo.Initselinfo.Aktualadattablaindex];
                        if (egyadattabla.Added || egyadattabla.Deleted || egyadattabla.Modified || egyadattabla.Rowadded)
                            szoveg += aktualTag[i].Szoveg + "\n";
                    }
                }
                if (szoveg == "")
                {
                    panel6.Controls[0].VisibleChanged -= usercontrol_VisibleChanged;
                    panel6.Controls.Clear();
                    return false;
                }
                else
                {
                    for (int i = 0; i < aktualTag.Length; i++)
                    {
                        fak.ForceAdattolt(aktualTag[i].AdatTablainfo);
                        Egyallapotinfo egy = aktualTag[i].AdatTablainfo.GetEgyallapotinfo(aktualControl);
                        if (egy != null)
                            egy.Modositott = false;
                    }
                    panel6.Controls[0].VisibleChanged -= usercontrol_VisibleChanged;
                    panel6.Controls.Clear();
                    //treeView1.ContextMenuStrip = contextMenuStrip1;
                    useraktiv = false;
                    aktualTag = null;
                    AktlistviewItem = null;
                    return false;
                }   
                //else if (MessageBox.Show(szoveg + " változásai elvesszenek?", "", MessageBoxButtons.OKCancel) == DialogResult.OK)
                //{
                //    for (int i = 0; i < aktualTag.Length; i++)
                //    {
                //        fak.ForceAdattolt(aktualTag[i].AdatTablainfo);
                //        Egyallapotinfo egy = aktualTag[i].AdatTablainfo.GetEgyallapotinfo(aktualControl);
                //        if (egy != null)
                //            egy.Modositott = false;
                //    }
                //    panel6.Controls[0].VisibleChanged -= usercontrol_VisibleChanged;
                //    panel6.Controls.Clear();
                //    //treeView1.ContextMenuStrip = contextMenuStrip1;
                //    useraktiv = false;
                //    aktualTag = null;
                //    AktlistviewItem = null;
                //    return false;
                //}
                //else
                //    return true;
            }
            return useraktiv;
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
        private void listViewMenu_Click(object sender, System.EventArgs e)
        {
            ListView aktview = (ListView)sender;
            MyMenu mymenu = (MyMenu)aktview.Tag;
            object[] param = (object[])aktview.FocusedItem.Tag;

            this.Text = "SIXIS-VIR [" + aktview.FocusedItem.Text + "]";

            if (aktview.FocusedItem != AktlistviewItem && param.Length != 1 && !Userabortkerdes() )
            {
                string nev = aktview.FocusedItem.Text;
                string contname = param[0].ToString();
                switch (contname)
                {
                    case "Kodtablak":
                        aktualControl = new Kodtablak(nev, param);
                        break;
                    case "Kodform":
                        aktualControl = new KodForm(nev, param);
                        break;
                    case "Szamla":
                        aktualControl = new Szamla(nev, param);
                        break;
                    case "PenzMozgas":
                        aktualControl = new PenzMozgas(nev,param);
                        break;
                    case "Kiegyenlites":
                        aktualControl = new Kiegyenlites(nev, param);
                        break;
                    case "Partnerform":
                        aktualControl = new Partnerform(nev, param);
                        break;
                    //case "Elokalkulacio":
                    //    cont = new Elokalkulacio(nev, param);
                    //    break;
                    case "KtgFelosztas":
                        aktualControl  = new Ktgfelosztas(nev, param);
                        break;
                    case "Szamlak":
                        aktualControl = new Szamlak(nev, param);
                        break;
                    case "BevetKiad":
                        aktualControl = new BevetKiad(nev, param);
                        break;
                    case "Eredmeny":
                        aktualControl = new Eredmeny(nev, param);
                        break;
                    case "Penzforgalom":
                        aktualControl = new Penzforgalom(nev, param);
                        break;
                    case "Bizonylatok":
                        aktualControl = new Bizonylatok(nev, param);
                        break;
                    case "Dolgozo":
                        aktualControl = new Dolgozo(nev, param);
                        break;
                    case "Rendszer":
                        aktualControl = new Rendszer(nev, param);
                        break;
                    case "AfaLista":
                        aktualControl = new AfaLista(nev, param);
                        break;
                }

                if (aktualControl != null)
                {
                    panel6.Visible = true;
                    //                   cont.Visible = true;
                    panel6.Controls.Add(aktualControl);
                    aktualControl.Dock = DockStyle.Fill;
                    //                    panel6.Controls[panel6.Controls.Count - 1].Visible = true;
                    panel6.Controls[panel6.Controls.Count - 1].VisibleChanged += new EventHandler(usercontrol_VisibleChanged);
                    AktlistviewItem = aktview.FocusedItem;
                    aktualTag = (MyTag[])param[1];
                    useraktiv = true;
                }
            }
        }

        private void usercontrol_VisibleChanged(object sender, EventArgs e)
        {
            object lockObject = new Object();
            lock (lockObject)
            {
                Control cont = (Control)sender;
                if (!cont.Visible)
                {
                    if (!cont.Enabled)
                        this.Close();
                    if(panel6.Controls.Count!=0)
                        panel6.Controls.Remove((Control)sender);
                    panel6.Visible = false;
                    AktlistviewItem = null;
                    aktualTag = null;
                    aktualControl = null;
                    useraktiv = false;
                }
            }
        }


        private void kilépésToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aboutBox.ShowDialog();
        }

        private void nyomtatoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (nyomtat.pageSetupDialog.ShowDialog() == DialogResult.OK)
                defPageSettings = nyomtat.pageSetupDialog.PageSettings;
        }

        private void export_Click(object sender, EventArgs e)
        {
            string sourceDir = Directory.GetCurrentDirectory();
            string sourceFile = sourceDir + "\\backup.sql";
            string dt = DateTime.Now.ToShortDateString().Replace('.', '_') + DateTime.Now.ToShortTimeString().Replace(':', '_');
            string backupFile = "vir_"+dt+".bak";
            try
            {
                //sw = File.CreateText(sourceDir + "\\vir_backup.bat");
                //sw.WriteLine("sqlcmd -i backup.sql");
                //sw.Close();

                if (File.Exists(sourceFile))
                {
                    File.Delete(sourceFile);
                }
                
                //sw = File.CreateText(sourceFile);
                //sw.WriteLine("USE vir\r\n"+
                //             "GO\r\n"+
                //             "BACKUP DATABASE vir TO DISK = '"+backupFile+"'\r\n" +
                //             "GO");
                //sw.Close();

                //System.Diagnostics.Process p = new System.Diagnostics.Process();
                //p.StartInfo.WorkingDirectory = sourceDir;
                //p.StartInfo.FileName = sourceDir + "\\vir_backup.bat";

                //Backup bkp = new Backup();
                //BackupRestoreBase brb = new BackupRestoreBase();

                //bkp.Action = BackupActionType.Database;
                //bkp.Database = "vir";
                //bkp.Devices.AddDevice(sourceDir+"\\"+backupFile,DeviceType.File);
                //try
                //{
                //    bkp.SqlBackup(svr);
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show(ex.ToString(), "\r\nHiba a kimentésnél.");
                //}

 
                //p.StartInfo.CreateNoWindow = false;
                //p.Start();
                //p.WaitForExit();
                //File.Move("C:\\Program Files\\Microsoft SQL Server\\MSSQL.1\\MSSQL\\Backup\\"+backupFile,
                //          sourceDir+"\\"+backupFile);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Hiba a path megállapításánál.");
            }
        }


        private void import_Click(object sender, EventArgs e)
        {
            string fileName = "";
            string sourceDir = Directory.GetCurrentDirectory();
            string sourceFile = sourceDir + "\\restore.sql";
            string dt = DateTime.Now.ToShortDateString().Replace('.', '_') + DateTime.Now.ToShortTimeString().Replace(':', '_');

            OpenFileDialog file = new OpenFileDialog();
            file.InitialDirectory = sourceDir;
            file.Filter = "backup files (*.bak)|vir_*.bak";
            if (file.ShowDialog() == DialogResult.OK)
            {
                fileName = file.FileName.Substring(file.FileName.LastIndexOf('\\') + 1);
                try
                {
                    //File.Move(sourceDir + "\\" + fileName,
                    //          "C:\\Program Files\\Microsoft SQL Server\\MSSQL.1\\MSSQL\\Backup\\" + fileName
                    //          );
                    //sw = File.CreateText(sourceDir + "\\vir_restore.bat");
                    //sw.WriteLine("sqlcmd -i restore.sql");
                    //sw.Close();

                    //if (File.Exists(sourceFile))
                    //{
                    //    File.Delete(sourceFile);
                    //}
                    //sw = File.CreateText(sourceFile);
                    //sw.WriteLine("USE master\r\n" +
                    //             "GO\r\n" +
                    //             "RESTORE DATABASE vir FROM DISK = '" + fileName + "'\r\n" +
                    //             "GO");
                    //sw.Close();

                    //System.Diagnostics.Process p = new System.Diagnostics.Process();
                    //p.StartInfo.WorkingDirectory = sourceDir;
                    //p.StartInfo.FileName = sourceDir + "\\vir_restore.bat";

                    //Restore res = new Restore();
                    //res.Database = "vir";
                    //res.Action = RestoreActionType.Database;
                    //res.Devices.AddDevice(sourceDir + "\\" + fileName, DeviceType.File);
                    //res.PercentCompleteNotification = 10;
                    //res.ReplaceDatabase = true;
                    //try
                    //{
                    //    res.SqlRestore(svr);
                    //}
                    //catch (Exception ex)
                    //{
                    //    MessageBox.Show(ex.ToString(), "\r\nHiba a visszatöltésnél.");
                    //}

                    //p.StartInfo.CreateNoWindow = false;
                    //// bezárni a az aktuális connecion-t mert különben nem megy a restore
                    //Sqlinterface sqlInterface = new Sqlinterface(rendszerconn, adatbazisfajta);
                    //sqlInterface.ConnClose(rendszerconn);
                    //p.Start();
                    //p.WaitForExit();
                    //sqlInterface.ConnOpen(rendszerconn);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Hiba a path megállapításánál.");
                }
            }
        }

        private void VIR_Activated(object sender, EventArgs e)
        {
            if (elso)
            {
                this.Refresh();
                elso = false;
            }
        }
        
        #endregion

    }
}