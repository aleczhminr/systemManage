using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace Model
{
	 	//Sys_VisitInfo
			/// <summary>
		/// Sys_VisitInfo
        /// </summary>	
    [Serializable]
	public partial class Sys_VisitInfo
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
        /// 记录类别 RecordType <para>（使用 VisitInfoEnum.RecordType 用信息 不在使用【Sys_RecordType】的数据）</para>
        /// </summary>		
        public int rt_Maxid{get;set;}
        /// <summary>
		/// 一级概要
        /// </summary>		
        public int vr_Maxid{get;set;}        
		/// <summary>
        /// 二级概要
        /// </summary>		
        public int vr_Minid{get;set;}
        /// <summary>
        /// 回访方式 VisitManner 
        /// <para>（使用 VisitInfoEnum.VisitManner 用信息 不在使用【Sys_VisitManner】的数据）</para>
        /// </summary>		
        public int vm_id{get;set;}        
		/// <summary>
        /// 主动状态 （1、主动向用户回访，2、用户来电）
        /// </summary>		
        public int initiative{get;set;}        
		/// <summary>
		/// 内容
        /// </summary>		
        public string vi_Content{get;set;}        
		/// <summary>
		/// 处理状态
        /// </summary>		
        public int handleStat{get;set;}        
		/// <summary>
		/// insertName
        /// </summary>		
        public string insertName{get;set;}        
		/// <summary>
		/// insertTime
        /// </summary>		
        public DateTime insertTime{get;set;}        
		/// <summary>
		/// retly_Count
        /// </summary>		
        public int retly_Count{get;set;}
        /// <summary>
        /// 联系方式 与 回访方式 一起用
        /// </summary>
        public string Contact { get; set; }


        /// <summary>
        /// 备注
        /// </summary>		
        public string remark { get; set; }  
        /// <summary>
        /// 三级概要
        /// </summary>
        public int vr_Threeid { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime vr_UpDateTime { get; set; }
	}


    public partial class Sys_VisitInfoBase
    {
        /// <summary>
        /// id
        /// </summary>		
        public int id { get; set; }
        /// <summary>
        /// accid
        /// </summary>		
        public int accid { get; set; }
        /// <summary>
        /// 店铺名称
        /// </summary>
        public string CompanyName { get; set; }

        #region 用户信息补全
        /// <summary>
        /// 用户版本
        /// </summary>
        public string Edit { get; set; }
        /// <summary>
        /// 用户真实姓名
        /// </summary>
        public string UserRealName { get; set; }
        /// <summary>
        /// 上次登录时间
        /// </summary>
        public string LoginLast { get; set; }
        #endregion

        /// <summary>
        /// 记录类别 RecordType <para>（使用 VisitInfoEnum.RecordType 用信息 不在使用【Sys_RecordType】的数据）</para>
        /// </summary>		
        public int rt_Maxid { get; set; }
        /// <summary>
        /// 记录类别
        /// </summary>
        public string recordType { get; set; }
        /// <summary>
        /// 回访方式 VisitManner 
        /// <para>（使用 VisitInfoEnum.VisitManner 用信息 不在使用【Sys_VisitManner】的数据）</para>
        /// </summary>		
        public int vm_id { get; set; }
        /// <summary>
        /// VisitManner
        /// </summary>
        public string visitManner { get; set; }
        /// <summary>
        /// 内容
        /// </summary>		
        public string vi_Content { get; set; }
        /// <summary>
        /// 处理状态
        /// </summary>		
        public int handleStat { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string handleStatName { get; set; }
        /// <summary>
        /// insertName
        /// </summary>		
        public string insertName { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime insertTime { get; set; }

        public string FeedBack { get; set; }
        public string Problem { get; set; }
        /// <summary>
        /// 用户回访问题
        /// </summary>
        public string t_mk { get; set; }
    }

    /// <summary>
    /// 详情的单条回访信息
    /// </summary>
    public partial class SysVisitParticularModel
    {
        public SysVisitParticularModel()
        {
            replyList = new List<Sys_VisitReply>();
            tagList = new List<SysVisitTagItem>();
        }
        /// <summary>
        /// id
        /// </summary>		
        public int id { get; set; }
        /// <summary>
        /// accid
        /// </summary>		
        public int accid { get; set; }
        /// <summary>
        /// 店铺名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 记录类别 RecordType <para>（使用 VisitInfoEnum.RecordType 用信息 不在使用【Sys_RecordType】的数据）</para>
        /// </summary>		
        public int rt_Maxid { get; set; }
        /// <summary>
        /// 记录类别
        /// </summary>
        public string recordType { get; set; }
        /// <summary>
        /// 回访方式 VisitManner 
        /// <para>（使用 VisitInfoEnum.VisitManner 用信息 不在使用【Sys_VisitManner】的数据）</para>
        /// </summary>		
        public int vm_id { get; set; }
        /// <summary>
        /// 回访方式
        /// </summary>
        public string visitManner { get; set; }
        /// <summary>
        /// 一级概要
        /// </summary>
        public int vr_Maxid { get; set; }
        /// <summary>
        /// 一级概要
        /// </summary>
        public string visitReasonOne { get; set; }

        /// <summary>
        /// 二级概要
        /// </summary>
        public int vr_Minid { get; set; }
        /// <summary>
        /// 二级概要
        /// </summary>
        public string visitReasonTwo { get; set; }
        /// <summary>
        /// 三级概要
        /// </summary>
        public int vr_Threeid { get; set; }
        /// <summary>
        /// 三级概要
        /// </summary>
        public string visitReasonThree { get; set; }

        /// <summary>
        /// 内容
        /// </summary>		
        public string vi_Content { get; set; }
        /// <summary>
        /// 处理状态
        /// </summary>		
        public int handleStat { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string handleStatName { get; set; }
        /// <summary>
        /// insertName
        /// </summary>		
        public string insertName { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime insertTime { get; set; }
        /// <summary>
        /// 回访标签
        /// </summary>
        public List<SysVisitTagItem> tagList { get; set; }

        /// <summary>
        /// 回访回复
        /// </summary>
        public List<Sys_VisitReply> replyList { get; set; }
    }

    /// <summary>
    /// 需要回访对像
    /// </summary>
    public partial class SysNeedVisitModel
    {
        public int id { get; set; }
        public int accid { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string vi_Content { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int handleStat { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string handleStatName { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime insertTime { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime vr_UpDateTime { get; set; }
        /// <summary>
        /// 录入人
        /// </summary>
        public string insertName { get; set; }
    }

    /// <summary>
    /// 事件对像
    /// </summary>
    public partial class SysCaseModel
    {
        public int id { get; set; }
        /// <summary>
        /// 店铺ID
        /// </summary>
        public int accid { get; set; }
        /// <summary>
        /// 店铺名称
        /// </summary>
        public string  CompanyName { get; set; }
        /// <summary>
        /// 三级概要
        /// </summary>
        public int vr_Threeid { get; set; }
        /// <summary>
        /// 三级概要
        /// </summary>
        public string visitReasonThree { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime insertTime { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime upDateTime { get; set; }
        /// <summary>
        /// 处理状态
        /// </summary>		
        public int handleStat { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string handleStatName { get; set; }
        /// <summary>
        /// 录入人
        /// </summary>
        public string insertName { get; set; }
    }
    
}

