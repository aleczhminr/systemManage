using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;

namespace Controls.Recommend
{
    public static class Recommend
    {
        public static string GetRecList(int page, int type, DateTime date, int accid)
        {
            return CommonLib.Helper.JsonSerializeObject(T_RecommendBLL.GetRecModel(page, type, date, accid),"yyyy-MM-dd HH:mm:ss");
        }
    }
}
