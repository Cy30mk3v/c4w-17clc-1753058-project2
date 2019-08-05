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


namespace Student_Management
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Text="Student management 17CLC1";

            this.label1.Font = new Font("Arial", 20);
            textBox2.PasswordChar = '*';
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

        private void Button1_Click(object sender, EventArgs e)
        {
            if (!checkPassWord())
            {
                MessageBox.Show("Please check your username or password");
            }
            else
            {
                //this.Close();
                Teacher form_T = new Teacher();
                form_T.ShowDialog();

                //Password
            }
        }
          
        private void addStudentListToView(ListView list,List<Student> students)
        {
            foreach(var student in students)
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
