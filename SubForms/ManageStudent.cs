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
    public partial class ManageStudent : Form
    {
        static string myconn = ConfigurationManager.ConnectionStrings["StudentManagementDBConnectionString"].ConnectionString;
        static string studentId;
        public ManageStudent()
        {
            InitializeComponent();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ManageStudent_Load(object sender, EventArgs e)
        {
            showBatch();
            ShowData();
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

            cmbAddBatch.DisplayMember = "batchName";
            cmbAddBatch.ValueMember = "batchName";
            cmbAddBatch.DataSource = dt;

            cmbAddBatch.Enabled = true;


            cmbUpBatch.DisplayMember = "batchName";
            cmbUpBatch.ValueMember = "batchName";
            cmbUpBatch.DataSource = dt;

            cmbUpBatch.Enabled = true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtAddStuName.Text == string.Empty)
            {
                MessageBox.Show("Please enter student name.", "ERROR !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (dtpAddDOB.Value == null)
            {
                MessageBox.Show("Please select birthday.", "Error !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (txtAddStuCont.Text == string.Empty)
            {
                MessageBox.Show("Please enter contact number.", "Error !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (cmbAddStreamType.SelectedIndex < 0)
            {
                MessageBox.Show("Please select stream.", "Error !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (cmbAddBatch.SelectedIndex < 0)
            {
                MessageBox.Show("Please select batch.", "Error !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (txtAddStuName.TextLength != 0 && txtAddStuCont.TextLength != 0)
            {
                string stream = cmbAddStreamType.Text;
                string batch = cmbAddBatch.Text;
                string DOB = dtpAddDOB.Value.ToString();

                SqlConnection conn = new SqlConnection(myconn);
                string sql = "INSERT INTO tbl_student (studentName,DOB,contactNO,stream,batch) VALUES (@studentName,@DOB,@contactNO,@stream,@batch) ";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@studentName", txtAddStuName.Text);
                cmd.Parameters.AddWithValue("@DOB", DOB);
                cmd.Parameters.AddWithValue("@contactNO", txtAddStuCont.Text);
                cmd.Parameters.AddWithValue("@stream", stream);
                cmd.Parameters.AddWithValue("@batch", batch);



                conn.Open();
                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    MessageBox.Show("New Student Added Successfull!!!", "Successfull !!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();
                    ShowData();
                }
                else
                {
                    MessageBox.Show("New Student Add Failed!!!", "Error !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        public void Clear()
        {
            txtAddStuName.Text = "";
            txtAddStuCont.Text = "";
            dtpAddDOB.Value = DateTime.Today;
            cmbAddStreamType.Text="";
            cmbAddBatch.Text="";
        }
        public void Clear1()
        {
            txtUpStuName.Text = "";
            txtUpStuCont.Text = "";
            dtpUpDOB.Value = DateTime.Today;
            cmbUpStreamType.Text = "";
            cmbUpBatch.Text = "";

        }

        public void ShowData()
        {
            SqlConnection conn = new SqlConnection(myconn);
            conn.Open();
            string sql = "SELECT * FROM tbl_student";
            SqlCommand cmd = new SqlCommand(sql, conn);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int ind = e.RowIndex;
            DataGridViewRow selectedRows = dataGridView1.Rows[ind];
            studentId = selectedRows.Cells[0].Value.ToString();
            txtUpStuName.Text = selectedRows.Cells[1].Value.ToString();
            dtpUpDOB.Value= Convert.ToDateTime(selectedRows.Cells[2].Value);
            txtUpStuCont.Text = selectedRows.Cells[3].Value.ToString();
            cmbUpStreamType.Text = selectedRows.Cells[4].Value.ToString();
            cmbUpBatch.Text = selectedRows.Cells[5].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtUpStuName.Text == string.Empty || txtUpStuCont.Text == string.Empty)
            {
                MessageBox.Show("Please Select Item from Table first.", "ERROR !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (cmbUpStreamType.Text == string.Empty)
            {
                MessageBox.Show("Please select stream.", "Error !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
           
            if (cmbUpBatch.Text == string.Empty)
            {
                MessageBox.Show("Please select batch.", "Error !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
             
            if (dtpUpDOB.Value == null)
            {
                MessageBox.Show("Please select birthday.", "Error !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (txtUpStuName.TextLength != 0 && txtUpStuCont.TextLength != 0)
            {
                string stream = cmbUpStreamType.Text;
                string batch = cmbUpBatch.Text;
                string DOB = dtpUpDOB.Value.ToString();

                SqlConnection conn = new SqlConnection(myconn);
                string sql = "UPDATE tbl_student SET studentName=@studentName, DOB=@DOB, contactNO=@contactNO, stream=@stream, batch=@batch WHERE studentId=@studentId";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@studentName", txtUpStuName.Text);
                cmd.Parameters.AddWithValue("@DOB", DOB);
                cmd.Parameters.AddWithValue("@contactNO", txtUpStuCont.Text);
                cmd.Parameters.AddWithValue("@stream", stream);
                cmd.Parameters.AddWithValue("@batch", batch);
                cmd.Parameters.AddWithValue("@studentId", studentId);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    MessageBox.Show("Student Updated Successfull!!!", "Successfull !!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear1();
                    ShowData();
                }
                else
                {
                    MessageBox.Show("Student Update Failed!!!", "Error !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (txtUpStuName.Text == string.Empty || txtUpStuCont.Text == string.Empty)
            {
                MessageBox.Show("Please Select Item from Table first.", "ERROR !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (cmbUpStreamType.Text == string.Empty)
            {
                MessageBox.Show("Please select stream.", "Error !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            if (cmbUpBatch.Text == string.Empty)
            {
                MessageBox.Show("Please select batch.", "Error !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            if (dtpUpDOB.Value == null)
            {
                MessageBox.Show("Please select birthday.", "Error !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (txtUpStuName.TextLength != 0 && txtUpStuCont.TextLength != 0)
            {
                SqlConnection conn = new SqlConnection(myconn);
                string sql = "DELETE FROM tbl_student WHERE studentId=@studentId";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@studentId", studentId);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    MessageBox.Show("Subject Delete Successfull!!!", "Successfull !!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear1();
                    ShowData();
                }
                else
                {
                    MessageBox.Show("Subject Delete Failed!!!", "Error !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }

        private void btnCan_Click(object sender, EventArgs e)
        {
            Clear1();
        }
    }
}
