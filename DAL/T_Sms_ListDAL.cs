using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.Model4View;

namespace DAL
{
    /// <summary>
    /// 短信列表
    /// </summary>
    public class T_Sms_ListDAL : Base.T_Sms_ListBaseDAL
    {
        public List<T_Sms_ListBasic> GetListBasic(int pageIndex, int pageSize, List<DapperWhere> sqlWhere, string filedOrder)
        {
            return GetList<T_Sms_ListBasic>(pageIndex, pageSize, "id,phoneNum phoneNumber,smsContent,sendtime,smsType,smsStatus,smsChannel,isFree", sqlWhere, filedOrder);
        }

        #region GetSmsList 短信发送列表
        /// <summary>
        /// 短信发送列表
        /// </summary>
        /// <param name="iPage">当前页数</param>
        /// <param name="accId">店铺Id</param>
        /// <param name="smsType">短信类型</param>
        /// <param name="smsStatus">短信状态</param>
        /// <param name="priority">通道优先级</param>
        /// <param name="bgDate">开始日期</param>
        /// <param name="edDate">结束日期</param>
        /// <returns></returns>
        public SmsListModel GetSmsList(int iPage, int accId, int smsType, int smsStatus, int priority, DateTime bgDate, DateTime edDate)
        {
            var smsListModel = new SmsListModel();

            var strSql = new StringBuilder();
            var strWhereSql = new StringBuilder();

            if (priority != -99)
            {
                //发送优先级
                strWhereSql.Append(" priority=@priority");
            }
            else
            {
                //发送优先级
                strWhereSql.Append(" priority>-2");
            }

            if (accId != 0)
            {
                //店铺Id
                strWhereSql.Append(" and accID=@accID");
            }

            if (smsType != -99)
            {
                //短信类型
                strWhereSql.Append(" and smsType=@smsType");
            }

            if (smsStatus != -99)
            {
                //短信状态
                strWhereSql.Append(" and smsStatus=@smsStatus");
            }

            if (bgDate != null && edDate != null)
            {
                //指定时间方式
                bgDate = Convert.ToDateTime(Convert.ToDateTime(bgDate).ToShortDateString());
                edDate = Convert.ToDateTime(Convert.ToDateTime(edDate).ToShortDateString()).AddHours(23).AddMinutes(59).AddSeconds(59);

                strWhereSql.Append(" and submitTime between @bgDate and @edDate");
            }

            strSql.Append(" select count(id) [rowCount],sum(totalSmsCnt) as SmsCnt,");
            strSql.Append(" sum(realSmsCnt) as RealCnt,");
            strSql.Append(" sum(case when isFree=1 then realSmsCnt else 0 end) as FreeCnt");
            strSql.Append(" from T_Sms_Notify");
            if (strWhereSql.Length > 0)
            {
                strSql.Append(" where" + strWhereSql.ToString());
            }

            smsListModel = HelperForFrontend.Query<SmsListModel>(strSql.ToString(),
                              new
                              {
                                  bgDate = bgDate,
                                  edDate = edDate,
                                  priority = priority,
                                  accID = accId,
                                  smsType = smsType,
                                  smsStatus = smsStatus,
                              }).ToList()[0];

            strSql.Clear();

            strSql.Append(" select TT.*,T_Account.CompanyName StoreName from (select row_number() over(order by completeTime desc) as rowNumer,");
            strSql.Append(" id, accID, userCnt, smsContent, smsType, useBalance, priority, succeedCnt, failCnt, skipCnt, smsStatus, submitTime, RegularTime, completeTime, smsChannel, totalSmsCnt, realSmsCnt, isFree,errDesc, ChannelEx,PhoneList,ReviewDesc");
            strSql.Append(" FROM T_Sms_Notify");
            if (strWhereSql.Length > 0)
            {
                strSql.Append(" where" + strWhereSql.ToString());
            }
            strSql.Append(" ) TT");
            strSql.Append(" left outer join T_Account on T_Account.id=TT.accID");
            strSql.Append(" where TT.rowNumer between @startIndex and @endIndex");

            //页数计算
            int bgNumber = ((iPage - 1) * 20) + 1;
            int edNumber = (iPage) * 20;

            List<SmsNotifyItem> itemList = HelperForFrontend.Query<SmsNotifyItem>(strSql.ToString(), new
            {
                priority = priority,
                accID = accId,
                smsType = smsType,
                smsStatus = smsStatus,
                bgDate = bgDate,
                edDate = edDate,
                startIndex = bgNumber,
                endIndex = edNumber
            }).ToList();

            smsListModel.Data = itemList;
            return smsListModel;
        }
        #endregion

        #region GetReviewList 获得短信审核列表
        /// <summary>
        /// 获得短信审核列表
        /// </summary>
        /// <param name="iPage">当前页数</param>
        /// <param name="iStatus">审核状态</param>
        /// <param name="operatorId">操作人员Id</param>
        /// <param name="bgDate">开始日期</param>
        /// <param name="edDate">结束日期</param>
        /// <returns></returns>
        public ReviewModel GetReviewList(int iPage, int iStatus, int operatorId, DateTime bgDate, DateTime edDate)
        {
            var reviewModel = new ReviewModel();

            var strSql = new StringBuilder();
            var strWhereSql = new StringBuilder();
            if (iStatus != 0)
            {
                //审核状态
                strWhereSql.Append(" and Review=@Review");
            }
            else
            {
                strWhereSql.Append(" and Review>0");
            }

            if (operatorId != 0)
            {
                //审核人员
                strWhereSql.Append(" and operatorId=@operatorId");
            }
            if (bgDate != null && edDate != null)
            {
                //指定时间方式
                bgDate = Convert.ToDateTime(Convert.ToDateTime(bgDate).ToShortDateString());
                edDate = Convert.ToDateTime(Convert.ToDateTime(edDate).ToShortDateString()).AddHours(23).AddMinutes(59).AddSeconds(59);

                strWhereSql.Append(" and submitTime between @bgDate and @edDate");
            }

            strSql.Append(" select count(id) as SumCnt,");
            strSql.Append(" sum(case when Review = 1 then 1 else 0 end) as WaitCnt,");
            strSql.Append(" sum(case when Review = 2 then 1 else 0 end) as PassCnt,");
            strSql.Append(" sum(case when Review = 3 then 1 else 0 end) as RejectCnt");
            strSql.Append(" from T_Sms_Notify");
            strSql.Append(" where accID>0 ");
            strSql.Append(strWhereSql.ToString());

            try
            {
                reviewModel = HelperForFrontend.Query<ReviewModel>(strSql.ToString(),
                    new { bgDate = bgDate, edDate = edDate, Review = iStatus, operatorId = operatorId }).ToList()[0];
                reviewModel.PageNow = iPage;
            }
            catch (Exception ex)
            {
                return null;
            }

            strSql.Clear();

            strSql.Append(" select TT.*,T_Account.CompanyName StoreName,'' as OperatorName from (select row_number() over(order by submitTime desc) as rowNumer,");
            strSql.Append(" id, accID, userCnt, smsContent, smsType, useBalance, priority, succeedCnt, failCnt, skipCnt, smsStatus, submitTime, RegularTime, completeTime, smsChannel, totalSmsCnt, realSmsCnt, isFree,errDesc, ChannelEx, Review, ReviewOperator, ReviewDate, ReviewDesc");
            strSql.Append(" FROM T_Sms_Notify");
            strSql.Append(" where accID>0");
            strSql.Append(strWhereSql.ToString());
            strSql.Append(" ) TT");
            strSql.Append(" left outer join T_Account on T_Account.id=TT.accID");
            strSql.Append(" where TT.rowNumer between @startIndex and @endIndex");

            //页数计算
            int bgNumber = ((iPage - 1) * 20) + 1;
            int edNumber = (iPage) * 20;

            try
            {
                List<SmsNotifyItem> itemList = HelperForFrontend.Query<SmsNotifyItem>(strSql.ToString(), new
                {
                    Review = iStatus,
                    operatorId = operatorId,
                    bgDate = bgDate,
                    edDate = edDate,
                    startIndex = bgNumber,
                    endIndex = edNumber
                }).ToList();

                reviewModel.Data = itemList;
            }
            catch (Exception ex)
            {
                return null;
            }

            return reviewModel;
        }
        #endregion

        #region GetSmsDetailList 得到短信详情
        /// <summary>
        /// 得到短信详情
        /// </summary>
        /// <param name="noticeid">标记ID</param>
        /// <param name="accid">店铺ID</param>
        /// <param name="page">分页</param>
        /// <param name="pageSiz">每页面显示行</param>
        /// <param name="phone">电话</param>
        /// <returns></returns>
        public SmsDetail GetSmsDetailList(int noticeid, int accid = 0, int page = 1, int pageSiz = 50, string phone = "")
        {

            List<SmsDetails> detailList = new List<SmsDetails>();
            SmsDetail details = new SmsDetail();

            //页数计算
            int bgNumber = ((page - 1) * pageSiz) + 1;
            int edNumber = (page) * pageSiz;

            StringBuilder strWhere = new StringBuilder();

            strWhere.Append(" notifyID=@notifyID ");
            if (accid > 0)
            {
                strWhere.Append(" and  accID=@accid ");
            }


            if (phone.Length > 0)
            {
                strWhere.Append(" and phoneNum like '%'+ @phone +'%'");
            }


            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from(SELECT ROW_NUMBER() over(order by id desc) as rowNumber, " +
                          "id, " +
                          "phoneNum," +
                          "cast(smsChannel as varchar(100)) as smsChannel, " +
                          "smsContent,LEN(smsContent) as ContentLength, " +
                          "sendtime, " +
                          "cast(smsStatus as varchar(100)) as smsStatus," +
                          "userID, " +
                          "realCnt, " +
                          "isFree, " +
                          "errDesc," +
                          "smsReport  " +
                          " FROM I200.dbo.T_Sms_List where  " + strWhere.ToString() + ") t where  t.rowNumber between @startIndex and @endIndex;");

            detailList = DapperHelper.Query<SmsDetails>(strSql.ToString(), new
            {
                accid = accid,
                notifyID = noticeid,
                phone = phone,
                startIndex = bgNumber,
                endIndex = edNumber
            }).ToList();


            strSql.Clear();

            strSql.Append("SELECT count(id) as smsCnt,'' as moreStr FROM I200.dbo.T_Sms_List where " + strWhere.ToString() + " ; ");

            details = DapperHelper.Query<SmsDetail>(strSql.ToString(), new
            {
                accid = accid,
                notifyID = noticeid,
                phone = phone,
                startIndex = bgNumber,
                endIndex = edNumber
            }).ToList()[0];

            details.detailList = detailList;

            return details;
        }
        #endregion

        #region SetReviewSms 短信审核
        /// <summary>
        /// 短信审核
        /// </summary>
        /// <param name="notifyId">短信Id</param>
        /// <param name="reviewInfo">审核状态</param>
        /// <param name="channelInfo">是否调整通道</param>
        /// <param name="editChannel">短信通道Json</param>
        /// <param name="operatorId">操作人员Id</param>
        /// <param name="reviewDesc">审核描述</param>
        /// <returns></returns>
        public int SetReviewSms(int notifyId, int reviewInfo, int channelInfo, string editChannel, int operatorId, string reviewDesc)
        {
            int result = 0;

            int reviewStatus = 2;
            var channelEx = string.Empty;
            int smschannel = 0;
            int smsStatus = 0;
            string smsChannelSql = "";

            if (reviewInfo == 1 || reviewInfo == 2 || reviewInfo == 3)
            {
                reviewStatus = reviewInfo;
                if (reviewInfo == 3)
                {
                    smsStatus = 3;
                }
            }
            if (channelInfo != 0)
            {
                channelEx = editChannel;
                smschannel = -10;
                smsChannelSql = " ,smsChannel=@smsChannel";
            }

            var strSql = new StringBuilder();
            strSql.Append(" UPDATE T_Sms_Notify");
            strSql.Append(" SET priority =@priority, ChannelEx =@ChannelEx, Review =@Review, ReviewOperator =@ReviewOperator, ReviewDate =@ReviewDate, ReviewDesc =@ReviewDesc, smsStatus=@smsStatus");
            strSql.Append(smsChannelSql);
            strSql.Append(" where Review=1 and id=@id;");

            result = HelperForFrontend.Execute(strSql.ToString(), new
            {
                id = notifyId,
                priority = 2,
                ChannelEx = channelEx,
                Review = reviewStatus,
                ReviewOperator = operatorId,
                ReviewDate = DateTime.Now,
                ReviewDesc = reviewDesc,
                smsChannel = smschannel,
                smsStatus = smsStatus
            });

            return result;
        }
        #endregion

        #region UpdateSmsContent 更新短信内容
        /// <summary>
        /// 更新短信内容
        /// </summary>
        /// <param name="smsId"></param>
        /// <param name="smsContent"></param>
        /// <returns></returns>
        public bool UpdateSmsContent(int smsId, string smsContent)
        {
            bool status = false;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Sms_Notify set smsContent=@smsContent where id=@id;");

            try
            {
                int result = HelperForFrontend.Execute(strSql.ToString(), new {id = smsId, smsContent = smsContent});
                if (result>0)
                {
                    status = true;
                }
            }
            catch (Exception ex)
            {
                status = false;
            }

            return status;
        }
        #endregion

        #region GetReviewCount 获得审核统计信息
        /// <summary>
        /// 获得审核统计信息
        /// </summary>
        /// <param name="iStatus">审核状态</param>
        /// <param name="bgDate">起始日期</param>
        /// <param name="edDate">结束日期</param>
        /// <returns></returns>
        public ReviewModel GetReviewCount(int iStatus, DateTime bgDate, DateTime edDate)
        {
            var reviewModel = new ReviewModel();

            var strSql = new StringBuilder();
            var strWhereSql = new StringBuilder();
            if (iStatus != 0)
            {
                //审核状态
                strWhereSql.Append(" and Review=@Review");
            }
            else
            {
                strWhereSql.Append(" and Review>0");
            }

            if (bgDate != null && edDate != null)
            {
                //指定时间方式
                bgDate = Convert.ToDateTime(Convert.ToDateTime(bgDate).ToShortDateString());
                edDate = Convert.ToDateTime(Convert.ToDateTime(edDate).ToShortDateString()).AddHours(23).AddMinutes(59).AddSeconds(59);

                strWhereSql.Append(" and submitTime between @bgDate and @edDate");
            }

            strSql.Append(" select count(id) as SumCnt,");
            strSql.Append(" sum(case when Review = 1 then 1 else 0 end) as WaitCnt,");
            strSql.Append(" sum(case when Review = 2 then 1 else 0 end) as PassCnt,");
            strSql.Append(" sum(case when Review = 3 then 1 else 0 end) as RejectCnt");
            strSql.Append(" from T_Sms_Notify");
            strSql.Append(" where accID>0 ");
            strSql.Append(strWhereSql.ToString());

            try
            {
                reviewModel = HelperForFrontend.Query<ReviewModel>(strWhereSql.ToString(), new
                {
                    Review = iStatus,
                    bgDate = bgDate,
                    edDate = edDate
                }).ToList()[0];
            }
            catch (Exception)
            {
                reviewModel = null;
            }

            return reviewModel;
        }
        #endregion

        #region GetChannelSendNum 短信通道管理
        /// <summary>
        /// 获取短信通道累计发送数
        /// </summary>
        /// <param name="channel"></param>
        /// <returns></returns>
        public int GetChannelSendNum(int channel)
        {
            int result = -1;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select sum(realCnt) as smsCnt from T_Sms_List where smsStatus=1 and smsChannel=" +
                          channel.ToString());

            try
            {
                result = HelperForFrontend.Query<int>(strSql.ToString()).ToList()[0];
            }
            catch (Exception)
            {
                result = -1;    
            }

            return result;
        }

        #endregion

        /// <summary>
        /// 获取短信审核效率
        /// </summary>
        /// <param name="stDate"></param>
        /// <param name="edDate"></param>
        /// <returns></returns>
        public SmsPerformance GetSmsPerformance(DateTime stDate, DateTime edDate)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select MAX(interval) Longest,MIN(interval) Shortest,AVG(interval) Average, sum(case when interval<2 then 1 else 0 end)*100/COUNT(*) as Ratio,name from ");
            strSql.Append("(select DATEDIFF(MINUTE,submittime,reviewDate) interval,m.name from " +
                          "[i200].[dbo].[T_Sms_Notify] " +
                          "left join Sys_Manage_User m " +
                          "ON m.Id=ReviewOperator where reviewDate is not null and submittime between @stDate and @edDate and DATEDIFF(HH ,CAST(submitTime as date),submitTime) between 9 and 17) t " +
                          "group by name order by Ratio desc");

            SmsPerformance smsPerform = new SmsPerformance();

            smsPerform.dataList =
               DapperHelper.Query<SmsCheckTime>(strSql.ToString(), new { stDate = stDate, edDate = edDate }).ToList();
           
            return smsPerform;
        }

        /// <summary>
        /// 获取维客短信发送条数
        /// </summary>
        /// <param name="accId"></param>
        /// <returns></returns>
        public int GetFreeSmsCnt(int accId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select sum(realCnt) as Cnt from [i200].[dbo].[T_Sms_List] where isFree=1 and accID=@accId;");

            return DapperHelper.ExecuteScalar<int>(strSql.ToString(), new {accId = accId});
        }
    }
}
