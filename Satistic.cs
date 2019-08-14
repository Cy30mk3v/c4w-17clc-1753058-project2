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
    public partial class Satistic : Form
    {
        public Satistic(List<Grade> G,string code, string Class)
        {
            InitializeComponent();
            label6.Text = Report.getCourseName(code);
            label7.Text = Class;
            label8.Text = code;
            label12.Text = G.Count().ToString();
            int p = countpercent(G);
           // chart1.Series["Student"].Points.AddXY("Pass", p);
            //chart1.Series["Student"].Points.AddXY("Fail", 100 - p);
            List<Grade> pass = new List<Grade>();
            pass = getPass(G);

            label13.Text = pass.Count().ToString();
            List<Grade> fail = new List<Grade>();
            fail = getFail(G);
            label14.Text = fail.Count().ToString();
            int i = 1;
            foreach (Grade g in pass)
            {
                ListViewItem temp = new ListViewItem(i.ToString());
                i++;
                temp.SubItems.Add(g.StudentID.ToString());
                temp.SubItems.Add(g.StudentName);
                temp.SubItems.Add(g.Sum_Grade.ToString());
                listView1.Items.Add(temp);
            }
            i = 1;
            foreach (Grade g in fail)
            {
                ListViewItem temp = new ListViewItem(i.ToString());
                i++;
                temp.SubItems.Add(g.StudentID.ToString());
                temp.SubItems.Add(g.StudentName);
                temp.SubItems.Add(g.Sum_Grade.ToString());
                listView2.Items.Add(temp);
            }
            listView1.Update();
            listView2.Update();
        }


        List<Grade> getPass (List<Grade> G)
        {
            List<Grade> grades = new List<Grade>();
            foreach (Grade Grade in G)
            {
                Console.WriteLine(Grade.Sum_Grade);
                if (Grade.Sum_Grade>=5.0)
                {
                    
                    grades.Add(Grade);
                }
            }
            return grades;
        }

        int countpercent(List<Grade> total)
        {
            List<Grade> Pass = getPass(total);
            float p = (float)Pass.Count / (float)total.Count();
            p *= 100;
            return (int)p;
        }
        List<Grade> getFail(List<Grade> G)
        {
            List<Grade> grades = new List<Grade>();
            foreach (Grade Grade in G)
            {
                if (Grade.Sum_Grade < 5.0)
                {
                    grades.Add(Grade);
                }
            }
            return grades;
        }

       
        private void Chart1_Click(object sender, EventArgs e)
        {

        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void Label5_Click(object sender, EventArgs e)
        {

        }

        private void Label12_Click(object sender, EventArgs e)
        {

        }
    }
}

