using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Enum
{
    public static class WithdrawalEnum
    {
        /// <summary>
        /// 提现状态
        /// </summary>
        public enum WithdrawalStatus : int
        {
            已申请 = 0,
            审核通过 = 1,
            已发放 = 2,
            提现成功 = 3,
            过期自动默认确认收款 = 4,
            审核不通过 = 5
        }
    }
}
