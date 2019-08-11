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
            open.ShowDialog();
            string path = open.FileName;
            if (path == null)
            {
                return;
            }
            FileInfo fi = new FileInfo(path);
            Console.WriteLine("Button1");
            if (Path.GetExtension(path) != ".csv")
            {
                MessageBox.Show("Not csv file!");
            }
            else
            {
               

                listView1.Items.Clear();
                List<Student> students = new List<Student>();
                students = Report.GetStudents(path);
               foreach (Student s in students)
                {

                    ListViewItem temp = new ListViewItem();
                    temp = addStudentToLV(s);
                    listView1.Items.Add(temp);
                    
                }
                listView1.Update();
            }
            
        }

        public ListViewItem addStudentToLV(Student S)
        {
            ListViewItem item = new ListViewItem(S.ID.ToString());
            item.SubItems.Add(S.StudentID.ToString());
            item.SubItems.Add(S.Name);
            item.SubItems.Add(S.Gender.ToString());
            item.SubItems.Add(S.Social_ID);
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
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = "Provider=SQLNCLI11;Server=DESKTOP-SS8KMOM;Database=StudentManagement;Trusted_Connection=Yes;";
            conn.Open();

            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select * from Student";

            OleDbDataReader rd = cmd.ExecuteReader();
            listView1.Items.Clear();
            while (rd.Read())
            {
                var item = new ListViewItem();
                item.Text = rd.GetInt32(0).ToString();
                item.SubItems.Add(rd.GetInt32(1).ToString());
                item.SubItems.Add(rd.GetString(2));
                Console.WriteLine(rd.GetString(3));
                if (rd.GetString(3).Equals("M"))
                {
                    item.SubItems.Add("M");
                }
                else
                {
                    item.SubItems.Add("F");
                }
                item.SubItems.Add(rd.GetString(4));
                listView1.Items.Add(item);
            }
            conn.Close();
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
            Student S = new Student();
            S.Name = "Hung";
            S.Gender = 'M';
            S.StudentID = 1234567;
            S.Social_ID = "1211212";
            Report.addStudentToDB(S);
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox2.Text=="Student list" || comboBox2.Text == "Time-table list")
            {
                label3.Hide();
                label3.Enabled = false;
                comboBox3.Hide();
                comboBox3.Enabled = false;
                //label2.Text = "Class";
            }
            if(comboBox2.Text == "Class-Course list" || comboBox2.Text=="Grade list")
            {
                label3.Show();
                label3.Enabled = true;
                comboBox3.Show();
                comboBox3.Enabled = true;
            }
        }
    }
}
