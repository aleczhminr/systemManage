using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using DAL.Base;
using Model;
namespace BLL.Base
{
	 	//Sys_agent_Loginfo
		public static class Sys_agent_LoginfoBaseBLL
	{
   		  
	 
		
		#region  Method
        /// <summary>
        /// 新增加一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Add(Sys_agent_Loginfo model)
		{   
		 Sys_agent_LoginfoBaseDAL dal=new Sys_agent_LoginfoBaseDAL();
		 return dal.Add(model);
		}
        
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static bool Update(Sys_agent_Loginfo model)
        {
		 Sys_agent_LoginfoBaseDAL dal=new Sys_agent_LoginfoBaseDAL();
            return dal.Update(model);
        }
         /// <summary>
        /// 删除一条数据
        /// </summary>
        public static bool Delete(int id){
        Sys_agent_LoginfoBaseDAL dal=new Sys_agent_LoginfoBaseDAL();
            return dal.Delete(id);
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static Sys_agent_Loginfo GetModel(int id){
        Sys_agent_LoginfoBaseDAL dal=new Sys_agent_LoginfoBaseDAL();
        return dal.GetModel(id);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="dapperWheres">查询条件列表</param>
        public static List<Sys_agent_Loginfo> GetList(List<DapperWhere> dapperWheres)
        {
        	Sys_agent_LoginfoBaseDAL dal=new Sys_agent_LoginfoBaseDAL();
        	return dal.GetList(dapperWheres);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="top">前几行</param>
        /// <param name="dapperWheres">查询条件列表</param>
        /// <param name="filedOrder">排序</param>
        public static List<Sys_agent_Loginfo> GetList(int top, List<DapperWhere> dapperWheres, string filedOrder){
        	Sys_agent_LoginfoBaseDAL dal=new Sys_agent_LoginfoBaseDAL();
        	return dal.GetList(top,dapperWheres,filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="top">前几行</param>
        /// <param name="columnName">需要获取的列名<para>为了方便不在处理，列与列用逗号分开，参照SQL写法</para></param>
        /// <param name="dapperWheres">查询条件列表</param>
        /// <param name="filedOrder">排序</param>
        public static List<T> GetList<T>(int top, string columnName, List<DapperWhere> dapperWheres, string filedOrder)
        {
        	Sys_agent_LoginfoBaseDAL dal=new Sys_agent_LoginfoBaseDAL();
        	return dal.GetList<T>(top,columnName,dapperWheres,filedOrder);
        }
        /// <summary>
        /// 得到总数
        /// </summary>
        /// <param name="dapperWheres">条件列表</param>
        /// <returns></returns>
        public static int GetCount(List<DapperWhere> dapperWheres)
        {
        	Sys_agent_LoginfoBaseDAL dal=new Sys_agent_LoginfoBaseDAL();
        	return dal.GetCount(dapperWheres);
        }
        /// <summary>
        /// 分页得到列表
        /// </summary>
        /// <param name="pageIndex">显示页号</param>
        /// <param name="pageSize">每页显示数</param>
        /// <param name="dapperWheres">条件列表</param>
        /// <param name="filedOrder">排序</param>
        /// <returns>返回列表</returns>
        public static List<Sys_agent_Loginfo> GetList(int pageIndex,int pageSize,List<DapperWhere> dapperWheres,string filedOrder)
        {
        	Sys_agent_LoginfoBaseDAL dal=new Sys_agent_LoginfoBaseDAL();
        	return dal.GetList(pageIndex,pageSize,dapperWheres,filedOrder);
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
        public static List<T> GetList<T>(int pageIndex, int pageSize, string columnName, List<DapperWhere> dapperWheres, string filedOrder)
        {
        	Sys_agent_LoginfoBaseDAL dal=new Sys_agent_LoginfoBaseDAL();
        	return dal.GetList<T>(pageIndex,pageSize,columnName,dapperWheres,filedOrder);
        }
        
        
        
#endregion
   
	}
}