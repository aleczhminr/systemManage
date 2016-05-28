using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
namespace DAL
{
    /// <summary>
    /// 回访回复信息
    /// </summary>
    public class Sys_VisitReplyDAL : Base.Sys_VisitReplyBaseDAL
    {
        /// <summary>
        /// 得到一个回访的回复 信息
        /// </summary>
        /// <param name="vid"></param>
        /// <returns></returns>
        public List<Sys_VisitReply> GetList(int vid)
        {
            List<DapperWhere> sqlWhere = new List<DapperWhere>();
            sqlWhere.Add(new DapperWhere("vi_id", vid));
            var list = GetList(0, sqlWhere, " id asc");
            return list;
        }
    }
}
