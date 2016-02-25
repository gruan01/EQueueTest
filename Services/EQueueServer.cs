using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;
using ECommon.Configurations;
using EQueue.Broker;
using log4net.Core;
using ECommon.Components;

namespace Services {
    public class EQueueServer : ServiceControl {

        private BrokerController Controller = null;

        public bool Start(HostControl hostControl) {
            try {
                this.Controller = this.CreateBroker();
                this.Controller.Start();
                return true;
            } catch {
                return false;
            }
        }

        public bool Stop(HostControl hostControl) {
            if (this.Controller != null)
                this.Controller.Shutdown();
            return true;
        }

        private BrokerController CreateBroker() {
            return BrokerController.Create(new BrokerSetting() {

                AutoCreateTopic = false

            });
        }
    }
}
