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
    public partial class AddBatch : Form
    {
        static string myconn = ConfigurationManager.ConnectionStrings["StudentManagementDBConnectionString"].ConnectionString;
        static string batchId;
        public AddBatch()
        {
            InitializeComponent();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtAddBName.Text == string.Empty)
            {
                MessageBox.Show("Please enter batch name.", "Error !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (txtAddBName.TextLength != 0)
            {
                SqlConnection conn = new SqlConnection(myconn);
                string sql = "INSERT INTO tbl_batch (batchName) VALUES (@batchName) ";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@batchName", txtAddBName.Text);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    MessageBox.Show("New Batch Added Successfull!!!", "Successfull !!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();
                    ShowData();
                }
                else
                {
                    MessageBox.Show("New Batch Add Failed!!!", "Error !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }
        public void Clear()
        {
            txtAddBName.Text = "";
        }
        public void Clear1()
        {
            txtUpBName.Text = "";
        }

        private void AddBatch_Load(object sender, EventArgs e)
        {
            ShowData();
        }

        public void ShowData()
        {
            SqlConnection conn = new SqlConnection(myconn);
            conn.Open();
            string sql = "SELECT * FROM tbl_batch";
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
            batchId = selectedRows.Cells[0].Value.ToString();
            txtUpBName.Text = selectedRows.Cells[1].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtUpBName.Text == string.Empty)
            {
                MessageBox.Show("Please Select Item from Table first.", "ERROR !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (txtUpBName.TextLength != 0)
            {
                SqlConnection conn = new SqlConnection(myconn);
                string sql = "UPDATE tbl_batch SET batchName=@batchName WHERE batchId=@batchId";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@batchName", txtUpBName.Text);
                cmd.Parameters.AddWithValue("@batchId", batchId);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    MessageBox.Show("Batch Updated Successfull!!!", "Successfull !!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear1();
                    ShowData();
                }
                else
                {
                    MessageBox.Show("Batch Update Failed!!!", "Error !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (txtUpBName.Text == string.Empty)
            {
                MessageBox.Show("Please Select Item from Table first.", "ERROR !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (txtUpBName.TextLength != 0)
            {
                SqlConnection conn = new SqlConnection(myconn);
                string sql = "DELETE FROM tbl_batch WHERE batchId=@batchId";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@batchId", batchId);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    MessageBox.Show("Batch Delete Successfull!!!", "Successfull !!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear1();
                    ShowData();
                }
                else
                {
                    MessageBox.Show("Batch Delete Failed!!!", "Error !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }

        private void btnCan_Click(object sender, EventArgs e)
        {
            Clear1();
        }
    }
}
