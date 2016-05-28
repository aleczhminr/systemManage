using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class T_AlipayInfoDAL
    {               

        #region  GetPageCount  获取总行数
        /// <summary>
        /// 获取总行数
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public int GetPageCount(string strWhere)
        {
            int count = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) from T_AlipayInfo");

            if (strWhere.Length > 0)
            {
                strSql.Append(" where ");
            }
            strSql.Append(strWhere);
            try
            {
                count = HelperForFrontend.Query<int>(strSql.ToString()).ToList()[0];
            }
            catch (Exception ex)
            {
                count = 0;
            }
            return count;
        }
        #endregion

        #region GetPage  获取分页数据
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="Column"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public List<AlipayInfoModel> GetPage(int pageIndex, int pageSize, string Column, string strWhere)
        {
            List<AlipayInfoModel> listitem = new List<AlipayInfoModel>();
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            pageSize = pageSize < 1 ? 20 : pageSize;
            //页数计算
            int bgNumber = ((pageIndex - 1) * pageSize) + 1;
            int edNumber = (pageIndex) * pageSize;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from (");
            strSql.Append("select ROW_NUMBER() over (order by id desc) as rowNumber ");
            if (Column.Length == 0)
            {
                Column = "[id],[accId],dbo.GetAccountName(accId) accountName,[status],[companyType],[createTime],[modifiedTime],[completeTime],[artificialPerson],[phone],[alipayAccount],[companyName],[industryClassification],[businessArea],[shopOutPhotoURL],[shopInPhotoURL],[businessLicensePhotoURL],[doorPhotoURL],[firstShopWorkPhotoURL],[secondShopWorkPhotoURL],[thirdShopWorkPhotoURL],[companyAddress],[PID],[Key],[remark]";
            }
            strSql.Append(" ," + Column + " ");
            strSql.Append(" from T_AlipayInfo  ");
            if (strWhere.Length > 0)
            {
                strSql.Append(" where ");
            }
            strSql.Append(strWhere);
            strSql.Append(" ) t ");
            strSql.Append(" where t.rowNumber between @bgNumber and @edNumber; ");
            try
            {
                listitem = HelperForFrontend.Query<AlipayInfoModel>(strSql.ToString(), new
                {
                    bgNumber = bgNumber,
                    edNumber = edNumber
                }).ToList();
            }
            catch (Exception ex)
            {
                listitem = null;
            }
            return listitem;

        }

        #endregion

        #region   GetAlipayInfoModel 获取一个model
        public AlipayInfoModel GetAlipayInfoModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select top 1 [id],[accId],dbo.GetAccountName(accId) accountName,[status],[companyType],[createTime],[modifiedTime],[completeTime],[artificialPerson],[phone],[alipayAccount],[companyName],[industryClassification],[businessArea],[shopOutPhotoURL],[shopInPhotoURL],[businessLicensePhotoURL],[doorPhotoURL],[firstShopWorkPhotoURL],[secondShopWorkPhotoURL],[thirdShopWorkPhotoURL],[companyAddress],[PID],[Key],[remark] ");
            strSql.Append(" from T_AlipayInfo ");
            strSql.Append(" where id=@id");
            return HelperForFrontend.GetModel<AlipayInfoModel>(strSql.ToString(), new { id = id });
        }
        #endregion

        #region  UpdateStatusForSuccessStep 更新正确的步骤状态

        public string UpdateStatusForSuccessStep(int status, int id)
        {
            string sResult = string.Empty;
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update T_AlipayInfo set status = @status where id=@id ");
            try
            {
                sResult = HelperForFrontend.Execute(strSql.ToString(), new
                {
                    id = @id,
                    status = @status
                }).ToString();
            }
            catch (Exception ex)
            {     
                sResult = "error";
            }
            return sResult;
        }
        #endregion

        #region     UpdateStatusForFailedStep 更新错误的步骤状态
        public string UpdateStatusForFailedStep(int status, int id, string remark)
        {
            string sResult = string.Empty;
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update T_AlipayInfo set status = @status, remark=@remark where id=@id ");
            try
            {
                sResult = HelperForFrontend.Execute(strSql.ToString(), new
                {
                    id = @id,
                    status = @status,
                    remark=@remark
                }).ToString();
            }
            catch (Exception ex)
            {
                sResult = "error";
            }
            return sResult;
        }

        #endregion

                    
    }
}
