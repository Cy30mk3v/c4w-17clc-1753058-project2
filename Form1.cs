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
using Student_Management.BS;
using Student_Management.DAL;


namespace Student_Management
{
    public partial class Form1 : Form
    {
        public string username { get; set; }
        public string password { get; set; }
        public Form1()
        {
            InitializeComponent();
            this.Text="Student management 17CLC1";

            //this.label1.Font = new Font("Arial", 20);
            textBox2.PasswordChar = '*';
            this.Icon = Student_Management.Properties.Resources._1_28_512;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Label3_Click(object sender, EventArgs e)
        {

        }

        void newLogin()
        {
            textBox1.Clear();
            textBox2.Clear();
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            username = textBox1.Text;
            password = textBox2.Text;
            List<Account> accounts = new List<Account>();
            accounts = Report.GetAccountsFromDB();

            Business b = new Business();
            if (!Business.checkPassword(username, password))
            {
                MessageBox.Show("Please check your username or password!");
            }
            else
            {
                if (username == "giaovu")
                {
                    Teacher_form t = new Teacher_form();
                    newLogin();
                    t.ShowDialog();
                }
                else
                {

                }
            }
        }


        private void addStudentListToView(ListView list,List<Student> students)
        {
            foreach(Student student in students)
            {
                ListViewItem temp = new ListViewItem(student.ID.ToString());
                temp.SubItems.Add(student.StudentID.ToString());
                temp.SubItems.Add(student.Name);
                temp.SubItems.Add(student.Gender.ToString());
                temp.SubItems.Add(student.Social_ID);
                list.Items.Add(temp);

            }
        }
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                Button1_Click(sender, e);
            }
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Button1_Click(sender, e);
            }
        }

        private void TextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {   
                Button1_Click(sender, e);
            }
        }
    }
}
