using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //T_Task_Journal
    /// <summary>
    /// 前台分享任务奖励
    /// </summary>	
    [Serializable]
    public partial class T_Task_Journal
    {

        /// <summary>
        /// id
        /// </summary>		
        public int id { get; set; }
        /// <summary>
        /// acc_id
        /// </summary>		
        public int acc_id { get; set; }
        /// <summary>
        /// t_explan
        /// </summary>		
        public string t_explan { get; set; }
        /// <summary>
        /// t_type
        /// </summary>		
        public string t_type { get; set; }
        /// <summary>
        /// t_time
        /// </summary>		
        public DateTime t_time { get; set; }
        /// <summary>
        /// t_remarks
        /// </summary>		
        public string t_remarks { get; set; }
        /// <summary>
        /// t_status
        /// </summary>		
        public int t_status { get; set; }
        /// <summary>
        /// t_source
        /// </summary>		
        public string t_source { get; set; }
        /// <summary>
        /// t_longitude
        /// </summary>		
        public string t_longitude { get; set; }
        /// <summary>
        /// t_latitude
        /// </summary>		
        public string t_latitude { get; set; }
		/// <summary>
		/// 建议
		/// </summary>
		public string t_advance { get; set; }

    }

    /// <summary>
    /// 前台任务信息
    /// </summary>	
    public class T_Task_JournalInfo:T_Task_Journal
    {
        /// <summary>
        /// 店铺名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 状态名称
        /// </summary>
        public string t_StatusName { get; set; }
    }
}

