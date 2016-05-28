using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BLL
{
    /// <summary>
    /// 短信预告
    /// </summary>
    public static class T_NoticeTextBLL
    {
        #region GetNoticeText 获取通知消息
        /// <summary>
        /// 获取通知消息
        /// </summary>
        /// <param name="typeId">类型Id</param>
        /// <returns></returns>
        public static  NoticeTextModel GetNoticeText(int typeId)
        {
            T_NoticeTextDAL dal = new T_NoticeTextDAL();
            return dal.GetNoticeText(typeId);
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
        public static  int SaveNoticeText(int noticeType, int noticeId, int flagEdit, int display, string noticeText, string opName, string opIp)
        {
            T_NoticeTextDAL dal = new T_NoticeTextDAL();
            return dal.SaveNoticeText(noticeType, noticeId, flagEdit, display, noticeText, opName, opIp);
        }
        #endregion
    }
}
