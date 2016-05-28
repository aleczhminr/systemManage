using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Enum
{
    /// <summary>
    /// 店铺信息
    /// </summary>
    public static class AccountEnum
    {
        /// <summary>
        /// 版本
        /// </summary>
        public enum StoreVer : int
        {
            免费 = 0,
            免费版 = 1,
            标准版 = 2,
            高级版 = 3,
            专家版 = 4,
            行业版 = 5
        }

        /// <summary>
        /// 活跃状态
        /// </summary>
        public enum ActiveStatus : int
        {
            休眠 = -1,
            流失 = -3,
            新注册 = 1,
            需关怀 = 3,
            流失需关怀 = 4,
            活跃 = 5,
            忠诚 = 7
        }

        public enum LoginMode : int
        {
            Web网页密码登录 = 0,
            Web网页Token登录 = 1,
            PC客户端 = 3,
            移动端 = 4
        }

        public static string GetSourceTagName(int tagId)
        {
            string list = "";

            switch (tagId)
            {
                case 21:
                    list = "IOS";
                    break;
                case 22:
                    list = "PC";
                    break;
                case 23:
                    list = "SEM";
                    break;
                case 24:
                    list = "WEB";
                    break;
                case 30:
                    list = "Android";
                    break;
                case 457:
                    list = "iPad";
                    break;
                default:
                    list = "WEB";
                    break;
            }
            return list;
        }

        public static string GetSourceName(int remark)
        {
            string list = "";

            switch (remark)
            {
                case 10:
                    list = "IOS";
                    break;
                case 8:
                    list = "PC";
                    break;
                case 23:
                    list = "SEM";
                    break;
                case 0:
                    list = "WEB";
                    break;
                case 11:
                    list = "Android";
                    break;
                case 13:
                    list = "iPad";
                    break;
                default:
                    list = "WEB";
                    break;
            }
            return list;
        }
    }
}
