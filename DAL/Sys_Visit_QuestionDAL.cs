using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Utility;

namespace DAL
{
    public class Sys_Visit_QuestionDAL
    {
        /// <summary>
        /// 获取回访页面的用户问题信息
        /// </summary>
        /// <param name="accId"></param>
        /// <returns></returns>
        public List<UserQuestion> GetUserQuestion(int accId)
        {
            StringBuilder strSql = new StringBuilder();

            InitialUserQuestion(accId);

            strSql.Append("select q.Id Qid,q.Qtype,q.Qdesc,q.Qtext,ql.Reply from Sys_Visit_Question q " +
                          "left join Sys_Visit_Question_List ql " +
                          "on ql.Qid=q.Id " +
                          "where q.Status=1 and ql.AccId=@accid " +
                          "order by q.Qorder;");

            try
            {
                return DapperHelper.Query<UserQuestion>(strSql.ToString(), new
                {
                    accid = accId
                }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error("获取用户回访问题出错！", ex);
                return null;
            }
        }

        /// <summary>
        /// 新增/修改用户问题
        /// </summary>
        /// <param name="accId"></param>
        /// <param name="qid"></param>
        /// <param name="reply"></param>
        /// <returns></returns>
        public int AddQuestionReply(int accId, int qid, string reply)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("if exists (select * from Sys_Visit_Question_List where AccId=@accid and Qid=@qid) ");
            strSql.Append(" begin ");
            strSql.Append(" update Sys_Visit_Question_List set Reply=@reply where AccId=@accid and Qid=@qid; ");
            strSql.Append(" end ");
            strSql.Append(" else ");
            strSql.Append(" begin ");
            strSql.Append(" insert into Sys_Visit_Question_List (AccId,Qid,Reply) values (@accid,@qid,@reply) ");
            //strSql.Append(" select @@IDENTITY; ");
            strSql.Append(" end");

            try
            {
                return DapperHelper.Execute(strSql.ToString(), new
                {
                    accid = accId,
                    qid = qid,
                    reply = reply
                });
            }
            catch (Exception ex)
            {
                Logger.Error("更新用户回访问题出错！", ex);
                return 0;
            }
        }

        /// <summary>
        /// 初始化用户问题信息
        /// </summary>
        /// <param name="accId"></param>
        /// <returns></returns>
        public void InitialUserQuestion(int accId)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("if exists (select * from Sys_Visit_Question_List where AccId=@accid) ");
            strSql.Append(" begin ");
            strSql.Append(" select 1; ");
            strSql.Append(" end ");
            strSql.Append(" else ");
            strSql.Append(" begin ");
            strSql.Append(" select 0 ");
            //strSql.Append(" select @@IDENTITY; ");
            strSql.Append(" end");

            int mark = DapperHelper.ExecuteScalar<int>(strSql.ToString(), new {accid = accId});

            if (mark == 0)
            {
                strSql.Clear();

                strSql.Append("select * from Sys_Visit_Question where Status=1;");
                List<Sys_Visit_Question> list = DapperHelper.Query<Sys_Visit_Question>(strSql.ToString()).ToList();

                strSql.Clear();
                foreach (var item in list)
                {
                    strSql.Append("insert into Sys_Visit_Question_List (AccId,Qid,Reply) Values (@accid," + item.Id +
                                  ",'');");
                }

                DapperHelper.Execute(strSql.ToString(), new {accid = accId});
            }
        }
    }
}
