using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class SourceFilterModel
    {
        public string SourceName { get; set; }
        public BehaviorSet SourceData { get; set; }
    }

    /// <summary>
    /// 各渠道漏斗关注的动作数据
    /// </summary>
    public class BehaviorSet
    {
        public int Initialed { get; set; }
        public int Reg { get; set; }
        public int Login { get; set; }
        public int DataInput { get; set; }
        public int SaleInput { get; set; }
        public int SecRetention { get; set; }
        public int Paid { get; set; }
        public int Active { get; set; }
    }
}
