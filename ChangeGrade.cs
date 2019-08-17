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

namespace Student_Management
{
    public partial class ChangeGrade : Form
    {
        public ChangeGrade(string StudentID, string code)
        {
            InitializeComponent();
            label1.Text = StudentID;
            label3.Text = code;
            label2.Text=Report.getCourseName(code);
            label4.Text = Report.getStudentName(StudentID);
            comboBox1.Text = comboBox2.Text = comboBox3.Text = comboBox4.Text = "0";
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            float p1, p2, p3, p4;
            p1 = float.Parse(comboBox1.Text);
            p2 = float.Parse(comboBox2.Text);
            p3 = float.Parse(comboBox3.Text);
            p4 = float.Parse(comboBox4.Text);
            Report.updateGrade(label1.Text, label3.Text, p1, p2, p3, p4);
            MessageBox.Show("Success!");
            return;
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
