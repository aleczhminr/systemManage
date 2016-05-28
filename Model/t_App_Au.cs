using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //t_App_Au
    /// <summary>
    /// 店铺开通权限
    /// </summary>	
    [Serializable]
    public partial class t_App_Au
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
        /// 产品KEY
        /// </summary>		
        public int appkey { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>		
        public string appName { get; set; }
        /// <summary>
        /// 开通时间
        /// </summary>		
        public DateTime stattime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>		
        public DateTime endtime { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>		
        public DateTime aa_time { get; set; }
        /// <summary>
        /// 备注
        /// </summary>		
        public string aa_remark { get; set; }
        /// <summary>
        /// 短网址
        /// </summary>		
        public string aa_ShortUrl { get; set; }
        /// <summary>
        /// 状态 <para> （主要用于标识  有些产品是 分免费 和收费）</para>
        /// </summary>		
        public int aa_Status { get; set; }

    }

    /// <summary>
    /// app数据列表
    /// </summary>
    public class AppInfoModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 店铺Id
        /// </summary>
        public int AccId { get; set; }

        /// <summary>
        /// AppKey
        /// </summary>
        public int AppKey { get; set; }

        /// <summary>
        /// App名称
        /// </summary>
        public string AppName { get; set; }

        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 截至日期
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 操作日期
        /// </summary>
        public DateTime OperatorDate { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// Url地址
        /// </summary>
        public string ShortUrl { get; set; }

        /// <summary>
        /// 支付状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 激活状态
        /// </summary>
        public int ActiveStatus { get; set; }

        /// <summary>
        /// 剩余天数
        /// </summary>
        public int LastDays { get; set; }

        /// <summary>
        /// 显示状态
        /// </summary>
        public string DisplayStatus { get; set; }
    }

    /// <summary>
    /// 权限列表
    /// </summary>
    public class AppList
    {
        public AppList()
        {
            Data = new List<AppInfoModel>();
        }
        private int _Rows = 0;
        /// <summary>
        /// 激活总数
        /// </summary>
        public int ActiveSum { get; set; }

        /// <summary>
        /// App总数
        /// </summary>
        public int Rows { get { return Data.Count; } }

        /// <summary>
        /// App列表
        /// </summary>
        public List<AppInfoModel> Data { get; set; }
    }
}

