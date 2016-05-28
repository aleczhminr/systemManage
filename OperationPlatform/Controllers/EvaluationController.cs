using Controls.Evaluation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OperationPlatform.Controllers
{
    public class EvaluationController : Controller
    {
        //
        // GET: /Evaluation/
        public ActionResult Index()
        {
            return View();
        }
        public string getEvaluationRecord(int pageIndex = 1, int productType = -99, int displayType = -99, string busId = "", string evaluationID = "", string accId = "", string start = "", string end = "")
        {
            return Evaluation.getEvaluationRecord(pageIndex, productType, displayType, busId, evaluationID, accId, start, end);
        }
        public string UpdateEvaluation(int evaluationid, int status)
        {
            return Evaluation.UpdateEvaluation(evaluationid, status);
        }
    }
}