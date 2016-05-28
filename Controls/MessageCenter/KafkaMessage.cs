using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Utility;

namespace Controls.MessageCenter
{
    public class KafkaMessage
    {
        public KafkaMessage()
        {

        }

        /// <summary>
        /// 写入队列信息
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="specModel"></param>
        /// <returns></returns>
        public bool SendMsg(int eventId, string specModel)
        {
            var bResult = true;
            var topicName = "msgnotify";

            EventMessage sendModel = new EventMessage();
            sendModel.EventId = eventId;
            sendModel.SpecModel = specModel;

            try
            {
                if (topicName != "")
                {
                    var strJson = CommonLib.Helper.JsonSerializeObject(sendModel);
                    var kafkaControl = new KafkaService();
                    kafkaControl.Send(topicName, strJson);
                }
                else
                {
                    bResult = false;
                    Logger.Warn("KafKa未找到对应Topic错误");
                }
            }
            catch (Exception ex)
            {
                bResult = false;
                Logger.Error("KafKa队列写入错误", ex);
            }

            return bResult;
        }

    }
}
