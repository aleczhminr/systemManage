using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 运营商通道绑定设置
    /// </summary>
    public class SmsChannel
    {
        /// <summary>
        /// 系统通道
        /// </summary>
        public SmsChannelItem SmsSystem { get; set; }

        /// <summary>
        /// 快速通道
        /// </summary>
        public SmsChannelItem SmsFast { get; set; }

        /// <summary>
        /// 广告通道
        /// </summary>
        public SmsChannelItem SmsGroup { get; set; }
    }

    /// <summary>
    /// 运营商通道绑定设置Item
    /// </summary>
    public class SmsChannelItem
    {
        /// <summary>
        /// 移动
        /// </summary>
        public SmsChannelUnit ChinaMobile { get; set; }

        /// <summary>
        /// 联通
        /// </summary>
        public SmsChannelUnit ChinaUnicom { get; set; }

        /// <summary>
        /// 电信
        /// </summary>
        public SmsChannelUnit ChinaTelecom { get; set; }
    }


    public class SmsChannelUnit
    {
        /// <summary>
        /// 通道ID
        /// </summary>
        public int ChannelId { get; set; }

        /// <summary>
        /// 通道名称
        /// </summary>
        public string ChannelName { get; set; }
    }

    public class SmsPriorityItem
    {
        /// <summary>
        /// 系统优先级
        /// </summary>
        public SmsChannelUnit SysPriority { get; set; }

        /// <summary>
        /// 单发优先级
        /// </summary>
        public SmsChannelUnit FastPriority { get; set; }

        /// <summary>
        /// 群发优先级
        /// </summary>
        public SmsChannelUnit GroupPriority { get; set; }
    }

    /// <summary>
    /// 短信通道信息
    /// </summary>
    public class SmsChannelInfo
    {
        /// <summary>
        /// 发送方式
        /// </summary>
        public int SendMode { get; set; }

        /// <summary>
        /// 通道绑定设置
        /// </summary>
        public SmsPriorityItem SmsPriority { get; set; }

        /// <summary>
        /// 运营商绑定设置
        /// </summary>
        public SmsChannel SmsOperator { get; set; }
    }

    public class SmsConfigInfoTmp
    {
        public int sendmode { get; set; }
        public string Value { get; set; }
        public int sms_channel_sys { get; set; }
        public int sms_channel_f { get; set; }
        public int sms_channel_s { get; set; }
    }

    /// <summary>
    /// 通道列表
    /// </summary>
    public class ChannelList
    {
        public List<SmsChannelUnit> Channel { get; set; }
    }
}
