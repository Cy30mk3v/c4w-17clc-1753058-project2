using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Student_Management.DTO;
using System.Data.OleDb;

namespace Student_Management.DAL
{
    public class Report
    {
        static public List<Student> GetStudents(string path)
        {
            List<Student> result = new List<Student>();
            StreamReader sr = new StreamReader(path);
            string line;
            line = sr.ReadLine();
            var split = line.Split(',');


            line = sr.ReadLine();
            int i = 1;
            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                var values = line.Split(',');
                Student temp = new Student();
                temp.ID = i;
                i++;
                temp.StudentID = Convert.ToInt32(values[1]);
                temp.Name = (values[2]);
                temp.Gender = 'F';
                if (values[3] == "Nam")
                {
                    temp.Gender = 'M';
                }
               
                temp.Social_ID = (values[4]);
                result.Add(temp);

            }
            return result;
        }

        static public void addStudentToDB(Student S)
        {
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = "Provider=SQLNCLI11;Server=DESKTOP-SS8KMOM;Database=StudentManagement;Trusted_Connection=Yes;";
            conn.Open();
            string split1 = ",'";
            string s2 = "'";
            string split2 = s2.ToString();
            string split3 = split2 + split1;
            string pre =  "INSERT INTO Student (StudentID,Name,Gender,Social_ID) VALUES (";
            string post = S.StudentID.ToString() + split1 + S.Name + split3 + S.Gender.ToString() + split3 + S.Social_ID + split2 + S.@class  + ")";
            OleDbCommand cmd = new OleDbCommand(pre + post, conn);

            Console.WriteLine(pre + post);
           
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        static public List<Account> GetAccountsFromDB()
        {

            
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = "Provider=SQLNCLI11;Server=DESKTOP-SS8KMOM;Database=StudentManagement;Trusted_Connection=Yes;";
            conn.Open();

            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM Accounts";

            OleDbDataReader rd = cmd.ExecuteReader();
            List<Account> accounts = new List<Account>();
            while (rd.Read())
            {
                var temp = new Account();
                temp.UserName = rd.GetString(0);
                temp.PassWord = rd.GetString(1);
                Console.WriteLine(rd.GetString(0));
                accounts.Add(temp);
            }
            conn.Close();
            

            return accounts;


        }
        static public List<Student> GetStudentFromDB()
        {
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = "Provider=SQLNCLI11;Server=DESKTOP-SS8KMOM;Database=StudentManagement;Trusted_Connection=Yes;";
            conn.Open();

            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select * from Student";

            OleDbDataReader rd = cmd.ExecuteReader();
            List<Student> results = new List<Student>();
            while (rd.Read())
            {
                var item = new Student();
                item.ID = rd.GetInt32(0);
                item.StudentID= (rd.GetInt32(1));
                item.Name=(rd.GetString(2));
                //Console.WriteLine(rd.GetString(3));
                if (rd.GetString(3).Equals("M"))
                {
                    item.Gender = 'M';
                }
                else
                {
                    item.Gender = 'F';
                }
                item.Social_ID=(rd.GetString(4));
                results.Add(item);
            }
            conn.Close();
            return results;
        }

        static public List<Class> GetClassFromDB()
        {
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = "Provider=SQLNCLI11;Server=DESKTOP-SS8KMOM;Database=StudentManagement;Trusted_Connection=Yes;";
            conn.Open();

            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select * from Class";

            OleDbDataReader rd = cmd.ExecuteReader();
            List<Class> results = new List<Class>();
            while (rd.Read())
            {
                var item = new Class();
                item.Name = rd.GetString(0);
                results.Add(item);
            }
            conn.Close();
            return results;
        }
    }
}
