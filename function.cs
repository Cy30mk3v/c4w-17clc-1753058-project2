using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
 

namespace Student_Management
{
    public partial class Form1
    {
        

        public string hashPassword(string value)
        {
            SHA1 h = new SHA1CryptoServiceProvider();
            byte[] temp;
            temp = h.ComputeHash(Encoding.UTF8.GetBytes(value));
            StringBuilder sb = new StringBuilder();
            for(int i=0;i<temp.Length;i++)
            {
                sb.Append(temp[i].ToString("x2"));
            }
            return sb.ToString();
        }
        public bool checkPassWord()
        {
            //var hashed = new System.Security.Cryptography.HashAlgorithm();
            

            if(hashPassword(textBox2.Text)==hashPassword("123"))
            {
                return true;
            }
            return false;
        }

        public void connectDB()
        {
            
        }

       

        //public void 
    }
}
