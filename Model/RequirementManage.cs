using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 需求列表Model
    /// </summary>
    public class RequirementManage
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 所属问题分类Id
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        /// 反馈分类
        /// </summary>        
        public int RequirementType { get; set; }
        /// <summary>
        /// 问题描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 问题状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTime HandleTime { get; set; }
        /// <summary>
        /// 处理者
        /// </summary>
        public string Handler { get; set; }
        /// <summary>
        /// 操作者
        /// </summary>
        public int Operator { get; set; }
        /// <summary>
        /// 问题相关链接
        /// </summary>
        public string LinkRef { get; set; }
        /// <summary>
        /// 问题反馈描述
        /// </summary>
        public string FeedbackDesc { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 来源店铺Id
        /// </summary>
        public string AccId { get; set; }
        /// <summary>
        /// 关联反馈Id
        /// </summary>
        public int RefId { get; set; }
        /// <summary>
        /// 反馈描述
        /// </summary>
        public string OriginDesc { get; set; }

        public string dt_Remark { get; set; }
        public string dt_Source { get; set; }
        public int Terminal { get; set; }
        public int UserVal { get; set; }
        public int Difficult { get; set; }

        #region ext info
        /// <summary>
        /// 操作者姓名
        /// </summary>
        public string OpName { get; set; }
        /// <summary>
        /// 类别名称
        /// </summary>
        public string CateName { get; set; }

        #endregion
    }

    /// <summary>
    /// 需求分类Model
    /// </summary>
    public class RequirementCategory
    {
        public int Id { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        public string CategoryName { get; set; }
        /// <summary>
        /// 父级分类Id
        /// </summary>
        public int ParentCategoryId { get; set; }
        /// <summary>
        /// 有效状态
        /// </summary>
        public int ActiveStatus { get;set;}
    }
}
