using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //T_OutLinkType
    /// <summary>
    /// 推广连接类别
    /// </summary>	
    [Serializable]
    public partial class T_OutLinkType
    {

        /// <summary>
        /// id
        /// </summary>		
        public int id { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>		
        public string ot_name { get; set; }
        /// <summary>
        /// 分类排序
        /// </summary>		
        public int ot_order { get; set; }
        /// <summary>
        /// 父ID
        /// </summary>		
        public int ot_id { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>		
        public DateTime insTime { get; set; }
        /// <summary>
        /// 录入人
        /// </summary>		
        public string insName { get; set; }

    }
    /// <summary>
    /// 推广链接
    /// </summary>
    public partial class OutLinkType
    {
        public OutLinkType()
        {
            itemList = new List<OutLinkType>();
        }
        public OutLinkType(int _id,string _ot_name)
        {
            id = _id;
            ot_name = _ot_name;
            itemList = new List<OutLinkType>();
        }
        public int id { get; set; }
        public string ot_name { get; set; }

        public List<OutLinkType> itemList { get; set; }
    } 
}

