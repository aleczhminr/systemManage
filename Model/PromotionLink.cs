using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class PromotionLinkList
    {
        public PromotionLinkList()
        {
            DataList = new List<PromotionLink>();
        }

        public List<PromotionLink> DataList { get; set; }
    }

    public class PromotionLink
    {
        public int LinkType { get; set; }
        public string LinkName { get; set; }
        public string LinkUrl { get; set; }
        public string Remark { get; set; }
        public int State { get; set; }
        public string ManagerId { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
