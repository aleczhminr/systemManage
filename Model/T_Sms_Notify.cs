using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    //T_Sms_Notify
    /// <summary>
    /// T_Sms_Notify
    /// </summary>	
    [Serializable]
    public partial class T_Sms_Notify
    {

        /// <summary>
        /// 队列Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 店铺id
        /// </summary>
        public int AccId { get; set; }

        /// <summary>
        /// 会员uid列表
        /// </summary>
        public string UserList { get; set; }

        /// <summary>
        /// 已发送会员uid列表
        /// </summary>
        public string SendedUserList { get; set; }

        /// <summary>
        /// 号码数
        /// </summary>
        [Display(Name = "号码数")]
        public int UserCnt { get; set; }

        /// <summary>
        /// 短信内容
        /// </summary>
        [Display(Name = "短信内容")]
        public string SmsContent { get; set; }

        /// <summary>
        /// 短信类型
        /// </summary>
        public int SmsType { get; set; }

        /// <summary>
        /// 余额不足是否继续发送
        /// </summary>
        public int UseBalance { get; set; }

        /// <summary>
        /// 优先级
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// 成功发送号码数
        /// </summary>
        public int SucceedCnt { get; set; }

        /// <summary>
        /// 失败数
        /// </summary>
        [Display(Name = "失败")]
        public int FailCnt { get; set; }

        /// <summary>
        /// 跳过数
        /// </summary>
        public int SkipCnt { get; set; }

        /// <summary>
        /// 短信发送状态
        /// </summary>
        [Display(Name = "状态")]
        public int SmsStatus { get; set; }

        /// <summary>
        /// 提交时间
        /// </summary>
        [Display(Name = "提交时间")]
        public DateTime SubmitTime { get; set; }

        /// <summary>
        /// 定时日期
        /// </summary>
        public DateTime RegularTime { get; set; }

        /// <summary>
        /// 完成日期
        /// </summary>
        [Display(Name = "发送时间")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}")]
        public DateTime CompleteTime { get; set; }

        /// <summary>
        /// 短信通道
        /// </summary>
        public int SmsChannel { get; set; }

        /// <summary>
        /// 手机发送列表
        /// </summary>
        public string PhoneList { get; set; }

        /// <summary>
        /// 已发送成功列表
        /// </summary>
        public string SendedPhoneList { get; set; }

        /// <summary>
        /// 发送总数
        /// </summary>
        [Display(Name = "条数")]
        public int TotalSmsCnt { get; set; }

        /// <summary>
        /// 实际发送总数
        /// </summary>
        [Display(Name = "成功")]
        public int RealSmsCnt { get; set; }

        /// <summary>
        /// 是否免费
        /// </summary>
        public int IsFree { get; set; }

        /// <summary>
        /// 错误描述
        /// </summary>
        public string ErrDesc { get; set; }

        /// <summary>
        /// 通道扩展信息
        /// </summary>
        public string ChannelEx { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public int Review { get; set; }

        /// <summary>
        /// 审核人员Id
        /// </summary>
        public int ReviewOperator { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime? ReviewDate { get; set; }

        /// <summary>
        /// 审核备注
        /// </summary>
        [Display(Name = "审核处理")]
        public string ReviewDesc { get; set; }

    }

    public class SmsNotifyItem : T_Sms_Notify
    {
        /// <summary>
        /// 店铺名称
        /// </summary>
        [Display(Name = "店铺名称")]
        public string StoreName { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        [Display(Name = "审核状态")]
        public string ReviewName { get; set; }

        /// <summary>
        /// 操作人员名称
        /// </summary>
        public string OperatorName { get; set; }

        /// <summary>
        /// 短信长度
        /// </summary>
        public int ContentLength { get; set; }

        /// <summary>
        /// 短信分类
        /// </summary>
        public string SmsTypeName { get; set; }

        /// <summary>
        /// 发送状态显示
        /// </summary>
        public string StatusName { get; set; }

        /// <summary>
        /// 发送优先级
        /// </summary>
        [Display(Name = "优先级")]
        public string PriorityName { get; set; }
    }

    /// <summary>
    /// 短信列表Model
    /// </summary>
    public class SmsListModel
    {
        public SmsListModel()
        {
            rowCount = 0;
            PageNow = 1;
            MaxPage = 1;
            SumCnt = 0;
            SumSuccess = 0; SumFail = 0;
            SumFree = 0;
        }
        /// <summary>
        /// 每页面显示数
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 总行数
        /// </summary>
        public int rowCount { get; set; }
        /// <summary>
        /// 当前页数
        /// </summary>
        public int PageNow { get; set; }

        /// <summary>
        /// 分页Html
        /// </summary>
        public string PageHtml { get; set; }

        /// <summary>
        /// 发送总计
        /// </summary>
        public int SumCnt { get; set; }

        /// <summary>
        /// 成功条数
        /// </summary>
        public int SumSuccess { get; set; }

        /// <summary>
        /// 失败条数
        /// </summary>
        public int SumFail { get; set; }

        /// <summary>
        /// 免费条数
        /// </summary>
        public int SumFree { get; set; }

        /// <summary>
        /// 审核列表
        /// </summary>
        public List<SmsNotifyItem> Data { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int MaxPage { get; set; }
    }

    /// <summary>
    /// 短信审核Model
    /// </summary>
    public class ReviewModel
    {
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 当前页数
        /// </summary>
        public int PageNow { get; set; }

        /// <summary>
        /// 分页Html
        /// </summary>
        public string PageHtml { get; set; }

        /// <summary>
        /// 审核通过数量
        /// </summary>
        public int PassCnt { get; set; }

        /// <summary>
        /// 等待数量
        /// </summary>
        public int WaitCnt { get; set; }

        /// <summary>
        /// 拒绝条数
        /// </summary>
        public int RejectCnt { get; set; }

        /// <summary>
        /// 审核列表
        /// </summary>
        public List<SmsNotifyItem> Data { get; set; }
    }

    #region SmsEditChannel 短信通道设置
    /// <summary>
    /// 短信通道设置
    /// </summary>
    public class SmsEditChannel
    {
        /// <summary>
        /// 移动
        /// </summary>
        public int ChinaMobile { get; set; }

        /// <summary>
        /// 联通
        /// </summary>
        public int ChinaUnicom { get; set; }

        /// <summary>
        /// 电信
        /// </summary>
        public int ChinaTelecom { get; set; }
    }
    #endregion
}

