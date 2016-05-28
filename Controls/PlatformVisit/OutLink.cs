using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using BLL;
namespace Controls.PlatformVisit
{
    public static class OutLink
    {

        /// <summary>
        /// 得到列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static Dictionary<string,string> GetList(int pageIndex, int majorType, int minorType, string adder, string linkName, int status, DateTime stDate, DateTime edDate)
        {
            int pageSize = 15;
            List<DapperWhere> sqlWhere = new List<DapperWhere>();

            if (adder.Length>0)
            {
                List<int> idList = Sys_Manage_UserBLL.GetIdByName(adder);
                string idStr = "";
                if (idList.Count > 0)
                {
                    foreach (int id in idList)
                    {
                        idStr += id.ToString() + ",";
                    }
                    idStr = idStr.Substring(0, idStr.LastIndexOf(','));
                    sqlWhere.Add(new DapperWhere("idStr", idStr, " Sys_Manage_User.id in (" + idStr + ")"));
                }
                else
                {
                    return null;
                }
            }
            
            if (stDate.ToShortDateString()!=DateTime.Now.ToShortDateString())
            {
                sqlWhere.Add(new DapperWhere("stDate", stDate, " I200.dbo.T_OutLink.createTime>=@stDate"));
                sqlWhere.Add(new DapperWhere("edDate", edDate, " I200.dbo.T_OutLink.createTime<=@edDate"));
            }

            if (minorType!=-1)
            {
                sqlWhere.Add(new DapperWhere("minorType", minorType, " I200.dbo.T_OutLink.linktype=@minorType"));
            }
            else if (majorType != -1)
            {
                string typeId = "";
                List<OutLinkType> list = GetTypeList();

                foreach (OutLinkType linkType in list)
                {
                    if (linkType.id == majorType)
                    {
                        foreach (OutLinkType item in linkType.itemList)
                        {
                            typeId += item.id + ",";
                        }
                        typeId = typeId.Substring(0, typeId.LastIndexOf(','));

                    }
                }
                sqlWhere.Add(new DapperWhere("typeId", typeId, " I200.dbo.T_OutLink.linktype in (" + typeId + ") "));
            }

            if (linkName!="")
            {
                sqlWhere.Add(new DapperWhere("linkName", linkName, " I200.dbo.T_OutLink.linkname like '%'+@linkName+'%'"));
            }

            if (status!=-1)
            {
                sqlWhere.Add(new DapperWhere("status", status, " I200.dbo.T_OutLink.state=@status"));
            }
               
            return T_OutLinkBLL.GetList(pageIndex, pageSize, sqlWhere, " createTime desc");
        }
        /// <summary>
        /// 增加推广
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Add(T_OutLink model)
        {
            return T_OutLinkBLL.Add(model);
        }
        /// <summary>
        /// 外链分类列表
        /// </summary>
        /// <returns></returns>
        public static List<OutLinkType> GetTypeList()
        {
            return T_OutLinkTypeBLL.GetList();
        }

        public static int ChangeStatus(string status, string id)
        {
            return T_OutLinkTypeBLL.ChangeStatus(status, id);
        }
    }
}
