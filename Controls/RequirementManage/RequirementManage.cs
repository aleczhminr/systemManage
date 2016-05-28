using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using Controls.MessageCenter;
using Model;
using Utility;

namespace Controls.RequirementManage
{
    public static class RequirementManage
    {

        public static string AddNewReqModel(int accId, int refId, int cateId, int reqType, string desc, int op, string originDesc, int device, int val, int diff)
        {
            Model.RequirementManage model = new Model.RequirementManage();

            model.AccId = accId.ToString();
            model.RefId = refId;
            model.CategoryId = cateId;
            model.RequirementType = reqType;
            model.Description = desc;
            model.Operator = op;
            model.Status = 0;
            model.OriginDesc = originDesc;

            model.Terminal = device;
            model.UserVal = val;
            model.Difficult = diff;

            int reqId = RequirementManageBLL.AddNewModel(model);
            if (reqId != 0)
            {
                if (Sys_TaskDailyBLL.UpdateReqId(reqId, refId) != 0)
                {
                    try
                    {
                        #region Kafka Message 反馈转为需求后

                        AfterImportReq iModel = new AfterImportReq();
                        iModel.EventId = 6;
                        iModel.AccId = accId;

                        iModel.RequirementDesc = desc;

                        string specModel = CommonLib.Helper.JsonSerializeObject(iModel);

                        KafkaMessage mSend = new KafkaMessage();
                        mSend.SendMsg(6, specModel);

                        #endregion
                    }
                    catch (Exception ex)
                    {
                        Logger.Error("反馈转为需求后推送模板消息错误", ex);
                    }

                    return "添加成功！";
                }
                else
                {
                    return "添加关联信息出错！请联系技术~";
                }
            }
            else
            {
                return "添加新的需求记录出错！请联系技术~";
            }
        }

        /// <summary>
        /// 获取需求类型列表
        /// </summary>
        /// <returns></returns>
        public static string GetReqList()
        {
            List<RequirementCategory> list = RequirementManageBLL.GetCateList();
            if (list != null && list.Count > 0)
            {
                return CommonLib.Helper.JsonSerializeObject(list);
            }
            else
            {
                return "";
            }
        }

        public static string AddNewCategory(string cateName)
        {
            RequirementCategory model = new RequirementCategory();

            int reVal = RequirementManageBLL.AddNewCategoryItem(cateName);
            if (reVal != 0)
            {
                model.ActiveStatus = 1;
                model.CategoryName = cateName;
                model.Id = reVal;
                model.ParentCategoryId = 0;

                return CommonLib.Helper.JsonSerializeObject(model);
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 获取需求列表 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="stDate"></param>
        /// <param name="edDate"></param>
        /// <param name="reqType"></param>
        /// <param name="status"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string GetRequireList(int pageIndex, string stDate, string edDate, int reqType,
            int status, string content,int terminal)
        {
            Dictionary<string, object> dicData = RequirementManageBLL.GetRequireList(pageIndex, stDate, edDate,
                    reqType, status, content, terminal);

            if (dicData != null)
            {
                int rowCount = Convert.ToInt32(dicData["count"]);
                int maxPage = rowCount % 15 == 0 ? rowCount / 15 : (rowCount / 15 + 1);

                dicData["maxPage"] = maxPage;

                List<Model.RequirementManage> list = (List<Model.RequirementManage>) dicData["data"];
                foreach (var item in list)
                {
                    item.OpName = Sys_Manage_UserBLL.GetManageUserNameById(item.Operator);
                    item.CateName = RequirementManageBLL.GetCateNameById(item.CategoryId);
                }
            }

            return
                CommonLib.Helper.JsonSerializeObject(dicData,"yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 变更任务状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string ChangeTaskStatus(int id, int type, string desc, string accid)
        {
            string stat = RequirementManageBLL.ChangeTaskStatus(id, type, desc);

            if (stat != "0" && !string.IsNullOrEmpty(accid) && type == 3)
            {
                try
                {
                    #region Kafka Message 反馈转为需求后

                    AfterImportReq iModel = new AfterImportReq();
                    iModel.EventId = 7;
                    iModel.AccId = Convert.ToInt32(accid);

                    iModel.RequirementDesc = desc;

                    string specModel = CommonLib.Helper.JsonSerializeObject(iModel);

                    KafkaMessage mSend = new KafkaMessage();
                    mSend.SendMsg(7, specModel);

                    #endregion
                }
                catch (Exception ex)
                {
                    Logger.Error("反馈转为需求后推送模板消息错误", ex);
                }
            }

            return stat;
            
        }

        /// <summary>
        /// 获取任务提示
        /// </summary>
        /// <param name="device"></param>
        /// <param name="cate"></param>
        /// <param name="module"></param>
        /// <param name="val"></param>
        /// <param name="diff"></param>
        /// <returns></returns>
        public static string GetDesc(int device, int cate, int module, int val, int diff)
        {
            return RequirementManageBLL.GetDesc(device, cate, module, val, diff);
        }
    }
}
