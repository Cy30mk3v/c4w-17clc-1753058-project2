using System;
using System.Security.Cryptography;
using System.Text;
using Student_Management.DTO;
using Student_Management.DAL;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;



namespace Student_Management.BS
{
    
    public class Business
    {
        public const int Student_list = 1;
        public const int Time_table_list = 2;
        public const int Class_Course_list = 3;
        public const int Grade_list = 4;
            public string hashPassword(string value)
        {
            SHA1 h = new SHA1CryptoServiceProvider();
            byte[] temp;
            temp = h.ComputeHash(Encoding.UTF8.GetBytes(value));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < temp.Length; i++)
            {
                sb.Append(temp[i].ToString("x2"));
            }
            return sb.ToString();
        }

        static public bool checkPassword(string username, string password)
        {
            List<Account> accounts = new List<Account>();
            Report r = new Report();
            //Console.WriteLine("aa");
            accounts = Report.GetAccountsFromDB();
            Business b = new Business();
            foreach (var account in accounts)
            {
                //b.hashPassword(
                Console.WriteLine(account.UserName + " " + account.PassWord);
                if (username == account.UserName && password == account.PassWord)
                    return true;
            }
            return false;
        }

        static public bool checkStudentInDB(int StudentID)
        {
            List<Student> students = new List<Student>();
            students = Report.GetStudentFromDB();
            foreach (Student S in students)
            {
                if (StudentID == S.StudentID)
                    return true;
            }
            return false;
        }

        static public int checkCSV(string path)
        {
            StreamReader sr = new StreamReader(path);
            string line;
            line = sr.ReadLine();
            var split = line.Split(',');
            line = sr.ReadLine();
            Console.WriteLine(split.Count().ToString());
            if (split[0].Contains("-"))
            {
                if (split.Count() == 7)
                {
                    return Grade_list;
                }
                else
                {
                    return Class_Course_list;
                }
            }
            else
            {
                if (split.Count() == 5)
                {
                    return Student_list;
                }
                else
                {
                    return Time_table_list;
                }
            }
        }

        public bool checkClassInDB(string Class)
        {
            List<Class> classes = new List<Class>();
            classes = Report.GetClassFromDB();
            foreach (Class c in classes)
            {
                if (c.Name == Class)
                    return true;
            }
            return false;
        }

        public bool checkStudentAlreadySignCourse()
        {


            return true;
        }


    }
}
