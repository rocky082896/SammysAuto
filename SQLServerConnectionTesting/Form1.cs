using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SQLServerConnectionTesting
{
    public partial class Form1 : Form
    {
        private string constr = "Server=10.63.3.9;Database=rfid_db;User Id=rfidms_user;Password=Rockyjane28;";
        private string sql = "SELECT * FROM tbl_taghistory";
        private SqlConnection con;
        private SqlDataAdapter da;
        public Form1(){
            InitializeComponent();
            Init();
        }

        private void Init(){
            DataTable dt = new DataTable();
            using (con = new SqlConnection(constr))
            {
                con.Open();
                da = new SqlDataAdapter(sql, con);
                da.Fill(dt);
                con.Close();
            }
            dataGridView1.DataSource = dt.DefaultView;
        }
    }
}