using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Model;
using DAL;
using Utility;

namespace BLL
{
    /// <summary>
    /// 回访信息
    /// </summary>
   public static  class Sys_VisitInfoBLL
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
       public static int Add(Sys_VisitInfo model)
       {
           Sys_VisitInfoDAL dal = new Sys_VisitInfoDAL();
           return dal.Add(model);
       }

        /// <summary>
        /// 得到列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="dapperWheres"></param>
        /// <param name="filedOrder"></param>
        /// <returns></returns>
       public static List<Sys_VisitInfoBase> GetList(int pageIndex, int pageSize, List<DapperWhere> dapperWheres, string filedOrder)
       {
           Sys_VisitInfoDAL dal = new Sys_VisitInfoDAL();
           var list= dal.GetList(pageIndex, pageSize, dapperWheres, filedOrder);

           try
           {
               foreach (Sys_VisitInfoBase item in list)
               {
                   string prob = "";
                   if (string.IsNullOrEmpty(item.t_mk))
                   {
                       prob = "反馈问题：未说明";       
                   }
                   else
                   {
                       prob = "反馈问题：" + item.t_mk;    
                   }                                          

                   string feedBack = (item.vi_Content.IndexOf('｛') > 0 && item.vi_Content.Length > 0)
                       ? ("客服回复：" + item.vi_Content.Substring(item.vi_Content.IndexOf('｝') + 1,
                           item.vi_Content.Length - item.vi_Content.IndexOf('｝') - 1))
                       : ("客服回复：" + item.vi_Content);

                   string reg = @"[<].*?[>]";
                   //source = Regex.Replace(source, reg, "");

                   item.FeedBack = Regex.Replace(feedBack, reg, ""); ;
                   item.Problem = prob;

                   #region abandon code
                   //string temp = item.vi_Content.Substring(item.vi_Content.IndexOf('：') + 1,
                   //    item.vi_Content.Length - item.vi_Content.IndexOf('：') - 1);

                   /////^[\u0391-\uFFE5]+$/ 

                   //if (temp.LastIndexOf('>') > 0 && temp.LastIndexOf('>') < temp.Length)
                   //{
                   //    string fb = "回复：";
                   //    item.FeedBack = temp.Substring(temp.LastIndexOf('｝') + 1,
                   //                        temp.LastIndexOf('/') - temp.LastIndexOf('｝') - 2);
                   //    if (item.FeedBack.Length>0)
                   //    {
                   //        item.FeedBack = fb + item.FeedBack;
                   //    }
                   //    else
                   //    {
                   //        item.FeedBack = fb + temp.Substring(temp.LastIndexOf('>') + 1,
                   //                        temp.Length - temp.LastIndexOf('>') - 1);

                   //        if (item.FeedBack.Length <= fb.Length)
                   //        {
                   //            item.FeedBack = fb + temp.Substring(temp.LastIndexOf('｝') + 1,
                   //                        temp.Length - temp.LastIndexOf('｝') - 2);
                   //        }
                   //    }

                   //    if (item.FeedBack.IndexOf('>')>0)
                   //    {
                   //        item.FeedBack = fb + item.FeedBack.Substring(item.FeedBack.IndexOf('>') + 1,
                   //            item.FeedBack.Length - item.FeedBack.IndexOf('>')-1);      
                   //    }
                   //}
                   //else
                   //{
                   //    item.FeedBack = item.vi_Content;
                   //}
                   //if (temp.LastIndexOf('【') > 0)
                   //{
                   //    item.Problem = "反馈问题：" +
                   //                  temp.Substring(temp.IndexOf('>') + 1,
                   //                      temp.LastIndexOf('】') - temp.IndexOf('>'));
                   //}
                   #endregion
               }
           }
           catch (Exception ex)
           {
               Logger.Error("visitList", ex);
           }

           

           foreach (Sys_VisitInfoBase item in list)
           {
               item.recordType = Enum.GetName(typeof(Model.Enum.VisitInfoEnum.RecordType), item.rt_Maxid);
               item.visitManner = Enum.GetName(typeof(Model.Enum.VisitInfoEnum.VisitManner), item.vm_id);
               item.handleStatName = Enum.GetName(typeof(Model.Enum.VisitInfoEnum.HandleStat), item.handleStat);

           }

           return list;
       }

        /// <summary>
        /// 得到一个详情的信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
       public static SysVisitParticularModel GetParticularModel(int id)
       {
           Sys_VisitInfoDAL dal=new Sys_VisitInfoDAL();
           var model = dal.GetParticularModel(id);
           if (model != null)
           {
               model.recordType = Enum.GetName(typeof(Model.Enum.VisitInfoEnum.RecordType), model.rt_Maxid);
               model.visitManner = Enum.GetName(typeof(Model.Enum.VisitInfoEnum.VisitManner), model.vm_id);
               model.handleStatName = Enum.GetName(typeof(Model.Enum.VisitInfoEnum.HandleStat), model.handleStat);

               model.CompanyName = T_AccountBLL.GetCompanyName(model.accid);

               model.tagList = Sys_VisitTagNexusBLL.GetVisitInfoTagList(model.id);

               model.replyList = Sys_VisitReplyBLL.GetList(model.id);

               #region 得到事件概要
               List<int> vrids = new List<int>(); ;
               if (model.vr_Maxid > 0)
               {
                   vrids.Add(model.vr_Maxid);
               }
               if (model.vr_Minid > 0)
               {
                   vrids.Add(model.vr_Minid);
               }
               if (model.vr_Threeid > 0)
               {
                   vrids.Add(model.vr_Threeid);
               }

               if (vrids.Count() > 0)
               {
                   List<Sys_VisitReason> vrList = Sys_VisitReasonBLL.GetList(vrids.ToArray());

                   if (vrList != null)
                   {
                       foreach (Sys_VisitReason vrItem in vrList)
                       {
                           if (vrItem.id == model.vr_Maxid)
                           {
                               model.visitReasonOne = vrItem.vr_name;
                           }
                           else if (vrItem.id == model.vr_Minid)
                           {
                               model.visitReasonTwo = vrItem.vr_name;
                           }
                           else if (vrItem.id == model.vr_Threeid)
                           {
                               model.visitReasonThree = vrItem.vr_name;
                           }
                       }
                   }

               }
               #endregion


           }
           return model;
       }
        /// <summary>
        /// 得到单个店铺需要处理回访
        /// </summary>
        /// <param name="accid"></param>
        /// <returns></returns>
       public static List<SysNeedVisitModel> GetNeedVisitList(int accid)
       {
           Sys_VisitInfoDAL dal = new Sys_VisitInfoDAL();
           var list = dal.GetNeedVisitList(accid);

           foreach (SysNeedVisitModel item in list)
           { 
               item.handleStatName = Enum.GetName(typeof(Model.Enum.VisitInfoEnum.HandleStat), item.handleStat);

           }
           return list;
       }
        /// <summary>
        /// 更新回访状态
        /// </summary>
        /// <param name="vid"></param>
        /// <param name="handleStat"></param>
        /// <returns></returns>
       public static bool UpdateVisitStat(int vid, int handleStat)
       {
           Sys_VisitInfoDAL dal = new Sys_VisitInfoDAL();
           return dal.UpdateVisitStat(vid, handleStat);
       }
        /// <summary>
        /// 得到事件列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="dapperWhere"></param>
        /// <param name="filedOrder"></param>
        /// <returns></returns>
       public static List<SysCaseModel> GetCaseList(int pageIndex, int pageSize, List<DapperWhere> dapperWhere, string filedOrder)
       {
           Sys_VisitInfoDAL dal = new Sys_VisitInfoDAL();
           var list= dal.GetCaseList(pageIndex, pageSize, dapperWhere, filedOrder);


           foreach (SysCaseModel item in list)
           {
               item.handleStatName = Enum.GetName(typeof(Model.Enum.VisitInfoEnum.HandleStat), item.handleStat);

           }

           return list;
       }

        public static VisitPeriodModel GetVisitPeriod(int accid)
        {
            Sys_VisitInfoDAL dal = new Sys_VisitInfoDAL();
            VisitPeriodModel model = new VisitPeriodModel();

            try
            {
                dynamic data = dal.GetLastVisit(accid);

                model.Accid = data.accid;
                model.LastVisitTime = data.insertTime;
                model.Period = data.diff;
                model.VisitWay = Enum.GetName(typeof(Model.Enum.VisitInfoEnum.VisitManner), data.vm_id);

                return model;
            }
            catch (Exception ex)
            {

                return null;
            }
         
        }

    }
}
