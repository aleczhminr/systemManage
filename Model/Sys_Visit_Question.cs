using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Sys_Visit_Question
    {
        public int Id { get; set; }
        /// <summary>
        /// 问题类型
        /// </summary>
        public int Qtype { get; set; }
        /// <summary>
        /// 问题描述
        /// </summary>
        public string Qdesc { get; set; }
        /// <summary>
        /// 问题内容
        /// </summary>
        public string Qtext { get; set; }
        /// <summary>
        /// 问题排序
        /// </summary>
        public int Qorder { get; set; }
        /// <summary>
        /// 问题状态
        /// </summary>
        public int Status { get; set; }
    }

    public class Sys_Visit_Question_List
    {
        public int Id { get; set; }
        public int AccId { get; set; }
        /// <summary>
        /// 问题Id
        /// </summary>
        public int Qid { get; set; }
        /// <summary>
        /// 客服记录
        /// </summary>
        public string Reply { get; set; }
    }

    public class UserQuestion
    {
        public int Qid { get; set; }

        /// <summary>
        /// 问题类型
        /// </summary>
        public int Qtype { get; set; }
        /// <summary>
        /// 问题描述
        /// </summary>
        public string Qdesc { get; set; }
        /// <summary>
        /// 问题内容
        /// </summary>
        public string Qtext { get; set; }
        /// <summary>
        /// 客服记录
        /// </summary>
        public string Reply { get; set; }
    }

    public class UserQuestionGroup
    {
        public UserQuestionGroup()
        {
            DataList = new Dictionary<string, List<UserQuestion>>();
        }

        public Dictionary<string, List<UserQuestion>> DataList { get; set; }
    }
}
