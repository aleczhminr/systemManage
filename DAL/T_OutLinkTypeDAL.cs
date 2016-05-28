using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
namespace DAL
{
    /// <summary>
    /// 推广链接分类
    /// </summary>
    public class T_OutLinkTypeDAL : Base.T_OutLinkTypeBaseDAL
    {
        /// <summary>
        /// 得到列表
        /// </summary>
        /// <returns></returns>
        public List<OutLinkType> GetList()
        {

            List<T_OutLinkType> allList = GetList(new List<DapperWhere>());
            List<OutLinkType> typeList = new List<OutLinkType>();



            foreach (T_OutLinkType item in allList.Where(x => x.ot_id == 0))
            {
                OutLinkType maxList = new OutLinkType(item.id, item.ot_name);
                foreach (T_OutLinkType twoItem in allList.Where(x => x.ot_id == item.id))
                {
                    maxList.itemList.Add(new OutLinkType(twoItem.id, twoItem.ot_name));
                }
                typeList.Add(maxList);
            }
            return typeList;
        }

        public int ChangeStatus(string status, string id)
        {
            int rowId = int.Parse(id);

            StringBuilder strSql = new StringBuilder();
            switch (status)
            {
                case "1":
                    strSql.Append("update i200.dbo.T_OutLink set state=0 where id=@id;");
                    break;
                case "0":
                    strSql.Append("update i200.dbo.T_OutLink set state=1 where id=@id;");
                    break;
            }

            try
            {
                int st = DapperHelper.Execute(strSql.ToString(), new { id = rowId });
                if (st != 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {

                return 0;
            }

        }
    }
}
