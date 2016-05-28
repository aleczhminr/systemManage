using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class DashBoardModel
    {
        /// <summary>
        /// 初始化列表
        /// </summary>
        public DashBoardModel()
        {
            Data = new Dictionary<string, int>();
            //AccIdList = new Dictionary<string, List<int>>();
        }

        /// <summary>
        /// DashBoard指标名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// DashBoard总计数
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// DashBoard数据字典
        /// </summary>
        public Dictionary<string, int> Data { get; set; }

        //public Dictionary<string, List<int>> AccIdList { get; set; }
    }
}
