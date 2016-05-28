using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ActiveChangeModel
    {
        public ActiveChangeModel()
        {
            dataList = new List<ActiveChangeUnit>();
        }

        /// <summary>
        /// 给定时间段内的具体数据列表
        /// </summary>
        public List<ActiveChangeUnit> dataList { get; set; }
    }

    public class ActiveChangeUnit
    {
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// 新增活跃
        /// </summary>
        public int NewActive { get; set; }
        /// <summary>
        /// 新增活跃用户列表
        /// </summary>
        public string NewAccids { get; set; }
        /// <summary>
        /// 流失活跃用户列表
        /// </summary>
        public string LostAccids { get; set; }
        /// <summary>
        /// 流失活跃
        /// </summary>
        public int LostActive { get; set; }
        /// <summary>
        /// 新增活跃和流失活跃的差值
        /// </summary>
        public int NetValue { get; set; }
        /// <summary>
        /// 新增活跃比流失活跃净增百分比
        /// </summary>
        public decimal Percent { get; set; }
        /// <summary>
        /// 每行的标识符字符串
        /// </summary>
        public string RowMark { get; set; }
        /// <summary>
        /// 是否为周末 -1周末 -0非周末
        /// </summary>
        public string Weekend { get; set; }
    }
}
