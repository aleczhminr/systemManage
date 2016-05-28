using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class IntegralExchangeModel
    {
        public IntegralExchangeModel()
        {
            DataList = new List<Sys_DailyIntegralExchange>();
        }

        public DateTime DayTime { get; set; }
        public List<Sys_DailyIntegralExchange> DataList { get; set; }
    }

    public class Sys_DailyIntegralExchange
    {
        public int Id { get; set; }
        public DateTime DayDate { get; set; }
        public string ProductName { get; set; }
        public int VisitNum { get; set; }
        public int ExchangeNum { get; set; }
        public decimal Ratio { get; set; }
        public int ProductId { get; set; }
    }
}
