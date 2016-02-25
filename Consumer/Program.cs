using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommon.Configurations;
using EQueue.Clients.Consumers;
using System.Reflection;
using Topshelf;
using ECommon.Components;
using ECommon.Serializing;
using log4net.Core;
using log4net;
using ECommon.ProtocolBuf;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using EQueue.Configurations;

namespace ConsumerClient {
    public class Program {

        static void Main(string[] args) {

            var logger = log4net.LogManager.GetLogger(typeof(Program));

            //Must
            ECommon.Configurations.Configuration
                .Create()
                .UseAutofac()//Must
                .RegisterCommonComponents()
                .UseLog4Net()
                //.UseJsonNet()
                .RegisterUnhandledExceptionHandler()
                .RegisterEQueueComponents();


            HostFactory.Run(x => {

                x.StartAutomaticallyDelayed();
                x.RunAsLocalSystem();
                x.SetDescription("EQueue Consumer Client Service");
                x.SetDisplayName("EQueue.Consumer.Client.Service");
                x.SetInstanceName("EQueue.Consumer.Client.Service");
                x.SetServiceName("EQueue.Consumer.Client.Service");


                x.Service(s => {

                    var server = new Server();

                    try {
                        //var catalog = MefHelper.SafeDirectoryCatalog(AppDomain.CurrentDomain.BaseDirectory);
                        var catalog = new DirectoryCatalog(AppDomain.CurrentDomain.BaseDirectory);
                        var container = new CompositionContainer(catalog);

                        var batch = new CompositionBatch();
                        batch.AddExportedValue<ILog>(logger);
                        batch.AddExportedValue<IBinarySerializer>(new ProtocolBufSerializer());

                        container.Compose(batch);
                        container.ComposeParts(server);
                    } catch (Exception ex) {
                        logger.Fatal(ex.Message, ex);
                    }

                    return server;
                });

            });
        }

    }
}
