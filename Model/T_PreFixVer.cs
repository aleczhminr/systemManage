using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class T_PreFixVer
    {
        /// <summary>
        /// id
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 前缀名称
        /// </summary>
        public string preName { get; set; }

        /// <summary>
        /// 代理商名称
        /// </summary>
        public string agentName { get; set; }

        /// <summary>
        /// 代理商id
        /// </summary>
        public int agentId { get; set; }

        /// <summary>
        /// 后台显示名称
        /// </summary>
        public string displayName { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 后台添加人员id
        /// </summary>
        public int operatorId { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime createDate { get; set; }
    }
}
