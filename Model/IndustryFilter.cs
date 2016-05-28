using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class IndustryFilter
    {
    }

    /// <summary>
    /// 行业清洗结果表
    /// </summary>
    public class IndustryResult
    {
        public int Id { get; set; }
        public int AccId { get; set; }
        public string CompanyName { get; set; }
        public string Eindustry_1 { get; set; }
        public string Eindustry_2 { get; set; }
        public string Sindustry_1 { get; set; }
        public string Sindustry_2 { get; set; }
        public string FormerShopType { get; set; }
        public DateTime LastOperTime { get; set; }
        public int UpdateCount { get; set; }
        public string Remark { get; set; }
    }

    /// <summary>
    /// 行业清洗记录
    /// </summary>
    public class IndustryFilterLog
    {
        public int AccId { get; set; }
        public int Id { get; set; }
        public string FormerIndustry { get; set; }
        public string NowIndustry { get; set; }
        public DateTime UpdateTime { get; set; }
        public string Keyword { get; set; }
    }

    /// <summary>
    /// 行业清洗字典
    /// </summary>
    public class IndustryFilterDic
    {
        public int Id { get; set; }
        public int IndustryId { get; set; }
        public string Industry_1 { get; set; }
        public string Industry_2 { get; set; }
        public string FilterWord { get; set; }
        public string SpecFilter { get; set; }
    }

    public class ShopNamePair
    {
        public int AccId { get; set; }
        public string CompanyName { get; set; }
    }

    public class ShopExtIndustry
    {
        public int AccId { get; set; }
        public string IndustryStr { get; set; }

        /// <summary>
        /// 大行业
        /// </summary>
        public string Industry_1 { get; set; }
        /// <summary>
        /// 小行业
        /// </summary>
        public string Industry_2 { get; set; }
    }

    public class ShopIndustryDic
    {
        public int ParentId { get; set; }
        public string Keyword { get; set; }
    }
}
