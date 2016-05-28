using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class CarePercentModel
    {
        public CarePercentModel()
        {
            ListData = new List<PercentPart>();
        }

        public List<PercentPart> ListData { get; set; }
    }

    public class PercentPart
    {
        public PercentPart(string series, int part,int all,string percent)
        {
            this.Series = series;
            this.PartCount = part;
            this.AllCount = all;
            this.Percent = percent;
        }

        public string Series { get; set; }
        public int PartCount { get; set; }
        public int AllCount { get; set; }
        public string Percent { get; set; }
    }
}
