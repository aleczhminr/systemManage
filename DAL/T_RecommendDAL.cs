using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DAL
{
    public class T_RecommendDAL
    {
        public RecommendModel GetRecList(int page, int type, DateTime date, int accId)
        {
            RecommendModel reModel = new RecommendModel();
            StringBuilder strSql = new StringBuilder();
            StringBuilder whereSql = new StringBuilder();

            int bgNumber = ((page - 1) * 15) + 1;
            int edNumber = (page) * 15;

            if (accId != -1)
            {
                //店铺Id
                whereSql.Append(" and (accId=@accId or recommendAccId=@accId)");
            }

            if (type != -1)
            {
                switch (type)
                {
                    case 2:
                        //推荐成功
                        whereSql.Append(" and T_Recommend_Log.reStatus=1");
                        break;
                    case 1:
                        //推荐失败
                        whereSql.Append(" and T_Recommend_Log.reStatus=2");
                        break;
                }
            }

            if (date != DateTime.MinValue)
            {
                whereSql.Append(" and DATEDIFF(day,createTime,@creatDate)=0");
            }

            //只显示被推荐数据
            whereSql.Append(" and T_Recommend_Log.type=2");

            strSql.Append(" select count(id) as rowCnt from i200.dbo.T_Recommend_Log");
            strSql.Append(" where 1=1");
            strSql.Append(whereSql.ToString());

            reModel.RowCount = DapperHelper.ExecuteScalar<int>(strSql.ToString(), new { accId = accId, creatDate = date });
            reModel.maxPage = (reModel.RowCount % 15 == 0) ? reModel.RowCount / 15 : (reModel.RowCount / 15 + 1);

            strSql.Clear();

            strSql.Append(" SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (order by i200.dbo.T_Recommend_Log.id desc");
            strSql.Append(")AS Row, i200.dbo.T_Recommend_Log.id, createTime, accId, i200.dbo.T_Recommend_Log.type, recommendName, recommendValue, recommendAccId, reStatus, T_A.CompanyName as AccName, T_B.CompanyName as ReAccName from i200.dbo.T_Recommend_Log ");
            strSql.Append(" left outer join i200.dbo.T_Account T_A on T_A.ID=i200.dbo.T_Recommend_Log.accId");
            strSql.Append(" left outer join i200.dbo.T_Account T_B on T_B.ID=i200.dbo.T_Recommend_Log.recommendAccId");
            strSql.Append(" where 1=1");
            strSql.Append(whereSql.ToString());
            strSql.Append(" ) T where T.Row between @bgNumber and @edNumber;");

            reModel.Data =
                DapperHelper.Query<RecommendItem>(strSql.ToString(),
                    new { accId = accId, creatDate = date, bgNumber = bgNumber, edNumber = edNumber }).ToList();

            List<int> accIdList = new List<int>();
            string accids = "(";

            foreach (RecommendItem item in reModel.Data)
            {
                if (!accIdList.Contains(item.RecommendAccId))
                {
                    accIdList.Add(item.RecommendAccId);
                    accids += item.RecommendAccId.ToString() + ",";
                }
            }

            if (accids.Length>1)
            {
                accids = accids.Substring(0, accids.LastIndexOf(',')) + ")";

                StringBuilder strEx = new StringBuilder();
                strEx.Append(
                    "select count(recommendAccId) cnt,recommendAccId from i200.dbo.T_Recommend_Log where recommendAccId in " +
                    accids + " group by recommendAccId");

                List<dynamic> countList = DapperHelper.Query<dynamic>(strEx.ToString()).ToList();

                foreach (dynamic item in countList)
                {
                    //if (reModel.Data.Exists(x => x.RecommendAccId == item.recommendAccId))
                    //{
                    List<RecommendItem> list=reModel.Data.FindAll(x => x.RecommendAccId == item.recommendAccId);
                    foreach (RecommendItem findItem in list)
                    {
                        findItem.ReTimes = item.cnt;
                    }
                    //}
                }
            }

            
            return reModel;
        }
    }
}
