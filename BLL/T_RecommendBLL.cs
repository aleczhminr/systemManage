using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;

namespace BLL
{
    public static class T_RecommendBLL
    {
        public static RecommendModel GetRecModel(int page, int type, DateTime date, int accid)
        {
            T_RecommendDAL dal = new T_RecommendDAL();
            return dal.GetRecList(page, type, date, accid);
        }
    }
}
