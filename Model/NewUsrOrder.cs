using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class NewUsrOrder
    {
        public NewUsrOrder()
        {
            DataList = new List<NewUsrOrderItem>();
        }

        public List<NewUsrOrderItem> DataList { get; set; }
    }

    public class NewUsrOrderItem
    {
        public NewUsrOrderItem()
        {
            Ratio = "无法计算";
        }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 用户数
        /// </summary>
        public int UsrCount { get; set; }
        /// <summary>
        /// 用户列表
        /// </summary>
        public string AccidList { get; set; }
        /// <summary>
        /// 占比
        /// </summary>
        public string Ratio { get; set; }
    }

    public class NewOrderProduct
    {
        public int accId { get; set; }
        public string displayName { get; set; }
    }
}
