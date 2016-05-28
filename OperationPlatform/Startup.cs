using Hangfire;
using Microsoft.Owin;
using Owin;
using Hangfire;

[assembly: OwinStartupAttribute(typeof(OperationPlatform.Startup))]
namespace OperationPlatform
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            //添加HangFire相关处理
            GlobalConfiguration.Configuration.UseSqlServerStorage(System.Configuration.ConfigurationManager.AppSettings["TaskDb"].ToString());

            app.UseHangfireDashboard();
            app.UseHangfireServer();
        }
    }
}
