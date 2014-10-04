using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Text;


namespace Könyvtar.ClassGyujtemeny
{
    /// <summary>
    /// Zusammenfassung für ClassGyujtemeny.
    /// </summary>
    public class Gyujtemeny
    {
        private ToolStripButton toolStripButton;
        private string mdiChildSearchString;

        /// <summary>
        /// Szövegböl a jobboldal visszaadása a megadott hosszon
        /// </summary>
        /// <param name="s">a szöveg</param>
        /// <param name="l">hossz</param>
        /// <returns></returns>
        public string Right(string s, int l)
        {
            return s.Substring(s.Length - l, l);
        }

        /// <summary>
        /// Dátumtípus átalakítása String-é
        /// az eredmény formátum: ("YYYY.MM.DD HH:MM:SS.XXX"</param>
        /// </summary>
        /// <param name="d">A dátum amit át kell alakítani</param>
        /// <returns>Az átalakított string típusu dátum</returns>
        public string DateToString(DateTime d)
        {
            string s;
            string ms = "000" + d.Millisecond.ToString();
            s = d.ToString("yyyy.MM.dd HH:mm:ss") + "." + ms.Substring(ms.Length - 3);

            return s;
        }

        /// <summary>
        /// Dátumtípus átalakítása String-é
        ///az eredmény formátum: ("YYYY.MM.DD HH:MM:SS.XXX" vagy "YYYY.DD.MM HH:MM:SS.XXX")</param>
        /// </summary>
        /// <param name="d">A dátum amit át kell alakítani</param>
        /// <param name="f">Milyen formátumu legyen az erdeménystring.</param>
        /// <returns>Az átalakított string típusu dátum</returns>
        public string DateToString(DateTime d, string f)
        {
            string ev;
            string ho = "00";
            int lho;
            string nap = "00";
            int lnap;
            string ora = "00";
            int lora;
            string perc = "00";
            int lperc;
            string sec = "00";
            int lsec;
            string msec = "000";
            int lmsec;
            string s;

            ev = d.Year.ToString();
            ho = ho + d.Month.ToString();
            lho = ho.Length;
            ho = ho.Substring(lho - 2, 2);
            nap = nap + d.Day.ToString();
            lnap = nap.Length;
            nap = nap.Substring(lnap - 2, 2);
            ora = ora + d.Hour.ToString();
            lora = ora.Length;
            ora = ora.Substring(lora - 2, 2);
            perc = perc + d.Minute.ToString();
            lperc = perc.Length;
            perc = perc.Substring(lperc - 2, 2);
            sec = sec + d.Second.ToString();
            lsec = sec.Length;
            sec = sec.Substring(lsec - 2, 2);
            msec = msec + d.Millisecond.ToString();
            lmsec = msec.Length;
            msec = msec.Substring(lmsec - 3, 3);

            if (f == "YYYY.MM.DD HH:MM:SS.XXX")
            {
                s = ev + "." + ho + "." + nap + " " + ora + ":" + perc + ":" + sec + "." + msec;
            }
            else if (f == "YYYY.DD.MM HH:MM:SS.XXX")
            {
                s = ev + "." + nap + "." + ho + " " + ora + ":" + perc + ":" + sec + "." + msec;
            }
            else
            {
                s = ev + "." + ho + "." + nap;
            }
            return s;
        }

        /// <summary>
        /// Stringformátumú dátum átalakítása String-é.
        /// </summary>
        /// <param name="d"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public string DateStringToString(string d, string f)
        {
            string ev = "0000";
            string ho = "00";
            string nap = "00";
            string ora = "00";
            string perc = "00";
            string sec = "00";
            string msec = "000";
            string s;

            d = d.Replace(".", "");
            d = d.Replace(":", "");
            ev = d.Substring(0, 4);
            ho = d.Substring(4, 2);
            nap = d.Substring(6, 2);
            if (d.TrimEnd().Length > 8)
            {
                ora = d.Substring(9, 2);
                perc = d.Substring(11, 2);
                sec = d.Substring(13, 2);
                msec = d.Substring(15, 3);
            }

            switch (f)
            {
                case "YYYY.MM.DD HH:MM:SS.XXX":
                    s = ev + "." + ho + "." + nap + " " + ora + ":" + perc + ":" + sec + "." + msec;
                    break;
                case "YYYY.MM.DD HH:MM:SS":
                    s = ev + "." + ho + "." + nap + " " + ora + ":" + perc + ":" + sec;
                    break;
                case "YYYY.MM.DD":
                    s = ev + "." + ho + "." + nap;
                    break;
                case "YYYY.DD.MM HH:MM:SS.XXX":
                    s = ev + "." + nap + "." + ho + " " + ora + ":" + perc + ":" + sec + "." + msec;
                    break;
                case "YYYY.DD.MM":
                    s = ev + "." + nap + "." + ho;
                    break;
                default:
                    s = ev + "." + ho + "." + nap + " " + ora + ":" + perc + ":" + sec + "." + msec;
                    break;
            }
            return s;
        }

        /// <summary>
        /// Egy dátum érték benne van-e a kezdö és vég dátum között.
        /// </summary>
        /// <param name="n">A keresett dátum</param>
        /// <param name="k">Kezdö dátum</param>
        /// <param name="v">Vég dátum</param>
        /// <returns></returns>
        public bool DateBetween(DateTime n, DateTime k, DateTime v)
        {
            if ((n >= k) && (n <= v))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Egy String átalakítása Bool értékre.
        /// Lehetséges bementek I, 1, T -> ebböl true lesz.
        /// Különben false.
        /// </summary>
        /// <param name="par">Az átalakítandó string</param>
        /// <returns>Bool érték true vagy false</returns>
        public bool StringToBool(string par)
        {
            if (par == "I" || par == "1" || par == "T")
                return true;
            else
                return false;
        }

        /// <summary>
        /// Egy Logikai érték (Bool) átalakítása String-é.
        /// Ha true akkor a String paraméter kerül visszaadásra. Ennek értéke lehet I, 1, T.
        /// Abban az esetben, hat false akkor a következö táblázat alapján alakul a String.
        /// I -> N, 1 -> 0, egyébként F.
        /// </summary>
        /// <param name="par">Bool értek</param>
        /// <param name="c">Visszaadandó formátum</param>
        /// <returns></returns>
        public string BoolToString(bool par, string c)
        {
            if (par)
                return c;
            else
            {
                if (c == "I")
                    return "N";
                if (c == "1")
                    return "0";
                else
                    return "F";
            }
        }

        /// <summary>
        /// String keresése Stringben, azaz Str2 benne van-e Str1-ben
        /// </summary>
        /// <param name="str1">String amiben keresni kell</param>
        /// <param name="str2">A keresendö String</param>
        /// <returns>Benne van akkor true amugy false</returns>
        public bool StringInString(string str1, string str2) /// str2 benne van-e str1-ben
        {
            bool ret = false;

            if (str1 != null && str2 != null)
                for (int i = 0; i <= (str1.Length - str2.Length); i++)
                    if (str1.Substring(i, str2.Length).ToUpper() == str2.ToUpper())
                        ret = true;
                    else
                        ret = false;
            return ret;
        }

        /// <summary>
        /// Változott-e egy mezö értéke?
        /// Összehasonlításra kerülnek a Control-változói és a DataRow mezöi. Amennyiben
        /// a Control-változó neve = DataRow-mezö nevével megnézi, hogy értékük
        /// azonos-e. Kulönbözöség esetén visszatérés true (változott) más esetben false.
        /// Csak a kovetkezö typok veszi figyelembe: TextBox, ComboBox, DateTimePicker,
        /// CheckBox, RadioButton.
        /// </summary>
        /// <param name="con">Control panel</param>
        /// <param name="row">egy Datarow record</param>
        /// <returns></returns>
        public bool isFieldsChanged(Control.ControlCollection con, DataRow row)
        {
            bool ret = false;
            string vValue = String.Empty;

            for (int i = 0; i < con.Count; i++)
            {
                string type = con[i].GetType().Name;		    /// a változó tipusa
                string vName = con[i].Name.ToUpper();	        /// a változó neve
                if (row.Table.Columns.IndexOf(vName) > -1)
                    vValue = row[vName].ToString().Trim();     /// a változó értéke

                if (con[i].HasChildren)                         /// van-e gyerek Control -> akkor rekursiv hivás
                    ret = this.isFieldsChanged(con[i].Controls, row);
                else if (type == "TextBox" || type == "ComboBox" || type == "DateTimePicker")
                {
                    if (vValue != con[i].Text)
                        ret = true;
                }
                else if (type == "CheckBox" || type == "RadioButton")
                {
                    if (type == "CheckBox")
                    {
                        if (vValue != BoolToString(((CheckBox)con[i]).Checked, "I"))
                            ret = true;
                    }
                    else if (type == "RadioButton")
                    {
                        if (vValue != BoolToString(((RadioButton)con[i]).Checked, "I"))
                            ret = true;
                    }
                }
                if (ret)
                    break;
            }

            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="con"></param>
        /// <param name="row"></param>
        /// <param name="fName"></param>
        /// <returns></returns>
        public string isFieldChanged(Control.ControlCollection con, DataRow row, string fName)
        {
            string ret = String.Empty;

            for (int i = 0; i < con.Count; i++)
            {
                string type = con[i].GetType().Name;		    /// a változó tipusa
                string vName = con[i].Name.ToUpper();	        /// a változó neve
                string fValue = row[fName].ToString().Trim();   /// a mezö értéke

                if (con[i].HasChildren)                         /// van-e gyerek Control -> akkor rekursiv hivás
                    ret = this.isFieldChanged(con[i].Controls, row, fName);
                else if (vName == fName)
                {
                    if (type == "TextBox" || type == "ComboBox" || type == "DateTimePicker")
                    {
                        if (fValue != con[i].Text)
                            ret = con[i].Text;
                    }
                    else if (type == "CheckBox" || type == "RadioButton")
                    {
                        if (type == "CheckBox")
                        {
                            if (fValue != BoolToString(((CheckBox)con[i]).Checked, "I"))
                                ret = BoolToString(((CheckBox)con[i]).Checked, "I");
                        }
                        else if (type == "RadioButton")
                        {
                            if (fValue != BoolToString(((RadioButton)con[i]).Checked, "I"))
                                ret = BoolToString(((RadioButton)con[i]).Checked, "I");
                        }
                    }
                }
                if (ret != String.Empty)
                    break;
            }

            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="con"></param>
        /// <param name="fName"></param>
        /// <param name="ertek"></param>
        public void isFildExists(Control.ControlCollection con, string fName, string ertek)
        {
            for (int i = 0; i < con.Count; i++)
            {
                string type = con[i].GetType().Name;		/// a változó tipusa
                string vName = con[i].Name.ToUpper();	    /// a változó neve

                if (con[i].HasChildren)                     /// van-e gyerek Control -> akkor rekursiv hivás
                    this.isFildExists(con[i].Controls, fName, ertek);
                else if (type == "TextBox" || type == "ComboBox" || type == "DateTimePicker")
                {
                    if (vName == fName)                     /// egyenlö-e a Változó neve a Mezö nevével
                        con[i].Text = ertek.Trim();
                }
                else if (type == "CheckBox" || type == "RadioButton")
                    if (vName == fName)                     /// egyenlö-e a Változó neve a Mezö nevével
                        if (type == "CheckBox")
                            ((CheckBox)con[i]).Checked = StringToBool(ertek);
                        else if (type == "RadioButton")
                            ((RadioButton)con[i]).Checked = StringToBool(ertek);
            }
        }

        /// <summary>
        /// Létezik-e a 
        /// </summary>
        /// <param name="con"></param>
        /// <param name="row"></param>
        public void isVariabelExists(Control.ControlCollection con, DataRow row)
        {
            for (int i = 0; i < con.Count; i++)
            {
                string type = con[i].GetType().Name;		/// a változó tipusa
                string vName = con[i].Name.ToUpper();	    /// a változó neve

                if (con[i].HasChildren)                     /// van-e gyerek Control -> akkor rekursiv hivás
                    this.isVariabelExists(con[i].Controls, row);
                else if (row.Table.Columns.IndexOf(vName) > 0)
                    if (type == "TextBox" || type == "ComboBox" || type == "DateTimePicker")
                        con[i].Text = row[vName].ToString().Trim();
                    else if (type == "CheckBox" || type == "RadioButton")
                        if (type == "CheckBox")
                            ((CheckBox)con[i]).Checked = StringToBool(row[vName].ToString());
                        else if (type == "RadioButton")
                            ((RadioButton)con[i]).Checked = StringToBool(row[vName].ToString());
            }
        }

        /// <summary>
        /// adatbázisba kiirás ha a harmadik paraméter true, egyebként változóba ir táblából
        /// </summary>
        /// <param name="con"></param>
        /// <param name="row"></param>
        /// <param name="kiiras"></param>
        public void isVariabelExists(Control.ControlCollection con, DataRow row, bool kiiras)
        {
            for (int i = 0; i < con.Count; i++)
            {
                string type = con[i].GetType().Name;		/// a változó tipusa
                string vName = con[i].Name.ToUpper();	    /// a változó neve

                if (con[i].HasChildren)                     /// van-e gyerek Control -> akkor rekursiv hivás
                    this.isVariabelExists(con[i].Controls, row, kiiras);
                else if (row.Table.Columns.IndexOf(vName) > 0)
                    if (type == "TextBox" || type == "ComboBox" || type == "DateTimePicker")
                        row[vName] = con[i].Text;
                    else if (type == "CheckBox" || type == "RadioButton")
                        if (type == "CheckBox")
                            row[vName] = BoolToString(((CheckBox)con[i]).Checked, "I");
                        else if (type == "RadioButton")
                            row[vName] = BoolToString(((RadioButton)con[i]).Checked, "I");
            }
        }

        public void ToolStripButtonAdd(ToolStrip tS, string name, string text, Image image)
        {
            toolStripButton = new ToolStripButton();

            for (int i = 0; i < tS.Items.Count; i++)
                ((ToolStripButton)(tS.Items[i])).Checked = false;
            tS.Items.AddRange(new ToolStripItem[] { this.toolStripButton });
            this.toolStripButton.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripButton.Image = image;
            this.toolStripButton.ImageTransparentColor = System.Drawing.Color.White;
            this.toolStripButton.Name = name;
            this.toolStripButton.Size = new System.Drawing.Size(103, 22);
            this.toolStripButton.Text = text;
            this.toolStripButton.Checked = true;
        }

        public void ToolStripButtonAdd(ToolStrip tS, string name, string text)
        {
            toolStripButton = new ToolStripButton();

            for (int i = 0; i < tS.Items.Count; i++)
                ((ToolStripButton)(tS.Items[i])).Checked = false;
            tS.Items.AddRange(new ToolStripItem[] { this.toolStripButton });
            this.toolStripButton.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripButton.Name = name;
            this.toolStripButton.Size = new System.Drawing.Size(103, 22);
            this.toolStripButton.Text = text;
            this.toolStripButton.Checked = true;
        }

 //       public void ToolStripButtonUpd(ToolStrip tS, string name, string text)
 //       {
 //           tS.Items[name].Text = text;
 //       }

 //       public void ToolStripButtonRemove(ToolStrip tS, string name)
 //       {
 //           tS.Items.RemoveAt(tS.Items.IndexOfKey(name));
 //       }

 //       public void ToolStripButtonCheck(ToolStrip tS, string name)
  //      {
  //          for (int i = 0; i < tS.Items.Count; i++)
  //         {
  //              ((ToolStripButton)(tS.Items[i])).Checked = false;
  //              if (tS.Items[i].Name == name)
  //                  ((ToolStripButton)(tS.Items[i])).Checked = true;
  //          }
  //      }

        public int MdiChildIndex(Form[] mdi, string s)
        {
            mdiChildSearchString = s;
            return Array.FindIndex(mdi, MdiName);
        }

        private bool MdiName(Form f)
        {
            if (f.Name == mdiChildSearchString)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Visszaadja a kivánt paramétert a sxs_config.ini fileból
        /// </summary>
        /// <param name="s">A keresett paraméter azonosítója</param>
        /// <returns>A kivánt paraméter</returns>
        internal string StringFromConfig(string s)
        {
            string ret;
            int pozicio = -1;
            int pozCR = -1;
            int start = -1;
            int hossz = -1;
            string sourceFile = Directory.GetCurrentDirectory() + "\\sxs_config.ini";
            StreamReader sr = File.OpenText(sourceFile);
            string config = sr.ReadLine();
            sr.Close();

            s = s + " := ";

            pozicio = config.IndexOf(s);
            start = pozicio + s.Length;
            pozCR = config.IndexOf('\r', pozicio);
            hossz = pozCR - start;
            ret = config.Substring(start);

            return ret;
        }

        /// <summary>
        /// Visszaadja a kivánt paramétert a megadott textböl
        /// </summary>
        /// <param name="s">A keresett paraméter azonosítója</param>
        /// <param name="config">a szöveg amiben keresni kell az azonosítót</param>
        /// <returns>A kivánt paraméter</returns>
        internal string StringFromConfig(string s, string config)
        {
            string ret;
            int pozicio = -1;
            int pozCR = -1;
            int start = -1;
            int hossz = -1;

            s = s + " := ";

            pozicio = config.IndexOf(s);
            start = pozicio + s.Length;
            pozCR = config.IndexOf('\r', pozicio);
            hossz = pozCR - start;
            ret = config.Substring(start, hossz);

            return ret;
        }

        public string passwordCrypt(string pass)
        {
            char[] toCrypt = pass.ToCharArray();
            string ret = "";

            for (int i = 0; i < toCrypt.Length; i++)
            {
                toCrypt[i] = Convert.ToChar((toCrypt[i] + 13));
                ret += toCrypt[i].ToString();
            }

            return ret;
        }

        public string passwordUnCrypt(string pass)
        {
            char[] toCrypt = pass.ToCharArray();
            string ret = "";

            for (int i = 0; i < toCrypt.Length; i++)
            {
                toCrypt[i] = Convert.ToChar((toCrypt[i] - 13));
                ret += toCrypt[i].ToString();
            }

            return ret;
        }
    }
}
