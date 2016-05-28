using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //t_PayRecord
    /// <summary>
    /// t_PayRecord
    /// </summary>	
    [Serializable]
    public partial class t_PayRecord
    {

        /// <summary>
        /// ID
        /// </summary>		
        public long ID { get; set; }
        /// <summary>
        /// 支付时间    
        /// </summary>		
        public DateTime PayDate { get; set; }
        /// <summary>
        /// 支付金额
        /// </summary>		
        public decimal PaySum { get; set; }
        /// <summary>
        /// 支出大分类
        /// </summary>		
        public string PayMaxType { get; set; }
        /// <summary>
        /// 支出名称
        /// </summary>		
        public string PayName { get; set; }
        /// <summary>
        /// 支出小分类
        /// </summary>		
        public string PayMinType { get; set; }
        /// <summary>
        /// RandomNumber
        /// </summary>		
        public string RandomNumber { get; set; }
        /// <summary>
        /// 店铺ID
        /// </summary>		
        public long ShopperId { get; set; }
        /// <summary>
        /// 是否货款
        /// </summary>		
        public int IfLoan { get; set; }

    }
    /// <summary>
    /// 支出信息
    /// </summary>
    public partial class t_PayRecordBasic
    {
        /// <summary>
        /// ID
        /// </summary>		
        public long ID { get; set; }
        /// <summary>
        /// 支出大分类
        /// </summary>		
        public string PayMaxType { get; set; }
        /// <summary>
        /// 支出小分类
        /// </summary>		
        public string PayMinType { get; set; } 
        /// <summary>
        /// 支出名称
        /// </summary>		
        public string PayName { get; set; }
        /// <summary>
        /// 支付金额
        /// </summary>		
        public decimal PaySum { get; set; }
        /// <summary>
        /// 支付时间    
        /// </summary>		
        public DateTime PayDate { get; set; }
    }
}

