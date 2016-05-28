using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class AlphaApplyModel
    {
        public int id { get; set; }
        /// <summary>
        /// 参与者店铺编号
        /// </summary>
        public int userAccId { get; set; }
        /// <summary>
        /// 参与者行业
        /// </summary>
        public string userJob { get; set; }
        /// <summary>
        /// 参与者电话号码
        /// </summary>
        public string userPhone { get; set; }
        /// <summary>
        /// 参与者店铺名称
        /// </summary>
        public string userAccName { get; set; }
        /// <summary>
        /// 参与者使用方式(手机端客户端网页端)
        /// </summary>
        public string userAlphaClient { get; set; }
        /// <summary>
        /// 新增时间
        /// </summary>
        public DateTime createTime { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 状态枚举
        /// </summary>
        public string statusName { get; set; }
        /// <summary>
        /// 内测版本
        /// </summary>
        public string alphaVersion { get; set; }
    }
}
