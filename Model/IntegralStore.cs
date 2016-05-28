using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 积分商城
    /// </summary>
    public class IntegralStore
    {
        /// <summary>
        /// 商城产品
        /// </summary>
        public class StoreProject
        {
            public StoreProject()
            {

            }

            /// <summary>
            /// 主键值
            /// </summary>
            public int Key { get; set; }
            /// <summary>
            /// 产品名称
            /// </summary>
            public string ProjectName { get; set; }
            /// <summary>
            /// 需要积分
            /// </summary>
            public int Integral { get; set; }
            /// <summary>
            /// 数量
            /// </summary>
            public int Quantity { get; set; }
            /// <summary>
            /// 业务ID
            /// </summary>
            public int BusId { get; set; }
            /// <summary>
            /// 是否为实物  {0：虚拟产品，1：实物}
            /// </summary>
            public int isEntity { get; set; }
            /// <summary>
            /// 限制类别 { 1:按月兑换}
            /// </summary>
            public int LimitType { get; set; }
            /// <summary>
            /// 限制值
            /// </summary>
            public int LimitNumber { get; set; }



            public StoreProject GetModel(int type)
            {
                StoreProject model = new StoreProject();
                switch (type)
                {

                    case 1:
                        model.Key = 1;
                        model.ProjectName = "高级版一个月";
                        model.Integral = 1000;
                        model.Quantity = 1;
                        model.BusId = 5;
                        model.isEntity = 0;
                        model.LimitType = 0;
                        model.LimitNumber = 0;
                        break;

                    case 2:
                        model.Key = 2;
                        model.ProjectName = "高级版三个月";
                        model.Integral = 2500;
                        model.Quantity = 3;
                        model.BusId = 5;
                        model.isEntity = 0;
                        model.LimitType = 0;
                        model.LimitNumber = 0;
                        break;

                    case 3:
                        model.Key = 3;
                        model.ProjectName = "维客套餐";
                        model.Integral = 4000;
                        model.Quantity = 1;
                        model.BusId = 52;
                        model.isEntity = 0;
                        model.LimitType = 0;
                        model.LimitNumber = 0;
                        break;

                    case 4:
                        model.Key = 4;
                        model.ProjectName = "扫描枪";
                        model.Integral = 8000;
                        model.Quantity = 1;
                        model.BusId = 0;
                        model.isEntity = 1;
                        model.LimitType = 0;
                        model.LimitNumber = 0;
                        break;

                    case 5:
                        model.Key = 5;
                        model.ProjectName = "暂结账功能一个月";
                        model.Integral = 1000;
                        model.Quantity = 1;
                        model.BusId = 29;
                        model.isEntity = 0;
                        model.LimitType = 0;
                        model.LimitNumber = 0;
                        break;

                    case 6:
                        model.Key = 6;
                        model.ProjectName = "暂结账功能三个月";
                        model.Integral = 2500;
                        model.Quantity = 5;
                        model.BusId = 29;
                        model.isEntity = 0;
                        model.LimitType = 0;
                        model.LimitNumber = 0;
                        break;

                    case 7:
                        model.Key = 7;
                        model.ProjectName = "新能量套餐";
                        model.Integral = 5000;
                        model.Quantity = 1;
                        model.BusId = 34;
                        model.isEntity = 0;
                        model.LimitType = 0;
                        model.LimitNumber = 0;
                        break;

                    case 8:
                        model.Key = 8;
                        model.ProjectName = "58H 热敏打印机";
                        model.Integral = 6000;
                        model.Quantity = 1;
                        model.BusId = 0;
                        model.isEntity = 1;
                        model.LimitType = 0;
                        model.LimitNumber = 0;
                        break;

                    case 9:
                        model.Key = 9;
                        model.ProjectName = "分店管理功能一个月";
                        model.Integral = 1000;
                        model.Quantity = 1;
                        model.BusId = 30;
                        model.isEntity = 0;
                        model.LimitType = 0;
                        model.LimitNumber = 0;
                        break;

                    case 10:
                        model.Key = 10;
                        model.ProjectName = "分店管理功能三个月";
                        model.Integral = 2500;
                        model.Quantity = 3;
                        model.BusId = 30;
                        model.isEntity = 0;
                        model.LimitType = 0;
                        model.LimitNumber = 0;
                        break;

                    case 11:
                        model.Key = 11;
                        model.ProjectName = "聚给力套餐";
                        model.Integral = 12000;
                        model.Quantity = 1;
                        model.BusId = 13;
                        model.isEntity = 0;
                        model.LimitType = 0;
                        model.LimitNumber = 0;
                        break;

                    case 12:
                        model.Key = 12;
                        model.ProjectName = "58mm热敏收银纸";
                        model.Integral = 3000;
                        model.Quantity = 1;
                        model.BusId = 0;
                        model.isEntity = 1;
                        model.LimitType = 0;
                        model.LimitNumber = 0;
                        break;

                    case 13:
                        model.Key = 13;
                        model.ProjectName = "短信200条";
                        model.Integral = 1000;
                        model.Quantity = 2;
                        model.BusId = 1;
                        model.isEntity = 0;
                        model.LimitType = 1;
                        model.LimitNumber = 1;
                        break;

                    case 14:
                        model.Key = 14;
                        model.ProjectName = "短信600条";
                        model.Integral = 2500;
                        model.Quantity = 6;
                        model.BusId = 1;
                        model.isEntity = 0;
                        model.LimitType = 1;
                        model.LimitNumber = 1;
                        break;

                    case 15:
                        model.Key = 15;
                        model.ProjectName = "生意专家T恤";
                        model.Integral = 1000;
                        model.Quantity = 1;
                        model.BusId = 0;
                        model.isEntity = 1;
                        model.LimitType = 0;
                        model.LimitNumber = 0;
                        break;

                    case 16:
                        model.Key = 16;
                        model.ProjectName = "热敏条码打印机";
                        model.Integral = 20000;
                        model.Quantity = 1;
                        model.BusId = 0;
                        model.isEntity = 1;
                        model.LimitType = 0;
                        model.LimitNumber = 0;
                        break;

                    case 17:
                        model.Key = 17;
                        model.ProjectName = "手机橱窗一个月";
                        model.Integral = 500;
                        model.Quantity = 1;
                        model.BusId = 6;
                        model.isEntity = 0;
                        model.LimitType = 0;
                        model.LimitNumber = 0;
                        break;

                    case 18:
                        model.Key = 18;
                        model.ProjectName = "手机橱窗三个月";
                        model.Integral = 1200;
                        model.Quantity = 3;
                        model.BusId = 6;
                        model.isEntity = 0;
                        model.LimitType = 0;
                        model.LimitNumber = 0;
                        break;

                    case 19:
                        model.Key = 19;
                        model.ProjectName = "USB 3.0 8G U盘";
                        model.Integral = 1500;
                        model.Quantity = 1;
                        model.BusId = 0;
                        model.isEntity = 1;
                        model.LimitType = 0;
                        model.LimitNumber = 0;
                        break;

                    case 20:
                        model.Key = 20;
                        model.ProjectName = "保险箱";
                        model.Integral = 15000;
                        model.Quantity = 1;
                        model.BusId = 0;
                        model.isEntity = 1;
                        model.LimitType = 0;
                        model.LimitNumber = 0;
                        break;

                    case 21:
                        model.Key = 21;
                        model.ProjectName = "优惠券一个月";
                        model.Integral = 300;
                        model.Quantity = 1;
                        model.BusId = 7;
                        model.isEntity = 0;
                        model.LimitType = 0;
                        model.LimitNumber = 0;
                        break;

                    case 22:
                        model.Key = 22;
                        model.ProjectName = "优惠券三个月";
                        model.Integral = 800;
                        model.Quantity = 3;
                        model.BusId = 7;
                        model.isEntity = 0;
                        model.LimitType = 0;
                        model.LimitNumber = 0;
                        break;

                    case 23:
                        model.Key = 23;
                        model.ProjectName = "点钞机";
                        model.Integral = 20000;
                        model.Quantity = 1;
                        model.BusId = 0;
                        model.isEntity = 1;
                        model.LimitType = 0;
                        model.LimitNumber = 0;
                        break;

                    case 24:
                        model.Key = 24;
                        model.ProjectName = "拉卡拉POS";
                        model.Integral = 25000;
                        model.Quantity = 1;
                        model.BusId = 0;
                        model.isEntity = 1;
                        model.LimitType = 0;
                        model.LimitNumber = 0;
                        break;
                    case 25:
                        model.Key = 25;
                        model.ProjectName = "维客短信_生日提醒";
                        model.Integral = 300;
                        model.Quantity = 1;
                        model.BusId = 46;
                        model.isEntity = 0;
                        model.LimitType = 0;
                        model.LimitNumber = 0;
                        break;
                    case 26:
                        model.Key = 26;
                        model.ProjectName = "维客短信_生日提醒";
                        model.Integral = 800;
                        model.Quantity = 3;
                        model.BusId = 46;
                        model.isEntity = 0;
                        model.LimitType = 0;
                        model.LimitNumber = 0;
                        break;
                    case 27:
                        model.Key = 27;
                        model.ProjectName = "维客短信_新增会员";
                        model.Integral = 300;
                        model.Quantity = 1;
                        model.BusId = 51;
                        model.isEntity = 0;
                        model.LimitType = 0;
                        model.LimitNumber = 0;
                        break;
                    case 28:
                        model.Key = 28;
                        model.ProjectName = "维客短信_新增会员";
                        model.Integral = 800;
                        model.Quantity = 3;
                        model.BusId = 51;
                        model.isEntity = 0;
                        model.LimitType = 0;
                        model.LimitNumber = 0;
                        break;
                    case 29:
                        model.Key = 29;
                        model.ProjectName = "维客短信_储值提醒";
                        model.Integral = 300;
                        model.Quantity = 1;
                        model.BusId = 48;
                        model.isEntity = 0;
                        model.LimitType = 0;
                        model.LimitNumber = 0;
                        break;
                    case 30:
                        model.Key = 30;
                        model.ProjectName = "维客短信_储值提醒";
                        model.Integral = 800;
                        model.Quantity = 3;
                        model.BusId = 48;
                        model.isEntity = 0;
                        model.LimitType = 0;
                        model.LimitNumber = 0;
                        break;
                    case 31:
                        model.Key = 31;
                        model.ProjectName = "维客短信_消费提醒";
                        model.Integral = 300;
                        model.Quantity = 1;
                        model.BusId = 50;
                        model.isEntity = 0;
                        model.LimitType = 0;
                        model.LimitNumber = 0;
                        break;
                    case 32:
                        model.Key = 32;
                        model.ProjectName = "维客短信_消费提醒";
                        model.Integral = 800;
                        model.Quantity = 3;
                        model.BusId = 50;
                        model.isEntity = 0;
                        model.LimitType = 0;
                        model.LimitNumber = 0;
                        break;
                    case 33:
                        model.Key = 33;
                        model.ProjectName = "计算器";
                        model.Integral = 2000;
                        model.Quantity = 1;
                        model.BusId = 0;
                        model.isEntity = 1;
                        model.LimitType = 0;
                        model.LimitNumber = 0;
                        break;
                    case 34:
                        model.Key = 34;
                        model.ProjectName = "小米插线板";
                        model.Integral = 3500;
                        model.Quantity = 1;
                        model.BusId = 0;
                        model.isEntity = 1;
                        model.LimitType = 0;
                        model.LimitNumber = 0;
                        break;
                    case 35:
                        model.Key = 35;
                        model.ProjectName = "小米移动电源";
                        model.Integral = 3500;
                        model.Quantity = 1;
                        model.BusId = 0;
                        model.isEntity = 1;
                        model.LimitType = 0;
                        model.LimitNumber = 0;
                        break;
                    default:
                        return null;
                }
                return model;
            }
        }

        /// <summary>
        /// 发送信息
        /// </summary>
        public class PostQuery
        {
            /// <summary>
            /// 店铺ID
            /// </summary>
            public int accid { get; set; }
            /// <summary>
            /// 产品ID
            /// </summary>
            public int projectId { get; set; }
            /// <summary>
            /// 收件人
            /// </summary>
            public string recipients { get; set; }
            /// <summary>
            /// 收件省
            /// </summary>
            public string province { get; set; }
            /// <summary>
            /// 收件街道信息
            /// </summary>
            public string street { get; set; }
            /// <summary>
            /// 邮编
            /// </summary>
            public string postcode { get; set; }
            /// <summary>
            /// 收件人电话
            /// </summary>
            public string phoneNumber { get; set; }
            /// <summary>
            /// 操作人
            /// </summary>
            public string operatorName { get; set; }
            /// <summary>
            /// IP地址
            /// </summary>
            public string ip { get; set; }
        }

    }
}
