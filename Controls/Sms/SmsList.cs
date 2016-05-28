using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using BLL;
using Model.Model4View;

namespace Controls.Sms
{
    public class SmsList
    {
        /// <summary>
        /// 获取短信列表
        /// </summary>
        /// <param name="iPage"></param>
        /// <param name="accId"></param>
        /// <param name="smsType"></param>
        /// <param name="smsStatus"></param>
        /// <param name="priority"></param>
        /// <param name="bgDate"></param>
        /// <param name="edDate"></param>
        /// <returns></returns>
        public static SmsListModel GetSmsList(int iPage, int accId, int smsType, int smsStatus, int priority,
            DateTime bgDate, DateTime edDate)
        {
            return T_Sms_ListBLL.GetSmsList(iPage, accId, smsType, smsStatus, priority, bgDate, edDate);
        }

        /// <summary>
        /// 获取短信审核列表
        /// </summary>
        /// <param name="iPage"></param>
        /// <param name="iStatus"></param>
        /// <param name="operatorId"></param>
        /// <param name="bgDate"></param>
        /// <param name="edDate"></param>
        /// <returns></returns>
        public static ReviewModel GetReviewList(int iPage, int iStatus, int operatorId, DateTime bgDate, DateTime edDate)
        {
            return T_Sms_ListBLL.GetReviewList(iPage, iStatus, operatorId, bgDate, edDate);
        }

        /// <summary>
        /// 获取短信详情列表
        /// </summary>
        /// <param name="noticeid"></param>
        /// <param name="accid"></param>
        /// <param name="page"></param>
        /// <param name="pageSiz"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        public static SmsDetail GetSmsDetailList(int noticeid, int accid = 0, int page = 1, int pageSiz = 50,
            string phone = "")
        {
            return T_Sms_ListBLL.GetSmsDetailList(noticeid, accid, page, pageSiz, phone);
        }
        
        /// <summary>
        /// 执行短信审核的操作
        /// </summary>
        /// <param name="notifyId"></param>
        /// <param name="reviewInfo"></param>
        /// <param name="channelInfo"></param>
        /// <param name="chinaMobile"></param>
        /// <param name="chinaUnicom"></param>
        /// <param name="chinaTelecom"></param>
        /// <param name="operatorId"></param>
        /// <param name="reviewDesc"></param>
        /// <returns></returns>
        public static int SetReviewSms(int notifyId, int reviewInfo, int channelInfo, int chinaMobile, int chinaUnicom,
            int chinaTelecom, int operatorId, string reviewDesc)
        {
            return T_Sms_ListBLL.SetReviewSms(notifyId, reviewInfo, channelInfo, chinaMobile, chinaUnicom,
                chinaTelecom, operatorId, reviewDesc);
        }

        /// <summary>
        /// 更新短信内容
        /// </summary>
        /// <param name="smsId"></param>
        /// <param name="smsContent"></param>
        /// <returns></returns>
        public static bool UpdateSmsContent(int smsId, string smsContent)
        {
            return T_Sms_ListBLL.UpdateSmsContent(smsId, smsContent);
        }

        /// <summary>
        /// 获取审核统计信息
        /// </summary>
        /// <param name="iStatus"></param>
        /// <param name="bgDate"></param>
        /// <param name="edDate"></param>
        /// <returns></returns>
        public static ReviewModel GetReviewCount(int iStatus, DateTime bgDate, DateTime edDate)
        {
            return T_Sms_ListBLL.GetReviewCount(iStatus, bgDate, edDate);
        }

                /// <summary>
        /// 获取通知消息
        /// </summary>
        /// <param name="typeId">类型Id</param>
        /// <returns></returns>
        public static NoticeTextModel GetNoticeText(int typeId)
        {
            return T_NoticeTextBLL.GetNoticeText(typeId);
        }
        /// <summary>
        /// 保存短信通告信息
        /// </summary>
        /// <param name="noticeType">通告类型</param>
        /// <param name="noticeId">Id</param>
        /// <param name="flagEdit">修改标记</param>
        /// <param name="display">显示设置</param>
        /// <param name="noticeText">通告内容</param>
        /// <returns></returns>
        public static int SaveNoticeText(int noticeType, int noticeId, int flagEdit, int display, string noticeText, string opName, string opIp)
        {

            return T_NoticeTextBLL.SaveNoticeText(noticeType, noticeId, flagEdit, display, noticeText, opName, opIp);
        }

        public static Dictionary<string, string> GetSmsContent(int page, string type, string subType)
        {
            return T_Common_SmsBLL.GetSmsContent(page, type,subType);
        }

        public static List<string> GetMinCate(string maxCate)
        {
            return T_Common_SmsBLL.GetMinCate(maxCate);
        }

        public static string AddCate(string cate)
        {
            return T_Common_SmsBLL.AddCate(cate);
        }

        public static string UpdateCommonSmsContent(int smsid, string maxCate, string minCate, string smscontent)
        {
            return T_Common_SmsBLL.UpdateCommonSmsContent(smsid, maxCate, minCate, smscontent);
        }

        public static string AddCommonSms(string maxCate, string minCate, string smscontent)
        {
            return T_Common_SmsBLL.AddCommonSms(maxCate, minCate, smscontent);
        }

        public static string DeleteSms(int id)
        {
            return T_Common_SmsBLL.DeleteSms(id);
        }

        public static string GetCateList(string type)
        {
            return T_Common_SmsBLL.GetCateList(type);
        }

        public static string UpdateCateName(string maxName, string minName, string updateName)
        {
            return T_Common_SmsBLL.UpdateCateName(maxName, minName, updateName);
        }

        public static string DeleteCate(string maxName, string minName)
        {
            return T_Common_SmsBLL.DeleteCate(maxName, minName);
        }

        public static string ChangeStatus(string maxName, string minName)
        {
            return T_Common_SmsBLL.ChangeStatus(maxName, minName);
        }

        public static string ChangeRanking(string maxName, string minName, int rank)
        {
            return T_Common_SmsBLL.ChangeRanking(maxName, minName, rank);
        }
        /// <summary>
        /// 获取短信审核效率
        /// </summary>
        /// <param name="stDate"></param>
        /// <param name="edDate"></param>
        /// <returns></returns>
        public static SmsPerformance GetSmsPerformance(DateTime stDate, DateTime edDate)
        {
            return T_Sms_ListBLL.GetSmsPerformance(stDate, edDate);
        }
    }
}
