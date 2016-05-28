using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class VisitPeriodModel
    {
        /// <summary>
        /// 店铺ID
        /// </summary>
        public int Accid { get; set; }

        /// <summary>
        /// 最后一次回访时间
        /// </summary>
        public DateTime LastVisitTime { get; set; }
        /// <summary>
        /// 最近一次的回访方式
        /// </summary>
        public string VisitWay { get; set; }
        /// <summary>
        /// 最后一次回访截止至目前的时间
        /// </summary>
        public int Period { get; set; }
    }
}
