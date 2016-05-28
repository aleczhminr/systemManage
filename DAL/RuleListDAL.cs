using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Utility;

namespace DAL
{
    public class RuleListDAL
    {
        /// <summary>
        /// 获取规则列表
        /// </summary>
        /// <returns></returns>
        public List<FilterCondition> GetRuleConditionList(string whereStr)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select * from Sys_RuleDetail ");
            if (!string.IsNullOrEmpty(whereStr))
            {
                strSql.Append(whereStr);
            }

            try
            {
                return DapperHelper.Query<FilterCondition>(strSql.ToString()).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error("获取规则列表出错！", ex);
                return null;
            }
        }

        /// <summary>
        /// 更新规则详情列表
        /// </summary>
        /// <param name="list"></param>
        /// <param name="type"></param>
        /// <param name="verifId"></param>
        /// <returns></returns>
        public int AddNewRule(List<FilterCondition> list, int type, string verifId)
        {
            StringBuilder strSql = new StringBuilder();

            if (list != null && list.Count > 0)
            {
                foreach (var conItem in list)
                {
                    switch (conItem.ConditionType)
                    {

                        case "TimePair":
                            if (string.IsNullOrEmpty(conItem.DataRange.Max.ToString()) || conItem.DataRange.Max.ToString() == "null")
                            {
                                conItem.DataRange.Max = "null";
                            }
                            else
                            {
                                conItem.DataRange.Max = Convert.ToDateTime(conItem.DataRange.Max);
                            }
                            if (string.IsNullOrEmpty(conItem.DataRange.Min.ToString()) || conItem.DataRange.Min.ToString() == "null")
                            {
                                conItem.DataRange.Min = "null";
                            }
                            else
                            {
                                conItem.DataRange.Min = Convert.ToDateTime(conItem.DataRange.Min);
                            }
                            strSql.Append(
                                "insert into Sys_RuleList (VerifId,RuleId,MaxValue,MinValue,RangeData,Remark) " +
                                "Values ('" + verifId + "'," + conItem.Id + ",'" +
                                conItem.DataRange.Max + "','" +
                                conItem.DataRange.Min +
                                "',null,'" + conItem.Remark + "');");
                            break;
                        case "StrPair":
                        case "IntPair":
                            if (string.IsNullOrEmpty(conItem.DataRange.Max.ToString()))
                            {
                                conItem.DataRange.Max = "null";
                            }
                            if (string.IsNullOrEmpty(conItem.DataRange.Min.ToString()))
                            {
                                conItem.DataRange.Min = "null";
                            }
                            strSql.Append("insert into Sys_RuleList (VerifId,RuleId,MaxValue,MinValue,RangeData,Remark) " +
                                          "Values ('" + verifId + "'," + conItem.Id + "," +
                                          conItem.DataRange.Max.ToString() + "," + conItem.DataRange.Min.ToString() +
                                          ",null,'" + conItem.Remark + "');");
                            break;
                        case "IntRange":
                        case "StrRange":
                        case "SpecRange":
                            string intList = "";
                            foreach (var intItem in conItem.DataRange.Range)
                            {
                                intList += intItem + ",";
                            }
                            intList = intList.Substring(0, intList.LastIndexOf(','));

                            strSql.Append("insert into Sys_RuleList (VerifId,RuleId,MaxValue,MinValue,RangeData,Remark) " +
                                         "Values ('" + verifId + "'," + conItem.Id + ",null,null,'" + intList + "','" + conItem.Remark + "');");
                            break;
                        default:
                            break;
                    }
                }

                try
                {
                    return DapperHelper.Execute(strSql.ToString());
                }
                catch (Exception ex)
                {
                    Logger.Error("插入规则详情表出错！", ex);
                    return 0;
                }
            }

            else
            {
                return 0;
            }
        }


        /// <summary>
        /// 用于筛选时返回Accid
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public List<int> GetAccIdByStr(string str)
        {
            try
            {
                return DapperHelper.Query<int>(str).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error("在筛选时获取Accid错误！", ex);
                return null;
            }

        }

        #region 获取范围值枚举项的方法组

        public string GetRangeItem(string colName)
        {
            StringBuilder strSql = new StringBuilder();

            switch (colName)
            {
                case "AgentId":
                    strSql.Append("select ID id,AgentName t_Name,AgentGroup extInfo from Sys_agent_mess;");
                    break;
                case "busId":
                    strSql.Append("select busId id,displayName t_Name from i200.dbo.T_Order_Project;");
                    break;
                case "tag_id":
                    strSql.Append("select id,t_Name from Sys_TagInfo where tagStatus=1;");
                    break;
                case "industry":
                    strSql.Append("select IndustryId id,Industry_2 t_Name,Industry_1 extInfo from SysStat_IndustryFilterDic;");
                    break;
                default:
                    Logger.Info("非法的传入值");
                    return "";
            }

            try
            {
                List<RangePair> list = DapperHelper.Query<RangePair>(strSql.ToString()).ToList();

                //行业其它的例外处理
                if (colName== "industry")
                {
                    foreach (var item in list)
                    {
                        if (item.t_Name=="其它")
                        {
                            item.t_Name += "(" + item.extInfo + ")";
                        }
                    }
                }

                //订单产品的例外处理
                if (colName == "busId")
                {
                    list.Remove(list.Find(x => x.id == 52));
                }

                return CommonLib.Helper.JsonSerializeObject(list);
            }
            catch (Exception ex)
            {
                Logger.Error("筛选器获取枚举项出错！", ex);
                return "";
            }

        }

        /// <summary>
        /// 记录规则相关Sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="verif"></param>
        /// <param name="conditionGroup"></param>
        /// <returns></returns>
        public int AddSqlRecord(string sql, string verif, int timeMark, int group)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("insert into Sys_RuleSql (Verification,TimeMark,SqlStr,SendingMark,ConditionGroup) " +
                          "Values (@verif,@timeMark,@sql,0,@group);");

            try
            {
                return DapperHelper.Execute(strSql.ToString(), new
                {
                    verif = verif,
                    timeMark = timeMark,
                    sql = sql,
                    group = group
                });
            }
            catch (Exception ex)
            {
                Logger.Error("记录筛选器规则关联SQL出错！", ex);
                return 0;
            }
        }

        #endregion

        /// <summary>
        ///  根据规则Id获取对应规则的表名
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetTableNameById(int id)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select TableName from Sys_RuleDetail where Id=@id;");

            try
            {
                return DapperHelper.ExecuteScalar<string>(strSql.ToString(), new
                {
                    id = id
                });
            }
            catch (Exception ex)
            {
                Logger.Error("根据规则Id获取表名出错！", ex);
                return "";
            }

        }

        /// <summary>
        /// 根据筛选器标识返回原始条件中不带有时间标识的Sql语句
        /// </summary>
        /// <param name="verif"></param>
        /// <returns></returns>
        public List<string> GetOriginSql(string verif)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select SqlStr from Sys_RuleSql where TimeMark=0 and Verification=@verif;");

            try
            {
                return DapperHelper.Query<string>(strSql.ToString(), new
                {
                    verif = verif
                }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error("获取筛选器生成的原始条件组出错！", ex);
                return null;
            }
        }

    }
}
