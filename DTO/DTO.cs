using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Management.DTO
{ 
    public class Teacher
    {
        public string name { get; set; }
    }


    public class Student
    {
        public int ID { get; set; }
        public int StudentID { get; set; }
        public string Name { get; set; }
        public char Gender { get; set; }
        public string Social_ID { get; set; }

        public string Class { get; set; }

        public string Courses { get; set; }

    }

    public class Account
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
    }

    public class Class
    {
        public string Name { get; set; }
    }
}

   


