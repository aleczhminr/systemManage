using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OperationPlatform.Controllers
{
    [OperationPlatform.App_Start.LoginAuthentication]
    public class GoodsStoreController : Controller
    {
        //
        // GET: /GoodsStore/
        public ActionResult Index()
        {
            return View();
        }

        public string GetList(int pageIndex, int isPic, string gname = "", int? start = null, int? end = null, string isToday = "0")
        {
            var list = Controls.O2O.GoodsInfo.GetList(pageIndex, 10, isPic, gname, start, end);
            return CommonLib.Helper.JsonSerializeObject(list);
        }

        public string GetPicList(string gid, string ygid)
        {
            string json = "[]";
            List<int> gids = new List<int>();
            List<int> ygids = new List<int>();
            if (gid.Length > 0)
            {
                foreach (string item in gid.Split(','))
                {
                    int i = 0;
                    if (int.TryParse(item, out i))
                    {
                        gids.Add(i);
                    }
                }
            }
            if (ygid.Length > 0)
            {
                foreach (string item in ygid.Split(','))
                {
                    int i = 0;
                    if (int.TryParse(item, out i))
                    {
                        ygids.Add(i);
                    }
                }
            }
            if (gids.Count > 0 || ygids.Count > 0)
            {
                var list = Controls.O2O.GoodsInfo.GetPic(gids.ToArray(), ygids.ToArray());
                json = CommonLib.Helper.JsonSerializeObject(list);
            }
            return json;
        }

        public string GetGoodsInfo(int gid)
        {
            var list = Controls.O2O.GoodsInfo.GetGoodsInfoList(gid);
            return CommonLib.Helper.JsonSerializeObject(list);
        }
    }
}