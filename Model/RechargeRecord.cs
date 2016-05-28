using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class RechargeRecord
    {
        public int Id { get; set; }
        public int AccId { get; set; }
        public string OrderNo { get; set; }
        public int State { get; set; }
        /// <summary>
        /// 充值面值
        /// </summary>
        public int CardNum { get; set; }
        /// <summary>
        /// 实际支付金额
        /// </summary>
        public decimal RealNum { get; set; }
        /// <summary>
        /// 用户支付金额
        /// </summary>
        public decimal PaidNum { get; set; }
        /// <summary>
        /// 补贴差额
        /// </summary>
        public decimal GapNum { get; set; }
        public string CardName { get; set; }
        public DateTime AddTime { get; set; }
        public int Oid { get; set; }
    }


    public class RecordSummary
    {
        /// <summary>
        /// 成功订单笔数
        /// </summary>
        public int SuccessCnt { get; set; }
        /// <summary>
        /// 失败笔数
        /// </summary>
        public int FailCnt { get; set; }
        /// <summary>
        /// 用户支付总数
        /// </summary>
        public decimal UsrPaid { get; set; }
        /// <summary>
        /// 我们支付总数
        /// </summary>
        public decimal WePaid { get; set; }
        /// <summary>
        /// 差额总数
        /// </summary>
        public decimal GapSum { get; set; }

        public int AccidCount { get; set; }
    }
}
