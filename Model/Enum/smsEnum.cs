using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Enum
{
    public static class smsEnum
    {
        //
        /// <summary>
        /// 短信通道
        /// </summary>
        public enum smsChannel : int
        {
            //亿美系统 = 10,
            //亿美快速 = 11,
            //亿美广告 = 12,
            //华兴快速 = 21,
            //华兴广告 = 22,
            电信通道 = 31,
            //维拓1069 = 41,
            //维拓广告 = 42,
            //北程系统 = 51,
            助通系统_移动 = 60,
            助通系统 = 61,
            助通快速 = 62,
            助通广告 = 63,
            助通后台自用 = 64,
            助通通知=65,
            //云信高速触发 = 70,
            //云信备案通知 = 71,
            //云信综合营销 = 72,
            //云信测试通道 = 73,
            客通系统 = 80,
            客通快速 = 81,
            客通营销 = 82,
            //九象广告 = 92,
            云片系统 = 100,
            火尼营销 = 112,
            禁止发送 = 0
        }

        //系统发送优先级
        /// <summary>
        /// 系统发送优先级
        /// </summary>
        public enum smsPriority : int
        {
            审核 = -1,
            系统 = 0,
            普通 = 1,
            群发 = 2
        }

        //T_Sms_Notify_短信状态
        /// <summary>
        /// T_Sms_Notify_短信状态
        /// </summary>
        public enum smsStatus : int
        {
            等待发送 = 0,
            已提交 = 1,
            正在发送 = 2,
            发送失败 = 3
        }

        //短信类型
        /// <summary>
        /// 短信类型
        /// </summary>
        public enum smsType : int
        {
            重置密码 = 0,
            短信营销 = 1,
            充值提醒 = 2,
            电子账单 = 3,
            店铺经营 = 4,
            会员注册 = 6,
            生日祝福 = 7,
            日程提醒 = 8,
            用户注册 = 9,
            密码找回 = 10,
            订单提醒 = 11,
            回访营销 = 12,
            测试短信 = 99
        }

        //T_Sms_List_短信状态
        /// <summary>
        /// T_Sms_List_短信状态
        /// </summary>
        public enum smsListStatus : int
        {
            发送失败_敏感词 = -2,
            发送失败 = -1,
            发送成功 = 1

        }
        /// <summary>
        /// 短信审核状态
        /// </summary>
        public enum smsReview : int
        {
            等待审核 = 1,
            审核通过 = 2,
            拒绝发送 = 3
        }
    }
}
