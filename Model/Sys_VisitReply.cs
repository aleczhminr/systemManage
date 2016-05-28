using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace Model
{
	 	//Sys_VisitReply
			/// <summary>
    /// 回访回复
        /// </summary>	
    [Serializable]
	public partial class Sys_VisitReply
	{
   		     
      	/// <summary>
		/// id
        /// </summary>		
        public int id { get; set; }
		/// <summary>
        /// 回访信息ID
        /// </summary>		
        public int vi_id { get; set; }
		/// <summary>
        /// 店铺ID
        /// </summary>		
        public int accid { get; set; }
		/// <summary>
        /// 回复内容
        /// </summary>		
        public string vr_Content { get; set; }
		/// <summary>
        /// 处理状态
        /// </summary>		
        public int vr_Stat { get; set; }
		/// <summary>
        /// 回复人
        /// </summary>		
        public string vr_Name { get; set; }
		/// <summary>
        /// 回复时间
        /// </summary>		
        public DateTime vr_Time { get; set; }
		/// <summary>
        /// 回复信息状态 0 未查看，1已查看
        /// </summary>		
        public int reply_Stat { get; set; }
		   
	}
}

