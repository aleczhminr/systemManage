using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;

namespace BLL
{
    public static class RuleListBLL
    {
        /// <summary>
        /// 添加新规则详情
        /// </summary>
        /// <param name="list"></param>
        /// <param name="type"></param>
        /// <param name="verifId"></param>
        /// <returns></returns>
        public static int AddNewRule(List<FilterCondition> list, int type, string verifId)
        {
            RuleListDAL dal = new RuleListDAL();
            return dal.AddNewRule(list, type, verifId);
        }

        /// <summary>
        /// 用于获取筛选时的Accid集合
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static List<int> GetAccIdByStr(string str)
        {
            RuleListDAL dal = new RuleListDAL();
            return dal.GetAccIdByStr(str);
        }

        /// <summary>
        /// 获取规则条件列表
        /// </summary>
        /// <param name="whereStr"></param>
        /// <returns></returns>
        public static List<FilterCondition> GetRuleConditionList(string whereStr)
        {
            RuleListDAL dal = new RuleListDAL();
            return dal.GetRuleConditionList(whereStr);
        }

        /// <summary>
        /// 获取筛选器针对范围的枚举项
        /// </summary>
        /// <param name="colName"></param>
        /// <returns></returns>
        public static string GetRangeItem(string colName)
        {
            RuleListDAL dal = new RuleListDAL();
            return dal.GetRangeItem(colName);
        }

        public static int AddSqlRecord(string sql, string verif, int timeMark,int group)
        {
            RuleListDAL dal = new RuleListDAL();
            return dal.AddSqlRecord(sql, verif, timeMark, group);
        }

        /// <summary>
        /// 根据规则Id获取规则对应的表名
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetTableNameById(int id)
        {
            RuleListDAL dal = new RuleListDAL();
            return dal.GetTableNameById(id);
        }

        /// <summary>
        /// 根据筛选器标识返回原始条件中没有时间条件的Sql组
        /// </summary>
        /// <param name="verif"></param>
        /// <returns></returns>
        public static List<string> GetOriginSql(string verif)
        {
            RuleListDAL dal = new RuleListDAL();
            return dal.GetOriginSql(verif);
        }
    }
}
