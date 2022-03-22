using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student_Management_System.SubForms
{
    public partial class YourInfo : Form
    {
        static string myconn = ConfigurationManager.ConnectionStrings["StudentManagementDBConnectionString"].ConnectionString;

        public YourInfo()
        {
            InitializeComponent();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void YourInfo_Load(object sender, EventArgs e)
        {
            string LoguserName = Form1.logUsername;

            SqlConnection conn = new SqlConnection(myconn);
            string sqlSlt = "SELECT * FROM tbl_student WHERE studentName=@LoguserName";
            SqlCommand cmd1 = new SqlCommand(sqlSlt, conn);
            cmd1.Parameters.AddWithValue("@LoguserName", LoguserName);

            conn.Open();
            SqlDataReader reader = cmd1.ExecuteReader();

            while(reader.Read())
            {
                txtStuName.Text = (reader["studentName"].ToString());
                string getDateN = (reader["DOB"].ToString());
                dtpDOB.Value = Convert.ToDateTime(getDateN);
                txtStuCont.Text = (reader["contactNO"].ToString());
                txtStream.Text = (reader["stream"].ToString());
                txtBatch.Text = (reader["batch"].ToString());
            }

            reader.Close();

        }
    }
}
