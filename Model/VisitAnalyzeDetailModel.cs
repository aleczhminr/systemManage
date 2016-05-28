using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class VisitAnalyzeDetailModel
    {
        public VisitAnalyzeDetailModel()
        {
            detailList = new List<VisitAnalyzeDetail>();
        }

        public List<VisitAnalyzeDetail> detailList { get; set; }
    }

    public class VisitAnalyzeDetail
    {
        public int? accid { get; set; }
        public string accName { get; set; }
        public string otherSoft { get; set; }
        public string useCause { get; set; }
        public string otherSource { get; set; }
    }
}
