using Common;
using EQueue.Clients.Consumers;
using Handlers;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace ConsumerClient {
    public class Server : ServiceControl {


        private List<Consumer> Consumers = new List<Consumer>();

        [ImportMany]
        public IEnumerable<BaseHandler> Handlers {
            get;
            set;
        }

        [Import]
        public Lazy<ILog> Logger {
            get;
            set;
        }


        public bool Start(HostControl hostControl) {
            if (this.Handlers == null || this.Handlers.Count() == 0) {
                this.Logger.Value.Fatal("Not import any Handlers");
                return false;
            } else {
                this.Subscribe();
                return true;
            }
        }

        public bool Stop(HostControl hostControl) {
            foreach (var consumer in this.Consumers)
                consumer.Shutdown();

            return true;
        }


        private void Subscribe() {

            foreach (var h in this.Handlers) {

                //var set = h.GetType()
                //    .CustomAttributes
                //    .OfType<EQueueSetAttribute>()
                //    .FirstOrDefault();
                var set = h.EQueueSet;

                if (set != null) {
                    var consumer = new Consumer(set.Group)
                                    .SetMessageHandler(h)
                                    .Subscribe(set.Topic);

                    this.Consumers.Add(consumer);
                    consumer.Start();
                }
            }
        }
    }
}
