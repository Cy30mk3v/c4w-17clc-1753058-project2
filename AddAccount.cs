using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Student_Management.DAL;
using Student_Management.BS;

namespace Student_Management
{
    public partial class AddAccount : Form
    {
        public AddAccount()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string date = dateTimePicker1.Value.ToString("dd-mm-yyyy");
            //if(dateTimePicker1.Value.)
            //Console.WriteLine(date);
            date.Replace("-", "");
            if(string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Must input Student's ID!");
                return;
            }
            if(Business.checkAccountExist(textBox1.Text))
            {
                MessageBox.Show("This account is already existed!");
                return;
            }
            Report.addAccount(textBox1.Text, date);
            MessageBox.Show("Create account successed!");
            this.Close();
        }
    }
}
