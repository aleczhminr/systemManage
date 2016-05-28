using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// 回访标签短信
    /// </summary>
   public class Sys_VisitTagNexusDAL:Base.Sys_VisitTagNexusBaseDAL
    {
       /// <summary>
       /// 增加新的关系
       /// </summary>
       /// <param name="visitId"></param>
       /// <param name="tagId"></param>
       /// <param name="insertName"></param>
       /// <returns></returns>
       public int Add(int visitId,int tagId,string insertName)
       {
           StringBuilder strSql = new StringBuilder();
           strSql.Append(" declare @nid int; ");
           strSql.Append(" select @nid=id from Sys_VisitTagNexus where tid=@tid and vid=@vid; ");
           strSql.Append(" if(ISNULL(@nid,0)>0) ");
           strSql.Append(" begin ");
           strSql.Append(" 	select @nid; ");
           strSql.Append(" end ");
           strSql.Append(" else ");
           strSql.Append(" begin ");
           strSql.Append(" 	insert into Sys_VisitTagNexus(tid,vid,insertName,insertTime) ");
           strSql.Append(" 	values(@tid,@vid,@iname,GETDATE()); ");
           strSql.Append(" 	select @@IDENTITY; ");
           strSql.Append(" end ");
           object rl = DapperHelper.ExecuteScalar(strSql.ToString(), new { tid = tagId, vid = visitId, iname = insertName });
           if (rl != null)
           {
               return Convert.ToInt32(rl);
           }
           else
           {
               return 0;
           }
       }


       /// <summary>
       /// 得到一个回访的标签
       /// </summary>
       /// <param name="vid"></param>
       /// <returns></returns>
       public List<SysVisitTagItem> GetVisitInfoTagList(int vid)
       {
           StringBuilder strSql = new StringBuilder();
           strSql.Append("select id,tagName from Sys_VisitTag where id in(");
           strSql.Append("select tid from Sys_VisitTagNexus where vid=@vid)");

           return DapperHelper.Query<SysVisitTagItem>(strSql.ToString(), new { vid = vid }).ToList();
       }
    }
}
