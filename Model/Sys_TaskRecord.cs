using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //Sys_TaskRecord
    /// <summary>
    /// 
    /// </summary>	
    [Serializable]
    public partial class Sys_TaskRecord
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
        /// t_mk
        /// </summary>		
        public string t_mk { get; set; }
        /// <summary>
        /// vm_id
        /// </summary>		
        public int vm_id { get; set; }
        /// <summary>
        /// dt_Time
        /// </summary>		
        public DateTime dt_Time { get; set; }
        /// <summary>
        /// dt_Level
        /// </summary>		
        public int dt_Level { get; set; }
        /// <summary>
        /// dt_Status
        /// </summary>		
        public int dt_Status { get; set; }
        /// <summary>
        /// inertTime
        /// </summary>		
        public DateTime inertTime { get; set; }
        /// <summary>
        /// insetName
        /// </summary>		
        public string insetName { get; set; }
        /// <summary>
        /// treatTime
        /// </summary>		
        public DateTime treatTime { get; set; }
        /// <summary>
        /// treatName
        /// </summary>		
        public string treatName { get; set; }
        /// <summary>
        /// vi_Id
        /// </summary>		
        public int vi_Id { get; set; }

    }
}

