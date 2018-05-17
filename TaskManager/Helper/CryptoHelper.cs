using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace TaskManager.Helper
{
    public class CryptoHelper
    {
        public static string CryptoPassword(string password)
        {
            using(SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(password));
                var stringBuilder = new StringBuilder(hash.Length * 2);
                foreach(byte b in hash)
                {
                    stringBuilder.Append(b.ToString("X2"));
                }

                return stringBuilder.ToString();
            }
        }
    }
}