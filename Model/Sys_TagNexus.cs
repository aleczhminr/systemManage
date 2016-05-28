using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //Sys_TagNexus
    /// <summary>
    /// 标签关系
    /// </summary>	
    [Serializable]
    public partial class Sys_TagNexus
    {

        /// <summary>
        /// id
        /// </summary>		
        public int id { get; set; }
        /// <summary>
        /// 标签ID
        /// </summary>		
        public int tag_id { get; set; }
        /// <summary>
        /// 店铺ID
        /// </summary>		
        public int acc_id { get; set; }
        /// <summary>
        /// 录入人
        /// </summary>		
        public string insertName { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>		
        public DateTime insertTime { get; set; }

    }


}

