using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace OperationPlatform
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        void Application_Error(object sender, EventArgs e)
        {
            // 在出现未处理的错误时运行的代码

            try
            {

                Exception ex = Server.GetLastError().GetBaseException();
                string message = "";
                if (ex != null)
                {
                    // 错误的信息
                    message += "<br/>错误信息:" + ex.Message + "<br>";
                    // 错误的堆栈
                    message += "错误堆栈:" + ex.StackTrace.ToString() + "<br>";// Replace(" ", "<br/>") + "<br>"; 
                    // 出错的方法名
                    message += "错误方法:" + ex.TargetSite.Name + "<br>";
                    // 出错的类名
                    message += "错误类名:" + ex.TargetSite.DeclaringType.FullName + "<br>";

                    message += "错误应用名称:" + ex.Source + "<br>";

                    //出错页面地址
                    message += "错误页面地址:" + Request.Url.ToString() + "<br>";

                    //出错店铺ID
                    if (Session["adminid"] != null)
                    {
                        message += "出错ID:" + Session["adminid"].ToString() + "<br>";
                    }
                    message += "<br/><br/>";

                    string str = "error";

                   Utility.Logger.Error(message, null);
                    Response.Write(str);

                }

                // 清空最后的错误
                Server.ClearError();
            }
            catch
            {
                // ignore
            }
        }
    }
}
