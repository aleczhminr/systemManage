using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class PayAfterServDataModel
    {
        public PayAfterServDataModel()
        {
            spList = new List<ServicePayPair>();
            detailList = new List<ServiceDetail>();
        }

        public List<ServicePayPair> spList { get; set; }
        public List<ServiceDetail> detailList { get; set; }
    }

    public class ServicePayPair
    {
        /// <summary>
        /// 客服人员
        /// </summary>
        public string person { get; set; }

        /// <summary>
        /// 成交金额
        /// </summary>
        public string count { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int serviceCounut { get; set; }

        public string orderCnt { get; set; }
        public int serviceAllCounut { get; set; }
    }

    public class ServiceDetail
    {
        /// <summary>
        /// 客服人员
        /// </summary>
        public string person { get; set; }

        /// <summary>
        /// 店铺名称
        /// </summary>
        public string comName { get; set; }

        /// <summary>
        /// 销售产品名称
        /// </summary>
        public string saleName { get; set; }

        /// <summary>
        /// 交易时间
        /// </summary>
        public string saleTime { get; set; }

        /// <summary>
        /// 交易金额
        /// </summary>
        public string buyMoney { get; set; }
    }
}
