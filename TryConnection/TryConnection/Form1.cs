using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace TryConnection
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // string constring = @"Data Source=SPS-N-PF0T1NSN;Initial Catalog=productdb;Integrated Security=True;User ID=publicCon; Password=$p1010101010;";
            string constring = @"Data Source=10.63.3.17;Network Library=DBMSSOCN;Initial Catalog=productdb;User ID=publicCon;Password=$p1010101010;";
            string sql = "SELECT TOP 1 * FROM tbl_users ORDER BY user_id DESC";
            SqlConnection con = new SqlConnection(constring);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
                label1.Text = dr["user_id"].ToString();

            con.Close();
        }
    }
}
