using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;

namespace BLL
{
    public static class IndustryFilterBLL
    {
        public static int UpdateIndustryAccount()
        {
            IndustryFilterDAL dal = new IndustryFilterDAL();
            return dal.UpdateIndustryAccount();
        }

        public static int UpdateIndustryByExtendinfo()
        {
            IndustryFilterDAL dal = new IndustryFilterDAL();
            return dal.UpdateIndustryByExtendinfo();
        }

        public static int UpdateFormerShopType()
        {
            IndustryFilterDAL dal = new IndustryFilterDAL();
            return dal.UpdateFormerShopType();
        }

        public static List<IndustryFilterDic> GetFilterDic()
        {
            IndustryFilterDAL dal = new IndustryFilterDAL();
            return dal.GetFilterDic();
        }

        public static int FilterBaseIndustry(ShopExtIndustry model)
        {
            IndustryFilterDAL dal = new IndustryFilterDAL();
            return dal.FilterBaseIndustry(model);
        }

        public static int UpdateIndustryFilterLog(IndustryFilterLog model)
        {
            IndustryFilterDAL dal = new IndustryFilterDAL();
            return dal.UpdateIndustryFilterLog(model);
        }

        public static ShopIndustryDic GetIndustryPairDic(int id)
        {
            IndustryFilterDAL dal = new IndustryFilterDAL();
            return dal.GetIndustryPairDic(id);
        }

        public static int UpdateExtIndustry(ShopExtIndustry model)
        {
            IndustryFilterDAL dal = new IndustryFilterDAL();
            return dal.UpdateExtIndustry(model);
        }

        public static List<ShopNamePair> GetAccIdPair()
        {
            IndustryFilterDAL dal = new IndustryFilterDAL();
            return dal.GetAccIdPair();
        }

        public static int UpdateFilterStatus(int accid)
        {
            IndustryFilterDAL dal = new IndustryFilterDAL();
            return dal.UpdateFilterStatus(accid);
        }
    }
}
