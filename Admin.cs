using Student_Management_System.SubForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Student_Management_System
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void manageUsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageUsers mu = new ManageUsers();
            mu.Show();

        }

        private void addSubjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddSubjectFrm ast = new AddSubjectFrm();
            ast.Show();
        }

        private void addBatchDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddBatch ab = new AddBatch();
            ab.Show();
        }

        private void manageStudentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageStudent ms = new ManageStudent();
            ms.Show();
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            
        }

        private void submissionStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StudentAssignment sa = new StudentAssignment();
            sa.Show();
        }

        private void assignmentGradesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AssignmentGrades ag = new AssignmentGrades();
            ag.Show();
        }

        private void viewStatusOfAssignmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewStatus views = new ViewStatus();
            views.Show();
        }

        private void Admin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 frm = new Form1();
            frm.Show();
        }
    }
}
