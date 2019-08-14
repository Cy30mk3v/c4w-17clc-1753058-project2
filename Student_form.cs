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
    public partial class Student_form : Form
    {
        public int ID;
        public Student_form(int studentID)
        {
            InitializeComponent();
            ID = studentID;
            this.Icon = Student_Management.Properties.Resources._1_28_512;
            List<Grade> grades = new List<Grade>();
            label3.Text = studentID.ToString();
            
            grades = Report.getStudentGrade(studentID);
            int i = 1;
            label4.Text = grades[0].StudentName;
            foreach(Grade G in grades)
            {
                ListViewItem item = new ListViewItem(i.ToString());
                i++;
                item.SubItems.Add(G.CodeCourse);
                item.SubItems.Add(Report.getCourseName(G.CodeCourse));
                item.SubItems.Add(G.Mid_Term.ToString());
                item.SubItems.Add(G.Final_Term.ToString());
                item.SubItems.Add(G.Other_grade.ToString());
                item.SubItems.Add(G.Sum_Grade.ToString());
                listView1.Items.Add(item);
            }
            listView1.Update();
        }


        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Change_Password change = new Change_Password(ID.ToString());
            change.ShowDialog();
        }
    }
}
