using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;

namespace BLL
{
    public static class Sys_UserMessageBLL
    {
        /// <summary>
        /// 获取用户消息列表
        /// </summary>
        /// <param name="accid"></param>
        /// <param name="pageIndex"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="msgType"></param>
        /// <param name="content"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public static UserMessageModel GetUserList(int accid, int pageIndex, string start, string end, int msgType, string content, string title)
        {
            UserMessageModel model = new UserMessageModel();
            Sys_UserMessageDAL dal = new Sys_UserMessageDAL();

            string whereStr = "";

            if (!string.IsNullOrEmpty(start))
            {
                whereStr += " PushTime>='" + start + "' ";
            }
            if (!string.IsNullOrEmpty(end))
            {
                whereStr += " PushTime<='" + end + "' ";
            }
            if (msgType != -99)
            {
                whereStr += " ChannelId=" + msgType + " ";
            }
            if (!string.IsNullOrEmpty(content))
            {
                whereStr += " PushContent like '%" + content + "%' ";
            }
            if (!string.IsNullOrEmpty(title))
            {
                whereStr += " Title like '%" + title + "%' ";
            }

            List<Sys_UserMessageModel> list = dal.GetUserMessageList(pageIndex, accid, whereStr);

            foreach (var item in list)
            {
                if (item.ChannelId == 3 || item.ChannelId == 4)
                {
                    item.PushTime = item.PushTime.AddHours(8);
                }
            }

            model.DataList = list;

            int count = dal.GetUserMessageCount(accid, whereStr);
            int pageCount = count % 15 == 0 ? (count / 15) : (count / 15 + 1);

            model.RowCount = count;
            model.PageCount = pageCount;

            return model;
        }

        /// <summary>
        /// 更新店铺消息汇总表
        /// </summary>
        /// <param name="accid"></param>
        /// <returns></returns>
        public static int UpdateUserMessage(int accid)
        {
            Sys_UserMessageDAL dal = new Sys_UserMessageDAL();

            //循环处理所有信息渠道
            for (int i = 1; i < 5; i++)
            {
                //获取该渠道最后一次更新时间
                DateTime dt = dal.GetLastUpdateTime(i,accid);
                //更新表
                dal.UpdateUserMessage(accid, i, dt);
            }

            return 1;
        }
    }
}
