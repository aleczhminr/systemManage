using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Utility;

namespace DAL
{
    public class RequirementManageDAL
    {
        /// <summary>
        /// 添加新的需求Model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddNewModel(RequirementManage model)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(
                "insert into RequirementManage (CategoryId,RequirementType,Description,Status,CreateTime,Operator,AccId,RefId,OriginDesc,Terminal,UserVal,Difficult) " +
                "Values(@CategoryId,@RequirementType,@Description,@Status,@CreateTime,@Operator,@AccId,@RefId,@OriginDesc,@Terminal,@UserVal,@Difficult);" +
                "select @@IDENTITY;");

            try
            {
                return DapperHelper.ExecuteScalar<int>(strSql.ToString(), new
                {
                    CategoryId = model.CategoryId,
                    RequirementType = model.RequirementType,
                    Description = model.Description,
                    Status = model.Status,
                    CreateTime = DateTime.Now,
                    Operator = model.Operator,
                    AccId = model.AccId,
                    RefId = model.RefId,
                    OriginDesc=model.OriginDesc,
                    Terminal = model.Terminal,
                    UserVal = model.UserVal,
                    Difficult = model.Difficult
                });
            }
            catch (Exception ex)
            {
                Logger.Error("新增需求信息出错！", ex);
                return 0;
            }
        }

        /// <summary>
        /// 获取需求类型列表
        /// </summary>
        /// <returns></returns>
        public List<RequirementCategory> GetCateList()
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select * from RequirementCategory where ActiveStatus=1;");

            try
            {
                return DapperHelper.Query<RequirementCategory>(strSql.ToString()).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error("获取需求分类列表出错！", ex);
                return null;
            }
        }

        /// <summary>
        /// 添加新的需求分类
        /// </summary>
        /// <param name="cateName"></param>
        /// <returns></returns>
        public int AddNewCategoryItem(string cateName)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("insert into RequirementCategory (CategoryName,ParentCategoryId,ActiveStatus) " +
                          "Values(@CategoryName,0,1);" +
                          "select @@IDENTITY;");

            try
            {
                return DapperHelper.ExecuteScalar<int>(strSql.ToString(), new {CategoryName = cateName});
            }
            catch (Exception ex)
            {
                Logger.Error("添加新的需求分类出错！", ex);
                return 0;
            }
        }

        /// <summary>
        /// 获取需求列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="stDate"></param>
        /// <param name="edDate"></param>
        /// <param name="reqType"></param>
        /// <param name="status"></param>
        /// <param name="content"></param>
        /// <param name="terminal"></param>
        /// <returns></returns>
        public Dictionary<string, object> GetRequireList(int pageIndex, string stDate, string edDate, int reqType, int status, string content, int terminal)
        {
            Dictionary<string, object> dicData = new Dictionary<string, object>()
            {
                {"data",null},
                {"count",null}
            };

            StringBuilder strSql = new StringBuilder();
            string strWhere = "";

            //页数计算
            int bgNumber = ((pageIndex - 1) * 15) + 1;
            int edNumber = (pageIndex) * 15;

            strSql.Append("select * from (");

            strSql.Append("select rm.*,td.dt_Remark,td.dt_Source,ROW_NUMBER() over (order by rm.id desc) rowNumber from RequirementManage rm left join Sys_TaskDaily td on rm.refid=td.id where 1=1 ");

            if (Convert.ToDateTime(edDate) > Convert.ToDateTime(stDate))
            {
                if (stDate != "")
                {
                    DateTime stTime = Convert.ToDateTime(stDate);
                    strWhere += " and rm.CreateTime >='" + stTime.ToString("yyyy-MM-dd") + "' ";
                }
                if (edDate != "")
                {
                    DateTime edTime = Convert.ToDateTime(edDate);
                    strWhere += " and rm.CreateTime <'" + edTime.AddDays(1).Date.ToString("yyyy-MM-dd") + "' ";
                }
            }
            else if ((Convert.ToDateTime(edDate) == Convert.ToDateTime(stDate)) && Convert.ToDateTime(stDate).ToShortDateString() != DateTime.Now.ToShortDateString())
            {
                DateTime time = Convert.ToDateTime(stDate);
                strWhere += " and datediff(day,rm.CreateTime,'" + time.Date.ToString("yyyy-MM-dd") + "')=0";
            }
            
            if (!string.IsNullOrEmpty(content))
            {
                strWhere += " and rm.OriginDesc like '%" + content + "%' ";
            }
            if (reqType != -99)
            {
                strWhere += " and rm.RequirementType=" + reqType;
            }
            if (status != -99)
            {
                strWhere += " and rm.Status=" + status;
            }
            if (terminal != -99)
            {
                strWhere += " and rm.Terminal=" + terminal;
            }
            strSql.Append(strWhere);

            strSql.Append(" ) t ");
            strSql.Append(" where t.rowNumber between @bgNumber and @edNumber; ");

            try
            {
                List<RequirementManage> list = DapperHelper.Query<RequirementManage>(strSql.ToString(), new
                {
                    bgNumber = bgNumber,
                    edNumber = edNumber
                }).ToList();

                foreach (var item in list)
                {
                    if (item.dt_Source == "论坛反馈" && item.dt_Remark.IndexOf('@')==0)
                    {
                        item.dt_Remark = item.dt_Remark.Substring(item.dt_Remark.IndexOf('(') + 1,
                            item.dt_Remark.IndexOf(')') - item.dt_Remark.IndexOf('(') - 1);
                    }
                }

                dicData["data"] = list;
                dicData["count"] = GetPageCount(strWhere);

                return dicData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 返回符合条件的记录条数
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public int GetPageCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select count(*) from RequirementManage rm where 1=1 ");
            strSql.Append(strWhere);

            return DapperHelper.ExecuteScalar<int>(strSql.ToString());
        }

        /// <summary>
        /// 通过类别Id获取需求类别名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetCateNameById(int id)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select CategoryName from RequirementCategory where Id=@id;");

            try
            {
                return DapperHelper.ExecuteScalar<string>(strSql.ToString(), new {id = id});
            }
            catch (Exception ex)
            {
                Logger.Error("获取需求类型名称出错！", ex);
                return "";
            }
        }

        /// <summary>
        /// 变更任务状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public string ChangeTaskStatus(int id, int type, string desc)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("update RequirementManage set Status=" + type + ",Description='" + desc + "' where Id=" + id);

            return DapperHelper.Execute(strSql.ToString()).ToString();
        }


        public string GetDesc(int device, int cate, int module, int val, int diff)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(
                "select Description from RequirementManage where CategoryId=@module and RequirementType=@cate and Terminal=@device and UserVal=@val and Difficult=@diff;");

            List<string> list = DapperHelper.Query<string>(strSql.ToString(), new
            {
                module = module,
                cate = cate,
                device = device,
                val = val,
                diff = diff
            }).ToList();

            if (list != null && list.Count > 0)
            {
                return CommonLib.Helper.JsonSerializeObject(list);
            }
            else
            {
                return "";
            }
        }

    }
}
