using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using BLL;
using Controls.Helper4Control;
using Model.Model4View;

namespace Controls.OrderAnalyze
{
    public class OrderAnalyze
    {
        public static string getOrderAnalyzeData(string type, string dataType, DateTime BgTime, DateTime EdTime)
        {
            string strJson = "";
            DateTime bgTime = new DateTime();
            DateTime edTime = new DateTime();

            if (dataType == "oth")
            {
                bgTime = BgTime;
                edTime = EdTime.AddHours(23).AddMinutes(59).AddSeconds(59);

            }
            else if (dataType == "month")
            {
                DateTime FirstDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                DateTime LastDay = FirstDay.AddMonths(1).AddDays(-1);

                bgTime = FirstDay;
                edTime = LastDay.AddHours(23).AddMinutes(59).AddSeconds(59);
            }
            else if (dataType == "lastmonth")
            {
                DateTime nowTime = DateTime.Now.AddMonths(-1);
                DateTime FirstDay = new DateTime(nowTime.Year, nowTime.Month, 1);
                DateTime LastDay = FirstDay.AddMonths(1).AddDays(-1);

                bgTime = FirstDay;
                edTime = LastDay.AddHours(23).AddMinutes(59).AddSeconds(59);
            }
            else if (dataType == "3month")
            {
                DateTime nowTime = DateTime.Now.AddMonths(-2);
                DateTime FirstDay = new DateTime(nowTime.Year, nowTime.Month, 1);
                DateTime LastDay = FirstDay.AddMonths(3).AddDays(-1);

                bgTime = FirstDay;
                edTime = LastDay.AddHours(23).AddMinutes(59).AddSeconds(59);
            }
            else if (dataType == "today")
            {
                bgTime = DateTime.Today.AddHours(0).AddMinutes(0).AddSeconds(0);
                edTime = DateTime.Today.AddHours(23).AddMinutes(59).AddSeconds(59);
            }


            OrderAnalysis orModel = T_Order_businessBLL.GetBussinessOrderAnalyse(bgTime, edTime);


            StringBuilder pieData = new StringBuilder();
            StringBuilder listData = new StringBuilder();

            if (orModel != null && orModel.itemList.Count > 0)
            {
                double AllNum = orModel.num;
                double AllQuantity = orModel.quantity;
                double AllsmsNum = orModel.smsNum;
                double AllaccNum = orModel.accNum;
                double AllMoney = orModel.money;

                foreach (OrderAnalysisItem dr in orModel.itemList)
                {
                    listData.Append("{\"busname\":\"" + SetBusName(dr.busName) + "\",\"num\":" + Convert.ToDouble(dr.num) + ",\"quantity\":" + Convert.ToDouble(dr.quantity) + ",\"smsNum\":" + Convert.ToDouble(dr.smsNum) + ",\"accNum\":" + Convert.ToDouble(dr.accNum) + ",\"money\":" + Convert.ToDouble(dr.money) + ",\"baifen\":" + GetBaiFen(Convert.ToDouble(dr.money), AllMoney) + ",\"busmclass\":\"" + dr.bus_mclass + "\"},");
                    pieData.Append("{\"label\":\"" + dr.busName + "\",\"value\":" + GetBaiFen(Convert.ToDouble(dr.money), AllMoney) + ",\"toolText\":\"" + dr.busName + "{br}" + GetBaiFen(Convert.ToDouble(dr.money), AllMoney) + "%\"},");
                }

                listData.Append("{\"busname\":\"合计\",\"num\":" + AllNum + ",\"quantity\":" + AllQuantity + ",\"smsNum\":" + AllsmsNum + ",\"accNum\":" + AllaccNum + ",\"money\":" + AllMoney + ",\"baifen\":100,\"busmclass\":\"总\"}");

                string pieJson = "{\"chart\":{\"caption\":\"订单类型分析\", \"showpercentvalues\":\"1\",\"baseFontSize\":\"12\"}, \"data\":[" + pieData.ToString().Trim(',') + "]} ";

                strJson = "{\"orderClass\":[" + listData.ToString() + "],\"pie\":" + pieJson + "}";
            }
            else
            {
                strJson = "none";
            }
            return strJson;
        }

        public static string SetBusName(string busName)
        {
            if (busName == "条短信包")
            {
                return "平台订单[短信]";
            }
            else if (busName == "个月高级用户版")
            {
                return "平台订单[高级版]";
            }
            else
            {
                return busName;
            }
        }

        public static string GetBaiFen(double moeny, double AllMoney)
        {
            double bai = (moeny / AllMoney) * 100;
            return ControlHelper.formats(bai, 2).ToString();
        }

        public static string getOrderTrend(string dataType, string keyword, DateTime BgTime, DateTime EdTime)
        {
            DateTime bgTime = new DateTime();
            DateTime edTime = new DateTime();

            if (dataType == "oth")
            {
                bgTime = BgTime;
                edTime = EdTime.AddHours(23).AddMinutes(59).AddSeconds(59);

            }
            else if (dataType == "month")
            {
                DateTime FirstDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                DateTime LastDay = FirstDay.AddMonths(1).AddDays(-1);

                bgTime = FirstDay;
                edTime = LastDay.AddHours(23).AddMinutes(59).AddSeconds(59);
            }
            else if (dataType == "lastmonth")
            {
                DateTime nowTime = DateTime.Now.AddMonths(-1);
                DateTime FirstDay = new DateTime(nowTime.Year, nowTime.Month, 1);
                DateTime LastDay = FirstDay.AddMonths(1).AddDays(-1);

                bgTime = FirstDay;
                edTime = LastDay.AddHours(23).AddMinutes(59).AddSeconds(59);
            }
            else if (dataType == "3month")
            {
                DateTime nowTime = DateTime.Now.AddMonths(-2);
                DateTime FirstDay = new DateTime(nowTime.Year, nowTime.Month, 1);
                DateTime LastDay = FirstDay.AddMonths(3).AddDays(-1);

                bgTime = FirstDay;
                edTime = LastDay.AddHours(23).AddMinutes(59).AddSeconds(59);
            }
            else if (dataType == "today")
            {
                bgTime = DateTime.Today.AddHours(0).AddMinutes(0).AddSeconds(0);
                edTime = DateTime.Today.AddHours(23).AddMinutes(59).AddSeconds(59);
            }

            List<dynamic> ds = T_Order_businessBLL.GetSingleBussinessOrder(bgTime, edTime, keyword);

            SingleOrderUsrType chartModel = new SingleOrderUsrType();

            if (ds != null)
            {
                DateTime nowDay = DateTime.Now.Date;
                DateTime dayDate = new DateTime(1890, 1, 1);
                dayDate = bgTime;
                while (dayDate <= edTime)
                {
                    int newUsrCount = 0;
                    int oldUsrCount = 0;
                    UsrType dList = new UsrType();
                    dList.XLable = dayDate.ToString("yyyy-MM-dd");

                    List<dynamic> list = ds.Where(x => x.transactionDate == dayDate).ToList();

                    foreach (dynamic dr in list)
                    {
                        if (dr.regTime != DateTime.MinValue)
                        {
                            DateTime dt = Convert.ToDateTime(dr.regTime);
                            if (dt.Year == dayDate.Year && dt.Month == dayDate.Month)
                            {
                                newUsrCount += Convert.ToInt32(dr.busSumMoney);
                            }
                            else
                            {
                                oldUsrCount += Convert.ToInt32(dr.busSumMoney);
                            }
                        }
                    }

                    dList.date = dayDate;
                    dList.newUsr = newUsrCount;
                    dList.oldUsr = oldUsrCount;

                    if (dList.date != DateTime.Now.Date)
                    {
                        chartModel.usrType.Add(dList);
                    }

                    dayDate = dayDate.AddDays(1);
                }

                return CommonLib.Helper.JsonSerializeObject(chartModel);
            }
            else
            {
                return "[]";
            }
        }

        public static string GetNewUsrOrder(DateTime stDate, DateTime edDate)
        {
            NewUsrOrder model = new NewUsrOrder();
            List<NewOrderProduct> list = T_OrderInfoBLL.GetNewUsrOrder(stDate, edDate);
            List<int> accidList = new List<int>();

            int allAcc = 0;

            List<string> prodList = list.Select(x => x.displayName).Distinct().ToList();

            foreach (string pName in prodList)
            {
                NewUsrOrderItem item = new NewUsrOrderItem();
                accidList = list.Where(x => x.displayName == pName).Select(x => x.accId).ToList();

                item.ProductName = pName;
                item.UsrCount = accidList.Count;
                item.AccidList = CommonLib.Helper.JsonSerializeObject(accidList);

                model.DataList.Add(item);
            }

            //if (allAcc != 0)
            //{
            //    foreach (NewUsrOrderItem item in model.DataList)
            //    {
            //        item.Ratio = (item.UsrCount*100/allAcc).ToString("f2") + "%";
            //    }
            //}

            model.DataList = model.DataList.OrderBy(x => x.UsrCount).ToList();
            model.DataList.Reverse();

            return CommonLib.Helper.JsonSerializeObject(model);
        }

        public static string GetOrderType(string dataType, DateTime BgTime, DateTime EdTime)
        {
            FusionPieModel model = new FusionPieModel();

            string strJson = "";

            DateTime bgTime = new DateTime();
            DateTime edTime = new DateTime();

            if (dataType == "oth")
            {
                bgTime = BgTime;
                edTime = EdTime.AddHours(23).AddMinutes(59).AddSeconds(59);

            }
            else if (dataType == "month")
            {
                DateTime FirstDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                DateTime LastDay = FirstDay.AddMonths(1).AddDays(-1);

                bgTime = FirstDay;
                edTime = LastDay.AddHours(23).AddMinutes(59).AddSeconds(59);
            }
            else if (dataType == "lastmonth")
            {
                DateTime nowTime = DateTime.Now.AddMonths(-1);
                DateTime FirstDay = new DateTime(nowTime.Year, nowTime.Month, 1);
                DateTime LastDay = FirstDay.AddMonths(1).AddDays(-1);

                bgTime = FirstDay;
                edTime = LastDay.AddHours(23).AddMinutes(59).AddSeconds(59);
            }
            else if (dataType == "3month")
            {
                DateTime nowTime = DateTime.Now.AddMonths(-2);
                DateTime FirstDay = new DateTime(nowTime.Year, nowTime.Month, 1);
                DateTime LastDay = FirstDay.AddMonths(3).AddDays(-1);

                bgTime = FirstDay;
                edTime = LastDay.AddHours(23).AddMinutes(59).AddSeconds(59);
            }
            else if (dataType == "today")
            {
                bgTime = DateTime.Today.AddHours(0).AddMinutes(0).AddSeconds(0);
                edTime = DateTime.Today.AddHours(23).AddMinutes(59).AddSeconds(59);
            }

            List<OrderInfoModel> list = T_OrderInfoBLL.GetOrderInfoByDate(bgTime, edTime, 0);
            double self = 0;
            double other = 0;
            double otherServ = 0;
            //int count = 0;//调试信息

            foreach (var item in list)
            {
                OrderItemProp tempModel = T_Order_businessBLL.GetListItemProps(Convert.ToInt32(item.busId), item.accId);
                if (tempModel.SelfMark=="1")
                {
                    self += Convert.ToDouble(item.RealPayMoney);
                }
                else if (tempModel.PureSms=="1")
                {
                    otherServ += Convert.ToDouble(item.RealPayMoney);
                    //count++;
                }
                else
                {
                    other += Convert.ToDouble(item.RealPayMoney);
                }
            }

            //获取实物商品相关统计(实物商品计入他营产品)
            List<OrderInfoModel> MaterialList = T_OrderInfoBLL.GetOrderInfoByDate(bgTime, edTime, 1);
            if (MaterialList != null && MaterialList.Count > 0)
            {
                foreach (var item in MaterialList)
                {
                    other += Convert.ToDouble(item.RealPayMoney);
                }
            }

            //获取京东订单那相关统计(订单商品计入他营产品)
            List<OrderInfoModel> JdList = T_OrderInfoBLL.GetOrderInfoByDate(bgTime, edTime, 2);
            if (MaterialList != null && MaterialList.Count > 0)
            {
                foreach (var item in MaterialList)
                {
                    other += Convert.ToDouble(item.RealPayMoney);
                }
            }

            //OrderPartition orderMoney = T_OrderInfoBLL.GetOrderType(bgTime, edTime);

            double allMoney = self + other + otherServ;

            model.chart.caption = "产品类型比例";
            model.chart.baseFontSize = "12";
            model.chart.showpercentvalues = "1";

            PieData servData = new PieData();
            servData.label = "第三方服务";
            servData.value = GetBaiFen(Convert.ToDouble(otherServ), allMoney);
            servData.toolText = "第三方服务，" + GetBaiFen(Convert.ToDouble(otherServ), allMoney) + "% (￥" + ControlHelper.formats(otherServ, 2).ToString() + ")";

            model.data.Add(servData);

            PieData selfData = new PieData();
            selfData.label = "Saas服务";
            selfData.value = GetBaiFen(Convert.ToDouble(self), allMoney);
            selfData.toolText = "Saas服务，" + GetBaiFen(Convert.ToDouble(self), allMoney) + "% (￥" + ControlHelper.formats(self, 2).ToString() + ")";

            model.data.Add(selfData);

            PieData otherData = new PieData();
            otherData.label = "他营";
            otherData.value = GetBaiFen(Convert.ToDouble(other), allMoney);
            otherData.toolText = "他营，" + GetBaiFen(Convert.ToDouble(other), allMoney) + "% (￥" + ControlHelper.formats(other, 2).ToString() + ")";

            model.data.Add(otherData);

            return CommonLib.Helper.JsonSerializeObject(model);
        }
    }
}
