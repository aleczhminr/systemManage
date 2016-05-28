using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class AgentModel
    {
        public AgentModel()
        {
        }

        public int ID { get; set; }
        public string AgentId { get; set; }
        public string AgentPassword { get; set; }
        public string AgentName { get; set; }
        public int AgentGrade { get; set; }
        public string AgentPhone { get; set; }
        public string AgentAddress { get; set; }
        public string AgentIdCard { get; set; }
        public string AgentNumber { get; set; }
        public string AgentEmail { get; set; }
        public string AgentQQ { get; set; }
        public int AgentLoginTimes { get; set; }
        public DateTime AgentLastLoginTime { get; set; }
        public string Creater { get; set; }
        public string CreatingDate { get; set; }
        public string Remark { get; set; }
        public string AgentLink { get; set; }
        public int bindAccID { get; set; }
        public string bindUser { get; set; }
        public int activeStatus { get; set; }
        public int AgentGroup { get; set; }
        /// <summary>
        /// 代理商分类（1代表代理商，2代表服务商）
        /// </summary>
        public int ServiceType { get; set; }
    }

    public class AgentList
    {
        public AgentList()
        {
            maxPage = 0;
            rowCount = 0;
            list = new List<AgentModel>();
        }

        public int maxPage { get; set; }
        public int rowCount { get; set; }
        public List<AgentModel> list { get; set; }
    }
}
