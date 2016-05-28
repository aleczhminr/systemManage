using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace OperationPlatform.HelperEx
{
    public class Class_Password
    {
        private const string KEY_64 = "GcG9wHw5";
        //注意了，是8个字符，64位  
        private const string IV_64 = "fXPk9y06";

        //  
        // TODO: 在此处添加构造函数逻辑  
        //  
        public Class_Password()
        {
        }

        #region EnCode 加密
        /// <summary>  
        /// EnCode 加密  
        /// </summary>  
        /// <param name="data">要加密的字符串</param>  
        /// <returns></returns>  

        public string EnCode(string data)
        {
            if (data == "")
            {
                return "";
            }
            else
            {
                byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(KEY_64);
                byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV_64);

                DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
                int i = cryptoProvider.KeySize;
                MemoryStream ms = new MemoryStream();

                CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateEncryptor(byKey, byIV), CryptoStreamMode.Write);

                StreamWriter sw = new StreamWriter(cst);
                sw.Write(data);
                sw.Flush();
                cst.FlushFinalBlock();
                sw.Flush();
                return Convert.ToBase64String(ms.GetBuffer(), 0, System.Convert.ToInt32(ms.Length));
            }

        }
        #endregion

        #region DeCode 解密
        /// <summary>  
        /// DeCode 解密  
        /// </summary>  
        /// <param name="data">要解密的字符串</param>  
        /// <returns></returns>  
        public string DeCode(string data)
        {

            if (data == "")
            {
                return "";
            }
            else
            {
                byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(KEY_64);
                byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV_64);

                byte[] byEnc = null;
                try
                {
                    byEnc = Convert.FromBase64String(data);
                }
                catch
                {
                    return null;
                }

                DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
                MemoryStream ms = new MemoryStream(byEnc);

                CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateDecryptor(byKey, byIV), CryptoStreamMode.Read);
                StreamReader sr = new StreamReader(cst);
                return sr.ReadToEnd();
            }


        }
        #endregion
    }
}