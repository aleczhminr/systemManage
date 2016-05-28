using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Utility;

namespace DAL
{
    public class MessageTemplateDAL
    {
        /// <summary>
        /// 添加新的用户模板
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string AddTemplate(TriggerTemplateModel model)
        {
            StringBuilder strSql = new StringBuilder();

            //如果已经存在则禁用掉
            if (CheckTemplateExist(model.EventId))
            {
                UnactiveOldTemplate(model.EventId);
            }

            strSql.Append("insert into Sys_MessageNotify (MissionTarget,MissionName,UserDesc," +
                          "SmsMark,MobileMark,WebMark,EmailMark,SmsContent,MobileTitle,MobileContent," +
                          "MobileContentType,MobileContentUrl,EmailTitle,EmailContent,WebTitle,WebContent,CreateTime,Operator,EnableStatus,EventId) " +
                          "Values (@MissionTarget,@MissionName,@UserDesc," +
                          "@SmsMark,@MobileMark,@WebMark,@EmailMark,@SmsContent,@MobileTitle,@MobileContent," +
                          "@MobileContentType,@MobileContentUrl,@EmailTitle,@EmailContent,@WebTitle,@WebContent,@CreateTime,@Operator,@EnableStatus,@EventId);");
            try
            {
                return DapperHelper.Execute(strSql.ToString(), model).ToString();
            }
            catch (Exception ex)
            {
                Logger.Error("插入触发类消息模板出错", ex);
                return "0";
            }
        }

        /// <summary>
        /// 确认是否存在事件对应的模板
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public bool CheckTemplateExist(int eventId)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select count(eventId) from Sys_MessageNotify where eventId=@eventId and EnableStatus=1;");

            int reVal = DapperHelper.ExecuteScalar<int>(strSql.ToString(), new { eventId = eventId });
            if (reVal > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 禁用老模板
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public int UnactiveOldTemplate(int eventId)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("update Sys_MessageNotify set EnableStatus=0 where eventId=@eventId;");

            return DapperHelper.Execute(strSql.ToString(), new { eventId = eventId });
        }

        /// <summary>
        /// 根据Id获取模板
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TriggerTemplateModel GeTemplateModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Sys_MessageNotify where EventId=@id and EnableStatus=1;");

            try
            {
                return DapperHelper.GetModel<TriggerTemplateModel>(strSql.ToString(), new { id = id });
            }
            catch (Exception ex)
            {
                Logger.Error("获取模板信息出错！", ex);
                return null;
            }
        }

        /// <summary>
        /// 获取全部的Kafka定义事件
        /// </summary>
        /// <returns></returns>
        public List<Sys_KafkaEvent> GetAllKafkaEvent()
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select * from Sys_KafkaEvent;");

            try
            {
                return DapperHelper.Query<Sys_KafkaEvent>(strSql.ToString()).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error("获取kafka定义事件出错！", ex);
                return null;
            }
        }

        /// <summary>
        /// 获取消息发送模板列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="sendMethod"></param>
        /// <param name="target"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public ListView GetTemplateList(int pageIndex, string[] sendMethod, string target, string name)
        {
            ListView viewModel = new ListView();

            StringBuilder strSql = new StringBuilder();
            StringBuilder strCount = new StringBuilder();

            int bgNumber = ((pageIndex - 1) * 15) + 1;
            int edNumber = (pageIndex) * 15;

            string where = " where EnableStatus=1 ";
            #region 拼接模板条件
            //任务目标
            if (!string.IsNullOrEmpty(target))
            {
                where += " and MissionTarget='" + target + "' ";
            }
            //任务名称
            if (!string.IsNullOrEmpty(name))
            {
                where += " and MissionName like '%" + name + "%' ";
            }
            //发送渠道
            if (sendMethod != null && sendMethod.Length > 0)
            {
                foreach (var item in sendMethod)
                {
                    where += " and " + item + " =1 ";
                }
            }

            #endregion

            strSql.Append(
                "select * from (select ROW_NUMBER() over (order by id desc) rowNumber,* from Sys_MessageNotify " + where +
                ") T where T.rowNumber between @bgNumber and @edNumber;");

            strCount.Append("select count(*) from Sys_MessageNotify " + where);

            try
            {
                viewModel.dataList = DapperHelper.Query<TriggerTemplateModel>(strSql.ToString(), new
                {
                    bgNumber = bgNumber,
                    edNumber = edNumber
                }).ToList();

                viewModel.count = DapperHelper.ExecuteScalar<int>(strCount.ToString(), new
                {
                    bgNumber = bgNumber,
                    edNumber = edNumber
                });

                if (viewModel.count<15)
                {
                    viewModel.maxPage = 1;
                }
                else
                {
                    viewModel.maxPage = viewModel.count % 15 == 0 ? viewModel.count % 15 : (viewModel.count % 15 + 1);    
                }
                

                return viewModel;
            }
            catch (Exception ex)
            {
                Logger.Error("获取消息模板列表出错！", ex);
                return null;
            }
        }
    }
}
