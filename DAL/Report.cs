using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Student_Management.DTO;
using Student_Management.DAL;

namespace Student_Management.DAL
{
    public class Report 
    {
        public List<Student> GetStudents(string path)
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

                temp.StudentID = Convert.ToInt32(values[1]);
                temp.Name=(values[2]);
                temp.Gender=Convert.ToChar(values[3]);
                temp.Social_ID=(values[4]);
                result.Add(temp);

            }
            return result;
        }
    }
}
