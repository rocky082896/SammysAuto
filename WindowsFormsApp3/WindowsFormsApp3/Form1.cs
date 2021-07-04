using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            Image img = Image.FromFile(@"C:\Users\sps.roland.bitos\Desktop\Design competition\1.jfif");
            pictureBox1.Image = img;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            Image img = Image.FromFile(@"C:\Users\sps.roland.bitos\Desktop\Design competition\2.jpg");
            pictureBox1.Image = img;
        }
    }
}
