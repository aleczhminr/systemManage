using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //Sys_VisitTagNexus
    /// <summary>
    /// 回访标签关系
    /// </summary>	
    [Serializable]
    public partial class Sys_VisitTagNexus
    {

        /// <summary>
        /// id
        /// </summary>		
        public int id { get; set; }
        /// <summary>
        /// 标签ID
        /// </summary>		
        public int tid { get; set; }
        /// <summary>
        /// 回访ID
        /// </summary>		
        public int vid { get; set; }
        /// <summary>
        /// insertName
        /// </summary>		
        public string insertName { get; set; }
        /// <summary>
        /// insertTime
        /// </summary>		
        public DateTime insertTime { get; set; }

    }
}

