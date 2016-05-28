using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using Model;
using Utility;

namespace Controls.MessageTemplate
{
    public static class MessageTemplateControl
    {
        public static string AddTemplate(TriggerTemplateModel model)
        {
            return MessageTemplateBLL.AddTemplate(model);
        }

        /// <summary>
        /// 从模板生成发送批次并自动发送
        /// </summary>
        /// <param name="id"></param>
        /// <param name="eventName"></param>
        /// <param name="specModel"></param>
        /// <param name="accid"></param>
        /// <param name="uid"></param>
        /// <param name="uName"></param>
        /// <returns></returns>
        public static int SendNotifyMsg(int id, string eventName, string specModel, int accid, int uid, string uName)
        {
            //获取模板
            TriggerTemplateModel tModel = MessageTemplateBLL.GetModelById(id);

            try
            {
                //拆解模板并生成批次
                string batchId = GenerateBatchByTemplate(tModel, accid, eventName, specModel,id);
                MessageSysControl.SendMsgAuto(batchId, uid, uName);
                return 1;
            }
            catch (Exception ex)
            {
                Logger.Error("获取前台消息发送信息出错！", ex);
                return 0;
            }


        }

        /// <summary>
        /// 根据模板生成批次信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="accid"></param>
        /// <param name="eventName"></param>
        /// <param name="specModel"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public static string GenerateBatchByTemplate(TriggerTemplateModel model, int accid, string eventName, string specModel,int eventId)
        {
            List<string> list = new List<string>();
            list.Add(accid.ToString());
            string accidSet = CommonLib.Helper.JsonSerializeObject(list);

            //订单推送全局模板
            string orderWebPushingContent =
                "<p>尊敬的【店铺名称】：<br><br>您已成功购买【产品名称】，感谢您的支持。我们为您准备了【产品名称】速用指南，帮您快速上手。<br><br><br></p> " +
                "<p style='text-align: center'> <a style='font-size: 16px;' href='【帮助链接】' target='_blank' title='' class='btn btn-primary'>【产品名称】 速用指南</a> </p> " +
                "<br><br>客服电话：4006006815<br>客服QQ：4006006815<br><p></p>";

            string emailTemplate =
                "<div style='min-height:30px; padding-top:35px; padding-bottom:35px; text-align:center;width:618px; margin:0 auto 0;'> " +
                "<div style='font-size:24px; font-weight:bold;color:#437C08;'>" +
                "<span> √ </span>购买成功</div> " +
                "<div style='text-align:left;font-size:16px; color:#333;margin-top: 35px;'>亲爱的，<span style='color:#57A630; font-weight:bold;'>【店主姓名】（<a href='http://app.i200.cn/LoginReg/Login.aspx?username={{注册账号}}' style='text-decoration:none;color:#57A630;' target='_blank'>【店铺名称】</a>）：" +
                "</span></div> <div style='text-align:left; color:#696969; font-size:16px;margin-top: 35px; line-height:26px;'>" +
                "您的订单<span style='color:#57a630;'>【订单编号】</span>已经成功支付，【附加说明】生意专家感谢您的支持！</div> " +
                "<div style='text-align:left;color:#696969; font-size:16px;margin-top: 35px;'>您已经购买了：" +
                "</div> <table border='0' cellpadding='0' cellspacing='0' style='width:100%; font-size:12px;border-top:1px solid #DBDCDC;border-right:1px solid #DBDCDC;'> " +
                "<tr> <th style='border-left:1px solid #DBDCDC;border-bottom:1px solid #DBDCDC; text-align:center;width:99px; height:30px;'>购买项目</th> " +
                "<th style='border-left:1px solid #DBDCDC;border-bottom:1px solid #DBDCDC; text-align:center;width:99px;'>订单金额</th> " +
                "<th style='border-left:1px solid #DBDCDC;border-bottom:1px solid #DBDCDC; text-align:center;width:99px;'>规格</th>" +
                " <th style='border-left:1px solid #DBDCDC;border-bottom:1px solid #DBDCDC; text-align:center;width:320px;'>功能简介</th> </tr> " +
                "<tr> <td style='border-left:1px solid #DBDCDC;border-bottom:1px solid #DBDCDC; text-align:center;'>【产品名称】</td> " +
                "<td style='border-left:1px solid #DBDCDC;border-bottom:1px solid #DBDCDC; text-align:center; color:#57A630;'>￥【产品价格】</td> " +
                "<td style='border-left:1px solid #DBDCDC;border-bottom:1px solid #DBDCDC; text-align:center;'>【产品数量】</td> " +
                "<td style='border-left:1px solid #DBDCDC;border-bottom:1px solid #DBDCDC; text-align:center; padding:12px; text-align:left;'>【产品描述】</td> " +
                "</tr> </table> <div style='text-align:left;color:#696969; font-size:16px;margin-top: 35px;'>更多实惠，值得拥有：</div> " +
                "<div style='margin-top:22px; color:#888;'> " +
                "<table border='0' cellpadding='0' cellspacing='0' style='width:100%; font-size:12px;border-top:1px solid #DBDCDC;border-right:1px solid #DBDCDC;'> <tr> " +
                "<th style='border-left:1px solid #DBDCDC;border-bottom:1px solid #DBDCDC; text-align:center;width:99px; height:30px;'>推荐项目</th> " +
                "<th style='border-left:1px solid #DBDCDC;border-bottom:1px solid #DBDCDC; text-align:center;width:140px;'>订单金额</th> " +
                "<th style='border-left:1px solid #DBDCDC;border-bottom:1px solid #DBDCDC; text-align:center;width:379px;'>功能简介</th> </tr> " +
                "【推荐项目】" +
                "</table> </div> <div style='font-size:12px; text-align:left; margin-top:20px;color:#888;'>" +
                "返回<span style=' color:#57A630; font-size:12px;'>生意专家</span>，点击页面上方的“安全退出”，重新登录，您购买的项目即可生效。</div> </div>";

            string recommendSms =
                " <tr> <td style='border-left:1px solid #DBDCDC;border-bottom:1px solid #DBDCDC; text-align:center;'>短信包</td> " +
                "<td style='border-left:1px solid #DBDCDC;border-bottom:1px solid #DBDCDC; text-align:center; color:#57A630;'>低至4分/条</td> " +
                "<td style='border-left:1px solid #DBDCDC;border-bottom:1px solid #DBDCDC; text-align:center; padding:12px; text-align:left;'>" +
                "做活动、送祝福，发账单都可以用，1069通道发送，98%到达率，每条低至4分钱。 </td> </tr>";

            string recommendMobile =
                "<tr> <td style='border-left:1px solid #DBDCDC;border-bottom:1px solid #DBDCDC; text-align:center;'>100元话费直充</td> " +
                "<td style='border-left:1px solid #DBDCDC;border-bottom:1px solid #DBDCDC; text-align:center; color:#57A630;'>￥96.8（原价￥100）</td> " +
                "<td style='border-left:1px solid #DBDCDC;border-bottom:1px solid #DBDCDC; text-align:center; padding:12px; text-align:left;'>" +
                "哪充都一样，选最便宜的！守店，顺便开展话费充值业务，动动手指，月入上百！</td> </tr>";

            string recommendAdvance =
                "<tr> <td style='border-left:1px solid #DBDCDC;border-bottom:1px solid #DBDCDC; text-align:center;'>高级版</td> " +
                "<td style='border-left:1px solid #DBDCDC;border-bottom:1px solid #DBDCDC; text-align:center; color:#57A630;'>￥299/年</td> " +
                "<td style='border-left:1px solid #DBDCDC;border-bottom:1px solid #DBDCDC; text-align:center; padding:12px; text-align:left;'>" +
                "多达20名店员的权限、业绩管理让您的生意井井有条，会员储值、计次、优惠券功能把您拓客锁客。</td> </tr>";

            #region 用Model填充模板内容

            switch (eventId)
            {
                case 1:
                    //AfterFirstLogin fModel = CommonLib.Helper.JsonDeserializeObject<AfterFirstLogin>(specModel);
                    //model.WebContent=model.WebContent.Replace("【店铺名称】", fModel.ShopName);
                    break;
                case 4://绑定优惠券
                    AfterBindCoupon cModel = CommonLib.Helper.JsonDeserializeObject<AfterBindCoupon>(specModel);
                    model.WebContent = model.WebContent.Replace("【礼金券名称】", cModel.CouponDesc);
                    model.WebContent = model.WebContent.Replace("【到期时间】", cModel.EndDate.ToShortDateString());
                    //model.WebContent = model.WebContent.Replace("【销售时间】", cModel.SaleTime.ToLongDateString());
                    //model.WebContent = model.WebContent.Replace("【销售商品】", cModel.GoodsName);
                    break;
                case 5://实物订单发货后
                    AfterExpress eModel = CommonLib.Helper.JsonDeserializeObject<AfterExpress>(specModel);
                    model.SmsContent = model.SmsContent.Replace("【商品名称】", eModel.GoodsName);
                    model.SmsContent = model.SmsContent.Replace("【物流公司】", eModel.ExpressName);
                    model.SmsContent = model.SmsContent.Replace("【快递单号】", eModel.ExpressCode);

                    model.WebTitle = model.WebTitle.Replace("【商品名称】", eModel.GoodsName);
                    model.WebContent = model.WebContent.Replace("【商品名称】", eModel.GoodsName);
                    model.WebContent = model.WebContent.Replace("【物流公司】", eModel.ExpressName);
                    model.WebContent = model.WebContent.Replace("【快递单号】", eModel.ExpressCode);

                    model.MobileTitle = model.WebTitle.Replace("【商品名称】", eModel.GoodsName);
                    model.MobileContent = model.MobileContent.Replace("【商品名称】", eModel.GoodsName);
                    model.MobileContent = model.MobileContent.Replace("【物流公司】", eModel.ExpressName);
                    model.MobileContent = model.MobileContent.Replace("【快递单号】", eModel.ExpressCode);

                    break;
                case 6://反馈转为需求后
                case 7://需求完成后
                    AfterImportReq iModel = CommonLib.Helper.JsonDeserializeObject<AfterImportReq>(specModel);

                    if (model.EventId==6)
                    {
                        model.SmsContent = model.SmsContent.Replace("【建议描述】", iModel.RequirementDesc);
                    }
                    else
                    {
                        model.WebContent = model.WebContent.Replace("【建议描述】", iModel.RequirementDesc);
                        model.MobileContent = model.MobileContent.Replace("【建议描述】", iModel.RequirementDesc);
                    }
                    
                    break;
                case 101://高级版 订单完成后
                    AfterOrderSuccess orderAdvanceModel = CommonLib.Helper.JsonDeserializeObject<AfterOrderSuccess>(specModel);
                    //短信
                    model.SmsContent = model.SmsContent.Replace("【店铺名称】", orderAdvanceModel.ShopName);
                    model.SmsContent = model.SmsContent.Replace("【产品名称】", orderAdvanceModel.OrderName);
                    //邮件
                    model.EmailTitle = model.EmailTitle.Replace("【订单编号】", orderAdvanceModel.OrderNo);
                    //TODO:邮件内容
                    model.EmailContent = emailTemplate;
                    model.EmailContent = model.EmailContent.Replace("【店主姓名】", orderAdvanceModel.UserName);
                    model.EmailContent = model.EmailContent.Replace("【店铺名称】", orderAdvanceModel.ShopName);
                    model.EmailContent = model.EmailContent.Replace("【订单编号】", orderAdvanceModel.OrderNo);
                    model.EmailContent = model.EmailContent.Replace("【附加说明】", "");
                    //TODO:补充产品信息
                    model.EmailContent = model.EmailContent.Replace("【产品名称】", orderAdvanceModel.OrderName);
                    model.EmailContent = model.EmailContent.Replace("【产品价格】", orderAdvanceModel.OrderMoney.ToString());
                    model.EmailContent = model.EmailContent.Replace("【产品数量】",
                        orderAdvanceModel.OrderNum.ToString() + "个月");
                    model.EmailContent = model.EmailContent.Replace("【产品描述】",
                        "多达20名店员的权限、业绩管理让您的生意井井有条，会员储值、计次、优惠券功能把您拓客锁客。");
                    model.EmailContent = model.EmailContent.Replace("【推荐项目】", recommendSms + recommendMobile);

                    //站内推送
                    model.WebTitle = model.WebTitle.Replace("【产品名称】", orderAdvanceModel.OrderName);
                    model.WebContent = orderWebPushingContent.Replace("【店铺名称】", orderAdvanceModel.ShopName);
                    model.WebContent = model.WebContent.Replace("【产品名称】", orderAdvanceModel.OrderName);
                    model.WebContent = model.WebContent.Replace("【帮助链接】", "http://www.i200.cn/bbs/thread-3642-1-1.html");

                    break;
                case 102://手机橱窗 订单完成后
                    AfterOrderSuccess orderMobileShowModel = CommonLib.Helper.JsonDeserializeObject<AfterOrderSuccess>(specModel);
                    //短信
                    model.SmsContent = model.SmsContent.Replace("【店铺名称】", orderMobileShowModel.ShopName);
                    model.SmsContent = model.SmsContent.Replace("【产品名称】", orderMobileShowModel.OrderName);
                    //邮件
                    model.EmailTitle = model.EmailTitle.Replace("【订单编号】", orderMobileShowModel.OrderNo);
                    //TODO:邮件内容
                    model.EmailContent = emailTemplate;
                    model.EmailContent = model.EmailContent.Replace("【店主姓名】", orderMobileShowModel.UserName);
                    model.EmailContent = model.EmailContent.Replace("【店铺名称】", orderMobileShowModel.ShopName);
                    model.EmailContent = model.EmailContent.Replace("【订单编号】", orderMobileShowModel.OrderNo);
                    model.EmailContent = model.EmailContent.Replace("【附加说明】", "");
                    //TODO:补充产品信息
                    model.EmailContent = model.EmailContent.Replace("【产品名称】", orderMobileShowModel.OrderName);
                    model.EmailContent = model.EmailContent.Replace("【产品价格】", orderMobileShowModel.OrderMoney.ToString());
                    model.EmailContent = model.EmailContent.Replace("【产品数量】", orderMobileShowModel.OrderNum.ToString() + "个月");
                    model.EmailContent = model.EmailContent.Replace("【产品描述】", "价格不过百，再开一家店，线上线下生意联动。");
                    model.EmailContent = model.EmailContent.Replace("【推荐项目】", recommendAdvance);

                    //站内推送
                    model.WebTitle = model.WebTitle.Replace("【产品名称】", orderMobileShowModel.OrderName);
                    model.WebContent = orderWebPushingContent.Replace("【店铺名称】", orderMobileShowModel.ShopName);
                    model.WebContent = model.WebContent.Replace("【产品名称】", orderMobileShowModel.OrderName);
                    model.WebContent = model.WebContent.Replace("【帮助链接】", "http://www.i200.cn/bbs/thread-1938-1-1.html");
                    break;
                case 103://短信包 订单完成后
                    AfterOrderSuccess orderSmsModel = CommonLib.Helper.JsonDeserializeObject<AfterOrderSuccess>(specModel);
                    //短信
                    model.SmsContent = model.SmsContent.Replace("【店铺名称】", orderSmsModel.ShopName);
                    model.SmsContent = model.SmsContent.Replace("【产品名称】", orderSmsModel.OrderName);
                    //邮件
                    model.EmailTitle = model.EmailTitle.Replace("【订单编号】", orderSmsModel.OrderNo);
                    //TODO:邮件内容
                    model.EmailContent = emailTemplate;
                    model.EmailContent = model.EmailContent.Replace("【店主姓名】", orderSmsModel.UserName);
                    model.EmailContent = model.EmailContent.Replace("【店铺名称】", orderSmsModel.ShopName);
                    model.EmailContent = model.EmailContent.Replace("【订单编号】", orderSmsModel.OrderNo);
                    model.EmailContent = model.EmailContent.Replace("【附加说明】", "");
                    //TODO:补充产品信息
                    model.EmailContent = model.EmailContent.Replace("【产品名称】", orderSmsModel.OrderName);
                    model.EmailContent = model.EmailContent.Replace("【产品价格】", orderSmsModel.OrderMoney.ToString());
                    model.EmailContent = model.EmailContent.Replace("【产品数量】", orderSmsModel.OrderNum.ToString());
                    model.EmailContent = model.EmailContent.Replace("【产品描述】", "做活动、送祝福，发账单都可以用，1069通道发送，98%到达率，每条低至4分钱。");
                    model.EmailContent = model.EmailContent.Replace("【推荐项目】", recommendAdvance);

                    //站内推送
                    //无

                    break;
                case 104://实物商品 订单完成后
                    AfterOrderSuccess orderMaterialModel = CommonLib.Helper.JsonDeserializeObject<AfterOrderSuccess>(specModel);
                    //短信
                    model.SmsContent = model.SmsContent.Replace("【店铺名称】", orderMaterialModel.ShopName);
                    model.SmsContent = model.SmsContent.Replace("【产品名称】", orderMaterialModel.OrderName);
                    //邮件
                    model.EmailTitle = model.EmailTitle.Replace("【订单编号】", orderMaterialModel.OrderNo);
                    //TODO:邮件内容
                    model.EmailContent = emailTemplate;
                    model.EmailContent = model.EmailContent.Replace("【店主姓名】", orderMaterialModel.UserName);
                    model.EmailContent = model.EmailContent.Replace("【店铺名称】", orderMaterialModel.ShopName);
                    model.EmailContent = model.EmailContent.Replace("【订单编号】", orderMaterialModel.OrderNo);
                    model.EmailContent = model.EmailContent.Replace("【附加说明】", "我们将尽快为您安排发货，");
                    //TODO:补充产品信息
                    model.EmailContent = model.EmailContent.Replace("【产品名称】", orderMaterialModel.OrderName);
                    model.EmailContent = model.EmailContent.Replace("【产品价格】", orderMaterialModel.OrderMoney.ToString());
                    model.EmailContent = model.EmailContent.Replace("【产品数量】", orderMaterialModel.OrderNum.ToString());
                    model.EmailContent = model.EmailContent.Replace("【产品描述】", "请参照具体订单内容");
                    model.EmailContent = model.EmailContent.Replace("【推荐项目】", recommendAdvance + recommendSms);

                    //站内推送
                    //无
                    break;
                case 105://京东 订单完成后
                    AfterOrderSuccess orderJdModel = CommonLib.Helper.JsonDeserializeObject<AfterOrderSuccess>(specModel);
                    //短信
                    model.SmsContent = model.SmsContent.Replace("【店铺名称】", orderJdModel.ShopName);
                    model.SmsContent = model.SmsContent.Replace("【订单编号】", orderJdModel.OrderExpressId);
                    //邮件
                    model.EmailTitle = model.EmailTitle.Replace("【订单编号】", orderJdModel.OrderNo);
                    //TODO:邮件内容
                    model.EmailContent = emailTemplate;
                    model.EmailContent = model.EmailContent.Replace("【店主姓名】", orderJdModel.UserName);
                    model.EmailContent = model.EmailContent.Replace("【店铺名称】", orderJdModel.ShopName);
                    model.EmailContent = model.EmailContent.Replace("【订单编号】", orderJdModel.OrderNo);
                    model.EmailContent = model.EmailContent.Replace("【附加说明】", "承运物流是京东快递，发货后快递单号会以短信形式单独发给您，");
                    //TODO:补充产品信息
                    model.EmailContent = model.EmailContent.Replace("【产品名称】", orderJdModel.OrderName);
                    model.EmailContent = model.EmailContent.Replace("【产品价格】", orderJdModel.OrderMoney.ToString());
                    model.EmailContent = model.EmailContent.Replace("【产品数量】", orderJdModel.OrderNum.ToString());
                    model.EmailContent = model.EmailContent.Replace("【产品描述】", "请参照具体订单内容");
                    model.EmailContent = model.EmailContent.Replace("【推荐项目】", recommendAdvance + recommendSms);

                    //站内推送
                    //无

                    break;
                case 106://话费充值 订单完成后
                    AfterOrderSuccess orderMobileModel = CommonLib.Helper.JsonDeserializeObject<AfterOrderSuccess>(specModel);

                    model.SmsContent = model.SmsContent.Replace("【店铺名称】", orderMobileModel.ShopName);
                    model.SmsContent = model.SmsContent.Replace("【手机号码】", orderMobileModel.PhoneNumber);
                    model.SmsContent = model.SmsContent.Replace("【话费金额】", orderMobileModel.OrderMoney.ToString());

                    break;
                case 107://行业版 订单完成后
                    AfterOrderSuccess orderIndustryModel = CommonLib.Helper.JsonDeserializeObject<AfterOrderSuccess>(specModel);
                    //短信
                    model.SmsContent = model.SmsContent.Replace("【店铺名称】", orderIndustryModel.ShopName);
                    model.SmsContent = model.SmsContent.Replace("【产品名称】", orderIndustryModel.OrderName + "行业版");
                    //邮件
                    model.EmailTitle = model.EmailTitle.Replace("【订单编号】", orderIndustryModel.OrderNo);
                    //TODO:邮件内容
                    model.EmailContent = emailTemplate;
                    model.EmailContent = model.EmailContent.Replace("【店主姓名】", orderIndustryModel.UserName);
                    model.EmailContent = model.EmailContent.Replace("【店铺名称】", orderIndustryModel.ShopName);
                    model.EmailContent = model.EmailContent.Replace("【订单编号】", orderIndustryModel.OrderNo);
                    model.EmailContent = model.EmailContent.Replace("【附加说明】", "");
                    //TODO:补充产品信息
                    model.EmailContent = model.EmailContent.Replace("【产品名称】", orderIndustryModel.OrderName + "行业版");
                    model.EmailContent = model.EmailContent.Replace("【产品价格】", orderIndustryModel.OrderMoney.ToString());
                    model.EmailContent = model.EmailContent.Replace("【产品数量】", orderIndustryModel.OrderNum.ToString());
                    if (orderIndustryModel.BusId==67)//服装鞋帽
                    {
                        model.EmailContent = model.EmailContent.Replace("【产品描述】", "颜色尺码高效管理，多达50名店员任你设置。");
                    }
                    else if (orderIndustryModel.BusId == 68)//家电数码
                    {
                        model.EmailContent = model.EmailContent.Replace("【产品描述】", "商品串码高效管理，多达50名店员任你设置。");
                    }
                    else if (orderIndustryModel.BusId == 77)//美业
                    {
                        model.EmailContent = model.EmailContent.Replace("【产品描述】", "自由设置员工业绩提成规则，灵活添加会员多张计次卡。");
                    }
                    else
                    {
                        model.EmailContent = model.EmailContent.Replace("【产品描述】", "");
                    }
                    
                    model.EmailContent = model.EmailContent.Replace("【推荐项目】", recommendSms + recommendMobile);

                    //站内推送
                    model.WebTitle = model.WebTitle.Replace("【产品名称】", orderIndustryModel.OrderName);
                    model.WebContent = orderWebPushingContent.Replace("【店铺名称】", orderIndustryModel.ShopName);
                    model.WebContent = model.WebContent.Replace("【产品名称】", orderIndustryModel.OrderName);
                    model.WebContent = model.WebContent.Replace("【帮助链接】", "http://www.i200.cn/bbs/thread-7229-1-1.html");
                    break;
                case 108://连锁版 订单完成后
                    //AfterOrderSuccess orderLinkModel = CommonLib.Helper.JsonDeserializeObject<AfterOrderSuccess>(specModel);
                    ////短信
                    //model.SmsContent = model.SmsContent.Replace("【店铺名称】", orderLinkModel.ShopName);
                    //model.SmsContent = model.SmsContent.Replace("【产品名称】", orderLinkModel.OrderName);
                    ////邮件
                    //model.EmailTitle = model.EmailTitle.Replace("【产品名称】", orderLinkModel.OrderName);
                    ////TODO:邮件内容

                    ////站内推送
                    //model.WebTitle = model.WebTitle.Replace("【产品名称】", orderLinkModel.OrderName);
                    //model.WebContent = orderWebPushingContent.Replace("【店铺名称】", orderLinkModel.ShopName);
                    //model.WebContent = model.WebContent.Replace("【产品名称】", orderLinkModel.OrderName);
                    //model.WebContent = model.WebContent.Replace("【帮助链接】", "http://www.i200.cn/bbs/thread-3642-1-1.html");
                    break;
            }
            #endregion

            try
            {
                return //调用通用的批次生成方法
                    MessageSysControl.PreparePushingMsg(accidSet, 1, 4, model.MobileTitle, model.WebTitle, model.EmailTitle,
                                                        model.SmsContent, model.MobileContent, model.WebContent, model.EmailContent,
                                                        eventName, "", "trigger", model.MobileContentType, model.MobileContentUrl);
            }
            catch (Exception ex)
            {
                Logger.Error("根据模板生成批次信息出错！", ex);
                return "";
            }


        }

    }
}
