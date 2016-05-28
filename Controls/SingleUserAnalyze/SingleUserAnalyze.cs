using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using Model;

namespace Controls.SingleUserAnalyze
{
    public static class SingleUserAnalyze
    {
        public static string GetSingleUsrAnalyze(int accId)
        {
            Model.SingleUserAnalyze analyzeModel = SingleUserAnalyzeBLL.GetUsrAnalyze(accId);
            analyzeModel.AccId = accId;

            analyzeModel.RegTimeStr = analyzeModel.RegTime.ToString("yyyy-MM-dd");

            //用户活跃度判断
            if (analyzeModel.ActiveStatus < 0)
            {
                analyzeModel.ActiveDescription = "流失";
            }
            else if (analyzeModel.ActiveStatus == 0)
            {
                analyzeModel.ActiveDescription = "新注册";
            }
            else if (analyzeModel.ActiveStatus > 0 && analyzeModel.ActiveStatus < 5)
            {
                analyzeModel.ActiveDescription = "需关怀";
            }
            else if (analyzeModel.ActiveStatus == 5)
            {
                analyzeModel.ActiveDescription = "活跃";
            }
            else
            {
                analyzeModel.ActiveDescription = "忠诚";
            }

            //判断用户商品录入的方式
            if (analyzeModel.GoodsNum == 0)
            {
                analyzeModel.GoodsInput = "尚未录入商品";
            }
            else if ((analyzeModel.GoodsNum - analyzeModel.GoodsNumWithBarCode) > analyzeModel.GoodsNumWithBarCode)
            {
                analyzeModel.GoodsInput = "手工录入";
            }
            else
            {
                analyzeModel.GoodsInput = "条码录入";
            }

            //判断销售最多的时间段
            if (!string.IsNullOrEmpty(analyzeModel.MaxSaleTimeStr))
            {
                string[] time = analyzeModel.MaxSaleTimeStr.Split(',');

                switch (time.Length)
                {
                    case 2:
                        analyzeModel.MaxSaleTime1 = "每日" + time[1] + "时";
                        analyzeModel.MaxSaleTime2 = "无记录";
                        analyzeModel.MaxSaleTime3 = "无记录";
                        break;
                    case 3:
                        analyzeModel.MaxSaleTime1 = "每日" + time[1] + "时";
                        analyzeModel.MaxSaleTime2 = "每日" + time[2] + "时";
                        analyzeModel.MaxSaleTime3 = "无记录";
                        break;
                    case 4:
                        analyzeModel.MaxSaleTime1 = "每日" + time[1] + "时";
                        analyzeModel.MaxSaleTime2 = "每日" + time[2] + "时";
                        analyzeModel.MaxSaleTime3 = "每日" + time[3] + "时";
                        break;
                    case 1:
                    default:
                        break;
                }

            }
            else
            {
                analyzeModel.MaxSaleTime1 = "尚未有销售记录";
                analyzeModel.MaxSaleTime2 = "尚未有销售记录";
                analyzeModel.MaxSaleTime3 = "尚未有销售记录";
            }



            if (!string.IsNullOrEmpty(analyzeModel.Source))
            {
                switch (analyzeModel.Source)
                {
                    case "30":
                        analyzeModel.Source = "Android";
                        break;
                    case "21":
                        analyzeModel.Source = "IOS";
                        break;
                    case "22":
                        analyzeModel.Source = "PC";
                        break;
                    case "23":
                        analyzeModel.Source = "SEM";
                        break;
                    case "24":
                        analyzeModel.Source = "WEB";
                        break;
                    default:
                        analyzeModel.Source = "WEB";
                        break;
                }
            }
            else
            {
                analyzeModel.Source = "Unknown";
            }

            List<DateTime> saleTimeList = SingleUserAnalyzeBLL.GetSaleDateInterval(accId);
            double totalSpan = 0;
            if (saleTimeList.Count > 1)
            {
                analyzeModel.SuccessDeal = saleTimeList.Count + "笔";
                for (int i = 0; i < saleTimeList.Count - 1; i++)
                {
                    TimeSpan ts = saleTimeList[i].Subtract(saleTimeList[i + 1]).Duration();
                    totalSpan += Convert.ToInt32(ts.TotalMinutes);
                }

                double span = totalSpan / (saleTimeList.Count - 1);
                if (span > 60)
                {
                    analyzeModel.Interval = Convert.ToInt32(span / 60) + "小时" + Convert.ToInt32(span % 60) + "分钟";
                }
                else
                {
                    analyzeModel.Interval = span.ToString("F2") + "分钟";
                }
            }
            else
            {
                analyzeModel.SuccessDeal = saleTimeList.Count + "笔";
                analyzeModel.Interval = "尚无销售成交间隔记录";
            }

            return CommonLib.Helper.JsonSerializeObject(analyzeModel);
        }

        public static string GetMostSaleList(int accId)
        {
            return CommonLib.Helper.JsonSerializeObject(SingleUserAnalyzeBLL.GetMostSaleList(accId));
        }

        public static string GetMostProfitList(int accId)
        {
            return CommonLib.Helper.JsonSerializeObject(SingleUserAnalyzeBLL.GetMostProfitList(accId));
        }
    }
}
