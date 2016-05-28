using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 活跃率相关Model
    /// </summary>
    public class ActiveStatus
    {
        //S_Date [st],accountNum [an],NewAccNum [na],
        //unknownNum [un],reg_Attention [ra],
        //Attention [atten] ,loginNum [ln],
        //registration [rs],activeNum [acn],
        //faithfulNum [fn],dormancyNum [dn],
        //outflowNum [on],CONVERT(varchar(100),S_Date, 23) [time]
        public ActiveStatus()
        {
            Time = new DateTime();
        }
        
        /// <summary>
        /// 显示日期
        /// </summary>
        public DateTime ShowDate { get; set; }
        /// <summary>
        /// 平台用户数
        /// </summary>
        public int AllUsr { get; set; }
        /// <summary>
        /// 新注册用户
        /// </summary>
        public int NewReg { get; set; }
        /// <summary>
        /// 未分类用户
        /// </summary>
        public int UnknownUsr { get; set; }
        /// <summary>
        /// 需关怀
        /// </summary>
        public int RegAttention { get; set; }
        /// <summary>
        /// 流失需关怀
        /// </summary>
        public int Attention { get; set; }
        /// <summary>
        /// 登录用户数
        /// </summary>
        public int LoginUsr { get; set; }
        /// <summary>
        /// 注册人数
        /// </summary>
        public int RegUsr { get; set; }
        /// <summary>
        /// 活跃用户数
        /// </summary>
        public int ActiveUsr { get; set; }
        /// <summary>
        /// 忠诚用户数
        /// </summary>
        public int FaithUsr { get; set; }
        /// <summary>
        /// 休眠人数
        /// </summary>
        public int SleepUsr { get; set; }
        /// <summary>
        /// 流失用户数
        /// </summary>
        public int OutUsr { get; set; }
        /// <summary>
        /// 统计日期时间
        /// </summary>
        public DateTime Time { get; set; }
    }

    public class ActiveModel
    {
        /// <summary>
        /// 活跃状态
        /// </summary>
        public int active { get; set; }
        /// <summary>
        /// 活跃状态人数
        /// </summary>
        public int cnt { get; set; }
    }
}
