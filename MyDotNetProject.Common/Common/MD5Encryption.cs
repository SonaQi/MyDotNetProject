using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MyDotNetProject.Common.Common
{
    public static class MD5Encryption
    {

        /// <summary>
        /// MD5加密(UTF8编码)
        /// </summary>
        /// <param name="plaintext">明文</param>
        /// <returns></returns>
        public static string Encryption(string plaintext)
        {
            if (string.IsNullOrWhiteSpace(plaintext)) return plaintext;
            MD5CryptoServiceProvider service = new MD5CryptoServiceProvider();
            byte[] _bytes = Encoding.UTF8.GetBytes(plaintext);
            byte[] hashByte = service.ComputeHash(_bytes);
            string ciphertext = string.Join("", hashByte.Select(x => x.ToString("x2")));
            return ciphertext;
        }
    }
}
