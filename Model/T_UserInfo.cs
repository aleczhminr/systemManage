using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //T_UserInfo
    /// <summary>
    /// T_UserInfo
    /// </summary>	
    [Serializable]
    public partial class T_UserInfo
    {

        /// <summary>
        /// uid
        /// </summary>		
        public long uid { get; set; }
        /// <summary>
        /// uNumber
        /// </summary>		
        public string uNumber { get; set; }
        /// <summary>
        /// uPwd
        /// </summary>		
        public string uPwd { get; set; }
        /// <summary>
        /// uName
        /// </summary>		
        public string uName { get; set; }
        /// <summary>
        /// uSex
        /// </summary>		
        public int uSex { get; set; }
        /// <summary>
        /// uEmail
        /// </summary>		
        public string uEmail { get; set; }
        /// <summary>
        /// uPhone
        /// </summary>		
        public string uPhone { get; set; }
        /// <summary>
        /// uRegTime
        /// </summary>		
        public DateTime uRegTime { get; set; }
        /// <summary>
        /// uQQ
        /// </summary>		
        public string uQQ { get; set; }
        /// <summary>
        /// uIntegral
        /// </summary>		
        public int uIntegral { get; set; }
        /// <summary>
        /// accID
        /// </summary>		
        public int accID { get; set; }
        /// <summary>
        /// uIntegralUsed
        /// </summary>		
        public int uIntegralUsed { get; set; }
        /// <summary>
        /// uAddress
        /// </summary>		
        public string uAddress { get; set; }
        /// <summary>
        /// uLike
        /// </summary>		
        public string uLike { get; set; }
        /// <summary>
        /// uRemark
        /// </summary>		
        public string uRemark { get; set; }
        /// <summary>
        /// uStoreMoney
        /// </summary>		
        public decimal uStoreMoney { get; set; }
        /// <summary>
        /// uGroup
        /// </summary>		
        public int uGroup { get; set; }
        /// <summary>
        /// uPY
        /// </summary>		
        public string uPY { get; set; }
        /// <summary>
        /// uPinYin
        /// </summary>		
        public string uPinYin { get; set; }
        /// <summary>
        /// isTime
        /// </summary>		
        public int isTime { get; set; }
        /// <summary>
        /// keepTime
        /// </summary>		
        public int keepTime { get; set; }
        /// <summary>
        /// uNick
        /// </summary>		
        public int uNick { get; set; }
        /// <summary>
        /// uRank
        /// </summary>		
        public int uRank { get; set; }
        /// <summary>
        /// uInsertTime
        /// </summary>		
        public DateTime uInsertTime { get; set; }
        /// <summary>
        /// uOperator
        /// </summary>		
        public int uOperator { get; set; }
        /// <summary>
        /// uStoreTimes
        /// </summary>		
        public int uStoreTimes { get; set; }
        /// <summary>
        /// uLastBuyDate
        /// </summary>		
        public DateTime uLastBuyDate { get; set; }
        /// <summary>
        /// uAuthCode
        /// </summary>		
        public string uAuthCode { get; set; }
        /// <summary>
        /// uAuthCodeTime
        /// </summary>		
        public DateTime uAuthCodeTime { get; set; }
        /// <summary>
        /// uLockRank
        /// </summary>		
        public int uLockRank { get; set; }
        /// <summary>
        /// uPortrait
        /// </summary>		
        public string uPortrait { get; set; }
        /// <summary>
        /// weixin
        /// </summary>		
        public string weixin { get; set; }
        /// <summary>
        /// alipay
        /// </summary>		
        public string alipay { get; set; }

    }
}

