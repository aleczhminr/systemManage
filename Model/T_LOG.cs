using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //T_LOG
    /// <summary>
    /// T_LOG
    /// </summary>	
    [Serializable]
    public partial class T_LOG
    {

        /// <summary>
        /// ID
        /// </summary>		
        public int ID { get; set; }
        /// <summary>
        /// 日志类型，1登录日志
        /// </summary>		
        public int LogType { get; set; }
        /// <summary>
        /// OperDate
        /// </summary>		
        public DateTime OperDate { get; set; }
        /// <summary>
        /// Account_user_id
        /// </summary>		
        public int Account_user_id { get; set; }
        /// <summary>
        /// LogInfo
        /// </summary>		
        public string LogInfo { get; set; }
        /// <summary>
        /// 0表示系统日志,1手工
        /// </summary>		
        public int LogMode { get; set; }
        /// <summary>
        /// 关键字，例如登录的时候记录IP地址
        /// </summary>		
        public string TypeID { get; set; }
        /// <summary>
        /// Accountid
        /// </summary>		
        public int Accountid { get; set; }
        /// <summary>
        /// Loginbrslast
        /// </summary>		
        public string Loginbrslast { get; set; }

    }

    /// <summary>
    /// 店铺最后登录信息
    /// </summary>
    public partial class T_LogAccountLast
    {
        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime OperDate { get; set; }
        /// <summary>
        /// IP
        /// </summary>
        public string Ip { get; set; }
        /// <summary>
        /// IP解析地址
        /// </summary>
        public string IpAddressInfo { get; set; }
        /// <summary>
        /// 来源记录
        /// </summary>
        public string LogMode { get; set; }
        /// <summary>
        /// 来源
        /// </summary>
        public string LogSource { get; set; }
    }

    public class LogClientDic
    {
        public int Cnt { get; set; }
        public string Source { get; set; }
    }
}

