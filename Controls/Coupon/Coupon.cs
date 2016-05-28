using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using Model;
using System.Text.RegularExpressions;
using System.Collections;
using System.Threading;
namespace Controls.Coupon
{
    /// <summary>
    /// 优惠券
    /// </summary>
    public class Coupon
    {
        /// <summary>
        /// 未使用优惠券Code缓存列表
        /// </summary>
        public static Hashtable HtCouponList = new Hashtable();


        //委托Login处理其他事务
        private delegate void BatchCreateCouponDelegate(int groupId, int iCreateNum);

        /// <summary>
        /// 优惠券列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static Dictionary<string, object> GetList(int pageIndex, int pageSize, string couponDesc = "", DateTime? statTime = null, DateTime? endTime = null, string operatorName = "", string expired = "y")
        {
            Dictionary<string, object> list = new Dictionary<string, object>();
            List<DapperWhere> sqlWhere = new List<DapperWhere>();

            if (couponDesc != "")
            {
                sqlWhere.Add(new DapperWhere("couponDesc", couponDesc, "couponDesc like '%'+ @couponDesc +'%'"));
            }
            if (statTime != null)
            {
                sqlWhere.Add(new DapperWhere("statTime", statTime, " createDate>@statTime "));
            }
            if (endTime != null)
            {
                sqlWhere.Add(new DapperWhere("endTime", endTime, "createDate<@endTime"));
            }

            if (expired=="n")
            {
                sqlWhere.Add(new DapperWhere("endDate", DateTime.Now, "endDate>@endDate"));
            }

            if (operatorName != "")
            {
                if (operatorName == "0")
                {
                    sqlWhere.Add(new DapperWhere("operatorId", 0));
                }
                else if (operatorName == "1")
                {
                    sqlWhere.Add(new DapperWhere("operatorId", 0, " operatorId > @operatorId"));
                }
                else
                {
                    sqlWhere.Add(new DapperWhere("operatorName", operatorName)
                    {
                        Where = " operatorId in (select id from Sys_I200.dbo.Sys_Manage_User where name like '%'+ @operatorName +'%') "
                    });
                }
            }


            if (pageSize < 1)
            {
                pageSize = 20;
            }

            int rowCount = 0;
            if (pageIndex == 1)
            {
                rowCount = BLL.Base.T_Order_CouponInfoBaseBLL.GetCount(sqlWhere);
            }
            int maxPage = 0;
            if (rowCount > 0)
            {
                maxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(rowCount) / pageSize));
            }

            list["rowCount"] = rowCount;
            list["maxPage"] = maxPage;
            list["pageIndex"] = pageIndex;
            list["listData"] = T_Order_CouponInfoBLL.GetList(pageIndex, pageSize, sqlWhere," id desc");

            return list;
        }
        /// <summary>
        /// 优惠券内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static OrderCouponInfo GetModel(int id)
        {
            return T_Order_CouponInfoBLL.GetModel(id);
        }
        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="dapperWheres"></param>
        /// <param name="filedOrder"></param>
        /// <returns></returns>
        public static Dictionary<string, object> GetInfoList(int pageIndex, int pageSize, int groupId, int? couponStatus = null)
        {
            Dictionary<string, object> list = new Dictionary<string, object>();
            List<DapperWhere> dapperWheres = new List<DapperWhere>();
            dapperWheres.Add(new DapperWhere("groupId", groupId));
            if (couponStatus != null)
            {
                dapperWheres.Add(new DapperWhere("couponStatus", couponStatus));
            } 


            if (pageSize < 1)
            {
                pageSize = 20;
            }

            int rowCount = 0;
            if (pageIndex == 1)
            {
                rowCount = BLL.Base.T_Order_CouponListBaseBLL.GetCount(dapperWheres);
            }
            int maxPage = 0;
            if (rowCount > 0)
            {
                maxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(rowCount) / pageSize));
            }

            list["rowCount"] = rowCount;
            list["maxPage"] = maxPage;
            list["pageIndex"] = pageIndex;
            list["listData"] = T_Order_CouponListBLL.GetList(pageIndex, pageSize, dapperWheres, " id desc");

            return list;
        }




        /// <summary>
        /// 批量生成优惠券，委托
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="iCreateNum"></param>
        public void BatchCreateCouponGroupDelegate(int groupId, int iCreateNum)
        {
            var CouponDelegate = new BatchCreateCouponDelegate(BatchCreateCouponGroup);
            CouponDelegate.BeginInvoke(groupId, iCreateNum, null, null);
        }
        /// <summary>
        /// 批量生成优惠券 无返回值 
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="iCreateNum"></param>
        public void BatchCreateCouponGroup(int groupId, int iCreateNum)
        {
            CreateCouponGroup(groupId, iCreateNum);
        }
        /// <summary>
        /// 批量生成优惠码
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="createNum"></param>
        /// <returns></returns>
        public bool CreateCouponGroup(int groupId, int iCreateNum=1)
        {
            int iCount = iCreateNum;

            if (iCreateNum > 1000)
            {
                return false;
            }

            //获得需要生成概要信息
            OrderCouponInfo couponInfo = T_Order_CouponInfoBLL.GetModel(groupId);
            //获得当前总生成条数
            int couponListCount = couponInfo.produceNum;

            //新生成优惠码
            List<string> couponCreateList = new List<string>();

            //判断是否超出限制数量
            if ((iCreateNum + couponListCount) > couponInfo.maxLimitNum)
            {
                iCount = couponInfo.maxLimitNum - couponListCount;
            }

            string couponCode = "";
            string couponPre = "";

            //前缀命名
            if (couponInfo.couponType == 1)
            {
                couponPre = "DJ";
            }
            else if (couponInfo.couponType == 2)
            {
                couponPre = "GN";
            }

            while (iCount > 0)
            {
                //循环生成优惠券Code
                couponCode = couponPre + CreateNumber();
                //Utility.Logger.Info(couponCode);
                if (!CheckCouponCode(couponCode))
                {

                    if (!(T_Order_CouponListBLL.CreateCouponGroup(couponInfo.id, couponCode, couponInfo.couponValue, couponInfo.endDate) > 0))
                    {
                        return false;
                    } 
                    UpdateHtCouponList(0, couponCode);
                    iCount--;
                    if (iCount > 0)
                    {
                        Thread.Sleep(100);
                    }
                }
            }
            return true;

        }


        /// <summary>
        /// 生成随机优惠编码
        /// </summary>
        /// <returns></returns>
        public string CreateNumber()
        {
            var strCode = string.Empty;
            Regex regex = new Regex(@"[012478BIOQUVZJNY]");
            do
            {
                strCode = CommonLib.Helper.CreateUUID();
            } while (regex.IsMatch(strCode));

            return strCode;
        }
        /// <summary>
        /// 判断coupon是否存在
        /// </summary>
        /// <param name="couponCode">礼金券code</param>
        /// <returns></returns>
        public bool CheckCouponCode(string couponCode)
        {
            //初始化缓存Coupon列表
            if (HtCouponList == null || HtCouponList.Count == 0)
            { 
                Hashtable htCoupon = GetAllCoupon();
                if (HtCouponList == null)
                {
                    HtCouponList = htCoupon;
                }
                else
                {
                    lock (HtCouponList)
                    {
                        HtCouponList = htCoupon;
                    }
                }
            }

            //判断是否存在
            bool bResult = false;
            if (HtCouponList != null)
            {
                if (HtCouponList.ContainsKey(couponCode))
                {
                    bResult = true;
                }
            }
            // Utility.Logger.Info(Newtonsoft.Json.JsonConvert.SerializeObject(HtCouponList));
            return bResult;
        }

        /// <summary>
        /// 更新缓存的优惠券列表
        /// </summary>
        /// <param name="accId">店铺id</param>
        /// <param name="couponCode">优惠券code</param>
        public void UpdateHtCouponList(int accId, string couponCode)
        {
            if (HtCouponList != null)
            {
                lock (HtCouponList)
                {
                    HtCouponList.Add(couponCode, accId);
                }
            }

        }

        public Hashtable GetAllCoupon()
        {
            Hashtable htCoupon = new Hashtable();
            List<dynamic> list = T_Order_CouponListBLL.GetAllCoupon();
            foreach (dynamic row in list)
            {
                if (row.couponId != null)
                {
                    htCoupon.Add(row.couponId.ToString().Trim(), int.Parse(row.id.ToString().Trim()));
                }
            }
            return htCoupon;
        }
    }
}
