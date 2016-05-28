using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OperationPlatform.HelperEx
{
    public class RandomSeed
    {
        /// <summary>
        /// 产生随机数因子，防止短时间内产生重复的随机数
        /// </summary>
        /// <returns></returns>
        public static int GetRandomSeed()
        {
            byte[] bytes = new byte[4];
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }

    }
}