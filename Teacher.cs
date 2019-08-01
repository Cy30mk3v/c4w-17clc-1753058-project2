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

namespace Student_Management
{
    public partial class Teacher : Form
    {
        public Teacher()
        {
            InitializeComponent();
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
            FileInfo fi = new FileInfo(path);
            
            if (Path.GetExtension(path) != ".csv")
            {
                MessageBox.Show("Not csv file!");
            }
            else
            {
                StreamReader sr = new StreamReader(path);
                string line;
                
                line = sr.ReadLine();
                var split = line.Split(',');
                label1.Text = split[0];
                Console.WriteLine(line);
                line = sr.ReadLine();
                Console.WriteLine(line);
                //int i = 0;
                //line = sr.ReadLine();
                listView1.Items.Clear();
                while(!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    var values = line.Split(',');
                    ListViewItem temp = new ListViewItem();
                    temp.Text = values[0];
                    
                    temp.SubItems.Add(values[1]);
                    temp.SubItems.Add(values[2]);
                    temp.SubItems.Add(values[3]);
                    temp.SubItems.Add(values[4]);
                    Console.WriteLine(values[0]+ values[1] + values[2] + values[3]);
                    listView1.Items.Add(temp);
                    
                }
                listView1.Update();
            }
            
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
                    item.SubItems.Add("Nam");
                    Console.WriteLine(rd.GetString(3));
                }
                else
                {
                    item.SubItems.Add("Nữ");
                }
                item.SubItems.Add(rd.GetString(4));
                listView1.Items.Add(item);
            }
            conn.Close();
            //return results;

        }
    }
}
