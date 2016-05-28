using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controls.UserPortrait;
using Model;

namespace OperationPlatform.Controllers
{
    public class UserPortraitController : Controller
    {
        // GET: UserPortrait
        public ActionResult Index()
        {
            return View();
        }

        public string GetSingleUsrPortrait(int accId)
        {
            return UserPortrait.GetSingleUsrPortrait(accId);
        }

        public string AddNewDicItem(int itemType,string addItemValue,int parentId=0)
        {
            return UserPortrait.AddNewDicItem(itemType, addItemValue, parentId);
        }

        public string GetDicItem()
        {
            return UserPortrait.GetDicItem();
        }

        public string AddUserPortrait(P_Sys_UserPortraitModel model)
        {
            return UserPortrait.AddUserPortrait(model);
        }

        public string GetUserExtInfo(int accid)
        {
            return UserPortrait.GetUserExtInfo(accid);
        }

        public string GetRemarkInfo(string remarkIdStr)
        {
            return CommonLib.Helper.JsonSerializeObject(UserPortrait.GetRemarkInfo(remarkIdStr));
        }
    }
}
