using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Model;
namespace DAL
{
	/// <summary>
	/// 系统表签
	/// </summary>
	public class Sys_TagInfoDAL : Base.Sys_TagInfoBaseDAL
	{
		/// <summary>
		/// 得到总列表
		/// </summary>
		/// <returns></returns>
		public List<Sys_TagInfoBasic> GetAllList()
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select id,t_Name,t_Color,t_BgColor,t_order from Sys_TagInfo where tagStatus=1 and insertName<>'系统' order by t_order;");
			return DapperHelper.Query<Sys_TagInfoBasic>(strSql.ToString()).ToList();
		}

		/// <summary>
		/// 增加标签
		/// </summary>
		/// <param name="tagName"></param>
		/// <param name="insertName"></param>
		/// <returns></returns>
		public new int Add(string tagName, string insertName)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" declare @tagid int; ");
			strSql.Append(" select @tagid=id from Sys_TagInfo where t_Name=@tageName; ");
			strSql.Append(" if(ISNULL(@tagid,0)=0) ");
			strSql.Append(" begin ");
			strSql.Append(" 	insert into Sys_TagInfo(t_Name,insertName,tagStatus) ");
			strSql.Append(" values(@tageName,@insertName,1); ");
			strSql.Append(" select @@IDENTITY; ");
			strSql.Append(" end ");
			strSql.Append(" else ");
			strSql.Append(" 	select @tagid; ");

			object r = DapperHelper.ExecuteScalar(strSql.ToString(), new
			{
				tageName = tagName,
				insertName = insertName
			});
			if (r != null)
			{
				return Convert.ToInt32(r);
			}
			else
			{
				return 0;
			}
		}

		/// <summary>
		/// 得到标签分类问题
		/// </summary>
		/// <returns></returns>
		public List<SysTagTypeBasic> GetTagQuestions(int accid, int[] excludTypeList)
		{

			string excludType = "";
			if (excludTypeList.Count() > 0)
			{
				foreach (int type in excludTypeList)
				{
					if (excludType.Length > 0)
					{
						excludType += ",";
					}
					excludType += type.ToString();
				}
			}
			

			StringBuilder strSql = new StringBuilder();
			strSql.Append("select tagType tagTypeName,tagTypeid,t_Name,id,t_Color,t_BgColor,t_order from Sys_TagInfo ");

			if (excludType.Length > 0)
			{
				strSql.Append(" where tagTypeid not in(" + excludType + ")  ");
			}
			strSql.Append(" order by tagTypeid,t_order asc,id asc;");

			List<dynamic> list = DapperHelper.Query(strSql.ToString()).ToList();

			List<Sys_TagInfoBasic> tagInfoList = new List<Sys_TagInfoBasic>();

			Dictionary<string, SysTagTypeBasic> TypeList = new Dictionary<string, SysTagTypeBasic>();
			foreach (dynamic item in list)
			{
				string key = item.tagTypeName.ToString();
				if (!TypeList.ContainsKey(key))
				{
					TypeList[key] = new SysTagTypeBasic();
				}

				TypeList[key].tagTypeName = key;
				TypeList[key].tagTypeId = item.tagTypeid.ToString();

				Sys_TagInfoBasic itemBasic = new Sys_TagInfoBasic();
				itemBasic.id = Convert.ToInt32(item.id);
				itemBasic.t_Name = item.t_Name.ToString();
				itemBasic.t_Color = item.t_Color.ToString();
				itemBasic.t_BgColor = item.t_BgColor.ToString();
				itemBasic.t_order = Convert.ToInt32(item.t_order);


				TypeList[key].itemList.Add(itemBasic);

			}

			return TypeList.Values.ToList();
		}

		public Dictionary<string,string> GetTagByCondition(int pageIndex, int tagType, string insertName = "",
			string tagName = "")
		{
			Dictionary<string, string> dict = new Dictionary<string, string>()
			{
				{"list",""},
				{"PageCount",""},
				{"RowCount",""},
			};

			StringBuilder strSql = new StringBuilder();
			string where = " 1=1 ";

			int bgNumber = ((pageIndex - 1) * 15) + 1;
			int edNumber = (pageIndex) * 15;

			if (tagType!=-99)
			{
				where += " and tagTypeid=" + tagType + " ";
			}
			if (!string.IsNullOrEmpty(insertName))
			{
				where += " and insertName like '%" + insertName + "%' ";
			}
			if (!string.IsNullOrEmpty(tagName))
			{
				where += " and t_Name like '%" + tagName + "%' ";
			}

			where += " and tagStatus=1 ";

			strSql.Append("select * from ( select *,row_number() over (order by id) rowNumber from Sys_TagInfo where" +
						  where + ") t where t.rowNumber between @bgNumber and @edNumber;");

			List<Sys_TagInfoBasic> list = DapperHelper.Query<Sys_TagInfoBasic>(strSql.ToString(),
				new {bgNumber = bgNumber, edNumber = edNumber})
				.ToList();

			//dict["list"] =
			//    CommonLib.Helper.JsonSerializeObject(
			//        , "yyyy-mm-dd HH:mm:ss");

			strSql.Clear();

			strSql.Append("select tag_id,count(*) countNum from [sys_i200].[dbo].[Sys_TagNexus] group by tag_id;");
			List<dynamic> tagList = DapperHelper.Query<dynamic>(strSql.ToString()).ToList();

			foreach (Sys_TagInfoBasic item in list)
			{
				try
				{
					item.AccidCount = Convert.ToInt32(tagList.Find(x => x.tag_id == item.id).countNum);
				}
				catch (Exception ex)
				{

					item.AccidCount = 0;
				}
				
			}

			strSql.Clear();
			strSql.Append("select count(*) from Sys_TagInfo where" + where);

			int rowCount = DapperHelper.ExecuteScalar<int>(strSql.ToString());

			dict["list"] = CommonLib.Helper.JsonSerializeObject(list, "yyyy-mm-dd HH:mm:ss");
			dict["RowCount"] = rowCount.ToString();

			dict["PageCount"] = (rowCount%15 == 0 ? rowCount/15 : (rowCount/15 + 1)).ToString();

			return dict;
		}

		public Sys_TagInfoBasic GetSingleModel(int id)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select * from Sys_TagInfo where id=@id;");

			return DapperHelper.GetModel<Sys_TagInfoBasic>(strSql.ToString(), new {id = id});
		}

		public int ModifyModel(int tagId, string tagName, string tagType, string tagTypeName, int tagStatus)
		{
			StringBuilder strSql = new StringBuilder();
			string updateCol = "";


			if (!string.IsNullOrEmpty(tagName))
			{
				updateCol += " t_Name='" + tagName + "' ";
			}
			if (!string.IsNullOrEmpty(tagType))
			{
				
				if (updateCol.Length>1)
				{
					updateCol += " , tagTypeid=" + tagType.ToString() + " ";
				}
				else
				{
					updateCol += " tagTypeid=" + tagType.ToString() + " ";
				}

				updateCol += " , tagType='" + tagTypeName + "' ";
			}

			if (updateCol.Length > 1)
			{
				updateCol += ",tagStatus=" + tagStatus;
			}
			else
			{
				updateCol += " tagStatus=" + tagStatus;
			}

			strSql.Append("update Sys_TagInfo set " + updateCol + " where id=" + tagId);

			return DapperHelper.Execute(strSql.ToString());
		}


		public string TagTransfer()
		{
			StringBuilder strSql = new StringBuilder();

			strSql.Append("select acc_id accid " +
						  "from Sys_TagNexus where tag_id=( select id from Sys_TagInfo where t_Name='朋友介绍'); ");

			List<int> accidList = DapperHelper.Query<int>(strSql.ToString()).ToList();

		    string reStr = "";

			foreach (int accid in accidList)
			{
				strSql.Clear();

				strSql.Append("if exists (select * from P_Sys_UserPortrait where AccId=@accid) ");
				strSql.Append(" begin ");
				strSql.Append(" select Id from P_Sys_UserPortrait where AccId=@accid; ");
				strSql.Append(" end ");
				strSql.Append(" else ");
				strSql.Append(" begin ");
                strSql.Append(" insert into P_Sys_UserPortrait (InsertTime,AccId,industry,UserSourcePortrait,ChoiceReason,PotentialNeed) values (GETDATE(),@accid,',',',',',',',') ");
				strSql.Append(" select @@IDENTITY; ");
				strSql.Append(" end");

				int reVal = DapperHelper.ExecuteScalar<int>(strSql.ToString(), new {accid = accid});

				strSql.Clear();

                strSql.Append("select UserSourcePortrait from P_Sys_UserPortrait where AccId=@accid;");
			    string industry = DapperHelper.ExecuteScalar<string>(strSql.ToString(), new {accid = @accid});

			    if (string.IsNullOrEmpty(industry))
			    {
                    industry = ",64,";
			    }
			    else
			    {
                    if (!industry.Contains(",64,"))
                    {
                        industry += ",64,";
                    }

                    //if (!industry.Contains(",27,"))
                    //{
                    //    industry += ",27,";
                    //}
			    }

                strSql.Clear();

                strSql.Append("update P_Sys_UserPortrait set UserSourcePortrait='" + industry + "' where accid=@accid;");

			    reVal = DapperHelper.Execute(strSql.ToString(), new {accid = accid});

			    if (reVal!=1)
			    {
			        reStr += " " + accid + ":" + reVal + "; ";
			    }
			}

            return reStr;
		}
	}


}
