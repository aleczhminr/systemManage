using BLL;
using Model;
using Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Sms
{
    public class SmsChannelControl
    {
        #region GetChannel 获得通道列表
        /// <summary>
        /// 获得通道列表
        /// </summary>
        /// <returns></returns>
        public ChannelList GetChannel()
        {
            var oChannel = new ChannelList();
            var oList = new List<SmsChannelUnit>();
            foreach (int i in Enum.GetValues(typeof(smsEnum.smsChannel)))
            {
                var oItem = new SmsChannelUnit();
                oItem.ChannelId = i;
                oItem.ChannelName = Enum.GetName(typeof(smsEnum.smsChannel), i);
                oList.Add(oItem);
            }
            oChannel.Channel = oList;
            return oChannel;
        }
        #endregion

        #region GetChannelBalance 获得通道余额信息
        /// <summary>
        /// 获得通道余额信息
        /// </summary>
        /// <param name="channelId"></param>
        /// <returns></returns>
        public string GetChannelBalance(int channelId)
        {
            string sUrl = "http://web.i200.cn:30018/date.aspx?dts=" + channelId.ToString();
            var sResult = CommonLib.Helper.SendHttpGet(sUrl, null);
            return sResult;
        } 
        #endregion

        #region SetChannel 更新运营商绑定通道设置
        /// <summary>
        /// 更新运营商绑定通道设置
        /// </summary>
        /// <param name="channelName">通道名称</param>
        /// <param name="channelId">通道ID</param>
        /// <returns></returns>
        public int SetChannel(string channelName, int channelId)
        {
            //运营商绑定方式
            int iResult = -1;
            var smsChannel = new SmsChannelBLL();
            bool bResult = smsChannel.SetSmsChannelOption(channelName, channelId);
            if (bResult)
            {
                //通知短信服务程序切换更新通道信息
                string smsReloadUrl = "http://web.i200.cn:30018/date.aspx?dts=reload";
                CommonLib.Helper.SendHttpGet(smsReloadUrl, null);
                iResult = 1;
            }

            return iResult;
        } 
        #endregion

        #region SetRevisitChannel 更新后台回访通道设置
        /// <summary>
        /// 更新后台回访通道设置
        /// </summary>
        /// <param name="channelName">通道名称</param>
        /// <param name="channelId">通道ID</param>
        /// <returns></returns>
        public int SetRevisitChannel(string channelName, int channelId)
        {
            //运营商绑定方式
            int iResult = -1;
            var smsChannel = new SmsChannelBLL();

            if (channelName == "Return_mobile")
            {
                channelName = "group_mobile";
            }
            else if (channelName == "Return_unicom")
            {
                channelName = "group_unicom";
            }
            else if (channelName == "Return_telecom")
            {
                channelName = "group_telecom";
            }

            bool bResult = smsChannel.SetSysSmsChannelOption(channelName, channelId);
            if (bResult)
            {
                //通知短信服务程序切换更新通道信息
                string smsReloadUrl = "http://127.0.0.1:10080/date.aspx?dts=reload";
                CommonLib.Helper.SendHttpGet(smsReloadUrl, null);
                iResult = 1;
            }

            return iResult;
        }
        #endregion
    }
}
