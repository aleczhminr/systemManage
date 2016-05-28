using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Controls.MessageCenter
{
   public static class MessageCenterControls
    {

        #region PostMessage 发送单条推送消息
        /// <summary>
        /// 发送单条推送消息
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
       public static string PostMessage(string accIdList, string msgTitle, string msgContent, int operatorId, string operstorName, DateTime? timingTime = null)
        { 
            accIdList = accIdList.Replace('，', ',').Trim(); 

            int iResult = Utility.MessageCenter.PostMessage(accIdList, msgTitle, msgContent, operatorId, operstorName, timingTime);

            return iResult.ToString();
        }
        #endregion

        #region PostGlobal 发送全局公告信息
        /// <summary>
        /// 发送全局公告信息
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
       public static string PostGlobal(string msgTitle, string msgContent, int userPower, int operatorId, string operstorName, DateTime? timingTime = null, DateTime? expire = null)
        {
            int iResult = -1; 
            if (userPower == 0)
            { 
                //过期时间
                DateTime expireTime = Convert.ToDateTime(DateTime.Now.AddDays(2).ToShortDateString());
                if (expire != null)
                {
                    expireTime = Convert.ToDateTime(expire);
                }
                expireTime = expireTime.AddHours(23).AddMinutes(59).AddSeconds(59);


                iResult = Utility.MessageCenter.PostGlobal(msgTitle, msgContent, operatorId, operstorName, expireTime, timingTime);
            }
            return iResult.ToString();
        }
        #endregion

        #region GetStatus 检测店铺在线状态
        /// <summary>
        /// 检测店铺在线状态
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
       public static string GetStatus(int accId)
        {
            var sResult = Utility.MessageCenter.GetStatus(accId);

            return sResult;
        }
        #endregion

        #region GetOnline 当前在线统计
        /// <summary>
        /// 当前在线统计
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
       public static string GetOnline()
        {
            var oResult = Utility.MessageCenter.GetOnline();
            var strJson =CommonLib.Helper.JsonSerializeObject(oResult);

            return strJson;
        }
        #endregion

        #region GetMessage 店铺消息列表
        /// <summary>
        /// 店铺消息列表
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
       public static string GetMessage(int accId, string boardCastId, int pageSize, int nowPage)
        {   
            var oResutlt =Utility.MessageCenter.GetMessage(accId, boardCastId, pageSize, nowPage);


            var sResult = CommonLib.Helper.JsonSerializeObject(oResutlt, "yyyy'-'MM'-'dd HH':'mm");

            return sResult;
        }
        #endregion     

        #region GetMessageNotice 系统消息列表
        /// <summary>
        /// 系统消息列表
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
       public static string GetMessageNotice(int pageSize, int nowPage, int? oShowType = null)
        { 
            int msgShowType = -1; 
            if (oShowType != null && oShowType.ToString().Trim() != "")
            {
                msgShowType =Convert.ToInt32( oShowType);
            }

            var oResutlt =Utility.MessageCenter.GetMessageNotice(pageSize, nowPage, msgShowType);


            var sResult = CommonLib.Helper.JsonSerializeObject(oResutlt, "yyyy'-'MM'-'dd HH':'mm");

            return sResult;
        }
        #endregion

        #region GetMessageAnalysis 获得简单统计信息
        /// <summary>
        /// 获得简单统计信息
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
       public static string GetMessageAnalysis(int pageSize, int nowPage, int? oShowType = null)
        { 
            int msgShowType = -1; 
            if (oShowType != null && oShowType.ToString().Trim() != "")
            {
                msgShowType = Convert.ToInt32(oShowType);
            }

            var oResutlt =Utility.MessageCenter.GetMessageAnalysis(pageSize, nowPage, msgShowType);

            var sResult = CommonLib.Helper.JsonSerializeObject(oResutlt, "yyyy'-'MM'-'dd HH':'mm");

            return sResult;
        }
        #endregion


        #region PostMobileMessage 发送手机端消息推送
        /// <summary>
        /// 发送手机端消息推送
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
       public static string PostMobileMessage(string accIdList, string msgTitle, string msgContent, int operatorId, string operstorName, DateTime? timingTime = null)
        { 
            accIdList = accIdList.Replace('，', ',').Trim(); 

            int iResult =Utility.MessageCenter.PostMobileMessage(accIdList, msgTitle, msgContent, operatorId, operstorName, timingTime);

            return iResult.ToString();
        }
        #endregion

        #region PostMobileGlobal 发送手机端公告信息
        /// <summary>
        /// 发送手机端公告信息
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
       public static string PostMobileGlobal(int userPower, string msgTitle, string msgContent, int operatorId, string operstorName, DateTime? timingTime = null, DateTime? expire = null)
        {
            int iResult = -1; 
            if (userPower == 0)
            {
                //存在权限 
                 

                //过期时间
                DateTime expireTime = Convert.ToDateTime(DateTime.Now.AddDays(2).ToShortDateString());
                if (expire != null)
                {
                    expireTime = Convert.ToDateTime(expire);
                }
                expireTime = expireTime.AddHours(23).AddMinutes(59).AddSeconds(59);
                 

                iResult =Utility.MessageCenter.PostMobileGlobal(msgTitle, msgContent, operatorId, operstorName, expireTime, timingTime);
            }
            return iResult.ToString();
        }
        #endregion

        #region GetMobileMessage 店铺手机端消息列表
        /// <summary>
        /// 店铺手机端消息列表
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
       public static string GetMobileMessage(int accId, string boardCastId, int pageSize, int nowPage)
        {   
            var oResutlt =Utility.MessageCenter.GetMobileMessage(accId, boardCastId, pageSize, nowPage);
            var sResult = CommonLib.Helper.JsonSerializeObject(oResutlt, "yyyy'-'MM'-'dd HH':'mm");

            return sResult;
        }
        #endregion

        #region GetMobileMessageNotice 手机端系统消息列表
        /// <summary>
        /// 手机端系统消息列表
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
       public static string GetMobileMessageNotice(int pageSize, int nowPage, int? oShowType = null)
        { 
            int msgShowType = -1; 
            if (oShowType != null && oShowType.ToString().Trim() != "")
            {
                msgShowType = Convert.ToInt32(oShowType);
            }

            var oResutlt =Utility.MessageCenter.GetMobileMessageNotice(pageSize, nowPage, msgShowType);
            var sResult = CommonLib.Helper.JsonSerializeObject(oResutlt, "yyyy'-'MM'-'dd HH':'mm");

            return sResult;
        }
        #endregion

        #region GetMobileMessageAnalysis 获得手机端简单统计信息
        /// <summary>
        /// 获得手机端简单统计信息
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
       public static string GetMobileMessageAnalysis(int pageSize, int nowPage, int? oShowType = null)
        { 
            int msgShowType = -1; 
            if (oShowType != null && oShowType.ToString().Trim() != "")
            {
                msgShowType = Convert.ToInt32(oShowType);
            }

            var oResutlt =Utility.MessageCenter.GetMobileMessageAnalysis(pageSize, nowPage, msgShowType);


            var sResult = CommonLib.Helper.JsonSerializeObject(oResutlt, "yyyy'-'MM'-'dd HH':'mm");

            return sResult;
        }
        #endregion


    }
}
