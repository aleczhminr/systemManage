using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Enum
{
    /// <summary>
    /// 兑换日志
    /// </summary>
    public static class ExchangeLogEnum
    {
        /// <summary>
        /// 导出类别
        /// </summary>
        public enum State : int
        {
            等待发货 = 0,
            已发货 = 1,
            处理成功 = 2,
            处理失败 = 3
        }
    }
}
