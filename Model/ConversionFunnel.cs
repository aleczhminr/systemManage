using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ConversionFunnel
    {
        /// <summary>
        /// 注册用户数
        /// </summary>
        public int RegNum { get; set; }
        /// <summary>
        /// 活跃用户数
        /// </summary>
        public int ActiveNum { get; set; }
        /// <summary>
        /// 付费用户数
        /// </summary>
        public int PayNum { get; set; }
    }
}
