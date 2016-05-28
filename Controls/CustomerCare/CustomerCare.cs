using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using BLL;
using Utility;

namespace Controls.CustomerCare
{
    public static class CustomerCare
    {
        /// <summary>
        /// 获取填充图表的回访数据
        /// </summary>
        /// <param name="context"></param>
        public static string getServiceInfo(DateTime startTime, DateTime endTime)
        {
            Dictionary<string, List<dynamic>> DataDic = BLL.CustomerCareBLL.GetServStatChartData(startTime, endTime);

                if (DataDic != null && DataDic.Count > 0)
                {
                    Logger.Info(CommonLib.Helper.JsonSerializeObject(startTime));
                    Logger.Info(CommonLib.Helper.JsonSerializeObject(endTime));
                    string jsonDt = SetChartData(DataDic, startTime, endTime);
                    return jsonDt;
                }
                else
                {
                    return "[]";
                }

            

            //IsoDateTimeConverter timeConverter = new IsoDateTimeConverter();
            //timeConverter.DateTimeFormat = "yyyy'/'MM'/'dd HH':'mm";
            //context.Response.Write(JsonConvert.SerializeObject(list, timeConverter));
        }

        public static string SetChartData(Dictionary<string, List<dynamic>> DataDic, DateTime begTime, DateTime endTime)
        {
            begTime = begTime.Date;
            endTime = endTime.Date;

            ServiceStatModel chartModel = new ServiceStatModel();

                if (DataDic != null)
                {
                    DateTime nowDay = DateTime.Now.Date;
                    DateTime dayDate = new DateTime(1890, 1, 1);
                    dayDate = begTime;
                    while (dayDate <= endTime)
                    {
                        int newUsrCount = 0;
                        int oldUsrCount = 0;
                        VisitUserType dList = new VisitUserType();
                        dList.XLable = dayDate.ToString("yyyy-MM-dd");

                        List<dynamic> list = DataDic["UsrPer"].Where(x => x.insertTime.ToString() == dayDate.ToString("yyyy-MM-dd")).ToList();

                        foreach (dynamic dr in list)
                        {
                            if (dr.regTime != DateTime.MinValue)
                            {
                                DateTime dt = Convert.ToDateTime(dr.regTime);
                                if (dt.Year == dayDate.Year && dt.Month == dayDate.Month)
                                {
                                    newUsrCount++;
                                }
                                else
                                {
                                    oldUsrCount++;
                                }
                            }
                        }

                        dList.date = dayDate;
                        dList.newUsr = newUsrCount;
                        dList.oldUsr = oldUsrCount;

                        if (dList.date != DateTime.Now.Date)
                        {
                            chartModel.usrList.Add(dList);
                        }

                        dayDate = dayDate.AddDays(1);
                    }

                    //客服人员
                    foreach (dynamic dr in DataDic["ServPer"])
                    {
                        if (dr.insertName != null)
                        {
                            ServPerson sp = new ServPerson();
                            sp.key = dr.insertName.ToString();
                            sp.value = Convert.ToInt32(dr.cnt);
                            chartModel.servPersonPer.Add(sp);
                        }
                        
                    }
                    //渠道
                    foreach (dynamic dr in DataDic["ChannelPer"])
                    {
                        if (dr.vm_name!=null)
                        {
                            if (dr.vm_name.ToString() != "")
                            {
                                VisitChannel vc = new VisitChannel();
                                vc.key = dr.vm_name.ToString();
                                vc.value = Convert.ToInt32(dr.cnt);
                                chartModel.channelPer.Add(vc);
                            }
                        }
                        
                    }

                    return CommonLib.Helper.JsonSerializeObject(chartModel);
                }
                else
                {
                    return "[]";
                }
            
        }

        public static string getOrderInfo(DateTime startTime, DateTime endTime, int dayCnt, int type, string person)
        {
            Dictionary<string, object> list = new Dictionary<string, object>();

            List<dynamic> ds = CustomerCareBLL.getOrderInfo(startTime, endTime, dayCnt, type, person);

            if (ds != null && ds.Count > 0)
            {
                string jsonDt = SetPaymentData(ds, type, person, startTime, endTime);
                return jsonDt;
            }
            else
            {
                return "[]";
            }
        }

        public static string SetPaymentData(List<dynamic> data, int type, string person, DateTime startTime, DateTime endTime)
        {
            string jsData = "[]";
            PayAfterServDataModel sps = new PayAfterServDataModel();

            if (type == 2)
            {
                if (data != null && data.Count>0)
                {
                    foreach (dynamic dr in data)
                    {
                        ServicePayPair sp = new ServicePayPair();
                        sp.person = dr.insertName;
                        sp.serviceCounut = CustomerCareBLL.GetVisitCount(1,sp.person, startTime, endTime);
                        sp.serviceAllCounut = CustomerCareBLL.GetVisitCount(2, sp.person, startTime, endTime);
                        sp.orderCnt = dr.orderCnt.ToString();
                        sp.count = dr.moneyCnt.ToString();
                        sps.spList.Add(sp);
                    }

                }

            }
            if (type == 1)
            {
                if (data != null && data.Count > 0)
                {

                    List<dynamic> list = data.Where(x => x.insertName.ToString() == person).ToList();

                    foreach (dynamic dr in list)
                    {
                        ServiceDetail sd = new ServiceDetail();
                        sd.person = dr.insertName;
                        sd.comName = dr.CompanyName;
                        sd.saleName = dr.displayName;
                        sd.saleTime = dr.buytime.ToString();
                        sd.buyMoney = ((Convert.ToInt32(dr.cnt) == 1
                            ? dr.buymoney.ToString()
                            : dr.buymoney.ToString() + "(" + dr.cnt.ToString() + "人AA)"));
                        sps.detailList.Add(sd);
                    }

                }
            }

            jsData = CommonLib.Helper.JsonSerializeObject(sps);
            return jsData;
        }

        /// <summary>
        /// 获取客服回访数据
        /// </summary>
        /// <returns></returns>
        public static string getVisitinfo()
        {
            List<dynamic> listCustomer = CustomerCareBLL.getVisitInfo_customer();
            List<dynamic> listSoftware = CustomerCareBLL.getVisitInfo_software();

            int count = CustomerCareBLL.getVisitInfo_count();

            string jsonData = setVisitData(listCustomer, listSoftware, count);

            return jsonData;
        }

        public static string setVisitData(List<dynamic> listCustomer, List<dynamic> listSoftware, int count)
        {
            CustomerAnalyzeModel cam = new CustomerAnalyzeModel();
            if (listCustomer != null && listSoftware != null && listCustomer.Count > 0 && listSoftware.Count > 0)
            {
                foreach (dynamic dr in listCustomer)
                {
                    CustomerSource cs = new CustomerSource();
                    string type = dr.a_customerSourceType.ToString();

                    switch (type)
                    {
                        case "1":
                            cs.sourceType = "朋友介绍";
                            break;
                        case "2":
                            cs.sourceType = "百度搜索";
                            break;
                        case "3":
                            cs.sourceType = "其他";
                            break;
                        default:
                            cs.sourceType = "";
                            break;
                    }
                    cs.sourceCount = Convert.ToInt32(dr.Cnt);
                    cam.csList.Add(cs);
                }
                //渠道
                foreach (dynamic dr in listSoftware)
                {
                    OtherSoftware os = new OtherSoftware();
                    string type = dr.a_OtherSoftwareType.ToString();

                    switch (type)
                    {
                        case "1":
                            os.softwareType = "使用过其他软件";
                            break;
                        case "2":
                            os.softwareType = "未使用过其他软件";
                            break;
                        default:
                            os.softwareType = "";
                            break;
                    }
                    os.softwareCount = Convert.ToInt32(dr.Cnt);
                    cam.osList.Add(os);
                }

                cam.unrecordNum = count;

                return CommonLib.Helper.JsonSerializeObject(cam);
            }
            else
            {
                return "[]";
            }
        }

        public static string getVisitDetail(int type)
        {
            string where = string.Empty;

            switch (type)
            {
                case 1:
                    where = " a_customerSourceType=1 ";
                    break;
                case 2:
                    where = " a_customerSourceType=2 ";
                    break;
                case 3:
                    where = " a_customerSourceType=3 ";
                    break;
                case 4:
                    where = " a_otherSoftwareType=1 ";
                    break;
                case 5:
                    where = " a_otherSoftwareType=2 ";
                    break;
                default:
                    break;
            }

            if (string.IsNullOrEmpty(where))
            {
                return "[]";
            }
            else
            {
                List<dynamic> ds = CustomerCareBLL.getVisitDetail(where);

                if (ds != null && ds.Count > 0)
                {
                    string jsonDt = setVisitAnalyzeDetail(ds);
                    return jsonDt;
                }
                else
                {
                    return "[]";
                }

            }    
        }

        public static string setVisitAnalyzeDetail(List<dynamic> ds)
        {
            VisitAnalyzeDetailModel detailModel = new VisitAnalyzeDetailModel();

            if (ds != null && ds.Count > 0)
            {
                foreach (dynamic dr in ds)
                {
                    VisitAnalyzeDetail model = new VisitAnalyzeDetail();
                    model.accid = Convert.ToInt32(dr.accid);
                    model.accName = dr.CompanyName == null ? "" : dr.CompanyName.ToString();
                    model.otherSoft = dr.a_OtherSoftware == null ? "" : dr.a_OtherSoftware.ToString();
                    model.useCause = dr.a_UseCause == null ? "" : dr.a_UseCause.ToString();
                    model.otherSource = dr.a_CustomerSource == null ? "" : dr.a_CustomerSource.ToString();
                    detailModel.detailList.Add(model);
                }

                return CommonLib.Helper.JsonSerializeObject(detailModel);
            }
            else
            {
                return "[]";
            }
        }

        /// <summary>
        /// 获取客服后用户登录留存信息
        /// </summary>
        /// <param name="stDate"></param>
        /// <param name="edDate"></param>
        /// <returns></returns>
        public static string GetCareRetention(DateTime stDate, DateTime edDate, string dateType, string usrName)
        {
            UserRetentionModel careRetentionModel = new UserRetentionModel();
            careRetentionModel = CustomerCareBLL.GetCareRetention(stDate, edDate, dateType, usrName);

            return CommonLib.Helper.JsonSerializeObject(careRetentionModel);
        }

       /// <summary>
       /// 获取不同用户的服务比
       /// </summary>
       /// <param name="stDate"></param>
       /// <param name="edDate"></param>
       /// <param name="usrName"></param>
       /// <param name="partIndex"></param>
       /// <returns></returns>
        public static string GetCarePartitionPer(DateTime stDate, DateTime edDate, string usrName, int partIndex)
        {
            CarePercentModel carePer = new CarePercentModel();
            carePer = CustomerCareBLL.GetCarePartitionPer(stDate, edDate, usrName, partIndex);

            return CommonLib.Helper.JsonSerializeObject(carePer);

        }

        /// <summary>
        /// 获取给定时间内每月的续费用户
        /// </summary>
        /// <param name="stDate"></param>
        /// <param name="edDate"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetOrderRenewal(DateTime stDate, DateTime edDate, string type)
        {
            List<OrderRenewalModel> ordList = new List<OrderRenewalModel>();
            ordList = CustomerCareBLL.GetOrderRenewal(stDate, edDate, type);
            return CommonLib.Helper.JsonSerializeObject(ordList,"");
        }
        /// <summary>
        /// 提现相关
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="withdrawStatus"></param>
        /// <param name="payType"></param>
        /// <param name="withdrawalNo"></param>
        /// <param name="accId"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static string GetWithdrawalRecord(int pageIndex, int withdrawStatus, int payType, string withdrawalNo, string accId, string start, string end)
        {
            string Column = string.Empty;
            string strWhere = string.Empty;
            Dictionary<string, string> dic = new Dictionary<string, string>()
            {
                {"RowCount",""},
                {"PageCount",""},
                {"list",""}
            };
            if (Convert.ToDateTime(end) > Convert.ToDateTime(start))
            {
                if (start != "")
                {
                    DateTime stTime = Convert.ToDateTime(start);
                    strWhere += " twr.createTime >='" + stTime.ToString("yyyy-MM-dd") + "' and";
                }
                if (end != "")
                {
                    DateTime edTime = Convert.ToDateTime(end);
                    strWhere += " twr.createTime <'" + edTime.AddDays(1).Date.ToString("yyyy-MM-dd") + "' and";
                }
            }

            if (withdrawStatus != -99)
            {
                strWhere += " twr.status=" + withdrawStatus.ToString() + " and ";
            }
            //payType: 0 支付宝 对应paymentType=0
            //payType: 1 银行卡 对应paymentType=2、3、4...
            if (payType != -99)
            {
                if (payType==0)
                {
                    strWhere += " twi.paymentType=0  and ";
                }
                else if (payType == 1)
                {
                    strWhere += " twi.paymentType>1  and ";
                }                
            }
            if (withdrawalNo != "")
            {
                strWhere += " id=" + withdrawalNo + " and ";
            }
            if (accId != "")
            {
                strWhere += " twr.accId=" + accId + " and ";
            }
            if (strWhere.Length > 0)
            {
                strWhere = strWhere.Substring(0, strWhere.LastIndexOf('a'));
            }

            int pageCount = CustomerCareBLL.GetPageCount(strWhere);
            int pageSize = 15;
            if (pageCount != 0)
            {
                dic["RowCount"] = pageCount.ToString();
            }
            else
            {
                dic["RowCount"] = "0";
            }
            if (pageCount > 0)
            {
                dic["PageCount"] = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(pageCount) / Convert.ToDecimal(pageSize))).ToString();
            }
            else
            {
                dic["PageCount"] = "0";
            }

            List<WithdrawalRecordModel> withdrawalList = CustomerCareBLL.GetPage(pageIndex, pageSize, Column, strWhere);
            dic["list"] = CommonLib.Helper.JsonSerializeObject(withdrawalList, "yyyy-MM-dd HH:mm:ss");
            return CommonLib.Helper.JsonSerializeObject(dic);
        }
        public static string UpdateWithdrawalStatus(int withdrawalRecordId, int status, string operatorIP, int operatorUserId, int accid,string operatorName)
        {
            string iResult = "0";
            decimal withdrawalMoney = 0;
            bool isSaved = CustomerCareBLL.UpdateWithdrawalStatus(withdrawalRecordId, status, operatorIP, operatorUserId);
            
            if (isSaved)
            {
                iResult = "1";
                if (status == 3)
                {
                    withdrawalMoney = CustomerCareBLL.GetWithdrawalMoneyByWithdrawalRecordId(withdrawalRecordId);
                    string msgTitle = "提现成功提示";
                    string msgContent = "您的" + withdrawalMoney.ToString() + "元提现金额今日已经成功汇出，请注意查收。";
                    int sendMessage = Utility.MessageCenter.PostMessage(accid.ToString(), msgTitle, msgContent, operatorUserId, operatorName,null);
                    int sendMobileMessage = Utility.MessageCenter.PostMobileMessage(accid.ToString(), msgTitle, msgContent, operatorUserId, operatorName,null);
                }
            }
            return iResult;
        }
    }
}
