using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace Model
{
	 	//Sys_Account
			/// <summary>
		/// Sys_Account
        /// </summary>	
    [Serializable]
	public partial class Sys_Account
	{
   		     
      	/// <summary>
		/// id
        /// </summary>		
        public int id{get;set;}        
		/// <summary>
		/// accid
        /// </summary>		
        public int accid{get;set;}        
		/// <summary>
		/// a_QQ
        /// </summary>		
        public string a_QQ{get;set;}        
		/// <summary>
		/// a_WeiXin
        /// </summary>		
        public string a_WeiXin{get;set;}        
		/// <summary>
		/// a_Tel
        /// </summary>		
        public string a_Tel{get;set;}        
		/// <summary>
		/// a_Address
        /// </summary>		
        public string a_Address{get;set;}        
		/// <summary>
		/// a_Industry
        /// </summary>		
        public string a_Industry{get;set;}        
		/// <summary>
		/// a_Name
        /// </summary>		
        public string a_Name{get;set;}        
		/// <summary>
		/// a_IdentityNumber
        /// </summary>		
        public string a_IdentityNumber{get;set;}        
		/// <summary>
		/// a_ShopSize
        /// </summary>		
        public string a_ShopSize{get;set;}        
		/// <summary>
		/// a_Operate
        /// </summary>		
        public string a_Operate{get;set;}        
		/// <summary>
		/// a_Duration
        /// </summary>		
        public string a_Duration{get;set;}        
		/// <summary>
		/// a_OtherSoftware
        /// </summary>		
        public string a_OtherSoftware{get;set;}        
		/// <summary>
		/// a_Remark
        /// </summary>		
        public string a_Remark{get;set;}        
		/// <summary>
		/// a_OtherSoftwareType
        /// </summary>		
        public int a_OtherSoftwareType{get;set;}        
		/// <summary>
		/// a_CustomerSourceType
        /// </summary>		
        public int a_CustomerSourceType{get;set;}        
		/// <summary>
		/// a_CustomerSource
        /// </summary>		
        public string a_CustomerSource{get;set;}        
		/// <summary>
		/// feedbackTel
        /// </summary>		
        public string feedbackTel{get;set;}        
		/// <summary>
		/// feedbackQQ
        /// </summary>		
        public string feedbackQQ{get;set;}        
		/// <summary>
		/// a_UseCause
        /// </summary>		
        public string a_UseCause{get;set;}        
		/// <summary>
		/// sysAddress
        /// </summary>		
        public string sysAddress{get;set;}        
		   
	}

    public partial class SysAccountInfo
    {
        public int accid { get; set; }
        /// <summary>
        /// QQ
        /// </summary>
        public string sys_qq { get; set; }
        /// <summary>
        /// 微信
        /// </summary>
        public string sys_weixin { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string sys_tel { get; set; }
        /// <summary>
        /// 店铺大小
        /// </summary>
        public string sys_shopsize { get; set; }
        /// <summary>
        /// 经营内容
        /// </summary>
        public string sys_operate { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string sys_address { get; set; }
        /// <summary>
        /// 行业
        /// </summary>
        public string sys_industry { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string sys_name { get; set; }
        /// <summary>
        /// 身份证
        /// </summary>
        public string sys_indentity { get; set; }
        /// <summary>
        /// 开店时长
        /// </summary>
        public string sys_duration { get; set; }
        /// <summary>
        /// 用过软件
        /// </summary>
        public string sys_software { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string sys_remark { get; set; }

        /// <summary>
        /// 反馈QQ
        /// </summary>
        public string feedbackQQ { get; set; }
        /// <summary>
        /// 反馈电话
        /// </summary>
        public string feedbackTel { get; set; }

        /// <summary>
        /// 大概地址
        /// </summary>
        public string aboutAddress { get; set; }
    }

}

