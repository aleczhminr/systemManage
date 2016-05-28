using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Enum
{
    public static class Task_JournalEnum
    {
        public enum Type : int
        {
            未知 = 0,
            签到 = 1,
            每日 = 2,
            微信 = 3,
            行业 = 4,
            地址 = 5,
            邮件 = 6,
            短信 = 7,
            QQ空间 = 8,
            腾讯微博 = 9,
            新浪微博 = 10,
            百科 = 11,
            知道 = 12,
            新手任务 = 13
        }
        public enum status : int
        {
            等待审核 = 0,
            处理完成 = 1,
            审核通过 = 2
        }
    }
}
