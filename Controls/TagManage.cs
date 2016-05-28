using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;

namespace Controls
{
    public static class TagManage
    {
        public static string GetTagInfo(int pageIndex, int tagType, string insertName = "", string tagName = "")
        {
            return
                CommonLib.Helper.JsonSerializeObject(Sys_TagInfoBLL.GetTagInfoByCondition(pageIndex, tagType, insertName,
                    tagName));
        }

        public static string GetSingleModel(int id)
        {
            return CommonLib.Helper.JsonSerializeObject(Sys_TagInfoBLL.GetSingleModel(id));
        }

        public static string ModifyModel(int id, string tagName, string tagTypeId, string tagTypeName, int tagStatus)
        {
            return Sys_TagInfoBLL.ModifyModel(id, tagName, tagTypeId, tagTypeName, tagStatus);
        }

        public static string AddNewTag(string tagName, string insertName)
        {
            if (Sys_TagInfoBLL.Add(tagName, insertName) != 0)
            {
                return "1";
            }
            else
            {
                return "0";
            }
        }

        //Test tagImport
        public static string TagImport()
        {
            return Sys_TagInfoBLL.TagImport();
        }
    }
}
