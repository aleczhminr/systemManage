using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class MessageBatch
    {
        public int Id { get; set; }
        /// <summary>
        /// 批次Id
        /// </summary>
        public string BatchId { get; set; }
        /// <summary>
        /// Accid集合Json
        /// </summary>
        public string AccIdSet { get; set; }
        /// <summary>
        /// Accid计数
        /// </summary>
        public int AccIdCount { get; set; }
        /// <summary>
        /// 条件集合
        /// </summary>
        public string ConditionDesc { get; set; }
        /// <summary>
        /// AccId来源标记
        /// </summary>
        public int SourceType { get; set; }
        /// <summary>
        /// 筛选器记录标记
        /// </summary>
        public string FilterLogVerify { get; set; }
        /// <summary>
        /// 批次生成时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 批次提交时间
        /// </summary>
        public DateTime SubmitTime { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        public int ReviewState { get; set; }
        /// <summary>
        /// 发送状态
        /// </summary>
        public int SendingStatus { get; set; }
        /// <summary>
        /// 统计发送到达数
        /// </summary>
        public int FeedBackArrive { get; set; }
        /// <summary>
        /// 统计到达打开数
        /// </summary>
        public int FeedBackOpen { get; set; }
        /// <summary>
        /// 信息通道标记
        /// </summary>
        public int ChannelId { get; set; }
        /// <summary>
        /// 消息标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 消息备注（发送说明）
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 消息跟踪动作分类
        /// </summary>
        public int ActionType { get; set; }
        /// <summary>
        /// 消息动作标记
        /// </summary>
        public int ActionMark { get; set; }
        /// <summary>
        /// 发送者
        /// </summary>
        public int Operator { get; set; }
        /// <summary>
        /// 发送详情表的ID
        /// </summary>
        public string SendId { get; set; }
        /// <summary>
        /// 操作者姓名
        /// </summary>
        public string OperatorName { get; set; }
        /// <summary>
        /// 推送消息打开率
        /// </summary>
        public string OpenRatio { get; set; }

        public string ContentType { get; set; }
        public string ContentUrl { get; set; }
    }

    public class MessageChannel
    {
        public int Id { get; set; }
        /// <summary>
        /// 信息通道名称
        /// </summary>
        public string ChannelName { get; set; }
    }

    public class MessageDetail
    {
        public int Id { get; set; }
        /// <summary>
        /// 批次Id
        /// </summary>
        public string BatchId { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public int AccId { get; set; }
        /// <summary>
        /// 消息标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 到达标记
        /// </summary>
        public int ArriveMark { get; set; }
        /// <summary>
        /// 打开标记
        /// </summary>
        public int OpenMark { get; set; }
        /// <summary>
        /// 信息通道Id
        /// </summary>
        public int ChannelId { get; set; }
        /// <summary>
        /// 消息发送说明
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 消息跟踪动作分类
        /// </summary>
        public int ActionType { get; set; }
        /// <summary>
        /// 消息动作标记
        /// </summary>
        public int ActionMark { get; set; }

        public DateTime CreateTime { get; set; }
        public DateTime SubmitTime { get; set; }

        public string CompanyName { get; set; }
        public string AccIdNumber { get; set; }
        public string ContentType { get; set; }
        public string ContentUrl { get; set; }
    }

    public class MessageSending
    {
        public int Id { get; set; }
        public string BatchId { get; set; }
        public string AccIdSet { get; set; }
        public int AccIdCount { get; set; }

        /// <summary>
        /// 批次渠道集合
        /// </summary>
        public string ChannelSet { get; set; }
        public string Remark { get; set; }
        public int ActionMark { get; set; }
        public int ActionType { get; set; }
        public DateTime CreateTime { get; set; }
    }

    public class SmsRequest
    {
        /// <summary>
        /// 短信内容
        /// </summary>
        public string smsContent { get; set; }
        /// <summary>
        /// 号码列表
        /// </summary>
        public string[] phoneList { get; set; }
        /// <summary>
        /// 短信类型
        /// </summary>
        public int smsType { get; set; }
    }

    public class EmailRequest
    {
        /// <summary>
        /// 邮件标题
        /// </summary>
        public string EmailTitle { get; set; }

        /// <summary>
        /// 邮件内容
        /// </summary>
        public string EmailContent { get; set; }

        /// <summary>
        /// 邮件备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 邮件通道
        /// </summary>
        public int EmailChannel { get; set; }

        /// <summary>
        /// 模板ID
        /// </summary>
        public int TemplateIndex { get; set; }

        /// <summary>
        /// 发送类型
        /// </summary>
        public int SendType { get; set; }

        /// <summary>
        /// 邮件地址列表
        /// </summary>
        public string[] AddressList { get; set; }
    }

    public class ResponseModel
    {
        /// <summary>
        /// 执行状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 错误代码
        /// </summary>
        public int ErrCode { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrMsg { get; set; }

        /// <summary>
        /// 业务Obj
        /// </summary>
        public object Data { get; set; }
    }

    public class MobMessageRequest
    {
        public MobMessageRequest()
        {
            operatorId = 0;
        }

        public string accIdList { get; set; }
        public string msgTitle { get; set; }
        public string msgContent { get; set; }
        public int operatorId { get; set; }
        public string operatorName { get; set; }
        public DateTime? timingTime { get; set; }
        public string ContentType { get; set; }
        public string ContentUrl { get; set; }
    }
    public class WebMessageRequest
    {
        public WebMessageRequest()
        {
            operatorId = 0;
        }
        public string accIdList { get; set; }
        public string msgTitle { get; set; }
        public string msgContent { get; set; }
        public int operatorId { get; set; }
        public string operatorName { get; set; }
        public DateTime? timingTime { get; set; }
    }

    public class SendStatus
    {
        public string SendId { get; set; }
        public List<SendStatusItem> DataList { get; set; }
    }

    public class SendStatusItem
    {
        /// <summary>
        /// 目标Id
        /// </summary>
        public string TargetId { get; set; }

        /// <summary>
        /// 发送状态
        /// </summary>
        public int SendStatus { get; set; }

        /// <summary>
        /// 是否弹窗
        /// </summary>
        public int IsPush { get; set; }

        /// <summary>
        /// 是否已读
        /// </summary>
        public int IsRead { get; set; }
    }

    public class BatchSummary
    {
        public BatchSummary(string batchId)
        {
            BatchId = batchId;

            AccIdCount = 0;
            SmsSend = 0;
            SmsArrive = 0;
            SmsOpen = 0;
            EmailSend = 0;
            EmailArrive = 0;
            EmailOpen = 0;
            WebSend = 0;
            WebArrive = 0;
            WebOpen = 0;
            MobSend = 0;
            MobArrive = 0;
            MobOpen = 0;
        }

        public string BatchId { get;set; }
        public int AccIdCount { get; set; }        
        public int SmsSend { get;set; }
        public int SmsArrive { get; set; }
        public int SmsOpen { get; set; }

        /// <summary>
        /// 短信打开率
        /// </summary>
        public string SmsPartition { get; set; }

        public int EmailSend { get; set; }
        public int EmailArrive { get; set; }
        public int EmailOpen { get; set; }

        /// <summary>
        /// 邮件打开率
        /// </summary>
        public string EmailPartition { get; set; }

        public int WebSend { get;set;}
        public int WebArrive { get; set; }
        public int WebOpen { get; set; }

        /// <summary>
        /// 站内推送打开率
        /// </summary>
        public string WebPartition { get; set; }

        public int MobSend { get; set; }
        public int MobArrive { get; set; }
        public int MobOpen { get; set; }

        /// <summary>
        /// 移动端推送打开率
        /// </summary>
        public string MobPartition { get; set; }

        public string SendRemark { get; set; }
        public DateTime CreateTime { get; set; }

    }

    public class HeaderModel
    {
        /// <summary>
        /// 加密签名
        /// </summary>
        public string Signature { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public string Timestamp { get; set; }

        /// <summary>
        /// 随机数
        /// </summary>
        public string Nonce { get; set; }

        /// <summary>
        /// app秘钥
        /// </summary>
        public string AppKey { get; set; }

        public string SecretKey { get; set; }

    }

    public class GenericMessageModel
    {
        public int ChannelId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Remark { get; set; }
        public string AccidSet { get; set; }
    }

    public class RecurringUpdateModel
    {
        public string BatchId { get; set; }
        public int ChannelId { get; set; }
        public string SendId { get; set; }
    }
}
