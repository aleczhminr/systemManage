using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //Sys_DailyCheckRecord
    /// <summary>
    /// Sys_DailyCheckRecord
    /// </summary>	
    [Serializable]
    public partial class Sys_DailyCheckRecord
    {

        /// <summary>
        /// Id
        /// </summary>		
        public int Id { get; set; }
        /// <summary>
        /// 任务ID
        /// </summary>		
        public int dcId { get; set; }
        /// <summary>
        /// 任务名称
        /// </summary>		
        public string TaskName { get; set; }
        /// <summary>
        /// 操作值
        /// </summary>		
        public string TaskValue { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>		
        public string Operator { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>		
        public DateTime OperateTime { get; set; }
        /// <summary>
        /// 备注
        /// </summary>		
        public string Remark { get; set; }

    }
}

