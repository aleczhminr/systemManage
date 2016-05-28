using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace Model
{
	 	//Sys_SysAreaData
			/// <summary>
		/// Sys_SysAreaData
        /// </summary>	
    [Serializable]
	public partial class Sys_SysAreaData
	{
   		     
      	/// <summary>
		/// id
        /// </summary>		
        public int id{get;set;}        
		/// <summary>
		/// 记录日期
        /// </summary>		
        public DateTime recDate{get;set;}        
		/// <summary>
		/// 区域ID
        /// </summary>		
        public string areaID{get;set;}        
		/// <summary>
		/// 区域EN
        /// </summary>		
        public string areaEn{get;set;}        
		/// <summary>
		/// 区域名称
        /// </summary>		
        public string areaName{get;set;}        
		/// <summary>
		/// 注册店铺数
        /// </summary>		
        public int regCnt{get;set;}        
		/// <summary>
		/// 店铺会员数
        /// </summary>		
        public int MemberCnt{get;set;}        
		/// <summary>
		/// 销售笔数
        /// </summary>		
        public int saleCnt{get;set;}        
		/// <summary>
		/// 支出笔数
        /// </summary>		
        public int payCnt{get;set;}        
		/// <summary>
		/// 短信条数
        /// </summary>		
        public int smsCnt{get;set;}        
		/// <summary>
		/// 订单金额
        /// </summary>		
        public decimal orderSum{get;set;}        
		/// <summary>
		/// 活跃用户
        /// </summary>		
        public int UserActiveCnt{get;set;}        
		/// <summary>
		/// 当天登录用户
        /// </summary>		
        public int UserLoginCnt{get;set;}        
		/// <summary>
		/// 休眠用户
        /// </summary>		
        public int UserSleepCnt{get;set;}        
		/// <summary>
		/// 流失用户
        /// </summary>		
        public int UserLostCnt{get;set;}        
		   
	}
}

