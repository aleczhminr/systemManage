using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Enum
{
    public static class AlipayInfoEnum
    {
        public enum AlipayInfoStatusEnum : int
        {
            重新上传文字信息编辑 = -10,
            重新上传图片信息编辑 = -1,
            重新确认条码收单订单 = -3,
            重新用户输入Key和Pid码 = -6,
            上传文字信息编辑 = 0,
            上传图片信息编辑 = 1,
            等待审核基础信息 = 2,
            确认条码收单订单 = 3,
            等待审核点击确认 = 4,
            等待支付宝审核 = 5,
            用户输入Key和Pid码 = 6,
            等待客服审核KeyPid = 7,
            开通成功 = 8
        }
        public enum CompanyTypeEnum : int
        {
            非法类别 = 0,
            公司 = 1,
            个人 = 2
        }
    }
}
