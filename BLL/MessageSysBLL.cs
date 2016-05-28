using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;

namespace BLL
{
    public static class MessageSysBLL
    {
        public static string AddMessageBatch(MessageBatch model)
        {
            MessageSysDAL dal = new MessageSysDAL();
            int batchMark = dal.AddMessageBatch(model);
            string msg = "";

            if (batchMark != 0)
            {
                //string reMsg = AddMessageDetailFromBatch(model);
                msg += "消息提交成功！";
                //为该批次消息添加发送详情队列

                return msg + AddMessageDetailFromBatch(model);    
            }
            else
            {
                return "发送渠道ID为" + model.ChannelId + "的消息批次" + model.BatchId + "添加失败";
            }
        }

        public static string AddMessageDetailFromBatch(MessageBatch model)
        {
            MessageSysDAL dal = new MessageSysDAL();
            string reMsg = "";

            List<int> accIdList = CommonLib.Helper.JsonDeserializeObject<List<int>>(model.AccIdSet);

            foreach (var accid in accIdList)
            {
                MessageDetail tempModel = new MessageDetail();
                tempModel.AccId = accid;
                tempModel.BatchId = model.BatchId;
                tempModel.ChannelId = model.ChannelId;
                tempModel.Content = model.Content;
                tempModel.CreateTime=DateTime.Now;
                tempModel.Remark = model.Remark;
                tempModel.Title = model.Title;
                tempModel.ArriveMark = 0;
                tempModel.OpenMark = 0;
                tempModel.ContentType = model.ContentType;
                tempModel.ContentUrl = model.ContentUrl;

                tempModel.AccIdNumber = T_AccountBLL.GetAccountNumber(model.ChannelId, accid);
                string tMsg = AddMessageDetail(tempModel);
                if (tMsg!="成功")
                {
                    reMsg += tMsg;
                }
            }

            return reMsg;
        }

        //public static string AddMessageDetailFromSmsBatch(MessageBatch model)
        //{
        //    MessageSysDAL dal = new MessageSysDAL();
        //    string reMsg = "";

        //    List<int> accIdList = CommonLib.Helper.JsonDeserializeObject<List<int>>(model.AccIdSet);

        //    foreach (var accid in accIdList)
        //    {
        //        MessageDetail tempModel = new MessageDetail();
        //        tempModel.AccId = accid;
        //        tempModel.BatchId = model.BatchId;
        //        tempModel.ChannelId = model.ChannelId;
        //        tempModel.Content = model.Content;
        //        tempModel.CreateTime = DateTime.Now;
        //        tempModel.Remark = model.Remark;
        //        tempModel.Title = model.Title;
        //        tempModel.ArriveMark = 0;
        //        tempModel.OpenMark = 0;

        //        string tMsg = AddMessageDetail(tempModel);
        //        if (tMsg != "成功")
        //        {
        //            reMsg += tMsg;
        //        }
        //    }

        //    return reMsg;
        //}

        public static string AddMessageDetail(MessageDetail model)
        {
            MessageSysDAL dal = new MessageSysDAL();
            if (dal.AddMessageDetail(model) == 1)
            {
                return "成功";
            }
            else
            {
                return model.AccId + "详情添加失败！";
            }
        }

        public static Dictionary<string,object> GetBatchList(int pageIndex, string stDate, string edDate, string batchId,
            string remark, string content, int sourceType, int channel)
        {
            MessageSysDAL dal = new MessageSysDAL();
            return dal.GetBatchList(pageIndex, stDate, edDate, batchId, remark, content, sourceType, channel);
        }

        public static Dictionary<string, object> GetBatchSummaryInfo(int pageIndex, string stDate, string edDate, string batchId,
            string remark, int sourceType, int channel)
        {
            MessageSysDAL dal = new MessageSysDAL();
            return dal.GetBatchSummaryInfo(pageIndex, stDate, edDate, batchId, remark, sourceType, channel);
        }

        public static Dictionary<string, object> GetBatchDetail(int pageIndex, string batchId, int channelId, int arrived = 0)
        {
            MessageSysDAL dal = new MessageSysDAL();
            return dal.GetBatchDetail(pageIndex, batchId, channelId,arrived);
        }

        public static int AddMsgSendingInfo(MessageSending model)
        {
            MessageSysDAL dal = new MessageSysDAL();
            return dal.AddMsgSendingInfo(model);
        }

        /// <summary>
        /// 获取批次发送详情信息
        /// </summary>
        /// <param name="batchId"></param>
        /// <param name="channelId"></param>
        /// <returns></returns>
        public static List<MessageDetail> GetBatchForSend(string batchId, int channelId)
        {
            MessageSysDAL dal = new MessageSysDAL();
            return dal.GetBatchForSend(batchId, channelId);
        }

        /// <summary>
        /// 更新批次表的发送数和发送表关联ID
        /// </summary>
        /// <param name="batchId"></param>
        /// <param name="channelId"></param>
        /// <param name="count"></param>
        /// <param name="sendId"></param>
        /// <returns></returns>
        public static int UpdateAllowCount(string batchId, int channelId, int count, string sendId)
        {
            MessageSysDAL dal = new MessageSysDAL();
            return dal.UpdateAllowCount(batchId, channelId, count, sendId);
        }

        /// <summary>
        /// 更新发送详细表的无号码标记
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int UpdateDetailUnableMark(int id)
        {
            MessageSysDAL dal = new MessageSysDAL();
            return dal.UpdateDetailUnableMark(id);
        }

        public static int GetBatchCount(string batchId,int channelId)
        {
            MessageSysDAL dal = new MessageSysDAL();
            return dal.GetBatchCount(batchId, channelId);
        }

        public static MessageBatch GetSingleBatchInfo(string batchId, int channelId)
        {
            MessageSysDAL dal = new MessageSysDAL();
            return dal.GetSingleBatchInfo(batchId, channelId);
        }

        /// <summary>
        /// 更新批次详情表发送状态
        /// </summary>
        /// <param name="batchId"></param>
        /// <param name="channelId"></param>
        /// <param name="statusModel"></param>
        /// <returns></returns>
        public static int UpdateDetailSendingStatus(string batchId, int channelId, SendStatus statusModel)
        {
            MessageSysDAL dal = new MessageSysDAL();
            return dal.UpdateDetailSendingStatus(batchId, channelId, statusModel);
        }

        /// <summary>
        /// 更新批次总表发送状态
        /// </summary>
        /// <param name="batchId"></param>
        /// <param name="channelId"></param>
        /// <returns></returns>
        public static int UpdateBatchSendingStatus(string batchId, int channelId)
        {
            MessageSysDAL dal = new MessageSysDAL();
            return dal.UpdateBatchSendingStatus(batchId, channelId);
        }

        public static int UpdateOperator(int operId, string batchId, int channelId)
        {
            MessageSysDAL dal = new MessageSysDAL();
            return dal.UpdateOperator(operId, batchId, channelId);
        }

        /// <summary>
        /// 根据批次id获取该批次的所有发送渠道
        /// </summary>
        /// <param name="batchId"></param>
        /// <returns></returns>
        public static List<int> GetBatchChannel(string batchId)
        {
            MessageSysDAL dal = new MessageSysDAL();
            return dal.GetBatchChannel(batchId);
        }

        public static List<RecurringUpdateModel> GetUpdateModel()
        {
            MessageSysDAL dal = new MessageSysDAL();
            return dal.GetUpdateModel();
        }

        public static List<string> GetPushUserList(int id)
        {
            MessageSysDAL dal = new MessageSysDAL();
            return dal.GetPushUserList(id);
        }

        /// <summary>
        /// 获取需要单个处理的消息列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<SingleShopInfoForMsg> GetSingleUserMsgBatch(int id)
        {
            MessageSysDAL dal = new MessageSysDAL();
            return dal.GetSingleUserMsg(id);
        }
    }
}
