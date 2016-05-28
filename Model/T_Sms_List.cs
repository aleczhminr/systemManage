using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //T_Sms_List
    /// <summary>
    /// 短信发送列表
    /// </summary>	
    [Serializable]
    public partial class T_Sms_List
    {

        /// <summary>
        /// id
        /// </summary>		
        public int id { get; set; }
        /// <summary>
        /// phoneNum
        /// </summary>		
        public string phoneNum { get; set; }
        /// <summary>
        /// smsContent
        /// </summary>		
        public string smsContent { get; set; }
        /// <summary>
        /// sendtime
        /// </summary>		
        public DateTime sendtime { get; set; }
        /// <summary>
        /// accID
        /// </summary>		
        public int accID { get; set; }
        /// <summary>
        /// smsType
        /// </summary>		
        public int smsType { get; set; }
        /// <summary>
        /// smsStatus
        /// </summary>		
        public int smsStatus { get; set; }
        /// <summary>
        /// smsChannel
        /// </summary>		
        public int smsChannel { get; set; }
        /// <summary>
        /// userID
        /// </summary>		
        public long userID { get; set; }
        /// <summary>
        /// priority
        /// </summary>		
        public int priority { get; set; }
        /// <summary>
        /// realCnt
        /// </summary>		
        public int realCnt { get; set; }
        /// <summary>
        /// notifyID
        /// </summary>		
        public long notifyID { get; set; }
        /// <summary>
        /// isFree
        /// </summary>		
        public int isFree { get; set; }
        /// <summary>
        /// errDesc
        /// </summary>		
        public string errDesc { get; set; }
        /// <summary>
        /// smsReport
        /// </summary>		
        public string smsReport { get; set; }
        /// <summary>
        /// Seqid
        /// </summary>		
        public long Seqid { get; set; }
        /// <summary>
        /// IspType
        /// </summary>		
        public int IspType { get; set; }

    }

    /// <summary>
    /// 短信信息
    /// </summary>
    public partial class T_Sms_ListBasic
    {
        public int id { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        public string phoneNumber { get; set; }
        /// <summary>
        /// 短信内容
        /// </summary>
        public string smsContent { get; set; }
        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime sendTime { get; set; }
        /// <summary>
        /// 短信类别
        /// </summary>
        public int smsType { get; set; }
        /// <summary>
        /// 短信状态
        /// </summary>
        public string smsTypeName { get; set; }
        /// <summary>
        /// 短信状态
        /// </summary>
        public int smsStatus { get; set; }
        /// <summary>
        /// 短信状态
        /// </summary>
        public string smsStatusName { get; set; }
        /// <summary>
        /// 短信通道
        /// </summary>
        public int smsChannel { get; set; }
        /// <summary>
        /// 是否免费
        /// </summary>
        public int isFree { get; set; }
    }


}

