using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SampleAutoCompleteWithDatabase
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            string s = "E0001";
            MessageBox.Show(s.Substring(0,0) + " " + s.Substring(1));
        }
    }
}
