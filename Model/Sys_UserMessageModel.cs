using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class UserMessageModel
    {
        public int RowCount { get; set; }
        public int PageCount { get; set; }
        public List<Sys_UserMessageModel> DataList { get; set; }
    }

    public class Sys_UserMessageModel
    {
        public int Id { get; set; }
        public string MsgKey { get; set; }
        public int Accid { get; set; }
        public int ChannelId { get; set; }
        public int IsPush { get; set; }
        public int IsRead { get; set; }
        public DateTime PushTime { get; set; }
        public string Title { get; set; }
        public string PushContent { get; set; }
        public int Operator { get; set; }
        public string OperatorName { get; set; }
    }
}
