
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public static class T_LogInfoBLL
    {
        public static List<dynamic> GetIntegrals(DateTime stDate, DateTime edDate)
        {
            T_LogInfoDAL dal = new T_LogInfoDAL();
            return dal.GetIntegrals(stDate, edDate);
        }
    }
}
