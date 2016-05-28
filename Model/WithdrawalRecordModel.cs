using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class WithdrawalRecordModel
    {
        public int id { get; set; }
        public int accId { get; set; }
        public string accountName { get; set; }

        /// <summary>
        /// 账户类别
        /// </summary>
        public string paymentType { get; set; }

        /// <summary>
        /// 店铺提现账户信息
        /// </summary>
        public string accountInfo { get; set; }

        /// <summary>
        /// 店铺提现账户名
        /// </summary>
        public string paymentName { get; set; }
        /// <summary>
        /// 店铺提现账户ID
        /// </summary>
        public int withdrawalInfoId { get; set; }
        /// <summary>
        /// 本次提现金额
        /// </summary>
        public decimal withdrawalAmount { get; set; }
        /// <summary>
        /// 本次提现剩余金额
        /// </summary>
        public decimal balanceAmount { get; set; }
        /// <summary>
        /// 本次提现时的总收入
        /// </summary>
        public decimal totalAmount { get; set; }
        /// <summary>
        /// 提现申请时间
        /// </summary>
        public DateTime createTime { get; set; }
        /// <summary>
        /// 审核通过时间
        /// </summary>
        public DateTime certTime { get; set; }
        /// <summary>
        /// 转款时间
        /// </summary>
        public DateTime transferTime { get; set; }
        /// <summary>
        /// 用户确认收款时间
        /// </summary>
        public DateTime completeTime { get; set; }
        /// <summary>
        /// 状态 枚举
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 状态 枚举
        /// </summary>
        public string statusname { get; set; } 
    }
}
