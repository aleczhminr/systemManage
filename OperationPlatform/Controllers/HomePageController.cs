using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using Model;
using OperationPlatform.Models;
using Controls;
using Controls.SysAccount;
using Model.Model4View; 

namespace OperationPlatform.Controllers
{
    [OperationPlatform.App_Start.LoginAuthentication]
    public class HomePageController : Controller
    {
        // GET: HomePage
        public ActionResult Index()
        {
            return View();
            
        }

        #region 修改密码

        /// <summary>
        /// 定向到修改密码的页面
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ChangePwd()
        {
            //if (Session["adminid"] == null)
            //    return RedirectToAction("Index", "HomePage");
            //else
            //    return RedirectToAction("NewPwd", "HomePage");
            return RedirectToAction("NewPwd", "HomePage");
        }

        /// <summary>
        /// 显示修改密码页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult NewPwd()
        {
            ManageUserModel uM = (ManageUserModel)Session["logUser"];
            ViewBag.UsrName = uM.UserName;
            return View();
        }

        /// <summary>
        /// 执行修改密码的动作
        /// </summary>
        /// <param name="viewData"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult NewPwd(NewPwdModels viewData)
        {
            ManageUserModel uM = (ManageUserModel)Session["logUser"];
            string id = uM.UserID.ToString();
            string newPwd = viewData.NewPwd;
            string oldPwd = viewData.OldPwd;

            ViewBag.js = Account.ChangePwd(newPwd, oldPwd, id);
            return View();
        }
        #endregion



        #region 查看登录日志
        /// <summary>
        /// 定向到查看日志的页面
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ViewLoginLog()
        {
            //if (Session["adminid"] == null)
            //    return RedirectToAction("Index", "HomePage");
            //else
            //    return RedirectToAction("LoginLog", "HomePage");
            return RedirectToAction("LoginLog", "HomePage");
        }

        /// <summary>
        /// 初始化查看登录日志的页面
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ActionResult LoginLog(int pageIndex = 1, int pageSize = 20)
        {
            ViewBag.dataCount = GetLogListCount("");
            ViewBag.data = GetLogList(pageIndex, pageSize);
            return View();
        }

        /// <summary>
        /// 获取分页后的登录日志列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public string GetLogList(int pageIndex, int pageSize)
        {
            var model = Account.GetLoginLogList(pageIndex, pageSize, "");

            return Newtonsoft.Json.JsonConvert.SerializeObject(model);
        }

        /// <summary>
        /// 获取登录日志总行数
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public int GetLogListCount(string where)
        {
            return Account.GetLogListCount(where);
        }
        #endregion



        #region 店铺审核
        /// <summary>
        /// 定向到店铺审核的页面
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ViewShopCheck()
        {
            if (Session["logUser"] == null)
                return RedirectToAction("Index", "HomePage");
            else
                return RedirectToAction("ShopCheck", "HomePage");
        }

        /// <summary>
        /// 显示店铺审核页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ShopCheck()
        {;

            return View();
        }

        public string GetShopCheck(int timeType=0,DateTime? regTime = null)
        {
            DateTime startTime = DateTime.Now;
            if (timeType == 0)
            {
                startTime = DateTime.Now;
            }
            else if (timeType == 1)
            {
                startTime = DateTime.Now.AddDays(-1);
            }
            else
            {
                if (regTime == null)
                {
                    startTime = DateTime.Now;
                }
                else
                {
                    startTime = Convert.ToDateTime(regTime);
                }
            }
            List<UncheckedShopModel> list = Account.GetUncheckedShopList(startTime);
            return CommonLib.Helper.JsonSerializeObject(list,"yyyy-MM-dd HH:mm:ss");
        }


        /// <summary>
        /// 执行审核通过的动作
        /// </summary>
        /// <returns></returns>
        public ActionResult ShopChecked(int id,string phone)
        {
            return Content(CheckShopInfo(id, phone), "text/html");
        }


        /// <summary>
        /// 执行店铺删除的动作
        /// </summary>
        /// <returns></returns>
        public ActionResult ShopDelete(int id)
        {
            return Content(DeleteUncheckedShop(id), "text/html");
        }

        /// <summary>
        /// 审核店铺信息
        /// </summary>
        /// <returns></returns>
        public string CheckShopInfo(int id,string phone)
        {
            int type = string.IsNullOrEmpty(phone) ? 0 : 1;
            int status = Account.CheckShop(id, type);

            if (status>0)
            {
                return "1";
            }
            else
            {
                return "2";
            }
        }

        /// <summary>
        /// 删除审核店铺
        /// </summary>
        /// <param name="accid"></param>
        /// <returns></returns>
        public string DeleteUncheckedShop(int accid)
        {
            int status = Account.DeleteShopDuringChecking(accid);
            if (status > 0)
            {
                return "<script>alert('删除成功！');window.location='/HomePage/ShopCheck'; </script>";
            }
            else
            {
                return "<script>alert('删除失败！');window.location='/HomePage/ShopCheck'; </script>";
            }
        }

        #endregion

    }
}
