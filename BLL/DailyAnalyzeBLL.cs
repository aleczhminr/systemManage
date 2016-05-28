using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAL;

namespace BLL
{
    public static class DailyAnalyzeBLL
    {
        public static List<IEnumerable<dynamic>> GetDailyAnalyzeList(int page, int source, string column, string whereStr, DateTime dateTime, string orderWhere)
        {
            DailyAnalyzeDAL dal = new DailyAnalyzeDAL();
            return dal.GetDailyAnalyzeList(page, source, column, whereStr, dateTime, orderWhere);
        }
    }
}
