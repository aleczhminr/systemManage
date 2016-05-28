using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using DAL.Base;
using Model;
namespace BLL.Base
{
	 	//SysRpt_ShopActive
		public static class SysRpt_ShopActiveBaseBLL
	{
   		  
	 
		
		#region  Method
        /// <summary>
        /// 新增加一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Add(SysRpt_ShopActive model)
		{   
		 SysRpt_ShopActiveBaseDAL dal=new SysRpt_ShopActiveBaseDAL();
		 return dal.Add(model);
		}
        
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static bool Update(SysRpt_ShopActive model)
        {
		 SysRpt_ShopActiveBaseDAL dal=new SysRpt_ShopActiveBaseDAL();
            return dal.Update(model);
        }
         /// <summary>
        /// 删除一条数据
        /// </summary>
        public static bool Delete(int id){
        SysRpt_ShopActiveBaseDAL dal=new SysRpt_ShopActiveBaseDAL();
            return dal.Delete(id);
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static SysRpt_ShopActive GetModel(int id){
        SysRpt_ShopActiveBaseDAL dal=new SysRpt_ShopActiveBaseDAL();
        return dal.GetModel(id);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="dapperWheres">查询条件列表</param>
        public static List<SysRpt_ShopActive> GetList(List<DapperWhere> dapperWheres)
        {
        	SysRpt_ShopActiveBaseDAL dal=new SysRpt_ShopActiveBaseDAL();
        	return dal.GetList(dapperWheres);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="top">前几行</param>
        /// <param name="dapperWheres">查询条件列表</param>
        /// <param name="filedOrder">排序</param>
        public static List<SysRpt_ShopActive> GetList(int top, List<DapperWhere> dapperWheres, string filedOrder){
        	SysRpt_ShopActiveBaseDAL dal=new SysRpt_ShopActiveBaseDAL();
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
        	SysRpt_ShopActiveBaseDAL dal=new SysRpt_ShopActiveBaseDAL();
        	return dal.GetList<T>(top,columnName,dapperWheres,filedOrder);
        }
        /// <summary>
        /// 得到总数
        /// </summary>
        /// <param name="dapperWheres">条件列表</param>
        /// <returns></returns>
        public static int GetCount(List<DapperWhere> dapperWheres)
        {
        	SysRpt_ShopActiveBaseDAL dal=new SysRpt_ShopActiveBaseDAL();
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
        public static List<SysRpt_ShopActive> GetList(int pageIndex,int pageSize,List<DapperWhere> dapperWheres,string filedOrder)
        {
        	SysRpt_ShopActiveBaseDAL dal=new SysRpt_ShopActiveBaseDAL();
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
        	SysRpt_ShopActiveBaseDAL dal=new SysRpt_ShopActiveBaseDAL();
        	return dal.GetList<T>(pageIndex,pageSize,columnName,dapperWheres,filedOrder);
        }
        
        
        
#endregion
   
	}
}