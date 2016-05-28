using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using Model;
using System.Linq;
namespace DAL.Base
{
	 	//Sys_SysRevisit_AccInfo
			/// <summary>
		/// Sys_SysRevisit_AccInfo
		/// </summary>
	public  class Sys_SysRevisit_AccInfoBaseDAL
	{ 
        /// <summary>
        /// 新增加一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(Sys_SysRevisit_AccInfo model)
        {

            StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into Sys_SysRevisit_AccInfo(");			
            strSql.Append("accID,CreateTime,ActiveStatus,LoginTimes,LastLoginTime,LastLoginDays,GoodsCnt,UserCnt,SalesCnt,PayCnt,SmsCnt,OrderCnt,OrderSum,avgLoginTimes,avgSalesCnt,avgGoodsCnt,avgUserCnt,avgPayCnt,avgSmsCnt,LastSales,LastGoods,LastUser,LastPay,LastSms,LastRevisit,accVer,verBgTime,verEdTime,verEdDays,SmsBalance,UserBalance,RevisitClass,RevisitItem,RevisitDesc,RevisitStatus,RevisitFlag,ParentRevisit");
			strSql.Append(") values (");
            strSql.Append("@accID,@CreateTime,@ActiveStatus,@LoginTimes,@LastLoginTime,@LastLoginDays,@GoodsCnt,@UserCnt,@SalesCnt,@PayCnt,@SmsCnt,@OrderCnt,@OrderSum,@avgLoginTimes,@avgSalesCnt,@avgGoodsCnt,@avgUserCnt,@avgPayCnt,@avgSmsCnt,@LastSales,@LastGoods,@LastUser,@LastPay,@LastSms,@LastRevisit,@accVer,@verBgTime,@verEdTime,@verEdDays,@SmsBalance,@UserBalance,@RevisitClass,@RevisitItem,@RevisitDesc,@RevisitStatus,@RevisitFlag,@ParentRevisit");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");             
            object obj = DapperHelper.ExecuteScalar(strSql.ToString(), model);
            if (obj != null)
            {
                return Convert.ToInt32(obj);
            }
            else
            {
                return 0;
            }
        }
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Sys_SysRevisit_AccInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Sys_SysRevisit_AccInfo set ");
			                                                
            strSql.Append(" accID = @accID , ");                                    
            strSql.Append(" CreateTime = @CreateTime , ");                                    
            strSql.Append(" ActiveStatus = @ActiveStatus , ");                                    
            strSql.Append(" LoginTimes = @LoginTimes , ");                                    
            strSql.Append(" LastLoginTime = @LastLoginTime , ");                                    
            strSql.Append(" LastLoginDays = @LastLoginDays , ");                                    
            strSql.Append(" GoodsCnt = @GoodsCnt , ");                                    
            strSql.Append(" UserCnt = @UserCnt , ");                                    
            strSql.Append(" SalesCnt = @SalesCnt , ");                                    
            strSql.Append(" PayCnt = @PayCnt , ");                                    
            strSql.Append(" SmsCnt = @SmsCnt , ");                                    
            strSql.Append(" OrderCnt = @OrderCnt , ");                                    
            strSql.Append(" OrderSum = @OrderSum , ");                                    
            strSql.Append(" avgLoginTimes = @avgLoginTimes , ");                                    
            strSql.Append(" avgSalesCnt = @avgSalesCnt , ");                                    
            strSql.Append(" avgGoodsCnt = @avgGoodsCnt , ");                                    
            strSql.Append(" avgUserCnt = @avgUserCnt , ");                                    
            strSql.Append(" avgPayCnt = @avgPayCnt , ");                                    
            strSql.Append(" avgSmsCnt = @avgSmsCnt , ");                                    
            strSql.Append(" LastSales = @LastSales , ");                                    
            strSql.Append(" LastGoods = @LastGoods , ");                                    
            strSql.Append(" LastUser = @LastUser , ");                                    
            strSql.Append(" LastPay = @LastPay , ");                                    
            strSql.Append(" LastSms = @LastSms , ");                                    
            strSql.Append(" LastRevisit = @LastRevisit , ");                                    
            strSql.Append(" accVer = @accVer , ");                                    
            strSql.Append(" verBgTime = @verBgTime , ");                                    
            strSql.Append(" verEdTime = @verEdTime , ");                                    
            strSql.Append(" verEdDays = @verEdDays , ");                                    
            strSql.Append(" SmsBalance = @SmsBalance , ");                                    
            strSql.Append(" UserBalance = @UserBalance , ");                                    
            strSql.Append(" RevisitClass = @RevisitClass , ");                                    
            strSql.Append(" RevisitItem = @RevisitItem , ");                                    
            strSql.Append(" RevisitDesc = @RevisitDesc , ");                                    
            strSql.Append(" RevisitStatus = @RevisitStatus , ");                                    
            strSql.Append(" RevisitFlag = @RevisitFlag , ");                                    
            strSql.Append(" ParentRevisit = @ParentRevisit  ");            			
			strSql.Append(" where id=@id ");
		
            int row = DapperHelper.Execute(strSql.ToString(), model);
            if (row > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
		}
		
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Sys_SysRevisit_AccInfo ");
			strSql.Append(" where id=@id");

            int rows = DapperHelper.Execute(strSql.ToString(), new { id = id  });
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
		}
		 
		
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Sys_SysRevisit_AccInfo GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select id, accID, CreateTime, ActiveStatus, LoginTimes, LastLoginTime, LastLoginDays, GoodsCnt, UserCnt, SalesCnt, PayCnt, SmsCnt, OrderCnt, OrderSum, avgLoginTimes, avgSalesCnt, avgGoodsCnt, avgUserCnt, avgPayCnt, avgSmsCnt, LastSales, LastGoods, LastUser, LastPay, LastSms, LastRevisit, accVer, verBgTime, verEdTime, verEdDays, SmsBalance, UserBalance, RevisitClass, RevisitItem, RevisitDesc, RevisitStatus, RevisitFlag, ParentRevisit  ");			
			strSql.Append("  from Sys_SysRevisit_AccInfo ");
			strSql.Append(" where id=@id");
            return  DapperHelper.GetModel<Sys_SysRevisit_AccInfo>(strSql.ToString(), new { id  = id  });
		}
		
        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="dapperWheres">查询条件列表</param>
        public List<Sys_SysRevisit_AccInfo> GetList(List<DapperWhere> dapperWheres)
        {
            StringBuilder strSql = new StringBuilder();

            string where = "";
            Dictionary<string, object> parm = new Dictionary<string, object>();
            foreach(DapperWhere item in dapperWheres){
                if (where.Length > 0)
                {
                    where += " and ";
                }
                where += item.Where;
                parm[item.ColumnName] = item.Value;
            }

            strSql.Append("select * ");
            strSql.Append(" FROM Sys_SysRevisit_AccInfo ");
            if (where.Length > 0)
            {
                strSql.Append(" where " + where);
            }

            return DapperHelper.Query<Sys_SysRevisit_AccInfo>(strSql.ToString(), parm).ToList();
        }
		
        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="top">前几行</param>
        /// <param name="dapperWheres">查询条件列表</param>
        /// <param name="filedOrder">排序</param>
        public List<Sys_SysRevisit_AccInfo> GetList(int top, List<DapperWhere> dapperWheres, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();

            string where = "";
            Dictionary<string, object> parm = new Dictionary<string, object>();
            foreach (DapperWhere item in dapperWheres)
            {
                if (where.Length > 0)
                {
                    where += " and ";
                }
                where += item.Where;
                parm[item.ColumnName] = item.Value;
            }

            strSql.Append("select ");

            if (top > 0)
            {
                strSql.Append(" top " + top.ToString() + " ");
            }
            strSql.Append(" * ");
            strSql.Append(" FROM Sys_SysRevisit_AccInfo ");
            if (where.Length > 0)
            {
                strSql.Append(" where " + where);
            }

            if (filedOrder.Length > 0)
            {
                strSql.Append(" order by " + filedOrder );
            }
            strSql.Append(";");
            return DapperHelper.Query<Sys_SysRevisit_AccInfo>(strSql.ToString(), parm).ToList();
        }
		 
		

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="top">前几行</param>
        /// <param name="columnName">需要获取的列名<para>为了方便不在处理，列与列用逗号分开，参照SQL写法</para></param>
        /// <param name="dapperWheres">查询条件列表</param>
        /// <param name="filedOrder">排序</param>
        public List<T> GetList<T>(int top, string columnName, List<DapperWhere> dapperWheres, string filedOrder)
        { 

            StringBuilder strSql = new StringBuilder();

            string where = "";
            Dictionary<string, object> parm = new Dictionary<string, object>();
            foreach (DapperWhere item in dapperWheres)
            {
                if (where.Length > 0)
                {
                    where += " and ";
                }
                where += item.Where;
                parm[item.ColumnName] = item.Value;
            }

            strSql.Append("select ");

            if (top > 0)
            {
                strSql.Append(" top " + top.ToString() + " ");
            }
            strSql.Append(" " + columnName + " ");
            strSql.Append(" FROM Sys_SysRevisit_AccInfo ");
            if (where.Length > 0)
            {
                strSql.Append(" where " + where);
            }

            if (filedOrder.Length > 0)
            {
                strSql.Append(" order by " + filedOrder);
            }
            strSql.Append(";");
            return DapperHelper.Query<T>(strSql.ToString(), parm).ToList();
        }

        /// <summary>
        /// 得到总数
        /// </summary>
        /// <param name="dapperWheres">条件列表</param>
        /// <returns></returns>
        public int GetCount(List<DapperWhere> dapperWheres)
        {
            string where = "";
            Dictionary<string, object> parm = new Dictionary<string, object>();
            foreach (DapperWhere item in dapperWheres)
            {
                if (where.Length > 0)
                {
                    where += " and ";
                }
                where += item.Where;
                parm[item.ColumnName] = item.Value;
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select count(*) from Sys_SysRevisit_AccInfo ");

            if (where.Length > 0)
            {
                strSql.Append(" where " + where);
            }
            object obj = DapperHelper.ExecuteScalar(strSql.ToString(), parm);
            if (obj != null)
            {
                return Convert.ToInt32(obj);
            }
            else
            {
                return 0;
            }
        }
   
        /// <summary>
        /// 分页得到列表
        /// </summary>
        /// <param name="pageIndex">显示页号</param>
        /// <param name="pageSize">每页显示数</param>
        /// <param name="dapperWheres">条件列表</param>
        /// <param name="filedOrder">排序</param>
        /// <returns>返回列表</returns>
        public List<Sys_SysRevisit_AccInfo> GetList(int pageIndex,int pageSize,List<DapperWhere> dapperWheres,string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            string where = "";
            Dictionary<string, object> parm = new Dictionary<string, object>();
            foreach (DapperWhere item in dapperWheres)
            {
                if (where.Length > 0)
                {
                    where += " and ";
                }
                where += item.Where;
                parm[item.ColumnName] = item.Value;
            }
            strSql.Append(" SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER ( ");
            if (filedOrder.Length > 0)
            {
                strSql.Append(" order by " + filedOrder + " ");
            }
            else
            {
                strSql.Append(" order by T.id  desc");
            }
            strSql.Append(" )AS Row, * from Sys_SysRevisit_AccInfo T  ");

            if (where.Length > 0)
            {
                strSql.Append(" where " + where + " ");
            }
            strSql.Append(" ) TT ");
            strSql.Append(" WHERE TT.Row between @startIndex and @endIndex; ");

            if (pageIndex < 1)
            {
                pageIndex = 1;
            }

            int bgNumber = ((pageIndex - 1) * pageSize) + 1;
            int edNumber = (pageIndex) * pageSize;
            parm["startIndex"] = bgNumber;
            parm["endIndex"] = edNumber;

            return DapperHelper.Query<Sys_SysRevisit_AccInfo>(strSql.ToString(), parm).ToList();
        }


        /// <summary>
        /// 分页得到列表
        /// </summary>
        /// <param name="pageIndex">显示页号</param>
        /// <param name="pageSize">每页显示数</param>
        /// <param name="columnName">需要获取的列名<para>为了方便不在处理，列与列用逗号分开，参照SQL写法</para></param>
        /// <param name="dapperWheres">条件列表</param>
        /// <param name="filedOrder">排序</param>
        /// <returns>返回列表</returns>
        public List<T> GetList<T>(int pageIndex, int pageSize, string columnName, List<DapperWhere> dapperWheres, string filedOrder)
        {
            if (columnName.Length < 1)
            {
                columnName = "*";
            }

            StringBuilder strSql = new StringBuilder();
            string where = "";
            Dictionary<string, object> parm = new Dictionary<string, object>();
            foreach (DapperWhere item in dapperWheres)
            {
                if (where.Length > 0)
                {
                    where += " and ";
                }
                where += item.Where;
                parm[item.ColumnName] = item.Value;
            }
            strSql.Append(" SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER ( ");
            if (filedOrder.Length > 0)
            {
                strSql.Append(" order by " + filedOrder + " ");
            }
            else
            {
                strSql.Append(" order by T.id  desc");
            }
            strSql.Append(" )AS overRow, " + columnName + " from Sys_SysRevisit_AccInfo T  ");

            if (where.Length > 0)
            {
                strSql.Append(" where " + where + " ");
            }
            strSql.Append(" ) TT ");
            strSql.Append(" WHERE TT.overRow between @startIndex and @endIndex; ");

            if (pageIndex < 1)
            {
                pageIndex = 1;
            }

            int bgNumber = ((pageIndex - 1) * pageSize) + 1;
            int edNumber = (pageIndex) * pageSize;
            parm["startIndex"] = bgNumber;
            parm["endIndex"] = edNumber;

            return DapperHelper.Query<T>(strSql.ToString(), parm).ToList();
        }

	}
}
 