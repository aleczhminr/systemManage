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
    public class ShopListController : Controller
    {
        //
        // GET: /ShopList/
        public ActionResult Index(string column="",string value="",string searchStr="")
        {
            ViewBag.Column = column;
            ViewBag.Value = value;
            ViewBag.Str = searchStr;

            return View();
        }

        public string ShopList(int pageIndex, string companyName = "", string userRealName = "", string phoneNumber = "", string userEmail = "", int bbsUid = 0, int agentId = 0, string agentName = "", string serviceManager = "", string regSource = "all", DateTime? startRegTime = null, DateTime? endRegTime = null,string searchStr="")
        {
            var list = Controls.Shop.ShopSearch.GetSearchList(pageIndex, 15, companyName, userRealName, phoneNumber, userEmail, bbsUid, agentId, agentName, serviceManager, regSource,startRegTime,endRegTime,searchStr);

            //IndexDetailModel detailModel = new IndexDetailModel();

            //int[] accids = CommonLib.Helper.JsonDeserializeObject<List<int>>(list["accidList"].ToString()).ToArray();


            //detailModel.listData = DashBoardAnalyzeBLL.GetGeneralList(accids);
            //detailModel.rowCount = Convert.ToInt32(list["rowCount"]);
            //detailModel.maxPage = Convert.ToInt32(list["maxPage"]);

            //return CommonLib.Helper.JsonSerializeObject(detailModel, "yyyy-MM-dd HH:mm:ss");
            return CommonLib.Helper.JsonSerializeObject(list, "yyyy-MM-dd HH:mm:ss");
        }

	}
}