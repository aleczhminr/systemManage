using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;

namespace BLL
{
    public static class RequirementManageBLL
    {
        /// <summary>
        /// 新增需求
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int AddNewModel(RequirementManage model)
        {
            RequirementManageDAL dal = new RequirementManageDAL();
            return dal.AddNewModel(model);
        }
        
        /// <summary>
        /// 获取需求分类列表
        /// </summary>
        /// <returns></returns>
        public static List<RequirementCategory> GetCateList()
        {
            RequirementManageDAL dal = new RequirementManageDAL();
            return dal.GetCateList();
        }

        /// <summary>
        /// 新增需求分类
        /// </summary>
        /// <param name="cateName"></param>
        /// <returns></returns>
        public static int AddNewCategoryItem(string cateName)
        {
            RequirementManageDAL dal = new RequirementManageDAL();
            return dal.AddNewCategoryItem(cateName);
        }

        /// <summary>
        /// 获取需求列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="stDate"></param>
        /// <param name="edDate"></param>
        /// <param name="reqType"></param>
        /// <param name="status"></param>
        /// <param name="content"></param>
        /// <param name="terminal"></param>
        /// <returns></returns>
        public static Dictionary<string, object> GetRequireList(int pageIndex, string stDate, string edDate, int reqType,
            int status, string content, int terminal)
        {
            RequirementManageDAL dal = new RequirementManageDAL();
            return dal.GetRequireList(pageIndex, stDate, edDate, reqType, status, content, terminal);
        }

        /// <summary>
        /// 通过类别Id获取类别名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetCateNameById(int id)
        {
            RequirementManageDAL dal = new RequirementManageDAL();
            return dal.GetCateNameById(id);
        }

        /// <summary>
        /// 变更任务状态 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string ChangeTaskStatus(int id, int type, string desc)
        {
            RequirementManageDAL dal = new RequirementManageDAL();
            return dal.ChangeTaskStatus(id, type,desc);
        }

        public static string GetDesc(int device, int cate, int module, int val, int diff)
        {
            RequirementManageDAL dal = new RequirementManageDAL();
            return dal.GetDesc(device, cate, module, val, diff);
        }
    }
}
