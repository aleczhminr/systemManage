using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace OperationPlatform.HelperEx
{
    public class mmd5
    {
        // Hash an input string and return the hash as
        // a 32 character hexadecimal string.
        public static string getMd5(string input)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            int i = 0;
            for (i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        public string getMd5Hash(string input)
        {
            // Create a new instance of the MD5 object.
            MD5 md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            int i = 0;
            for (i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();

        }


        // Verify a hash against a string.
        public bool verifyMd5Hash(string input, string hash)
        {
            // Hash the input.
            string hashOfInput = getMd5Hash(input);

            // Create a StringComparer an comare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public string sess(string s, int wes)
        {
            string[] str = s.Split(new char[] { '*' });
            return str[wes];
        }

        //encrypt my string

        public static string MyEncrypt(string OriginStr)
        {

            Class_Password myEncryptionClass = new Class_Password();

            return myEncryptionClass.EnCode(OriginStr);

        }

        //unencrypt mystring

        public static string MyDecrypt(string NewString)
        {

            Class_Password myEncryptionClass = new Class_Password();

            return myEncryptionClass.DeCode(NewString);

        }
    }
}