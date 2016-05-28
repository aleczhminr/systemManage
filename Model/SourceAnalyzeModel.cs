using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 来源信息对像
    /// </summary>
    public class SourceAnalyzeModel
    {
        public SourceAnalyzeModel()
        {
            ItemList = new Dictionary<string, SourceAnalyzeItemList>();
        }
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime DateTime { get; set; }
        /// <summary>
        /// 列表
        /// </summary>
        public Dictionary<string, SourceAnalyzeItemList> ItemList { get; set; }
        /// <summary>
        /// 总合
        /// </summary>
        public decimal CountValue { get; set; }
        /// <summary>
        /// 总数
        /// </summary>
        public int count { get; set; }
    }
    /// <summary>
    /// 来源信息列表对像
    /// </summary>
    public class SourceAnalyzeItemList
    {
        public SourceAnalyzeItemList()
        { 
        }
        public SourceAnalyzeItemList(int val)
        {
            CountValue = val;
        }
        /// <summary>
        /// 来源
        /// </summary>
        public int SourceId { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public decimal CountValue { get; set; }
        /// <summary>
        /// 比率
        /// </summary>
        public decimal ValueScore { get; set; }
        /// <summary>
        ///  是否周末判断
        /// </summary>
        public string weekend { get; set; }

        public string DetailSource { get; set; }
    }
}
