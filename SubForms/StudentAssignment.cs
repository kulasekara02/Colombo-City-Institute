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
    public partial class StudentAssignment : Form
    {
        static string myconn = ConfigurationManager.ConnectionStrings["StudentManagementDBConnectionString"].ConnectionString;
        
        public StudentAssignment()
        {
            InitializeComponent();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void StudentAssignment_Load(object sender, EventArgs e)
        {
            showBatch();
        }
        public void showBatch()
        {
            SqlConnection conn = new SqlConnection(myconn);
            conn.Open();
            string sql = "SELECT batchName FROM tbl_batch";
            SqlCommand cmd = new SqlCommand(sql, conn);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            cmbBatch.DisplayMember = "batchName";
            cmbBatch.ValueMember = "batchName";
            cmbBatch.DataSource = dt;

            cmbBatch.Enabled = true;

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtAssignName.Text == string.Empty)
            {
                MessageBox.Show("Please enter assignment name.", "ERROR !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (txtStudentName.Text == string.Empty)
            {
                MessageBox.Show("Please enter student name.", "Error !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (cmbBatch.SelectedIndex < 0)
            {
                MessageBox.Show("Please select batch.", "Error !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (cmbStatus.SelectedIndex < 0)
            {
                MessageBox.Show("Please select status.", "Error !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (txtAssignName.TextLength != 0 && txtStudentName.TextLength != 0)
            {
                string batch = cmbBatch.Text;
                string status = cmbStatus.Text;


                SqlConnection conn = new SqlConnection(myconn);
                string sqlSlt = "SELECT studentId FROM tbl_student WHERE studentName=@studentName";
                SqlCommand cmd1 = new SqlCommand(sqlSlt, conn);
                cmd1.Parameters.AddWithValue("@studentName", txtStudentName.Text);
                conn.Open();
                int stdId = Convert.ToInt32(cmd1.ExecuteScalar());

                string sql = "INSERT INTO tbl_assignment (assignmentName,studentId,Status,batch) VALUES (@assignmentName,@studentId,@Status,@batch) ";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@assignmentName", txtAssignName.Text);
                cmd.Parameters.AddWithValue("@studentId", stdId);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@batch", batch);


                //conn.Open();
                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    MessageBox.Show("Assignment Added Successfull!!!", "Successfull !!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    /*Clear();
                    ShowData();*/
                }
                else
                {
                    MessageBox.Show("Assignment Add Failed!!!", "Error !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
