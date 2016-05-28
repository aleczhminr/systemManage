using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAL;
namespace BLL
{
    /// <summary>
    /// 回访标签关系
    /// </summary>
   public static class Sys_VisitTagNexusBLL
    {
       /// <summary>
       /// 增加新的关系
       /// </summary>
       /// <param name="visitId"></param>
       /// <param name="tagId"></param>
       /// <param name="insertName"></param>
       /// <returns></returns>
       public static int Add(int visitId, int tagId, string insertName)
       {
           Sys_VisitTagNexusDAL dal = new Sys_VisitTagNexusDAL();
           return dal.Add(visitId, tagId, insertName);
       }
              /// <summary>
       /// 得到一个回访的标签
       /// </summary>
       /// <param name="vid"></param>
       /// <returns></returns>
       public static  List<SysVisitTagItem> GetVisitInfoTagList(int vid)
       {
           Sys_VisitTagNexusDAL dal = new Sys_VisitTagNexusDAL();
           return dal.GetVisitInfoTagList(vid);
       }
    }
}
