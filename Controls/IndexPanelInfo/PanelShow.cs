using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BLL;
using Model;
using Utility;

namespace Controls.IndexPanelInfo
{
    public static class PanelShow
    {
        /// <summary>
        /// 用于生成日期组合集合的静态List
        /// </summary>
        public static List<string> SetEnumList = new List<string>();

        /// <summary>
        /// 获取每日和前日的对比数据
        /// </summary>
        /// <param name="type"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public static string getTableData(string type, DateTime day, string source)
        {
            chartDataModel model = SysRpt_WebDayInfoBLL.GetDailyGraph(type, day,source);
            string jsData = CommonLib.Helper.JsonSerializeObject(model);

            return jsData;
        }

        /// <summary>
        /// 新增一条KPI预设数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string AddKpi(MonthlyKPI model)
        {
            int status = Sys_PresetKPIBLL.AddKpi(model);

            string tip = string.Empty;
            switch (status)
            {
                case 0:
                    tip = "数据处理出错！";
                    break;
                case 1:
                    tip = "添加成功！";
                    break;
                case 2:
                    tip = "更新成功！";
                    break;
            }

            return tip;
        }

        /// <summary>
        /// 删除一条KPI数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string DeleteKpi(int id)
        {
            int status = Sys_PresetKPIBLL.DeleteKpi(id);

            if (status==0)
            {
                return "没有找到该条数据！";
            }
            else
            {
                return "数据删除成功！";
            }
        }

        /// <summary>
        /// 获取KPI列表，默认参数为只获取列表
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public static List<MonthlyKPI> GetKpiList(int page)
        {
            return Sys_PresetKPIBLL.GetList(page, false, DateTime.Now);
        }

        /// <summary>
        /// 获取完成指标百分比
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GetMonthlyData(DateTime dt)
        {
            List<dynamic> list = SysRpt_WebDayInfoBLL.GetMonthlyData(dt);
            List<MonthlyKPI> listPreset = Sys_PresetKPIBLL.GetList(1, true, dt);

            List<KPICompareModel> modelList = new List<KPICompareModel>();

            if (list != null && list.Count > 0 && listPreset != null && listPreset.Count > 0)
            {
                modelList.Add(new KPICompareModel("注册量", Convert.ToInt32(list[0].Account), listPreset[0].RegNum,
                (Convert.ToInt32(list[0].Account) * 100 / listPreset[0].RegNum).ToString("#0.00")));

                modelList.Add(new KPICompareModel("销售笔数", Convert.ToInt32(list[0].Sale), listPreset[0].SellCount,
                    (Convert.ToInt32(list[0].Sale) * 100 / listPreset[0].SellCount).ToString("#0.00")));

                modelList.Add(new KPICompareModel("新增会员", Convert.ToInt32(list[0].Usr), listPreset[0].UsrAdd,
                    (Convert.ToInt32(list[0].Usr) * 100 / listPreset[0].UsrAdd).ToString("#0.00")));

                modelList.Add(new KPICompareModel("新增商品", Convert.ToInt32(list[0].Goods), listPreset[0].Sku,
                    (Convert.ToInt32(list[0].Goods) * 100 / listPreset[0].Sku).ToString("#0.00")));

                modelList.Add(new KPICompareModel("短信量", Convert.ToInt32(list[0].Sms), listPreset[0].Sms,
                    (Convert.ToInt32(list[0].Sms) * 100 / listPreset[0].Sms).ToString("#0.00")));

                //modelList.Add(new KPICompareModel("订单笔数", Convert.ToInt32(list[0].Ord), listPreset[0].OrderCount,
                    //(Convert.ToInt32(list[0].Ord) * 100 / listPreset[0].OrderCount).ToString("#0.00")));
                modelList.Add(new KPICompareModel("订单金额", Convert.ToInt32(list[0].ordMon), listPreset[0].OrderCount,
                    (Convert.ToInt32(list[0].ordMon) * 100 / listPreset[0].OrderCount).ToString("#0.00")));
            }


            return CommonLib.Helper.JsonSerializeObject(modelList);
        }

        public static string GetMonthlyReview()
        {
            List<MonReviewModel> monthList = new List<MonReviewModel>();
            List<MonthlyKPI> kpiModel = Sys_PresetKPIBLL.GetList(1, false, DateTime.Now);
            foreach (MonthlyKPI kpi in kpiModel)
            {
                MonReviewModel monthReview = new MonReviewModel();
                monthReview.Month = kpi.MDate.ToString("yyyy-MM");
                monthReview.MonthlyData =
                    CommonLib.Helper.JsonDeserializeObject<List<KPICompareModel>>(GetMonthlyData(kpi.MDate));

                monthList.Add(monthReview);
            }

            return CommonLib.Helper.JsonSerializeObject(monthList);
        }

               
        public static string GetAllData()
        {
            PanelShowModel.SomedayDataCount allData = new PanelShowModel.SomedayDataCount();
            dynamic all = SysRpt_WebDayInfoBLL.GetAllData();
            allData.nowTime = DateTime.Now;
            allData.regNum = Convert.ToInt32(all.Account);
            allData.saleNum = Convert.ToInt32(all.Sale);
            allData.userNum = Convert.ToInt32(all.Usr);
            allData.goodsNum = Convert.ToInt32(all.Goods);
            allData.smsNum = Convert.ToInt32(all.Sms);
            allData.orderNum = Convert.ToInt32(all.Ord);
            allData.orderMoney = Convert.ToDecimal(all.ordMon);

            return CommonLib.Helper.JsonSerializeObject(allData);
        }

        /// <summary>
        /// 今天昨天数据对比
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, object> GetTodayYesterdayContrast()
        {
            //得到今日数据
            PanelShowModel.SomedayDataCount todayModel = SysRpt_WebDayInfoBLL.GetNowDataCount();
            //得到昨天数据
            PanelShowModel.SomedayDataCount yestaredayModel = SysRpt_WebDayInfoBLL.GetModelByDate(DateTime.Now.AddDays(-1));

            if (yestaredayModel == null)
            {
                yestaredayModel = new PanelShowModel.SomedayDataCount(DateTime.Now.AddDays(-1));
            }
            if (todayModel == null)
            {
                todayModel = new PanelShowModel.SomedayDataCount(DateTime.Now);
            }
            todayModel.nowTimeString = "今天";
            yestaredayModel.nowTimeString = "昨天";
            Dictionary<string, object> list = new Dictionary<string, object>();
            list["today"] = todayModel;
            list["yestareday"] = yestaredayModel;

            return list;
        }
        /// <summary>
        /// 今天昨天对比更多
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, object> GetTodayYesterdayContrastMore()
        {
            //得到昨天此时数据
            PanelShowModel.SomedayDataCount yestaredayModel = SysRpt_WebDayInfoBLL.GetYesterdayPeerData();
            PanelShowModel.SomedayDataCount activ = SysRpt_WebDayInfoBLL.GetAverageData(DateTime.Now.AddDays(-30), DateTime.Now);

            yestaredayModel.nowTimeString = "昨日此时";
            activ.nowTimeString = "近30日平均";
            Dictionary<string, object> list = new Dictionary<string, object>();
            list["yestareday"] = yestaredayModel;
            list["average"] = activ;

            return list;
        }

        /// <summary>
        /// 返回今日昨日此时数据对比
        /// </summary>
        /// <returns></returns>
        public static string GetYesterdayNowData()
        {
            PanelShowModel.SomedayDataCount todayModel = SysRpt_WebDayInfoBLL.GetNowDataCount();
            PanelShowModel.SomedayDataCount yestaredayModel = SysRpt_WebDayInfoBLL.GetYesterdayPeerData();

            Dictionary<string, object> list = new Dictionary<string, object>();
            list["today"] = todayModel;
            list["yestareday"] = yestaredayModel;

            return CommonLib.Helper.JsonSerializeObject(list);
        }

        /// <summary>
        /// 返回当天登录用户数据
        /// </summary>
        /// <param name="nowDay"></param>
        /// <returns></returns>
        public static string GetLoginDashBoardData(DateTime nowDay)
        {
            var model = new DashBoardModel();
            var cacheKey = "GetLoginDashBoardData:" + nowDay.ToString("yyyyMMdd");
            var cacheResult = "";
            //Cache Redis
            cacheResult = RedisHelper.GetKey(cacheKey);
            if (cacheResult != null && cacheResult != "")
            {
                model = CommonLib.Helper.JsonDeserializeObject<DashBoardModel>(cacheResult);
            }
            else
            {
                model = DashBoardAnalyzeBLL.GetLoginDashBoardData(nowDay);
                var cacheExpire = 60 * 5;
                RedisHelper.SetKey(cacheKey, CommonLib.Helper.JsonSerializeObject(model), cacheExpire);
            }

            return CommonLib.Helper.JsonSerializeObject(model);
        }

        /// <summary>
        /// 返回当天活跃用户数据
        /// </summary>
        /// <param name="nowDay"></param>
        /// <returns></returns>
        public static string GetActiveDashBoardData(DateTime nowDay)
        {
            var model = new DashBoardModel();
            var cacheKey = "GetActiveDashBoardData:" + nowDay.ToString("yyyyMMdd");
            var cacheResult = "";
            //Cache Redis
            cacheResult = RedisHelper.GetKey(cacheKey);
            if (cacheResult != null && cacheResult != "")
            {
                model = CommonLib.Helper.JsonDeserializeObject<DashBoardModel>(cacheResult);
            }
            else
            {
                model = DashBoardAnalyzeBLL.GetActiveDashBoardData(nowDay);
                var cacheExpire = 60 * 60 * 6;
                RedisHelper.SetKey(cacheKey, CommonLib.Helper.JsonSerializeObject(model), cacheExpire);
            }

            return CommonLib.Helper.JsonSerializeObject(model);
        }

        /// <summary>
        /// 返回当天新增用户的使用数据
        /// </summary>
        /// <param name="nowDay"></param>
        /// <returns></returns>
        public static string GetNewUsrDashBoardData(DateTime nowDay)
        {
            DashBoardModel model = DashBoardAnalyzeBLL.GetNewUsrDashBoardData(nowDay);
            return CommonLib.Helper.JsonSerializeObject(model);
        }

        public static string GetNewUsrDashBoardDataDetail(DateTime nowDay)
        {
            DashBoardModel model = DashBoardAnalyzeBLL.GetNewUsrDashBoardDataDetail(nowDay);
            return CommonLib.Helper.JsonSerializeObject(model);
        }

        /// <summary>
        /// 返回当日新增用户的来源渠道
        /// </summary>
        /// <returns></returns>
        public static string GetRegUsr(DateTime date)
        {
            return CommonLib.Helper.JsonSerializeObject(DashBoardAnalyzeBLL.GetRegUsrDiff(date));
        }

        public static string GetLoginType(DateTime date)
        {
            var model = new LoginTypeDiff();
            var cacheKey = "GetLoginTypeDiffData:" + date.ToString("yyyyMMdd");
            var cacheResult = "";
            //Cache Redis
            cacheResult = RedisHelper.GetKey(cacheKey);
            if (cacheResult != null && cacheResult != "")
            {
                model = CommonLib.Helper.JsonDeserializeObject<LoginTypeDiff>(cacheResult);
            }
            else
            {
                model = DashBoardAnalyzeBLL.GetLoginTypeDiff(date);
                var cacheExpire = 60 * 5;
                RedisHelper.SetKey(cacheKey, CommonLib.Helper.JsonSerializeObject(model), cacheExpire);
            }

            return CommonLib.Helper.JsonSerializeObject(model);            
        }
        /// <summary>
        /// 返回KPI数据和区分用户活跃类型后的数据
        /// </summary>
        /// <param name="nowDay"></param>
        /// <returns></returns>
        public static string GetStatusType(DateTime nowDay)
        {
            List<DashBoardModel> modelList = DashBoardAnalyzeBLL.GetStatusType(nowDay);
            PanelShowModel.SomedayDataCount todayModel = SysRpt_WebDayInfoBLL.GetNowDataCount();
            foreach (DashBoardModel model in modelList)
            {
                switch (model.Name)
                {
                    case "销售笔数":
                        model.Total = todayModel.saleNum;
                        break;
                    case "会员数量":
                        model.Total = todayModel.userNum;
                        break;
                    case "商品数量":
                        model.Total = todayModel.goodsNum;
                        break;
                    case "短信数量":
                        model.Total = todayModel.smsNum;
                        break;
                }
            }

            return CommonLib.Helper.JsonSerializeObject(modelList);
        }

        public static string GetFunnelData(string timeType, string sourceType, DateTime stDate, DateTime edDate)
        {
            ConversionFunnel funnelModel = new ConversionFunnel();
            DateTime bgTime = new DateTime();
            DateTime edTime = new DateTime();

            switch (timeType)
            {
                case "1m":
                    edTime = DateTime.Now;
                    bgTime = DateTime.Now.AddMonths(-1);
                    break;
                case "3m":
                    edTime = DateTime.Now;
                    bgTime = DateTime.Now.AddMonths(-3);
                    break;
                case "custom":
                    bgTime = stDate;
                    edTime = edDate;
                    break;
            }

            //funnelModel = DashBoardAnalyzeBLL.GetConversionFunnel(sourceType,bgTime, edTime);

            return CommonLib.Helper.JsonSerializeObject(DashBoardAnalyzeBLL.GetConversionFunnel(sourceType, bgTime, edTime));
        }

        public static string GetFunnelDataEx(string activeList)
        {
            return CommonLib.Helper.JsonSerializeObject(DashBoardAnalyzeBLL.GetConversionFunnelEx(activeList));
        }

        public static string GetActiveVenn(DateTime stDate, DateTime edDate)
        {
            ActiveUsrList accidListModel = DashBoardAnalyzeBLL.GetActiveListForVenn(stDate, edDate);
            VennSetsModel setsModel = new VennSetsModel();
            VennSet vennSet=new VennSet();
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
                if (comCount==1)
                {
                    try
                    {
                        vennSet.SetsCount = accidListModel.ActiveAccids.Find(x => x.DayDate.ToShortDateString() == vennSet.SetsElements[0]).AccidList.Count;    
                    }
                    catch (Exception ex)
                    {
                        vennSet.SetsCount = 0;
                        
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
                    }
                    catch (Exception ex)
                    {

                        vennSet.SetsCount = 0;
                    }
                }

                setsModel.SetsList.Add(new VennSet(vennSet.SetsElements, vennSet.SetsCount));
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
