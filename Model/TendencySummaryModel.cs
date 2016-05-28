using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class TendencySummaryModel
    {
        public TendencySummaryModel(string name)
        {
            ColName = name;
            NowNum = 0;
            FormerNum = 0;
            Ratio = "0";
        }

        /// <summary>
        /// 列名
        /// </summary>
        public string ColName { get; set; }
        /// <summary>
        /// 当前时间段数量
        /// </summary>
        public int NowNum { get; set; }
        /// <summary>
        /// 前一个时间段数量
        /// </summary>
        public int FormerNum { get; set; }
        /// <summary>
        /// 增长比例
        /// </summary>
        public string Ratio { get; set; }
    }
}
