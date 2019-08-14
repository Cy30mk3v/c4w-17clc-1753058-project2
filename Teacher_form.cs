using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using Student_Management.DTO;
using Student_Management.DAL;
using Student_Management.BS;


namespace Student_Management
{
    public partial class Teacher_form : Form
    {
        public Teacher_form()
        {
            InitializeComponent();
            label3.Hide();
            label3.Enabled = false;
            comboBox3.Hide();
            comboBox3.Enabled = false;
            this.Icon = Student_Management.Properties.Resources._1_28_512;
        }

        public void readCSV()
        {


        }
        private void Button1_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog open = new OpenFileDialog();
            open.InitialDirectory = "C:\\";
            var a=open.ShowDialog();
            string path = open.FileName;
            if (path == null || a!=DialogResult.OK)
            {
                return;
            }
            FileInfo fi = new FileInfo(path);
            //Console.WriteLine("Button1");
            if (Path.GetExtension(path) != ".csv")
            {
                MessageBox.Show("Not csv file!");
            }
            else
            {
               

                //listView1.Items.Clear();
                
                if (Business.checkCSV(path) == Business.Student_list)
                {
                    List<Student> students = new List<Student>();
                    students = Report.GetStudents(path);
                    Report.addClassToDB(students[0].Class);
                    Console.WriteLine("Success in f1");
                    foreach (Student s in students)
                    {
                        //Console.WriteLine(s.StudentID);
                        Report.addStudentToDB(s);

                    }
                }
                if(Business.checkCSV(path)==Business.Time_table_list)
                {
                    List<Course> courses = new List<Course>();
                    courses = Report.GetCourse(path);
                    
                    foreach(Course temp in courses)
                    {
                        //Console.WriteLine(temp.codeName + " " + temp.FullName + " " + temp.room + " " + temp.Class);
                        Report.addCourseToDB(temp);
                    }
                }
                if(Business.checkCSV(path)==Business.Class_Course_list)
                {
                    List<Grade> grades = new List<Grade>();
                    grades = Report.GetListCourse_Class_List(path);
                    foreach(Grade G in grades)
                    {
                        Report.addCourse_Class_List_EToDB(G);
                    }
                }
                if(Business.checkCSV(path)==Business.Grade_list)
                {
                    List<Grade> grades = new List<Grade>();
                    grades = Report.GetGrades(path);
                    foreach(Grade G in grades)
                    {
                        Report.addGradeToDB(G);
                    }
                }
                //listView1.Update();
            }
            
        }
        public void putGradeFromClassCourseListToLV()
        {

        }
        public ListViewItem addStudentToLV(Student S, int i)
        {
            ListViewItem item = new ListViewItem(i.ToString());
            item.SubItems.Add(S.StudentID.ToString());
            item.SubItems.Add(S.Name);
            item.SubItems.Add(S.Gender.ToString());
            item.SubItems.Add(S.Social_ID);
            return item;
        }

        public ListViewItem addGradeToLV(Grade G, int i)
        {
            ListViewItem item= new ListViewItem(i.ToString());
            item.SubItems.Add(G.StudentID.ToString());
            item.SubItems.Add(G.StudentName);
            item.SubItems.Add(G.Mid_Term.ToString());
            item.SubItems.Add(G.Final_Term.ToString());
            item.SubItems.Add(G.Other_grade.ToString());
            item.SubItems.Add(G.Sum_Grade.ToString());
            return item;
        }
        public ListViewItem addCourseToLV(Course S, int i)
        {
            ListViewItem item = new ListViewItem(i.ToString());
            item.SubItems.Add(S.codeName);
            item.SubItems.Add(S.FullName);
            item.SubItems.Add(S.room);
           
            return item;
        }
        public void addStudent()
        {

        }
        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            
            listView1.Items.Clear();
            List <Student> students= new List<Student>();
            students = Report.GetStudentFromDB();
            int i = 1;
            foreach(Student S in students)
            {
                var item = new ListViewItem();
                item = addStudentToLV(S,i);
                i++;
                
                listView1.Items.Add(item);
            }
            listView1.Update();
            //return results;

        }

        private void Teacher_Load(object sender, EventArgs e)
        {

        }

        private void Button4_Click(object sender, EventArgs e)
        {

        }

        private void Button3_Click(object sender, EventArgs e)
        {
            this.Close();
            
           
        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void Button5_Click(object sender, EventArgs e)
        {
            AddStudent add = new AddStudent();
            add.ShowDialog();
            Student S1 = new Student();
            S1 = add.S;
            if(S1.StudentID==default(int))
            {
                return;
            }
            if(Business.checkStudentInDB(S1.StudentID))
            {
                MessageBox.Show("This student already existed!");
                return;

            }
            Report.addStudentToDB(S1);
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            comboBox1.Items.Clear();
            if (comboBox2.Text=="Student list" || comboBox2.Text == "Time-table list")
            {
                
                List<Class> classes = new List<Class>();
                if(comboBox2.Text=="Student list")
                {
                    classes = Report.GetClassFromDB();
                    
                    if(listView1.Columns.Count!=5)
                    {
                        listView1.Columns[1].Text = "MSSV";
                        listView1.Columns[2].Text= "Họ và Tên";
                        listView1.Columns[3].Text="Giới tính";
                        listView1.Columns[4].Text = "CMND";
                        listView1.Columns[5].Width = 0;
                        listView1.Columns[6].Width = 0;
                        //listView1.Columns[7].Width = 0;
                        listView1.Update();
                    }
                }
                else
                {
                    Console.WriteLine("a");
                    //if(listView1.Columns.Count)
                    if (listView1.Columns.Count != 4)
                    {
                        classes = Report.GetClassFromDB_Course();
                        listView1.Columns[1].Text = "Mã môn";
                        listView1.Columns[2].Text = "Tên môn";
                        listView1.Columns[3].Text = "Phòng học";
                        listView1.Columns[4].Width = 0;
                        listView1.Columns[5].Width = 0;
                        listView1.Columns[6].Width = 0;
                        // listView1.Columns[7].Width = 0;
                        listView1.Update();
                    }
                   
                }
                foreach(Class C in classes)
                {
                    comboBox1.Items.Add(C.Name);
                }
                comboBox1.Items.Add("All");
                
                label3.Hide();
                label3.Enabled = false;
                comboBox3.Hide();
                comboBox3.Enabled = false;
                //label2.Text = "Class";
            }
            if(comboBox2.Text == "Class-Course list" || comboBox2.Text=="Grade list")
            {
                List<Class> classes = new List<Class>();
                classes = Report.GetClassFromDB();
                foreach (Class C in classes)
                {
                    comboBox1.Items.Add(C.Name);
                }
                comboBox1.Items.Add("All");
                List<Course> courses = new List<Course>();
                courses = Report.GetCourseFromDB_Class(comboBox1.Text);
                foreach(Course course in courses)
                {
                    comboBox3.Items.Add(course.codeName);
                }
                label3.Show();
                label3.Enabled = true;
                comboBox3.Show();
                comboBox3.Enabled = true;
                if (comboBox2.Text=="Class-Course list")
                {
                    if (listView1.Columns.Count != 5)
                    {
                        listView1.Columns[1].Text = "MSSV";
                        listView1.Columns[2].Text = "Họ và Tên";
                        listView1.Columns[3].Text = "Giới tính";
                        listView1.Columns[4].Text = "CMND";
                        listView1.Columns[5].Width = 0;
                        listView1.Columns[6].Width = 0;
                        //listView1.Columns[7].Width = 0;
                        listView1.Update();
                    }
                }
                else
                {
                    if (listView1.Columns.Count != 4)
                    {
                        classes = Report.GetClassFromDB_Course();
                        listView1.Columns[1].Text = "MSSV";
                        listView1.Columns[2].Text = "Họ và Tên";
                     
                        listView1.Columns[3].Text = "Điểm GK";
                        listView1.Columns[4].Text = "Điểm CK";
                        listView1.Columns[5].Text = "Điểm khác";
                        listView1.Columns[6].Text = "Điểm tổng";
                        listView1.Update();
                    }
                }
             
            }
        }

        private void Button4_Click_1(object sender, EventArgs e)
        {

        }

        private void Button6_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text == "Student list")
            {
                List<Student> students = new List<Student>();
                if (comboBox1.Text == "All")
                {
                    students = Report.GetStudentFromDB();
                }
                else
                {
                    students = Report.GetStudentFromDB_Class(comboBox1.Text);
                }

                listView1.Items.Clear();
                int i = 1;
                foreach (Student S in students)
                {
                    var item = new ListViewItem();
                    item = addStudentToLV(S,i);
                    i++;

                    listView1.Items.Add(item);
                }
                listView1.Update();
            }
            if(comboBox2.Text=="Time-table list")
            {
                List<Course> courses = new List<Course>();
                if (comboBox1.Text == "All")
                {
                    courses = Report.getCourseFromDB();
                }
                else
                {
                    courses = Report.GetCourseFromDB_Class(comboBox1.Text);
                }

                listView1.Items.Clear();
                int i = 1;
                foreach (Course S in courses)
                {
                    var item = new ListViewItem();
                    item = addCourseToLV(S, i);
                    i++;

                    listView1.Items.Add(item);
                }
                listView1.Update();
            }
            if (comboBox2.Text == "Class-Course list")
            {
                List<Student> students = new List<Student>();
                //Console.WriteLine(comboBox1.Text);
                students = Report.GetStudentsFromCCL_DB(comboBox3.Text, comboBox1.Text);
                listView1.Items.Clear();
                int i = 1;
                foreach (Student S in students)
                {
                    var item = new ListViewItem();
                    item = addStudentToLV(S, i);
                    i++;

                    listView1.Items.Add(item);
                }
                listView1.Update();
            }
            if(comboBox2.Text=="Grade list")
            {
                Console.WriteLine("Right grade?");
                List<Grade> grades = new List<Grade>();
                grades = Report.GetGradesFromDB(comboBox1.Text, comboBox3.Text);
                listView1.Items.Clear();
                int i = 1;
                foreach (Grade G in grades)
                {
                    var item = new ListViewItem();
                    item = addGradeToLV(G, i);
                    i++;
                    Console.WriteLine(i.ToString());
                    listView1.Items.Add(item);
                }
                listView1.Update();
            }
        }

        private void AddToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Class = comboBox1.Text;
            List<Class> courses = new List<Class>();
            Console.WriteLine("Changed!");
            comboBox3.Items.Clear();
            courses = Report.GetClassesforCCL_DB(Class);
            foreach (Class C in courses)
            {
                Console.WriteLine(C.Name);
                comboBox3.Items.Add(C.Name);
            }
           
        }
    }
}
