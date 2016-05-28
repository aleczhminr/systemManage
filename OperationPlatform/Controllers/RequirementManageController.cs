using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Model;
using RequirementManage = Controls.RequirementManage.RequirementManage;

namespace OperationPlatform.Controllers
{
    public class RequirementManageController : Controller
    {
        // GET: RequirementManage
        public ActionResult Index()
        {
            return View();
        }        

        /// <summary>
        /// 获取需求类型列表
        /// </summary>
        /// <returns></returns>
        public string GetReqType()
        {
            return RequirementManage.GetReqList();
        }

        /// <summary>
        /// 添加新需求
        /// </summary>
        /// <param name="accId"></param>
        /// <param name="refId"></param>
        /// <param name="cateId"></param>
        /// <param name="reqType"></param>
        /// <param name="desc"></param>
        /// <param name="originDesc"></param>
        /// <param name="device"></param>
        /// <param name="val"></param>
        /// <param name="diff"></param>
        /// <returns></returns>
        public string AddNewReq(int accId, int refId, int cateId, int reqType, string desc,string originDesc,int device,int val,int diff)
        {
            ManageUserModel uM = (ManageUserModel)Session["logUser"];
            int op = uM.UserID;

            if (op != 38 && op != 68 && op != 74 && op != 83 && op != 84 && op != 44 && op != 54)
            {
                return "您没有新增需求的权限~";
            }
            else
            {
                return Controls.RequirementManage.RequirementManage.AddNewReqModel(accId, refId, cateId, reqType, desc, op, originDesc, device, val, diff);    
            }
            
        }

        /// <summary>
        /// 新增需求类型
        /// </summary>
        /// <param name="cateName"></param>
        /// <returns></returns>
        public string AddNewCategory(string cateName)
        {
            return RequirementManage.AddNewCategory(cateName);
        }

        /// <summary>
        /// 获取需求说明提示
        /// </summary>
        /// <param name="device"></param>
        /// <param name="cate"></param>
        /// <param name="module"></param>
        /// <param name="val"></param>
        /// <param name="diff"></param>
        /// <returns></returns>
        public string GetDesc(int device, int cate, int module, int val, int diff)
        {
            return RequirementManage.GetDesc(device, cate, module, val, diff);
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
        /// <returns></returns>
        public string GetRequireList(int pageIndex, string stDate, string edDate, int reqType,
            int status, string content,int terminal)
        {
            return RequirementManage.GetRequireList(pageIndex, stDate, edDate,
                reqType, status, content, terminal);
        }

        /// <summary>
        /// 变更任务状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="statusType"></param>
        /// <returns></returns>
        public string ChangeTaskStatus(int id, int statusType, string desc, string accid)
        {
            return RequirementManage.ChangeTaskStatus(id, statusType, desc, accid);
        }
    }
}
