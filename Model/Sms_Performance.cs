using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class SmsPerformance
    {
        public SmsPerformance()
        {
            dataList = new List<SmsCheckTime>();
        }

        public List<SmsCheckTime> dataList { get; set; }
    }

    public class SmsCheckTime
    {
        public string Name { get; set; }
        public string Longest { get; set; }
        public string Shortest { get; set; }
        public string Average { get; set; }
        public decimal Ratio { get; set; }
    }
}
