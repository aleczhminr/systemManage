using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public static class T_Product_EvaluationBLL
    {
        public static int GetPageCount(string strWhere)
        {
            T_Product_EvaluationDAL dal = new T_Product_EvaluationDAL();
            return dal.GetPageCount(strWhere);
        }

        public static List<EvaluationModel> GetPage(int pageIndex, int pageSize, string Column, string strWhere)
        {
            T_Product_EvaluationDAL dal = new T_Product_EvaluationDAL();
            List<EvaluationModel> listitem = dal.GetPage(pageIndex, pageSize, Column, strWhere);
            string projectwhere = string.Empty;
            string materialGoodswhere = string.Empty;
            foreach (EvaluationModel item in listitem)
            {
                item.statusName = Enum.GetName(typeof(Model.Enum.EvaluationEnum.isDisplay), item.isDisplay);
                if (item.productType == 1)
                {
                    projectwhere += item.productID + ",";
                }
                if (item.productType == 2)
                {
                    materialGoodswhere += item.productID + ",";
                }
            }
            if (materialGoodswhere.Length > 0)
            {
                materialGoodswhere = materialGoodswhere.Substring(0, materialGoodswhere.LastIndexOf(','));
                T_MaterialGoodsDAL mgdal = new T_MaterialGoodsDAL();
                List<MaterialGoodsBaseModel> materiallistitem = mgdal.GetMaterialGoodsName(materialGoodswhere);
                foreach (EvaluationModel eitem in listitem)
                {
                    foreach (MaterialGoodsBaseModel oitem in materiallistitem)
                    {
                        if (eitem.productID == oitem.GoodsId)
                        {
                            eitem.productName = oitem.AliasName;
                        }
                    }
                }
            }
            if (projectwhere.Length > 0)
            {
                projectwhere = projectwhere.Substring(0, projectwhere.LastIndexOf(','));
                T_Order_ProjectDAL opdal = new T_Order_ProjectDAL();
                List<Order_Project_Model> projectlistitem = opdal.GetProjectProductName(projectwhere);
                foreach (EvaluationModel eitem in listitem)
                {
                    foreach (Order_Project_Model oitem in projectlistitem)
                    {
                        if (eitem.productID == oitem.busId)
                        {
                            eitem.productName = oitem.displayName;
                        }
                    }
                }
            }
            return listitem;
        }

        public static bool UpdateEvaluation(int evaluationid, int status)
        {
            T_Product_EvaluationDAL dal = new T_Product_EvaluationDAL();
            return dal.UpdateEvaluation(evaluationid, status);
        }
    }
}
