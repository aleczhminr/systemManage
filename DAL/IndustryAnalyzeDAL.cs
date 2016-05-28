using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DAL
{
    public class IndustryAnalyzeDAL
    {
        //获取用户老的行业信息
        public List<IndustryAnalyzeModel> GetUserIndustry()
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select UserId AccId,ShopType Industry from [i200].[dbo].[tb_user_infor] " +
                          "where ShopType<>'' and ShopType is not null;");

            return DapperHelper.Query<IndustryAnalyzeModel>(strSql.ToString()).ToList();
        }
    }
}
