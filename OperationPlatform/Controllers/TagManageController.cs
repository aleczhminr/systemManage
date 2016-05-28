using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controls;
using Model;

namespace OperationPlatform.Controllers
{
    public class TagManageController : Controller
    {
        // GET: TagManage
        public ActionResult Index()
        {
            return View();
        }

        public string GetTagInfo(int pageIndex,int tagType,string insertName="",string tagName="")
        {
            return TagManage.GetTagInfo(pageIndex, tagType, insertName, tagName);
        }

        public string GetSingleModel(int id)
        {
            return TagManage.GetSingleModel(id);
        }

        public string ModifyModel(int id, string tagName, string tagTypeId, string tagTypeName, int tagStatus)
        {
            return TagManage.ModifyModel(id, tagName, tagTypeId, tagTypeName,tagStatus);
        }

        public string AddNewTag(string tagName)
        {
            ManageUserModel uM = (ManageUserModel)Session["logUser"];

            return TagManage.AddNewTag(tagName, uM.Name);
        }

        //Test transfer
        public string ImportTag()
        {
            return TagManage.TagImport();
        }

    }
}
