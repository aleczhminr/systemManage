using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class VennSetsModel
    {
        public VennSetsModel()
        {
            SetsList = new List<VennSet>();
        }

        /// <summary>
        /// 页面集合元素的集合
        /// </summary>
        public List<VennSet> SetsList { get; set; }
    }

    public class VennSet
    {
        public VennSet()
        {
            SetsElements = new List<string>();
            SetsDetail = new List<int>();
        }

        public VennSet(List<string> list,int count)
        {
            SetsElements = list;
            SetsCount = count;
            SetsDetail = new List<int>();
        }

        public VennSet(List<string> list, int count, List<int> dList)
        {
            SetsElements = list;
            SetsCount = count;
            SetsDetail = dList;
        }

        /// <summary>
        /// 集合数组
        /// </summary>
        public List<string> SetsElements { get; set; }
        /// <summary>
        /// 集合数
        /// </summary>
        public int SetsCount { get; set; }
        /// <summary>
        /// 集合详情数据
        /// </summary>
        public List<int> SetsDetail { get; set; }
    }

    public class ActiveUsrList
    {
        public ActiveUsrList()
        {
            ActiveAccids = new List<DailyActiveList>();
        }
        public List<DailyActiveList> ActiveAccids { get; set; }
    }

    public class DailyActiveList
    {
        public DailyActiveList()
        {
            AccidList = new List<int>();
        }

        public DailyActiveList(DateTime dt, List<int> list)
        {
            DayDate = dt;
            AccidList = list;
        }

        public DateTime DayDate { get; set; }
        public List<int> AccidList { get; set; }
    }
}
