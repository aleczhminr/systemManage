using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class HourAnalysisDataM
    {
        public HourAnalysisDataM()
        {
            DataList = new Dictionary<string, HourAnalysisDataList>();

        }
        public Dictionary<string, HourAnalysisDataList> DataList { get; set; }

        public int count { get; set; }
    }

    public class HourAnalysisDataList
    {
        public HourAnalysisDataList()
        {
            ItemList = new List<HourAnalysisItemList>();
        }
        public List<HourAnalysisItemList> ItemList { get; set; }

        public string type { get; set; }
    }

    public class HourAnalysisItemList
    {

        public int hour { get; set; }

        public int num { get; set; }

    }
}
