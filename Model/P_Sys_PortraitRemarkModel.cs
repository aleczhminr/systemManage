using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class P_Sys_PortraitRemarkModel
    {
        public int Id { get; set; }
        public int ItemType { get; set; }
        public string Keyword { get; set; }
        public string InsertName { get; set; }
        public DateTime InsertTime { get; set; }
    }

    public class RemarkList
    {
        public RemarkList()
        {
            DataList = new List<P_Sys_PortraitRemarkModel>();
        }

        public List<P_Sys_PortraitRemarkModel> DataList { get; set; }
    }
}
