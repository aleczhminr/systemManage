using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hangfire.Annotations;
using Model;
using Utility;

namespace DAL
{
    public class IndustryFilterDAL
    {
        //更新清洗结果表的店铺字段信息
        /// <summary>
        /// 更新行业清洗基表
        /// </summary>
        /// <returns></returns>
        public int UpdateIndustryAccount()
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("insert into SysStat_IndustryFilter (AccId,CompanyName) " +
                          "select ID,CompanyName from i200.dbo.T_Account where ID not in (select AccId from SysStat_IndustryFilter);");

            try
            {
                return DapperHelper.Execute(strSql.ToString());
            }
            catch (Exception ex)
            {
                Logger.Error("更新同步行业清洗用户表出错！", ex);
                return -1;
            }
        }

        //以现有扩展信息更新行业结果
        public int UpdateIndustryByExtendinfo()
        {
            StringBuilder strSql = new StringBuilder();
            List<ShopExtIndustry> shopExtList = new List<ShopExtIndustry>();

            //获取有分类的店铺及其大小分类
            strSql.Append("select AccId,Industry IndustryStr from P_Sys_UserPortrait where PATINDEX('%[0-9]%',Industry)<>0;");

            try
            {
                shopExtList = DapperHelper.Query<ShopExtIndustry>(strSql.ToString()).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error("获取现有用户扩展信息行业出错！", ex);
            }

            //如果获取到信息进行处理
            if (shopExtList.Count > 0)
            {
                foreach (var item in shopExtList)
                {
                    List<int> industryList = new List<int>();
                    
                    List<string> industryStrList =
                        item.IndustryStr.Split(',').ToList();

                    foreach (var indust in industryStrList)
                    {
                        int i = 0;
                        if (int.TryParse(indust, out i))
                        {
                            industryList.Add(i);
                        }
                    }

                    if (industryList.Count > 0)
                    {
                        //获取大小行业
                        foreach (var itemId in industryList)
                        {
                            ShopIndustryDic dic = GetIndustryPairDic(itemId);
                            if (dic.ParentId == 0)
                            {
                                item.Industry_1 = dic.Keyword;
                            }
                            else
                            {
                                item.Industry_2 = dic.Keyword;
                            }
                        }
                    }

                    //更新用户行业表
                    UpdateExtIndustry(item);
                }

                return 1;
            }
            else
            {
                return -1;
            }
        }

        //以infor表中匹配的对应信息更新店铺行业        
        /// <summary>
        /// 用tb_user_infor更新行业清洗基表
        /// </summary>
        /// <returns></returns>
        public int UpdateFormerShopType()
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("update SysStat_IndustryFilter " +
                          "set FormerShopType=t.ShopType " +
                          "from i200.dbo.tb_user_infor t " +
                          "left join SysStat_IndustryFilter i " +
                          "on t.UserId=i.AccId;");

            try
            { 
                return DapperHelper.Execute(strSql.ToString());
            }
            catch (Exception ex)
            {
                Logger.Error("更新旧表行业出错！", ex);
                return -1;
            }
        }

        //获取清洗字典
        public List<IndustryFilterDic> GetFilterDic()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from SysStat_IndustryFilterDic;");
            return DapperHelper.Query<IndustryFilterDic>(strSql.ToString()).ToList();
        }

        //清洗未获取到的店铺
        public int FilterBaseIndustry(ShopExtIndustry model)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(
                "update SysStat_IndustryFilter set Sindustry_1=@Aindustry,Sindustry_2=@Bindustry where AccId=@accid;");

            try
            {
                return DapperHelper.Execute(strSql.ToString(), new
                {
                    Aindustry = model.Industry_1,
                    Bindustry = model.Industry_2,
                    accid = model.AccId
                });
            }
            catch (Exception ex)
            {
                Logger.Error("用扩展信息更新用户行业清洗基表出错！", ex);
                return -1;
            }
        }

        #region Helper
        //记录更新日志
        public int UpdateIndustryFilterLog(IndustryFilterLog model)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(
                "insert into SysStat_IndustryFilter_Log (AccId,FormerIndustry,NowIndustry,UpdateTime,Keyword) " +
                "Values (@AccId,@FormerIndustry,@NowIndustry,@UpdateTime,@Keyword);");

            return DapperHelper.Execute(strSql.ToString(), model);
        }

        /// <summary>
        /// 通过Id获取字典项和父级Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ShopIndustryDic GetIndustryPairDic(int id)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select Keyword,ParentId from P_Sys_PortraitDictionary where Id=@id;");

            return DapperHelper.GetModel<ShopIndustryDic>(strSql.ToString(), new {id = id});
        }

        /// <summary>
        /// 更新用户行业清洗基表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateExtIndustry(ShopExtIndustry model)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(
                "update SysStat_IndustryFilter set Eindustry_1=@Aindustry,Eindustry_2=@Bindustry where AccId=@accid;");

            try
            {
                return DapperHelper.Execute(strSql.ToString(), new
                {
                    Aindustry = model.Industry_1,
                    Bindustry = model.Industry_2,
                    accid = model.AccId
                });
            }
            catch (Exception ex)
            {
                Logger.Error("用扩展信息更新用户行业清洗基表出错！", ex);
                return -1;
            }
        }

        /// <summary>
        /// 获取清洗基表的店铺Id和店铺名称
        /// </summary>
        /// <returns></returns>
        public List<ShopNamePair> GetAccIdPair()
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select AccId,CompanyName from SysStat_IndustryFilter;");

            return DapperHelper.Query<ShopNamePair>(strSql.ToString()).ToList();
        }

        /// <summary>
        /// 更新清洗状态
        /// </summary>
        /// <param name="accid"></param>
        /// <returns></returns>
        public int UpdateFilterStatus(int accid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SysStat_IndustryFilter set Remark='1' where AccId=@accid;");

            return DapperHelper.Execute(strSql.ToString(), new {accid = accid});
        }

        #endregion
    }
}
