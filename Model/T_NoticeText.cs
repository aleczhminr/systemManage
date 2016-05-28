using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //T_NoticeText
    /// <summary>
    /// T_NoticeText
    /// </summary>	
    [Serializable]
    public partial class T_NoticeText
    {

        /// <summary>
        /// id
        /// </summary>		
        public int id { get; set; }
        /// <summary>
        /// nType
        /// </summary>		
        public int nType { get; set; }
        /// <summary>
        /// nDisplay
        /// </summary>		
        public int nDisplay { get; set; }
        /// <summary>
        /// nTitle
        /// </summary>		
        public string nTitle { get; set; }
        /// <summary>
        /// nContent
        /// </summary>		
        public string nContent { get; set; }
        /// <summary>
        /// nTime
        /// </summary>		
        public DateTime nTime { get; set; }
        /// <summary>
        /// nOperatorName
        /// </summary>		
        public string nOperatorName { get; set; }
        /// <summary>
        /// nOperatorIp
        /// </summary>		
        public string nOperatorIp { get; set; }

    }


    public class NoticeTextModel
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public int Display { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime DateTime { get; set; }
    }
}

