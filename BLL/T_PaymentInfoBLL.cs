using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;

namespace BLL
{
    public static class T_PaymentInfoBLL
    {
        public static int Add(AlipayUserInfo model)
        {
            T_PaymentInfoDAL dal = new T_PaymentInfoDAL();
            return dal.Add(model);
        }
    }
}
