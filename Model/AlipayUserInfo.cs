using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class AlipayUserInfo
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public int AccId { get; set; }
        /// <summary>
        /// 支付宝账号
        /// </summary>
        public string AliAccount { get; set; }
        /// <summary>
        /// 支付宝Pid
        /// </summary>
        public string AliPid { get; set; }
        /// <summary>
        /// 支付宝Key
        /// </summary>
        public string AliKey { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        public string PhoneNum { get; set; }
        /// <summary>
        /// 店主姓名
        /// </summary>
        public string AccName { get; set; }
    }
}
