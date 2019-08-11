using System;
using System.Security.Cryptography;
using System.Text;
using Student_Management.DTO;
using Student_Management.DAL;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Student_Management.BS
{
    public class Business
    {
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

        public bool checkStudentInDB(int StudentID)
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
    }
}
