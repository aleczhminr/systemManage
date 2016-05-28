using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace Model
{
	 	//Sys_SysRevisit_AccInfo
			/// <summary>
		/// Sys_SysRevisit_AccInfo
        /// </summary>	
    [Serializable]
	public partial class Sys_SysRevisit_AccInfo
	{
   		     
      	/// <summary>
		/// id
        /// </summary>		
        public int id{get;set;}        
		/// <summary>
		/// 店铺ID
        /// </summary>		
        public int accID{get;set;}        
		/// <summary>
		/// 创建时间
        /// </summary>		
        public DateTime CreateTime{get;set;}        
		/// <summary>
		/// 店铺活跃状态
        /// </summary>		
        public int ActiveStatus{get;set;}        
		/// <summary>
		/// 登录次数
        /// </summary>		
        public int LoginTimes{get;set;}        
		/// <summary>
		/// 最近一次登录时间
        /// </summary>		
        public DateTime LastLoginTime{get;set;}        
		/// <summary>
		/// 最后登录距现在天数
        /// </summary>		
        public int LastLoginDays{get;set;}        
		/// <summary>
		/// 商品数量
        /// </summary>		
        public int GoodsCnt{get;set;}        
		/// <summary>
		/// 店铺会员数
        /// </summary>		
        public int UserCnt{get;set;}        
		/// <summary>
		/// 销售笔数
        /// </summary>		
        public int SalesCnt{get;set;}        
		/// <summary>
		/// 支出笔数
        /// </summary>		
        public int PayCnt{get;set;}        
		/// <summary>
		/// 短信发送数
        /// </summary>		
        public int SmsCnt{get;set;}        
		/// <summary>
		/// 店铺成功订单数
        /// </summary>		
        public int OrderCnt{get;set;}        
		/// <summary>
		/// 店铺订单金额
        /// </summary>		
        public decimal OrderSum{get;set;}        
		/// <summary>
		/// 平均登录次数
        /// </summary>		
        public decimal avgLoginTimes{get;set;}        
		/// <summary>
		/// 平均销售笔数
        /// </summary>		
        public decimal avgSalesCnt{get;set;}        
		/// <summary>
		/// 平均录入商品数
        /// </summary>		
        public decimal avgGoodsCnt{get;set;}        
		/// <summary>
		/// 平均录入会员数
        /// </summary>		
        public decimal avgUserCnt{get;set;}        
		/// <summary>
		/// 平均支出笔数
        /// </summary>		
        public decimal avgPayCnt{get;set;}        
		/// <summary>
		/// 平均短信发送数
        /// </summary>		
        public decimal avgSmsCnt{get;set;}        
		/// <summary>
		/// 上次销售日期
        /// </summary>		
        public DateTime LastSales{get;set;}        
		/// <summary>
		/// 上次录入商品日期
        /// </summary>		
        public DateTime LastGoods{get;set;}        
		/// <summary>
		/// 上次录入会员日期
        /// </summary>		
        public DateTime LastUser{get;set;}        
		/// <summary>
		/// 上次支出日期
        /// </summary>		
        public DateTime LastPay{get;set;}        
		/// <summary>
		/// 上次短信发送日期
        /// </summary>		
        public DateTime LastSms{get;set;}        
		/// <summary>
		/// 上次回访日期
        /// </summary>		
        public DateTime LastRevisit{get;set;}        
		/// <summary>
		/// 店铺版本
        /// </summary>		
        public int accVer{get;set;}        
		/// <summary>
		/// 版本开始时间
        /// </summary>		
        public DateTime verBgTime{get;set;}        
		/// <summary>
		/// 版本到期时间
        /// </summary>		
        public DateTime verEdTime{get;set;}        
		/// <summary>
		/// 版本距今剩余到期天数
        /// </summary>		
        public int verEdDays{get;set;}        
		/// <summary>
		/// 短信余额
        /// </summary>		
        public int SmsBalance{get;set;}        
		/// <summary>
		/// 会员剩余
        /// </summary>		
        public int UserBalance{get;set;}        
		/// <summary>
		/// 回访分类
        /// </summary>		
        public int RevisitClass{get;set;}        
		/// <summary>
		/// 回访项目
        /// </summary>		
        public string RevisitItem{get;set;}        
		/// <summary>
		/// 回访描述
        /// </summary>		
        public string RevisitDesc{get;set;}        
		/// <summary>
		/// 回访状态
        /// </summary>		
        public int RevisitStatus{get;set;}        
		/// <summary>
		/// 关联回访记录
        /// </summary>		
        public int RevisitFlag{get;set;}        
		/// <summary>
		/// 上级回访记录
        /// </summary>		
        public int ParentRevisit{get;set;}        
		   
	}
}

