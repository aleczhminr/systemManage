using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;

namespace BLL
{
    public static class Sys_Visit_QuestionBLL
    {
        public static UserQuestionGroup GetUserQuestion(int accId)
        {
            Dictionary<string, List<UserQuestion>> dic = new Dictionary<string, List<UserQuestion>>();
            UserQuestionGroup gModel = new UserQuestionGroup();

            Sys_Visit_QuestionDAL dal = new Sys_Visit_QuestionDAL();
            List<UserQuestion> list = dal.GetUserQuestion(accId);

            if (list==null)
            {
                return null;
            }
            else
            {
                List<UserQuestion> common = list.FindAll(x => x.Qtype == 2);    //普通问题
                List<UserQuestion> important = list.FindAll(x => x.Qtype == 1);  //重要问题

                if (common.Count>0)
                {
                    dic["common"] = common;
                }
                else
                {
                    dic["common"] = null;
                }

                if (important.Count > 0)
                {
                    dic["important"] = important;
                }
                else
                {
                    dic["important"] = null;
                }

                gModel.DataList = dic;

                return gModel;
            }


        }

        public static int AddQuestionReply(int accId, int qid, string reply)
        {
            Sys_Visit_QuestionDAL dal = new Sys_Visit_QuestionDAL();
            return dal.AddQuestionReply(accId, qid, reply);
        }
    }
}
