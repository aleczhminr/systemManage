using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    //string pieJson = "{\"chart\":{\"caption\":\"订单类型分析\", \"showpercentvalues\":\"1\",\"baseFontSize\":\"12\"}, \"data\":[" + pieData.ToString().Trim(',') + "]} ";
    public class FusionPieModel
    {
        public FusionPieModel()
        {
            chart = new ChartSummary();
            data = new List<PieData>();
        }

        public ChartSummary chart { get; set; }
        public List<PieData> data { get; set; }

    }

    public class ChartSummary
    {
        public string caption { get; set; }
        public string showpercentvalues { get; set; }
        public string baseFontSize { get; set; }
    }

    //pieData.Append("{\"label\":\"" + dr.busName + "\",\"value\":" + GetBaiFen(Convert.ToDouble(dr.money), AllMoney) + ",\"toolText\":\"" + dr.busName + "{br}" + GetBaiFen(Convert.ToDouble(dr.money), AllMoney) + "%\"},");
    public class PieData
    {
        public string label { get; set; }
        public string value { get; set; }
        public string toolText { get; set; }        
    }

    public class OrderPartition
    {
        public decimal self { get; set; }
        public decimal other { get; set; }
    }
}
