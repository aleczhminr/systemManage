using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DAL
{
    public class Sys_DailyCheckDAL
    {
        /// <summary>
        /// 新增加一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddTask(Sys_DailyCheck model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Sys_DailyCheck(");
            strSql.Append("TaskStatus,TaskName,IsRepeat,RepeatType,RepeatTime,RemindTime,Reminder,Recorder,RecordTime");
            strSql.Append(") values (");
            strSql.Append("@TaskStatus,@TaskName,@IsRepeat,@RepeatType,@RepeatTime,@RemindTime,@Reminder,@Recorder,@RecordTime");
            strSql.Append(");select @@IDENTITY; ");

            object obj = DapperHelper.ExecuteScalar(strSql.ToString(), model);
            if (obj != null)
            {
                return Convert.ToInt32(obj);
            }
            else
            {
                return 0;
            }
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="top">前几行</param>
        /// <param name="columnName">需要获取的列名<para>为了方便不在处理，列与列用逗号分开，参照SQL写法</para></param>
        /// <param name="dapperWheres">查询条件列表</param>
        /// <param name="filedOrder">排序</param>
        public List<T> GetList<T>(int top, string columnName, List<DapperWhere> dapperWheres, string filedOrder)
        {

            StringBuilder strSql = new StringBuilder();

            string where = "";
            Dictionary<string, object> parm = new Dictionary<string, object>();
            foreach (DapperWhere item in dapperWheres)
            {
                if (where.Length > 0)
                {
                    where += " and ";
                }
                where += item.Where;
                parm[item.ColumnName] = item.Value;
            }

            strSql.Append("select ");

            if (top > 0)
            {
                strSql.Append(" top " + top.ToString() + " ");
            }
            strSql.Append(" " + columnName + " ");
            strSql.Append(" FROM Sys_DailyCheck ");
            if (where.Length > 0)
            {
                strSql.Append(" where " + where);
            }

            if (filedOrder.Length > 0)
            {
                strSql.Append(" order by " + filedOrder);
            }
            strSql.Append(";");
            return DapperHelper.Query<T>(strSql.ToString(), parm).ToList();
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Sys_DailyCheck set TaskStatus=1 where Id=@id");
            int re = DapperHelper.Execute(strSql.ToString(), new { id = id });
            if (re > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
