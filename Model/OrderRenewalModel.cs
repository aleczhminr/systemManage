using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class OrderRenewalModel
    {
        public OrderRenewalModel()
        {
            ExAccids = new List<int>();
            ReAccids = new List<int>();
            ExpireUsr = 0;
            RenewalUsr = 0;
            NotReUsr = 0;
            Ratio = 0;
        }

        public string Date { get; set; }
        public int ExpireUsr { get; set; }
        public int RenewalUsr { get; set; }
        public int NotReUsr { get; set; }
        public List<int> ExAccids { get; set; }
        public List<int> ReAccids { get; set; }
        public decimal Ratio { get; set; }
        public string ExAccidStr { get; set; }
        public string ReAccidStr { get; set; }
        public string NotReAccidStr { get; set; }
        public string DailyFlag { get; set; }
    }

    public class EndTimeModel
    {
        public DateTime EndTime { get; set; }
        public int AccId { get; set; }
    }
}
