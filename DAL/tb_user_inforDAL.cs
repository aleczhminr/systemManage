using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// 店铺其他信息
    /// </summary>
    public class tb_user_inforDAL : Base.tb_user_inforBaseDAL
    {
        /// <summary>
        /// 初始化一个店铺信息
        /// </summary>
        /// <param name="accid"></param>
        public void InitializeUserInfor(int accid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("     if(NOT exists(select ID from tb_user_infor where UserId=@accid)) ");
            strSql.Append(" 	begin");
            strSql.Append(" 	    insert into tb_user_infor(UserId) values(@accid); ");
            strSql.Append(" 	end ");
            HelperForFrontend.Execute(strSql.ToString(), new { accid = accid });
        }

        /// <summary>
        /// 更新模板
        /// </summary>
        /// <param name="accid"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public bool UpdateHtmlTemes(int accid, int val)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_user_infor set htmlThemes=@htmlthemes where userId=@accid;");

            int rows = HelperForFrontend.Execute(strSql.ToString(), new { htmlthemes = val, accid = accid });
            if (rows > 0)
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
