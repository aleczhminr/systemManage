using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //T_ExchangeLog
    /// <summary>
    /// T_ExchangeLog
    /// </summary>	
    [Serializable]
    public partial class T_ExchangeLog
    {

        /// <summary>
        /// id
        /// </summary>		
        public int id { get; set; }
        /// <summary>
        /// accid
        /// </summary>		
        public int accid { get; set; }
        /// <summary>
        /// 产品id
        /// </summary>		
        public int eProjectId { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>		
        public string eProjectName { get; set; }
        /// <summary>
        /// 兑换积分
        /// </summary>		
        public int eIntegral { get; set; }
        /// <summary>
        /// 登录数量
        /// </summary>		
        public int eQuantity { get; set; }
        /// <summary>
        /// 状态
        /// </summary>		
        public int eState { get; set; }
        /// <summary>
        /// 收件人
        /// </summary>		
        public string eRecipient { get; set; }
        /// <summary>
        /// 收件人电话
        /// </summary>		
        public string ePhoneNumber { get; set; }
        /// <summary>
        /// 省
        /// </summary>		
        public string eProvince { get; set; }
        /// <summary>
        /// 街道
        /// </summary>		
        public string eStreet { get; set; }
        /// <summary>
        /// 邮编
        /// </summary>		
        public string ePostcode { get; set; }
        /// <summary>
        /// 操作人员
        /// </summary>		
        public string eOperator { get; set; }
        /// <summary>
        /// IP
        /// </summary>		
        public string eIp { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>		
        public DateTime eInsertTime { get; set; }
        /// <summary>
        /// 物流
        /// </summary>		
        public string eLogistics { get; set; }
        /// <summary>
        /// 物流单号
        /// </summary>		
        public string eLogisticsNumber { get; set; }
        /// <summary>
        /// 客服人员
        /// </summary>		
        public string eSysName { get; set; }
        /// <summary>
        /// 客服记录时间
        /// </summary>		
        public DateTime eSysTime { get; set; }

    }

    public partial class T_ExchangeLogInfo : T_ExchangeLog
    {
        /// <summary>
        /// 店铺名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 状态名称
        /// </summary>
        public string eStateName { get; set; }
    }

}

