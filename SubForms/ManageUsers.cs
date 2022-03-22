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
    public partial class ManageUsers : Form
    {
        static string myconn = ConfigurationManager.ConnectionStrings["StudentManagementDBConnectionString"].ConnectionString;

        static string userID;
        public ManageUsers()
        {
            InitializeComponent();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cmbAddUType.SelectedIndex < 0)
            {
                MessageBox.Show("Please select user type.", "Error !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (txtAddUName.Text == string.Empty)
            {
                MessageBox.Show("Please enter user name.", "ERROR !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (txtAddPass.Text == string.Empty)
            {
                MessageBox.Show("Please enter password.", "ERROR !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (txtAddUName.TextLength != 0 && txtAddPass.TextLength != 0)
            {
                string type = cmbAddUType.Text;
                string addedDate = DateTime.Today.ToString("d/MM/yyyy");

                SqlConnection conn = new SqlConnection(myconn);
                string sql = "INSERT INTO tbl_users (userName,userPassword,userType,addedDate) VALUES (@userName, @userPassword, @userType, @addedDate) ";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@userName", txtAddUName.Text);
                cmd.Parameters.AddWithValue("@userPassword", txtAddPass.Text);
                cmd.Parameters.AddWithValue("@userType", type);
                cmd.Parameters.AddWithValue("@addedDate", addedDate);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    MessageBox.Show("New User Added Successfull!!!", "Successfull !!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();
                    ShowData();
                }
                else
                {
                    MessageBox.Show("New User Add Failed!!!", "Error !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        public void Clear()
        {
            txtAddPass.Text = "";
            txtAddUName.Text = "";
            cmbAddUType.Text = "";
        }
        public void Clear1()
        {
            txtUpPass.Text = "";
            txtUpUName.Text = "";
            cmbUpUType.Text = "";
        }

        private void ManageUsers_Load(object sender, EventArgs e)
        {
            ShowData();
        }

        public void ShowData()
        {
            SqlConnection conn = new SqlConnection(myconn);
            conn.Open();
            string sql = "SELECT * FROM tbl_users";
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
            userID = selectedRows.Cells[0].Value.ToString();
            txtUpUName.Text = selectedRows.Cells[1].Value.ToString();
            txtUpPass.Text = selectedRows.Cells[2].Value.ToString();
            cmbUpUType.Text = selectedRows.Cells[3].Value.ToString();
            
        }
        //Should change here
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtUpUName.Text == string.Empty || txtUpPass.Text == string.Empty)
            {
                MessageBox.Show("Please Select Item from Table first.", "ERROR !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (cmbUpUType.Text == string.Empty)
            {
                MessageBox.Show("Please select user type.", "Error !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }


            if (txtUpUName.TextLength != 0 && txtUpPass.TextLength != 0)
            {
                string type = cmbUpUType.Text;

                SqlConnection conn = new SqlConnection(myconn);
                string sql = "UPDATE tbl_users SET userName=@userName, userPassword =@userPassword, userType=@userType WHERE id=@userID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@userName", txtUpUName.Text);
                cmd.Parameters.AddWithValue("@userPassword", txtUpPass.Text);
                cmd.Parameters.AddWithValue("@userType", type);
                cmd.Parameters.AddWithValue("@userID", userID);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    MessageBox.Show("User Updated Successfull!!!", "Successfull !!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear1();
                    ShowData();
                }
                else
                {
                    MessageBox.Show("User Update Failed!!!", "Error !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (txtUpUName.Text == string.Empty || txtUpPass.Text == string.Empty)
            {
                MessageBox.Show("Please Select Item from Table first.", "ERROR !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (cmbUpUType.Text == string.Empty)
            {
                MessageBox.Show("Please select user type.", "Error !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }


            if (txtUpUName.TextLength != 0 && txtUpPass.TextLength != 0)
            {
                SqlConnection conn = new SqlConnection(myconn);
                string sql = "DELETE FROM tbl_users WHERE id=@userID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@userID", userID);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    MessageBox.Show("User Delete Successfull!!!", "Successfull !!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear1();
                    ShowData();
                }
                else
                {
                    MessageBox.Show("User Delete Failed!!!", "Error !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }

        private void btnCan_Click(object sender, EventArgs e)
        {
            Clear1();
        }
    }
}
