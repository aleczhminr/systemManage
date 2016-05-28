using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OperationPlatform.Controllers
{
    [OperationPlatform.App_Start.LoginAuthentication]
    public class MapDemoController : Controller
    {
        // GET: MapDemo
        public ActionResult Index()
        {
            return View();
        }

        public string DemoMap(string gName)
        {
            var list = Controls.AreaMap.AreaMap.GetGoodsListEx(gName);
            return CommonLib.Helper.JsonSerializeObject(list);
        }

        public string PoiAround(string lng,string lat)
        {
            var list = Controls.AreaMap.AreaMap.GetShopAround(lng, lat);
            return CommonLib.Helper.JsonSerializeObject(list);
        }

        public string GetShopAddress(string shopName)
        {
            return CommonLib.Helper.JsonSerializeObject(Controls.AreaMap.AreaMap.GetSpecShop(shopName));
        }

        // GET: MapDemo/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MapDemo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MapDemo/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: MapDemo/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MapDemo/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: MapDemo/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MapDemo/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
