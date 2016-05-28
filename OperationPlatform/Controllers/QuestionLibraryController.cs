using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OperationPlatform.Controllers
{
    [OperationPlatform.App_Start.LoginAuthentication]
    public class QuestionLibraryController : Controller
    {
        //
        // GET: /QuestionLibrary/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult QuestionList()
        {
            return View();
        }
	}
}