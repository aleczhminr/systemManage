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
    public class SmsChannelBLL
    {
        /// <summary>
        /// 获得短信通道设置信息
        /// </summary>
        /// <returns></returns>
        public SmsChannelInfo GetChannelInfo()
        {
            SmsChannelDal dal = new SmsChannelDal();
            SmsChannelInfo smsChannelInfo = dal.GetChannelInfo();
            if (smsChannelInfo.SendMode == 2)
            {
                //运营商绑定模式
                smsChannelInfo.SmsOperator.SmsSystem.ChinaMobile.ChannelName = Enum.GetName(typeof(smsEnum.smsChannel), smsChannelInfo.SmsOperator.SmsSystem.ChinaMobile.ChannelId);
                smsChannelInfo.SmsOperator.SmsSystem.ChinaUnicom.ChannelName = Enum.GetName(typeof(smsEnum.smsChannel), smsChannelInfo.SmsOperator.SmsSystem.ChinaUnicom.ChannelId);
                smsChannelInfo.SmsOperator.SmsSystem.ChinaTelecom.ChannelName = Enum.GetName(typeof(smsEnum.smsChannel), smsChannelInfo.SmsOperator.SmsSystem.ChinaTelecom.ChannelId);

                smsChannelInfo.SmsOperator.SmsFast.ChinaMobile.ChannelName = Enum.GetName(typeof(smsEnum.smsChannel), smsChannelInfo.SmsOperator.SmsFast.ChinaMobile.ChannelId);
                smsChannelInfo.SmsOperator.SmsFast.ChinaUnicom.ChannelName = Enum.GetName(typeof(smsEnum.smsChannel), smsChannelInfo.SmsOperator.SmsFast.ChinaUnicom.ChannelId);
                smsChannelInfo.SmsOperator.SmsFast.ChinaTelecom.ChannelName = Enum.GetName(typeof(smsEnum.smsChannel), smsChannelInfo.SmsOperator.SmsFast.ChinaTelecom.ChannelId);

                smsChannelInfo.SmsOperator.SmsGroup.ChinaMobile.ChannelName = Enum.GetName(typeof(smsEnum.smsChannel), smsChannelInfo.SmsOperator.SmsGroup.ChinaMobile.ChannelId);
                smsChannelInfo.SmsOperator.SmsGroup.ChinaUnicom.ChannelName = Enum.GetName(typeof(smsEnum.smsChannel), smsChannelInfo.SmsOperator.SmsGroup.ChinaUnicom.ChannelId);
                smsChannelInfo.SmsOperator.SmsGroup.ChinaTelecom.ChannelName = Enum.GetName(typeof(smsEnum.smsChannel), smsChannelInfo.SmsOperator.SmsGroup.ChinaTelecom.ChannelId);
            }
            else
            {
                //通道绑定模式
                smsChannelInfo.SmsPriority.SysPriority.ChannelName = Enum.GetName(typeof(smsEnum.smsChannel), smsChannelInfo.SmsPriority.SysPriority.ChannelId);
                smsChannelInfo.SmsPriority.FastPriority.ChannelName = Enum.GetName(typeof(smsEnum.smsChannel), smsChannelInfo.SmsPriority.FastPriority.ChannelId);
                smsChannelInfo.SmsPriority.GroupPriority.ChannelName = Enum.GetName(typeof(smsEnum.smsChannel), smsChannelInfo.SmsPriority.GroupPriority.ChannelId);

            }
            return smsChannelInfo;
        }

        /// <summary>
        /// 更新运营商绑定通道设置
        /// </summary>
        /// <param name="typeName">通道名称</param>
        /// <param name="channelId">通道ID</param>
        /// <returns></returns>
        public bool SetSmsChannelOption(string typeName, int channelId)
        {
            bool bResult = false;
            SmsChannelDal dal = new SmsChannelDal();
            SmsChannelInfo smsChannelInfo = GetChannelInfo();

            switch (typeName)
            {
                case "sys_mobile":
                    //系统_移动
                    smsChannelInfo.SmsOperator.SmsSystem.ChinaMobile.ChannelId = channelId;
                    smsChannelInfo.SmsOperator.SmsSystem.ChinaMobile.ChannelName = null;
                    break;
                case "sys_unicom":
                    //系统_联通
                    smsChannelInfo.SmsOperator.SmsSystem.ChinaUnicom.ChannelId = channelId;
                    smsChannelInfo.SmsOperator.SmsSystem.ChinaUnicom.ChannelName = null;
                    break;
                case "sys_telecom":
                    //系统_电信
                    smsChannelInfo.SmsOperator.SmsSystem.ChinaTelecom.ChannelId = channelId;
                    smsChannelInfo.SmsOperator.SmsSystem.ChinaTelecom.ChannelName = null;
                    break;
                case "fast_mobile":
                    //普通_移动
                    smsChannelInfo.SmsOperator.SmsFast.ChinaMobile.ChannelId = channelId;
                    smsChannelInfo.SmsOperator.SmsFast.ChinaMobile.ChannelName = null;
                    break;
                case "fast_unicom":
                    //普通_移动
                    smsChannelInfo.SmsOperator.SmsFast.ChinaUnicom.ChannelId = channelId;
                    smsChannelInfo.SmsOperator.SmsFast.ChinaUnicom.ChannelName = null;
                    break;
                case "fast_telecom":
                    //普通_电信
                    smsChannelInfo.SmsOperator.SmsFast.ChinaTelecom.ChannelId = channelId;
                    smsChannelInfo.SmsOperator.SmsFast.ChinaTelecom.ChannelName = null;
                    break;
                case "group_mobile":
                    //广告_移动
                    smsChannelInfo.SmsOperator.SmsGroup.ChinaMobile.ChannelId = channelId;
                    smsChannelInfo.SmsOperator.SmsGroup.ChinaMobile.ChannelName = null;
                    break;
                case "group_unicom":
                    //广告_联通
                    smsChannelInfo.SmsOperator.SmsGroup.ChinaUnicom.ChannelId = channelId;
                    smsChannelInfo.SmsOperator.SmsGroup.ChinaUnicom.ChannelName = null;
                    break;
                case "group_telecom":
                    //广告_电信
                    smsChannelInfo.SmsOperator.SmsGroup.ChinaTelecom.ChannelId = channelId;
                    smsChannelInfo.SmsOperator.SmsGroup.ChinaTelecom.ChannelName = null;
                    break;
            }

            SmsChannel smsChannel = smsChannelInfo.SmsOperator;
            return dal.SetSmsChannelOption(smsChannel);
        }


        /// <summary>
        /// 获得回访短信通道设置信息
        /// </summary>
        /// <returns></returns>
        public SmsChannelInfo GetSysChannelInfo()
        {
            SmsChannelDal dal = new SmsChannelDal();
            SmsChannelInfo smsChannelInfo = dal.GetSysChannelInfo();
            if (smsChannelInfo.SendMode == 2)
            {
                //运营商绑定模式
                smsChannelInfo.SmsOperator.SmsSystem.ChinaMobile.ChannelName = Enum.GetName(typeof(smsEnum.smsChannel), smsChannelInfo.SmsOperator.SmsSystem.ChinaMobile.ChannelId);
                smsChannelInfo.SmsOperator.SmsSystem.ChinaUnicom.ChannelName = Enum.GetName(typeof(smsEnum.smsChannel), smsChannelInfo.SmsOperator.SmsSystem.ChinaUnicom.ChannelId);
                smsChannelInfo.SmsOperator.SmsSystem.ChinaTelecom.ChannelName = Enum.GetName(typeof(smsEnum.smsChannel), smsChannelInfo.SmsOperator.SmsSystem.ChinaTelecom.ChannelId);

                smsChannelInfo.SmsOperator.SmsFast.ChinaMobile.ChannelName = Enum.GetName(typeof(smsEnum.smsChannel), smsChannelInfo.SmsOperator.SmsFast.ChinaMobile.ChannelId);
                smsChannelInfo.SmsOperator.SmsFast.ChinaUnicom.ChannelName = Enum.GetName(typeof(smsEnum.smsChannel), smsChannelInfo.SmsOperator.SmsFast.ChinaUnicom.ChannelId);
                smsChannelInfo.SmsOperator.SmsFast.ChinaTelecom.ChannelName = Enum.GetName(typeof(smsEnum.smsChannel), smsChannelInfo.SmsOperator.SmsFast.ChinaTelecom.ChannelId);

                smsChannelInfo.SmsOperator.SmsGroup.ChinaMobile.ChannelName = Enum.GetName(typeof(smsEnum.smsChannel), smsChannelInfo.SmsOperator.SmsGroup.ChinaMobile.ChannelId);
                smsChannelInfo.SmsOperator.SmsGroup.ChinaUnicom.ChannelName = Enum.GetName(typeof(smsEnum.smsChannel), smsChannelInfo.SmsOperator.SmsGroup.ChinaUnicom.ChannelId);
                smsChannelInfo.SmsOperator.SmsGroup.ChinaTelecom.ChannelName = Enum.GetName(typeof(smsEnum.smsChannel), smsChannelInfo.SmsOperator.SmsGroup.ChinaTelecom.ChannelId);
            }
            else
            {
                //通道绑定模式
                smsChannelInfo.SmsPriority.SysPriority.ChannelName = Enum.GetName(typeof(smsEnum.smsChannel), smsChannelInfo.SmsPriority.SysPriority.ChannelId);
                smsChannelInfo.SmsPriority.FastPriority.ChannelName = Enum.GetName(typeof(smsEnum.smsChannel), smsChannelInfo.SmsPriority.FastPriority.ChannelId);
                smsChannelInfo.SmsPriority.GroupPriority.ChannelName = Enum.GetName(typeof(smsEnum.smsChannel), smsChannelInfo.SmsPriority.GroupPriority.ChannelId);

            }
            return smsChannelInfo;
        }


        /// <summary>
        /// 更新后台回访运营商绑定通道设置
        /// </summary>
        /// <param name="typeName">通道名称</param>
        /// <param name="channelId">通道ID</param>
        /// <returns></returns>
        public bool SetSysSmsChannelOption(string typeName, int channelId)
        {
            bool bResult = false;
            SmsChannelDal dal = new SmsChannelDal();
            SmsChannelInfo smsChannelInfo = GetSysChannelInfo();

            switch (typeName)
            {
                case "sys_mobile":
                    //系统_移动
                    smsChannelInfo.SmsOperator.SmsSystem.ChinaMobile.ChannelId = channelId;
                    smsChannelInfo.SmsOperator.SmsSystem.ChinaMobile.ChannelName = "";
                    break;
                case "sys_unicom":
                    //系统_联通
                    smsChannelInfo.SmsOperator.SmsSystem.ChinaUnicom.ChannelId = channelId;
                    smsChannelInfo.SmsOperator.SmsSystem.ChinaUnicom.ChannelName = "";
                    break;
                case "sys_telecom":
                    //系统_电信
                    smsChannelInfo.SmsOperator.SmsSystem.ChinaTelecom.ChannelId = channelId;
                    smsChannelInfo.SmsOperator.SmsSystem.ChinaTelecom.ChannelName = "";
                    break;
                case "fast_mobile":
                    //普通_移动
                    smsChannelInfo.SmsOperator.SmsFast.ChinaMobile.ChannelId = channelId;
                    smsChannelInfo.SmsOperator.SmsFast.ChinaMobile.ChannelName = "";
                    break;
                case "fast_unicom":
                    //普通_移动
                    smsChannelInfo.SmsOperator.SmsFast.ChinaUnicom.ChannelId = channelId;
                    smsChannelInfo.SmsOperator.SmsFast.ChinaUnicom.ChannelName = "";
                    break;
                case "fast_telecom":
                    //普通_电信
                    smsChannelInfo.SmsOperator.SmsFast.ChinaTelecom.ChannelId = channelId;
                    smsChannelInfo.SmsOperator.SmsFast.ChinaTelecom.ChannelName = "";
                    break;
                case "group_mobile":
                    //广告_移动
                    smsChannelInfo.SmsOperator.SmsGroup.ChinaMobile.ChannelId = channelId;
                    smsChannelInfo.SmsOperator.SmsGroup.ChinaMobile.ChannelName = "";
                    break;
                case "group_unicom":
                    //广告_联通
                    smsChannelInfo.SmsOperator.SmsGroup.ChinaUnicom.ChannelId = channelId;
                    smsChannelInfo.SmsOperator.SmsGroup.ChinaUnicom.ChannelName = "";
                    break;
                case "group_telecom":
                    //广告_电信
                    smsChannelInfo.SmsOperator.SmsGroup.ChinaTelecom.ChannelId = channelId;
                    smsChannelInfo.SmsOperator.SmsGroup.ChinaTelecom.ChannelName = "";
                    break;
            }

            SmsChannel smsChannel = smsChannelInfo.SmsOperator;
            return dal.SetSysSmsChannelOption(smsChannel);
        }

        /// <summary>
        /// 短信通道测试
        /// </summary>
        /// <param name="strContent"></param>
        /// <param name="phoneNum"></param>
        /// <param name="userCnt"></param>
        /// <param name="channelId"></param>
        /// <param name="smsFlag"></param>
        /// <returns></returns>
        public string GetChannelTest(string strContent, string phoneNum, int userCnt, int channelId, int smsFlag)
        {
            SmsChannelDal dal = new SmsChannelDal();
            return dal.GetChannelTest(strContent, phoneNum, userCnt, channelId, smsFlag);
        }

        /// <summary>
        /// 获得短信通道发送条数
        /// </summary>
        /// <param name="channelId">通道Id</param>
        /// <returns></returns>
        public int GetChannelSum(int channelId)
        {
            SmsChannelDal dal = new SmsChannelDal();
            return dal.GetChannelSum(channelId);
        }

    }
}
