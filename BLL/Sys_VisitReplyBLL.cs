using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;
namespace BLL
{
    /// <summary>
    /// 
    /// </summary>
    public static class Sys_VisitReplyBLL
    {
        /// <summary>
        /// 得到一个回访的回复 信息
        /// </summary>
        /// <param name="vid"></param>
        /// <returns></returns>
        public static List<Sys_VisitReply> GetList(int vid)
        {
            Sys_VisitReplyDAL dal = new Sys_VisitReplyDAL();
            return dal.GetList(vid);
        }
    }
}
