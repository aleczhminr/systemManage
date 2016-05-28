using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class SynthesisIFilterLogBLL
    {
        /// <summary>
        /// 得到统计中的 会员
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="verif"></param>
        /// <returns></returns>
        public static string GetAccountList(int uid, string verif)
        {
            SynthesisIFilterLogDAL dal = new SynthesisIFilterLogDAL();
            return dal.GetAccountList(uid, verif);
        }
    }
}
