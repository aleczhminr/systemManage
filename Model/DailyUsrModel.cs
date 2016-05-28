using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace Model
{
    public class DailyAnalyzeModel
    {
        public DailyAnalyzeModel()
        {
            UsrList = new List<DailyUsrModel>();
        }

        public int RowCount { get; set; }

        public List<DailyUsrModel> UsrList { get; set; }

    }

    public class DailyUsrModel
    {
        /// <summary>
        /// 序号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 店铺名称
        /// </summary>
        public string AccountName { get; set; }
        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime RegTime { get; set; }
        /// <summary>
        /// 注册来源
        /// </summary>
        public string RegSource { get; set; }
        /// <summary>
        /// 销售笔数
        /// </summary>
        public decimal SaleNum { get; set; }
        /// <summary>
        /// 会员笔数
        /// </summary>
        public decimal MemberPaid { get; set; }
        /// <summary>
        /// 零售笔数
        /// </summary>
        public decimal Retail { get; set; }
        /// <summary>
        /// 短信数
        /// </summary>
        public int SmsNum { get; set; }
        /// <summary>
        /// 订单数
        /// </summary>
        public int OrderNum { get; set; }
        /// <summary>
        /// 商品数
        /// </summary>
        public int GoodsNum { get; set; }
        /// <summary>
        /// 会员数
        /// </summary>
        public int MemberNum { get; set; }
        /// <summary>
        /// 是否签到
        /// </summary>
        public int SignFlag { get; set; }
        /// <summary>
        /// 心情数
        /// </summary>
        public int MoodNum { get; set; }
    }
}
