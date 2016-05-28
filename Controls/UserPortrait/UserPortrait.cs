using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using BLL;
using Model;

namespace Controls.UserPortrait
{
    public static class UserPortrait
    {
        public static string GetSingleUsrPortrait(int accId)
        {
            UserBehaviorChartModel chartModel = new UserBehaviorChartModel();

            chartModel = UserPortraitBLL.GetSingleUsrPortrait(accId);

            return CommonLib.Helper.JsonSerializeObject(chartModel);
        }

        public static string AddNewDicItem(int itemType, string addItemValue, int parentId = 0)
        {
            int reVal = UserPortraitBLL.AddNewDicItem(itemType, addItemValue, parentId);

            if (reVal>0)
            {
                return "Success";
            }
            else
            {
                return "";
            }

        }

        public static string GetDicItem()
        {
            return CommonLib.Helper.JsonSerializeObject(UserPortraitBLL.GetDicList());
        }

        public static string AddUserPortrait(P_Sys_UserPortraitModel model)
        {
            //string remark = ",";

            //if (!string.IsNullOrEmpty(model.RemarkContent1))
            //{
            //    int reVal1 = UserPortraitBLL.AddRemark(model.RemarkType1, model.RemarkContent1);
            //    if (reVal1 > 0)
            //    {
            //        remark += reVal1.ToString() + ",";
            //    }
            //}

            //if (!string.IsNullOrEmpty(model.RemarkContent2))
            //{
            //    int reVal2 = UserPortraitBLL.AddRemark(model.RemarkType2, model.RemarkContent2);
            //    if (reVal2 > 0)
            //    {
            //        remark += reVal2.ToString() + ",";
            //    }
            //}

            //if (remark.Length > 1)
            //{
            //    model.RemarkId = remark;
            //}
            //用户拓展信息保留一个备注
            model.RemarkId = model.RemarkContent1;
            return UserPortraitBLL.AddUserPortrait(model);
        }

        public static string GetUserExtInfo(int accid)
        {
            return CommonLib.Helper.JsonSerializeObject(UserPortraitBLL.GetUserExtInfo(accid));
        }

        public static RemarkList GetRemarkInfo(string remarkId)
        {
            return UserPortraitBLL.GetRemarkInfo(remarkId);
        }
    }
}
