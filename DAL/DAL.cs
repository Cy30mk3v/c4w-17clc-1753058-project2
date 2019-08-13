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

            Console.WriteLine(split[0]);
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
                temp.Class = split[0];
                result.Add(temp);

            }
            return result;
        }

        static public List<Course> GetCourse(string path)
        {
            List<Course> result = new List<Course>();
            StreamReader sr = new StreamReader(path);
            string line;
            line = sr.ReadLine();
            var split = line.Split(',');
            string Class = split[0];
            Console.WriteLine(split[0]);
            line = sr.ReadLine();
            int i = 1;
            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                var values = line.Split(',');
                Course temp = new Course();
                temp.ID = i;
                i++;
                temp.codeName = (values[1]);
                temp.FullName = (values[2]);
                temp.room = values[3];
                temp.Class = Class;
                result.Add(temp);
                Console.WriteLine(temp.codeName + " " + temp.FullName + " " + temp.room + " " + temp.Class);
            }
            return result;
        }


        static public void addCourseToDB(Course course)
        {
            List<Course> courses = new List<Course>();
            courses = getCourseFromDB();
            bool check = true;
            foreach (Course C in courses)
            {
                if (C.codeName == course.codeName)
                {
                    Console.WriteLine(C.codeName + " " + course.codeName);
                    break;
                }
            }
            if (check == false)
            {
                Console.WriteLine("Not good");
                return;
            }
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = "Provider=SQLNCLI11;Server=DESKTOP-SS8KMOM;Database=StudentManagement;Trusted_Connection=Yes;";
            conn.Open();
            string split1 = ",'";
            string s2 = "'";
            string split2 = s2.ToString();
            string split3 = split2 + split1;
            string pre = "INSERT INTO Course (codeName,FullName,Room,Class) VALUES (";
            string post = split2+course.codeName +split2+ ",N'" + course.FullName + split3 + course.room +  split3 + course.Class+split2+")";
            OleDbCommand cmd = new OleDbCommand(pre + post, conn);

            Console.WriteLine(pre + post);

            cmd.ExecuteNonQuery();
            conn.Close();
        }

        static public List<Course> getCourseFromDB()
        {
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = "Provider=SQLNCLI11;Server=DESKTOP-SS8KMOM;Database=StudentManagement;Trusted_Connection=Yes;";
            conn.Open();

            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM Course";

            OleDbDataReader rd = cmd.ExecuteReader();
            List<Course> accounts = new List<Course>();
            while (rd.Read())
            {
                var temp = new Course();
                temp.codeName = rd.GetString(1);
                temp.FullName = rd.GetString(2);
                //Console.WriteLine(rd.GetString(0));
                temp.room = rd.GetString(3);
                accounts.Add(temp);
            }
            conn.Close();


            return accounts;

        }
        static public void addClassToDB(string Class)
        {
            List<Class> classes = new List<Class>();
            classes = GetClassFromDB();
            foreach(Class C in classes)
            {
                if (C.Name == Class)
                    return;
            }
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = "Provider=SQLNCLI11;Server=DESKTOP-SS8KMOM;Database=StudentManagement;Trusted_Connection=Yes;";
            conn.Open();
            string s2 = "'";
            string split2 = s2.ToString();
            string pre = "INSERT INTO Class VALUES (";
            string post = split2 + Class + split2 + ")";
            OleDbCommand cmd = new OleDbCommand(pre + post, conn);

            //Console.WriteLine(pre + post);

            cmd.ExecuteNonQuery();
            conn.Close();
        }
        static public void addStudentToDB(Student S)
        {
            List<Student> students = new List<Student>();
            students = GetStudentFromDB();
            bool check = true;
            foreach(Student stu in students)
            {
                if (stu.StudentID == S.StudentID)
                {
                    //Console.WriteLine("a" + stu.StudentID.ToString() + "/" + S.StudentID.ToString());
                    check = false;
                    break;
                }
            }
            if(check==false)
            {
                //Console.WriteLine("Not good");
                return;
            }
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = "Provider=SQLNCLI11;Server=DESKTOP-SS8KMOM;Database=StudentManagement;Trusted_Connection=Yes;";
            conn.Open();
            string split1 = ",'";
            string s2 = "'";
            string split2 = s2.ToString();
            string split3 = split2 + split1;
            string pre =  "INSERT INTO Student (StudentID,Name,Gender,Social_ID,Class) VALUES (";
            string post = S.StudentID.ToString() + ",N'" +S.Name + split3 + S.Gender.ToString() + split3 + S.Social_ID + split3 + S.Class  + split2+")";
            OleDbCommand cmd = new OleDbCommand(pre + post, conn);

            //Console.WriteLine(pre + post);
           
            cmd.ExecuteNonQuery();
            conn.Close();

            //string insert = "INSERT INTO Grade(StudentID, CodeCourse) SELECT Student.StudentID,Course.codeName FROM Student,Course WHERE Student.Class = Course.Class GROUP BY codeName,Student.ID,Student.StudentID HAVING Student.ID = MAX(Student.ID)";

            string insert_1 = "INSERT INTO Grade(StudentID, CodeCourse,StudentName)\n";
            string insert_2 = "SELECT Student.StudentID,Course.codeName,Student.Name \n";
            string insert_3 = "FROM Student,Course\n";
            string insert_4 = "WHERE Student.Class = Course.Class AND NOT EXISTS(SELECT G.StudentID, G.CodeCourse ";

            string insert_5 = "FROM Grade G ";

            string insert_6 = "WHERE G.StudentID = Student.StudentID AND G.CodeCourse = Course.codeName)\n";
            string insert_7 = "GROUP BY codeName,Student.ID,Student.StudentID\n";
            string insert_8 = "HAVING Student.ID = MAX(Student.ID)";
            string insert = insert_1 + insert_2 + insert_3 + insert_4 + insert_5 + insert_6 + insert_7 + insert_8;
            conn.Open();
            OleDbCommand cmd1 = new OleDbCommand(insert, conn);
           
            cmd1.ExecuteNonQuery();
            conn.Close();
        }

        static public void addStudentToGradeList(int StudentID, string StudentName, string Code)
        {

            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = "Provider=SQLNCLI11;Server=DESKTOP-SS8KMOM;Database=StudentManagement;Trusted_Connection=Yes;";
            conn.Open();

            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = conn;
            cmd.CommandText = "INSERT INTO Grade(StudentID, CodeCourse,StudentName) VALUES (" + StudentID.ToString() + "," + "'" + Code + "','" + StudentName + "')";
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

        static public List<Student> GetStudentFromDB_Class(string Class)
        {
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = "Provider=SQLNCLI11;Server=DESKTOP-SS8KMOM;Database=StudentManagement;Trusted_Connection=Yes;";
            conn.Open();

            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select * from Student WHERE Class =" + "'" + Class + "'" ;

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
                item.Class = (rd.GetString(5));
                results.Add(item);
            }
            conn.Close();
            var sorted = results.OrderBy(q => q.ID).ToList();
            
            return sorted;
        }

        static public List<Course> GetCoursesOfStudent(int studentID)
        {
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = "Provider=SQLNCLI11;Server=DESKTOP-SS8KMOM;Database=StudentManagement;Trusted_Connection=Yes;";
            conn.Open();

            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM Grade WHERE StudentID=" + "'" + studentID.ToString() + "'";

            OleDbDataReader rd = cmd.ExecuteReader();
            List<Course> result = new List<Course>();
            while(rd.Read())
            {
                var item = new Course();
                item.codeName = rd.GetString(0);
                result.Add(item);
            }
            conn.Close();
            return result;
        }

        static public int GetStudentID(string name)
        {
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = "Provider=SQLNCLI11;Server=DESKTOP-SS8KMOM;Database=StudentManagement;Trusted_Connection=Yes;";
            conn.Open();

            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT StudentID FROM Student WHERE Name=" + "'" + name + "'";

            OleDbDataReader rd = cmd.ExecuteReader();
            int result = 0;
            while (rd.Read())
            {
                result = rd.GetInt32(0);
            }
            conn.Close();
            return result;
        }

        static public string GetCourseIDandClass(string name)
        {
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = "Provider=SQLNCLI11;Server=DESKTOP-SS8KMOM;Database=StudentManagement;Trusted_Connection=Yes;";
            conn.Open();

            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT codeName FROM Student WHERE Name=" + "'" + name + "'";

            OleDbDataReader rd = cmd.ExecuteReader();
            string result = null;
            while (rd.Read())
            {
                result = rd.GetString(0);
            }
            conn.Close();
            return result;
        }
        static public List<Course> GetCourseFromDB_Class(string Class)
        {
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = "Provider=SQLNCLI11;Server=DESKTOP-SS8KMOM;Database=StudentManagement;Trusted_Connection=Yes;";
            conn.Open();

            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select * from Course WHERE Class =" + "'" + Class + "'";

            OleDbDataReader rd = cmd.ExecuteReader();
            List<Course> results = new List<Course>();
            while (rd.Read())
            {
                var item = new Course();
                item.ID = rd.GetInt32(0);
                item.codeName = (rd.GetString(1));
                item.FullName = (rd.GetString(2));
                //Console.WriteLine(rd.GetString(3));
                item.room = rd.GetString(3);
                results.Add(item);
            }
            conn.Close();
            var sorted = results.OrderBy(q => q.ID).ToList();

            return sorted;
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
                item.Class = (rd.GetString(5));
                results.Add(item);
            }
            conn.Close();
            var sorted = results.OrderBy(q => q.ID).ToList();
            
            return sorted;
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


        static public List<Class> GetClassFromDB_Course()
        {
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = "Provider=SQLNCLI11;Server=DESKTOP-SS8KMOM;Database=StudentManagement;Trusted_Connection=Yes;";
            conn.Open();

            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select DISTINCT (Class) from Course";

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
