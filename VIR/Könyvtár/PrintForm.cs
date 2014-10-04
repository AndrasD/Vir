using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;

namespace Könyvtar.Printlib 
{
    public partial class PrintForm : Form
    {
        #region Változók

        int intern_StartPage = 0;          // Nyomtatandó kezdö oldal
        int intern_EndPage = 0;            // Nyomtatandó utolsó oldal
        int intern_CurrPage = 0;           // Aktuális oldal
        bool printInitialized = false;      // Nyomtatás inicializált?
        int[] startLines;                    // elsö sorok oldalanként
        string[] textToPrint = new string[] { "" }; // kinyomtatandó szöveg
        int pages = 0;                     // oldalszám
        Font printFont = new Font("Arial", 12f, FontStyle.Regular);     // text betütípusa
        Font headerFont = new Font("Arial", 12f, FontStyle.Bold);       // fejléc betütípusa
        int headLines = 2;                 // fejlécsorok száma
        bool includeLineNumbers = false;    // sorszám nyomtatás?
        bool printHeader = true;            // fejléc nyomtatás?
        string headerString = "SIXIS Software Kft";                       // fejléc szöveg
        bool antiAlias = false;             // Nyomtatás megtekintésénél minöségi?

        #endregion

        #region Tulajdonságok (Get és Set)

        /// <summary>
        /// Get vagy Set a nyomtatandó szöveg
        /// </summary>
        public string[] TextToPrint
        {
            get { return this.textToPrint; }
            set { this.textToPrint = value; }
        }
        /// <summary>
        /// Get vagy Set a szöveg betütípusa
        /// </summary>
        public Font PrintFont
        {
            get { return this.printFont; }
            set { this.printFont = value; }
        }

        /// <summary>
        /// Get vagy Set a fejléc betütípusa
        /// </summary>
        public Font HeaderFont
        {
            get { return this.headerFont; }
            set { this.headerFont = value; }
        }

        /// <summary>
        /// Get vagy Set a fejléc sorainak számát
        /// </summary>
        public int HeadLines
        {
            get { return this.headLines; }
            set { this.headLines = value; }
        }

        /// <summary>
        /// Get vagy Set. Logikai érték, hogy a sorszámot nyomtatni kell-e vagy sem
        /// </summary>
        public bool IncludeLineNumbers
        {
            get { return this.includeLineNumbers; }
            set { this.includeLineNumbers = value; }
        }

        /// <summary>
        /// Get vagy Set. Logikai érték, hogy a fejlécet nyomtatni kell-e vagy sem
        /// </summary>
        public bool PrintHeader
        {
            get { return this.printHeader; }
            set { this.printHeader = value; }
        }

        /// <summary>
        /// Get vagy Set a fejléc szövegét
        /// </summary>
        public string HeaderString
        {
            get { return this.headerString; }
            set { this.headerString = value; }
        }

        /// <summary>
        /// Get vagy Set. Nyomtatás megtekintésénél finom-rajzolat kell-e vagy sem
        /// </summary>
        public bool AntiAlias
        {
            get { return this.antiAlias; }
            set { this.antiAlias = value; }
        }

        /// <summary>
        /// Get vagy Set a printDocument értékét
        /// </summary>
        public PrintDocument pDoc
        {
            get { return this.printDocument; }
            set { this.printDocument = value; }
        }

        public object reportSource
        {
            get { return this.viewer.ReportSource; }
            set { this.viewer.ReportSource = value; }
        }

        #endregion

        #region Nyilvános függvények

        /// <summary>
        /// A nyomtatás (Method - printDocument) felhívása. 
        /// </summary>
        public void DoPrint()
        {
            viewer.PrintReport();
            //printDocument.Print();
        }

        /// <summary>
        /// A nyomtatás elönézet-dialogus felhivása
        /// </summary>
        /// <param name="text">A kinyomtatandó szöveg</param>
        /// <param name="pS">Az alapértelmezett oldalbeállítás</param>
        public void DoPreview(PageSettings pS)
        {
            pDoc.DefaultPageSettings = pS;

            viewer.ShowFirstPage();
            viewer.Zoom(100);

            this.ShowDialog();
        }

        /// <summary>
        /// A nyomtatás elönézet-dialogus felhivása
        /// </summary>
        /// <param name="text">A kinyomtatandó szöveg</param>
        /// <param name="pS">Az alapértelmezett oldalbeállítás</param>
        public void DoPreview(string[] text, PageSettings pS)
        {
            textToPrint = text;
            pDoc.DefaultPageSettings = pS;

            viewer.ShowFirstPage();
            viewer.Zoom(100);

            this.ShowDialog();
        }

        /// <summary>
        /// Nyomtatás.
        /// A nyomtatás párbeszédablak megjelenítésével.
        /// </summary>
        /// <param name="text">A kinyomtatandó szöveg</param>
        /// <param name="pS">Az alapértelmezett oldalbeállítás</param>
        public void DoPrintWithDialog(string[] text, PageSettings pS)
        {
            textToPrint = text;
            pDoc.DefaultPageSettings = pS;

            if (printDialog.ShowDialog() == DialogResult.OK)
                DoPrint();
        }

        /// <summary>
        /// CrystalReport-ban definiált paraméter(ek)hez az érték(ek) megadása.
        /// </summary>
        /// <param name="parName">A paraméter neve</param>
        /// <param name="parValue">Adott paraméterhez tartozó értek</param>
        /// <param name="parTyp">Adott paraméterhez tartozó értekek mivé kell konvertálni</param>
        /// <returns></returns>
        public ParameterFields PrintParams(string[] parName, string[] parValue, string[] parTyp)
        {
            ParameterFields paramFields = new ParameterFields(); ;
            ParameterField paramField;
            ParameterDiscreteValue value;

            for (int i = 0; i < parName.Length; i++)
            {
                paramFields = new ParameterFields();
                paramField = new ParameterField();
                value = new ParameterDiscreteValue();

                paramField.ParameterFieldName = parName[i];
                switch (parTyp[i])
                {
                    case "int":
                        value.Value = Convert.ToInt32(parValue[i]);
                        break;
                }
                paramField.CurrentValues.Add(value);
                paramFields.Add(paramField);
            }
            return paramFields;
        }

        #endregion

        #region Events

        /// <summary>
        /// Nyomtatást inicializál
        /// </summary>
        private void printDocument_BeginPrint(object sender, PrintEventArgs e)
        {
            this.printInitialized = false;
        }

        /// <summary>
        /// Befejezi a nyomtatást
        /// </summary>
        private void printDocument_EndPrint(object sender, PrintEventArgs e)
        {
            this.printInitialized = false;
        }

        private void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            int cpl = GetCharsPerLine(g, e.MarginBounds);

            // Nyomtat
            if (!this.printInitialized)
            {
                InitializePrint(g, e.MarginBounds, e.PageSettings.PrinterSettings);
                this.printInitialized = true;
            }

            if (this.printHeader)
                DoPrintHeader(g, e.MarginBounds, intern_CurrPage + 1);

            PrintOut(g, e.MarginBounds, this.startLines[this.intern_CurrPage], cpl, true);

            this.intern_CurrPage++;

            e.HasMorePages = (this.intern_CurrPage < this.intern_EndPage);
        }

        private void PageNo_TextChanged(object sender, EventArgs e)
        {
            if (PageNo.Text.Trim() == "1")
            {
                FirstPage_Button.Enabled = false;
                BackPage_Button.Enabled = false;
            }
            else
            {
                FirstPage_Button.Enabled = true;
                BackPage_Button.Enabled = true;
            }
        }

        /// <summary>
        /// Ugrás az elsö oldalra
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FirstPage_Button_Click(object sender, EventArgs e)
        {
            if (PageNo.Text.Trim() != "1")
            {
                viewer.ShowFirstPage();
                PageNo.Text = viewer.GetCurrentPageNumber().ToString();
            }
        }

        private void BackPage_Button_Click(object sender, EventArgs e)
        {
            if (PageNo.Text.Trim() != "1")
            {
                viewer.ShowPreviousPage();
                PageNo.Text = viewer.GetCurrentPageNumber().ToString();
            }
        }

        private void NextPage_Button_Click(object sender, EventArgs e)
        {
            viewer.ShowNextPage();
            PageNo.Text = viewer.GetCurrentPageNumber().ToString();
            if (PageNo.Text.Trim() != "1")
            {
                BackPage_Button.Enabled = true;
                FirstPage_Button.Enabled = true;
            }

        }

        private void LastPage_Button_Click(object sender, EventArgs e)
        {
            viewer.ShowLastPage();
            PageNo.Text = viewer.GetCurrentPageNumber().ToString();
            if (PageNo.Text.Trim() != "1")
            {
                BackPage_Button.Enabled = true;
                FirstPage_Button.Enabled = true;
            }
        }


        /// <summary>
        /// Nyomtatás nézetének automatikus beállítás
        /// </summary>
        private void btnZoomAuto_Click(object sender, System.EventArgs e)
        {
            viewer.Zoom(50);
        }

        /// <summary>
        /// Százalékos zoomolás
        /// </summary>
        private void comboBoxZoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxZoom.Text != "")
                viewer.Zoom(Convert.ToInt32(comboBoxZoom.Text.Replace(" %", "")));
        }

        /// <summary>
        /// Lista exportálása
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Export_Button_Click(object sender, EventArgs e)
        {
            viewer.ExportReport();
        }


        /// <summary>
        /// A dokumentum nyomtatása
        /// </summary>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            DoPrint();
            //if (this.printDialog.ShowDialog() == DialogResult.OK)
            //    this.DoPrint();
            //else
            //    DialogResult = DialogResult.None;
        }

        /// <summary>
        /// PrintForm bezárása
        /// </summary>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// A nyomtatás, a nyomtatandó terület beállítása
        /// </summary>
        private void btnSetup_Click(object sender, EventArgs e)
        {
            this.pageSetupDialog.PageSettings = pDoc.DefaultPageSettings;

            if (this.pageSetupDialog.ShowDialog() == DialogResult.OK)
            {
                pDoc.DefaultPageSettings = pageSetupDialog.PageSettings;
            }
            else
                DialogResult = DialogResult.None;
        }

        #endregion

        #region Segédfüggvények

        /// <summary>
        /// Kiszámolja hány oldal lesz a nyomtatás.
        /// Elementi minden oldal elsö nyomtatható sorát.
        /// </summary>
        /// <param name="g">A Graphics-Objekt a nyomtatáshoz</param>
        /// <param name="printRect">A Rectangle-Objekt , az a terület ahova nyomtatni lehet</param>
        private void CalculateStartLines(Graphics g, Rectangle printRect)
        {
            int charsPerLine = GetCharsPerLine(g, printRect);

            ArrayList arl = new ArrayList();
            int lineNumber = 0;

            while (lineNumber < this.textToPrint.Length)
            {
                arl.Add(lineNumber);
                lineNumber = PrintOut(g, printRect, lineNumber, charsPerLine, false);
            }

            this.startLines = (int[])arl.ToArray(typeof(System.Int32));
            this.pages = this.startLines.Length;
        }

        /// <summary>
        /// Megadja a fejléc méretét , amit a 
        /// nyomtatható területböl le kell vonni
        /// </summary>
        /// <param name="g">A Graphics-Objekt a nyomtatáshoz</param>
        /// <returns>Egy SizeF-Instanz a Fejléc méretével</returns>
        private SizeF GetHeadSize(Graphics g)
        {
            SizeF szf = g.MeasureString("W", this.headerFont);
            szf.Height *= 2;
            return szf;
        }

        /// <summary>
        /// Nyomtatás inicializálása, alapértékek megadása
        /// </summary>
        /// <param name="g">A Graphics-Objekt a nyomtatáshoz</param>
        /// <param name="printRect">A Rectangle-Objekt , az a terület ahova nyomtatni lehet</param>
        /// <param name="pr">A PrinterSettings-Objekt a nyomtatási terület értékeivel</param>
        private void InitializePrint(Graphics g, Rectangle printRect, PrinterSettings pS)
        {
            CalculateStartLines(g, printRect);

            if (pS.PrintRange.Equals(PrintRange.SomePages))
            {
                this.intern_StartPage = (pS.FromPage > pages) ? pages : pS.FromPage;
                this.intern_EndPage = (pS.ToPage > pages) ? pages : pS.ToPage;
            }
            else
            {
                this.intern_StartPage = 1;
                this.intern_EndPage = pages;
            }
            this.intern_CurrPage = intern_StartPage - 1;
        }

        /// <summary>
        /// Fejléc nyomtatása
        /// </summary>
        /// <param name="g">A Graphics-Objekt a nyomtatáshoz</param>
        /// <param name="printRect">A Rectangle-Objekt , az a terület ahova nyomtatni lehet</param>
        /// <param name="pnr">A fejlécben megjelenö oldalszám</param>
        private void DoPrintHeader(Graphics g, Rectangle printRect, int pnr)
        {
            StringFormat sf = new StringFormat(StringFormatFlags.NoWrap);
            RectangleF printRectF = (RectangleF)printRect;

            // Oldalszám nyomtatása
            string s = String.Format("Seite {0}/{1}", pnr, this.pages);
            SizeF szf = g.MeasureString(s, this.headerFont);
            g.DrawString(s, this.headerFont, Brushes.Black, printRectF);

            // headerString jobbraigazitani és ha kell röviditeni
            sf.Alignment = StringAlignment.Far;
            sf.Trimming = StringTrimming.EllipsisPath;

            RectangleF rf = new RectangleF(printRectF.X + (szf.Width * 1.2f), printRectF.Y, printRectF.Width - (szf.Width * 1.2f), szf.Height);

            // headerString kiirni
            g.DrawString(this.headerString, this.headerFont, Brushes.Black, rf, sf);

            // Vonal nyomtatás
            float y = printRectF.Y + szf.Height * 1.2f;
            g.DrawLine(Pens.Black, printRectF.X, y, printRectF.Right, y);

            sf.Dispose();
        }

        /// <summary>
        /// A tényleges nyomtatás.
        /// PrintOut két dolgogra jó. Egy: a megadott szöveg nyomtatása 
        /// Kettö: Kiszámolja , hányoldal lesz a nyomtatás és megadja oldalanként
        /// a kezdö sor számát. Ezért kell a doPrint=false paraméter.
        /// A PrintOut rutin oldalnként hivodik fel
        /// </summary>
        /// <param name="g">A Graphics-Objekt a nyomtatáshoz</param>
        /// <param name="printRect">A Rectangle-Objekt , az a terület ahova nyomtatni lehet</param>
        /// <param name="lineNumber">Az oldal Start-Sorszáma</param>
        /// <param name="charsPerLine">Maximális karakterszám soronként</param>
        /// <param name="doPrint">Nyomtatni kell vagy sem</param>
        /// <returns>A következö oldal kezdösorszáma</returns>
        private int PrintOut(Graphics g, Rectangle printRect, int lineNumber, int charsPerLine, bool doPrint)
        {
            RectangleF printRectF = (RectangleF)printRect;
            StringFormat sf = StringFormat.GenericTypographic;

            // Sorszám nyomtatási szélessége
            float lineNumberWidth = g.MeasureString("888888", this.printFont, 0, sf).Width;

            // Irási poziciok a számitáshoz
            float x1 = printRectF.X;
            float x2 = includeLineNumbers ? printRectF.X + lineNumberWidth : printRectF.X;
            float y = this.printHeader ? printRectF.Y + GetHeadSize(g).Height : printRectF.Y;

            // Karakter per Sor
            charsPerLine = includeLineNumbers ? charsPerLine - 6 : charsPerLine;

            // nyomtatandó szöveg
            string s = String.Empty;

            // nyomtatandó szöveg mérete + "W"
            string stringToMeasure = String.Empty;

            do
            {
                // Betördelni és a Tabokat eltávolítani
                s = ReplaceTabs(this.textToPrint[lineNumber]);
                s = DoWrap(s, charsPerLine);
                stringToMeasure = String.Concat("W", s);

                // Nyomtatandó szöveg magassága
                float currLineHeight = g.MeasureString(stringToMeasure, this.printFont).Height;

                if (y + currLineHeight > printRectF.Bottom)
                    break;

                // NYOMTAT ha doPrint = true
                if (doPrint)
                {
                    if (includeLineNumbers)
                    {
                        string lineNrTxt = (lineNumber + 1).ToString("0000");
                        g.DrawString(lineNrTxt, this.printFont, Brushes.Black, x1, y);
                    }
                    g.DrawString(s, this.printFont, Brushes.Black, x2, y, sf);
                }

                y += currLineHeight;
                lineNumber++;
            } while (lineNumber < this.textToPrint.Length);

            return lineNumber;
        }

        /// <summary>
        /// Kiszámolja a maximális karakterszámot soronként, bázis:
        /// az aktuális betütípus. Csak fix szélességü betütípusokra
        /// érvényes.
        /// </summary>
        /// <param name="g">A Graphics-Objekt a nyomtatáshoz</param>
        /// <param name="printRect">A Rectangle-Objekt , az a terület ahova nyomtatni lehet</param>
        /// <returns>A maximális karakterszám per sor</returns>
        private int GetCharsPerLine(Graphics g, Rectangle printRect)
        {
            RectangleF printRectF = (RectangleF)printRect;
            SizeF lineSize = g.MeasureString("W", this.printFont, 0, StringFormat.GenericTypographic);
            return (int)(Math.Floor(printRectF.Width / lineSize.Width));
        }

        /// <summary>
        /// Tabulátorok cseréje Spacere. Tabulátoronként 2 Space.
        /// </summary>
        /// <param name="txt">A Text, amiben a cserét vegre kell hajtani</param>
        /// <returns></returns>
        private string ReplaceTabs(string txt)
        {
            string tabString = "  ";
            return txt.Replace(((char)9).ToString(), tabString);
        }

        /// <summary>
        /// Egy túl hoszzu STRING feldarabolása
        /// </summary>
        /// <param name="txt">A String, amit fel kell darabolni</param>
        /// <param name="cols">A maximális karakterszám soronként</param>
        /// <returns>A feldarabolt String</returns>
        private string DoWrap(string txt, int cols)
        {
            // Hosszú vagy sem?
            if (txt.Length <= cols)
                return txt;

            // Init
            StringBuilder sb = new StringBuilder();
            int currPos = 0;

            do
            {
                if (txt.Length > cols)
                {
                    currPos = cols;
                    while ((txt[currPos] != ' ') && (txt[currPos] != '.') && (txt[currPos] != '-') && (txt[currPos] != '/'))
                    {
                        currPos--;
                        if (currPos == 0)
                        {
                            currPos = cols;
                            break;
                        }
                    }
                }
                else
                    currPos = txt.Length;

                if (sb.Length > 0)
                    sb.Append("\r\n" + txt.Substring(0, currPos));
                else
                    sb.Append(txt.Substring(0, currPos));

                txt = txt.Remove(0, currPos);
            } while (!txt.Equals(String.Empty));

            return sb.ToString();
        }

        #endregion

        #region Initialising / Konstruktor

        /// <summary>
        /// Class értékek inicializálása, 
        /// Az oldalhatárok megadása
        /// </summary>
        private void InitValues()
        {
            Margins m = this.pDoc.DefaultPageSettings.Margins;

            //m.Left = (int)Math.Ceiling(3.75 / 0.0254);
            //m.Right = (int)Math.Ceiling(3.75 / 0.0254);
            //m.Top = (int)Math.Ceiling(5.5 / 0.0254);
            //m.Bottom = (int)Math.Ceiling(5.0 / 0.0254);
        }

        /// <summary>
        /// A Konstruktor
        /// </summary>
        public PrintForm()
        {
            InitializeComponent();
            InitValues();
        }
        #endregion    
    }
}