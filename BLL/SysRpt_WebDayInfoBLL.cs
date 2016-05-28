using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;

namespace BLL
{
    /// <summary>
    /// 每日信息统计 
    /// </summary>
    public static class SysRpt_WebDayInfoBLL
    {
        private static readonly Dictionary<string, string> SeriesDict = new Dictionary<string, string>
        {
            {"newShop", "新增店铺"},
            {"android","Android新增"},
            {"ios","IOS新增"},
            {"yesterShop","昨日新增店铺"},
            {"yesterAndroid","昨日Android新增"},
            {"yesterIOS","昨日IOS新增"},
            {"newUser","新增会员"},
            {"yesterUser","昨日新增会员"},
            {"newGoods","新增商品"},
            {"yesterGoods","昨日新增商品"},
            {"sell","销售笔数"},
            {"yesterSell","昨日销售笔数"},
            {"sellNum","销售金额"},
            {"yesterSellNum","昨日销售金额"},
            {"saleGoods","销售商品"},
            {"yesterSaleGoods","昨日销售商品"},
            {"pay","支出笔数"},
            {"yesterPay","昨日支出笔数"},
            {"payNum","支出金额"},
            {"yesterPayNum","昨日支出金额"},
            {"sms","当日短信"},
            {"yesterSms","昨日短信"},
            {"order","订单笔数"},
            {"yesterOrder","昨日订单笔数"},
            {"orderNum","订单金额"},
            {"yesterOrderNum","昨日订单金额"},
            {"client","客户端登录"},
            {"yesterClient","昨日客户端登录"},
            {"logNum","登录数"},
            {"yesterLogNum","昨日登录数"}
        };

        public static chartDataModel GetDailyGraph(string type, DateTime date, string source = "all")
        {
            SysRpt_WebDayInfoDAL dayInfo = new SysRpt_WebDayInfoDAL();

            chartDataModel chartData = new chartDataModel();
            Dictionary<string, charDataList> charItemList = new Dictionary<string, charDataList>();
            List<dynamic> list = dayInfo.GetIndexNowData(type, date);
            List<dynamic> listYesterday = dayInfo.GetIndexNowData(type, date.AddDays(-1));

            //Dictionary<string, charDataItemList> charDataItem =
            //new Dictionary<string, charDataItemList>();
            if (list != null && list.Count > 0)
            {
                DateTime hourTime = DateTime.Now;
                switch (type)
                {
                    //新增店铺
                    case "shop":
                        List<string> paramListShop = new List<string>();

                        if (source == "all")
                        {
                            paramListShop.Add("newShop");
                            paramListShop.Add("yesterShop");
                        }
                        else if (source == "android")
                        {
                            paramListShop.Add("android");
                            paramListShop.Add("yesterAndroid");
                        }
                        else if (source == "ios")
                        {
                            paramListShop.Add("ios");
                            paramListShop.Add("yesterIOS");
                        }

                        Dictionary<string, charDataList> charDataItemShop = InitChartData(paramListShop);

                        foreach (dynamic itemDataList in list)
                        {
                            var XLable = itemDataList.Period.ToString();
                            if (source == "all")
                            {
                                charDataItemShop[XLable].ItemList["newShop"].Values = itemDataList.shop;
                            }
                            else if (source == "android")
                            {
                                charDataItemShop[XLable].ItemList["android"].Values = itemDataList.android;
                            }
                            else if (source == "ios")
                            {
                                charDataItemShop[XLable].ItemList["ios"].Values = itemDataList.ios;
                            }
                        }

                        foreach (dynamic itemDataList in listYesterday)
                        {
                            var XLable = itemDataList.Period.ToString();
                            if (source == "all")
                                charDataItemShop[XLable].ItemList["yesterShop"].Values = itemDataList.shop;
                            else if (source == "android")
                                charDataItemShop[XLable].ItemList["yesterAndroid"].Values = itemDataList.android;
                            else if (source == "ios")
                                charDataItemShop[XLable].ItemList["yesterIOS"].Values = itemDataList.ios;
                        }

                        chartData.DataList = charDataItemShop;

                        break;
                    //新增会员
                    case "Reg":
                        List<string> paramListReg = new List<string>()
                        {
                            "newUser","yesterUser"
                        };
                        Dictionary<string, charDataList> charDataItemReg = InitChartData(paramListReg);

                        foreach (dynamic itemDataList in list)
                        {
                            var XLable = itemDataList.Period.ToString();
                            charDataItemReg[XLable].ItemList["newUser"].Values = itemDataList.Reg;
                        }

                        foreach (dynamic itemDataList in listYesterday)
                        {
                            var XLable = itemDataList.Period.ToString();
                            charDataItemReg[XLable].ItemList["yesterUser"].Values = itemDataList.Reg;
                        }

                        chartData.DataList = charDataItemReg;
                        break;
                    //新增商品
                    case "goods":
                        List<string> paramListGoods = new List<string>()
                        {
                            "newGoods","yesterGoods"
                        };
                        Dictionary<string, charDataList> charDataItemGoods = InitChartData(paramListGoods);

                        foreach (dynamic itemDataList in list)
                        {
                            var XLable = itemDataList.Period.ToString();
                            charDataItemGoods[XLable].ItemList["newGoods"].Values = itemDataList.goods;
                        }

                        foreach (dynamic itemDataList in listYesterday)
                        {
                            var XLable = itemDataList.Period.ToString();
                            charDataItemGoods[XLable].ItemList["yesterGoods"].Values = itemDataList.goods;
                        }

                        chartData.DataList = charDataItemGoods;
                        break;
                    //销售笔数和销售金额
                    case "Sell":
                        List<string> paramListSell = new List<string>()
                        {
                            "sell","yesterSell","sellNum","yesterSellNum"
                        };
                        Dictionary<string, charDataList> charDataItemSell = InitChartData(paramListSell);

                        foreach (dynamic itemDataList in list)
                        {
                            var XLable = itemDataList.Period.ToString();
                            charDataItemSell[XLable].ItemList["sell"].Values = itemDataList.Sell;
                            charDataItemSell[XLable].ItemList["sellNum"].Values = itemDataList.SellNum;
                        }

                        foreach (dynamic itemDataList in listYesterday)
                        {
                            var XLable = itemDataList.Period.ToString();
                            charDataItemSell[XLable].ItemList["yesterSell"].Values = itemDataList.Sell;
                            charDataItemSell[XLable].ItemList["yesterSellNum"].Values = itemDataList.SellNum;
                        }

                        chartData.DataList = charDataItemSell;
                        break;
                    //销售商品数
                    case "saleGoods":
                        List<string> paramListSaleGoods = new List<string>()
                        {
                            "saleGoods","yesterSaleGoods"
                        };
                        Dictionary<string, charDataList> charDataItemSaleGoods = InitChartData(paramListSaleGoods);

                        foreach (dynamic itemDataList in list)
                        {
                            var XLable = itemDataList.Period.ToString();
                            charDataItemSaleGoods[XLable].ItemList["saleGoods"].Values = itemDataList.saleGoods;
                        }

                        foreach (dynamic itemDataList in listYesterday)
                        {
                            var XLable = itemDataList.Period.ToString();
                            charDataItemSaleGoods[XLable].ItemList["yesterSaleGoods"].Values = itemDataList.saleGoods;
                        }

                        chartData.DataList = charDataItemSaleGoods;
                        break;
                    //当日支出
                    case "Pay":
                        List<string> paramListPay = new List<string>()
                        {
                            "pay","yesterPay","payNum","yesterPayNum"
                        };
                        Dictionary<string, charDataList> charDataItemPay = InitChartData(paramListPay);

                        foreach (dynamic itemDataList in list)
                        {
                            var XLable = itemDataList.Period.ToString();
                            charDataItemPay[XLable].ItemList["pay"].Values = itemDataList.Pay;
                            charDataItemPay[XLable].ItemList["payNum"].Values = itemDataList.PayNum;
                        }

                        foreach (dynamic itemDataList in listYesterday)
                        {
                            var XLable = itemDataList.Period.ToString();
                            charDataItemPay[XLable].ItemList["yesterPay"].Values = itemDataList.Pay;
                            charDataItemPay[XLable].ItemList["yesterPayNum"].Values = itemDataList.PayNum;
                        }

                        chartData.DataList = charDataItemPay;
                        break;
                    //当日短信
                    case "Sms":
                        List<string> paramListSms = new List<string>()
                        {
                            "sms","yesterSms"
                        };
                        Dictionary<string, charDataList> charDataItemSms = InitChartData(paramListSms);

                        foreach (dynamic itemDataList in list)
                        {
                            var XLable = itemDataList.Period.ToString();
                            charDataItemSms[XLable].ItemList["sms"].Values = itemDataList.Sms;
                        }

                        foreach (dynamic itemDataList in listYesterday)
                        {
                            var XLable = itemDataList.Period.ToString();
                            charDataItemSms[XLable].ItemList["yesterSms"].Values = itemDataList.Sms;
                        }

                        chartData.DataList = charDataItemSms;
                        break;
                    //当日订单
                    case "Order":
                        List<string> paramListOrder = new List<string>()
                        {
                            "order","yesterOrder","orderNum","yesterOrderNum"
                        };
                        Dictionary<string, charDataList> charDataItemOrder = InitChartData(paramListOrder);

                        foreach (dynamic itemDataList in list)
                        {
                            var XLable = itemDataList.Period.ToString();
                            charDataItemOrder[XLable].ItemList["order"].Values = itemDataList.Order;
                            charDataItemOrder[XLable].ItemList["orderNum"].Values = itemDataList.OrderNum;
                        }

                        foreach (dynamic itemDataList in listYesterday)
                        {
                            var XLable = itemDataList.Period.ToString();
                            charDataItemOrder[XLable].ItemList["yesterOrder"].Values = itemDataList.Order;
                            charDataItemOrder[XLable].ItemList["yesterOrderNum"].Values = itemDataList.OrderNum;
                        }

                        chartData.DataList = charDataItemOrder;
                        break;
                    //客户端登录
                    case "kehuduan":
                        List<string> paramListClient = new List<string>()
                        {
                            "client","yesterClient"
                        };
                        Dictionary<string, charDataList> charDataItemClient = InitChartData(paramListClient);

                        foreach (dynamic itemDataList in list)
                        {
                            var XLable = itemDataList.Period.ToString();
                            charDataItemClient[XLable].ItemList["client"].Values = itemDataList.kehuduan;
                        }

                        foreach (dynamic itemDataList in listYesterday)
                        {
                            var XLable = itemDataList.Period.ToString();
                            charDataItemClient[XLable].ItemList["yesterClient"].Values = itemDataList.kehuduan;
                        }

                        chartData.DataList = charDataItemClient;
                        break;
                    //登录
                    case "loginNum":
                        List<string> paramListLogNum = new List<string>()
                        {
                            "logNum","yesterLogNum"
                        };
                        Dictionary<string, charDataList> charDataItemLogNum = InitChartData(paramListLogNum);

                        foreach (dynamic itemDataList in list)
                        {
                            var XLable = itemDataList.Period.ToString();
                            charDataItemLogNum[XLable].ItemList["logNum"].Values = itemDataList.loginNum;
                        }

                        foreach (dynamic itemDataList in listYesterday)
                        {
                            var XLable = itemDataList.Period.ToString();
                            charDataItemLogNum[XLable].ItemList["yesterLogNum"].Values = itemDataList.loginNum;
                        }

                        chartData.DataList = charDataItemLogNum;
                        break;
                }
            }

            return chartData;
        }

        public static Dictionary<string, charDataList> InitChartData(List<string> paramList)
        {
            Dictionary<string, charDataList> charItemList = new Dictionary<string, charDataList>();

            for (int i = 0; i < 24; i++)
            {
                var XLable = i.ToString();
                charItemList[XLable] = new charDataList(XLable);

                Dictionary<string, charDataItemList> charDataItem =
                                new Dictionary<string, charDataItemList>();

                foreach (string param in paramList)
                {
                    charDataItem[param] = new charDataItemList()
                    {
                        Values = 0,
                        series = SeriesDict[param]
                    };
                }

                charItemList[XLable].ItemList = charDataItem;
            }

            return charItemList;
        }

        /// <summary>
        /// 得到当前系统今天的统计信息
        /// </summary>
        /// <param name="type">{新增店铺 "shop",新增会员 "Reg",新增商品 "goods",销售笔数和销售金额 "Sell",销售商品数 "saleGoods",当日支出 "Pay",当日短信 "Sms",当日订单 "Order",客户端登录 "kehuduan",登录 "loginNum"}</param>
        /// <returns></returns>
        public static List<dynamic> GetIndexNowData(string type, DateTime nowDay)
        {
            SysRpt_WebDayInfoDAL dal = new SysRpt_WebDayInfoDAL();
            return dal.GetIndexNowData(type, nowDay);
        }
        /// <summary>
        /// 得到某一天数据
        /// </summary>
        /// <param name="webDate">日期</param>
        /// <returns></returns>
        public static PanelShowModel.SomedayDataCount GetModelByDate(DateTime webDate)
        {
            SysRpt_WebDayInfoDAL dal = new SysRpt_WebDayInfoDAL();
            return dal.GetModelByDate(webDate);
        }
        /// <summary>
        /// 得到当前数据的汇总
        /// </summary>
        /// <param name="type"></param>
        /// <param name="nowDay"></param>
        /// <returns></returns>
        public static PanelShowModel.SomedayDataCount GetNowDataCount()
        {
            DateTime insertDate = DateTime.Now;
            PanelShowModel.SomedayDataCount todayModel = new PanelShowModel.SomedayDataCount();
            todayModel.nowTime = DateTime.Now.Date;
            //新注册
            todayModel.regNum = 0;
            List<dynamic> shopList = SysRpt_WebDayInfoBLL.GetIndexNowData("shop", insertDate);
            foreach (dynamic item in shopList)
            {
                todayModel.regNum += Convert.ToInt32(item.shop);
            }
            //销售笔
            todayModel.saleNum = 0;
            todayModel.saleMoney = 0;
            List<dynamic> saleList = SysRpt_WebDayInfoBLL.GetIndexNowData("Sell", insertDate);
            foreach (dynamic item in saleList)
            {
                todayModel.saleNum += Convert.ToInt32(item.Sell);
                todayModel.saleMoney += Convert.ToDecimal(item.SellNum);
            }


            //新增会员
            todayModel.userNum = 0;
            List<dynamic> userList = SysRpt_WebDayInfoBLL.GetIndexNowData("Reg", insertDate);
            foreach (dynamic item in userList)
            {
                todayModel.userNum += Convert.ToInt32(item.Reg);
            }


            //新增加商品
            todayModel.goodsNum = 0;
            List<dynamic> goodsList = SysRpt_WebDayInfoBLL.GetIndexNowData("goods", insertDate);
            foreach (dynamic item in goodsList)
            {
                todayModel.goodsNum += Convert.ToInt32(item.goods);
            }


            //短信数
            todayModel.smsNum = 0;
            List<dynamic> smsList = SysRpt_WebDayInfoBLL.GetIndexNowData("Sms", insertDate);
            foreach (dynamic item in smsList)
            {
                todayModel.smsNum += Convert.ToInt32(item.Sms);
            }

            //订单数
            todayModel.orderNum = 0;
            todayModel.orderMoney = 0;
            List<dynamic> orderList = SysRpt_WebDayInfoBLL.GetIndexNowData("Order", insertDate);
            foreach (dynamic item in orderList)
            {
                todayModel.orderNum += Convert.ToInt32(item.Order);
                todayModel.orderMoney += Convert.ToDecimal(item.OrderNum);
            }


            return todayModel;
        }
        /// <summary>
        /// 得到某日此时的数据
        /// </summary>
        /// <param name="webDate"></param>
        /// <returns></returns>
        public static PanelShowModel.SomedayDataCount GetThisMomentData(DateTime webDate)
        {
            SysRpt_WebDayInfoDAL dal = new SysRpt_WebDayInfoDAL();
            return dal.GetThisMomentData(webDate);
        }

        /// <summary>
        /// 返回昨日此时数据
        /// </summary>
        /// <returns></returns>
        public static PanelShowModel.SomedayDataCount GetYesterdayPeerData()
        {
            SysRpt_WebDayInfoDAL dal = new SysRpt_WebDayInfoDAL();
            return dal.GetYesterdayPeerData();
        }

        /// <summary>
        /// 得到某段时间内的平均数据
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static PanelShowModel.SomedayDataCount GetAverageData(DateTime startTime, DateTime endTime)
        {
            SysRpt_WebDayInfoDAL dal = new SysRpt_WebDayInfoDAL();
            return dal.GetAverageData(startTime, endTime);
        }

        public static List<dynamic> GetMonthlyData(DateTime dt)
        {
            SysRpt_WebDayInfoDAL dal = new SysRpt_WebDayInfoDAL();
            return dal.GetMonthlyData(dt);
        }

        public static dynamic GetAllData()
        {
            SysRpt_WebDayInfoDAL dal = new SysRpt_WebDayInfoDAL();
            return dal.GetAllData();
        }
    }
}
