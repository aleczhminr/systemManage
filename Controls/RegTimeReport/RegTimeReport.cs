using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.RegTimeReport
{
    public static class RegTimeReport
    {
        public static HourAnalysisDataM RegTimeReportSource(int timeType, string[] types)
        {
            HourAnalysisDataM hourModel = new HourAnalysisDataM();
            Dictionary<string, HourAnalysisDataList> dataList = new Dictionary<string, HourAnalysisDataList>();
            
            foreach (string typeItem in types)
            {
                dataList[typeItem] = new HourAnalysisDataList();
                Dictionary<int, HourAnalysisItemList> hailList = new Dictionary<int, HourAnalysisItemList>();
                for (int i = 0; i < 24; i++)
                {
                    hailList[i] = new HourAnalysisItemList();
                    hailList[i].hour = i;
                }
                List<HourAnalysisItemList> returnList = new List<HourAnalysisItemList>();
                if (typeItem == "loginnum")
                {
                    returnList = T_LOGBLL.GetLogRegTimeReportSourceBL(timeType);//获取list
                }
                else if (typeItem == "salenum")
                {
                    returnList = T_LOGBLL.GetSaleRegTimeReportSourceBL(timeType);//获取list
                }
                else if (typeItem == "regnum")
                {
                    returnList = T_LOGBLL.GetRegRegTimeReportSourceBL(timeType);//获取list
                }
                else if (typeItem == "clientnum")
                {
                    returnList = T_LOGBLL.GetClientRegTimeReportSourceBL(timeType);//获取list
                }
                foreach (HourAnalysisItemList hail in returnList)
                {
                    hailList[hail.hour] = hail;
                }
                HourAnalysisDataList hadl = new HourAnalysisDataList();
                hadl.type = typeItem;
                hadl.ItemList = hailList.Values.ToList();

                dataList[typeItem] = hadl;
            }
            hourModel.DataList = dataList;
            hourModel.count = dataList.Count();
            return hourModel;
        }        
    }
}
