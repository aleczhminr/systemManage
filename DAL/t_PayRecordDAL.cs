using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
namespace DAL
{
    /// <summary>
    /// 支出信息
    /// </summary>
    public class t_PayRecordDAL : Base.t_PayRecordBaseDAL
    {
        /// <summary>
        /// 得到基本支出列表
        /// </summary>
        /// <param name="pageIndex">当前页面</param>
        /// <param name="pageSize">每页面显示数</param>
        /// <param name="sqlWhere">条件</param>
        /// <param name="filedOrder">排序</param>
        /// <returns></returns>
        public List<t_PayRecordBasic> GetBasicList(int pageIndex, int pageSize, List<DapperWhere> sqlWhere, string filedOrder)
        {
            string columnName = "ID,PayMaxType,PayMinType,PayName,PaySum,PayDate";
            return GetList<t_PayRecordBasic>(pageIndex, pageSize, columnName, sqlWhere, filedOrder);
        }

    }
}
