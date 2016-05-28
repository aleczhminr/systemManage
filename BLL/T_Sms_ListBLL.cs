using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib;
using DAL;
using Model;
using Model.Enum;
using Model.Model4View;
using System.Net;
using System.IO;

namespace BLL
{
  public static  class T_Sms_ListBLL
    {
      public static List<T_Sms_ListBasic> GetList(int pageIndex, int pageSize, List<DapperWhere> sqlWhere, string filedOrder)
      {
          T_Sms_ListDAL dal = new T_Sms_ListDAL();

          var list=dal.GetListBasic(pageIndex, pageSize, sqlWhere, filedOrder);

          foreach (T_Sms_ListBasic item in list)
          {
              item.smsTypeName = Enum.GetName(typeof(Model.Enum.smsEnum.smsType), item.smsType);
              item.smsStatusName = Enum.GetName(typeof(Model.Enum.smsEnum.smsListStatus), item.smsStatus);
          }

          return list;
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
      public static SmsListModel GetSmsList(int iPage, int accId, int smsType, int smsStatus, int priority, DateTime bgDate, DateTime edDate)
      {
          var dal = new T_Sms_ListDAL();
          var smsObj = dal.GetSmsList(iPage, accId, smsType, smsStatus, priority, bgDate, edDate);
          if (smsObj!=null&&smsObj.Data != null && smsObj.Data.Count > 0)
          {
              foreach (var item in smsObj.Data)
              {
                  item.SmsTypeName = Enum.GetName(typeof(smsEnum.smsType), item.SmsType);
                  item.StatusName = Enum.GetName(typeof(smsEnum.smsStatus), item.SmsStatus);
                  item.ReviewName = Enum.GetName(typeof(smsEnum.smsReview), item.Review);
                  item.PriorityName = Enum.GetName(typeof(smsEnum.smsPriority), item.Priority);
                  item.ContentLength = item.SmsContent.Length;
              }

              //分页处理
              //smsObj.PageHtml = CommonLib.Helper.GetPagination(iPage, smsObj.PageSize, "SmsListData", 20);
          }
          else
          {
              smsObj = new SmsListModel();
          }
          return smsObj;
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
      public static ReviewModel GetReviewList(int iPage, int iStatus, int operatorId, DateTime bgDate, DateTime edDate)
      {
          var dal = new T_Sms_ListDAL();
          var reviewObj = dal.GetReviewList(iPage, iStatus, operatorId, bgDate, edDate);
          if (reviewObj!=null&&reviewObj.Data != null && reviewObj.Data.Count > 0)
          {
              foreach (var item in reviewObj.Data)
              {
                  item.SmsTypeName = Enum.GetName(typeof(smsEnum.smsType), item.SmsType);
                  item.StatusName = Enum.GetName(typeof(smsEnum.smsStatus), item.SmsStatus);
                  item.ReviewName = Enum.GetName(typeof(smsEnum.smsReview), item.Review);
                  item.ContentLength = item.SmsContent.Length;
              }

              //分页处理
              //var helper = new Helper();
              //reviewObj.PageHtml = helper.GetPagination(iPage, reviewObj.PageSize, "GetReviewList", 20);
          }
          return reviewObj;
      }
      #endregion

      /// <summary>
      /// 获取短信详情列表
      /// </summary>
      /// <param name="noticeid"></param>
      /// <param name="accid"></param>
      /// <param name="page"></param>
      /// <param name="pageSiz"></param>
      /// <param name="phone"></param>
      /// <returns></returns>
      public static SmsDetail GetSmsDetailList(int noticeid, int accid = 0, int page = 1, int pageSiz = 50, string phone = "")
      {
          var dal = new T_Sms_ListDAL();
          var list= dal.GetSmsDetailList(noticeid, accid, page, pageSiz, phone);

          foreach (SmsDetails item in list.detailList)
          {
              item.smsChannel = Enum.GetName(typeof(Model.Enum.smsEnum.smsChannel), Convert.ToInt32(item.smsChannel));
              item.smsStatusName = Enum.GetName(typeof(Model.Enum.smsEnum.smsListStatus), item.smsStatus);
          }


          return list;
      }

      #region SetReviewSms 短信审核
      /// <summary>
      /// 短信审核
      /// </summary>
      /// <param name="notifyId">短信Id</param>
      /// <param name="reviewInfo">审核状态</param>
      /// <param name="channelInfo">通道信息</param>
      /// <param name="chainMobile">移动</param>
      /// <param name="chinaUnChinaUnicom">联通</param>
      /// <param name="ChinaTelecom">电信</param>
      /// <param name="operatorId">操作人员id</param>
      /// <param name="reviewDesc">审核描述</param>
      /// <returns></returns>
      public static int SetReviewSms(int notifyId, int reviewInfo, int channelInfo, int chinaMobile, int chinaUnicom, int chinaTelecom, int operatorId, string reviewDesc)
      {
          var dal = new T_Sms_ListDAL();
          var smsChannel = string.Empty;
          if (channelInfo == 1)
          {
              var smsEditChannel = new SmsEditChannel();
              smsEditChannel.ChinaMobile = chinaMobile;
              smsEditChannel.ChinaUnicom = chinaUnicom;
              smsEditChannel.ChinaTelecom = chinaTelecom;
              smsChannel = CommonLib.Helper.JsonSerializeObject(smsEditChannel);
          }
          return dal.SetReviewSms(notifyId, reviewInfo, channelInfo, smsChannel, operatorId, reviewDesc);
      }
      #endregion

      #region UpdateSmsContent 更新短信内容
      /// <summary>
      /// 更新短信内容
      /// </summary>
      /// <param name="smsId"></param>
      /// <param name="smsContent"></param>
      /// <returns></returns>
      public static bool UpdateSmsContent(int smsId, string smsContent)
      {
          var dal = new T_Sms_ListDAL();
          return dal.UpdateSmsContent(smsId, smsContent);
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
      public static ReviewModel GetReviewCount(int iStatus, DateTime bgDate, DateTime edDate)
      {
          var dal = new T_Sms_ListDAL();
          return dal.GetReviewCount(iStatus, bgDate, edDate);
      }
      #endregion

      #region 短信通道管理
      /// <summary>
      /// 获取短信通道的总发送量s
      /// </summary>
      /// <param name="channel"></param>
      /// <returns></returns>
      public static int GetChannelSendNum(string channel)
      {
          var dal = new T_Sms_ListDAL();
          return dal.GetChannelSendNum(int.Parse(channel));
      }

      /// <summary>
      /// 查询短信通道余额
      /// </summary>
      /// <param name="channel"></param>
      /// <returns></returns>
      public static string GetChannelBalance(string channel)
      {
          //通道余额查询
          string sResult = "";
          string sUrl = "http://web.i200.cn:30018/date.aspx?dts=";
          string sChannel = channel;
          if (sChannel == "11")
          {
              sResult = HttpGet(sUrl + "11", "");
          }
          else if (sChannel == "10")
          {
              sResult = HttpGet(sUrl + "10", "");
          }
          else if (sChannel == "21")
          {
              sResult = HttpGet(sUrl + "21", "");
          }
          else if (sChannel == "12")
          {
              sResult = HttpGet(sUrl + "12", "");
          }
          else if (sChannel == "22")
          {
              sResult = HttpGet(sUrl + "22", "");
          }
          else if (sChannel == "41")
          {
              sResult = HttpGet(sUrl + "41", "");
          }
          else if (sChannel == "42")
          {
              sResult = HttpGet(sUrl + "42", "");
          }
          else if (sChannel == "51")
          {
              sResult = HttpGet(sUrl + "51", "");
          }
          else if (sChannel == "60")
          {
              sResult = HttpGet(sUrl + "60", "");
          }
          else if (sChannel == "61")
          {
              sResult = HttpGet(sUrl + "61", "");
          }
          else if (sChannel == "62")
          {
              sResult = HttpGet(sUrl + "62", "");
          }
          else if (sChannel == "63")
          {
              sResult = HttpGet(sUrl + "63", "");
          }
          else if (sChannel == "64")
          {
              sResult = HttpGet(sUrl + "64", "");
          }
          else if (sChannel == "70")
          {
              sResult = HttpGet(sUrl + "70", "");
          }
          else if (sChannel == "71")
          {
              sResult = HttpGet(sUrl + "71", "");
          }
          else if (sChannel == "72")
          {
              sResult = HttpGet(sUrl + "72", "");
          }
          else if (sChannel == "73")
          {
              sResult = HttpGet(sUrl + "73", "");
          }
          else if (sChannel == "80")
          {
              sResult = HttpGet(sUrl + "80", "");
          }
          else if (sChannel == "82")
          {
              sResult = HttpGet(sUrl + "82", "");
          }
          else if (sChannel == "92")
          {
              sResult = HttpGet(sUrl + "92", "");
          }

          return sResult;
      }

      /// <summary>
      /// 获取请求页面的通用方法
      /// </summary>
      /// <param name="Url"></param>
      /// <param name="postDataStr"></param>
      /// <returns></returns>
      public static string HttpGet(string Url, string postDataStr)
      {
          HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);
          request.Method = "GET";
          request.ContentType = "text/html;charset=UTF-8";

          HttpWebResponse response = (HttpWebResponse)request.GetResponse();
          Stream myResponseStream = response.GetResponseStream();
          StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
          string retString = myStreamReader.ReadToEnd();
          myStreamReader.Close();
          myResponseStream.Close();

          return retString;
      }
      #endregion

      /// <summary>
      /// 获取短信审核效率
      /// </summary>
      /// <param name="stDate"></param>
      /// <param name="edDate"></param>
      /// <returns></returns>
      public static SmsPerformance GetSmsPerformance(DateTime stDate, DateTime edDate)
      {
          var dal = new T_Sms_ListDAL();
          return dal.GetSmsPerformance(stDate, edDate);
      }

    }
}
