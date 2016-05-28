using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //T_Goods_Attribute
    /// <summary>
    /// 自定义属性
    /// </summary>	
    [Serializable]
    public partial class T_Goods_Attribute
    {

        /// <summary>
        /// 自定义属性Id
        /// </summary>		
        public int gaId { get; set; }
        /// <summary>
        /// 属性名称
        /// </summary>		
        public string gaName { get; set; }
        /// <summary>
        /// 属性类型
        /// </summary>		
        public int gaType { get; set; }
        /// <summary>
        /// 属性备注
        /// </summary>		
        public string gaRemark { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>		
        public DateTime gaTime { get; set; }
        /// <summary>
        /// 激活状态
        /// </summary>		
        public int gaAlive { get; set; }
        /// <summary>
        /// 店铺Id
        /// </summary>		
        public int accId { get; set; }

    }
}

