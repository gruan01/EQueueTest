using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommon.Configurations;
using EQueue.Clients.Producers;
using EQueue.Protocols;

namespace ProducerClient {
    public class Program {

        static void Main(string[] args) {

            ECommon.Configurations.Configuration
                .Create()
                .UseAutofac()
                .RegisterCommonComponents()
                .RegisterUnhandledExceptionHandler()
                .UseProtoBufSerializer()
                .UseLog4Net()
                ;


            var producer = new Producer();
            producer.Start();
            byte[] datas = null;
            var msg = new Message("topic", 1, datas);
            producer.Send(msg, Guid.NewGuid().ToString("n"));
        }

    }
}
