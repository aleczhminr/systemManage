using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
  public  class T_Goods_AttributeDAL
    {
      /// <summary>
      /// 新增
      /// </summary>
      /// <param name="model"></param>
      /// <returns></returns>
      public int Add(T_Goods_Attribute model)
      {
          StringBuilder strSql = new StringBuilder();
          strSql.Append("insert into T_Goods_Attribute(");
          strSql.Append("gaName,gaType,gaRemark,gaTime,gaAlive,accId");
          strSql.Append(") values (");
          strSql.Append("@gaName,@gaType,@gaRemark,@gaTime,@gaAlive,@accId");
          strSql.Append(") ");
          strSql.Append(";select @@IDENTITY");
          object obj = HelperForFrontend.ExecuteScalar(strSql.ToString(), model);
          if (obj != null)
          {
              return Convert.ToInt32(obj);
          }
          else
          {
              return 0;
          }
      }

    }
}
