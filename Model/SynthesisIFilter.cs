using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{/// <summary>
    /// 综合筛选
    /// </summary>
    public class SynthesisIFilter
    {

        #region 高级检索
        /// <summary>
        /// 短信数小于会员数
        /// </summary>
        public string SmsLesserThanUser { get; set; }
        /// <summary>
        /// 购买产品 
        /// </summary>
        public int[] OrderProject { get; set; }
        /// <summary>
        /// 代理商 
        /// </summary>
        public int[] SearchByAgent { get; set; }
        /// <summary>
        /// 店铺状态
        /// </summary>
        public int[] AccountActive { get; set; }

        #endregion

        #region 标签检索
        /// <summary>
        /// 标签信息
        /// </summary>
        public int[] TagInfo { get; set; }
        #endregion

        #region 基本检索
        /// <summary>
        /// 注册时间
        /// </summary>
        public SynthesisIfilterItemString RegTimeByTime { get; set; }
        /// <summary>
        /// 按订单购买时间
        /// </summary>
        public SynthesisIfilterItemString BuyOrderByTime { get; set; }
        /// <summary>
        /// 会员数量
        /// </summary>
        public SynthesisIfilterItemInt userNumber { get; set; }

        /// <summary>
        /// 销售数量
        /// </summary>
        public SynthesisIfilterItemInt saleNumber { get; set; }

        /// <summary>
        /// 未登录天数
        /// </summary>
        public SynthesisIfilterItemInt LastLoginTime { get; set; }


        /// <summary>
        /// 订单金额
        /// </summary>
        public SynthesisIfilterItemInt orderMoney { get; set; }

        /// <summary>
        /// 发短信数量 
        /// </summary>
        public SynthesisIfilterItemInt smsNumber { get; set; }

        /// <summary>
        /// 商品数量
        /// </summary>
        public SynthesisIfilterItemInt goodsNumber { get; set; }

        /// <summary>
        /// 支出数量
        /// </summary>
        public SynthesisIfilterItemInt outlayNumber { get; set; }

        /// <summary>
        /// 登录次数
        /// </summary>
        public SynthesisIfilterItemInt loginTimes { get; set; }

        /// <summary>
        /// 剩余短信数
        /// </summary>
        public SynthesisIfilterItemInt smsResidue { get; set; }

        /// <summary>
        /// 使用代金券数
        /// </summary>
        public SynthesisIfilterItemInt useVoucher { get; set; }

        /// <summary>
        /// 店铺总积分
        /// </summary>
        public SynthesisIfilterItemInt accAllIntegral { get; set; }
        /// <summary>
        /// 店铺积分
        /// </summary>
        public SynthesisIfilterItemInt accIntegral { get; set; }
        /// <summary>
        /// 店铺已使用积分
        /// </summary>
        public SynthesisIfilterItemInt accUseIntegral { get; set; }
        /// <summary>
        /// 店员数量
        /// </summary>
        public SynthesisIfilterItemInt shopAssistant { get; set; }
        /// <summary>
        /// 分店数
        /// </summary>
        public SynthesisIfilterItemInt subbranch { get; set; }
        /// <summary>
        /// 客服次数
        /// </summary>
        public SynthesisIfilterItemInt serviceNumber { get; set; }
        /// <summary>
        /// 连续登录天数
        /// </summary>
        public SynthesisIfilterItemInt continuousLogin { get; set; }
        /// <summary>
        /// 最后登录时间
        /// </summary>
        public SynthesisIfilterItemString finallyLoginTime { get; set; }

        #endregion


        /// <summary>
        /// 当前首次活跃
        /// </summary>
        public string MonthIndexActive { get; set; }
        /// <summary>
        ///  当前活跃流失
        /// </summary>
        public string MonthActiveDrain { get; set; }
        /// <summary>
        /// 某段时间登录过
        /// </summary>
        public SynthesisIfilterItemString WhileLoggedOn { get; set; }

        /// <summary>
        /// 排除以下店铺
        /// </summary>
        public int[] excludeAccount { get; set; }

        /// <summary>
        /// 确认唯一的店铺
        /// </summary>
        public int[] soleAccount { get; set; }

    }

    /// <summary>
    /// 筛选属性值
    /// </summary>
    public class SynthesisIfilterItemString
    {
        public string max { get; set; }
        public string min { get; set; }
    }

    /// <summary>
    /// 筛选属性值
    /// </summary>
    public class SynthesisIfilterItemInt
    {
        public int? max { get; set; }
        public int? min { get; set; }
    }

    public class SynthesisIfilterItemTime
    {
        public DateTime? max { get; set; }
        public DateTime? min { get; set; }
    }

    /// <summary>
    /// 动态类型的值对/范围
    /// </summary>
    public class DataPair
    {
        public dynamic Max { get; set; }
        public dynamic Min { get; set; }
        public dynamic Range  { get; set; }
    }

    public class ConditionList
    {
        public ConditionList()
        {
            DataList = new List<FilterCondition>();
        }

        public List<FilterCondition> DataList { get; set; }
    }

    public class FilterCondition
    {
        /// <summary>
        /// 用于类型索引的索引器
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        //public dynamic this[string name]
        //{
        //    get
        //    {
        //        switch (name)
        //        {
        //            case "StrPair":
        //                return StrPair;
        //            case "IntPair":
        //                return IntPair;
        //            case "Range":
        //                return Range;
        //            default:
        //                return null;
        //        }

        //    }
        //}

        /// <summary>
        /// 规则Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 字段名称
        /// </summary>
        public string ColName { get; set; }
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }        
        /// <summary>
        /// 字段描述
        /// </summary>
        public string ColDesc { get; set; }
        /// <summary>
        /// 条件类型 
        /// StrPair 字符串对
        /// IntPair Int值对
        /// IntRange Int的范围类型
        /// TimePair 时间类型值对
        /// StrRange string类型的范围
        /// </summary>
        public string ConditionType { get; set; }
        /// <summary>
        /// 条件分组 1-用户属性信息 2-订单信息 3-业务信息 4-登录信息 5-基础数据信息 6-标签信息
        /// </summary>
        public int ConditionGroup { get; set; }

        #region 条件范围组

        public DataPair DataRange { get; set; }
        
        #endregion
        /// <summary>
        /// 用于描述条件的备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 时间类型专用
        /// </summary>
        public int DiffVal { get; set; }
    }

    /// <summary>
    /// 规则详情表对应Model
    /// </summary>
    public class FilterRule
    {
        public int Id { get; set; }
        /// <summary>
        /// 对应筛选器的验证值
        /// </summary>
        public string VerifId { get; set; }
        /// <summary>
        /// 规则类型
        /// 1-筛选器
        /// 2-手动添加
        /// </summary>
        public int Type { get; set; }
        public int RuleID { get; set; }
        public string MaxValue { get; set; }
        public string MinValue { get; set; }
        public dynamic RangeData { get; set; }
    }

    public class RangePair
    {
        public RangePair()
        {

        }

        public RangePair(int eId,string eName)
        {
            id = eId;
            t_Name = eName;
            extInfo = "";
        }

        public int id { get; set; }
        public string t_Name { get; set; }
        /// <summary>
        /// Item相关的附属信息
        /// </summary>
        public string extInfo { get; set; }
    }
}
