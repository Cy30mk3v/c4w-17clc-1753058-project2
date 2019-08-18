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
        public string SQL { get; set; }
        public Form1()
        {
            InitializeComponent();
            this.Text="Student management";

            //this.label1.Font = new Font("Arial", 20);
            textBox2.PasswordChar = '*';
            textBox3.Text= "Provider=SQLNCLI11;Server=DESKTOP-SS8KMOM;Database=StudentManagement;Trusted_Connection=Yes;";
            this.Icon = Student_Management.Properties.Resources._1_28_512;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public string getSQL()
        {
            return SQL;
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
            Report.SQL = textBox3.Text;
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
                    Student_form f = new Student_form(Convert.ToInt32(textBox1.Text));
                    newLogin();
                    f.ShowDialog();
                }
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

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void Button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {

        }

        private void Button4_Click(object sender, EventArgs e)
        {
            AddAccount add = new AddAccount();
            add.ShowDialog();
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            Button1_Click(sender, e);
        }

        private void TextBox3_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.textBox3,"Input your connect string");
            //toolTip1.SetToolTip(this.textBox3, "Input your connect string");
        }
    }
}
