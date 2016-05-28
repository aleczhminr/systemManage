using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using Model;
using System.Linq;
namespace DAL.Base
{
    //SysRpt_WebDayInfo
    /// <summary>
    /// SysRpt_WebDayInfo
    /// </summary>
    public class SysRpt_WebDayInfoBaseDAL
    {
        /// <summary>
        /// 新增加一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(SysRpt_WebDayInfo model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SysRpt_WebDayInfo(");
            strSql.Append("saleMoney,memSaleNum,memSaleMoney,retailSaleNum,retailSaleMoney,orderNum,orderMoney,orderAccount,loginNum,loginPaidNum,S_Date,reg_Attention,registration,moodNum,unknownNum,activeNum,faithfulNum,dormancyNum,outflowNum,outlayNum,outlayMoney,accountNum,addGoodsNum,saleGoodsNum,feedBackCnt,addTime,clientLogNum,acc_Rep,acc_RepCount,Attention,NewAccNum,userNum,smsNum,freeSmsNum,sysSmsNum,saleNum");
            strSql.Append(") values (");
            strSql.Append("@saleMoney,@memSaleNum,@memSaleMoney,@retailSaleNum,@retailSaleMoney,@orderNum,@orderMoney,@orderAccount,@loginNum,@loginPaidNum,@S_Date,@reg_Attention,@registration,@moodNum,@unknownNum,@activeNum,@faithfulNum,@dormancyNum,@outflowNum,@outlayNum,@outlayMoney,@accountNum,@addGoodsNum,@saleGoodsNum,@feedBackCnt,@addTime,@clientLogNum,@acc_Rep,@acc_RepCount,@Attention,@NewAccNum,@userNum,@smsNum,@freeSmsNum,@sysSmsNum,@saleNum");
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
        public bool Update(SysRpt_WebDayInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SysRpt_WebDayInfo set ");

            strSql.Append(" saleMoney = @saleMoney , ");
            strSql.Append(" memSaleNum = @memSaleNum , ");
            strSql.Append(" memSaleMoney = @memSaleMoney , ");
            strSql.Append(" retailSaleNum = @retailSaleNum , ");
            strSql.Append(" retailSaleMoney = @retailSaleMoney , ");
            strSql.Append(" orderNum = @orderNum , ");
            strSql.Append(" orderMoney = @orderMoney , ");
            strSql.Append(" orderAccount = @orderAccount , ");
            strSql.Append(" loginNum = @loginNum , ");
            strSql.Append(" loginPaidNum = @loginPaidNum , ");
            strSql.Append(" S_Date = @S_Date , ");
            strSql.Append(" reg_Attention = @reg_Attention , ");
            strSql.Append(" registration = @registration , ");
            strSql.Append(" moodNum = @moodNum , ");
            strSql.Append(" unknownNum = @unknownNum , ");
            strSql.Append(" activeNum = @activeNum , ");
            strSql.Append(" faithfulNum = @faithfulNum , ");
            strSql.Append(" dormancyNum = @dormancyNum , ");
            strSql.Append(" outflowNum = @outflowNum , ");
            strSql.Append(" outlayNum = @outlayNum , ");
            strSql.Append(" outlayMoney = @outlayMoney , ");
            strSql.Append(" accountNum = @accountNum , ");
            strSql.Append(" addGoodsNum = @addGoodsNum , ");
            strSql.Append(" saleGoodsNum = @saleGoodsNum , ");
            strSql.Append(" feedBackCnt = @feedBackCnt , ");
            strSql.Append(" addTime = @addTime , ");
            strSql.Append(" clientLogNum = @clientLogNum , ");
            strSql.Append(" acc_Rep = @acc_Rep , ");
            strSql.Append(" acc_RepCount = @acc_RepCount , ");
            strSql.Append(" Attention = @Attention , ");
            strSql.Append(" NewAccNum = @NewAccNum , ");
            strSql.Append(" userNum = @userNum , ");
            strSql.Append(" smsNum = @smsNum , ");
            strSql.Append(" freeSmsNum = @freeSmsNum , ");
            strSql.Append(" sysSmsNum = @sysSmsNum , ");
            strSql.Append(" saleNum = @saleNum  ");
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
            strSql.Append("delete from SysRpt_WebDayInfo ");
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
        public SysRpt_WebDayInfo GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id, saleMoney, memSaleNum, memSaleMoney, retailSaleNum, retailSaleMoney, orderNum, orderMoney, orderAccount, loginNum, loginPaidNum, S_Date, reg_Attention, registration, moodNum, unknownNum, activeNum, faithfulNum, dormancyNum, outflowNum, outlayNum, outlayMoney, accountNum, addGoodsNum, saleGoodsNum, feedBackCnt, addTime, clientLogNum, acc_Rep, acc_RepCount, Attention, NewAccNum, userNum, smsNum, freeSmsNum, sysSmsNum, saleNum  ");
            strSql.Append("  from SysRpt_WebDayInfo ");
            strSql.Append(" where id=@id");
            return DapperHelper.GetModel<SysRpt_WebDayInfo>(strSql.ToString(), new { id = id });
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="dapperWheres">查询条件列表</param>
        public List<SysRpt_WebDayInfo> GetList(List<DapperWhere> dapperWheres)
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
            strSql.Append(" FROM SysRpt_WebDayInfo ");
            if (where.Length > 0)
            {
                strSql.Append(" where " + where);
            }

            return DapperHelper.Query<SysRpt_WebDayInfo>(strSql.ToString(), parm).ToList();
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="top">前几行</param>
        /// <param name="dapperWheres">查询条件列表</param>
        /// <param name="filedOrder">排序</param>
        public List<SysRpt_WebDayInfo> GetList(int top, List<DapperWhere> dapperWheres, string filedOrder)
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
            strSql.Append(" FROM SysRpt_WebDayInfo ");
            if (where.Length > 0)
            {
                strSql.Append(" where " + where);
            }

            if (filedOrder.Length > 0)
            {
                strSql.Append(" order by " + filedOrder);
            }
            strSql.Append(";");
            return DapperHelper.Query<SysRpt_WebDayInfo>(strSql.ToString(), parm).ToList();
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
            strSql.Append(" FROM SysRpt_WebDayInfo ");
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
            strSql.Append(" select count(*) from SysRpt_WebDayInfo ");

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
        public List<SysRpt_WebDayInfo> GetList(int pageIndex, int pageSize, List<DapperWhere> dapperWheres, string filedOrder)
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
            strSql.Append(" )AS Row, * from SysRpt_WebDayInfo T  ");

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

            return DapperHelper.Query<SysRpt_WebDayInfo>(strSql.ToString(), parm).ToList();
        }


        /// <summary>
        /// 分页得到列表<para>如果没有对像的可以传入 object 或者  dynamic </para>
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
            strSql.Append(" )AS overRow, " + columnName + " from SysRpt_WebDayInfo T  ");

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
