using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class T_MaterialGoodsDAL
    {
        /// <summary>
        /// 根据ID获取实物列表
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<MaterialGoodsBaseModel> GetMaterialGoodsName(string where)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select GoodsId,ImagePath,AliasName from T_MaterialGoods ");
            strSql.Append(" where GoodsId in ( " + where + " )");
            List<MaterialGoodsBaseModel> listitem = new List<MaterialGoodsBaseModel>();
            listitem = HelperForFrontend.Query<MaterialGoodsBaseModel>(strSql.ToString()).ToList();
            return listitem;
        }
    }
}
