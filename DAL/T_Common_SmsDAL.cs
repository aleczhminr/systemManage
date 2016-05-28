using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class T_Common_SmsDAL
    {
        public Dictionary<string, string> GetSmsContent(int page, string type, string subType)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>()
            {
                {"list",""},
                {"rowCount",""},
                {"maxRow",""}
            };

            StringBuilder strSql = new StringBuilder();

            int pageIndex = page;
            int pageSize = 15;
            int bgNumber = ((pageIndex - 1) * pageSize) + 1;
            int edNumber = (pageIndex) * pageSize;

            List<T_Common_Sms> list = new List<T_Common_Sms>();

            if (string.IsNullOrEmpty(subType))
            {
                strSql.Append("select * from " +
                          "(select ROW_NUMBER() over (order by sms_time desc) rowNumber,* from i200.dbo.T_Common_Sms " +
                          "where sms_maxclass=@sms_max) t where t.rowNumber between @bgNumber and @edNumber;");

                list =
                    HelperForFrontend.Query<T_Common_Sms>(strSql.ToString(),
                        new { sms_max = type, bgNumber = bgNumber, edNumber = edNumber }).ToList();
            }
            else
            {
                strSql.Append("select * from " +
                          "(select ROW_NUMBER() over (order by sms_time desc) rowNumber,* from i200.dbo.T_Common_Sms " +
                          "where sms_maxclass=@sms_max and sms_class=@sms_min) t where t.rowNumber between @bgNumber and @edNumber;");

                list =
                    HelperForFrontend.Query<T_Common_Sms>(strSql.ToString(),
                        new { sms_max = type,sms_min=subType, bgNumber = bgNumber, edNumber = edNumber }).ToList();
            }
            

            strSql.Clear();

            int count = 0;

            if (string.IsNullOrEmpty(subType))
            {
                strSql.Append("select count(*) from i200.dbo.T_Common_Sms " +
                          "where sms_maxclass=@sms_max;");
                count = HelperForFrontend.ExecuteScalar<int>(strSql.ToString(), new { sms_max = type });
            }
            else
            {
                strSql.Append("select count(*) from i200.dbo.T_Common_Sms " +
                          "where sms_maxclass=@sms_max and sms_class=@sms_min;");

                count = HelperForFrontend.ExecuteScalar<int>(strSql.ToString(), new { sms_max = type, sms_min=subType });
            }
            

            

            dic["list"] = CommonLib.Helper.JsonSerializeObject(list, "yyyy-MM-dd");
            dic["rowCount"] = count.ToString();

            int maxRow = count % 15 == 0 ? count / 15 : (count / 15 + 1);
            dic["maxRow"] = maxRow.ToString();

            return dic;
        }

        public List<string> GetminCate(string maxCate)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select sms_class from i200.dbo.T_Common_Sms where sms_maxclass='" + maxCate +
                          "' group by sms_class;");

            return HelperForFrontend.Query<string>(strSql.ToString()).ToList();
        }

        public string AddCate(string cate)
        {
            return "";
        }

        public string UpdateCommonSmsContent(int smsid, string maxCate, string minCate, string smscontent)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "update i200.dbo.T_Common_Sms set sms_maxclass=@maxCate,sms_class=@minCate,sms_content=@content where id=@id;");
            string result = "0";

            try
            {
                int reVal = HelperForFrontend.Execute(strSql.ToString(),
                    new { maxCate = maxCate, minCate = minCate, content = smscontent, id = smsid });

                if (reVal > 0)
                {
                    result = "1";
                }
            }
            catch (Exception ex)
            {
                result = "0";
            }

            return result;
        }

        public string AddCommonSms(string maxCate, string minCate, string smscontent)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("declare @ranking varchar(10);if not exists( select id from i200.dbo.T_Common_Sms where sms_class='" +
                          minCate +
                          "') select @ranking='99';select @ranking=sms_ranking from i200.dbo.T_Common_Sms where sms_class='" +
                          minCate +
                          "';INSERT INTO i200.dbo.T_Common_Sms(sms_maxclass, sms_class, sms_content, sms_time, sms_ranking) VALUES ('" +
                          maxCate + "','" + minCate + "','" + smscontent + "','" + System.DateTime.Now + "',@ranking)");

            try
            {
                int reVal = HelperForFrontend.Execute(strSql.ToString());
                if (reVal > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {

                return "0";
            }
        }

        public string DeleteSms(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from i200.dbo.T_Common_Sms where id=@id");

            try
            {
                int reVal = HelperForFrontend.Execute(strSql.ToString(), new { id = id });
                if (reVal > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {

                return "0";
            }
        }

        public string GetCateList(string type)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "select sms_class,sms_ranking,HiddenType from [i200].[dbo].[T_Common_Sms] where sms_maxclass=@maxClass group by sms_class,sms_ranking,HiddenType order by sms_ranking");

            List<SimpleSms> list = HelperForFrontend.Query<SimpleSms>(strSql.ToString(), new { maxClass = type }).ToList();

            return CommonLib.Helper.JsonSerializeObject(list);
        }

        public string UpdateCateName(string maxName, string minName, string updateName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "update [i200].[dbo].[T_Common_Sms] set sms_class=@updateName where sms_maxclass=@maxName and sms_class=@minName;");

            try
            {
                int reVal = HelperForFrontend.Execute(strSql.ToString(),
                    new { updateName = updateName, maxName = maxName, minName = minName });

                if (reVal > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                return "0";
            }
        }

        public string DeleteCate(string maxName, string minName)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("delete from [i200].[dbo].[T_Common_Sms] where sms_maxclass=@maxName and sms_class=@minName;");

            try
            {
                int reVal = HelperForFrontend.Execute(strSql.ToString(), new { maxName = maxName, minName = minName });

                if (reVal > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                return "0";
            }
        }

        public string ChangeStatus(string maxName, string minName)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("declare @status int;");
            strSql.Append(
                "select @status=HiddenType from [i200].[dbo].[T_Common_Sms] where sms_maxclass=@maxName and sms_class=@minName;");
            strSql.Append("select @status;");

            int status = HelperForFrontend.ExecuteScalar<int>(strSql.ToString(), new { maxName = maxName, minName = minName });
            strSql.Clear();

            int update = (status == 0 ? 1 : 0);

            strSql.Append(
                "update [i200].[dbo].[T_Common_Sms] set HiddenType=@update where sms_maxclass=@maxName and sms_class=@minName;");

            try
            {
                int reVal = HelperForFrontend.Execute(strSql.ToString(),
                    new { update = update, maxName = maxName, minName = minName });

                if (reVal > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                return "0";
            }


        }

        public string ChangeRanking(string maxName, string minName, int rank)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(
                "update [i200].[dbo].[T_Common_Sms] set sms_ranking=@rank where sms_maxclass=@maxName and sms_class=@minName;");

            try
            {
                int reVal = HelperForFrontend.Execute(strSql.ToString(),
                    new { rank = rank, maxName = maxName, minName = minName });

                if (reVal > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                return "0";
            }
        }
    }
}
