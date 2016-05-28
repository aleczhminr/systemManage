using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public abstract class RedisHelper
    {
        private static string _redisHost = System.Configuration.ConfigurationManager.AppSettings["RedisHost"];
        private static string _preName = "SYSTEM:";

        /// <summary>
        /// 设置String缓存信息
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expirSeconds"></param>
        public static void SetKey(string key, string value, int expireSeconds = 0)
        {
            try
            {
                using (var connection = ConnectionMultiplexer.Connect(_redisHost))
                {
                    IDatabase redis = connection.GetDatabase();

                    if (expireSeconds > 0)
                    {
                        TimeSpan ts = new TimeSpan(0, 0, expireSeconds);
                        redis.StringSet(_preName + key, value, ts);
                    }
                    else
                    {
                        redis.StringSet(_preName + key, value);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Redis_SetKey错误", ex);
            }
        }
        
        /// <summary>
        /// 获取String缓存信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetKey(string key)
        {
            string strResult = "";
            try
            {
                using (var connection = ConnectionMultiplexer.Connect(_redisHost))
                {
                    IDatabase redis = connection.GetDatabase();
                    strResult = redis.StringGet(_preName + key);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Redis_GetKey错误", ex);
            }

            return strResult;
        }
        
    }
}
