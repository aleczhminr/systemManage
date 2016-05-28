using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace Model
{
	 	//Sys_Manage_User
			/// <summary>
		/// Sys_Manage_User
        /// </summary>	
    [Serializable]
	public partial class Sys_Manage_User
	{
   		     
      	/// <summary>
		/// Id
        /// </summary>		
        public int Id{get;set;}        
		/// <summary>
		/// UserName
        /// </summary>		
        public string UserName{get;set;}        
		/// <summary>
		/// PassWord
        /// </summary>		
        public string PassWord{get;set;}        
		/// <summary>
		/// P_session
        /// </summary>		
        public long P_session{get;set;}        
		/// <summary>
		/// LoginCounter
        /// </summary>		
        public long LoginCounter{get;set;}        
		/// <summary>
		/// LastLoginTime
        /// </summary>		
        public DateTime LastLoginTime{get;set;}        
		/// <summary>
		/// phone
        /// </summary>		
        public string phone{get;set;}        
		/// <summary>
		/// name
        /// </summary>		
        public string name{get;set;}        
		/// <summary>
		/// simcard
        /// </summary>		
        public string simcard{get;set;}        
		/// <summary>
		/// state
        /// </summary>		
        public int state{get;set;}        
		/// <summary>
		/// WeixinOpenid
        /// </summary>		
        public string WeixinOpenid{get;set;}        
		/// <summary>
		/// WeiXinType
        /// </summary>		
        public int WeiXinType{get;set;}
        /// <summary>
        /// 菜单权限
        /// </summary>
        public string MenuPermission { get; set; }
		   
	}

    [Serializable]
    public class ManageUserModel
    {
        /// <summary>
        /// 人员ID
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// 登录名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 登录权限
        /// </summary>
        public int PowerSession { get; set; }
        /// <summary>
        /// 菜单权限
        /// </summary>
        public string MenuPermission { get; set; }

        /// <summary>
        /// 登录次数
        /// </summary>
        public int LoginCnt { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 登录状态
        /// </summary>
        public bool LoginStatus { get; set; }

        /// <summary>
        /// 微信OpenId
        /// </summary>
        public string WeixinOpenid { get; set; }
    }

    public class SysManageUserBase
    {

        /// <summary>
        /// 账号ID
        /// </summary>		
        public int Id { get; set; }
        /// <summary>
        /// 登录账号
        /// </summary>		
        public string UserName { get; set; }
        /// <summary>
        /// 部门
        /// </summary>		
        public long P_session { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string P_sesionName { get; set; }
        /// <summary>
        /// 登录次数
        /// </summary>		
        public long LoginCounter { get; set; }
        /// <summary>
        /// 最后登录时间
        /// </summary>		
        public DateTime LastLoginTime { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>		
        public string phone { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>		
        public string name { get; set; }
        /// <summary>
        /// 账号状态
        /// </summary>		
        public int state { get; set; }
        /// <summary>
        /// 微信ID
        /// </summary>		
        public string WeixinOpenid { get; set; }
    }
}

