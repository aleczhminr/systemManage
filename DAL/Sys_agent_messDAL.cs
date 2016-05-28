using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Model;

namespace DAL
{
    public class Sys_agent_messDAL
    {
        public AgentList GetAgentList(int pageIndex)
        {
            AgentList agentModel = new AgentList();

            int bgNumber = ((pageIndex - 1) * 15) + 1;
            int edNumber = (pageIndex) * 15;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from (select row_number() over (order by ID desc) rowNumber,* from Sys_agent_mess) t where t.rowNumber between @bgNumber and @edNumber; ");

            agentModel.list =
                DapperHelper.Query<AgentModel>(strSql.ToString(), new {bgNumber = bgNumber, edNumber = edNumber})
                    .ToList();

            strSql.Clear();

            strSql.Append("select count(*) from Sys_agent_mess;");
            agentModel.rowCount = DapperHelper.ExecuteScalar<int>(strSql.ToString());

            agentModel.maxPage = (agentModel.rowCount%15 == 0) ? agentModel.rowCount/15 : (agentModel.rowCount/15 + 1);

            return agentModel;

        }

        public string ChangeStatus(int curStat,int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Sys_agent_mess set activeStatus=@stat where ID=@id;");

            try
            {
                int reVal = DapperHelper.Execute(strSql.ToString(), new {stat = curStat, id = id});
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

        public string ChangeGroup(int curStat, int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Sys_agent_mess set AgentGroup=@stat where ID=@id;");

            try
            {
                int reVal = DapperHelper.Execute(strSql.ToString(), new { stat = curStat, id = id });
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

        public string AddAgent(AgentModel model)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("if exists(select * from Sys_I200.dbo.Sys_agent_mess where AgentId=@AgentId) " +
                          " " +
                          " select -1;" +
                          "  " +
                          " else " +
                          " begin " +
                          " insert into Sys_I200.dbo.Sys_agent_mess (" +
                          " AgentId,AgentPassword,AgentName,AgentGrade," +
                          " AgentPhone,AgentAddress,AgentIdCard,AgentNumber," +
                          " AgentEmail,AgentQQ,Creater,CreatingDate,Remark,activeStatus,AgentLink,ServiceType) values (" +
                          " @AgentId,@AgentPassword,@AgentName,@AgentGrade," +
                          " @AgentPhone,@AgentAddress,@AgentIdCard,@AgentNumber," +
                          " @AgentEmail,@AgentQQ,@Creater,getdate(),@Remark,@activeStatus,@AgentLink,@ServiceType);" +
                          " select @@IDENTITY;" +
                          " end ");

            try
            {
                int reVal = DapperHelper.ExecuteScalar<int>(strSql.ToString(), new
                {
                    AgentId = model.AgentId,
                    AgentPassword = model.AgentPassword,
                    AgentName=model.AgentName,
                    AgentGrade=model.AgentGrade,
                    AgentPhone=model.AgentPhone,
                    AgentAddress=model.AgentAddress,
                    AgentIdCard=model.AgentIdCard,
                    AgentNumber=model.AgentNumber,
                    AgentEmail=model.AgentEmail,
                    AgentQQ=model.AgentQQ,
                    Creater=model.Creater,
                    Remark=model.Remark,
                    activeStatus=1,
                    AgentLink=model.AgentLink,
                    ServiceType=model.ServiceType
                });

                return reVal.ToString();
            }
            catch (Exception ex)
            {
                return "0";
            }
        }

        public AgentModel GetSingleModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Sys_agent_mess where ID=@id;");

            return DapperHelper.Query<AgentModel>(strSql.ToString(), new {id = id}).ToList()[0];
        }

        public chartDataModel GetAgentStat(string stDate, string edDate, string[] cols)
        {
            DateTime st = Convert.ToDateTime(stDate);
            DateTime ed = Convert.ToDateTime(edDate);

            StringBuilder strSql = new StringBuilder();
            StringBuilder strColumn = new StringBuilder();
            StringBuilder strDrop = new StringBuilder();

            Dictionary<string, string> ColumnList = new Dictionary<string, string>();
           
            strSql.Append("declare @Reg int,@Act int,@Loyal int;");
            strColumn.Append("select cast(@date as date) dayDate");

            if (cols.Contains("reg"))
            {
                strColumn.Append(",@Reg Reg");
                ColumnList.Add("Reg", "注册用户");

                strSql.Append("select @Reg=COUNT(*) from i200.dbo.T_Account where agentID>0 and state=1 and cast(RegTime as date) <=@date;");
            }
            if (cols.Contains("active"))
            {
                strColumn.Append(",@Act Active");
                ColumnList.Add("Active", "活跃用户");

                strSql.Append("select distinct accid into #actList from [Sys_I200].[dbo].[SysRpt_ShopActive] " +
                              "where DATEDIFF(DAY,@date,updatetime)<lastNum and DATEDIFF(DAY,@date,updatetime)>=0 and active>4;" +
                              "select @Act=COUNT(*) from i200.dbo.T_Account where AgentId>0 and state=1 and ID in (select accid from #actList);");

                strDrop.Append("drop table #actList;");
            }
            //if (cols.Contains("loyal"))
            //{
            //    strColumn.Append(",@Loyal Loyal");
            //    ColumnList.Add("Loyal", "忠诚用户");

            //    strSql.Append("select distinct accid into #loyList from [Sys_I200].[dbo].[SysRpt_ShopActive] " +
            //                  "where DATEDIFF(DAY,@date,updatetime)<lastNum and DATEDIFF(DAY,@date,updatetime)>0 and active=7;" +
            //                  "select @Loyal=COUNT(*) from i200.dbo.T_Account where AgentId<>0 and ID in (select accid from #loyList);");

            //    strDrop.Append("drop table #loyList;");
            //}

            strSql.Append(strDrop.ToString());
            strSql.Append(strColumn.ToString());
            

            List<dynamic> list = new List<dynamic>();

            DateTime iter = st;
            while (iter < ed)
            {
                list.Add(DapperHelper.Query<dynamic>(strSql.ToString(), new { date = iter.Date }).ToList()[0]);
                iter = iter.AddDays(1);
            }

            chartDataModel chartModel = new chartDataModel();
            return chartModel.SetDataAboutDataTime(st, ed, list, ColumnList, "代理商汇总"); 

        }

        public string GetRandomNum()
        {
            Random rd = new Random();
            string agentNum=rd.Next().ToString().Substring(0, 4);

            while (!IsSame(agentNum))
            {
                agentNum = rd.Next().ToString().Substring(0, 4);
            }

            return agentNum;
        }

        public bool IsSame(string num)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select AgentId from Sys_I200.dbo.Sys_agent_mess");

            List<string> idList = DapperHelper.Query<string>(strSql.ToString()).ToList();

            foreach (var str in idList)
            {
                string[] s = str.Split('-');
                if (s[2] == num)
                {

                    return false;
                }
            }

            return true;
        }

        public string ModifyAgentInfo(AgentModel model)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select AgentPassword from Sys_I200.dbo.Sys_agent_mess where id=@id;");
            string pwd = DapperHelper.ExecuteScalar<string>(strSql.ToString(), new {id = model.ID});

            strSql.Clear();

            if (model.AgentPassword!=pwd)
            {
                model.AgentPassword = CommonLib.Helper.Md5Hash(model.AgentPassword);
            }

            strSql.Append("update Sys_I200.dbo.Sys_agent_mess set " +
                          " AgentPassword=@pwd," +
                          " AgentName=@name," +
                          " AgentGrade=@grade," +
                          " AgentPhone=@phone," +
                          " AgentAddress=@address," +
                          " AgentIdCard=@idCard," +
                          " AgentNumber=@number," +
                          " AgentEmail=@email," +
                          " AgentQQ=@qq," +
                          " Remark=@remark ," +
                          " ServiceType=@serviceType " +
                          " where id=@id");

            try
            {
                int reVal = DapperHelper.Execute(strSql.ToString(), new
                {
                    pwd = model.AgentPassword,
                    name = model.AgentName,
                    grade = model.AgentGrade,
                    phone = model.AgentPhone,
                    address = model.AgentAddress,
                    idCard = model.AgentIdCard,
                    number = model.AgentNumber,
                    email = model.AgentEmail,
                    qq = model.AgentQQ,
                    remark = model.Remark,
                    serviceType = model.ServiceType,
                    id=model.ID
                });

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


        /// <summary>
        /// 产生并校验代理商编码
        /// </summary>
        /// <param name="agentName"></param>
        /// <returns></returns>
        public string CheckAndAutoGeneratedCode(string agentName)
        {
            var checkResult = GetAgentCode(agentName);
            if (checkResult.Item1)
            {
                CheckAndAutoGeneratedCode(agentName);
            }

            return checkResult.Item2;
        }

        /// <summary>
        /// 产生代理商编码
        /// </summary>
        /// <param name="agentName"></param>
        /// <returns></returns>
        private static Tuple<bool,string> GetAgentCode(string agentName)
        {
            var shorthandName = NPinyin.Pinyin.GetInitials(agentName);
            int[] randomNum = new int[] {1, 2, 3, 5, 6, 8, 9, 0};
            var numCode = new StringBuilder();
            for (int i = 0; i < 6-shorthandName.Length; i++)
            {
               //产生随机因子,防止在短时间内产生相同的随机数
               byte[] bytes = new byte[4];
               System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
               rng.GetBytes( bytes );
               var  randomSeed= BitConverter.ToInt32( bytes , 0 );
               Random ran = new Random(randomSeed);
               int randomKey = ran.Next(0, 7);
               numCode.Append(randomNum[randomKey]);
            }
            var agentCode = shorthandName.ToLower() + numCode.ToString();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Sys_agent_mess where AgentId=@AgentId;");
            var checkResult = DapperHelper.GetModel<int>(strSql.ToString(), new {AgentId = agentCode});
            return new Tuple<bool, string>(checkResult>=1,agentCode);
        }
    }
}
