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
    public partial class Student : Form
    {
        public Student()
        {
            InitializeComponent();
        }


        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void yourInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            YourInfo yi = new YourInfo();
            yi.Show();
        }

        private void assignmentStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AssignmentStatus astatus = new AssignmentStatus();
            astatus.Show();
        }

        private void assignmentGradesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewAssignmentStatus vaStatus = new ViewAssignmentStatus();
            vaStatus.Show();
        }
    }
}
