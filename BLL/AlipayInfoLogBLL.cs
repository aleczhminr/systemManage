using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public static class AlipayInfoLogBLL
    {
        public static bool AddAlipayInfoLogBase(T_AlipayInfoLogModel model)
        {
            var dal = new AlipayInfoLogDAL();
            return dal.AddAlipayInfoLogBase(model);
        }
    }
}
