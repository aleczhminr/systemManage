using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class EvaluationModel
    {
        /// <summary>
        /// 评论ID
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 店铺ID
        /// </summary>
        public int accid { get; set; }
        /// <summary>
        /// 店铺名称
        /// </summary>
        public string accountName { get; set; }
        /// <summary>
        /// 评论内容
        /// </summary>
        public string content { get; set; }

        /// <summary>
        /// 新增时间
        /// </summary>
        public DateTime createTime { get; set; }
        /// <summary>
        /// 评分
        /// </summary>
        public string score { get; set; }
        /// <summary>
        /// 商品ID
        /// </summary>
        public int productID { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string productName { get; set; }
        /// <summary>
        /// 商品类别,当前1表示虚拟商品，2表示实物
        /// </summary>
        public int productType { get; set; }

        /// <summary>
        /// 是否显示(0显示，1未处理，2不显示，保存时默认为1)
        /// </summary>
        public int isDisplay { get; set; }
        /// <summary>
        /// 是否被删除(0已删除，1未删除，保存时默认为1)
        /// </summary>
        public int isDelete { get; set; }

        /// <summary>
        /// 操作人IP
        /// </summary>
        public string operatorIP { get; set; }

        /// <summary>
        /// 操作人Id
        /// </summary>
        public int operatorUserId { get; set; }

        /// <summary>
        /// 积分改变数
        /// </summary>
        public string editVale { get; set; }
        /// <summary>
        /// 是否显示状态名字
        /// </summary>
        public string statusName { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
    }
    
}
