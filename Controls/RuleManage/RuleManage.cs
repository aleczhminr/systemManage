using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using Model;
using Utility;

namespace Controls.RuleManage
{
    public static class RuleManage
    {
        /// <summary>
        /// 根据规则组获取Sql语句并更新规则详情列表
        /// </summary>
        /// <param name="conditionList"></param>
        /// <param name="type">
        /// 用于标记来源是筛选器还是设定循环任务
        /// </param>
        /// <returns></returns>
        public static string GetWhereStrByCondition(List<FilterCondition> conditionList, int type, string verifId)
        {
            StringBuilder strWhere = new StringBuilder();
            //用于取交的AccId字典
            Dictionary<string, List<int>> accIdDic = new Dictionary<string, List<int>>();

            #region 更新规则详情列表
            //更新出错只记录日志
            if (RuleListBLL.AddNewRule(conditionList, type, verifId) == 0)
            {
                Logger.Info("在筛选操作中添加规则详情出错！");
            }
            #endregion

            #region 根据表名做分区
            //穷举涉及的表名
            List<string> tabNameList = new List<string>()
            {
                "i200.dbo.T_Account",
                "i200.dbo.T_OrderInfo",
                "i200.dbo.T_Business",
                "i200.dbo.T_LOG",
                "sys_i200.dbo.Sys_TagNexus",
                "sys_i200.dbo.SysRpt_ShopInfo",
                "sys_i200.dbo.SysRpt_ShopDayInfo"   //添加每日店铺汇总
            };

            foreach (var str in tabNameList)
            {
                List<FilterCondition> tempConditionList = conditionList.FindAll(x => x.TableName == str);

                if (tempConditionList != null && tempConditionList.Count > 0)
                {
                    accIdDic[str] = new List<int>();
                    StringBuilder strTemp = new StringBuilder();

                    //每日汇总表的特殊处理
                    StringBuilder strGroupBy = new StringBuilder();
                    strGroupBy.Append(" group by accountid,");

                    StringBuilder strHaving = new StringBuilder();
                    strHaving.Append(" having ");

                    switch (str)
                    {
                        case "i200.dbo.T_Account":
                            strTemp.Append("select ID from " + str + " where state=1 and ");
                            break;
                        case "i200.dbo.T_OrderInfo":
                            strTemp.Append("select accId from " + str + " where (orderstatus=2 or (orderstatus=1 and orderTypeId=2)) and ");
                            break;
                        case "i200.dbo.T_Business":
                        case "sys_i200.dbo.SysRpt_ShopInfo":
                            strTemp.Append("select isnull(accountid,0) accountid from " + str + " where ");
                            break;
                        case "sys_i200.dbo.Sys_TagNexus":
                            strTemp.Append("select acc_id from " + str + " where ");
                            break;
                        case "i200.dbo.T_LOG":
                            strTemp.Append("select accountid from i200.dbo.T_Log lt left join i200.dbo.T_Token_Api ta on lt.accountid=ta.accId where ");
                            break;
                        case "sys_i200.dbo.SysRpt_ShopDayInfo":
                            strTemp.Append("select accountid from " + str + " where ");
                            break;
                        default:
                            break;
                    }

                    //strTemp.Append(" and ");
                    //针对单表拼装SQL
                    #region 拼接SQL语句

                    foreach (var conditionItem in tempConditionList)
                    {
                        switch (conditionItem.ConditionType)
                        {
                            case "StrPair":
                            case "TimePair":
                                if (!string.IsNullOrEmpty(conditionItem.DataRange.Min.ToString()) && conditionItem.DataRange.Min.ToString() != "null")
                                {
                                    strTemp.Append(" (" + conditionItem.ColName + " >= '" +
                                               conditionItem.DataRange.Min + "') and ");
                                }
                                if (!string.IsNullOrEmpty(conditionItem.DataRange.Max.ToString()) && conditionItem.DataRange.Max.ToString() != "null")
                                {
                                    if (conditionItem.ConditionType == "TimePair")
                                    {
                                        DateTime dt = Convert.ToDateTime(conditionItem.DataRange.Max);

                                        strTemp.Append(" (" + conditionItem.ColName + " <= '" + dt.AddDays(1) +
                                                   "') and ");
                                    }
                                    else
                                    {
                                        strTemp.Append(" (" + conditionItem.ColName + " <= '" + conditionItem.DataRange.Max +
                                                   "') and ");
                                    }

                                }
                                //strTemp.Append(" (" + conditionItem.ColName + " between '" +
                                //               conditionItem.DataRange.Min + "' and '" + conditionItem.DataRange.Max +
                                //               "') and ");
                                break;
                            case "IntPair":
                                if (conditionItem.ColName.Contains("sum(")) //包含数据的处理
                                {
                                    string colName = conditionItem.ColName.Substring(
                                        conditionItem.ColName.IndexOf('(') + 1, conditionItem.ColName.IndexOf(')') - conditionItem.ColName.IndexOf('(') - 1);

                                    strGroupBy.Append(colName + ", ");

                                    if (conditionItem.DataRange.Min.ToString() != "null")
                                    {
                                        strHaving.Append(" (" + conditionItem.ColName + " >= " +
                                                   Convert.ToInt32(conditionItem.DataRange.Min) +
                                                   ") and ");

                                    }
                                    if (conditionItem.DataRange.Max.ToString() != "null")
                                    {
                                        strHaving.Append(" (" + conditionItem.ColName + " <= " +
                                                   Convert.ToInt32(conditionItem.DataRange.Max) +
                                                   ") and ");

                                    }
                                }
                                else
                                {
                                    if (conditionItem.DataRange.Min.ToString() != "null")
                                    {
                                        strTemp.Append(" (" + conditionItem.ColName + " >= " +
                                                   Convert.ToInt32(conditionItem.DataRange.Min) +
                                                   ") and ");

                                    }
                                    if (conditionItem.DataRange.Max.ToString() != "null")
                                    {
                                        strTemp.Append(" (" + conditionItem.ColName + " <= " +
                                                   Convert.ToInt32(conditionItem.DataRange.Max) +
                                                   ") and ");

                                    }
                                    //特殊条件组合下的字段处理
                                    switch (conditionItem.ColName)
                                    {
                                        //连续天数附加LoginType条件
                                        case "ContinuousDay":
                                            strTemp.Append(" loginType=1 and ");
                                            break;
                                    }
                                    //strTemp.Append(" (" + conditionItem.ColName + " between " +
                                    //               Convert.ToInt32(conditionItem.DataRange.Max) + " and " +
                                    //               Convert.ToInt32(conditionItem.DataRange.Min) +
                                    //               ") and ");
                                }
                                break;
                            case "IntRange":
                                string intList = "";

                                //前台登录库记录方式的不统一，导致登录相关的问题需要分端处理
                                //构建分析库、或者前台重构
                                //处理移动端分端登录的相关信息(移动端分端需要重构)
                                if (conditionItem.ColName == "LogMode")
                                {
                                    string mobileStr = "";

                                    foreach (var intItem in conditionItem.DataRange.Range)
                                    {
                                        int label = Convert.ToInt32(intItem);
                                        switch (label)
                                        {
                                            case 1:
                                            case 0:
                                            case 3:
                                                intList += intItem + ",";
                                                break;
                                            //移动端登录的特殊处理
                                            case 4:
                                                mobileStr += " ta.AppKey='iPhoneHT5I0O4HDN65' or ";
                                                intList += "4,";
                                                break;
                                            case 5:
                                                mobileStr += " ta.AppKey='iPadMaO8VUvVH0eBss' or ";
                                                intList += "4,";
                                                break;
                                            case 6:
                                                mobileStr += " ta.AppKey='AndroidYnHWyROQosO' or ";
                                                intList += "4,";
                                                break;
                                            default:
                                                break;
                                        }

                                    }

                                    if (!string.IsNullOrEmpty(intList))
                                    {
                                        intList = intList.Substring(0, intList.LastIndexOf(','));

                                        strTemp.Append(" (lt.LogMode in (" + intList + ")) and ");
                                    }
                                    if (!string.IsNullOrEmpty(mobileStr))
                                    {
                                        mobileStr = mobileStr.Substring(0, mobileStr.LastIndexOf('o'));

                                        strTemp.Append(" (" + mobileStr + ") and ");
                                    }

                                }
                                //只在某端登录的逻辑
                                //else if (conditionItem.ColName == "LogModeAlone")
                                //{

                                //}
                                else
                                {
                                    foreach (var intItem in conditionItem.DataRange.Range)
                                    {
                                        intList += intItem + ",";
                                    }
                                    intList = intList.Substring(0, intList.LastIndexOf(','));

                                    strTemp.Append(" (" + conditionItem.ColName + " in (" + intList + ")) and ");
                                }

                                break;
                            case "StrRange":
                                string strList = "";
                                foreach (var intItem in conditionItem.DataRange.Range)
                                {
                                    strList += "'" + intItem + "',";
                                }
                                intList = strList.Substring(0, strList.LastIndexOf(','));

                                strTemp.Append(" (" + conditionItem.ColName + " in (" + intList + ")) and ");
                                break;
                            default:
                                break;
                        }

                    }
                    #endregion

                    string sql = "";

                    if (str == "i200.dbo.T_LOG")
                    {
                        sql = strTemp.ToString().Substring(0, strTemp.ToString().LastIndexOf('a')) +
                              " group by accountid";
                    }
                    else if (str == "sys_i200.dbo.SysRpt_ShopDayInfo") //如果是每日店铺汇总表则组合三段Sql
                    {
                        sql = strTemp.ToString().Substring(0, strTemp.ToString().LastIndexOf('a')) +
                              strGroupBy.ToString().Substring(0, strGroupBy.ToString().LastIndexOf(',')) +
                              strHaving.ToString().Substring(0, strHaving.ToString().LastIndexOf('a'));
                    }
                    else
                    {
                        sql = strTemp.ToString().Substring(0, strTemp.ToString().LastIndexOf('a'));
                    }

                    accIdDic[str] = RuleListBLL.GetAccIdByStr(sql);

                    //判断是否存在时间类型
                    if (sql.IndexOf('/') > 0)
                    {
                        RuleListBLL.AddSqlRecord(sql, verifId, 1, tempConditionList[0].ConditionGroup);
                    }
                    else
                    {
                        RuleListBLL.AddSqlRecord(sql, verifId, 0, tempConditionList[0].ConditionGroup);
                    }


                }

            }
            #endregion

            #region 特殊自定义类、由于库不统一导致的独立条件的单独处理模块            
            //建立包含的店铺集合
            if (conditionList.Exists(x => x.ColName == "specificAccId"))
            {
                string jsonList = conditionList.Find(x => x.ColName == "specificAccId").DataRange.Range.ToString();

                List<int> listSpec =
                    CommonLib.Helper.JsonDeserializeObject<List<int>>(jsonList);
                accIdDic["specific"] = listSpec;
            }

            //建立排除的店铺集合
            if (conditionList.Exists(x => x.ColName == "specificEx"))
            {
                string jsonList = conditionList.Find(x => x.ColName == "specificEx").DataRange.Range.ToString();

                List<int> listSpec =
                    CommonLib.Helper.JsonDeserializeObject<List<int>>(jsonList);
                accIdDic["specificEx"] = listSpec;
            }

            //根据地区筛选
            if (conditionList.Exists(x => x.ColName == "areaQuery"))
            {
                string jsonList = conditionList.Find(x => x.ColName == "areaQuery").DataRange.Range.ToString();

                List<string> listSpec =
                    CommonLib.Helper.JsonDeserializeObject<List<string>>(jsonList);

                StringBuilder strArea = new StringBuilder();
                strArea.Append("select accid from [Sys_I200].[dbo].[Sys_Account] where 1=1 and ");


                string areaCondition = "";
                foreach (var item in listSpec)
                {
                    areaCondition += " sysAddress like '%" + item + "%' or ";
                }
                areaCondition = areaCondition.Substring(0, areaCondition.LastIndexOf('o'));
                strArea.Append(areaCondition);

                try
                {
                    accIdDic["areaQuery"] = RuleListBLL.GetAccIdByStr(strArea.ToString());
                    RuleListBLL.AddSqlRecord(strArea.ToString(), verifId, 0, 7);
                }
                catch (Exception ex)
                {
                    Logger.Error("获取用户地区筛选数据出错！", ex);
                    accIdDic["areaQuery"] = null;
                }
            }


            //只在移动端、PC端或者Web端登录的用户
            //属于需要添加到
            if (conditionList.Exists(x => x.ColName == "LogModeAlone"))
            {
                FilterCondition conditionModel = conditionList.Find(x => x.ColName == "LogModeAlone");
                string data = conditionList.Find(x => x.ColName == "LogModeAlone").DataRange.Range.ToString();
                List<int> listSpec =
                    CommonLib.Helper.JsonDeserializeObject<List<int>>(data);

                StringBuilder strSpecSql = new StringBuilder();
                //获取所有登录用户和分组
                strSpecSql.Append(
                    "select Accountid,LogMode into #logMode from i200.dbo.T_LOG group by Accountid,LogMode ;");

                strSpecSql.Append("select Accountid from #logMode ");

                switch (listSpec[0])
                {
                    case 4://移动端
                        strSpecSql.Append(
                            " where LogMode=4 and Accountid not in (select Accountid from #logMode " +
                            " where left(LogMode,1) in ('0','1','3')) group by Accountid ;");
                        break;
                    case 0://网页端
                        strSpecSql.Append(
                            " where left(LogMode,1) in ('0','1') and Accountid not in (select Accountid from #logMode " +
                            " where left(LogMode,1) in ('4','3')) group by Accountid ;");
                        break;
                    case 3://PC端
                        strSpecSql.Append(
                        " where left(LogMode,1) in ('3') and Accountid not in (select Accountid from #logMode " +
                        " where left(LogMode,1) in ('4','0','1')) group by Accountid ;");
                        break;
                }

                strSpecSql.Append("drop table #logMode;");

                //获取用户集
                accIdDic["LogModeAlone"] = RuleListBLL.GetAccIdByStr(strSpecSql.ToString());
                RuleListBLL.AddSqlRecord(strSpecSql.ToString(), verifId, 0, conditionModel.ConditionGroup);

            }

            //获取用户历史活跃状态
            if (conditionList.Exists(x => x.ColName == "activeEver"))
            {
                string jsonList = conditionList.Find(x => x.ColName == "activeEver").DataRange.Range.ToString();
                List<int> listSpec =
                    CommonLib.Helper.JsonDeserializeObject<List<int>>(jsonList);

                StringBuilder strEverActive = new StringBuilder();

                //拼接选择条件
                string where = "(";
                foreach (var intItem in listSpec)
                {
                    where += intItem + ",";
                }
                where = where.Substring(0, where.LastIndexOf(',')) + ") ";

                strEverActive.Append("select accid from Sys_I200.dbo.SysRpt_ShopActive where active in " + where +
                                     " and stateVal=1 group by accid");


                accIdDic["activeEver"] = RuleListBLL.GetAccIdByStr(strEverActive.ToString());
                RuleListBLL.AddSqlRecord(strEverActive.ToString(), verifId, 0, 7);

            }

            //获取用户行业
            if (conditionList.Exists(x => x.ColName == "industry"))
            {
                string jsonList = conditionList.Find(x => x.ColName == "industry").DataRange.Range.ToString();
                List<int> listSpec =
                    CommonLib.Helper.JsonDeserializeObject<List<int>>(jsonList);

                StringBuilder strIndustry = new StringBuilder();

                string strList = "";
                foreach (var intItem in listSpec)
                {
                    strList += intItem + ",";
                }
                strList = strList.Substring(0, strList.LastIndexOf(','));

                strIndustry.Append("select accid,Eindustry_1 m1,Eindustry_2 m2,Sindustry_1 s1,Sindustry_2 s2,0 industryId into #list from sys_i200.dbo.SysStat_IndustryFilter;");
                strIndustry.Append("update #list set s1=m1 where m1 is not null;  ");
                strIndustry.Append("update #list set s2=m2 where m2 is not null;   ");
                strIndustry.Append("update #list set #list.industryId=i.IndustryId from sys_i200.dbo.SysStat_IndustryFilterDic i  ");
                strIndustry.Append("where #list.s1=i.Industry_1 and #list.s2=i.[Industry_2];");

                strIndustry.Append("select accid from #list left join sys_i200.dbo.SysStat_IndustryFilterDic i " +
                                   "on #list.industryId=i.IndustryId where i.IndustryId in (" + strList + ");");
                strIndustry.Append("drop table #list;");

                accIdDic["industry"] = RuleListBLL.GetAccIdByStr(strIndustry.ToString());
                //暂时屏蔽记录插入
                //RuleListBLL.AddSqlRecord(strIndustry.ToString(), verifId, 0, 7);

            }
            #endregion

            #region 处理产生的集合并合并

            if (accIdDic.Count <= 0)
            {
                return "";
            }
            else
            {
                List<int> temp = accIdDic.First().Value;
                foreach (var item in accIdDic)
                {
                    if (item.Key != "specificEx" && item.Key != null)
                    {
                        temp = temp.Intersect(item.Value).ToList();
                    }
                }

                if (accIdDic.ContainsKey("specificEx"))
                {
                    temp = temp.Except(accIdDic["specificEx"]).ToList();
                }

                string where = " and accountid in (";

                foreach (var intItem in temp)
                {
                    where += intItem + ",";
                }

                if (where.LastIndexOf(',') <= 0)
                {
                    return "";
                }
                else
                {
                    where = where.Substring(0, where.LastIndexOf(',')) + ") ";

                    return where;
                }

            }
            #endregion

        }

        /// <summary>
        /// 返回规则列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public static List<FilterCondition> GetRuleConditionList(string strWhere)
        {
            return RuleListBLL.GetRuleConditionList(strWhere);
        }

        /// <summary>
        /// 获取所选范围值得枚举项
        /// </summary>
        /// <param name="colName"></param>
        /// <returns></returns>
        public static string GetEnumItem(string colName)
        {
            List<RangePair> list = new List<RangePair>();

            switch (colName)
            {
                #region 返回通用枚举结构
                case "Remark":
                    list.Add(new RangePair(10, "iPhone"));
                    list.Add(new RangePair(11, "Android"));
                    list.Add(new RangePair(8, "PC"));
                    list.Add(new RangePair(12, "MobileWeb"));
                    list.Add(new RangePair(13, "iPad"));
                    list.Add(new RangePair(0, "Web"));
                    return CommonLib.Helper.JsonSerializeObject(list);
                case "active":
                    list.Add(new RangePair(0, "新注册"));
                    list.Add(new RangePair(3, "需关怀"));
                    list.Add(new RangePair(5, "活跃"));
                    list.Add(new RangePair(7, "忠诚"));
                    list.Add(new RangePair(-1, "休眠"));
                    list.Add(new RangePair(-3, "流失"));
                    return CommonLib.Helper.JsonSerializeObject(list);
                case "aotjb":
                    list.Add(new RangePair(1, "标准版"));
                    list.Add(new RangePair(3, "高级版"));
                    return CommonLib.Helper.JsonSerializeObject(list);
                case "LogMode":
                    list.Add(new RangePair(0, "网页密码登录"));
                    list.Add(new RangePair(1, "网页Token登录"));
                    list.Add(new RangePair(3, "PC客户端"));
                    list.Add(new RangePair(4, "iPhone"));
                    list.Add(new RangePair(5, "iPad"));
                    list.Add(new RangePair(6, "Android"));
                    return CommonLib.Helper.JsonSerializeObject(list);
                case "LogModeAlone":
                    list.Add(new RangePair(0, "网页登录"));
                    list.Add(new RangePair(3, "客户端登录"));
                    list.Add(new RangePair(4, "移动端登录"));
                    return CommonLib.Helper.JsonSerializeObject(list);
                case "activeEver":
                    //list.Add(new RangePair(0,"新注册"));
                    list.Add(new RangePair(3, "需关怀"));
                    list.Add(new RangePair(5, "活跃"));
                    list.Add(new RangePair(7, "忠诚"));
                    list.Add(new RangePair(-1, "休眠"));
                    list.Add(new RangePair(-3, "流失"));
                    return CommonLib.Helper.JsonSerializeObject(list);
                #endregion
                default:
                    return RuleListBLL.GetRangeItem(colName);
            }

            return "";
        }
    }
}
