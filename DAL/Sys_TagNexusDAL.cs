using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
namespace DAL
{
    /// <summary>
    /// 店铺标签关系
    /// </summary>
   public class Sys_TagNexusDAL:Base.Sys_TagNexusBaseDAL
    {
       /// <summary>
       /// 根据店铺ID 得到标签信息
       /// </summary>
       /// <param name="accid"></param>
       /// <returns></returns>
       public List<Sys_TagInfoBasic> GetTagNexusByAccId(int accid)
       {
           StringBuilder strSql = new StringBuilder();
           strSql.Append("select DATEDIFF(SECOND,n.insertTime,getdate()) timediff,i.id,t_Name,t_Color,t_BgColor,t_order,tagType,tagTypeid from Sys_TagNexus n left join Sys_TagInfo i on n.tag_id=i.id where n.acc_id=@accid and i.tagStatus=1 order by t_order; ");
           return DapperHelper.Query<Sys_TagInfoBasic>(strSql.ToString(), new { accid = accid }).ToList();
       }

       /// <summary>
       /// 增加一条数据
       /// </summary>
       public new int Add(Sys_TagNexus model)
       {
           StringBuilder strSql = new StringBuilder();

           strSql.Append("if(exists(select * from Sys_TagNexus where acc_id=@acc_id and tag_id=@tag_id))");
           strSql.Append(" begin ");
           strSql.Append(" 	select -1; ");
           strSql.Append(" end ");
           strSql.Append(" else ");
           strSql.Append(" begin ");
           strSql.Append("insert into Sys_TagNexus(");
           strSql.Append("tag_id,acc_id,insertName,insertTime");
           strSql.Append(") values (");
           strSql.Append("@tag_id,@acc_id,@insertName,@insertTime");
           strSql.Append("); ");
           strSql.Append("select @@IDENTITY;");
           strSql.Append(" end ");

           object obj = DapperHelper.ExecuteScalar(strSql.ToString(), model);  
           if (obj == null)
           {
               return 0;
           }
           else
           {

               return Convert.ToInt32(obj);

           }

       }

       /// <summary>
       /// 移除标签
       /// </summary>
       /// <param name="accid"></param>
       /// <param name="tagid"></param>
       /// <returns></returns>
       public bool RemoveTag(int accid, int tagid)
       {
           StringBuilder strSql = new StringBuilder();
           strSql.Append("delete Sys_TagNexus where acc_id=@accId and tag_id=@tagId");
           int r = DapperHelper.Execute(strSql.ToString(), new
           {
               accId = accid,
               tagId = tagid
           });
           if (r > 0)
           {
               return true;
           }
           else
           {
               return false;
           }
       }
    }
}
