using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using Model;

namespace Controls.DailyCheck
{
    public static class DailyCheck
    {

        /// <summary>
        /// 得到所有的人员
        /// </summary>
        /// <returns></returns>
        public static List<RemindUsr> GetRemindUsr()
        {
            return Sys_Manage_UserBLL.GetRemindUsr();
        }
        /// <summary>
        /// 新增加
        /// </summary>
        /// <param name="dailyModel"></param>
        /// <returns></returns>
        public static int AddTask(Sys_DailyCheck dailyModel)
        {
            return Sys_DailyCheckBLL.AddTask(dailyModel);
        }
        /// <summary>
        /// 得到今天的任务
        /// </summary>
        /// <param name="mUid"></param>
        /// <returns></returns>
        public static List<Sys_DailyCheckBase> GetNowTaskList(int mUid)
        {
            DateTime nowDate = DateTime.Now.Date;
            int week =(int)nowDate.DayOfWeek;


            string columnName = "ID,TaskName,IsRepeat,RepeatType,RepeatTime,RemindTime,Reminder";
            #region 得到只有今天 才提醒的信息
            //得到单次 今天任务
            List<DapperWhere> oneRemind = new List<DapperWhere>();
            oneRemind.Add(new DapperWhere("IsRepeat", 0));
            oneRemind.Add(new DapperWhere("RepeatType", 1));
            oneRemind.Add(new DapperWhere("TaskStatus", 0));
            oneRemind.Add(new DapperWhere("RemindTime", nowDate.Add(new TimeSpan(23, 59, 59)), "RemindTime between cast(@RemindTime as date) and @RemindTime"));
            oneRemind.Add(new DapperWhere("Reminder", mUid.ToString(), "Reminder like '%,'+ @Reminder +',%'"));

            List<Sys_DailyCheckBase> oneList = Sys_DailyCheckBLL.GetList<Sys_DailyCheckBase>(0, columnName, oneRemind, " Id asc");
            #endregion

            #region 得到每周都会显示
            //得到每周 今天任务
            string weekString = "";
            switch (week)
            {
                case 0:
                    weekString = "七";
                    break;
                case 1:
                    weekString = "一";
                    break;
                case 2:
                    weekString = "二";
                    break;
                case 3:
                    weekString = "三";
                    break;
                case 4:
                    weekString = "四";
                    break;
                case 5:
                    weekString = "五";
                    break;
                case 6:
                    weekString = "六";
                    break;
            }
            List<DapperWhere> weekRemind = new List<DapperWhere>();
            weekRemind.Add(new DapperWhere("IsRepeat", 1));
            weekRemind.Add(new DapperWhere("RepeatType", 2));
            weekRemind.Add(new DapperWhere("TaskStatus",0));
            weekRemind.Add(new DapperWhere("Reminder", mUid.ToString(), "Reminder like '%,'+ @Reminder +',%'"));
            weekRemind.Add(new DapperWhere("RepeatTime", weekString, "RepeatTime like '%,'+ @RepeatTime +',%'"));

            List<Sys_DailyCheckBase> weekList = Sys_DailyCheckBLL.GetList<Sys_DailyCheckBase>(0, columnName, weekRemind, " id asc ");
            #endregion

            #region 得到 当日 这一号的拽住
            List<DapperWhere> dateRemind = new List<DapperWhere>();
            dateRemind.Add(new DapperWhere("IsRepeat", 1));
            dateRemind.Add(new DapperWhere("RepeatType", 3));
            dateRemind.Add(new DapperWhere("TaskStatus", 0));
            dateRemind.Add(new DapperWhere("Reminder", mUid.ToString(), "Reminder like '%,'+ @Reminder +',%'"));
            dateRemind.Add(new DapperWhere("RepeatTime", nowDate.Day.ToString(), "RepeatTime like '%,'+ @RepeatTime +',%'"));

            List<Sys_DailyCheckBase> dateList = Sys_DailyCheckBLL.GetList<Sys_DailyCheckBase>(0, columnName, dateRemind, " id asc ");
            #endregion


            List<Sys_DailyCheckBase> allList = new List<Sys_DailyCheckBase>();
            if (oneList != null && oneList.Count > 0)
            {
                allList.AddRange(oneList);
            }
            if (weekList != null && weekList.Count > 0)
            {
                allList.AddRange(weekList);
            }
            if (dateList != null && dateList.Count > 0)
            {
                allList.AddRange(dateList);
            }

            return allList;
        }

        /// <summary>
        /// 增加记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int AddRecord( Sys_DailyCheckRecord model)
        {
            return Sys_DailyCheckRecordBLL.Add(model);
        }
        /// <summary>
        /// 得到今天的日志记录
        /// </summary>
        /// <param name="dcid"></param>
        /// <returns></returns>
        public static List<Sys_DailyCheckRecord> GetNowDateRecordList(int[] dcid)
        {
            string dcIds = "";
            foreach (int dc in dcid)
            {
                dcIds += "," + dc;
            }

            List<DapperWhere> whereList = new List<DapperWhere>();
            whereList.Add(new DapperWhere("OperateTime", DateTime.Now.Date.Add(new TimeSpan(23, 59, 59)), "OperateTime between CAST(@OperateTime as date) and @OperateTime"));
            whereList.Add(new DapperWhere("dcId", 0, "dcId in(" + dcIds.Trim(',') + ")"));
            return Sys_DailyCheckRecordBLL.GetList(whereList);

        }
        /// <summary>
        /// 得到分页列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="uName"></param>
        /// <returns></returns>
        public static Dictionary<string, object> GetRecordList(int pageIndex, int pageSize, string uName)
        {

            Dictionary<string, object> list = new Dictionary<string, object>();

            List<DapperWhere> sqlWhere = new List<DapperWhere>(); 
            if (uName != "")
            {
                sqlWhere.Add(new DapperWhere("Operator", uName, " Operator like '%'+ @Operator +'%' "));
            } 



            if (pageSize < 1)
            {
                pageSize = 20;
            }

            int rowCount = 0;
            if (pageIndex == 1)
            {
                rowCount = BLL.Sys_DailyCheckRecordBLL.GetCount(sqlWhere);
            }
            int maxPage = 0;
            if (rowCount > 0)
            {
                maxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(rowCount) / pageSize));
            }

            list["rowCount"] = rowCount;
            list["maxPage"] = maxPage;
            list["pageIndex"] = pageIndex;
            list["listData"] = BLL.Sys_DailyCheckRecordBLL.GetList(pageIndex, pageSize, sqlWhere, " id desc");

            return list;
        }
        /// <summary>
        /// 得到列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="uName"></param>
        /// <returns></returns>
        public static Dictionary<string, object> GetList(int pageIndex, int pageSize,string uName)
        {
            Dictionary<string, object> list = new Dictionary<string, object>();

            List<DapperWhere> sqlWhere = new List<DapperWhere>();

            sqlWhere.Add(new DapperWhere("TaskStatus", 0));

            if (uName != "")
            {
                sqlWhere.Add(new DapperWhere("Operator", uName, " Operator like '%'+ @Operator +'%' "));
            }



            if (pageSize < 1)
            {
                pageSize = 20;
            }

            int rowCount = 0;
            if (pageIndex == 1)
            {
                rowCount = BLL.Base.Sys_DailyCheckBaseBLL.GetCount(sqlWhere);
            }
            int maxPage = 0;
            if (rowCount > 0)
            {
                maxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(rowCount) / pageSize));
            }

            list["rowCount"] = rowCount;
            list["maxPage"] = maxPage;
            list["pageIndex"] = pageIndex;

            List < Sys_DailyCheck > ModelList = BLL.Base.Sys_DailyCheckBaseBLL.GetList(pageIndex, pageSize, sqlWhere, " id desc");


            //得到 周类别， 得到当前账号信息

            List<RemindUsr> userList = Sys_Manage_UserBLL.GetRemindUsr();
            Dictionary<int, string> userKeyVal = new Dictionary<int, string>();
            foreach (RemindUsr itemUL in userList)
            {
                userKeyVal[itemUL.Id] = itemUL.Name;
            }


            foreach (Sys_DailyCheck itemModel in ModelList)
            {
                string[] uS = itemModel.Reminder.Trim(',').Split(',');
                string userName = "";
                foreach (string uIS in uS)
                {
                    int uISInt = 0;
                    if (int.TryParse(uIS, out uISInt))
                    {
                        try
                        {
                            userName += userKeyVal[uISInt] + "，";
                        }
                        catch (Exception ex)
                        {
                            userName += ""; 
                        }
                        
                    }
                }
                itemModel.Reminder = userName.Trim('，');

                string repeatTime = itemModel.RepeatTime;
                if (repeatTime != null && repeatTime.Length > 0)
                { 
                    string[] repeatTimes = repeatTime.Trim(',').Split(',');
                    repeatTime = "";
                    foreach (string i in repeatTimes)
                    {
                        int day = 0;
                        if (int.TryParse(i, out day))
                        {
                            repeatTime += "" + i + "日，";
                        }
                        else
                        {
                            repeatTime += "星期" + i + "，";
                        }
                    }
                }
                else
                {
                    repeatTime = Convert.ToDateTime(itemModel.RemindTime).ToString("yyyy-MM-dd");
                }
                itemModel.RepeatTime = repeatTime.Trim('，');
            }




            list["listData"] = ModelList;
            return list;
        }
        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DeleteDailyCheck(int id)
        {
            return Sys_DailyCheckBLL.Delete(id);
        }
    }
}
