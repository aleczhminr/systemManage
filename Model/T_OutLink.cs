using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //T_OutLink
    /// <summary>
    /// 推广链接
    /// </summary>	
    [Serializable]
    public partial class T_OutLink
    {

        /// <summary>
        /// id
        /// </summary>		
        public int id { get; set; }
        /// <summary>
        /// linktype
        /// </summary>		
        public int linktype { get; set; }
        /// <summary>
        /// linkurl
        /// </summary>		
        public string linkurl { get; set; }
        /// <summary>
        /// remark
        /// </summary>		
        public string remark { get; set; }
        /// <summary>
        /// ClickCount
        /// </summary>		
        public int ClickCount { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>		
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// managerid
        /// </summary>		
        public int managerid { get; set; }
        /// <summary>
        /// 1有效 0无效
        /// </summary>		
        public int state { get; set; }
        /// <summary>
        /// linkname
        /// </summary>		
        public string linkname { get; set; }
        /// <summary>
        /// linkClass
        /// </summary>		
        public int linkClass { get; set; }
        /// <summary>
        /// PV
        /// </summary>		
        public int PV { get; set; }
        /// <summary>
        /// EndTime
        /// </summary>		
        public DateTime EndTime { get; set; }
        /// <summary>
        /// ShortUrl
        /// </summary>		
        public string ShortUrl { get; set; }

    }
    /// <summary>
    /// 推广链接信息
    /// </summary>
    public partial class T_OutLinkInfo
    {
        public int id { get; set; }
        public string linkUrl { get; set; }
        public string remark { get; set; }
        public int ClickCount { get; set; }
        public DateTime createTime { get; set; }
        public int state { get; set; }
        public string linkName { get; set; }
        public string shortUrl { get; set; }
        public string manageName { get; set; }
        public string maxClassName { get; set; }
        public string minClassName { get; set; }

    }
}

