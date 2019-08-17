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
    public partial class AddStudent : Form
    {
        public Student S;
        public AddStudent()
        {
            InitializeComponent();
            List<Class> classes = new List<Class>();
            classes = Report.GetClassFromDB();
            foreach(Class c in classes)
            {
                Console.WriteLine(c.Name);
                comboBox2.Items.Add(c.Name);
            }
            S = new Student();
            dateTimePicker1.Value = DateTime.Now;
            

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        
        private void AddStudent_Load(object sender, EventArgs e)
        {

        }

        private void Label4_Click(object sender, EventArgs e)
        {

        }

        private void Label3_Click(object sender, EventArgs e)
        {

        }

        private void Label6_Click(object sender, EventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string date = dateTimePicker1.Value.ToString("dd-MM-yyyy");
            //if(dateTimePicker1.Value.)
            //Console.WriteLine(date);
            date = date.Replace("-", "");
            date = date.Replace("/", "");
            S.birthday = date;
            S.StudentID = Convert.ToInt32(textBox1.Text);
            S.Name = textBox2.Text;
            S.Gender = Convert.ToChar(comboBox1.Text);
            S.Social_ID = textBox3.Text;
            S.Class = comboBox2.Text;
            this.Close();
        }
    }
}
