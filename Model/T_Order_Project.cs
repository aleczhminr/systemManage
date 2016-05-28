using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //T_Order_Project
    /// <summary>
    /// T_Order_Project
    /// </summary>	
    [Serializable]
    public partial class T_Order_Project
    {

        /// <summary>
        /// 业务ID
        /// </summary>		
        public int busId { get; set; }
        /// <summary>
        /// 所属分类
        /// </summary>		
        public int projectType { get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>		
        public string displayName { get; set; }
        /// <summary>
        /// 附加标题
        /// </summary>		
        public string additionDesc { get; set; }
        /// <summary>
        /// 系统备注
        /// </summary>		
        public string remarkName { get; set; }
        /// <summary>
        /// 应付金额
        /// </summary>		
        public decimal payAbleMoney { get; set; }
        /// <summary>
        /// 默认价格
        /// </summary>		
        public decimal normalMoney { get; set; }
        /// <summary>
        /// 标准版价格
        /// </summary>		
        public decimal standardMoney { get; set; }
        /// <summary>
        /// 高级版价格
        /// </summary>		
        public decimal expertMoney { get; set; }
        /// <summary>
        /// 普通折扣金额
        /// </summary>		
        public decimal normalDiscount { get; set; }
        /// <summary>
        /// 标准版折扣金额
        /// </summary>		
        public decimal standardDiscount { get; set; }
        /// <summary>
        /// 高级版折扣金额
        /// </summary>		
        public decimal expertDiscount { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>		
        public DateTime createDate { get; set; }
        /// <summary>
        /// startDate
        /// </summary>		
        public DateTime startDate { get; set; }
        /// <summary>
        /// 截至日期
        /// </summary>		
        public DateTime closingDate { get; set; }
        /// <summary>
        /// 增加积分
        /// </summary>		
        public int additionIntegral { get; set; }
        /// <summary>
        /// 操作人员id
        /// </summary>		
        public int operatorId { get; set; }
        /// <summary>
        /// 操作人员ip
        /// </summary>		
        public string operatorIp { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>		
        public DateTime operatorTime { get; set; }
        /// <summary>
        /// hiddenVal
        /// </summary>		
        public int hiddenVal { get; set; }
        /// <summary>
        /// prefixAgent
        /// </summary>		
        public int prefixAgent { get; set; }
        /// <summary>
        /// allowBuy
        /// </summary>		
        public int allowBuy { get; set; }

    }
}

