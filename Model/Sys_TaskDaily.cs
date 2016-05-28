using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //Sys_TaskDaily
    /// <summary>
    /// Sys_TaskDaily
    /// </summary>	
    [Serializable]
    public partial class Sys_TaskDaily
    {

        /// <summary>
        /// id
        /// </summary>		
        public int id { get; set; }
        /// <summary>
        /// 店铺ID
        /// </summary>		
        public int accountid { get; set; }
        /// <summary>
        /// 内容
        /// </summary>		
        public string t_mk { get; set; }
        /// <summary>
        /// 回访方式
        /// </summary>		
        public int vm_id { get; set; }
        /// <summary>
        /// 任务操时 时间
        /// </summary>		
        public DateTime dt_Time { get; set; }
        /// <summary>
        /// 任务等级
        /// </summary>		
        public int dt_Level { get; set; }
        /// <summary>
        /// 任务状态
        /// </summary>		
        public int dt_Status { get; set; }
        /// <summary>
        /// 处理人
        /// </summary>		
        public string dt_Name { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>		
        public DateTime inertTime { get; set; }
        /// <summary>
        /// 录入人
        /// </summary>		
        public string insetName { get; set; }
        /// <summary>
        /// 回访备注
        /// </summary>		
        public string dt_remark { get; set; }
        /// <summary>
        /// 记录的登录账号ID
        /// </summary>		
        public int dt_logUid { get; set; }
        /// <summary>
        /// 来源
        /// </summary>		
        public string dt_Source { get; set; }

    }


    public partial class Sys_TaskDailyInfo
    {
        /// <summary>
        /// id
        /// </summary>		
        public int id { get; set; }
        /// <summary>
        /// 店铺ID
        /// </summary>		
        public int accountid { get; set; }
        /// <summary>
        /// 店铺名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 内容
        /// </summary>		
        public string t_mk { get; set; }
        /// <summary>
        /// 回访方式
        /// </summary>		
        public int vm_id { get; set; }
        /// <summary>
        /// 任务操时 时间
        /// </summary>		
        public DateTime dt_Time { get; set; }
        /// <summary>
        /// 任务等级
        /// </summary>		
        public int dt_Level { get; set; }
        /// <summary>
        /// 任务状态（0普通，1占用，2处理延后，3处理成功）
        /// </summary>		
        public int dt_Status { get; set; }
        /// <summary>
        /// 处理人
        /// </summary>		
        public string dt_Name { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>		
        public DateTime inertTime { get; set; }
        /// <summary>
        /// 录入人
        /// </summary>		
        public string insetName { get; set; }
        /// <summary>
        /// 回访备注
        /// </summary>		
        public string dt_remark { get; set; }
        /// <summary>
        /// 记录的登录账号ID
        /// </summary>		
        public int dt_logUid { get; set; }
        /// <summary>
        /// 来源
        /// </summary>		
        public string dt_Source { get; set; }

        public string forumUrl { get; set; }
    }
    /// <summary>
    /// 精简版本任务信息
    /// </summary>
    public partial class Sys_TaskDailySimplify
    {
        /// <summary>
        /// ID
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string t_mk { get; set; }
        /// <summary>
        /// 等级
        /// </summary>
        public int dt_Level { get; set; }
        /// <summary>
        /// 录入人
        /// </summary>		
        public string insetName { get; set; }
        /// <summary>
        /// 来源
        /// </summary>		
        public string dt_Source { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int dt_Status { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string dt_StatusName { get; set; }
    }

    public class SimpleVisitTask
    {
        public int id { get; set; }
        public int accountid { get; set; }
        public string t_mk { get; set; }
        public string dt_remark { get; set; }
    }

}

