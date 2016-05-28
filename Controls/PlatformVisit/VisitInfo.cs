using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BLL;
using System.Threading;
using System.Web.UI.HtmlControls;
using Utility;


namespace Controls.PlatformVisit
{
    /// <summary>
    /// 回访信息
    /// </summary>
    public static class VisitInfo
    {
        //委托处理其他事务
        private delegate void AddVisitInfoDelegate(int accid, int vid, string insertName, string tags, int taskid);
        /// <summary>
        /// 得到回访标签列表
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, SysVisitTagTypeInfo> GetVisitTagList()
        {
            List<DapperWhere> sqlWhere = new List<Model.DapperWhere>();
            sqlWhere.Add(new DapperWhere("vtState", 0));
            List<Sys_VisitTag> list = BLL.Base.Sys_VisitTagBaseBLL.GetList(0, sqlWhere, " vTypeId desc,t_Order desc,id desc");

            Dictionary<string, SysVisitTagTypeInfo> tagList = new Dictionary<string, Model.SysVisitTagTypeInfo>();


            foreach (Sys_VisitTag tag in list)
            {
                if (!tagList.ContainsKey(tag.vTypeName))
                {
                    tagList.Add(tag.vTypeName, new SysVisitTagTypeInfo(tag.vTypeId, tag.vTypeName));
                }
                tagList[tag.vTypeName].ItemList.Add(new SysVisitTagItem(tag.id, tag.tagName));
            }

            return tagList;
        }


        public static chartDataModel GetCaseModel(DateTime startTime, DateTime endTime, int rank)
        {
            //Dictionary<string, string> ColumnList = new Dictionary<string, string>();

            //List<DapperWhere> dappWhere = new List<DapperWhere>();

            //dappWhere.Add(new DapperWhere("startTime", startTime, " S_Date>=@startTime"));

            //dappWhere.Add(new DapperWhere("endTime", endTime, " S_Date<=@endTime"));

            //StringBuilder strColumn = new StringBuilder();

            //#region  主键
            //strColumn.Append("S_Date dayDate");

            //strColumn.Append(",isnull(loginNum,0) loginNum");
            //ColumnList.Add("loginNum", "登录用户");
            //strColumn.Append(",(isnull(activeNum,0)+isnull(faithfulNum,0)) activeNum");
            //ColumnList.Add("activeNum", "活跃用户");
            ////strColumn.Append(",isnull(faithfulNum,0) faithfulNum");
            ////ColumnList.Add("faithfulNum", "忠诚用户");

            //if (columns.Contains("shop"))
            //{
            //    strColumn.Append(",isnull(NewAccNum,0) NewAccNum");
            //    ColumnList.Add("NewAccNum", "店铺");
            //}
            //#endregion

            //List<dynamic> dataList = BLL.Base.SysRpt_WebDayInfoBaseBLL.GetList<dynamic>(0, strColumn.ToString(), dappWhere, " id desc");

            //chartDataModel chartModel = new chartDataModel();
            //return chartModel.SetDataAboutDataTime(startTime, endTime, dataList, ColumnList, "回访事件分析");
            return null;
        }

        /// <summary>
        /// 增加回访信息
        /// </summary>
        /// <param name="accid">店铺ID</param>
        /// <param name="recordType">记录类别</param>
        /// <param name="vmanner">回访方式</param>
        /// <param name="vrOne">第一概要</param>
        /// <param name="vrTwo">第二概要</param>
        /// <param name="vrThress">第三概要</param>
        /// <param name="content">内容</param>
        /// <param name="stat">状态</param>
        /// <param name="insertName">回访人</param>
        /// <param name="tags">标签</param>
        /// <param name="contact">回访方式值</param>
        /// <param name="taskid">任务ID</param>
        /// <returns></returns>
        public static int AddVisitInfo(int accid, int recordType, int vrOne, int vrTwo, int vrThree, int vmanner, string content, int stat, string insertName, string tags = "", string contact = "", int taskid = 0, int feedBack = 0)
        {


            Sys_VisitInfo visitInfo = new Sys_VisitInfo();
            visitInfo.accid = accid;
            visitInfo.rt_Maxid = recordType;
            visitInfo.vm_id = vmanner;
            visitInfo.Contact = contact;
            visitInfo.initiative = 0;
            visitInfo.vi_Content = content;
            visitInfo.handleStat = stat;
            visitInfo.insertName = insertName;
            visitInfo.insertTime = DateTime.Now;
            visitInfo.remark = "";

            visitInfo.vr_Maxid = vrOne;
            visitInfo.vr_Minid = vrTwo;
            visitInfo.vr_Threeid = vrThree;

            visitInfo.id = BLL.Sys_VisitInfoBLL.Add(visitInfo);
            
            //回访导入反馈
            if (feedBack == 1)
            {
                string trimContent = StripHTML(content);
                if (AddCustomCareFeedBack(insertName, trimContent, accid, 0) == 0)
                {
                    return 0;
                }                
            }
            

            if (visitInfo.id > 0)
            {
                var AddDelegate = new AddVisitInfoDelegate(AddVisitInfoOtherDispose);
                AddDelegate.BeginInvoke(accid, visitInfo.id, insertName, tags, taskid, null, null);
                return visitInfo.id;
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// 处理回访其他事件
        /// </summary>
        /// <param name="accid"></param>
        /// <param name="vid"></param>
        /// <param name="insertName"></param>
        /// <param name="tags"></param>
        /// <param name="taskid"></param>
        public static void AddVisitInfoOtherDispose(int accid, int vid, string insertName, string tags, int taskid)
        {
            //处理 日常任务记录为处理成功
            if (taskid > 0)
            {
                Sys_TaskDailyBLL.UpdateWorkOut(taskid, insertName, vid);
            }
            //增加标签
            if (tags.Length > 0)
            {
                foreach (string item in tags.Split(','))
                {
                    int tagid = 0;
                    if (int.TryParse(item, out tagid))
                    {
                        Sys_VisitTagNexusBLL.Add(vid, tagid, insertName);
                        Thread.Sleep(100);
                    }

                }
            }


        }

        /// <summary>
        /// 得到列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="dapperWheres"></param>
        /// <param name="filedOrder"></param>
        /// <returns></returns>
        public static Dictionary<string, object> GetList(int pageIndex, int pageSize, int status = -1, int manner = 0, DateTime? starttime = null, DateTime? endtime = null, int accid = 0, string insertname = "", string tag = "", int recordType = 0, string keyword = "")
        {
            Dictionary<string, object> list = new Dictionary<string, object>();
            List<DapperWhere> dapperWheres = new List<DapperWhere>();

            if (recordType == 0)
            {
                dapperWheres.Add(new DapperWhere("rt_Maxid", 3, "rt_Maxid<@rt_Maxid"));
            }
            else
            {
                dapperWheres.Add(new DapperWhere("rt_Maxid", 3));
            }

            if (accid > 0)
            {
                dapperWheres.Add(new DapperWhere("accid", accid));
            }

            if (status > -1)
            {
                dapperWheres.Add(new DapperWhere("handleStat", status));
            }

            if (manner > 0)
            {
                dapperWheres.Add(new DapperWhere("vm_id", manner));
            }
            if (starttime != null)
            {
                dapperWheres.Add(new DapperWhere("startTime", Convert.ToDateTime(starttime).Date, "insertTime>=@startTime"));
            }

            if (endtime != null)
            {
                dapperWheres.Add(new DapperWhere("endTime", Convert.ToDateTime(endtime).Date.Add(new TimeSpan(23, 59, 59)), "insertTime<=@endTime"));
            }

            if (insertname.Length > 0)
            {
                dapperWheres.Add(new DapperWhere("insertName", insertname.Trim()));
            }

            if (keyword.Length > 0)
            {
                dapperWheres.Add(new DapperWhere("vi_Content", keyword, "vi_Content like '%" + keyword + "%'"));
            }

            if (tag.Length > 0)
            {
                string tagList = "";
                int count = 0;
                foreach (string item in tag.Split(','))
                {
                    int tagid = 0;
                    if (int.TryParse(item, out tagid))
                    {
                        tagList += "," + item;
                    }
                    count++;
                }
                if (tagList.Length > 0)
                {
                    dapperWheres.Add(new DapperWhere("taglist", tagList, "id in(select vid from Sys_VisitTagNexus where tid in (" + tagList.Trim(',') + ") group by vid having count(*)=" + count + ")"));
                }

            }


            if (pageSize < 1)
            {
                pageSize = 20;
            }

            int rowCount = 0;
            if (pageIndex == 1)
            {
                rowCount = BLL.Base.Sys_VisitInfoBaseBLL.GetCount(dapperWheres);
            }
            int maxPage = 0;
            if (rowCount > 0)
            {
                maxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(rowCount) / pageSize));
            }


            List<Sys_VisitInfoBase> dataList = Sys_VisitInfoBLL.GetList(pageIndex, pageSize, dapperWheres, " id desc");

            dataList=dataList.OrderByDescending(x => x.insertTime).ToList();
            //foreach (Sys_VisitInfoBase item in dataList)
            //{
            //    string content = item.vi_Content;
            //    if (content.Contains('｛'))
            //    {
            //        string filterContent = "反馈问题：";

            //        filterContent += content.Substring(content.IndexOf('｛'), content.IndexOf('【')) + "<br/>回访记录：";
            //        filterContent += content.Substring(content.IndexOf('｝'), content.Length);

            //        item.vi_Content = filterContent;
            //    }

            //}

            list["rowCount"] = rowCount;
            list["maxPage"] = maxPage;
            list["pageIndex"] = pageIndex;
            list["listData"] = dataList;

            return list;
        }

        /// <summary>
        /// 得到一个详情的信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static SysVisitParticularModel GetParticularModel(int id)
        {
            return Sys_VisitInfoBLL.GetParticularModel(id);
        }

        /// <summary>
        /// 得到回访分类
        /// </summary>
        /// <param name="vrid"></param>
        /// <returns></returns>
        public static List<Sys_VisitReason> GetVisitReasonList(int vrMaxId)
        {
            return Sys_VisitReasonBLL.GetList(vrMaxId);
        }
        /// <summary>
        /// 得到回访方式
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, int> GetVisitManner()
        {
            var oChannel = new ChannelList();
            Dictionary<string, int> visitList = new Dictionary<string, int>();
            foreach (int i in Enum.GetValues(typeof(Model.Enum.VisitInfoEnum.VisitManner)))
            {
                visitList[Enum.GetName(typeof(Model.Enum.VisitInfoEnum.VisitManner), i)] = i;
            }
            return visitList;
        }
        /// <summary>
        /// 添加回复
        /// </summary>
        /// <param name="vi_id">回访ID</param>
        /// <param name="accid">店铺ID</param>
        /// <param name="vr_Content">回访内容</param>
        /// <param name="vr_Stat">回访状态</param>
        /// <param name="vr_Name">回访人</param>
        /// <param name="reply_Stat">查看状态</param>
        /// <returns></returns>
        public static int AddVisitReply(int vi_id, int accid, string vr_Content, int vr_Stat, string vr_Name, int reply_Stat = 1)
        {
            Sys_VisitReply model = new Sys_VisitReply();
            model.vi_id = vi_id;
            model.accid = accid;
            model.vr_Content = vr_Content;
            model.vr_Stat = vr_Stat;
            model.vr_Name = vr_Name;
            model.reply_Stat = reply_Stat;
            model.vr_Time = DateTime.Now;

            int aId = BLL.Base.Sys_VisitReplyBaseBLL.Add(model);
            if (aId > 0)
            {
                Sys_VisitInfoBLL.UpdateVisitStat(vi_id, vr_Stat);
                return 1;
            }
            else
            {
                return 0;
            }

        }

        /// <summary>
        /// 得到事件列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="dapperWhere"></param>
        /// <param name="filedOrder"></param>
        /// <returns></returns>
        public static Dictionary<string, object> GetCaseList(int pageIndex, int dataType, int classType, string revisitName, DateTime? statTime = null, DateTime? endTime = null)
        {
            List<DapperWhere> dapperWhere = new List<DapperWhere>();
            Dictionary<string, object> list = new Dictionary<string, object>();
            int pageSize = 15;


            if (dataType == 1)
            {
                dapperWhere.Add(new DapperWhere("insertTime", DateTime.Now, "insertTime between CAST(@insertTime as DATE) and @insertTime"));
                dapperWhere.Add(new DapperWhere("handleStat", 1, " handleStat!=@handleStat"));
            }
            else if (dataType == 2)
            {
                dapperWhere.Add(new DapperWhere("insertTime", DateTime.Now.Date, "insertTime < @insertTime"));
                dapperWhere.Add(new DapperWhere("handleStat", 1, " handleStat!=@handleStat"));
            }
            else
            {
                dapperWhere.Add(new DapperWhere("handleStat", 1));
                if (classType != -1)
                {
                    //分类筛选
                    dapperWhere.Add(new DapperWhere("vr_Threeid", classType));
                }
                if (revisitName != "")
                {
                    //回访人员
                    revisitName = "%" + revisitName + "%";
                    dapperWhere.Add(new DapperWhere("insertName", revisitName, " insertName like @insertName"));
                }
            }

            if (statTime != null)
            {
                dapperWhere.Add(new DapperWhere("bgDate", Convert.ToDateTime(statTime).Date, "insertTime >= @bgDate"));
            }
            if (endTime != null)
            {
                dapperWhere.Add(new DapperWhere("edDate", Convert.ToDateTime(endTime).Date.Add(new TimeSpan(23, 59, 59)), "insertTime <= @edDate"));
            }



            int rowCount = 0;
            if (pageIndex == 1)
            {
                rowCount = BLL.Base.Sys_VisitInfoBaseBLL.GetCount(dapperWhere);
            }
            int maxPage = 0;
            if (rowCount > 0)
            {
                maxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(rowCount) / pageSize));
            }

            list["rowCount"] = rowCount;
            list["maxPage"] = maxPage;
            list["pageIndex"] = pageIndex;
            list["listData"] = Sys_VisitInfoBLL.GetCaseList(pageIndex, pageSize, dapperWhere, " id desc");
            return list;
        }

        /// <summary>
        /// 插入论坛建议反馈
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetForumFeedBack(string url, string uName, string remark)
        {
            if (url.IndexOf("www.i200.cn/bbs") < 0)
            {
                return "";//不是合法地址
            }
            //制定编码格式
            var encoding = Encoding.GetEncoding("utf-8");
            //url的设置
            var request = (HttpWebRequest)WebRequest.Create(url);
            //设置请求的方式
            request.Method = "Get";
            //设置Content-Type 的值
            //request.ContentType = "application/x-www-form-urlencoded"; 

            //request.ContentLength = data.Length;
            //var outStream = request.GetRequestStream();
            //outStream.Write(data, 0, data.Length);
            //outStream.Close();
            var response = (HttpWebResponse)request.GetResponse();
            var srContent = new System.IO.StreamReader(response.GetResponseStream(), encoding);
            //获取抓取下来的页面内容
            var strPage = srContent.ReadToEnd();
            response.Close();
            srContent.Close();

            //获取帖子标题
            Regex regTitle = new Regex(@"(?i)(?<=<a.*?id=""thread_subject"".*?>)[^<]+(?=</a>)");
            MatchCollection mcTitle = regTitle.Matches(strPage);

            //获取帖子发表时间
            Regex regTime = new Regex(@"(?i)(?<=<em.*?id=""authorposton.*?>发表于)[^<]+(?=</em>)");
            MatchCollection mcTime = regTime.Matches(strPage);
            if (mcTime.Count == 0)
            {
                regTime = new Regex(@"(?i)(?<=<em.*?id=""authorposton.*?>发表于.*<span.*title="")[^<]+(?="">)");
                mcTime = regTime.Matches(strPage);
            }

            string postUser = strPage.Substring(strPage.IndexOf("class=\"pi\""), 300);

            //获取发帖人
            Regex regUser = new Regex(@"(?i)(?<=<a.*?uid=)[^<]+(?="".*?target)");
            MatchCollection mcUser = regUser.Matches(postUser);
            if (mcUser.Count==0)
            {
                regUser = new Regex(@"(?i)(?<=匿名.*?<em>)[^<]+(?=</em>)");
                mcUser = regUser.Matches(postUser);
            }

            Sys_TaskDailyInfo model = new Sys_TaskDailyInfo();

            try
            {
                model.t_mk = mcTitle[0].Groups[0].ToString();
                model.dt_Time = Convert.ToDateTime(mcTime[0].Groups[0].ToString());
                if (mcUser[0].Groups[0].ToString().IndexOf('.')>=0)
                {
                    model.accountid = 0;
                }
                else
                {
                    try
                    {
                        model.accountid = T_AccountBLL.GetAccIdBybbsId(Convert.ToInt32(mcUser[0].Groups[0].ToString()));
                    }
                    catch (Exception ex)
                    {
                        Logger.Error("转换论坛ID出错！", ex);
                        model.accountid = 0;            
                    }
                    
                }

                model.inertTime = DateTime.Now;
                model.dt_Level = 9;
                model.dt_Status = 99;

                model.insetName = uName;
                model.dt_remark = "@(" + url + ")" + remark;
                model.dt_logUid = 0;
                model.dt_Source = "论坛反馈";

                if (Sys_TaskDailyBLL.CheckForumUrl(url))
                {
                    return "已经存在该条反馈！";
                }
                else
                {
                    if (Sys_TaskDailyBLL.AddModel(model) > 0)
                    {
                        return "添加成功！";
                    }
                    else
                    {
                        return "添加出错！";
                    }
                }
                
            }
            catch (Exception ex)
            {
                Logger.Error("添加论坛反馈出错！",ex);
                return "添加反馈出错！";                
            }

            //return mcTitle[0].Groups[0].ToString() + "  " + mcTime[0].Groups[0].ToString() + "  " + mcUser[0].Groups[0].ToString();
            //foreach (Match m in mc)
            //{
            //    Console.WriteLine(m.Groups[0].ToString());
            //}

            //return strPage;
        }

        /// <summary>
        /// 添加客服回访信息
        /// </summary>
        /// <param name="uName"></param>
        /// <param name="content"></param>
        /// <param name="accId"></param>
        /// <param name="sourceMark">反馈来源信息</param>
        /// <returns></returns>
        public static int AddCustomCareFeedBack(string uName, string content, int accId, int sourceMark = 0)
        {
            Sys_TaskDailyInfo model = new Sys_TaskDailyInfo();

            string source = "";
            switch (sourceMark)
            {
                case 0:
                    source = "客服反馈";
                    break;
                case 1:
                    source = "推广建议";
                    break;
                default:
                    source = "客服反馈";
                    break;
            }

            if (Sys_TaskDailyBLL.CheckTaskDailyExist(content))
            {
                return 2;
            }

            try
            {
                model.t_mk = content;
                model.dt_Time = DateTime.Now;

                model.accountid = accId;

                model.inertTime = DateTime.Now;
                model.dt_Level = 9;
                model.dt_Status = 99;

                model.insetName = uName;
                model.dt_remark = "";
                model.dt_logUid = 0;
                model.dt_Source = source;
                
                if (Sys_TaskDailyBLL.AddModel(model) > 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }


            }
            catch (Exception ex)
            {
                Logger.Error("添加" + source + "出错！", ex);
                return 0;
            }
        }

        

        public static Dictionary<string, string> GetForumContent(string url)
        {
            Dictionary<string,string> dic=new Dictionary<string, string>()
            {
                {"title",""},
                {"user",""}
            };

            if (url.IndexOf("www.i200.cn/bbs") < 0)
            {
                return null;//不是合法地址
            }
            //制定编码格式
            var encoding = Encoding.GetEncoding("utf-8");
            //url的设置
            var request = (HttpWebRequest)WebRequest.Create(url);
            //设置请求的方式
            request.Method = "Get";
            //设置Content-Type 的值
            //request.ContentType = "application/x-www-form-urlencoded"; 

            //request.ContentLength = data.Length;
            //var outStream = request.GetRequestStream();
            //outStream.Write(data, 0, data.Length);
            //outStream.Close();
            var response = (HttpWebResponse)request.GetResponse();
            var srContent = new System.IO.StreamReader(response.GetResponseStream(), encoding);
            //获取抓取下来的页面内容
            var strPage = srContent.ReadToEnd();
            response.Close();
            srContent.Close();

            //获取帖子标题
            Regex regTitle = new Regex(@"(?i)(?<=<a.*?id=""thread_subject"".*?>)[^<]+(?=</a>)");
            MatchCollection mcTitle = regTitle.Matches(strPage);

            //获取帖子发表时间
            Regex regTime = new Regex(@"(?i)(?<=<em.*?id=""authorposton.*?>发表于)[^<]+(?=</em>)");
            MatchCollection mcTime = regTime.Matches(strPage);
            if (mcTime.Count == 0)
            {
                regTime = new Regex(@"(?i)(?<=<em.*?id=""authorposton.*?>发表于.*<span.*title="")[^<]+(?="">)");
                mcTime = regTime.Matches(strPage);
            }

            string postUser = strPage.Substring(strPage.IndexOf("class=\"pi\""), 300);

            //获取发帖人
            Regex regUser = new Regex(@"(?i)(?<=<a.*?uid=)[^<]+(?="".*?target)");
            MatchCollection mcUser = regUser.Matches(postUser);
            if (mcUser.Count == 0)
            {
                regUser = new Regex(@"(?i)(?<=匿名.*?<em>)[^<]+(?=</em>)");
                mcUser = regUser.Matches(postUser);
            }

            Sys_TaskDailyInfo model = new Sys_TaskDailyInfo();

            dic["title"] = mcTitle[0].Groups[0].ToString();
            if (mcUser[0].Groups[0].ToString().IndexOf('.') >= 0)
            {
                dic["user"] = "匿名用户";
            }
            else
            {
                try
                {
                    int accountid = T_AccountBLL.GetAccIdBybbsId(Convert.ToInt32(mcUser[0].Groups[0].ToString()));
                    dic["user"] = T_AccountBLL.GetCompanyName(accountid);
                }
                catch (Exception ex)
                {
                    Logger.Error("转换论坛ID出错！", ex);
                    dic["user"] = "匿名用户";
                }

            }

            return dic;
        }

        public static string StripHTML(string HTML) //google "StripHTML" 得到
        {
            string[] Regexs =
                                {
                                    @"<script[^>]*?>.*?</script>",
                                    @"<(\/\s*)?!?((\w+:)?\w+)(\w+(\s*=?\s*(([""'])(\\[""'tbnr]|[^\7])*?\7|\w+)|.{0})|\s)*?(\/\s*)?>",
                                    @"([\r\n])[\s]+",
                                    @"&(quot|#34);",
                                    @"&(amp|#38);",
                                    @"&(lt|#60);",
                                    @"&(gt|#62);",
                                    @"&(nbsp|#160);",
                                    @"&(iexcl|#161);",
                                    @"&(cent|#162);",
                                    @"&(pound|#163);",
                                    @"&(copy|#169);",
                                    @"&#(\d+);",
                                    @"-->",
                                    @"<!--.*\n"
                                };

            string[] Replaces =
                                {
                                    "",
                                    "",
                                    "",
                                    "\"",
                                    "&",
                                    "<",
                                    ">",
                                    " ",
                                    "\xa1", //chr(161),
                                    "\xa2", //chr(162),
                                    "\xa3", //chr(163),
                                    "\xa9", //chr(169),
                                    "",
                                    "\r\n",
                                    ""
                                };

            string s = HTML;
            for (int i = 0; i < Regexs.Length; i++)
            {
                s = new Regex(Regexs[i], RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(s, Replaces[i]);
            }
            s.Replace("<", "");
            s.Replace(">", "");
            s.Replace("\r\n", ";");
            s.Replace("br", ";");
            return s;
        }
    }
}
