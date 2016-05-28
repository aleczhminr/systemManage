using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace Model
{
    public class MonthlyKPI
    {
        /// <summary>
        /// 条目ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 阶段时间
        /// </summary>
        [Display(Name = "时间阶段")]
        [DisplayFormat(DataFormatString = "{0:yyyy年MM月}")]
        public DateTime MDate { get; set; }

        /// <summary>
        /// 注册量
        /// </summary>
        [Display(Name = "注册量")]
        public int RegNum { get; set; }

        /// <summary>
        /// 销售笔数
        /// </summary>
        [Display(Name = "销售笔数")]
        public int SellCount { get; set; }

        /// <summary>
        /// 新增会员
        /// </summary>
        [Display(Name = "新增会员")]
        public int UsrAdd { get; set; }

        /// <summary>
        /// 新增商品
        /// </summary>
        [Display(Name = "新增商品")]
        public int Sku { get; set; }

        /// <summary>
        /// 短信量
        /// </summary>
        [Display(Name = "短信量")]
        public int Sms { get; set; }

        /// <summary>
        /// 订单量
        /// </summary>
        [Display(Name = "订单量")]
        public int OrderCount { get; set; }
        /// <summary>
        /// 订单金额
        /// </summary>
        [Display(Name = "订单金额")]
        public int OrderMoney { get; set; }
    }

    public class MonthlyKPIContext : DbContext
    {
        public DbSet<MonthlyKPI> MonthlyKPIList { get; set; }
    }
}
