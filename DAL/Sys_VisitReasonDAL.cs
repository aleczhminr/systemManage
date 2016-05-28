using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// 回访分类
    /// </summary>
    public class Sys_VisitReasonDAL : Base.Sys_VisitReasonBaseDAL
    {

        /// <summary>
        /// 得到列表
        /// </summary>
        /// <param name="vrId"></param>
        /// <returns></returns>
        public List<Sys_VisitReason> GetList(int vrMaxId)
        {
            StringBuilder strSql = new StringBuilder();
            var strWhere = new StringBuilder();

            if (vrMaxId >= 0)
            {
                strWhere.Append(" vr_id=@vr_id");
            }
            else
            {
                strWhere.Append(" vr_rank=@vr_id");
            }

            strSql.Append("select * from Sys_VisitReason where vr_status=1 and ");
            strSql.Append(strWhere.ToString());

            return DapperHelper.Query<Sys_VisitReason>(strSql.ToString(), new { vr_id = Math.Abs(vrMaxId) }).ToList();
        }

        /// <summary>
        /// 根据ID得到列表
        /// </summary>
        /// <param name="vrid"></param>
        /// <returns></returns>
        public List<Sys_VisitReason> GetList(int[] vrid)
        {
            string vrids = "";
            foreach (int itemId in vrid)
            {
                if (vrids.Length>0)
                {
                    vrids += ",";
                }
                vrids += itemId.ToString();
            }

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Sys_VisitReason where vr_status=1 and id in(" + vrids.ToString() + ")");
            return DapperHelper.Query<Sys_VisitReason>(strSql.ToString()).ToList();
        }



    }
}
