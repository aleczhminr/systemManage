using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using Model;
using System.Linq;
namespace DAL.Base
{
    //SysRpt_ShopDayInfo
    /// <summary>
    /// 店铺每日信息汇总
    /// </summary>
    public class SysRpt_ShopDayInfoBaseDAL
    {
        /// <summary>
        /// 新增加一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(SysRpt_ShopDayInfo model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SysRpt_ShopDayInfo(");
            strSql.Append("dayDate,regTime,accountid,AgentId,userNum,saleGoodsNum,saleNum,saleMoney,memSaleNum,memSaleMoney,retailSaleNum,retailSaleMoney,smsNum,freeSmsNum,goodsNum,loginNum,lastLoginTime,registration,moodNum,orderMoney,orderNum,integration,integrationUse,lastIP,lastAddress,outlayNum,outlayMoney,active,allLoginNum,addTime,acc_Rep");
            strSql.Append(") values (");
            strSql.Append("@dayDate,@regTime,@accountid,@AgentId,@userNum,@saleGoodsNum,@saleNum,@saleMoney,@memSaleNum,@memSaleMoney,@retailSaleNum,@retailSaleMoney,@smsNum,@freeSmsNum,@goodsNum,@loginNum,@lastLoginTime,@registration,@moodNum,@orderMoney,@orderNum,@integration,@integrationUse,@lastIP,@lastAddress,@outlayNum,@outlayMoney,@active,@allLoginNum,@addTime,@acc_Rep");
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
        public bool Update(SysRpt_ShopDayInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SysRpt_ShopDayInfo set ");

            strSql.Append(" dayDate = @dayDate , ");
            strSql.Append(" regTime = @regTime , ");
            strSql.Append(" accountid = @accountid , ");
            strSql.Append(" AgentId = @AgentId , ");
            strSql.Append(" userNum = @userNum , ");
            strSql.Append(" saleGoodsNum = @saleGoodsNum , ");
            strSql.Append(" saleNum = @saleNum , ");
            strSql.Append(" saleMoney = @saleMoney , ");
            strSql.Append(" memSaleNum = @memSaleNum , ");
            strSql.Append(" memSaleMoney = @memSaleMoney , ");
            strSql.Append(" retailSaleNum = @retailSaleNum , ");
            strSql.Append(" retailSaleMoney = @retailSaleMoney , ");
            strSql.Append(" smsNum = @smsNum , ");
            strSql.Append(" freeSmsNum = @freeSmsNum , ");
            strSql.Append(" goodsNum = @goodsNum , ");
            strSql.Append(" loginNum = @loginNum , ");
            strSql.Append(" lastLoginTime = @lastLoginTime , ");
            strSql.Append(" registration = @registration , ");
            strSql.Append(" moodNum = @moodNum , ");
            strSql.Append(" orderMoney = @orderMoney , ");
            strSql.Append(" orderNum = @orderNum , ");
            strSql.Append(" integration = @integration , ");
            strSql.Append(" integrationUse = @integrationUse , ");
            strSql.Append(" lastIP = @lastIP , ");
            strSql.Append(" lastAddress = @lastAddress , ");
            strSql.Append(" outlayNum = @outlayNum , ");
            strSql.Append(" outlayMoney = @outlayMoney , ");
            strSql.Append(" active = @active , ");
            strSql.Append(" allLoginNum = @allLoginNum , ");
            strSql.Append(" addTime = @addTime , ");
            strSql.Append(" acc_Rep = @acc_Rep  ");
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

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SysRpt_ShopDayInfo ");
            strSql.Append(" where id=@id");

            int rows = DapperHelper.Execute(strSql.ToString(), new { id = id });
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
        public SysRpt_ShopDayInfo GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id, dayDate, regTime, accountid, AgentId, userNum, saleGoodsNum, saleNum, saleMoney, memSaleNum, memSaleMoney, retailSaleNum, retailSaleMoney, smsNum, freeSmsNum, goodsNum, loginNum, lastLoginTime, registration, moodNum, orderMoney, orderNum, integration, integrationUse, lastIP, lastAddress, outlayNum, outlayMoney, active, allLoginNum, addTime, acc_Rep  ");
            strSql.Append("  from SysRpt_ShopDayInfo ");
            strSql.Append(" where id=@id");
            return DapperHelper.GetModel<SysRpt_ShopDayInfo>(strSql.ToString(), new { id = id });
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="dapperWheres">查询条件列表</param>
        public List<SysRpt_ShopDayInfo> GetList(List<DapperWhere> dapperWheres)
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

            strSql.Append("select * ");
            strSql.Append(" FROM SysRpt_ShopDayInfo ");
            if (where.Length > 0)
            {
                strSql.Append(" where " + where);
            }

            return DapperHelper.Query<SysRpt_ShopDayInfo>(strSql.ToString(), parm).ToList();
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="top">前几行</param>
        /// <param name="dapperWheres">查询条件列表</param>
        /// <param name="filedOrder">排序</param>
        public List<SysRpt_ShopDayInfo> GetList(int top, List<DapperWhere> dapperWheres, string filedOrder)
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
            strSql.Append(" FROM SysRpt_ShopDayInfo ");
            if (where.Length > 0)
            {
                strSql.Append(" where " + where);
            }

            if (filedOrder.Length > 0)
            {
                strSql.Append(" order by " + filedOrder);
            }
            strSql.Append(";");
            return DapperHelper.Query<SysRpt_ShopDayInfo>(strSql.ToString(), parm).ToList();
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
            strSql.Append(" FROM SysRpt_ShopDayInfo ");
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
        /// 获得订单数据列表
        /// </summary>
        /// <param name="top">前几行</param>
        /// <param name="columnName">需要获取的列名<para>为了方便不在处理，列与列用逗号分开，参照SQL写法</para></param>
        /// <param name="dapperWheres">查询条件列表</param>
        /// <param name="filedOrder">排序</param>
        public  List<T> GetOrderList<T>(int top, string columnName, List<DapperWhere> dapperWheres, string filedOrder)
        {
            var strSql = new StringBuilder();
            var where = string.Empty;
            var parm = new Dictionary<string, object>();
            foreach (var item in dapperWheres)
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
            strSql.Append(" FROM [i200].[dbo].[T_OrderInfo]");
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
        /// 获得订单数据列表
        /// </summary>
        /// <param name="top">前几行</param>
        /// <param name="columnName">需要获取的列名<para>为了方便不在处理，列与列用逗号分开，参照SQL写法</para></param>
        /// <param name="dapperWheres">查询条件列表</param>
        /// <param name="filedOrder">排序</param>
        /// <param name="type">类型</param>
        public List<T> GetMonthSectionList<T>(int top, string columnName, List<DapperWhere> dapperWheres, string filedOrder,string type)
        {
            var strSql = new StringBuilder();
            var where = string.Empty;
            var parm = new Dictionary<string, object>();
            foreach (var item in dapperWheres)
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
            switch (type)
            {
                case "reg":
                    strSql.Append(" FROM [i200].[dbo].[T_UserInfo]");
                    break;
                case "sell":
                    strSql.Append(" FROM [i200].[dbo].[T_SaleInfo]");
                    break;
                case "goods":
                    strSql.Append(" FROM [i200].[dbo].[T_GoodsInfo] ");
                    break;
                case "order":
                    strSql.Append(" FROM [i200].[dbo].[T_OrderInfo]");
                    break;
                case "pay":
                    strSql.Append(" FROM [i200].[dbo].[t_PayRecord]");
                    break;
            }
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
            strSql.Append(" select count(*) from SysRpt_ShopDayInfo ");

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
        public List<SysRpt_ShopDayInfo> GetList(int pageIndex, int pageSize, List<DapperWhere> dapperWheres, string filedOrder)
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
            strSql.Append(" )AS Row, * from SysRpt_ShopDayInfo T  ");

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

            return DapperHelper.Query<SysRpt_ShopDayInfo>(strSql.ToString(), parm).ToList();
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
            strSql.Append(" )AS overRow, " + columnName + " from SysRpt_ShopDayInfo T  ");

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
