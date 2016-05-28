using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SmsManageDal
    {
        /// <summary>
        /// 返回小分类
        /// </summary>
        /// <param name="classid"></param>
        /// <returns></returns>
        public List<string> GetSmsClass(string classid)
        {
            List<string> smsClass = new List<string>();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT distinct sms_class FROM T_Common_Sms where sms_maxclass='" + classid + "'");

            try
            {
                smsClass = HelperForFrontend.Query<string>(strSql.ToString()).ToList();
            }
            catch (Exception)
            {
                smsClass = null;
            }

            return smsClass;
        }

        /// <summary>
        /// 保存短信
        /// </summary>
        /// <param name="classMax"></param>
        /// <param name="classMin"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public int SaveSms(string classMax, string classMin, string content)
        {
            int result = 0;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("declare @ranking varchar(10);if not exists( select id from T_Common_Sms where sms_class='" +
                          classMin +
                          "') select @ranking='99';select @ranking=sms_ranking from T_Common_Sms where sms_class='" +
                          classMin +
                          "';INSERT INTO T_Common_Sms(sms_maxclass, sms_class, sms_content, sms_time, sms_ranking) VALUES ('" +
                          classMax + "','" + classMin + "','" + content + "','" + System.DateTime.Now + "',@ranking)");

            try
            {
                result = HelperForFrontend.Execute(strSql.ToString());
            }
            catch (Exception ex)
            {
                    
            }

            return (result > 0 ? 1 : 0);
        }

        /// <summary>
        /// 删除短信
        /// </summary>
        /// <param name="smsId"></param>
        /// <returns></returns>
        public int DeleteSms(string smsId)
        {
            int result = 0;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE FROM T_Common_Sms where id='" + smsId + "'");

            try
            {
                result = HelperForFrontend.Execute(strSql.ToString());
            }
            catch (Exception ex)
            {
                    
            }

            return (result > 0 ? 1 : 0);
        }

        /// <summary>
        /// 更新短信
        /// </summary>
        /// <param name="classMax"></param>
        /// <param name="classMin"></param>
        /// <param name="content"></param>
        /// <param name="smsId"></param>
        /// <returns></returns>
        public int UpdateSms(string classMax, string classMin, string content, string smsId)
        {
            int result = 0;

            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "declare @ranking nvarchar(50);select @ranking='99';select @ranking=sms_ranking from T_Common_Sms where sms_class='" +
                classMin + "';UPDATE T_Common_Sms SET sms_maxclass ='" + classMax + "', sms_class ='" + classMin +
                "', sms_content ='" + content + "',sms_ranking=@ranking where id='" + smsId + "'");

            try
            {
                result = HelperForFrontend.Execute(strSql.ToString());
            }
            catch (Exception ex)
            {

            }

            return (result > 0 ? 1 : 0);
        }

        /// <summary>
        /// 修改分类
        /// </summary>
        /// <param name="classMax"></param>
        /// <param name="classMin"></param>
        /// <param name="cName"></param>
        /// <returns></returns>
        public int EditClass(string classMax, string classMin, string cName)
        {
            int result = 0;

            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "update [T_Common_Sms] set [sms_class]='" + cName + "' where sms_maxclass='" + classMax +
                "' and [sms_class]='" + classMin + "'");

            try
            {
                result = HelperForFrontend.Execute(strSql.ToString());
            }
            catch (Exception ex)
            {

            }

            return (result > 0 ? 1 : 0);
        }

        /// <summary>
        /// 修改排序
        /// </summary>
        /// <param name="classMax"></param>
        /// <param name="classMin"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public int EditOrder(string classMax, string classMin, string order)
        {
            int result = 0;

            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "update [T_Common_Sms] set [sms_ranking]='" + Convert.ToInt32(order).ToString() +
                "' where sms_maxclass='" + classMax + "' and [sms_class]='" + classMin + "'");

            try
            {
                result = HelperForFrontend.Execute(strSql.ToString());
            }
            catch (Exception ex)
            {

            }

            return (result > 0 ? 1 : 0);
        }


        /// <summary>
        /// 短信分类隐藏 
        /// </summary>
        /// <param name="classMax"></param>
        /// <param name="classMin"></param>
        /// <param name="hidden"></param>
        /// <returns></returns>
        public int HideSms(string classMax, string classMin, string hidden)
        {
            int result = 0;

            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "update [T_Common_Sms] set [HiddenType]='" + hidden + "' where sms_maxclass='" + classMax +
                "' and [sms_class]='" + classMin + "'");

            try
            {
                result = HelperForFrontend.Execute(strSql.ToString());
            }
            catch (Exception ex)
            {

            }

            return (result > 0 ? 1 : 0);
        }

        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="classMax"></param>
        /// <param name="classMin"></param>
        /// <returns></returns>
        public int ClassDelete(string classMax, string classMin)
        {
            int result = 0;

            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "delete [T_Common_Sms] where sms_maxclass='" + classMax + "' and [sms_class]='" + classMin + "'");

            try
            {
                result = HelperForFrontend.Execute(strSql.ToString());
            }
            catch (Exception ex)
            {

            }

            return (result > 0 ? 1 : 0);
        }

    }
}
