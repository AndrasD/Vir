using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Lekerdezesek
{
    public partial class AlSzamlak : Form
    {
        private DataTable tableSzamlak;

        public AlSzamlak(DataTable table, string kod)
        {
            tableSzamlak = table;
            this.Text = "Kód: " + kod;
            InitializeComponent();
        }

        private void AlSzamlak_Load(object sender, EventArgs e)
        {
            dataGV.DataSource = tableSzamlak;
        }
    }
}