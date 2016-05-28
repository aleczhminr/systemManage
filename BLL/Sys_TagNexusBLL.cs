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
    /// 店铺标签关系
    /// </summary>
    public static class Sys_TagNexusBLL
    {
        /// <summary>
       /// 根据店铺ID 得到标签信息
       /// </summary>
       /// <param name="accid"></param>
       /// <returns></returns>
        public static List<Sys_TagInfoBasic> GetTagNexusByAccId(int accid)
        {
            Sys_TagNexusDAL dal = new Sys_TagNexusDAL();
            return dal.GetTagNexusByAccId(accid);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(Sys_TagNexus model)
        {
            Sys_TagNexusDAL dal = new Sys_TagNexusDAL();
            return dal.Add(model);

        }
        /// <summary>
        /// 移除标签
        /// </summary>
        /// <param name="accid"></param>
        /// <param name="tagid"></param>
        /// <returns></returns>
        public static bool RemoveTag(int accid, int tagid)
        {
            Sys_TagNexusDAL dal = new Sys_TagNexusDAL();
            return dal.RemoveTag(accid, tagid);
        }
    }
}
