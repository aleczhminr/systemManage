using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DAL
{
    public class UserPortraitDAL
    {
        public UserBehaviorChartModel GetSingleUsrPortrait(int accId)
        {
            UserBehaviorChartModel model = new UserBehaviorChartModel();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Sys_I200.[dbo].[SysRpt_ShopDayInfo] where accountid=@accId order by dayDate;");

            model.DataList = DapperHelper.Query<SysRpt_ShopDayInfo>(strSql.ToString(), new {accId = accId}).ToList();

            return model;
        }

        public int AddNewDicItem(int itemType, string addItemValue, int parentId)
        {
            int reVal = 0;
            StringBuilder strSql = new StringBuilder();

            strSql.Append("insert into P_Sys_PortraitDictionary (ItemType,Keyword,ParentId) " +
                          "VALUES(@itemType,@addItemValue,@parentId);");

            try
            {
                reVal = DapperHelper.Execute(strSql.ToString(),
                    new {itemType = itemType, addItemValue = addItemValue, parentId = parentId});
            }
            catch (Exception ex)
            {
                return 0;
            }

            return reVal;
        }

        public PortaritDicList GetDicList()
        {
            PortaritDicList list = new PortaritDicList();
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select * from P_Sys_PortraitDictionary order by OrderBy;");

            list.DataList = DapperHelper.Query<P_Sys_PortraitDictionaryModel>(strSql.ToString()).ToList();

            return list;
        }

        /// <summary>
        /// 添加备注条件
        /// </summary>
        /// <param name="type"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public int AddRemark(int type, string content)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into P_Sys_PortraitRemark (ItemType,Keyword,InsertTime) values(@type,@content,@time);");
            strSql.Append("select @@IDENTITY;");

            try
            {
                int reVal = DapperHelper.ExecuteScalar<int>(strSql.ToString(),
                    new { type = type, content = content, time = DateTime.Now });

                return reVal;
            }
            catch (Exception ex)
            {

                return 0;
            }
            
        }

        public string AddUserPortrait(P_Sys_UserPortraitModel model)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select count(*) from P_Sys_UserPortrait where AccId=@accid;");

            int count = DapperHelper.ExecuteScalar<int>(strSql.ToString(), new {accid = model.AccId});
            strSql.Clear();

            if (count == 0)
            {
                strSql.Append("insert into P_Sys_UserPortrait (" +
                          " InsertTime," +
                          " AccId," +
                          " Industry," +
                          " UserSourcePortrait," +
                          " ChoiceReason," +
                          " CompetingProduct," +
                          " PotentialNeed," +
                          " Attitude," +
                          " BusinessExp," +
                          " PhoneNum," +
                          " QQNum," +
                          " AgeGrade," +
                          " Gender," +
                          " WebExpGrade," +
                          " SalesmanNum," +
                          " RealName," +
                          " Email," +
                          " Weixin," +
                          " IdCardNo," +
                          " InvoiceTitle," +
                          " ShopAddress," +
                          " BranchNum," +
                          " RemarkId,MainQuestion) values(" +
                          " @InsertTime," +
                          " @AccId," +
                          " @Industry," +
                          " @UserSourcePortrait," +
                          " @ChoiceReason," +
                          " @CompetingProduct," +
                          " @PotentialNeed," +
                          " @Attitude," +
                          " @BusinessExp," +
                          " @PhoneNum," +
                          " @QQNum," +
                          " @AgeGrade," +
                          " @Gender," +
                          " @WebExpGrade," +
                          " @SalesmanNum," +
                          " @RealName," +
                          " @Email," +
                          " @Weixin," +
                          " @IdCardNo," +
                          " @InvoiceTitle," +
                          " @ShopAddress," +
                          " @BranchNum," +
                          " @RemarkId,@MainQuestion);");

                try
                {
                    int reVal = DapperHelper.Execute(strSql.ToString(), new
                    {
                        InsertTime = DateTime.Now,
                        AccId = model.AccId,
                        Industry = model.Industry,
                        UserSourcePortrait = model.UserSourcePortrait,
                        ChoiceReason = model.ChoiceReason,
                        CompetingProduct = model.CompetingProduct,
                        PotentialNeed = model.PotentialNeed,
                        Attitude = model.Attitude,
                        BusinessExp = model.BusinessExp,
                        PhoneNum = model.PhoneNum,
                        QQNum = model.QQNum,
                        AgeGrade = model.AgeGrade,
                        Gender = model.Gender,
                        WebExpGrade = model.WebExpGrade,
                        SalesmanNum = model.SalesmanNum,
                        RealName=model.RealName,
                        Email=model.Email,
                        Weixin=model.Weinxin,
                        IdCardNo=model.IdCardNo,
                        InvoiceTitle=model.InvoiceTitle,
                        ShopAddress=model.ShopAddress,
                        BranchNum=model.BranchNum,
                        RemarkId = model.RemarkId,
                        MainQuestion=model.MainQuestion
                    });

                    if (reVal > 0)
                    {
                        return "";
                    }
                    else
                    {
                        return "err";
                    }
                }
                catch (Exception ex)
                {

                    return "err";
                }
            }
            else
            {
                strSql.Append("update P_Sys_UserPortrait set " +
                              " InsertTime=@insertTime," +
                              " Industry=@industry," +
                              " UserSourcePortrait=@userSource," +
                              " ChoiceReason=@choiceReason," +
                              " CompetingProduct=@competingProduct," +
                              " PotentialNeed=@potentialNeed," +
                              " Attitude=@attitude," +
                              " BusinessExp=@business," +
                              " PhoneNum=@phone," +
                              " QQNum=@qq," +
                              " AgeGrade=@ageGrade," +
                              " Gender=@gender," +
                              " WebExpGrade=@webExpGrade," +
                              " SalesmanNum=@salemanNum," +
                              " RealName=@RealName," +
                              " Email=@Email," +
                              " Weixin=@Weixin," +
                              " IdCardNo=@IdCardNo," +
                              " InvoiceTitle=@InvoiceTitle," +
                              " ShopAddress=@ShopAddress," +
                              " BranchNum=@BranchNum," +
                              " RemarkId=@remarkId,MainQuestion=@MainQuestion where AccId=@accid;");

                try
                {
                    int reVal = DapperHelper.Execute(strSql.ToString(), new
                    {
                        insertTime = DateTime.Now,
                        industry = model.Industry,
                        userSource = model.UserSourcePortrait,
                        choiceReason = model.ChoiceReason,
                        competingProduct = model.CompetingProduct,
                        potentialNeed = model.PotentialNeed,
                        attitude = model.Attitude,
                        business = model.BusinessExp,
                        phone = model.PhoneNum,
                        qq = model.QQNum,
                        ageGrade = model.AgeGrade,
                        gender = model.Gender,
                        webExpGrade = model.WebExpGrade,
                        salemanNum = model.SalesmanNum,
                        remarkId = model.RemarkId,
                        RealName = model.RealName,
                        Email = model.Email,
                        Weixin = model.Weinxin,
                        IdCardNo = model.IdCardNo,
                        InvoiceTitle = model.InvoiceTitle,
                        ShopAddress = model.ShopAddress,
                        BranchNum = model.BranchNum,
                        accid=model.AccId,
                        MainQuestion = model.MainQuestion
                    });

                    if (reVal > 0)
                    {
                        return "";
                    }
                    else
                    {
                        return "err";
                    }
                }
                catch (Exception ex)
                {

                    return "err";
                }
            }
            
        }

        public P_Sys_UserPortraitModel GetUserExtInfo(int accid)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select * from P_Sys_UserPortrait where AccId=@accid;");

            return DapperHelper.GetModel<P_Sys_UserPortraitModel>(strSql.ToString(), new {accid = accid});
        }

        public RemarkList GetRemarkInfo(string remarkId)
        {
            RemarkList list = new RemarkList();
            P_Sys_PortraitRemarkModel model = new P_Sys_PortraitRemarkModel();

            StringBuilder strSql = new StringBuilder();

            string[] strArr = remarkId.Split(',');

            foreach (string str in strArr)
            {
                if (!string.IsNullOrEmpty(str) && str != " ")
                {
                    strSql.Append("select * from P_Sys_PortraitRemark where Id=@id");
                    model = DapperHelper.GetModel<P_Sys_PortraitRemarkModel>(strSql.ToString(), new { id = int.Parse(str) });

                    strSql.Clear();
                    list.DataList.Add(model); 
                }
                
            }

            return list;
        }
    }
}
