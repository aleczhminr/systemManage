using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// 短信预告
    /// </summary>
   public class T_NoticeTextDAL:Base.T_NoticeTextBaseDAL
   {
       #region GetNoticeText 获取通知消息
       /// <summary>
       /// 获取通知消息
       /// </summary>
       /// <param name="typeId">类型Id</param>
       /// <returns></returns>
       public NoticeTextModel GetNoticeText(int typeId)
       {  
           var strSql = new StringBuilder();

           strSql.Append(" SELECT top(1) Id, nType Type, nDisplay Display, nTitle Title, nContent Content, nTime DateTime, nOperatorName, nOperatorIp");
           strSql.Append(" FROM i200.dbo.T_NoticeText");
           strSql.Append(" where nType=@nType");
           strSql.Append(" order by id desc");
           return DapperHelper.GetModel< NoticeTextModel>(strSql.ToString(), new { nType = typeId });
       }
       #endregion

       #region SaveNoticeText 保存短信通告信息
       /// <summary>
       /// 保存短信通告信息
       /// </summary>
       /// <param name="noticeType">通告类型</param>
       /// <param name="noticeId">Id</param>
       /// <param name="flagEdit">修改标记</param>
       /// <param name="display">显示设置</param>
       /// <param name="noticeText">通告内容</param>
       /// <returns></returns>
       public int SaveNoticeText(int noticeType, int noticeId, int flagEdit, int display, string noticeText, string opName, string opIp)
       {
           int iResult = -1;
           var strSql = new StringBuilder();

           if (flagEdit == 1)
           {
               //新增通告信息
               strSql.Append(" INSERT INTO T_NoticeText(nType, nDisplay, nTitle, nContent, nTime, nOperatorName, nOperatorIp)");
               strSql.Append(" VALUES (@nType,@nDisplay,@nTitle,@nContent,@nTime,@nOperatorName,@nOperatorIp)");
           }
           else
           {
               //更新信息
               strSql.Append(" UPDATE T_NoticeText");
               strSql.Append(" SET nDisplay = @nDisplay, nContent = @nContent");
               strSql.Append(" where id= @noticeId");
           }


           int rowAffected = HelperForFrontend.Execute(strSql.ToString(), new
           {
               noticeId = noticeId,
               nType = noticeType,
               nDisplay = display,
               nTitle = "",
               nContent = noticeText,
               nTime = DateTime.Now,
               nOperatorName = opName,
               nOperatorIp = opIp
           });
           if (rowAffected > 0)
           {
               iResult = 1;
           }

           return iResult;
       }
       #endregion
    }
}
