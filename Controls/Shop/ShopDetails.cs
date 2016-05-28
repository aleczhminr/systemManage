using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BLL;
using Controls.MessageCenter;
using Utility;

namespace Controls.Shop
{
    public class ShopDetails
    {

        /// <summary>
        /// 得到店铺的ID 和 注册 时间
        /// </summary>
        /// <param name="accid"></param>
        /// <returns></returns>
        public static dynamic GetAccIdAndRegTime(int accid)
        {
            List<DapperWhere> sqlWhere = new List<DapperWhere>();
            sqlWhere.Add(new DapperWhere("id", accid));
            List<dynamic> list = BLL.Base.T_AccountBaseBLL.GetList<dynamic>(1, "id,regTime", sqlWhere, " id desc");
            if (list != null)
            {
                return list[0];
            }
            else
            {
                return null;
            }
        }

        public static VisitPeriodModel GetVisitPeriod(int accid)
        {
            return Sys_VisitInfoBLL.GetVisitPeriod(accid);
        }

        /// <summary>
        /// 得到店铺基本信息
        /// </summary>
        /// <param name="accid"></param>
        /// <returns></returns>
        public static T_AccountBasic GetAccountBasic(int accid)
        {
            return T_AccountBLL.GetAccountBasic(accid);
        }
        /// <summary>
        /// 得到店铺登录账号列表
        /// </summary>
        /// <param name="accid"></param>
        /// <returns></returns>
        public static List<T_Account_UserBasic> GetAccountUserBasic(int accid)
        {
            return T_Account_UserBLL.GetAccountUserBasic(accid);
        }
        /// <summary>
        /// 得到店铺配置信息
        /// </summary>
        /// <param name="accid"></param>
        /// <returns></returns>
        public static T_AccountConfig.SmsBillingConfig GetAccountSmsBillingConfig(int accid)
        {
            return T_BusinessBLL.GetSmsBillingConfig(accid);
        }
        /// <summary>
        /// 得到店铺今天信息汇总
        /// </summary>
        /// <param name="accid"></param>
        /// <returns></returns>
        public static T_AccountSummarize.TodaySummarize GetAccountTodaySummarize(int accid)
        {
            return T_AccountBLL.GetAccountTodaySummarize(accid);
        }

        /// <summary>
        /// 得到店铺汇总信息（包含今日）
        /// <para>
        /// 此方便包含今日数据，如果为了快速显示请使用  GetAccountSummarize
        /// </para>
        /// </summary>
        /// <param name="accid"></param>
        /// <returns></returns>
        public static T_AccountSummarize.Summarize GetAccountAllSummarize(int accid)
        {
            T_AccountSummarize.Summarize summarize = SysRpt_ShopInfoBLL.GetAccountSummarize(accid);

            T_AccountSummarize.TodaySummarize todaySummarize = GetAccountTodaySummarize(accid);

            summarize.saleNum = summarize.saleNum + todaySummarize.saleNum;
            summarize.saleMoney = summarize.saleMoney + todaySummarize.saleMoney;
            summarize.userNum = summarize.userNum + todaySummarize.userNum;
            summarize.goodsNum = summarize.goodsNum + todaySummarize.goodsNum;
            summarize.orderNum = summarize.orderNum + todaySummarize.orderNum;
            summarize.orderMoney = summarize.orderMoney + todaySummarize.orderMoney;
            summarize.smsNum = summarize.smsNum + todaySummarize.smsNum;
            summarize.outlayNum = summarize.outlayNum + todaySummarize.outlayNum;
            summarize.outlayMoney = summarize.outlayMoney + todaySummarize.outlayMoney;
            summarize.couponNum = summarize.couponNum;
            summarize.useCouponNum = summarize.useCouponNum;

            return summarize;
        }

        public static string GetLogDetail(int accId)
        {
            string desc = "";

            Dictionary<string,int> dic=new Dictionary<string, int>()
            {
                {"Web端",0},
                {"PC端",0},
                {"Android",0},
                {"iPhone",0},
                {"iPad",0}
            };

            List<LogClientDic> list = SysRpt_ShopInfoBLL.GetLogClient(accId);
            foreach (var LogClientDic in list)
            {
                if (LogClientDic.Source == "0" || LogClientDic.Source == "1")
                {
                    dic["Web端"] += LogClientDic.Cnt;
                }
                else if (LogClientDic.Source.IndexOf('3') == 0)
                {
                    dic["PC端"] += LogClientDic.Cnt;
                }
                else if (LogClientDic.Source.IndexOf("Android") >= 0)
                {
                    dic["Android"] += LogClientDic.Cnt;
                }
                else if (LogClientDic.Source.IndexOf("iPad") >= 0)
                {
                    dic["iPad"] += LogClientDic.Cnt;
                }
                else if (LogClientDic.Source.IndexOf("iPhone") >= 0)
                {
                    dic["iPhone"] += LogClientDic.Cnt;
                }
            }

            foreach (var item in dic.Keys)
            {
                if (dic[item]!=0)
                {
                    desc += item + ":" + dic[item] + "   ";
                }
            }

            return desc;
        }

        public static string GetLatestLogClient(int accId)
        {
            return SysRpt_ShopInfoBLL.GetLatestLogClient(accId);
        }

        /// <summary>
        /// 得到店铺汇总信息（不含今日）
        /// <para>此方法为了快速显示，如果需要完成统计请使用  GetAccountAllSummarize </para>
        /// </summary>
        /// <param name="accid"></param>
        /// <returns></returns>
        public static T_AccountSummarize.Summarize GetAccountSummarize(int accid)
        {
            return SysRpt_ShopInfoBLL.GetAccountSummarize(accid);
        }
        /// <summary>
        /// 得到一个店铺的权限列表
        /// </summary>
        /// <param name="accid"></param>
        /// <returns></returns>
        public static AppList GetAccountAppList(int accid)
        {
            var appModel = new AppList();
            appModel.Data = t_App_AuBLL.GetListByAccId(accid);

            var appList = new Dictionary<int, string>();
            appList.Add(1, "手机橱窗");
            appList.Add(2, "优惠券");
            appList.Add(3, "销售挂单");
            appList.Add(4, "百姓网");
            appList.Add(5, "分店功能");
            appList.Add(6, "多计次卡");
            appList.Add(7, "商品自定义属性");
            appList.Add(9, "工资计算");
            appList.Add(10, "支付宝收款");
            appList.Add(17, "专家版");
            appList.Add(18, "明细模板");
            appList.Add(19, "微信营销");
            appList.Add(21, "手机串号");
            //信息处理
            if (appModel.Rows > 0)
            {
                var iList = new List<int>();
                foreach (var item in appModel.Data)
                {
                    iList.Add(item.AppKey);
                    if (item.AppKey == 1 || item.AppKey == 2)
                    {
                        if (item.Status == 1)
                        {
                            item.DisplayStatus = "付费版本";
                            item.ActiveStatus = 2;
                        }
                        else
                        {
                            item.DisplayStatus = "试用版本";
                            item.ActiveStatus = 1;
                        }
                    }
                    else
                    {
                        item.DisplayStatus = "正常使用";
                        item.ActiveStatus = 2;
                    }

                    if (item.ActiveStatus == 2)
                    {
                        TimeSpan ts = item.EndDate - DateTime.Now;
                        if (ts.Days > 0)
                        {
                            item.LastDays = ts.Days;
                        }
                        else
                        {
                            item.LastDays = ts.Days;
                            item.DisplayStatus = "版本到期";
                            item.ActiveStatus = -1;
                        }
                    }
                }

                foreach (var item in appList)
                {
                    if (!iList.Contains(item.Key))
                    {
                        var model = new AppInfoModel();
                        model.AppKey = item.Key;
                        model.AppName = item.Value;
                        model.DisplayStatus = "未开通";
                        model.ShortUrl = "";
                        model.ActiveStatus = 0;
                        model.Remark = "";
                        appModel.Data.Add(model);
                    }
                }
            }
            else
            {
                var newModels = new List<AppInfoModel>();
                foreach (var item in appList)
                {
                    var model = new AppInfoModel();
                    model.AppKey = item.Key;
                    model.AppName = item.Value;
                    model.DisplayStatus = "未开通";
                    model.ShortUrl = "";
                    model.ActiveStatus = 0;
                    model.Remark = "";
                    newModels.Add(model);
                }
                appModel.Data = newModels;
            }



            return appModel;
        }

        /// <summary>
        /// 得到店铺的优惠券列表
        /// </summary>
        /// <param name="accid"></param>
        /// <returns></returns>
        public static List<ShopOrderCoupon> GetAccountCoupon(int accid)
        {
            return T_Order_CouponListBLL.GetCouponByAccId(accid);
        }
        /// <summary>
        /// 得到一个店铺的订单
        /// </summary>
        /// <param name="accid"></param>
        /// <returns></returns>
        public static DetailOrder GetAccountOrder(int accid)
        {
            List<dynamic> orderList = T_OrderInfoBLL.GetListByAccId(accid);
            string str = "";

            OrderItemProp propModel = new OrderItemProp();
            DetailOrder orderModel = new DetailOrder();            

            foreach (dynamic item in orderList)
            {
                str = item.orderPayType.ToString();
                switch (str)
                {
                    case "0":
                        item.orderPayType = "平台订单";
                        break;
                    case "1":
                        item.orderPayType = "线下订单";
                        break;
                    case "2":
                        item.orderPayType = "支付宝";
                        break;
                    case "3":
                        item.orderPayType = "财付通";
                        break;
                    case "4":
                        item.orderPayType = "快钱";
                        break;
                    case "5":
                        item.orderPayType = "微信支付";
                        break;
                    case "6":
                        item.orderPayType = "IOS支付";
                        break;
                    case "7":
                        item.orderPayType = "安卓支付";
                        break;
                }

                if (item.orderTypeId != 2)
                {
                    OrderItemProp tempModel = T_Order_businessBLL.GetListItemProps(Convert.ToInt32(item.busId), accid);

                    if (propModel.isFreeSms == 1 && tempModel.isFreeSms == 1)
                    {
                        propModel.Profit += Convert.ToDouble(item.RealPayMoney);
                        propModel.SumMoney += Convert.ToDouble(item.RealPayMoney);
                    }
                    else
                    {
                        propModel.Cost += tempModel.Cost;
                        propModel.Profit += Convert.ToDouble(item.RealPayMoney);
                        propModel.SumMoney += Convert.ToDouble(item.RealPayMoney) - tempModel.Cost;
                        propModel.isFreeSms = tempModel.isFreeSms;
                    }
                }
                else
                {
                    propModel.Cost += Convert.ToDouble(item.busSumMoney);
                    propModel.Profit += Convert.ToDouble(item.RealPayMoney);
                    propModel.SumMoney += Convert.ToDouble(item.RealPayMoney) - Convert.ToDouble(item.busSumMoney);
                }                
            }

            propModel.Cost = Math.Round(propModel.Cost, 2);
            propModel.Profit = Math.Round(propModel.Profit, 2);
            propModel.SumMoney = Math.Round(propModel.SumMoney, 2);

            orderModel.DataList = orderList;
            orderModel.SumInfo = propModel;

            return orderModel;
        }


        /// <summary>
        /// 得到一个店铺的标签
        /// </summary>
        /// <param name="accid"></param>
        /// <returns></returns>
        public static List<Sys_TagInfoBasic> GetAccountTag(int accid)
        {
            return Sys_TagNexusBLL.GetTagNexusByAccId(accid);
        }

        /// <summary>
        /// 得到所有标签列表
        /// </summary>
        /// <returns></returns>
        public static List<Sys_TagInfoBasic> GetSysTagList()
        {
            return Sys_TagInfoBLL.GetAllList();
        }
        /// <summary>
        /// 增加标签
        /// </summary>
        /// <param name="tagName"></param>
        /// <param name="insertName"></param>
        /// <returns></returns>
        public static int AddSysTag(string tagName, string insertName)
        {
            return Sys_TagInfoBLL.Add(tagName, insertName);
        }


        /// <summary>
        /// 一个店铺增加标签
        /// </summary>
        /// <param name="accid"></param>
        /// <param name="tagid"></param>
        /// <param name="inserName"></param>
        /// <returns></returns>
        public static int AddAccountTag(int accid, int tagid, string inserName)
        {

            Sys_TagNexus model = new Sys_TagNexus();
            model.tag_id = tagid;
            model.acc_id = accid;
            model.insertName = inserName;
            model.insertTime = DateTime.Now;
            model.id = Sys_TagNexusBLL.Add(model);
            if (model.id > 0)
            {
                return model.id;
            }
            else if (model.id == -1)
            {
                return -1;
            }
            else
            {
                return -2;
            }
        }
        /// <summary>
        /// 移除标签
        /// </summary>
        /// <param name="accid"></param>
        /// <param name="tagid"></param>
        /// <returns></returns>
        public static bool RemoveAccountTag(int accid, int tagid)
        {
            return Sys_TagNexusBLL.RemoveTag(accid, tagid);
        }
        /// <summary>
        /// 增加新标签
        /// </summary>
        /// <param name="accid"></param>
        /// <param name="tagName"></param>
        /// <param name="insertName"></param>
        /// <returns></returns>
        public static int AddShopSysNewTag(int accid, string tagName, string insertName)
        {
            int tagid = AddSysTag(tagName, insertName);
            if (tagid > 0)
            {
                int r = AddAccountTag(accid, tagid, insertName);
                if (r > 0)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                return 2;
            }
        }
        /// <summary>
        /// 得到店铺的销售汇总，不包含今天
        /// </summary>
        /// <param name="accid"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static List<dynamic> GetSale(int accid, DateTime startTime, DateTime endTime)
        {
            List<DapperWhere> sqlWhere = new List<DapperWhere>();

            sqlWhere.Add(new DapperWhere("statTime", startTime, "dayDate>=@statTime"));
            sqlWhere.Add(new DapperWhere("endTime", endTime, "dayDate<=@endTime"));
            sqlWhere.Add(new DapperWhere("accountid", accid));
            return BLL.Base.SysRpt_ShopDayInfoBaseBLL.GetList<dynamic>(0, "id,dayDate,saleNum,saleMoney", sqlWhere, "dayDate desc");
        }
        /// <summary>
        /// 得到 单个店铺的销售信息
        /// </summary>
        /// <param name="accid"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="dataType"></param>
        /// <returns></returns>
        public static Model.chartDataModel GetSaleChartData(int accid, DateTime startTime, DateTime endTime, int dataType)
        {
            List<dynamic> list = new List<dynamic>();
            List<DapperWhere> sqlWhere = new List<DapperWhere>();

            sqlWhere.Add(new DapperWhere("statTime", startTime, "dayDate>=@statTime"));
            sqlWhere.Add(new DapperWhere("endTime", endTime, "dayDate<=@endTime"));
            sqlWhere.Add(new DapperWhere("accountid", accid));

            string columnName = "id,dayDate,saleNum,memSaleNum,retailSaleNum";


            list = BLL.Base.SysRpt_ShopDayInfoBaseBLL.GetList<dynamic>(0, columnName, sqlWhere, "dayDate desc");

            chartDataModel chartModel = new chartDataModel();
            chartModel.captionTitle = "每日销售统计";

            DateTime forDateTime = startTime;
            //得到所有X轴
            while (forDateTime <= endTime)
            {
                if (forDateTime < DateTime.Now.Date)
                {
                    #region 得到所有X轴
                    charDataList charItemList = new charDataList();

                    charItemList.XLable = forDateTime.ToString("MM-dd");
                    charItemList.weekend =(int) forDateTime.DayOfWeek;

                    if (dataType == 1 || dataType == 4)//总销售
                    {
                        charItemList.ItemList["总销售"] = new charDataItemList(0)
                        {
                            series = "总销售"
                        };
                    }
                    if (dataType == 2 || dataType == 4)//会员
                    {
                        charItemList.ItemList["会员销售"] = new charDataItemList(0)
                        {
                            series = "会员销售"
                        };
                    }
                    if (dataType == 3 || dataType == 4)//零售
                    {
                        charItemList.ItemList["零售销售"] = new charDataItemList(0)
                        {
                            series = "零售销售"
                        };
                    }
                    chartModel.DataList[charItemList.XLable] = charItemList;
                    #endregion
                }
                forDateTime = forDateTime.AddDays(1);
            }



            foreach (dynamic item in list)
            {
                #region 给X轴增值
                charDataList charItemList = new charDataList();

                var XLable = Convert.ToDateTime(item.dayDate).ToString("MM-dd");


                if (dataType == 1 || dataType == 4)//总销售
                {
                    chartModel.DataList[XLable].ItemList["总销售"].Values = item.saleNum;
                }
                if (dataType == 2 || dataType == 4)//会员
                {
                    chartModel.DataList[XLable].ItemList["会员销售"].Values = item.memSaleNum;
                }
                if (dataType == 3 || dataType == 4)//零售
                {
                    chartModel.DataList[XLable].ItemList["零售销售"].Values = item.retailSaleNum;
                }
                #endregion
            }
            return chartModel;
        }

        /// <summary>
        /// 得到短信列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="accid"></param>
        /// <returns></returns>
        public static Dictionary<string, object> GetSmsList(int pageIndex, int pageSize, int accid,int? status=null,string phone="",DateTime? starttime=null,DateTime? endtime=null)
        {

            Dictionary<string, object> list = new Dictionary<string, object>();

            List<DapperWhere> sqlWhere = new List<DapperWhere>();
            sqlWhere.Add(new DapperWhere("accId", accid));

            if (status != null)
            {
                sqlWhere.Add(new DapperWhere("smsStatus", status));
            }

            if (starttime != null)
            {
                sqlWhere.Add(new DapperWhere("startTime",Convert.ToDateTime(starttime).Date, " sendtime>=@startTime "));
            }
            if (endtime != null)
            {
                DateTime end = Convert.ToDateTime(endtime);
                end = end.Date.Add(new TimeSpan(23, 59, 59));
                sqlWhere.Add(new DapperWhere("endtime", end, " sendtime<=@endtime "));
            }
            if (phone != "")
            {
                sqlWhere.Add(new DapperWhere("phoneNum", phone, " phoneNum like '%'+ @phoneNum +'%'"));
            }


            if (pageSize < 1)
            {
                pageSize = 20;
            }

            int rowCount = 0;
            if (pageIndex == 1)
            {
                rowCount = BLL.Base.T_Sms_ListBaseBLL.GetCount(sqlWhere);
            }
            int maxPage = 0;
            if (rowCount > 0)
            {
                maxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(rowCount) / pageSize));
            }

            list["rowCount"] = rowCount;
            list["maxPage"] = maxPage;
            list["pageIndex"] = pageIndex;
            list["listData"]= BLL.T_Sms_ListBLL.GetList(pageIndex, pageSize, sqlWhere, "id desc");

            return list;
        }


        /// <summary>
        /// 得到商品信息
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="accid"></param>
        /// <returns></returns>
        public static Dictionary<string, object> GetGoodsinfo(int pageIndex, int pageSize, int accid, string goodsname = "", DateTime? starttime = null, DateTime? endtime = null)
        {

            Dictionary<string, object> list = new Dictionary<string, object>();

            List<DapperWhere> sqlWhere = new List<DapperWhere>();
            sqlWhere.Add(new DapperWhere("accId", accid));

            if (starttime != null)
            {
                sqlWhere.Add(new DapperWhere("startTime", Convert.ToDateTime(starttime).Date, " gAddTime>=@startTime "));
            }
            if (endtime != null)
            {
                DateTime end = Convert.ToDateTime(endtime);
                end = end.Date.Add(new TimeSpan(23, 59, 59));
                sqlWhere.Add(new DapperWhere("endtime", end, " gAddTime<=@endtime "));
            }

            if (goodsname != "")
            {
                sqlWhere.Add(new DapperWhere("gName", goodsname, " gName like '%'+ @gName +'%' "));
            }



            if (pageSize < 1)
            {
                pageSize = 20;
            }

            int rowCount = 0;
            if (pageIndex == 1)
            {
                rowCount = BLL.Base.T_GoodsInfoBaseBLL.GetCount(sqlWhere);
            }
            int maxPage = 0;
            if (rowCount > 0)
            {
                maxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(rowCount) / pageSize));
            }

            list["rowCount"] = rowCount;
            list["maxPage"] = maxPage;
            list["pageIndex"] = pageIndex;
            list["listData"] = BLL.T_GoodsInfoBLL.GetBaseList(pageIndex, pageSize, sqlWhere, " gid desc");

            return list;
        }

        /// <summary>
        /// 支出信息
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="accid"></param>
        /// <returns></returns>
        public static Dictionary<string, object> GetPayRecord(int pageIndex, int pageSize, int accid, string name = "", DateTime? starttime = null, DateTime? endtime = null)
        {
            Dictionary<string, object> list = new Dictionary<string, object>();
            List<DapperWhere> sqlWhere = new List<DapperWhere>();
            sqlWhere.Add(new DapperWhere("ShopperId", accid));


            if (starttime != null)
            {
                sqlWhere.Add(new DapperWhere("startTime", Convert.ToDateTime(starttime).Date, " PayDate>=@startTime "));
            }
            if (endtime != null)
            {
                DateTime end = Convert.ToDateTime(endtime);
                end = end.Date.Add(new TimeSpan(23, 59, 59));
                sqlWhere.Add(new DapperWhere("endtime", end, " PayDate<=@endtime "));
            }

            if (name != "")
            {
                sqlWhere.Add(new DapperWhere("PayName", name, " PayName like '%'+ @PayName +'%' "));
            } 
            if (pageSize < 1)
            {
                pageSize = 20;
            }

            int rowCount = 0;
            if (pageIndex == 1)
            {
                rowCount = BLL.Base.t_PayRecordBaseBLL.GetCount(sqlWhere);
            }
            int maxPage = 0;
            if (rowCount > 0)
            {
                maxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(rowCount) / pageSize));
            }

            list["rowCount"] = rowCount;
            list["maxPage"] = maxPage;
            list["pageIndex"] = pageIndex;
            list["listData"] = BLL.t_PayRecordBLL.GetBasicList(pageIndex, pageSize, sqlWhere, " id desc");

            return list;
        }

        /// <summary>
        /// 店铺浏览器
        /// </summary>
        /// <param name="accountid"></param>
        /// <param name="statTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static List<dynamic> GetBrslastGroup(int accountid, DateTime statTime, DateTime endTime)
        {
            return BLL.T_LOGBLL.GetBrslastGroup(accountid, statTime, endTime);
        }

        /// <summary>
        /// 店铺成长图标
        /// </summary>
        /// <param name="accid"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="dataType"></param>
        /// <returns></returns>
        public static chartDataModel GetGrowingChart(int accountid, string[] dataType, DateTime? startTime=null, DateTime? endTime=null)
        {
            return BLL.SysRpt_ShopDayInfoBLL.GetGrouwthList(accountid, dataType, startTime, endTime);
        }
        /// <summary>
        /// 开通店铺权限
        /// </summary>
        /// <param name="accid">店铺ID</param>
        /// <param name="appKey">权限KEY</param>
        /// <param name="operId">操作人ID</param>
        /// <returns></returns>
        public static int AddAccountApp(int accid, int appKey, int operId)
        {
            t_App_Au model = new t_App_Au();

            model.accid = accid;
            model.appkey = appKey;
            model.appName = Enum.GetName(typeof(Model.Enum.AppAuEnum.AppName), appKey);
            model.stattime = DateTime.Now;
            model.endtime = DateTime.Now.AddMonths(6);
            model.aa_time = DateTime.Now;
            model.aa_remark = "（后台" + operId + "授权）";
            model.aa_Status = 0;
            if (appKey == 1 || appKey == 2)
            {
                string rm = CommonLib.Helper.CreateUUID();
                T_AccountBLL.UpdateRandomNumber(accid, rm);
                tb_user_inforBLL.InitializeUserInfor(accid);
            }

            if (appKey == 7)
            {
                #region 颜色尺码
                List<DapperWhere> whereList = new List<DapperWhere>();
                whereList.Add(new DapperWhere("accId", accid));
                whereList.Add(new DapperWhere("gaType", 1));
                List<T_Goods_Attribute> attributeList = new List<T_Goods_Attribute>();
                attributeList = BLL.Base.T_Goods_AttributeBaseBLL.GetList(whereList);
                if (attributeList.Count < 2)
                {
                    Dictionary<string, int> attr = new Dictionary<string, int>();
                    attr["颜色"] = 0;
                    attr["尺码"] = 0;

                    foreach (T_Goods_Attribute item in attributeList)
                    {
                        attr[item.gaName] = 1;
                    }

                    foreach (KeyValuePair<string, int> ik in attr)
                    {
                        if (ik.Value == 0)
                        {
                            T_Goods_Attribute addModel = new T_Goods_Attribute();
                            addModel.gaName = ik.Key;
                            addModel.gaType = 1;
                            addModel.gaRemark = "";
                            addModel.gaTime = DateTime.Now;
                            addModel.gaAlive = 1;
                            addModel.accId = accid;
                            T_Goods_AttributeBLL.Add(addModel);
                        }
                    }

                }
                #endregion
            }
            if (appKey == 21)
            {
                #region 手机串号
                List<DapperWhere> whereList = new List<DapperWhere>();
                whereList.Add(new DapperWhere("accId", accid));
                whereList.Add(new DapperWhere("gaType", 2));
                List<T_Goods_Attribute> attributeList = new List<T_Goods_Attribute>();
                attributeList = BLL.Base.T_Goods_AttributeBaseBLL.GetList(whereList);
                if (attributeList.Count < 2)
                {
                    Dictionary<string, int> attr = new Dictionary<string, int>();
                    attr["串号"] = 0;

                    foreach (T_Goods_Attribute item in attributeList)
                    {
                        attr[item.gaName] = 1;
                    }

                    foreach (KeyValuePair<string, int> ik in attr)
                    {
                        if (ik.Value == 0)
                        {
                            T_Goods_Attribute addModel = new T_Goods_Attribute();
                            addModel.gaName = ik.Key;
                            addModel.gaType = 2;
                            addModel.gaRemark = "";
                            addModel.gaTime = DateTime.Now;
                            addModel.gaAlive = 0;
                            addModel.accId = accid;
                            T_Goods_AttributeBLL.Add(addModel);
                        }
                    }

                }
                #endregion
            }

            return t_App_AuBLL.Add(model);
        }

        /// <summary>
        /// 开通支付宝处理
        /// </summary>
        /// <param name="accId"></param>
        /// <param name="alipayAccount"></param>
        /// <param name="alipayPid"></param>
        /// <param name="alipayKey"></param>
        /// <returns></returns>
        public static int AddAccountAlipay(int accId, string alipayAccount, string alipayPid, string alipayKey)
        {
            AlipayUserInfo infoModel = new AlipayUserInfo();
            var model = T_AccountBLL.GetAccountBasic(accId);

            infoModel.AccId = accId;
            infoModel.AccName = model.UserRealName;
            infoModel.AliAccount = alipayAccount;
            infoModel.AliKey = alipayKey;
            infoModel.AliPid = alipayPid;
            infoModel.PhoneNum = model.PhoneNumber;

            return T_PaymentInfoBLL.Add(infoModel);

        }

        /// <summary>
        /// 移除店铺app授权
        /// </summary>
        /// <param name="accid"></param>
        /// <param name="appkey"></param>
        /// <returns></returns>
        public static bool RemoveAccountApp(int accid, int appkey)
        {
            return t_App_AuBLL.RemoveApp(appkey, accid);
        }

        /// <summary>
        /// 绑定优惠券
        /// <para>{-1:处理错误，0：优惠券不存在，1：优惠券已使用或者已经作废，2：绑定成功}</para>
        /// </summary>
        /// <param name="accountid">店铺ID</param>
        /// <param name="CouponID">优惠券编号</param>
        /// <returns></returns>
        public static int BingingCoupon(int accountid, string CouponID)
        {
            int reVal = T_Order_CouponListBLL.BindingAccount(accountid, CouponID);
            //绑定成功推送消息提醒
            if (reVal==2)
            {
                var model = T_Order_CouponListBLL.GetCouponInfoByCode(CouponID);
                if (model!=null)
                {
                    try
                    {
                        #region Kafka Message 优惠券到账后

                        AfterBindCoupon cModel = new AfterBindCoupon();
                        cModel.EventId = 4;
                        cModel.AccId = accountid;

                        cModel.CouponDesc = model.couponDesc;
                        cModel.EndDate = model.endDate;
                        string specModel = CommonLib.Helper.JsonSerializeObject(cModel);

                        KafkaMessage mSend = new KafkaMessage();
                        mSend.SendMsg(4, specModel);

                        #endregion

                        ////绑定礼金券 推送模板消息
                        //CommonLib.SystemAPI sysApi = new CommonLib.SystemAPI();
                        //sysApi.CouponBindingNotifyPushMessage(accId, couponListModel.CouponDesc, couponListModel.EndDate);
                    }
                    catch (Exception ex)
                    {
                        Logger.Error("绑定礼金券推送模板消息错误", ex);
                    }
                }
            }

            return reVal;
        }

        /// <summary>
        /// 更新店铺收集信息列
        /// </summary>
        /// <param name="accid">店铺ID</param>
        /// <param name="column">列表名称</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static int UpdateAccountCollectInfo(int accid, string column, object value)
        {
            #region 列名转化
            switch (column)
            {
                case "QQ":
                    column = "a_QQ";
                    break;
                case "WeiXin":
                    column = "a_WeiXin";
                    break;
                case "Tel":
                    column = "a_Tel";
                    break;
                case "Address":
                    column = "a_Address";
                    break;
                case "Industry":
                    column = "a_Industry";
                    break;
                case "Name":
                    column = "a_Name";
                    break;
                case "IdentityNumber":
                    column = "a_IdentityNumber";
                    break;
                case "ShopSize":
                    column = "a_ShopSize";
                    break;
                case "Operate":
                    column = "a_Operate";
                    break;
                case "Duration":
                    column = "a_Duration";
                    break;
                case "OtherSoftware":
                    column = "a_OtherSoftware";
                    break;
                case "Remark":
                    column = "a_Remark";
                    break;
                default:
                    column = "";
                    break;
            }
            #endregion
            if (column == "")
            {
                return -1;
            }
            else if (Sys_AccountBLL.UpdateColumn(accid, column, value))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// 得到一个店铺的信息
        /// </summary>
        /// <param name="accid"></param>
        /// <returns></returns>
        public static SysAccountInfo GetSysAccountInfo(int accid)
        {
            return Sys_AccountBLL.GetAccountInfo(accid);
        }
        /// <summary>
        /// 得到此店铺相关的分店 及 总店信息  如果需要 可以 转成对像
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static  Dictionary<string,object> GetSubbranchList(int id)
        {
            Dictionary<string, object> list = new Dictionary<string, object>();

            List<dynamic> branch = T_AccountBLL.GetSubbranchInfo(id);
            dynamic head = T_AccountBLL.GetHeadquartersInfo(id);

            list["subbranch"] = null;
            list["headquerters"] = null;

            if (branch != null && branch.Count > 0)
            {
                list["subbranch"] = branch;
                list["headquerters"] = T_AccountBLL.GetHeadquartersInfo(Convert.ToInt32(branch[0].ID));
            }

            if (head != null)
            {
                list["headquerters"] = head;
                list["subbranch"] = T_AccountBLL.GetSubbranchInfo(Convert.ToInt32(head.ID));
            }

            return list;
        }

        /// <summary>
        /// 修改手机橱窗模板
        /// </summary>
        /// <param name="accid"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool UpdateHtmlTemes(int accid, int val)
        {
            return tb_user_inforBLL.UpdateHtmlTemes(accid, val);
        }

        /// <summary>
        /// 得到标签问题
        /// </summary>
        /// <returns></returns>
        public static List<SysTagTypeBasic> GetTagQuestions(int accid, int[] excludTypeList)
        {
            return Sys_TagInfoBLL.GetTagQuestions(accid, excludTypeList);
        }
        /// <summary>
        /// 得到需要回访的回访列表
        /// </summary>
        /// <param name="accid"></param>
        /// <returns></returns>
        public static List<SysNeedVisitModel> GetNeedVisitList(int accid)
        {
            var list = Sys_VisitInfoBLL.GetNeedVisitList(accid);
            return list;
        }
        /// <summary>
        /// 得到一个店铺的最后登录来源
        /// </summary>
        /// <param name="accid"></param>
        /// <returns></returns>
        public static T_LogAccountLast GetAccountLastLogSource(int accid)
        {
            return T_LOGBLL.GetAccountLastLogSource(accid);
        }
         
    }
}
