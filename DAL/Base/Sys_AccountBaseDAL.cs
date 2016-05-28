using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using Model;
using System.Linq;
namespace DAL.Base
{
	 	//Sys_Account
			/// <summary>
		/// Sys_Account
		/// </summary>
	public  class Sys_AccountBaseDAL
	{ 
        /// <summary>
        /// 新增加一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(Sys_Account model)
        {

            StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into Sys_Account(");			
            strSql.Append("accid,a_QQ,a_WeiXin,a_Tel,a_Address,a_Industry,a_Name,a_IdentityNumber,a_ShopSize,a_Operate,a_Duration,a_OtherSoftware,a_Remark,a_OtherSoftwareType,a_CustomerSourceType,a_CustomerSource,feedbackTel,feedbackQQ,a_UseCause,sysAddress");
			strSql.Append(") values (");
            strSql.Append("@accid,@a_QQ,@a_WeiXin,@a_Tel,@a_Address,@a_Industry,@a_Name,@a_IdentityNumber,@a_ShopSize,@a_Operate,@a_Duration,@a_OtherSoftware,@a_Remark,@a_OtherSoftwareType,@a_CustomerSourceType,@a_CustomerSource,@feedbackTel,@feedbackQQ,@a_UseCause,@sysAddress");            
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
		public bool Update(Sys_Account model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Sys_Account set ");
			                                                
            strSql.Append(" accid = @accid , ");                                    
            strSql.Append(" a_QQ = @a_QQ , ");                                    
            strSql.Append(" a_WeiXin = @a_WeiXin , ");                                    
            strSql.Append(" a_Tel = @a_Tel , ");                                    
            strSql.Append(" a_Address = @a_Address , ");                                    
            strSql.Append(" a_Industry = @a_Industry , ");                                    
            strSql.Append(" a_Name = @a_Name , ");                                    
            strSql.Append(" a_IdentityNumber = @a_IdentityNumber , ");                                    
            strSql.Append(" a_ShopSize = @a_ShopSize , ");                                    
            strSql.Append(" a_Operate = @a_Operate , ");                                    
            strSql.Append(" a_Duration = @a_Duration , ");                                    
            strSql.Append(" a_OtherSoftware = @a_OtherSoftware , ");                                    
            strSql.Append(" a_Remark = @a_Remark , ");                                    
            strSql.Append(" a_OtherSoftwareType = @a_OtherSoftwareType , ");                                    
            strSql.Append(" a_CustomerSourceType = @a_CustomerSourceType , ");                                    
            strSql.Append(" a_CustomerSource = @a_CustomerSource , ");                                    
            strSql.Append(" feedbackTel = @feedbackTel , ");                                    
            strSql.Append(" feedbackQQ = @feedbackQQ , ");                                    
            strSql.Append(" a_UseCause = @a_UseCause , ");                                    
            strSql.Append(" sysAddress = @sysAddress  ");            			
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
			strSql.Append("delete from Sys_Account ");
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
		public Sys_Account GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select id, accid, a_QQ, a_WeiXin, a_Tel, a_Address, a_Industry, a_Name, a_IdentityNumber, a_ShopSize, a_Operate, a_Duration, a_OtherSoftware, a_Remark, a_OtherSoftwareType, a_CustomerSourceType, a_CustomerSource, feedbackTel, feedbackQQ, a_UseCause, sysAddress  ");			
			strSql.Append("  from Sys_Account ");
			strSql.Append(" where id=@id");
            return  DapperHelper.GetModel<Sys_Account>(strSql.ToString(), new { id  = id  });
		}
		
        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="dapperWheres">查询条件列表</param>
        public List<Sys_Account> GetList(List<DapperWhere> dapperWheres)
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
            strSql.Append(" FROM Sys_Account ");
            if (where.Length > 0)
            {
                strSql.Append(" where " + where);
            }

            return DapperHelper.Query<Sys_Account>(strSql.ToString(), parm).ToList();
        }
		
        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="top">前几行</param>
        /// <param name="dapperWheres">查询条件列表</param>
        /// <param name="filedOrder">排序</param>
        public List<Sys_Account> GetList(int top, List<DapperWhere> dapperWheres, string filedOrder)
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
            strSql.Append(" FROM Sys_Account ");
            if (where.Length > 0)
            {
                strSql.Append(" where " + where);
            }

            if (filedOrder.Length > 0)
            {
                strSql.Append(" order by " + filedOrder );
            }
            strSql.Append(";");
            return DapperHelper.Query<Sys_Account>(strSql.ToString(), parm).ToList();
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
            strSql.Append(" FROM Sys_Account ");
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
            strSql.Append(" select count(*) from Sys_Account ");

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
        public List<Sys_Account> GetList(int pageIndex,int pageSize,List<DapperWhere> dapperWheres,string filedOrder)
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
            strSql.Append(" )AS Row, * from Sys_Account T  ");

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

            return DapperHelper.Query<Sys_Account>(strSql.ToString(), parm).ToList();
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
            strSql.Append(" )AS overRow, " + columnName + " from Sys_Account T  ");

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
 