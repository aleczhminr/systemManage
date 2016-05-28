using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;

namespace BLL
{
    public static class UserPortraitBLL
    {
        public static UserBehaviorChartModel GetSingleUsrPortrait(int accId)
        {
            UserPortraitDAL dal = new UserPortraitDAL();
            return dal.GetSingleUsrPortrait(accId);
        }

        public static int AddNewDicItem(int itemType, string addItemValue, int parentId)
        {
            UserPortraitDAL dal = new UserPortraitDAL();
            return dal.AddNewDicItem(itemType, addItemValue, parentId);
        }

        public static PortaritDicList GetDicList()
        {
            UserPortraitDAL dal = new UserPortraitDAL();
            return dal.GetDicList();
        }

        public static string AddUserPortrait(P_Sys_UserPortraitModel model)
        {
            UserPortraitDAL dal = new UserPortraitDAL();
            return dal.AddUserPortrait(model);
        }

        public static int AddRemark(int type, string content)
        {
            UserPortraitDAL dal = new UserPortraitDAL();
            return dal.AddRemark(type, content);
        }

        public static P_Sys_UserPortraitModel GetUserExtInfo(int accid)
        {
            UserPortraitDAL dal = new UserPortraitDAL();
            return dal.GetUserExtInfo(accid);
        }

        public static RemarkList GetRemarkInfo(string remarkId)
        {
            UserPortraitDAL dal = new UserPortraitDAL();
            return dal.GetRemarkInfo(remarkId);
        }
    }
}
