using Model;
using Model.O2O;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.O2O
{
    /// <summary>
    /// 商品信息
    /// </summary>
   public static class GoodsInfo
    {

        

       /// <summary>
       /// 得到短信列表
       /// </summary>
       /// <param name="pageIndex"></param>
       /// <param name="pageSize"></param>
       /// <param name="accid"></param>
       /// <returns></returns>
       public static Dictionary<string, object> GetList(int pageIndex, int pageSize, int isPic, string gname = "", int? start = null, int? end = null, string isToday="0")
       {

           Dictionary<string, object> list = new Dictionary<string, object>();

           List<DapperWhere> sqlWhere = new List<DapperWhere>();

           if (start != null)
           {
               sqlWhere.Add(new DapperWhere("startNum", start, "accountNum>=@startNum"));
           }
           if (end != null)
           {
               sqlWhere.Add(new DapperWhere("endNum", start, "accountNum<=@endNum"));
           }
           if (isPic >= 0)
           {
               sqlWhere.Add(new DapperWhere("isPic", isPic));
           }

           if (gname != null && gname!="")
           {
               sqlWhere.Add(new DapperWhere("gName", gname, " gName like '%'+ @gName +'%' "));
           }

           //if (isToday == "1")
           //{
           //    sqlWhere.Add(new DapperWhere("gName", gname, " gName like '%'+ @gName +'%' "));
           //}

           if (pageSize < 1)
           {
               pageSize = 20;
           }

           int rowCount = 0;
           if (pageIndex == 1)
           {
               rowCount =BLL.O2O.NewGoodsInfoBaseBLL.GetCount(sqlWhere);
           }
           int maxPage = 0;
           if (rowCount > 0)
           {
               maxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(rowCount) / pageSize));
           }

           list["rowCount"] = rowCount;
           list["maxPage"] = maxPage;
           list["pageIndex"] = pageIndex;
           list["listData"] =BLL.O2O.NewGoodsInfoBaseBLL.GetList(pageIndex, pageSize, sqlWhere, "id desc");

           return list;
       }

       /// <summary>
       /// 得到商品图片
       /// </summary>
       /// <param name="gid"></param>
       /// <returns></returns>
       public static List<goodsPic> GetPic(int[] gid,int[] ygid)
       {
           string gidS = "";
           foreach (int g in gid)
           {
               gidS += g.ToString() + ",";
           }
           string ygidS = "";
           foreach (int g in ygid)
           {
               ygidS += g.ToString() + ",";
           }

           List<DapperWhere> sqlWhere = new List<DapperWhere>();
           if (gidS.Length > 0)
           {
               sqlWhere.Add(new DapperWhere("gid", gidS.Trim(','), "gid in (" + gidS.Trim(',') + ")"));
           }
           if (ygidS.Length > 0)
           {
               sqlWhere.Add(new DapperWhere("ygid", gidS.Trim(','), "ygid in (" + ygidS.Trim(',') + ")"));
           }
           List<goodsPic> list = BLL.O2O.goodsPicBaseBLL.GetList(0, sqlWhere, "picOrder desc,id desc");
           return list;
       }
       public static List<NewGoodsList> GetGoodsInfoList(int gid)
       {
           List<DapperWhere> sqlWhere = new List<DapperWhere>();
           sqlWhere.Add(new DapperWhere("gid", gid.ToString()));

           return BLL.O2O.NewGoodsListBaseBLL.GetList(sqlWhere);

       }
    }
}
