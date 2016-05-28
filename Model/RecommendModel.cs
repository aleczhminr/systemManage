using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class RecommendModel
    {
        public RecommendModel()
        {
            RowCount = 0;
            maxPage = 0;
            Data = new List<RecommendItem>();
        }

        public int RowCount { get; set; }
        public int maxPage { get; set; }
        public string PageHtml { get; set; }
        public List<RecommendItem> Data { get; set; }
    }

    /// <summary>
    /// 推荐内容
    /// </summary>
    public class RecommendItem
    {
        public int Id { get; set; }

        /// <summary>
        /// 推荐日期
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 店铺Id
        /// </summary>
        public int AccId { get; set; }

        /// <summary>
        /// 推荐类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 推荐活动
        /// </summary>
        public string RecommendName { get; set; }

        /// <summary>
        /// 推荐填写内容
        /// </summary>
        public string RecommendValue { get; set; }

        /// <summary>
        /// 推荐店铺Id
        /// </summary>
        public int RecommendAccId { get; set; }

        /// <summary>
        /// 店铺名称
        /// </summary>
        public string AccName { get; set; }

        /// <summary>
        /// 推荐店铺名称
        /// </summary>
        public string ReAccName { get; set; }

        /// <summary>
        /// 推荐状态
        /// </summary>
        public int ReStatus { get; set; }

        /// <summary>
        /// 推荐次数
        /// </summary>
        public int ReTimes { get; set; }
    }


    public class RecommandList
    {
        /// <summary>
        /// 总行数
        /// </summary>
        public int Rows { get; set; }

        /// <summary>
        /// 当前页数
        /// </summary>
        public int NowPage { get; set; }

        /// <summary>
        /// 成功邀请总数
        /// </summary>
        public int SuccessNum { get; set; }

        /// <summary>
        /// 推荐总数
        /// </summary>
        public int RecommandNum { get; set; }

        /// <summary>
        /// 分页Html
        /// </summary>
        public string PageHtml { get; set; }

        /// <summary>
        /// 邀请列表详情
        /// </summary>
        public List<RecommandItem> Data { get; set; }
    }

    public class RecommandItem
    {
        /// <summary>
        /// 记录Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 店铺Id
        /// </summary>
        public int AccId { get; set; }

        /// <summary>
        /// 店铺名称
        /// </summary>
        public string AccName { get; set; }

        /// <summary>
        /// 被邀请店铺Id
        /// </summary>
        public int ReAccId { get; set; }

        /// <summary>
        /// 被邀请店铺名称
        /// </summary>
        public string ReAccName { get; set; }

        /// <summary>
        /// 被邀请店铺账号
        /// </summary>
        public string ReAccount { get; set; }

        /// <summary>
        /// 邀请日期
        /// </summary>
        public DateTime LogDate { get; set; }

        /// <summary>
        /// 成功邀请日期
        /// </summary>
        public DateTime ReDate { get; set; }

        /// <summary>
        /// 邀请状态
        /// </summary>
        public int ReStatus { get; set; }

        /// <summary>
        /// 邀请状态描述
        /// </summary>
        public string ReStatusName { get; set; }
    }
}
