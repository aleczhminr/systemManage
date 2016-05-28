using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controls.DailyCheck;
using Model;

namespace OperationPlatform.Controllers
{
    [OperationPlatform.App_Start.LoginAuthentication]
    public class DailyCheckController : Controller
    {
        // GET: DailyCheck
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddCheckItem()
        {
            return View();
        }

        public ActionResult CheckTaskList()
        {
            return View();
        }

        public string GetRemindUsr()
        {
            return CommonLib.Helper.JsonSerializeObject(DailyCheck.GetRemindUsr());
        }

 
        public string AddTask(string taskName, int repeatType, string repeatTime, string reminder)
        {
            Sys_DailyCheck dailyModel = new Sys_DailyCheck();

            dailyModel.TaskName = taskName;
            dailyModel.RepeatType = repeatType;

            if (repeatTime.Length > 0)
            {
                if (repeatType == 1)
                {
                    dailyModel.RemindTime = Convert.ToDateTime(repeatTime);
                    dailyModel.IsRepeat = 0;
                }
                else
                {
                    dailyModel.RepeatTime ="," + repeatTime + ",";
                    dailyModel.IsRepeat = 1;
                }
            }
            else
            {
                return "-2";//请选择提醒 时间
            }
            if (reminder.Length > 0)
            {
                dailyModel.Reminder ="," + reminder.ToString() + ",";
            }
            else
            {
                return "-1";//请选择提醒 人
            }

            ManageUserModel uM = (ManageUserModel)Session["logUser"];
            dailyModel.Recorder = uM.Name;
            dailyModel.RecordTime = DateTime.Now;


            return DailyCheck.AddTask(dailyModel).ToString();
        }

        public string NowTaskList()
        {
            ManageUserModel uM = (ManageUserModel)Session["logUser"];
            var list = DailyCheck.GetNowTaskList(uM.UserID);

            return CommonLib.Helper.JsonSerializeObject(list,"yyyy-MM-dd HH:mm:ss");
        }

        public string AddRecord(int dcid, string taskName, string taskVal,string remark="")
        {
            ManageUserModel uM = (ManageUserModel)Session["logUser"];
            Sys_DailyCheckRecord cr = new Sys_DailyCheckRecord();
            cr.dcId = dcid;
            cr.TaskName = taskName;
            cr.TaskValue = taskVal;
            cr.Operator = uM.Name;
            cr.OperateTime = DateTime.Now;
            cr.Remark = remark;
            return DailyCheck.AddRecord(cr).ToString();
        }
        /// <summary>
        /// 得到日志
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        public string GetRecord(string tid)
        {

            List<int> tidList = new List<int>();
            foreach (string t in tid.Split(','))
            {
                int id = 0;
                if (int.TryParse(t, out id))
                {
                    tidList.Add(id);
                }
            }

            if (tidList.Count > 0)
            {
                return CommonLib.Helper.JsonSerializeObject(DailyCheck.GetNowDateRecordList(tidList.ToArray()), "HH:mm:ss");
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 得到任务列表
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public string GetList(int page)
        {
            var list = DailyCheck.GetList(page, 15, "");
            return CommonLib.Helper.JsonSerializeObject(list);
        }

        public string GetRecordList(int page, string uName="", int pagesize = 15)
        {
            var list = DailyCheck.GetRecordList(page, pagesize, uName);
            return CommonLib.Helper.JsonSerializeObject(list, "yyyy-MM-dd HH:mm:ss");
        }
        public string DeleteDailyCheck(int id)
        {
            if (DailyCheck.DeleteDailyCheck(id))
            {
                return "1";
            }
            else
            {
                return "0";
            }
        }

    }
}
