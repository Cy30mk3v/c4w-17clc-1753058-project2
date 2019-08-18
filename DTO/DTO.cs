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

        public string birthday { get; set; }

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

    public class Course
    {
        public int ID { get; set; }

        public string codeName { get; set; }
        public string FullName { get; set; }
        public string room { get; set; }
        public string Class { get; set; }
    }

    public class Grade
    {

        public int ID { get; set; }
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public string CodeCourse{ get; set; }
        public float Mid_Term { get; set; }
        public float Final_Term { get; set; }
        public float Other_grade { get; set; }
        public float Sum_Grade { get; set; }

        public string Sub_Class { get; set; }

        public string Main_Class { get; set; }
    }
}

   


