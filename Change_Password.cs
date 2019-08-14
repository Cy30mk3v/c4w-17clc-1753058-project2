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

namespace Student_Management
{
    public partial class Change_Password : Form
    {
        public string user;
       
        public Change_Password(string username)
        {
            InitializeComponent();
            user = username;
            textBox1.PasswordChar = '*';
            
        }

        private void Label1_Click(object sender, EventArgs e)
        {
            
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Not contains whitespace or empty password!");
                return;
            }
            Report.changePassword(user, textBox1.Text);
            MessageBox.Show("Change password successs!");
            this.Close();
        }
    }
}
