using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using BLL;

namespace Controls.DataAnalyze
{
    public static class DataAnalyze
    {
        /// <summary>
        /// 用于生成日期组合集合的静态List(General)
        /// </summary>
        public static List<string> SetEnumList = new List<string>();
        public static string GetVennData(string type, DateTime stDate, DateTime edDate)
        {
            ActiveUsrList accidListModel = DashBoardAnalyzeBLL.GetVennUsrList(type, stDate, edDate);
            VennSetsModel setsModel = new VennSetsModel();
            VennSet vennSet = new VennSet();
            List<int> tempAccidList = new List<int>();

            int comCount = 0;

            List<string> strList = new List<string>();
            DateTime iterDate = stDate;

            while (iterDate < edDate)
            {
                strList.Add(iterDate.ToShortDateString());
                iterDate = iterDate.AddDays(1);
            }

            Dictionary<string, int> dic = new Dictionary<string, int>();
            for (int i = 0; i < strList.Count; i++)
            {
                SetEnumList.Add(strList[i]);
                dic.Add(strList[i], i);
            }

            //递归获取所有日期组合的集合
            Combination(dic, strList);

            //初始化日期组合的集合
            foreach (string str in SetEnumList)
            {
                vennSet.SetsElements = str.Split(',').ToList();
                comCount = vennSet.SetsElements.Count;
                if (comCount == 1)
                {
                    try
                    {
                        vennSet.SetsCount = accidListModel.ActiveAccids.Find(x => x.DayDate.ToShortDateString() == vennSet.SetsElements[0]).AccidList.Count;
                        vennSet.SetsDetail = accidListModel.ActiveAccids.Find(x => x.DayDate.ToShortDateString() == vennSet.SetsElements[0]).AccidList;
                    }
                    catch (Exception ex)
                    {
                        vennSet.SetsCount = 0;
                        vennSet.SetsDetail = new List<int>();
                    }
                }
                else
                {
                    try
                    {
                        tempAccidList = accidListModel.ActiveAccids.Find(x => x.DayDate.ToShortDateString() == vennSet.SetsElements[0]).AccidList;
                        for (int i = 1; i < comCount; i++)
                        {
                            tempAccidList = tempAccidList.Intersect(accidListModel.ActiveAccids.Find(x => x.DayDate.ToShortDateString() == vennSet.SetsElements[i]).AccidList).ToList();
                        }
                        vennSet.SetsCount = tempAccidList.Count;
                        vennSet.SetsDetail = tempAccidList;
                    }
                    catch (Exception ex)
                    {
                        vennSet.SetsDetail = new List<int>();
                        vennSet.SetsCount = 0;
                    }
                }

                setsModel.SetsList.Add(new VennSet(vennSet.SetsElements, vennSet.SetsCount, vennSet.SetsDetail));
            }

            SetEnumList = new List<string>();
            return CommonLib.Helper.JsonSerializeObject(setsModel);
        }

        /// <summary>
        /// 获取初始日期列表的所有组合
        /// </summary>
        /// <param name="dd"></param>
        /// <param name="initList"></param>
        static void Combination(Dictionary<string, int> dd, List<string> initList)
        {
            Dictionary<string, int> dic = new Dictionary<string, int>();
            foreach (KeyValuePair<string, int> kv in dd)
            {
                for (int i = kv.Value + 1; i < initList.Count; i++)
                {
                    SetEnumList.Add(kv.Key + "," + initList[i]);
                    dic.Add(kv.Key + "," + initList[i], i);
                }
            }
            if (dic.Count > 0)
                Combination(dic, initList);
        }
    }
}
