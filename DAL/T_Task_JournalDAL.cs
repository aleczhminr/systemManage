using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// 前台分享记录
    /// </summary>
    public class T_Task_JournalDAL : Base.T_Task_JournalBaseDAL
    {

        /// <summary>
        /// 处理任务
        /// </summary>
        /// <param name="id"></param>
        /// <returns>｛-1：出现异常，0：信息不存在，1：已经处理完成，2、处理完成｝</returns>
        public int HandleTask(int id)
        {

            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append(" declare @now int,@new int,@num int,@acc_id int,@status int,@t_explan varchar(200),@t_type int;");
            sqlStr.Append(" select @acc_id=acc_id,@status=t_status,@t_explan=t_explan from T_Task_Journal where id=@id ;");
            sqlStr.Append(" if(@acc_id is null) ");
            sqlStr.Append(" begin ");
            sqlStr.Append(" 	select 0; ");//信息不存在
            sqlStr.Append(" end ");
            sqlStr.Append(" else ");
            sqlStr.Append(" begin ");
            sqlStr.Append(" 	if(@status=0) ");
            sqlStr.Append(" 	begin ");
            sqlStr.Append(" 		set @num=0; ");
            sqlStr.Append(" 		select @now=isnull(integral,0) from T_Business where accountid=@acc_id; ");
            sqlStr.Append(" 		if(@t_explan='分享生意专家') ");
            sqlStr.Append(" 		begin ");
            sqlStr.Append(" 			set @num=30;set @t_type=6; ");
            sqlStr.Append(" 		end ");
            sqlStr.Append(" 		else if(@t_explan='百度推广') ");
            sqlStr.Append(" 		begin ");
            sqlStr.Append(" 			set @num=50;set @t_explan='支持生意专家';set @t_type=5; ");
            sqlStr.Append(" 		end ");
            sqlStr.Append(" 		else if(@t_explan='推荐好友') ");
            sqlStr.Append(" 		begin ");
            sqlStr.Append(" 			set @num=100;set @t_type=10; ");
            sqlStr.Append(" 		end ");
            sqlStr.Append(" 		else if(@t_explan='关注微信') ");
            sqlStr.Append(" 		begin ");
            sqlStr.Append(" 			set @num=100;set @t_type=9; ");
            sqlStr.Append(" 		end ");
            sqlStr.Append(" 		if(@num>0) ");
            sqlStr.Append(" 		begin ");
            sqlStr.Append(" 			set @new=@now+@num; ");
            sqlStr.Append(" 		INSERT INTO T_LogInfo(accID, LogType, Keys, OriginalVal, EditVal, FinialVal, CreatTime, ReMark,Flags) ");
            sqlStr.Append(" 		VALUES	(@acc_id,@t_type,'Integral',@now,@num,@new,GETDATE(),@t_explan,isnull(@num,0)); ");
            sqlStr.Append(" 		update T_Business set integral=@new where accountid=@acc_id; ");
            sqlStr.Append(" 		update T_Task_Journal set t_status=1 where id=@id; ");
            sqlStr.Append(" 		select 2; ");//处理完成
            sqlStr.Append(" 		end ");
            sqlStr.Append(" 	end ");
            sqlStr.Append(" 	else ");
            sqlStr.Append(" 		select 1; ");//已经处理完成
            sqlStr.Append(" end ");

            object rid = HelperForFrontend.ExecuteScalar(sqlStr.ToString(), new { id = id });

            if (rid != null)
            {
                return Convert.ToInt32(rid);
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_Task_Journal ");
            strSql.Append(" where id=@id");


            int rNum = HelperForFrontend.Execute(strSql.ToString(), new { id = id });
            if (rNum > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="auditCon"></param>
        /// <returns></returns>
        public bool TaskAuditOk(int id, string auditCon)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Task_Journal set t_status=2,t_source=ISNULL(t_source,'')+@source where id=@id and t_status=0;");
            int r = HelperForFrontend.Execute(strSql.ToString(), new 
            {
                id = id,
                source = auditCon
            });
            if (r > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// api 发送积分
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int ApiTaskProvideIntegral(int id)
        {
            Dapper.AppApi api=new Dapper.AppApi();
            return api.TaskProvideIntegral(id);
        }
    }
}
