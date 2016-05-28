using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;

namespace BLL
{
    public static class SourceDataBLL
    {
        public static DxChartModel GetSourceData(DateTime stDate, DateTime edDate, string[] sourceType,
            string[] conditions)
        {
            SourceDataDAL dal=new SourceDataDAL();
            return dal.GetDataSource(stDate, edDate, sourceType,
                conditions);
        }
    }
}
