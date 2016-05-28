using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BLL;
using Controls.Helper4Control;
using Model;
using Model.Model4View;
using Utility;

namespace Controls.OperationReport
{
    public static class ReportControl
    {
        /// <summary>
        /// 获取两个时间段拉新数据
        /// </summary>
        /// <param name="stDate"></param>
        /// <param name="edDate"></param>
        /// <param name="lstDate"></param>
        /// <param name="ledDate"></param>
        /// <returns></returns>
        public static NewAccountModel GetNewAccountModel(DateTime stDate, DateTime edDate, DateTime lstDate,
            DateTime ledDate)
        {
            NewAccountModel model = new NewAccountModel();

            DateTime tedDate = ControlHelper.GetEndDate(edDate);
            List<NewAccountItem> tModel = OperationReportBLL.GetNewAccountModel(stDate, tedDate);

            DateTime ltedDate = ControlHelper.GetEndDate(ledDate);
            List<NewAccountItem> lModel = OperationReportBLL.GetNewAccountModel(lstDate, ltedDate);

            #region 合并两日数据

            foreach (var item in tModel)
            {
                foreach (var lItem in lModel)
                {
                    if (lItem.SourceName == item.SourceName)
                    {
                        item.LastWeekVal = lItem.ThisWeekVal;
                    }
                }
            }

            #endregion

            #region 数据分组

            List<ItemGroup> itemGroup = new List<ItemGroup>()
            {
                new ItemGroup("iOS",2,1),
                new ItemGroup("Android",10,1),
                new ItemGroup("主站",4,1),
                new ItemGroup("下载站",1,2),
                new ItemGroup("其他PC客户端",1,2),
                new ItemGroup("来源未知",1,2),
                new ItemGroup("总注册",1,2)
            };

            itemGroup.Find(x => x.GroupName == "总注册").DataList.Add(new NewAccountItem("总注册", 0, 0));

            itemGroup.Find(x => x.GroupName == "iOS").DataList.Add(new NewAccountItem("iPhone", 0, 0));
            itemGroup.Find(x => x.GroupName == "iOS").DataList.Add(new NewAccountItem("iPad", 0, 0));

            itemGroup.Find(x => x.GroupName == "Android").DataList.Add(new NewAccountItem("360", 0, 0));
            itemGroup.Find(x => x.GroupName == "Android").DataList.Add(new NewAccountItem("华为", 0, 0));
            itemGroup.Find(x => x.GroupName == "Android").DataList.Add(new NewAccountItem("小米", 0, 0));
            itemGroup.Find(x => x.GroupName == "Android").DataList.Add(new NewAccountItem("魅族", 0, 0));
            itemGroup.Find(x => x.GroupName == "Android").DataList.Add(new NewAccountItem("oppo", 0, 0));
            itemGroup.Find(x => x.GroupName == "Android").DataList.Add(new NewAccountItem("豌豆荚", 0, 0));
            itemGroup.Find(x => x.GroupName == "Android").DataList.Add(new NewAccountItem("百度", 0, 0));
            itemGroup.Find(x => x.GroupName == "Android").DataList.Add(new NewAccountItem("应用宝", 0, 0));
            itemGroup.Find(x => x.GroupName == "Android").DataList.Add(new NewAccountItem("安智", 0, 0));
            itemGroup.Find(x => x.GroupName == "Android").DataList.Add(new NewAccountItem("应用宝CPD", 0, 0)); 
            itemGroup.Find(x => x.GroupName == "Android").DataList.Add(new NewAccountItem("其他", 0, 0));

            itemGroup.Find(x => x.GroupName == "主站").DataList.Add(new NewAccountItem("手机web", 0, 0));
            itemGroup.Find(x => x.GroupName == "主站").DataList.Add(new NewAccountItem("主站安卓", 0, 0));
            itemGroup.Find(x => x.GroupName == "主站").DataList.Add(new NewAccountItem("主站客户端", 0, 0));
            itemGroup.Find(x => x.GroupName == "主站").DataList.Add(new NewAccountItem("主站注册", 0, 0));

            itemGroup.Find(x => x.GroupName == "下载站").DataList.Add(new NewAccountItem("下载站", 0, 0));
            itemGroup.Find(x => x.GroupName == "其他PC客户端").DataList.Add(new NewAccountItem("其他PC客户端", 0, 0));
            itemGroup.Find(x => x.GroupName == "来源未知").DataList.Add(new NewAccountItem("来源未知", 0, 0));

            foreach (var item in tModel)
            {
                itemGroup.Find(x => x.GroupName == "总注册").DataList[0].ThisWeekVal += item.ThisWeekVal;
                itemGroup.Find(x => x.GroupName == "总注册").DataList[0].LastWeekVal += item.LastWeekVal;

                foreach (var gItem in itemGroup)
                {
                    if (gItem.DataList.Find(x => x.SourceName == item.SourceName) != null)
                    {
                        gItem.DataList.Find(x => x.SourceName == item.SourceName).ThisWeekVal = item.ThisWeekVal;
                        gItem.DataList.Find(x => x.SourceName == item.SourceName).LastWeekVal = item.LastWeekVal;
                        gItem.DataList.Find(x => x.SourceName == item.SourceName).Percent =
                            item.LastWeekVal == 0
                                ? "-"
                                : (Convert.ToDecimal(item.ThisWeekVal - item.LastWeekVal) * 100 / item.LastWeekVal).ToString("F2") +
                                  "%";
                    }

                }
            }

            foreach (var item in itemGroup)
            {
                if (item.ColSpan > 1 && item.DataList.Count <= 0)
                {
                    item.GroupName = "Not Support";
                }
                if (item.RowSpan > 1)
                {
                    if (item.DataList.Count <= 0)
                    {
                        item.GroupName = "Not Support";
                    }
                    else
                    {
                        item.RowSpan = item.DataList.Count;
                    }
                }

                foreach (var dItem in item.DataList)
                {
                    if (string.IsNullOrEmpty(dItem.Percent))
                    {
                        if (dItem.LastWeekVal == 0)
                        {
                            dItem.Percent = "-";
                        }
                        else
                        {
                            dItem.Percent = (Convert.ToDecimal(dItem.ThisWeekVal - dItem.LastWeekVal) * 100 / dItem.LastWeekVal).ToString("F2") + "%";
                        }
                    }
                }

            }

            model.ItemGroupList = itemGroup;
            return model;

            #endregion
        }

        /// <summary>
        /// 获取本周期内转化数据
        /// </summary>
        /// <param name="stDate"></param>
        /// <param name="edDate"></param>
        /// <returns></returns>
        public static ConversionList GetConversionModel(DateTime stDate, DateTime edDate)
        {
            #region 获取转化数据

            List<ConversionModel> pageModelList = new List<ConversionModel>();
            pageModelList.Add(new ConversionModel("注册"));
            pageModelList.Add(new ConversionModel("登录"));
            pageModelList.Add(new ConversionModel("录入数据"));
            pageModelList.Add(new ConversionModel("次日留存"));
            pageModelList.Add(new ConversionModel("活跃"));
            pageModelList.Add(new ConversionModel("付费"));

            List<ConversionSource> modelList = OperationReportBLL.GetConversionList(stDate, edDate);
            foreach (var model in modelList)
            {
                switch (model.Source)
                {
                    case "iPhone":
                        pageModelList.Find(x => x.NameStr == "注册").iPhoneNum = model.Reg;
                        pageModelList.Find(x => x.NameStr == "登录").iPhoneNum = model.Login;
                        pageModelList.Find(x => x.NameStr == "录入数据").iPhoneNum = model.DataInput;
                        pageModelList.Find(x => x.NameStr == "次日留存").iPhoneNum = model.SecRetention;
                        pageModelList.Find(x => x.NameStr == "活跃").iPhoneNum = model.Active;
                        pageModelList.Find(x => x.NameStr == "付费").iPhoneNum = model.Paid;
                        break;
                    case "iPad":
                        pageModelList.Find(x => x.NameStr == "注册").iPadNum = model.Reg;
                        pageModelList.Find(x => x.NameStr == "登录").iPadNum = model.Login;
                        pageModelList.Find(x => x.NameStr == "录入数据").iPadNum = model.DataInput;
                        pageModelList.Find(x => x.NameStr == "次日留存").iPadNum = model.SecRetention;
                        pageModelList.Find(x => x.NameStr == "活跃").iPadNum = model.Active;
                        pageModelList.Find(x => x.NameStr == "付费").iPadNum = model.Paid;
                        break;
                    case "主站":
                        pageModelList.Find(x => x.NameStr == "注册").ZhuzhanNum = model.Reg;
                        pageModelList.Find(x => x.NameStr == "登录").ZhuzhanNum = model.Login;
                        pageModelList.Find(x => x.NameStr == "录入数据").ZhuzhanNum = model.DataInput;
                        pageModelList.Find(x => x.NameStr == "次日留存").ZhuzhanNum = model.SecRetention;
                        pageModelList.Find(x => x.NameStr == "活跃").ZhuzhanNum = model.Active;
                        pageModelList.Find(x => x.NameStr == "付费").ZhuzhanNum = model.Paid;
                        break;
                    case "360":
                        pageModelList.Find(x => x.NameStr == "注册").QihuNum = model.Reg;
                        pageModelList.Find(x => x.NameStr == "登录").QihuNum = model.Login;
                        pageModelList.Find(x => x.NameStr == "录入数据").QihuNum = model.DataInput;
                        pageModelList.Find(x => x.NameStr == "次日留存").QihuNum = model.SecRetention;
                        pageModelList.Find(x => x.NameStr == "活跃").QihuNum = model.Active;
                        pageModelList.Find(x => x.NameStr == "付费").QihuNum = model.Paid;
                        break;
                    case "应用宝":
                        pageModelList.Find(x => x.NameStr == "注册").TengxunNum = model.Reg;
                        pageModelList.Find(x => x.NameStr == "登录").TengxunNum = model.Login;
                        pageModelList.Find(x => x.NameStr == "录入数据").TengxunNum = model.DataInput;
                        pageModelList.Find(x => x.NameStr == "次日留存").TengxunNum = model.SecRetention;
                        pageModelList.Find(x => x.NameStr == "活跃").TengxunNum = model.Active;
                        pageModelList.Find(x => x.NameStr == "付费").TengxunNum = model.Paid;
                        break;
                    case "小米":
                        pageModelList.Find(x => x.NameStr == "注册").XiaomiNum = model.Reg;
                        pageModelList.Find(x => x.NameStr == "登录").XiaomiNum = model.Login;
                        pageModelList.Find(x => x.NameStr == "录入数据").XiaomiNum = model.DataInput;
                        pageModelList.Find(x => x.NameStr == "次日留存").XiaomiNum = model.SecRetention;
                        pageModelList.Find(x => x.NameStr == "活跃").XiaomiNum = model.Active;
                        pageModelList.Find(x => x.NameStr == "付费").XiaomiNum = model.Paid;
                        break;
                    case "华为":
                        pageModelList.Find(x => x.NameStr == "注册").HuaweiNum = model.Reg;
                        pageModelList.Find(x => x.NameStr == "登录").HuaweiNum = model.Login;
                        pageModelList.Find(x => x.NameStr == "录入数据").HuaweiNum = model.DataInput;
                        pageModelList.Find(x => x.NameStr == "次日留存").HuaweiNum = model.SecRetention;
                        pageModelList.Find(x => x.NameStr == "活跃").HuaweiNum = model.Active;
                        pageModelList.Find(x => x.NameStr == "付费").HuaweiNum = model.Paid;
                        break;
                    case "百度":
                        pageModelList.Find(x => x.NameStr == "注册").BaiduNum = model.Reg;
                        pageModelList.Find(x => x.NameStr == "登录").BaiduNum = model.Login;
                        pageModelList.Find(x => x.NameStr == "录入数据").BaiduNum = model.DataInput;
                        pageModelList.Find(x => x.NameStr == "次日留存").BaiduNum = model.SecRetention;
                        pageModelList.Find(x => x.NameStr == "活跃").BaiduNum = model.Active;
                        pageModelList.Find(x => x.NameStr == "付费").BaiduNum = model.Paid;
                        break;
                    case "魅族":
                        pageModelList.Find(x => x.NameStr == "注册").MeizuNum = model.Reg;
                        pageModelList.Find(x => x.NameStr == "登录").MeizuNum = model.Login;
                        pageModelList.Find(x => x.NameStr == "录入数据").MeizuNum = model.DataInput;
                        pageModelList.Find(x => x.NameStr == "次日留存").MeizuNum = model.SecRetention;
                        pageModelList.Find(x => x.NameStr == "活跃").MeizuNum = model.Active;
                        pageModelList.Find(x => x.NameStr == "付费").MeizuNum = model.Paid;
                        break;
                    case "oppo":
                        pageModelList.Find(x => x.NameStr == "注册").OppoNum = model.Reg;
                        pageModelList.Find(x => x.NameStr == "登录").OppoNum = model.Login;
                        pageModelList.Find(x => x.NameStr == "录入数据").OppoNum = model.DataInput;
                        pageModelList.Find(x => x.NameStr == "次日留存").OppoNum = model.SecRetention;
                        pageModelList.Find(x => x.NameStr == "活跃").OppoNum = model.Active;
                        pageModelList.Find(x => x.NameStr == "付费").OppoNum = model.Paid;
                        break;
                    case "安智":
                        pageModelList.Find(x => x.NameStr == "注册").AnzhiNum = model.Reg;
                        pageModelList.Find(x => x.NameStr == "登录").AnzhiNum = model.Login;
                        pageModelList.Find(x => x.NameStr == "录入数据").AnzhiNum = model.DataInput;
                        pageModelList.Find(x => x.NameStr == "次日留存").AnzhiNum = model.SecRetention;
                        pageModelList.Find(x => x.NameStr == "活跃").AnzhiNum = model.Active;
                        pageModelList.Find(x => x.NameStr == "付费").AnzhiNum = model.Paid;
                        break;
                    case "豌豆荚":
                        pageModelList.Find(x => x.NameStr == "注册").WandoujiaNum = model.Reg;
                        pageModelList.Find(x => x.NameStr == "登录").WandoujiaNum = model.Login;
                        pageModelList.Find(x => x.NameStr == "录入数据").WandoujiaNum = model.DataInput;
                        pageModelList.Find(x => x.NameStr == "次日留存").WandoujiaNum = model.SecRetention;
                        pageModelList.Find(x => x.NameStr == "活跃").WandoujiaNum = model.Active;
                        pageModelList.Find(x => x.NameStr == "付费").WandoujiaNum = model.Paid;
                        break;
                    case "其他":
                        pageModelList.Find(x => x.NameStr == "注册").Other = model.Reg;
                        pageModelList.Find(x => x.NameStr == "登录").Other = model.Login;
                        pageModelList.Find(x => x.NameStr == "录入数据").Other = model.DataInput;
                        pageModelList.Find(x => x.NameStr == "次日留存").Other = model.SecRetention;
                        pageModelList.Find(x => x.NameStr == "活跃").Other = model.Active;
                        pageModelList.Find(x => x.NameStr == "付费").Other = model.Paid;
                        break;
                    case "PC":
                        pageModelList.Find(x => x.NameStr == "注册").PcClientNum = model.Reg;
                        pageModelList.Find(x => x.NameStr == "登录").PcClientNum = model.Login;
                        pageModelList.Find(x => x.NameStr == "录入数据").PcClientNum = model.DataInput;
                        pageModelList.Find(x => x.NameStr == "次日留存").PcClientNum = model.SecRetention;
                        pageModelList.Find(x => x.NameStr == "活跃").PcClientNum = model.Active;
                        pageModelList.Find(x => x.NameStr == "付费").PcClientNum = model.Paid;
                        break;
                    case "网页":
                        pageModelList.Find(x => x.NameStr == "注册").WebNum = model.Reg;
                        pageModelList.Find(x => x.NameStr == "登录").WebNum = model.Login;
                        pageModelList.Find(x => x.NameStr == "录入数据").WebNum = model.DataInput;
                        pageModelList.Find(x => x.NameStr == "次日留存").WebNum = model.SecRetention;
                        pageModelList.Find(x => x.NameStr == "活跃").WebNum = model.Active;
                        pageModelList.Find(x => x.NameStr == "付费").WebNum = model.Paid;
                        break;
                }
            }

            ConversionList list = new ConversionList();
            list.DataList = pageModelList;
            return list;

            #endregion

        }

        /// <summary>
        /// 获取留存报表
        /// </summary>
        /// <param name="stDate"></param>
        /// <param name="edDate"></param>
        /// <param name="lstDate"></param>
        /// <param name="ledDate"></param>
        /// <returns></returns>
        public static List<UgcGroup> GetRetentionModel(DateTime stDate, DateTime edDate, DateTime lstDate,
            DateTime ledDate)
        {
            List<UgcGroup> list = new List<UgcGroup>();

            //初始化登录分维
            List<ItemGroup> itemGroup = new List<ItemGroup>();

            #region 生成留存头组数据
            //生成通用登录分维组外的字段信息
            ItemGroup logItem = new ItemGroup("日登录数", 1, 1);
            logItem.DataList.Add(new NewAccountItem("日均登录", 0, 0));
            itemGroup.Add(logItem);

            //生成用户登录Model组
            UgcGroup logGroup = GetGeneralUserDimension("用户登录", stDate, edDate, lstDate, ledDate);
            //合并同一条件组的字段信息
            if (itemGroup != null && itemGroup.Count >= 1)
            {
                logGroup = GetMergeGroup(logGroup, itemGroup, null);
            }
            //生成单独数据
            DateTime tedDate = ControlHelper.GetEndDate(edDate);
            List<NewAccountItem> tModel = OperationReportBLL.GetRetentionData(stDate, tedDate);

            DateTime ltedDate = ControlHelper.GetEndDate(ledDate);
            List<NewAccountItem> lModel = OperationReportBLL.GetRetentionData(lstDate, ltedDate);

            logGroup = GetGroupModel(tModel, lModel, logGroup);
            list.Add(logGroup);
            #endregion

            #region 生成留存其他数据
            itemGroup.Clear();
            logGroup = new UgcGroup("其他留存数据");

            //表格字段初始化
            itemGroup.Add(new ItemGroup("总登录率", 1, 1, new NewAccountItem("登录率", 0, 0)));
            itemGroup.Add(new ItemGroup("连续登录用户", 1, 1, new NewAccountItem("连续登录", 0, 0)));
            itemGroup.Add(new ItemGroup("净新增活忠", 1, 1, new NewAccountItem("新增活忠", 0, 0)));
            itemGroup.Add(new ItemGroup("流失活忠", 1, 1, new NewAccountItem("流失活忠", 0, 0)));
            itemGroup.Add(new ItemGroup("流失回流数", 1, 1, new NewAccountItem("流失回流", 0, 0)));
            itemGroup.Add(new ItemGroup("只移动端登录", 1, 1, new NewAccountItem("仅移动端登录", 0, 0)));
            itemGroup.Add(new ItemGroup("只电脑端登录", 1, 1, new NewAccountItem("仅电脑端登录", 0, 0)));
            itemGroup.Add(new ItemGroup("全端用户", 1, 1, new NewAccountItem("全端登录", 0, 0)));
            logGroup.DataList = itemGroup;

            list.Add(GetGroupModel(tModel, lModel, logGroup));

            #endregion


            return list;

        }

        /// <summary>
        /// 获取促活报表
        /// </summary>
        /// <param name="stDate"></param>
        /// <param name="edDate"></param>
        /// <param name="lstDate"></param>
        /// <param name="ledDate"></param>
        /// <returns></returns>
        public static List<UgcGroup> GetAvgDateModel(DateTime stDate, DateTime edDate, DateTime lstDate, DateTime ledDate)
        {
            //生成用户登录Model组，用于切分多维用户UGC均值
            UgcGroup logGroup = GetGeneralUserDimension("用户登录", stDate, edDate, lstDate, ledDate);
            //时间归一化
            DateTime tedDate = ControlHelper.GetEndDate(edDate);
            DateTime ltedDate = ControlHelper.GetEndDate(ledDate);

            int tAllLog = OperationReportBLL.GetIndependLog(stDate, tedDate);
            int lAllLog = OperationReportBLL.GetIndependLog(lstDate, ltedDate);

            //返回数据组
            List<UgcGroup> list = new List<UgcGroup>();


            #region 销售数据
            //生成头数据       
            UgcGroup saleGroup = GetGeneralUserDimension("收银记账");//组中置分维条件

            List<ItemGroup> itemGroupSaleBefore = new List<ItemGroup>();//组前置行
            itemGroupSaleBefore.Add(new ItemGroup("登录用户日销售笔数", 1, 1, new NewAccountItem("登录日均销售", 0, 0)));

            List<ItemGroup> itemGroupSaleAfter = new List<ItemGroup>();//组后置行
            itemGroupSaleAfter.Add(new ItemGroup("录入销售店铺数", 1, 1, new NewAccountItem("销售独立店铺", 0, 0)));
            itemGroupSaleAfter.Add(new ItemGroup("补录销售笔数", 1, 1, new NewAccountItem("补录笔数", 0, 0)));
            itemGroupSaleAfter.Add(new ItemGroup("补录销售店铺数", 1, 1, new NewAccountItem("补录店铺数", 0, 0)));
            itemGroupSaleAfter.Add(new ItemGroup("挂单笔数", 1, 1, new NewAccountItem("挂单笔数", 0, 0)));

            //生成完整组
            if (itemGroupSaleBefore != null && itemGroupSaleBefore.Count >= 1)
            {
                saleGroup = GetMergeGroup(saleGroup, itemGroupSaleBefore, itemGroupSaleAfter);
            }

            //生成分维数据并处理均值
            List<NewAccountItem> tSaleModel = OperationReportBLL.GetSalePart(stDate, tedDate);
            List<NewAccountItem> lSaleModel = OperationReportBLL.GetSalePart(lstDate, ltedDate);

            saleGroup = GetGroupModel(tSaleModel, lSaleModel, saleGroup);

            //均值处理(总值有“日均”关键字过滤)
            saleGroup = LogAvgProcess(logGroup, saleGroup, tAllLog, lAllLog);

            list.Add(saleGroup);
            #endregion


            #region 商品数据
            //生成头数据       
            UgcGroup goodsGroup = GetGeneralUserDimension("商品管理");//组中置分维条件

            List<ItemGroup> itemGroupgoodsBefore = new List<ItemGroup>();//组前置行
            itemGroupgoodsBefore.Add(new ItemGroup("登录用户日新增商品", 1, 1, new NewAccountItem("登录日均商品", 0, 0)));

            List<ItemGroup> itemGroupgoodsAfter = new List<ItemGroup>();//组后置行
            itemGroupgoodsAfter.Add(new ItemGroup("录入商品店铺数", 1, 1, new NewAccountItem("商品录入独立店铺", 0, 0)));

            //生成完整组
            if (itemGroupgoodsBefore != null && itemGroupgoodsBefore.Count >= 1)
            {
                goodsGroup = GetMergeGroup(goodsGroup, itemGroupgoodsBefore, itemGroupgoodsAfter);
            }

            //生成分维数据并处理均值
            List<NewAccountItem> tGoodsModel = OperationReportBLL.GetGoodsPart(stDate, tedDate);
            List<NewAccountItem> lGoodsModel = OperationReportBLL.GetGoodsPart(lstDate, ltedDate);

            goodsGroup = GetGroupModel(tGoodsModel, lGoodsModel, goodsGroup);

            //均值处理(总值有“日均”关键字过滤)
            goodsGroup = LogAvgProcess(logGroup, goodsGroup, tAllLog, lAllLog);

            list.Add(goodsGroup);
            #endregion


            #region 会员数据
            //生成头数据       
            UgcGroup userGroup = GetGeneralUserDimension("会员管理");//组中置分维条件

            List<ItemGroup> itemGroupUserBefore = new List<ItemGroup>();//组前置行
            itemGroupUserBefore.Add(new ItemGroup("登录用户日新增会员", 1, 1, new NewAccountItem("登录日均会员", 0, 0)));

            List<ItemGroup> itemGroupUserAfter = new List<ItemGroup>();//组后置行
            itemGroupUserAfter.Add(new ItemGroup("录入会员店铺数", 1, 1, new NewAccountItem("会员录入独立店铺", 0, 0)));

            //生成完整组
            if (itemGroupUserBefore != null && itemGroupUserBefore.Count >= 1)
            {
                userGroup = GetMergeGroup(userGroup, itemGroupUserBefore, itemGroupUserAfter);
            }

            //生成分维数据并处理均值
            List<NewAccountItem> tUserModel = OperationReportBLL.GetUserPart(stDate, tedDate);
            List<NewAccountItem> lUserModel = OperationReportBLL.GetUserPart(lstDate, ltedDate);

            userGroup = GetGroupModel(tUserModel, lUserModel, userGroup);

            //均值处理(总值有“日均”关键字过滤)
            userGroup = LogAvgProcess(logGroup, userGroup, tAllLog, lAllLog);

            list.Add(userGroup);
            #endregion


            #region 支出数据
            //生成头数据       
            UgcGroup payGroup = GetGeneralUserDimension("支出管理");//组中置分维条件

            List<ItemGroup> itemGroupPayBefore = new List<ItemGroup>();//组前置行
            itemGroupPayBefore.Add(new ItemGroup("登录用户日新增支出", 1, 1, new NewAccountItem("登录日均支出", 0, 0)));

            List<ItemGroup> itemGroupPayAfter = new List<ItemGroup>();//组后置行
            itemGroupPayAfter.Add(new ItemGroup("录入支出店铺数", 1, 1, new NewAccountItem("支出录入独立店铺", 0, 0)));

            //生成完整组
            if (itemGroupPayBefore != null && itemGroupPayBefore.Count >= 1)
            {
                payGroup = GetMergeGroup(payGroup, itemGroupPayBefore, itemGroupPayAfter);
            }

            //生成分维数据并处理均值
            List<NewAccountItem> tPayModel = OperationReportBLL.GetPayPart(stDate, tedDate);
            List<NewAccountItem> lPayModel = OperationReportBLL.GetPayPart(lstDate, ltedDate);

            payGroup = GetGroupModel(tPayModel, lPayModel, payGroup);

            //均值处理(总值有“日均”关键字过滤)
            payGroup = LogAvgProcess(logGroup, payGroup, tAllLog, lAllLog);

            list.Add(payGroup);
            #endregion


            #region 短信数据
            //生成头数据       
            UgcGroup smsGroup = GetGeneralUserDimension("短信发送");//组中置分维条件

            List<ItemGroup> itemGroupSmsBefore = new List<ItemGroup>();//组前置行
            itemGroupSmsBefore.Add(new ItemGroup("登录用户日短信数", 1, 1, new NewAccountItem("登录日均短信数", 0, 0)));

            List<ItemGroup> itemGroupSmsAfter = new List<ItemGroup>();//组后置行
            itemGroupSmsAfter.Add(new ItemGroup("发送短信店铺数", 1, 1, new NewAccountItem("发送短信独立店铺", 0, 0)));
            itemGroupSmsAfter.Add(new ItemGroup("营销短信数", 1, 1, new NewAccountItem("营销短信", 0, 0)));
            itemGroupSmsAfter.Add(new ItemGroup("维客短信数", 1, 1, new NewAccountItem("维客短信", 0, 0)));

            //生成完整组
            if (itemGroupSmsBefore != null && itemGroupSmsBefore.Count >= 1)
            {
                smsGroup = GetMergeGroup(smsGroup, itemGroupSmsBefore, itemGroupSmsAfter);
            }

            //生成分维数据并处理均值
            List<NewAccountItem> tSmsModel = OperationReportBLL.GetSmsPart(stDate, tedDate);
            List<NewAccountItem> lSmsModel = OperationReportBLL.GetSmsPart(lstDate, ltedDate);

            smsGroup = GetGroupModel(tSmsModel, lSmsModel, smsGroup);

            //均值处理(总值有“日均”关键字过滤)
            smsGroup = LogAvgProcess(logGroup, smsGroup, tAllLog, lAllLog);

            list.Add(smsGroup);
            #endregion

            #region 生成留存其他数据
            List<ItemGroup> itemGroupshowCase = new List<ItemGroup>();//组前置行
            UgcGroup showCaseGroup=new UgcGroup("手机橱窗");

            //表格字段初始化
            itemGroupshowCase.Add(new ItemGroup("手机橱窗浏览数", 1, 1, new NewAccountItem("pv", 0, 0)));
            itemGroupshowCase.Add(new ItemGroup("手机橱窗访客数", 1, 1, new NewAccountItem("uv", 0, 0)));
            itemGroupshowCase.Add(new ItemGroup("手机橱窗展示商品数", 1, 1, new NewAccountItem("日均新增商品", 0, 0)));
            itemGroupshowCase.Add(new ItemGroup("手机橱窗订单数", 1, 1, new NewAccountItem("手机橱窗订单", 0, 0)));
            itemGroupshowCase.Add(new ItemGroup("手机橱窗支付数", 1, 1, new NewAccountItem("手机橱窗支付", 0, 0)));
            itemGroupshowCase.Add(new ItemGroup("手机橱窗交易金额", 1, 1, new NewAccountItem("周期内交易金额", 0, 0)));
            itemGroupshowCase.Add(new ItemGroup("在版手机橱窗套餐用户数", 1, 1, new NewAccountItem("在版用户", 0, 0)));

            showCaseGroup.DataList = itemGroupshowCase;

            //生成分维数据并处理均值
            List<NewAccountItem> tShowModel = OperationReportBLL.GetShowPart(stDate, tedDate);
            List<NewAccountItem> lShowModel = OperationReportBLL.GetShowPart(lstDate, ltedDate);
            list.Add(GetGroupModel(tShowModel, lShowModel, showCaseGroup));

            #endregion


            return list;

        }

        /// <summary>
        /// 获取收入报表
        /// </summary>
        /// <param name="stDate"></param>
        /// <param name="edDate"></param>
        /// <param name="lstDate"></param>
        /// <param name="ledDate"></param>
        /// <returns></returns>
        public static List<UgcGroup> GetIncomeModel(DateTime stDate, DateTime edDate, DateTime lstDate, DateTime ledDate)
        {
            //时间归一化
            DateTime tedDate = ControlHelper.GetEndDate(edDate);
            DateTime ltedDate = ControlHelper.GetEndDate(ledDate);

            //返回数据组
            List<UgcGroup> list = new List<UgcGroup>();            

            #region 生成收入数据
            List<ItemGroup> itemGroupshowCase = new List<ItemGroup>();//组前置行
            UgcGroup incomeGroup = new UgcGroup("付费信息");

            //获取订单信息
            Dictionary<string, decimal> tDic = GetOrderInfo(stDate, tedDate);
            Dictionary<string, decimal> lDic = GetOrderInfo(lstDate, ltedDate);

            //表格字段初始化
            itemGroupshowCase.Add(new ItemGroup("总销售额", 1, 1, new NewAccountItem("总订单", tDic["总订单"], lDic["总订单"], ControlHelper.GetFixDecimalStr(tDic["总订单"], lDic["总订单"]))));
            itemGroupshowCase.Add(new ItemGroup("Saas服务", 1, 1, new NewAccountItem("Saas服务", tDic["Saas服务"], lDic["Saas服务"], ControlHelper.GetFixDecimalStr(tDic["Saas服务"], lDic["Saas服务"]))));
            itemGroupshowCase.Add(new ItemGroup("短信", 1, 1, new NewAccountItem("短信", tDic["短信"], lDic["短信"], ControlHelper.GetFixDecimalStr(tDic["短信"], lDic["短信"]))));
            itemGroupshowCase.Add(new ItemGroup("话费", 1, 1, new NewAccountItem("话费", tDic["话费"], lDic["话费"], ControlHelper.GetFixDecimalStr(tDic["话费"], lDic["话费"]))));
            itemGroupshowCase.Add(new ItemGroup("硬件", 1, 1, new NewAccountItem("硬件", tDic["硬件"], lDic["硬件"], ControlHelper.GetFixDecimalStr(tDic["硬件"], lDic["硬件"]))));

            itemGroupshowCase.Add(new ItemGroup("总销售笔数", 1, 1, new NewAccountItem("总订单笔数", tDic["总订单笔数"], lDic["总订单笔数"], ControlHelper.GetFixDecimalStr(tDic["总订单笔数"], lDic["总订单笔数"]))));
            itemGroupshowCase.Add(new ItemGroup("Saas服务笔数", 1, 1, new NewAccountItem("Saas服务笔数", tDic["Saas服务笔数"], lDic["Saas服务笔数"], ControlHelper.GetFixDecimalStr(tDic["Saas服务笔数"], lDic["Saas服务笔数"]))));
            itemGroupshowCase.Add(new ItemGroup("短信笔数", 1, 1, new NewAccountItem("短信笔数", tDic["短信笔数"], lDic["短信笔数"], ControlHelper.GetFixDecimalStr(tDic["短信笔数"], lDic["短信笔数"]))));
            itemGroupshowCase.Add(new ItemGroup("话费笔数", 1, 1, new NewAccountItem("话费笔数", tDic["话费笔数"], lDic["话费笔数"], ControlHelper.GetFixDecimalStr(tDic["话费笔数"], lDic["话费笔数"]))));
            itemGroupshowCase.Add(new ItemGroup("硬件笔数", 1, 1, new NewAccountItem("硬件笔数", tDic["硬件笔数"], lDic["硬件笔数"], ControlHelper.GetFixDecimalStr(tDic["硬件笔数"], lDic["硬件笔数"]))));

            itemGroupshowCase.Add(new ItemGroup("现金券", 1, 1, new NewAccountItem("使用现金券金额", 0, 0)));
            itemGroupshowCase.Add(new ItemGroup("抵现", 1, 1, new NewAccountItem("抵现金额", 0, 0)));
            itemGroupshowCase.Add(new ItemGroup("发出现金券", 1, 1, new NewAccountItem("发出现金券", 0, 0)));
            itemGroupshowCase.Add(new ItemGroup("使用现金券", 1, 1, new NewAccountItem("使用现金券", 0, 0)));
            itemGroupshowCase.Add(new ItemGroup("现金券使用率", 1, 1, new NewAccountItem("现金券使用率", 0, 0)));
            itemGroupshowCase.Add(new ItemGroup("付费用户数", 1, 1, new NewAccountItem("付费用户数", 0, 0)));
            itemGroupshowCase.Add(new ItemGroup("付费用户续约率", 1, 1, new NewAccountItem("付费用户续约率", 0, 0)));
            itemGroupshowCase.Add(new ItemGroup("高付费用户续约率", 1, 1, new NewAccountItem("高付费用户续约率", 0, 0)));
            itemGroupshowCase.Add(new ItemGroup("活忠用户平均收入", 1, 1, new NewAccountItem("活忠用户平均收入", 0, 0)));

            incomeGroup.DataList = itemGroupshowCase;

            //生成分维数据并处理均值
            List<NewAccountItem> tIncomeModel = OperationReportBLL.GetIncomePart(stDate, tedDate);
            List<NewAccountItem> lIncomeModel = OperationReportBLL.GetIncomePart(lstDate, ltedDate);
            list.Add(GetGroupModel(tIncomeModel, lIncomeModel, incomeGroup));

            #endregion


            return list;

        }

        #region 通用辅助程序
        /// <summary>
        /// 获取基本用户维度初始化组
        /// </summary>
        /// <param name="title"></param>
        /// <param name="stDate"></param>
        /// <param name="edDate"></param>
        /// <param name="lstDate"></param>
        /// <param name="ledDate"></param>
        /// <returns>
        /// 带时间参数则返回填充数据的列表
        /// 不带参数返回空列表
        /// </returns>
        public static UgcGroup GetGeneralUserDimension(string title, DateTime? stDate = null, DateTime? edDate = null, DateTime? lstDate = null, DateTime? ledDate = null)
        {
            UgcGroup group = new UgcGroup(title);

            List<ItemGroup> itemGroup = new List<ItemGroup>();

            itemGroup.Add(new ItemGroup("按设备", 4, 1));
            itemGroup.Add(new ItemGroup("按状态", 6, 1));
            itemGroup.Add(new ItemGroup("按版本", 2, 1));
            itemGroup.Add(new ItemGroup("按营业额", 5, 1));
            itemGroup.Add(new ItemGroup("按新老用户", 2, 1));

            itemGroup.Find(x => x.GroupName == "按设备").DataList.Add(new NewAccountItem("Android", 0, 0));
            itemGroup.Find(x => x.GroupName == "按设备").DataList.Add(new NewAccountItem("iPhone", 0, 0));
            itemGroup.Find(x => x.GroupName == "按设备").DataList.Add(new NewAccountItem("iPad", 0, 0));
            itemGroup.Find(x => x.GroupName == "按设备").DataList.Add(new NewAccountItem("Web & PC", 0, 0));

            itemGroup.Find(x => x.GroupName == "按状态").DataList.Add(new NewAccountItem("流失", 0, 0));
            itemGroup.Find(x => x.GroupName == "按状态").DataList.Add(new NewAccountItem("新注册", 0, 0));
            itemGroup.Find(x => x.GroupName == "按状态").DataList.Add(new NewAccountItem("休眠", 0, 0));
            itemGroup.Find(x => x.GroupName == "按状态").DataList.Add(new NewAccountItem("需关怀", 0, 0));
            itemGroup.Find(x => x.GroupName == "按状态").DataList.Add(new NewAccountItem("活跃", 0, 0));
            itemGroup.Find(x => x.GroupName == "按状态").DataList.Add(new NewAccountItem("忠诚", 0, 0));

            itemGroup.Find(x => x.GroupName == "按版本").DataList.Add(new NewAccountItem("免费版", 0, 0));
            itemGroup.Find(x => x.GroupName == "按版本").DataList.Add(new NewAccountItem("高级版", 0, 0));

            itemGroup.Find(x => x.GroupName == "按营业额").DataList.Add(new NewAccountItem("<1万", 0, 0));
            itemGroup.Find(x => x.GroupName == "按营业额").DataList.Add(new NewAccountItem("1-5万", 0, 0));
            itemGroup.Find(x => x.GroupName == "按营业额").DataList.Add(new NewAccountItem("5-10万", 0, 0));
            itemGroup.Find(x => x.GroupName == "按营业额").DataList.Add(new NewAccountItem("10-30万", 0, 0));
            itemGroup.Find(x => x.GroupName == "按营业额").DataList.Add(new NewAccountItem(">30万", 0, 0));

            itemGroup.Find(x => x.GroupName == "按新老用户").DataList.Add(new NewAccountItem("新用户", 0, 0));
            itemGroup.Find(x => x.GroupName == "按新老用户").DataList.Add(new NewAccountItem("老用户", 0, 0));

            group.DataList = itemGroup;

            //如果传入的时间参数不为null则默认初始化登录数据
            if (stDate != null && edDate != null)
            {
                DateTime st = Convert.ToDateTime(stDate);
                DateTime ed = Convert.ToDateTime(edDate);

                DateTime lst = Convert.ToDateTime(lstDate);
                DateTime led = Convert.ToDateTime(ledDate);

                //本周期登录的分组用户需要持久化 在获取均值的时候作为分母
                DateTime tedDate = ControlHelper.GetEndDate(ed);
                List<NewAccountItem> tModel = OperationReportBLL.GetDimensionLogin(st, tedDate);

                DateTime ltedDate = ControlHelper.GetEndDate(led);
                List<NewAccountItem> lModel = OperationReportBLL.GetDimensionLogin(lst, ltedDate);

                return GetGroupModel(tModel, lModel, group);
            }

            return group;
        }

        /// <summary>
        /// 生成条件组对比数据的通用输出
        /// </summary>
        /// <param name="tList"></param>
        /// <param name="lList"></param>
        /// <param name="emptyGroup"></param>
        /// <returns></returns>
        public static UgcGroup GetGroupModel(List<NewAccountItem> tList, List<NewAccountItem> lList, UgcGroup emptyGroup)
        {
            if (tList != null && lList != null && emptyGroup != null)
            {
                foreach (var item in tList)
                {
                    foreach (var lItem in lList)
                    {
                        if (lItem.SourceName == item.SourceName)
                        {
                            item.LastWeekVal = lItem.ThisWeekVal;
                        }
                    }
                }

                foreach (var item in tList)
                {
                    foreach (var gItem in emptyGroup.DataList)
                    {
                        //如果列表中存在
                        try
                        {
                            if (gItem.DataList.Find(x => x.SourceName == item.SourceName) != null)
                            {
                                gItem.DataList.Find(x => x.SourceName == item.SourceName).ThisWeekVal += item.ThisWeekVal;
                                gItem.DataList.Find(x => x.SourceName == item.SourceName).LastWeekVal += item.LastWeekVal;
                                gItem.DataList.Find(x => x.SourceName == item.SourceName).Percent =
                                    item.LastWeekVal == 0
                                        ? "-"
                                        : (Convert.ToDecimal(item.ThisWeekVal - item.LastWeekVal) * 100 / item.LastWeekVal).ToString("F2") +
                                          "%";
                            }
                            //else if(gItem.DataList.Exists(x => x.ThisWeekVal!=0)&& gItem.DataList.Exists(x => x.LastWeekVal != 0))
                            //{
                            //    gItem.DataList.Find(x => x.ThisWeekVal != 0).Percent =
                            //        item.LastWeekVal == 0
                            //            ? "-"
                            //            : (Convert.ToDecimal(item.ThisWeekVal - item.LastWeekVal) * 100 / item.LastWeekVal).ToString("F2") +
                            //              "%";
                            //}
                        }
                        catch (Exception ex)
                        {
                            Logger.Error("运营分析报表通用维度遍历出错！", ex);
                            continue;
                        }
                    }
                }

                return emptyGroup;
            }
            else
            {
                Logger.Warn("生成条件对比组的输入数据存在空数据！");
                return null;
            }
        }

        /// <summary>
        /// 拼装通用返回列表字段
        /// </summary>
        /// <param name="logGroup"></param>
        /// <param name="otherGroup"></param>
        /// <param name="afterGroup"></param>
        /// <returns></returns>
        public static UgcGroup GetMergeGroup(UgcGroup logGroup, List<ItemGroup> otherGroup, List<ItemGroup> afterGroup)
        {
            UgcGroup group = new UgcGroup();
            List<ItemGroup> itemGroup = new List<ItemGroup>();

            if (otherGroup != null && otherGroup.Count >= 0)
            {
                foreach (var item in otherGroup)
                {
                    itemGroup.Add(item);
                }
            }

            itemGroup.AddRange(logGroup.DataList);

            if (afterGroup != null && afterGroup.Count >= 0)
            {
                itemGroup.AddRange(afterGroup);
            }

            group.DataList = itemGroup;

            return group;
        }

        /// <summary>
        /// 分维数据的均值处理
        /// </summary>
        /// <param name="log"></param>
        /// <param name="data"></param>
        /// <param name="tAllLog"></param>
        /// <param name="lAllLog"></param>
        /// <returns></returns>
        public static UgcGroup LogAvgProcess(UgcGroup log, UgcGroup data,int tAllLog=0, int lAllLog = 0)
        {
            if (data != null && data.DataList != null && data.DataList.Count > 0)
            {
                foreach (var item in data.DataList)
                {
                    foreach (var lItem in item.DataList)
                    {
                        if (lItem.SourceName.Contains("日均"))
                        {
                            lItem.ThisWeekVal=
                                tAllLog == 0?0: Math.Round(lItem.ThisWeekVal / tAllLog, 2);
                            lItem.LastWeekVal =
                                lAllLog == 0 ? 0 : Math.Round(lItem.LastWeekVal / lAllLog, 2);
                            lItem.Percent =
                                lItem.LastWeekVal == 0
                                        ? "-"
                                        : (Convert.ToDecimal(lItem.ThisWeekVal - lItem.LastWeekVal) * 100 / lItem.LastWeekVal).ToString("F2") +
                                          "%";
                        }

                        if (log.DataList.Exists(x => x.DataList.Exists(y => y.SourceName == lItem.SourceName)))
                        {
                            var v1Data = log.DataList.Find(x => x.DataList.Exists(y => y.SourceName == lItem.SourceName));
                            var v2Data = v1Data.DataList.Find(x => x.SourceName == lItem.SourceName);

                            lItem.ThisWeekVal =
                                v2Data.ThisWeekVal == 0 ? 0 : Math.Round(lItem.ThisWeekVal/v2Data.ThisWeekVal, 2);
                            lItem.LastWeekVal =
                                v2Data.LastWeekVal == 0 ? 0 : Math.Round(lItem.LastWeekVal/v2Data.LastWeekVal, 2);

                            lItem.Percent=
                                lItem.LastWeekVal == 0
                                        ? "-"
                                        : (Convert.ToDecimal(lItem.ThisWeekVal - lItem.LastWeekVal) * 100 / lItem.LastWeekVal).ToString("F2") +
                                          "%";
                        }
                    }

                }
            }

            return data;

        }

        /// <summary>
        /// 获取订单信息
        /// </summary>
        /// <param name="stDate"></param>
        /// <param name="edDate"></param>
        /// <returns></returns>
        public static Dictionary<string,decimal> GetOrderInfo(DateTime stDate,DateTime edDate)
        {
            Dictionary<string,decimal> dic=new Dictionary<string, decimal>()
            {
                {"总订单",0},
                {"Saas服务",0},
                {"短信",0},
                {"话费",0},
                {"硬件",0},
                { "总订单笔数",0},
                { "Saas服务笔数",0},
                { "短信笔数",0},
                { "话费笔数",0},
                { "硬件笔数",0}
        };

            //获取订单类型
            List<OrderInfoModel> tOrdList = T_OrderInfoBLL.GetOrderInfoByDate(stDate, edDate, 0);
            decimal tSelf = 0;
            decimal tSms = 0;
            decimal tMaterial = 0;
            decimal tPhone = 0;

            foreach (var item in tOrdList)
            {
                OrderItemProp tempModel = T_Order_businessBLL.GetListItemProps(Convert.ToInt32(item.busId), item.accId);
                if (tempModel.SelfMark == "1")
                {
                    tSelf += Convert.ToDecimal(item.RealPayMoney);
                    dic["Saas服务笔数"] += 1;
                }
                else if (tempModel.PureSms == "1")
                {
                    tSms += Convert.ToDecimal(item.RealPayMoney);
                    dic["短信笔数"] += 1;
                }
                else
                {
                    tPhone += Convert.ToDecimal(item.RealPayMoney);
                    dic["话费笔数"] += 1;
                }
            }

            //获取实物商品相关统计(实物商品计入他营产品)
            List<OrderInfoModel> MaterialList = T_OrderInfoBLL.GetOrderInfoByDate(stDate, edDate, 1);
            if (MaterialList != null && MaterialList.Count > 0)
            {
                foreach (var item in MaterialList)
                {
                    tMaterial += Convert.ToDecimal(item.RealPayMoney);
                    dic["硬件笔数"] += 1;
                }
            }

            dic["Saas服务"] = tSelf;
            dic["短信"] = tSms;
            dic["话费"] = tPhone;
            dic["硬件"] = tMaterial;
            dic["总订单"] = tSelf+ tSms+ tPhone+ tMaterial;
            dic["总订单笔数"] = dic["Saas服务笔数"] + dic["短信笔数"] + dic["话费笔数"] + dic["硬件笔数"];

            return dic;
        }

        #endregion
    }
}
