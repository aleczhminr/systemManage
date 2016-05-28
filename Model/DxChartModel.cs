using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class DxChartModel
    {
        public DxChartModel()
        {
            DataList = new List<DxChartData>();
        }

        public List<DxChartData> DataList { get; set; }
    }

    public class DxChartData
    {
        public DxChartData()
        {
            Data = new Dictionary<string, decimal>();
        }

        public DxChartData(string date)
        {
            Date = date;
            Data = new Dictionary<string, decimal>();
        }

        /// <summary>
        /// 日期
        /// </summary>
        public string Date { get; set; }
        /// <summary>
        /// 数据列表
        /// </summary>
        public Dictionary<string, decimal> Data { get; set; }

    }
}
