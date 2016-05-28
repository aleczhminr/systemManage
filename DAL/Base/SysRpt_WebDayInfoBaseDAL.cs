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
        /// ������һ������
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
        /// ����һ������
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
        /// ɾ��һ������
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
        /// �õ�һ������ʵ��
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
        /// ��������б�
        /// </summary>
        /// <param name="dapperWheres">��ѯ�����б�</param>
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
        /// ��������б�
        /// </summary>
        /// <param name="top">ǰ����</param>
        /// <param name="dapperWheres">��ѯ�����б�</param>
        /// <param name="filedOrder">����</param>
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
        /// ��������б�
        /// </summary>
        /// <param name="top">ǰ����</param>
        /// <param name="columnName">��Ҫ��ȡ������<para>Ϊ�˷��㲻�ڴ����������ö��ŷֿ�������SQLд��</para></param>
        /// <param name="dapperWheres">��ѯ�����б�</param>
        /// <param name="filedOrder">����</param>
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
        /// �õ�����
        /// </summary>
        /// <param name="dapperWheres">�����б�</param>
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
        /// ��ҳ�õ��б�
        /// </summary>
        /// <param name="pageIndex">��ʾҳ��</param>
        /// <param name="pageSize">ÿҳ��ʾ��</param>
        /// <param name="dapperWheres">�����б�</param>
        /// <param name="filedOrder">����</param>
        /// <returns>�����б�</returns>
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
        /// ��ҳ�õ��б�<para>���û�ж���Ŀ��Դ��� object ����  dynamic </para>
        /// </summary>
        /// <param name="pageIndex">��ʾҳ��</param>
        /// <param name="pageSize">ÿҳ��ʾ��</param>
        /// <param name="columnName">��Ҫ��ȡ������<para>Ϊ�˷��㲻�ڴ����������ö��ŷֿ�������SQLд��</para></param>
        /// <param name="dapperWheres">�����б�</param>
        /// <param name="filedOrder">����</param>
        /// <returns>�����б�</returns>
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
