using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;

namespace BLL
{
    public static class IntegralStatBLL
    {
        public static IntegralPieModel GetIntegralPieModel(DateTime stDate, DateTime edDate)
        {
            IntegralPieModel model = new IntegralPieModel();
            IntegralStatDAL dal = new IntegralStatDAL();

            model.ExRatio = dal.GetExRatio(stDate, edDate);
            model.GoodsType = dal.GetGoodsType(stDate, edDate);
            //model.VisitRatio = dal.GetVisitRatio(stDate, edDate);
            model.VisitRatio = dal.GetPaidType(stDate, edDate)["fData"];
            model.VisitRatioSec = dal.GetPaidType(stDate, edDate)["sData"];

            return model;
        }
    }
}
