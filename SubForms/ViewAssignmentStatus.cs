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
    public partial class ViewAssignmentStatus : Form
    {
        static string myconn = ConfigurationManager.ConnectionStrings["StudentManagementDBConnectionString"].ConnectionString;

        public ViewAssignmentStatus()
        {
            InitializeComponent();
        }

        private void ViewAssignmentStatus_Load(object sender, EventArgs e)
        {
            string LoguserName = Form1.logUsername;


            SqlConnection conn = new SqlConnection(myconn);
            string sqlSlt = "SELECT studentId FROM tbl_student WHERE studentName=@LoguserName";
            SqlCommand cmd1 = new SqlCommand(sqlSlt, conn);
            cmd1.Parameters.AddWithValue("@LoguserName", LoguserName);
            conn.Open();
            int stdId = Convert.ToInt32(cmd1.ExecuteScalar());


            string sql = "SELECT assignmentName,grades FROM tbl_assignment WHERE studentId=@studentId";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@studentId", stdId);


            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
