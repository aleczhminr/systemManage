using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace Model
{
	 	//Sys_SysAreaData4Echarts
			/// <summary>
		/// Sys_SysAreaData4Echarts
        /// </summary>	
    [Serializable]
	public partial class Sys_SysAreaData4Echarts
	{
   		     
      	/// <summary>
		/// id
        /// </summary>		
        public int id{get;set;}        
		/// <summary>
		/// recDate
        /// </summary>		
        public DateTime recDate{get;set;}        
		/// <summary>
		/// areaID
        /// </summary>		
        public int areaID{get;set;}        
		/// <summary>
		/// areaName
        /// </summary>		
        public string areaName{get;set;}        
		/// <summary>
		/// regCnt
        /// </summary>		
        public int regCnt{get;set;}        
		/// <summary>
		/// MemberCnt
        /// </summary>		
        public int MemberCnt{get;set;}        
		/// <summary>
		/// saleCnt
        /// </summary>		
        public int saleCnt{get;set;}        
		/// <summary>
		/// payCnt
        /// </summary>		
        public int payCnt{get;set;}        
		/// <summary>
		/// smsCnt
        /// </summary>		
        public int smsCnt{get;set;}        
		/// <summary>
		/// orderSum
        /// </summary>		
        public decimal orderSum{get;set;}        
		/// <summary>
		/// UserActiveCnt
        /// </summary>		
        public int UserActiveCnt{get;set;}        
		/// <summary>
		/// UserLoginCnt
        /// </summary>		
        public int UserLoginCnt{get;set;}        
		/// <summary>
		/// UserSleepCnt
        /// </summary>		
        public int UserSleepCnt{get;set;}        
		/// <summary>
		/// UserLostCnt
        /// </summary>		
        public int UserLostCnt{get;set;}        
		/// <summary>
		/// prov_name
        /// </summary>		
        public string prov_name{get;set;}

    }
    public class Sys_AreaDate4EchartsMapList
    {
        /// <summary>
        /// 区域个数
        /// </summary>
        public int areaCount { get; set; }

        /// <summary>
        /// 区域总计
        /// </summary>
        public decimal areaSum { get; set; }

        /// <summary>
        /// 最大值
        /// </summary>
        public decimal areaMaxValue { get; set; }

        /// <summary>
        /// 最小值
        /// </summary>
        public decimal areaMinValue { get; set; }

        /// <summary>
        /// AreaDataMap详细列表
        /// </summary>
        public List<Sys_AreaData4EchartsMap> areaDataList { get; set; }
    }

    public class Sys_AreaData4EchartsMap
    {
        /// <summary>
        /// 记录日期
        /// </summary>		
        public DateTime recDate { get; set; }

        /// <summary>
        /// 区域ID
        /// </summary>		
        public string areaID { get; set; }

        /// <summary>
        /// 区域名称
        /// </summary>		
        public string areaName { get; set; }

        /// <summary>
        /// 注册店铺数
        /// </summary>		
        public int regCnt { get; set; }

        /// <summary>
        /// 店铺会员数
        /// </summary>		
        public int MemberCnt { get; set; }

        /// <summary>
        /// 销售笔数
        /// </summary>		
        public int saleCnt { get; set; }

        /// <summary>
        /// 支出笔数
        /// </summary>		
        public int payCnt { get; set; }

        /// <summary>
        /// 短信条数
        /// </summary>		
        public int smsCnt { get; set; }

        /// <summary>
        /// 订单金额
        /// </summary>		
        public decimal orderSum { get; set; }

        /// <summary>
        /// 活跃用户
        /// </summary>		
        public int UserActiveCnt { get; set; }

        /// <summary>
        /// 当天登录用户
        /// </summary>		
        public int UserLoginCnt { get; set; }

        /// <summary>
        /// 休眠用户
        /// </summary>		
        public int UserSleepCnt { get; set; }

        /// <summary>
        /// 流失用户
        /// </summary>		
        public int UserLostCnt { get; set; }

        /// <summary>
        /// 活跃率
        /// </summary>
        public string ActRatio { get; set; }

        /// <summary>
        /// 区域标示颜色
        /// </summary>
        public string areaColor { get; set; }
        public string prov_name { get; set; }
        /// <summary>
        /// 排序区域
        /// </summary>
        public int keyAreaValue { get; set; }
    }

    /// <summary>
    /// 店铺信息
    /// </summary>
    public class AreaShopInfoEx
    {
        /// <summary>
        /// 店铺ID
        /// </summary>
        public int accID { get; set; }

        /// <summary>
        /// 店铺名称
        /// </summary>
        public string accName { get; set; }

        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime regTime { get; set; }

        /// <summary>
        /// 登录次数
        /// </summary>
        public int loginTimes { get; set; }

        /// <summary>
        /// 最后登录
        /// </summary>
        public DateTime lastLoginTime { get; set; }

        /// <summary>
        /// 下一页
        /// </summary>
        public int nextPage { get; set; }

    }
}

