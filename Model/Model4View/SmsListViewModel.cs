using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;


namespace Model.Model4View
{
    
    /// <summary>
    /// 短信列表视图模型
    /// </summary>
    public class SmsListViewModel : DbContext
    {
        public DbSet<SmsNotifyItem> SmsListView { get; set; }
    }

    /// <summary>
    /// 短信详细列表
    /// </summary>
    public class SmsDetail
    {
        public int smsCnt { get; set; }

        public List<SmsDetails> detailList { get; set; }
    }

    /// <summary>
    /// 短信详细列表外层Model
    /// </summary>
    public class SmsDetails
    {
        public int id { get; set; }
        public string phoneNum { get; set; }
        /// <summary>
        /// 通道
        /// </summary>
        public string smsChannel { get; set; }
        public string smsContent { get; set; }
        public int ContentLength { get; set; }
        public DateTime sendtime { get; set; }
        /// <summary>
        /// 短信状态
        /// </summary>
        public int smsStatus { get; set; }
        /// <summary>
        /// 短信状态
        /// </summary>
        public string smsStatusName { get; set; }
        public int userID { get; set; }
        public int realCnt { get; set; }
        public int isFree { get; set; }
        public string errDesc { get; set; }
        public string smsReport { get; set; }

    }

    /// <summary>
    /// 审核统计信息
    /// </summary>
    public class ReviewStat
    {
        public int SumCnt { get; set; }
        public int WaitCnt { get; set; }
        public int PassCnt { get; set; }
        public int SumRejectCntCnt { get; set; }
    }
}
