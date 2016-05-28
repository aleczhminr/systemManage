using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Enum
{
    /// <summary>
    /// 开通的产品
    /// </summary>
   public static class AppAuEnum
    {
       /// <summary>
       /// 产品说明
        /// <para>
        /// 详细见：http://wiki.yuanbei.biz/doku.php?id项目文档:各类别说明:sys_visitinfo
        /// </para>
       /// </summary>
       public enum AppName : int
       {
           HTML5=1,
           优惠券=2,
           销售挂单=3,
           百姓网=4,
           分店=5,
           多计次卡=6,
           颜色尺码=7,
           维客短信=8,
           工资计算 = 9,
           支付宝 = 10,
           维客短信_会员生日 = 11,
           维客短信_充值提醒 = 13,
           维客短信_店铺经营 = 14,
           维客短信_电子账单 = 15,
           维客短信_会员注册 = 16,
           专家版 = 17,
           明细模板 = 18,
           微信营销 = 19,
           手机串号=21,
           新销售 = 200,
           新积分商城 = 201
       }

    }
}
