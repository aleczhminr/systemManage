using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Hangfire.Logging;
using KafkaNet;
using KafkaNet.Model;
using KafkaNet.Protocol;
using Utility;

namespace Controls.MessageCenter
{
    public class KafkaService
    {
        private static string kafkaHost = ConfigurationManager.ConnectionStrings["Kafka"].ConnectionString;
        private static KafkaOptions options;
        private static BrokerRouter router;
        private static Producer client;

        public KafkaService()
        {
            if (options == null)
            {
                options = new KafkaOptions(new Uri(kafkaHost))
                {
                    Log = new KafkaLog()
                };
            }
            if (router == null)
            {
                router = new BrokerRouter(options);
            }
            if (client == null)
            {
                client = new Producer(router)
                {
                    BatchSize = 100,
                    BatchDelayTime = TimeSpan.FromMilliseconds(2000)
                };
            }
        }

        public void Send(string topicName, string message)
        {
            Task.Run(() =>
            {
                try
                {
                    client.SendMessageAsync(topicName, new[] { new Message(message) }).Wait();
                }
                catch (Exception ex)
                {

                }
            });
        }


        public class KafkaLog : IKafkaLog
        {
            //private static ILog log = Logger.GetILog(MethodBase.GetCurrentMethod().DeclaringType);
            public KafkaLog() { }
            public void DebugFormat(string format, params object[] args)
            {
                //Because there is no global configure, so comment debug
                //log.DebugFormat(format, args);
            }
            public void ErrorFormat(string format, params object[] args)
            {
                Logger.Error(format + args, null);
            }
            public void FatalFormat(string format, params object[] args)
            {
                //log.FatalFormat(format, args);
                Logger.Error(format + args, null);
            }
            public void InfoFormat(string format, params object[] args)
            {
                //log.InfoFormat(format, args);
                Logger.Error(format + args, null);
            }
            public void WarnFormat(string format, params object[] args)
            {
                //log.WarnFormat(format, args);
                Logger.Error(format + args, null);
            }
        }
    }
}
