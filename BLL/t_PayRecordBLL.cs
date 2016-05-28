using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BLL
{
    /// <summary>
    /// 支出信息
    /// </summary>
    public static class t_PayRecordBLL
    {

        /// <summary>
        /// 得到基本支出列表
        /// </summary>
        /// <param name="pageIndex">当前页面</param>
        /// <param name="pageSize">每页面显示数</param>
        /// <param name="sqlWhere">条件</param>
        /// <param name="filedOrder">排序</param>
        /// <returns></returns>
        public static List<t_PayRecordBasic> GetBasicList(int pageIndex, int pageSize, List<DapperWhere> sqlWhere, string filedOrder)
        {
            t_PayRecordDAL dal = new t_PayRecordDAL();
            return dal.GetBasicList(pageIndex, pageSize, sqlWhere, filedOrder);
        }
    }
}
