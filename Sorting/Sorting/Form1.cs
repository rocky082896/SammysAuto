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

namespace Sorting
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            string constring = "server=localhost; username=root; id=; database=ajidb;";
            string sql = "SELECT * FROM dummy";
            MySqlConnection con = new MySqlConnection(constring);
            MySqlDataAdapter da = new MySqlDataAdapter(sql, con);
            con.Open();
            da.Fill(dt);
            con.Close();
            
            dt.Columns.Add("sum");
            for (int i = 0; i < dt.Rows.Count - 1;  i++){
                dt.Rows[i][3] = (int.Parse(dt.Rows[i][1].ToString()) - int.Parse(dt.Rows[i + 1][1].ToString())).ToString();
            }
            dataGridView1.DataSource = dt.DefaultView;
        }
    }
}
