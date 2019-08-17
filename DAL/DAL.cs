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
        /*Only for Course list*/
        static public void getClassAndCourse(string path, out string Class, out string Course)
        {
            List<Grade> result = new List<Grade>();
            StreamReader sr = new StreamReader(path);
            string line;
            line = sr.ReadLine();
            var split = line.Split(',');
            var split_sub = split[0].Split('-');

            Class = split_sub[0];
            Course = split_sub[1];
            
        }
        static public List<Student> GetStudents(string path)
        {
            List<Student> result = new List<Student>();
            StreamReader sr = new StreamReader(path);
            string line;
            line = sr.ReadLine();
            var split = line.Split(',');

            //Console.WriteLine(split[0]);
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
           //Console.WriteLine(split[0]);
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
                //Console.WriteLine(temp.codeName + " " + temp.FullName + " " + temp.room + " " + temp.Class);
            }
            return result;
        }

        static public string getCourseName (string code)
        {
            string name = null;
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = "Provider=SQLNCLI11;Server=DESKTOP-SS8KMOM;Database=StudentManagement;Trusted_Connection=Yes;";
            conn.Open();

            OleDbCommand cmd = conn.CreateCommand();
           
          
            cmd.CommandText = "SELECT Course.FullName FROM Course WHERE Course.codeName =?";
            OleDbParameter p1 = new OleDbParameter();
            cmd.Parameters.Add(p1);
            p1.Value = code;
            
            

            OleDbDataReader rd = cmd.ExecuteReader();
           
        
            while (rd.Read())
            {
                name = rd.GetString(0);
            }
            cmd.Parameters.Clear();
            conn.Close();


            return name;

        }

        static public string getStudentName(string studentID)
        {
            string name = null;
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = "Provider=SQLNCLI11;Server=DESKTOP-SS8KMOM;Database=StudentManagement;Trusted_Connection=Yes;";
            conn.Open();

            OleDbCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT Name FROM Student WHERE StudentID =?";

            OleDbParameter p1 = new OleDbParameter();
            cmd.Parameters.Add(p1);
            p1.Value = studentID;
            OleDbDataReader rd = cmd.ExecuteReader();


            while (rd.Read())
            {
                name = rd.GetString(0);
            }
            conn.Close();


            return name;

        }
        static public List<Grade> GetListCourse_Class_List(string path)
        {
            List<Grade> result = new List<Grade>();
            StreamReader sr = new StreamReader(path);
            string line;
            line = sr.ReadLine();
            var split = line.Split(',');
            var split_sub = split[0].Split('-');

            string Class = split_sub[0];
            string Course = split_sub[1];
            string Main_Class = Class.Substring(0, 2);
            //Console.WriteLine(Main_Class);
            line = sr.ReadLine();
            int i = 1;
            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                var values = line.Split(',');
                Grade temp = new Grade();
                temp.ID = i;
                i++;
                temp.StudentID = Convert.ToInt32(values[1]);
                temp.CodeCourse = Course;
                temp.StudentName = values[2];
                if (Main_Class != temp.StudentID.ToString().Substring(0, 2))
                {
                    temp.Sub_Class = Class;
                    temp.Main_Class = "NONE";
                    //Console.WriteLine("Sub");
                }
                else
                {
                    temp.Main_Class = Class;
                    temp.Sub_Class = "NONE";
                }
                //Console.WriteLine(temp.StudentID.ToString() + "/" + temp.CodeCourse + "/" + temp.Main_Class + "/" + temp.Sub_Class);
                result.Add(temp);
                // Console.WriteLine(temp.codeName + " " + temp.FullName + " " + temp.room + " " + temp.Class);
            }
            return result;
        }

        static public void removeStudentFromCourse(int studentID, string code, string Class)
        {
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = "Provider=SQLNCLI11;Server=DESKTOP-SS8KMOM;Database=StudentManagement;Trusted_Connection=Yes;";
            conn.Open();

            OleDbCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;

            OleDbParameter p1 = new OleDbParameter();
            cmd.Parameters.Add(p1);
            p1.Value = studentID;
            OleDbParameter p2 = new OleDbParameter();
            cmd.Parameters.Add(p2);
            p2.Value = code;
            OleDbParameter p3 = new OleDbParameter();
            cmd.Parameters.Add(p3);
            p3.Value = Class;
            OleDbParameter p4 = new OleDbParameter();
            cmd.Parameters.Add(p4);
            p4.Value = Class;
            cmd.CommandText = "DELETE * FROM Course WHERE StudentID=? AND CodeCourse = ? AND (Sub_Class = ? OR Class= ? )";
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        static public void addAccount(string StudentID, string birthday)
        {
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = "Provider=SQLNCLI11;Server=DESKTOP-SS8KMOM;Database=StudentManagement;Trusted_Connection=Yes;";
            conn.Open();

            OleDbCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;

            OleDbParameter p1 = new OleDbParameter();
            cmd.Parameters.Add(p1);
            p1.Value = StudentID;

            OleDbParameter p2 = new OleDbParameter();
            cmd.Parameters.Add(p2);
            p2.Value = birthday;
            Console.WriteLine(p1.Value.ToString() +  "/" + p2.Value.ToString());
            cmd.CommandText = "INSERT INTO Accounts (UserName,PassWord) values (?,?)";
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        static public List<Grade> GetGradesFromDB_CCL()
        {
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = "Provider=SQLNCLI11;Server=DESKTOP-SS8KMOM;Database=StudentManagement;Trusted_Connection=Yes;";
            conn.Open();

            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM Grade";

            OleDbDataReader rd = cmd.ExecuteReader();
            List<Grade> grades = new List<Grade>();
            int i = 1;
            while (rd.Read())
            {
                var temp = new Grade();

                temp.StudentID = rd.GetInt32(1);
                temp.StudentName = rd.GetString(2);
                temp.CodeCourse = rd.GetString(3);


                temp.Main_Class = rd.GetString(8);
                temp.Sub_Class = rd.GetString(9);
                grades.Add(temp);
            }
            conn.Close();


            return grades;
        }

        static public void addCourse_Class_List_EToDB(Grade G)
        {
            List<Grade> grades = new List<Grade>();
            grades = GetGradesFromDB_CCL();
            foreach (Grade grade in grades)
            {
                if (grade.StudentID == G.StudentID && (grade.Sub_Class == G.Sub_Class || grade.Main_Class == G.Main_Class) && G.CodeCourse==grade.CodeCourse)
                {
                    return;
                }
            }
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = "Provider=SQLNCLI11;Server=DESKTOP-SS8KMOM;Database=StudentManagement;Trusted_Connection=Yes;";
            conn.Open();
            OleDbCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            string Class;
            OleDbParameter p1 = new OleDbParameter();
            cmd.Parameters.Add(p1);
            p1.Value = G.StudentID;

            OleDbParameter p2 = new OleDbParameter();
            cmd.Parameters.Add(p2);
            p2.Value = G.StudentName;

            OleDbParameter p3 = new OleDbParameter();
            cmd.Parameters.Add(p3);
            p3.Value = G.CodeCourse;

            OleDbParameter p4 = new OleDbParameter();
            cmd.Parameters.Add(p4);
            
            if (G.Main_Class != "NONE")
            {
                p4.Value = G.Main_Class;

                cmd.CommandText = "INSERT INTO Grade (StudentID,StudentName,CodeCourse,Class) VALUES(?,?,?,?)";
                Class = G.Main_Class;
            }
            else
            {
                p4.Value = G.Sub_Class;
                cmd.CommandText = "INSERT INTO Grade (StudentID,StudentName,CodeCourse,Sub_Class) VALUES(?,?,?,?)";
                Class = G.Sub_Class;
            }
            //Console.WriteLine(G.StudentName + "aaaaaaaa");
            

           // Console.WriteLine(pre + post);

            cmd.ExecuteNonQuery();
            conn.Close();

        }

        static public void changePassword(string username, string newPass)
        {
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = "Provider=SQLNCLI11;Server=DESKTOP-SS8KMOM;Database=StudentManagement;Trusted_Connection=Yes;";
            conn.Open();

            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = conn;
            OleDbParameter p1 = new OleDbParameter();
            cmd.Parameters.Add(p1);
            p1.Value = newPass;

            OleDbParameter p2 = new OleDbParameter();
            cmd.Parameters.Add(p2);
            p2.Value = username;
            cmd.CommandText = "UPDATE Accounts SET PassWord=? WHERE UserName= ?";
            Console.WriteLine(cmd.CommandText);
            //Console.WriteLine(pre + post);

            cmd.ExecuteNonQuery();

            conn.Close();
        }

        static public void updateGrade(string studentID, string Course, float P1, float P2, float P3, float P4)
        {
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = "Provider=SQLNCLI11;Server=DESKTOP-SS8KMOM;Database=StudentManagement;Trusted_Connection=Yes;";
            conn.Open();

            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = conn;
           

            OleDbParameter p3 = new OleDbParameter();
            cmd.Parameters.Add(p3);
            p3.Value = P1;

            OleDbParameter p4 = new OleDbParameter();
            cmd.Parameters.Add(p4);
            p4.Value = P2;


            OleDbParameter p5 = new OleDbParameter();
            cmd.Parameters.Add(p5);
            p5.Value = P3;

            OleDbParameter p6 = new OleDbParameter();
            cmd.Parameters.Add(p6);
            p6.Value = P4;

            OleDbParameter p1 = new OleDbParameter();
            cmd.Parameters.Add(p1);
            p1.Value = studentID;

            OleDbParameter p2 = new OleDbParameter();
            cmd.Parameters.Add(p2);
            p2.Value = Course;
            cmd.CommandText = "UPDATE Grade SET Mid_Term=?,Final_Term=?,Other_Grade=?,Sum_Grade=? WHERE StudentID=? AND CodeCourse=?";

            //Console.WriteLine(cmd.CommandText);
            //Console.WriteLine(pre + post);

            cmd.ExecuteNonQuery();

            conn.Close();
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
                    //Console.WriteLine(C.codeName + " " + course.codeName);
                    break;
                }
            }
            if (check == false)
            {
                //Console.WriteLine("Not good");
                return;
            }
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = "Provider=SQLNCLI11;Server=DESKTOP-SS8KMOM;Database=StudentManagement;Trusted_Connection=Yes;";
            conn.Open();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = conn;
            OleDbParameter p1 = new OleDbParameter();
            cmd.Parameters.Add(p1);
            p1.Value = course.codeName;

            
            OleDbParameter p2 = new OleDbParameter();
            cmd.Parameters.Add(p2);
            p2.Value = course.FullName;

            OleDbParameter p3 = new OleDbParameter();
            cmd.Parameters.Add(p3);
            p3.Value = course.room;

           
            OleDbParameter p4 = new OleDbParameter();
            cmd.Parameters.Add(p4);
            p1.Value = course.Class;




            cmd.CommandText = "INSERT INTO Course (codeName,FullName,Room,Class) VALUES (?,?,?,?)";
           
          

            //Console.WriteLine(pre + post);

            cmd.ExecuteNonQuery();
            conn.Close();

            OleDbConnection conn1 = new OleDbConnection();
            conn1.ConnectionString = "Provider=SQLNCLI11;Server=DESKTOP-SS8KMOM;Database=StudentManagement;Trusted_Connection=Yes;";
            conn1.Open();
            string insert_1 = "INSERT INTO Grade(StudentID, CodeCourse,StudentName,Class)\n";
            string insert_2 = "SELECT Student.StudentID,Course.codeName,Student.Name,Student.Class \n";
            string insert_3 = "FROM Student,Course\n";
            string insert_4 = "WHERE Student.Class = Course.Class AND NOT EXISTS(SELECT G.StudentID, G.CodeCourse ";

            string insert_5 = "FROM Grade G ";

            string insert_6 = "WHERE G.StudentID = Student.StudentID AND G.CodeCourse = Course.codeName)\n";
            string insert_7 = "GROUP BY codeName,Student.Name,Student.StudentID,Student.ID,Student.Class\n";
            string insert_8 = "HAVING Student.ID = MAX(Student.ID)";
            string insert = insert_1 + insert_2 + insert_3 + insert_4 + insert_5 + insert_6 + insert_7 + insert_8;

            OleDbCommand cmd1 = new OleDbCommand(insert, conn1);

            cmd1.ExecuteNonQuery();
            conn1.Close();
        }

       static public void DeleteGradeL(string Class, string Course)
        {
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = "Provider=SQLNCLI11;Server=DESKTOP-SS8KMOM;Database=StudentManagement;Trusted_Connection=Yes;";
            conn.Open();

            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = conn;
            OleDbParameter p1 = new OleDbParameter();
            cmd.Parameters.Add(p1);
            p1.Value = Class;

          
            OleDbParameter p2 = new OleDbParameter();
            cmd.Parameters.Add(p2);
            p2.Value = Course;


            cmd.CommandText = "DELETE FROM Grade WHERE Grade.Class=? AND Grade.CodeCourse=?";


            //Console.WriteLine(pre + post);

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
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = conn;
            OleDbParameter p1 = new OleDbParameter();
            cmd.Parameters.Add(p1);
            p1.Value = Class;

            cmd.CommandText = "INSERT INTO Class VALUES (?)";
            
        

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
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = conn;
            OleDbParameter p1 = new OleDbParameter();
            cmd.Parameters.Add(p1);
            p1.Value = S.Name;

            
            OleDbParameter p2 = new OleDbParameter();
            cmd.Parameters.Add(p2);
            p2.Value = S.Gender;

            
            OleDbParameter p3 = new OleDbParameter();
            cmd.Parameters.Add(p3);
            p3.Value = S.Social_ID;

            
            OleDbParameter p4 = new OleDbParameter();
            cmd.Parameters.Add(p4);
            p4.Value = S.Class;
            cmd.CommandText =  "INSERT INTO Student (StudentID,Name,Gender,Social_ID,Class) VALUES (?,?,?,?)";
            
          

            //Console.WriteLine(pre + post);
           
            cmd.ExecuteNonQuery();
            conn.Close();

            //string insert = "INSERT INTO Grade(StudentID, CodeCourse) SELECT Student.StudentID,Course.codeName FROM Student,Course WHERE Student.Class = Course.Class GROUP BY codeName,Student.ID,Student.StudentID HAVING Student.ID = MAX(Student.ID)";
            
            
        }

        static public void addStudentToGradeList(int StudentID, string StudentName, string Code)
        {

            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = "Provider=SQLNCLI11;Server=DESKTOP-SS8KMOM;Database=StudentManagement;Trusted_Connection=Yes;";
            conn.Open();

            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = conn;
            OleDbParameter p1 = new OleDbParameter();
            cmd.Parameters.Add(p1);
            p1.Value = StudentID;
       
            OleDbParameter p2 = new OleDbParameter();
            cmd.Parameters.Add(p2);
            p2.Value = Code;

          
            OleDbParameter p3 = new OleDbParameter();
            cmd.Parameters.Add(p3);
            p1.Value = StudentName;
            cmd.CommandText = "INSERT INTO Grade(StudentID, CodeCourse,StudentName) VALUES (?,?,?)";
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
                //Console.WriteLine(rd.GetString(0));
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
            OleDbParameter p1 = new OleDbParameter();
            cmd.Parameters.Add(p1);
            p1.Value = Class;
            cmd.CommandText = "select * from Student WHERE Class =?" ;

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

       

        static public int GetStudentID(string name)
        {
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = "Provider=SQLNCLI11;Server=DESKTOP-SS8KMOM;Database=StudentManagement;Trusted_Connection=Yes;";
            conn.Open();

            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = conn;
            OleDbParameter p1 = new OleDbParameter();
            cmd.Parameters.Add(p1);
            p1.Value = name;
            cmd.CommandText = "SELECT StudentID FROM Student WHERE Name=?";

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
            OleDbParameter p1 = new OleDbParameter();
            cmd.Parameters.Add(p1);
            p1.Value = name;
            cmd.CommandText = "SELECT codeName FROM Student WHERE Name=?";

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
            OleDbParameter p1 = new OleDbParameter();
            cmd.Parameters.Add(p1);
            p1.Value = Class;
            cmd.CommandText = "select * from Course WHERE Class =?";

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

        static public List<Class> GetClassesforCCL_DB(string Class)
        {
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = "Provider=SQLNCLI11;Server=DESKTOP-SS8KMOM;Database=StudentManagement;Trusted_Connection=Yes;";
            conn.Open();

            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = conn;
            OleDbParameter p1 = new OleDbParameter();
            cmd.Parameters.Add(p1);
            p1.Value = Class;
            cmd.CommandText = "select DISTINCT G.CodeCourse from Grade G WHERE G.Sub_Class =? OR G.Class=?";

            OleDbDataReader rd = cmd.ExecuteReader();
            List<Class> results = new List<Class>();
            while (rd.Read())
            {
                var item = new Class();
                item.Name = rd.GetString(0);
                results.Add(item);
            }
            conn.Close();
            var sorted = results.OrderBy(q => q.Name).ToList();

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

        static public List<Grade> GetGrades(string path)
        {
            List<Grade> result = new List<Grade>();
            StreamReader sr = new StreamReader(path);
            string line;
            line = sr.ReadLine();
            var split = line.Split(',');
            var split_sub = split[0].Split('-');

            string Class = split_sub[0];
            string Course = split_sub[1];
            string Main_Class = Class.Substring(0, 2);
            //Console.WriteLine(Main_Class);
            line = sr.ReadLine();
            int i = 1;
            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                var values = line.Split(',');
                Grade temp = new Grade();
                temp.ID = i;
                i++;
                temp.StudentID = Convert.ToInt32(values[1]);
                temp.CodeCourse = Course;
                temp.StudentName = values[2];
                temp.Mid_Term = float.Parse(values[3]);
                temp.Final_Term = float.Parse(values[4]);
                temp.Other_grade = float.Parse(values[5]);
                temp.Sum_Grade = float.Parse(values[6]);
                if (Main_Class != temp.StudentID.ToString().Substring(0, 2))
                {
                    temp.Sub_Class = Class;
                    temp.Main_Class = "NONE";
                    //Console.WriteLine("Sub");
                }
                else
                {
                    temp.Main_Class = Class;
                    temp.Sub_Class = "NONE";
                }
                //Console.WriteLine(temp.StudentID.ToString() + "/" + temp.CodeCourse + "/" + temp.Main_Class + "/" + temp.Sub_Class);
                result.Add(temp);
                // Console.WriteLine(temp.codeName + " " + temp.FullName + " " + temp.room + " " + temp.Class);
            }
            return result;
        }

        static public void addGradeToDB(Grade G)
        {
            List<Grade> grades = new List<Grade>();
            grades = GetGradesFromDB_CCL();
            bool check = false;
            foreach (Grade grade in grades)
            {
                if(G.StudentID==grade.StudentID)
                {
                    if(G.Sub_Class=="NONE")
                    {
                        if (G.Main_Class == grade.Main_Class)
                            check = true;
                    }
                    if(G.Main_Class=="NONE")
                    {
                        if (G.Sub_Class == grade.Sub_Class)
                            check = true;
                    }
                }
            }
            if(check==false)
            {
                return;
            }
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = "Provider=SQLNCLI11;Server=DESKTOP-SS8KMOM;Database=StudentManagement;Trusted_Connection=Yes;";
            conn.Open();

            
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = conn;
            OleDbParameter p1 = new OleDbParameter();
            cmd.Parameters.Add(p1);
            p1.Value = G.Mid_Term;

          
            OleDbParameter p2 = new OleDbParameter();
            cmd.Parameters.Add(p2);
            p2.Value = G.Final_Term;

            OleDbParameter p3 = new OleDbParameter();
            cmd.Parameters.Add(p3);
            p3.Value = G.Other_grade;

           
            OleDbParameter p4 = new OleDbParameter();
            cmd.Parameters.Add(p4);
            p4.Value = G.Sum_Grade;

            
            OleDbParameter p5 = new OleDbParameter();
            cmd.Parameters.Add(p5);
            p5.Value = G.StudentID;

            
            OleDbParameter p6 = new OleDbParameter();
            cmd.Parameters.Add(p6);
            p6.Value = G.Main_Class;

           
            OleDbParameter p7 = new OleDbParameter();
            cmd.Parameters.Add(p7);
            p7.Value = G.Sub_Class;
            cmd.CommandText = "UPDATE Grade SET Mid_Term=?,Final_Term=?,Other_Grade=?,Sum_Grade=? WHERE Grade.StudentID =? AND (Grade.Class=?OR Grade.Sub_Class=?)";
         
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        static public List<Grade> GetGradesFromDB(string Class, string course_code)
        {
            List<Grade> results = new List<Grade>();
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = "Provider=SQLNCLI11;Server=DESKTOP-SS8KMOM;Database=StudentManagement;Trusted_Connection=Yes;";
            conn.Open();

            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = conn;
            OleDbParameter p1 = new OleDbParameter();
            cmd.Parameters.Add(p1);
            p1.Value = course_code;
           
            OleDbParameter p2 = new OleDbParameter();
            cmd.Parameters.Add(p2);
            p2.Value = Class;
            cmd.CommandText = "SELECT * FROM  Grade WHERE Grade.CodeCourse=? AND (Grade.Class=? OR Grade.Sub_Class=?)";
            //Console.WriteLine(cmd.CommandText);
            OleDbDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                var item = new Grade();
                item.StudentID = rd.GetInt32(1);
                item.StudentName = rd.GetString(2);
                item.Mid_Term = (float)rd.GetDouble(4);
                item.Final_Term = (float)rd.GetDouble(5);
                item.Other_grade = (float)rd.GetDouble(6);
                item.Sum_Grade = (float)rd.GetDouble(7);
                results.Add(item);
            }
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


        static public List<Student> GetStudentsFromCCL_DB(string code, string Class)
        {
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = "Provider=SQLNCLI11;Server=DESKTOP-SS8KMOM;Database=StudentManagement;Trusted_Connection=Yes;";
            conn.Open();

            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = conn;
            OleDbParameter p1 = new OleDbParameter();
            cmd.Parameters.Add(p1);
            p1.Value = Class;

            OleDbParameter p2 = new OleDbParameter();
            cmd.Parameters.Add(p2);
            p2.Value = Class;

            OleDbParameter p3 = new OleDbParameter();
            cmd.Parameters.Add(p3);
            p3.Value = code;

            cmd.CommandText = "SELECT S.* FROM Student S, Grade G WHERE S.StudentID = G.StudentID AND (G.Sub_Class =? OR G.Class=?) AND G.CodeCourse=?";
            //Console.WriteLine(cmd.CommandText);
            OleDbDataReader rd = cmd.ExecuteReader();
            List<Student> results = new List<Student>();
            while (rd.Read())
            {
                var item = new Student();
                item.StudentID = rd.GetInt32(1);
                item.Name = rd.GetString(2);
                item.Gender = Convert.ToChar(rd.GetString(3));
                item.Social_ID = rd.GetString(4);
                results.Add(item);
            }
            conn.Close();
            return results;
        }

        static public List<Grade> getStudentGrade(int studentID)
        {
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = "Provider=SQLNCLI11;Server=DESKTOP-SS8KMOM;Database=StudentManagement;Trusted_Connection=Yes;";
            conn.Open();

            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = conn;
            OleDbParameter p1 = new OleDbParameter();
            cmd.Parameters.Add(p1);
            p1.Value = studentID;

            cmd.CommandText = "SELECT G.* FROM Grade G WHERE G.StudentID =?";
            //Console.WriteLine(cmd.CommandText);
            OleDbDataReader rd = cmd.ExecuteReader();
            List<Grade> results = new List<Grade>();
            while (rd.Read())
            {
                var item = new Grade();
                item.StudentID = rd.GetInt32(1);
                item.StudentName = rd.GetString(2);
                item.CodeCourse = rd.GetString(3);
                item.Mid_Term = (float)rd.GetDouble(4);
                item.Final_Term = (float)rd.GetDouble(5);
                item.Other_grade = (float)rd.GetDouble(6);
                item.Sum_Grade = (float)rd.GetDouble(7);
                results.Add(item);
            }
            conn.Close();
            return results;
        }
    }
}
