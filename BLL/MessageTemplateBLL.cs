using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;

namespace BLL
{
    public static class MessageTemplateBLL
    {
        public static string AddTemplate(TriggerTemplateModel model)
        {
            MessageTemplateDAL dal = new MessageTemplateDAL();
            return dal.AddTemplate(model);
        }

        public static TriggerTemplateModel GetModelById(int id)
        {
            MessageTemplateDAL dal = new MessageTemplateDAL();
            return dal.GeTemplateModel(id);
        }

        public static List<Sys_KafkaEvent> GetAllKafkaEvent()
        {
            MessageTemplateDAL dal = new MessageTemplateDAL();
            return dal.GetAllKafkaEvent();
        }

        public static ListView GetTemplateList(int pageIndex, string[] sendMethod, string target, string name)
        {
            MessageTemplateDAL dal = new MessageTemplateDAL();
            return dal.GetTemplateList(pageIndex, sendMethod, target, name);
        }
    }
}
