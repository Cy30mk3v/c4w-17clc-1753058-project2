using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Student_Management.DTO;
using Student_Management.DAL;
using Student_Management.BS;

namespace Student_Management
{
    public partial class AddStudentToCourse : Form
    {
        public int StudentID;
        public string CourseID;
        public AddStudentToCourse()
        {
            InitializeComponent();
            List<Student> students = new List<Student>();
            students = Report.GetStudentFromDB();
            foreach(Student S in students)
            {
                comboBox1.Items.Add(S.Name);
            }
            List<Course> courses = new List<Course>();
            foreach(Course C in courses)
            {
                comboBox2.Items.Add(C.FullName);
            }

        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label5.Text = Report.GetStudentID(comboBox1.Text).ToString();
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            label6.Text = Report.GetCourseIDandClass(comboBox2.Text);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Report.addStudentToGradeList(Convert.ToInt32(label5.Text), comboBox1.Text, label6.Text);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
            return;
        }
    }
}
