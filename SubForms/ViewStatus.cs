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
    public partial class ViewStatus : Form
    {
        static string myconn = ConfigurationManager.ConnectionStrings["StudentManagementDBConnectionString"].ConnectionString;

        public ViewStatus()
        {
            InitializeComponent();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ViewStatus_Load(object sender, EventArgs e)
        {
            ShowData();
        }

        public void ShowData()
        {
            SqlConnection conn = new SqlConnection(myconn);
            string sql = "SELECT assignmentName,Status FROM tbl_assignment";
            conn.Open();
            
            SqlCommand cmd = new SqlCommand(sql, conn);
            
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keywords = txtSearch.Text;

            if (keywords != null)
            {

                SqlConnection conn = new SqlConnection(myconn);
                string sql = "Select s.studentId,s.studentName,s.batch,s.stream from tbl_student s inner join tbl_assignment a on s.studentId = a.studentId Where s.studentName LIKE '%" + keywords + "%' OR s.batch LIKE '%" + keywords + "%' OR s.stream LIKE '%" + keywords + "%'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
               
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;

            }
            else
            {
                ShowData();
            }
        }

        
    }
}
