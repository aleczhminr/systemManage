using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class FeedbackList
    {
        public FeedbackList()
        {
            dataList = new List<FeedbackModel>();
        }

        public List<FeedbackModel> dataList { get; set; }
    }

    public class FeedbackModel
    {
        public int id { get; set; }
        /// <summary>
        /// 店铺ID
        /// </summary>
        public int accountid { get; set; }
        /// <summary>
        /// 店铺名称
        /// </summary>
        public string companyName { get; set; }

        #region 用户信息补全
        /// <summary>
        /// 用户版本
        /// </summary>
        public string Edit { get; set; }
        /// <summary>
        /// 用户真实姓名
        /// </summary>
        public string UserRealName { get; set; }
        /// <summary>
        /// 上次登录时间
        /// </summary>
        public string LoginLast { get; set; }
        #endregion

        /// <summary>
        /// 反馈内容
        /// </summary>
        public string t_mk { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime inertTime { get; set; }
        /// <summary>
        /// 备注信息
        /// </summary>
        public string dt_remark { get; set; }
        /// <summary>
        /// 登录账号ID
        /// </summary>
        public int dt_logUid { get; set; }
        /// <summary>
        /// 来源
        /// </summary>
        public string dt_Source { get; set; }
        /// <summary>
        /// 回访ID
        /// </summary>
        public int vi_id { get; set; }

        public string forumUrl { get; set; }

        /// <summary>
        /// 关联的需求Id
        /// </summary>
        public int RequirementId { get; set; }

        #region 需求扩展属性
        /// <summary>
        /// 需求任务的状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 需求描述
        /// </summary>
        public string Description { get; set; }

        #endregion
    }
}
