using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace Student_Management_System
{
    public partial class Form1 : Form
    {
        static string myconn = ConfigurationManager.ConnectionStrings["StudentManagementDBConnectionString"].ConnectionString;
        public static string logUsername;
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cmbUType.SelectedIndex < 0)
            {
                MessageBox.Show("Please select User Type.", "Error !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (txtUName.Text == string.Empty)
            {
                MessageBox.Show("Please enter User Name.", "Error !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
         
            if (txtPass.Text == string.Empty)
            {
                MessageBox.Show("Please enter Password.", "Error !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (txtUName.TextLength != 0 && txtPass.TextLength != 0)
            {
                string type = cmbUType.Text;

                SqlConnection conn = new SqlConnection(myconn);
                string sql = "SELECT * FROM tbl_users WHERE userName=@userName AND userPassword=@userPassword AND userType=@userType";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@userName", txtUName.Text);
                cmd.Parameters.AddWithValue("@userPassword", txtPass.Text);
                cmd.Parameters.AddWithValue("@userType", type);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    logUsername = txtUName.Text;
                    MessageBox.Show("Login Successfull!!!", "Successfull !!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    switch (type)
                    {
                        case "Admin":
                            {
                                Admin a = new Admin();
                                a.Show();
                                this.Hide();
                            }
                            break;
                        
                        case "Student":
                            {
                                Student s = new Student();
                                s.Show();
                                this.Hide();
                            }
                            break;
                        default:
                            {
                                MessageBox.Show("Invalid User Type!!!", "Error !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Login Failed!!!", "Error !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }
    }
}
