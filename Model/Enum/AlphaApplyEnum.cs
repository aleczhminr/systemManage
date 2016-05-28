using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Enum
{
    public static class AlphaApplyEnum
    {
        /// <summary>
        /// 内测状态
        /// </summary>
        public enum AlphaStatus
        {
            已申请 = 0,
            审核通过 = 1,
            审核不通过 = 2,
            提前申请结束 = 3,
            正常内测结束 = 4
        }
    }
}
