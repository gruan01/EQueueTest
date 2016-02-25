using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;
using ECommon.Configurations;
using EQueue.Configurations;

namespace Services {
    public class Program {

        static void Main(string[] args) {

            ECommon.Configurations.Configuration
                            .Create()
                            .UseAutofac()
                            .RegisterCommonComponents()
                            .UseLog4Net()
                            .UseJsonNet()
                            .RegisterUnhandledExceptionHandler()
                            .RegisterEQueueComponents()
                            .UseDeleteMessageByCountStrategy(20);

            HostFactory.Run(x => {

                x.StartAutomaticallyDelayed();
                x.RunAsLocalSystem();
                x.SetDescription("EQueue Test Service");
                x.SetDisplayName("EQueue.Test.Service");
                x.SetInstanceName("EQueue.Test.Service");
                x.SetServiceName("EQueue.Test.Service");

                x.Service(s => {
                    var server = new EQueueServer();
                    return server;
                });

            });
        }

    }
}
