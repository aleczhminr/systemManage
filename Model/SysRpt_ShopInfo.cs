using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace Model
{
	 	//SysRpt_ShopInfo
			/// <summary>
		/// SysRpt_ShopInfo
        /// </summary>	
    [Serializable]
	public partial class SysRpt_ShopInfo
	{
   		     
      	/// <summary>
		/// id
        /// </summary>		
        public int id{get;set;}        
		/// <summary>
		/// accountid
        /// </summary>		
        public int accountid{get;set;}        
		/// <summary>
		/// AgentId
        /// </summary>		
        public int AgentId{get;set;}        
		/// <summary>
		/// userNum
        /// </summary>		
        public int userNum{get;set;}        
		/// <summary>
		/// allUserNum
        /// </summary>		
        public int allUserNum{get;set;}        
		/// <summary>
		/// smsNum
        /// </summary>		
        public int smsNum{get;set;}        
		/// <summary>
		/// freeSmsNum
        /// </summary>		
        public decimal freeSmsNum{get;set;}        
		/// <summary>
		/// allSmsNum
        /// </summary>		
        public int allSmsNum{get;set;}        
		/// <summary>
		/// integral
        /// </summary>		
        public int integral{get;set;}        
		/// <summary>
		/// goodsNum
        /// </summary>		
        public decimal goodsNum{get;set;}        
		/// <summary>
		/// registration
        /// </summary>		
        public int registration{get;set;}        
		/// <summary>
		/// regIntegral
        /// </summary>		
        public int regIntegral{get;set;}        
		/// <summary>
		/// moodNum
        /// </summary>		
        public int moodNum{get;set;}        
		/// <summary>
		/// saleNum
        /// </summary>		
        public decimal saleNum{get;set;}        
		/// <summary>
		/// saleMoney
        /// </summary>		
        public decimal saleMoney{get;set;}        
		/// <summary>
		/// orderNum
        /// </summary>		
        public decimal orderNum{get;set;}        
		/// <summary>
		/// orderMoney
        /// </summary>		
        public decimal orderMoney{get;set;}        
		/// <summary>
		/// outlayNum
        /// </summary>		
        public int outlayNum{get;set;}        
		/// <summary>
		/// outlayMoney
        /// </summary>		
        public decimal outlayMoney{get;set;}        
		/// <summary>
		/// active
        /// </summary>		
        public int active{get;set;}        
		/// <summary>
		/// maxActive
        /// </summary>		
        public int maxActive{get;set;}        
		/// <summary>
		/// srsaId
        /// </summary>		
        public int srsaId{get;set;}        
		/// <summary>
		/// allLoginNum
        /// </summary>		
        public int allLoginNum{get;set;}        
		/// <summary>
		/// regTime
        /// </summary>		
        public DateTime regTime{get;set;}        
		/// <summary>
		/// memSaleNum
        /// </summary>		
        public decimal memSaleNum{get;set;}        
		/// <summary>
		/// memSaleMoney
        /// </summary>		
        public decimal memSaleMoney{get;set;}        
		/// <summary>
		/// retailSaleNum
        /// </summary>		
        public decimal retailSaleNum{get;set;}        
		/// <summary>
		/// retailSaleMoney
        /// </summary>		
        public decimal retailSaleMoney{get;set;}        
		/// <summary>
		/// lastLoginTime
        /// </summary>		
        public DateTime lastLoginTime{get;set;}        
		/// <summary>
		/// loginType
        /// </summary>		
        public int loginType{get;set;}        
		/// <summary>
		/// ContinuousDay
        /// </summary>		
        public decimal ContinuousDay{get;set;}        
		/// <summary>
		/// updateTime
        /// </summary>		
        public DateTime updateTime{get;set;}        
		/// <summary>
		/// taskStat
        /// </summary>		
        public int taskStat{get;set;}        
		/// <summary>
		/// acc_Rep
        /// </summary>		
        public int acc_Rep{get;set;}        
		/// <summary>
		/// useVoucher
        /// </summary>		
        public int useVoucher{get;set;}        
		/// <summary>
		/// useIntegral
        /// </summary>		
        public int useIntegral{get;set;}        
		/// <summary>
		/// shopAssistant
        /// </summary>		
        public int shopAssistant{get;set;}        
		/// <summary>
		/// subbranch
        /// </summary>		
        public int subbranch{get;set;}        
		/// <summary>
		/// serviceNumber
        /// </summary>		
        public int serviceNumber{get;set;}        
		   
	}

    /// <summary>
    /// 后台店铺概要信息
    /// </summary>
    public partial class SysShopSummarizeInfo
    {
        public SysShopSummarizeInfo()
        {
            FirstActive = false;
            FirstLost = false;
        }

        /// <summary>
        /// 店铺ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 店铺名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime RegTime { get; set; }
        /// <summary>
        /// 版本
        /// </summary>
        public int aotjb { get; set; }
        /// <summary>
        /// 版本名称
        /// </summary>
        public string aotjbName { get; set; }
        /// <summary>
        /// 版本结束时间
        /// </summary>
        public DateTime aotjbEndTime { get; set; }
        /// <summary>
        /// 剩余短信数量
        /// </summary>
        public int dxunity { get; set; }
        /// <summary>
        /// 会员数量
        /// </summary>
        public int userNum { get; set; }
        /// <summary>
        /// 商品数量
        /// </summary>
        public int goodsNum { get; set; }
        /// <summary>
        /// 销售数量
        /// </summary>
        public int saleNum { get; set; }
        /// <summary>
        /// 短信数量
        /// </summary>
        public int smsNum { get; set; }
        /// <summary>
        /// 支出笔数
        /// </summary>
        public int outlayNum { get; set; }
        /// <summary>
        /// 总优惠券
        /// </summary>
        public int allCount { get; set; }
        /// <summary>
        /// 已经使用优惠券
        /// </summary>
        public int userCount { get; set; }
        /// <summary>
        /// 登录次数
        /// </summary>
        public int LoginTimeWeb { get; set; }
        /// <summary>
        /// 最后一次登录时间
        /// </summary>
        public DateTime LoginTimeLast { get; set; }
        /// <summary>
        /// 活跃状态
        /// </summary>
        public int active { get; set; }
        /// <summary>
        /// 状态值
        /// </summary>
        public string activeName { get; set; }
        /// <summary>
        /// 订单总金额
        /// </summary>
        public decimal orderMoney { get; set; }
        /// <summary>
        /// 是否首次活跃
        /// </summary>
        public bool FirstActive { get; set; }
        /// <summary>
        /// 是否首次流失
        /// </summary>
        public bool FirstLost { get; set; }
    }

    /// <summary>
    /// 首页数据面板的详情显示
    /// </summary>
    public class IndexDetailModel
    {
        public IndexDetailModel()
        {
            listData = new List<SysShopSummarizeInfo>();
        }

        public int rowCount { get; set; }
        public int maxPage { get; set; }

        public List<SysShopSummarizeInfo> listData { get; set; }
    }

    /// <summary>
    /// 地区用户状态Model
    /// </summary>
    public class LocationUsrStatus
    {
        public LocationUsrStatus()
        {
            FormerActive = 0;
            NowActive = 0;
        }
        public int FormerActive { get;set; }
        public int NowActive { get; set; }
    }
}

