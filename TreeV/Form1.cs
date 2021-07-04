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

namespace TreeV
{
    public partial class Form1 : Form
    {
        private MySqlConnection con;
        private MySqlDataReader dr;
        private MySqlCommand cmd;

        private string constr = "SERVER=localhost; DATABASE=robinsons_db; USERNAME=root; PASSWORD=;";
        public Form1()
        {
            InitializeComponent();
            string sql = "SELECT * FROM tbl_palletitems";
            DataTable dt = new DataTable();
            using (con = new MySqlConnection(constr))
            {
                try
                {
                    con.Open();
                    cmd = new MySqlCommand(sql, con);
                    dr = cmd.ExecuteReader();
                    dr.Read();

                    foreach (DataRow row in topics.Rows)
                    {
                        TreeNode node = new TreeNode(dr["name"], dr["topicId"]);
                        node.PopulateOnDemand = true;
                        treeView1.Nodes.Add(node);
                    }

                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }

           
        }
    }
}
