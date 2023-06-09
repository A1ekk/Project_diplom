using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Project.AppData
{
    public class MessageDigest5
    {
        public static string hashing(string getString)
        {
            string result;
            MD5 md = MD5.Create();
            byte[] hash = md.ComputeHash(Encoding.ASCII.GetBytes(getString));
            result = BitConverter.ToString(hash).Replace("-", String.Empty);
            result = result.ToLower();
            return result;
        }
    }
}
