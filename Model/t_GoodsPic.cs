using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //t_GoodsPic
    /// <summary>
    /// t_GoodsPic
    /// </summary>	
    [Serializable]
    public partial class t_GoodsPic
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
        /// 商品ID
        /// </summary>		
        public int gId { get; set; }
        /// <summary>
        /// 商品图片
        /// </summary>		
        public string gPicUrl { get; set; }
        /// <summary>
        /// 说明
        /// </summary>		
        public string gPicDepict { get; set; }
        /// <summary>
        /// 排序
        /// </summary>		
        public int gPicOrder { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>		
        public DateTime operatorTime { get; set; }
        /// <summary>
        /// 录入人
        /// </summary>		
        public int operatorId { get; set; }

    }

    /// <summary>
    /// 产品图片基本信息
    /// </summary>
    public partial class t_GoodsPicBasic
    {
        /// <summary>
        /// id
        /// </summary>		
        public int id { get; set; } 
        /// <summary>
        /// 商品ID
        /// </summary>		
        public int gId { get; set; }
        /// <summary>
        /// 商品图片
        /// </summary>		
        public string gPicUrl { get; set; } 
        /// <summary>
        /// 排序
        /// </summary>		
        public int gPicOrder { get; set; } 
    }
}

