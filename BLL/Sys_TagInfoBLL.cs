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
    /// 标签列表
    /// </summary>
    public static class Sys_TagInfoBLL
    {
        /// <summary>
        /// 得到总列表
        /// </summary>
        /// <returns></returns>
        public static List<Sys_TagInfoBasic> GetAllList()
        {
            Sys_TagInfoDAL dal = new Sys_TagInfoDAL();
            return dal.GetAllList();
        }

        /// <summary>
        /// 增加标签
        /// </summary>
        /// <param name="tagName"></param>
        /// <param name="insertName"></param>
        /// <returns></returns>
        public static int Add(string tagName, string insertName)
        {
            Sys_TagInfoDAL dal = new Sys_TagInfoDAL();
            return dal.Add(tagName, insertName);
        }
        /// <summary>
        /// 得到标签分类问题
        /// </summary>
        /// <returns></returns>
        public static List<SysTagTypeBasic> GetTagQuestions(int accid, int[] excludTypeList)
        {
            Sys_TagInfoDAL dal = new Sys_TagInfoDAL();
            return dal.GetTagQuestions(accid, excludTypeList);
        }

        public static Dictionary<string, string> GetTagInfoByCondition(int pageIndex, int tagType, string insertName = "", string tagName = "")
        {
            Sys_TagInfoDAL dal = new Sys_TagInfoDAL();
            return dal.GetTagByCondition(pageIndex, tagType, insertName, tagName);
        }

        public static Sys_TagInfoBasic GetSingleModel(int id)
        {
            Sys_TagInfoDAL dal = new Sys_TagInfoDAL();
            return dal.GetSingleModel(id);
        }

        public static string ModifyModel(int id, string tagName, string tagTypeId, string tagTypeName, int tagStatus)
        {
            Sys_TagInfoDAL dal = new Sys_TagInfoDAL();
            int reVal = dal.ModifyModel(id, tagName, tagTypeId, tagTypeName, tagStatus);

            return (reVal > 0 ? "1" : "0");
        }

        public static string TagImport()
        {
            Sys_TagInfoDAL dal = new Sys_TagInfoDAL();
            return dal.TagTransfer();
        }

    }
}
