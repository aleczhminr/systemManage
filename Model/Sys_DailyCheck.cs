using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 每日日检
    /// </summary>
    public class Sys_DailyCheck
    {

        public Sys_DailyCheck()
        {
            RemindTime = new DateTime(1990, 1, 1);
        }

        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 任务名称
        /// </summary>
        public string TaskName { get; set; }
        /// <summary>
        /// 是否重复任务 1-是 0-否
        /// </summary>
        public int IsRepeat { get; set; }
        /// <summary>
        /// 重复类型 1-一次 2-周重复 3-日期重复
        /// </summary>
        public int RepeatType { get; set; }
        /// <summary>
        /// 重复时间
        /// </summary>
        public string RepeatTime { get; set; }

        /// <summary>
        /// 提醒时间
        /// </summary>
        public DateTime RemindTime { get; set; }
        /// <summary>
        /// 提醒人
        /// </summary>
        public string Reminder { get; set; }
        /// <summary>
        /// 记录人
        /// </summary>
        public string Recorder { get; set; }
        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime RecordTime { get; set; }
        /// <summary>
        /// 任务状态
        /// </summary>
        public int TaskStatus { get; set; }
    }


    /// <summary>
    /// 日检基本信息
    /// </summary>
    public class Sys_DailyCheckBase
    {

        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 任务名称
        /// </summary>
        public string TaskName { get; set; }
        /// <summary>
        /// 是否重复任务 1-是 0-否
        /// </summary>
        public int IsRepeat { get; set; }
        /// <summary>
        /// 重复类型 1-一次 2-周重复 3-日期重复
        /// </summary>
        public int RepeatType { get; set; }
        /// <summary>
        /// 重复时间
        /// </summary>
        public string RepeatTime { get; set; }

        /// <summary>
        /// 提醒时间
        /// </summary>
        public DateTime RemindTime { get; set; }
        /// <summary>
        /// 提醒人
        /// </summary>
        public string Reminder { get; set; } 
    }
    public class RemindUsr
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
