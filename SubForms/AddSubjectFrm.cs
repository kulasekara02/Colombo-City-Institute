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
    public partial class AddSubjectFrm : Form
    {
        static string myconn = ConfigurationManager.ConnectionStrings["StudentManagementDBConnectionString"].ConnectionString;
        static string subjectId;
        public AddSubjectFrm()
        {
            InitializeComponent();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtAddSName.Text == string.Empty)
            {
                MessageBox.Show("Please enter batch name.", "Error !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (txtAddSName.TextLength != 0)
            {
                SqlConnection conn = new SqlConnection(myconn);
                string sql = "INSERT INTO tbl_subject (subjectName) VALUES (@subjectName) ";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@subjectName", txtAddSName.Text);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    MessageBox.Show("New Subject Added Successfull!!!", "Successfull !!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();
                    ShowData();
                }
                else
                {
                    MessageBox.Show("New Subject Add Failed!!!", "Error !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }
        public void Clear()
        {
            txtAddSName.Text = "";
        }
        public void Clear1()
        {
            txtUpSName.Text = "";
        }

        private void AddStudent_Load(object sender, EventArgs e)
        {
            ShowData();
        }

        public void ShowData()
        {
            SqlConnection conn = new SqlConnection(myconn);
            conn.Open();
            string sql = "SELECT * FROM tbl_subject";
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
            subjectId = selectedRows.Cells[0].Value.ToString();
            txtUpSName.Text = selectedRows.Cells[1].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtUpSName.Text == string.Empty)
            {
                MessageBox.Show("Please Select Item from Table first.", "ERROR !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (txtUpSName.TextLength != 0)
            {
                SqlConnection conn = new SqlConnection(myconn);
                string sql = "UPDATE tbl_subject SET subjectName=@subjectName WHERE subjectId=@subjectId";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@subjectName", txtUpSName.Text);
                cmd.Parameters.AddWithValue("@subjectId", subjectId);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    MessageBox.Show("Subject Updated Successfull!!!", "Successfull !!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear1();
                    ShowData();
                }
                else
                {
                    MessageBox.Show("Subject Update Failed!!!", "Error !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (txtUpSName.Text == string.Empty)
            {
                MessageBox.Show("Please Select Item from Table first.", "ERROR !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (txtUpSName.TextLength != 0)
            {
                SqlConnection conn = new SqlConnection(myconn);
                string sql = "DELETE FROM tbl_subject WHERE subjectId=@subjectId";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@subjectId", subjectId);

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
