using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 预设KPI数据对比MODEL
    /// </summary>
    public class KPICompareModel
    {
        public KPICompareModel(string name, int realCnt, int preCnt, string per)
        {
            SeriesName = name;
            RealCount = realCnt;
            PresetCount = preCnt;
            Percent = per;
        }

        /// <summary>
        /// 系列名称
        /// </summary>
        public string SeriesName { get; set; }
        /// <summary>
        /// 真实数据
        /// </summary>
        public int RealCount { get; set; }
        /// <summary>
        /// 预设指标
        /// </summary>
        public int PresetCount { get; set; }
        /// <summary>
        /// 真实数据占比
        /// </summary>
        public string Percent { get; set; }
    }

    public class MonReviewModel
    {
        public MonReviewModel()
        {
            MonthlyData = new List<KPICompareModel>();
        }

        public string Month { get; set; }

        public List<KPICompareModel> MonthlyData { get; set; }
    }
}
