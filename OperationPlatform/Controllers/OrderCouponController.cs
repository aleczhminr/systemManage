using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Model;

namespace OperationPlatform.Controllers
{
    [OperationPlatform.App_Start.LoginAuthentication]
    public class OrderCouponController : Controller
    {
        //
        // GET: /OrderCoupon/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CouponAdd()
        {
            return View();
        }

        public string GetOrderProject()
        {
            List<Order_Project_Model> list = T_Order_ProjectBLL.GetList(0, "busId,displayName,projectType",
                "closingDate>=GETDATE() and hiddenVal=0", " busid asc");

            return CommonLib.Helper.JsonSerializeObject(list);
        }

        public string GetAgent()
        {
            var prefixVerInfo = T_PreFixVerBLL.GetModelList("");

            return CommonLib.Helper.JsonSerializeObject(T_PreFixVerBLL.GetModelList(""));
        }

        public string AddCoupon(int couponType,int bindType,int bindValue,string bindName,int ruleType,int ruleValue,int couponValue,string couponDesc,
                int maxLimitNum, DateTime endDate, string remark, string prefix)
        {
            ManageUserModel uM = (ManageUserModel)Session["logUser"];

            T_Order_CouponInfo model = new T_Order_CouponInfo();

            model.couponType = couponType;
            model.bindType = bindType;
            model.bindValue = bindValue;
            model.bindName = bindName;
            model.ruleType = ruleType;
            model.ruleValue = ruleValue;
            model.couponValue = couponValue;
           model.couponStatus = 0;
           model.couponDesc = couponDesc;
           model.maxLimitNum = maxLimitNum;
           model.createDate = DateTime.Now;
           model.endDate = endDate.AddHours(23).AddMinutes(59).AddSeconds(59);
           model.remark = remark;
           model.operatorId = uM.UserID;
           model.operarorIp = Request.UserHostAddress;
           model.operatorTime = DateTime.Now;
           model.prefixAgent = (prefix == null ? 0 : Convert.ToInt32(prefix));

           model.id = T_Order_CouponInfoBLL.Add(model);
           if (model.id > 0)
           {
               return "0";
           }
           else
           {
               return "-1";
           }
      
        }

        public string CouponList(int page, string couponDesc = "", DateTime? statTime = null, DateTime? endTime = null, string operatorName = "", string expired = "y")
        {
            var list = Controls.Coupon.Coupon.GetList(page, 15, couponDesc, statTime, endTime, operatorName, expired);
            return CommonLib.Helper.JsonSerializeObject(list, "yyyy-MM-dd HH:mm:ss"); ;
        }

        public string CouponInfo(int id)
        {
            var model = Controls.Coupon.Coupon.GetModel(id);
            return CommonLib.Helper.JsonSerializeObject(model, "yyyy-MM-dd HH:mm");
        }
        public string CouponInfoList(int ocid, int page, int? couponStatus = null)
        {
            var list = Controls.Coupon.Coupon.GetInfoList(page, 10, ocid, couponStatus);
            return CommonLib.Helper.JsonSerializeObject(list, "yyyy-MM-dd HH:mm");
        }

        public string GenerateCoupon(int ocid, int num)
        {
            if (num < 1)
            {
                num = 1;
            }
            var coupon = new Controls.Coupon.Coupon();
            try
            {
                coupon.BatchCreateCouponGroupDelegate(ocid, num);
                return "1";
            }
            catch (Exception)
            {
                return "0";
            }
        }

        public string BingCoupon(int accountid, string CouponID)
        {
            //{-1:处理错误，0：优惠券不存在，1：优惠券已使用或者已经作废，2：绑定成功}
            return Controls.Shop.ShopDetails.BingingCoupon(accountid, CouponID).ToString();
        }
	}
}