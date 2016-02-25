using Common;
using Common.Models;
using ECommon.Logging;
using EQueue.Clients.Consumers;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handlers {


    [Export(typeof(BaseHandler))]
    public class Handler2 : BaseHandler<TextMessage> {


        public override Task<bool> Handle(TextMessage data, EQueue.Protocols.QueueMessage message) {
            this.Logger.Value.Info(data);
            return Task.FromResult(true);
        }
    }
}
