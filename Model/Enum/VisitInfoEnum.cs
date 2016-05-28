using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Enum
{
    public static class VisitInfoEnum
    {
        /// <summary>
        /// 记录类别
        /// </summary>
        public enum RecordType : int
        {
            客服回访=1,
            用户来电=2,
            系统回访=3
        }
        /// <summary>
        /// 回访方式
        /// </summary>
        public enum VisitManner : int
        {
            电话 = 1,
            QQ = 2,
            邮件 = 3,
            微信 = 4,
            短信 = 5,
            论坛 = 7,
            消息中心=8
        }
        /// <summary>
        /// 处理状态
        /// </summary>
        public enum HandleStat:int
        {
            正常=0,
            处理完成=1,
            回访失败=2,
            继续回访=4
        }
    }
}
