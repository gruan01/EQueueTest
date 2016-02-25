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


    [Export(typeof(IMessageHandler))]
    [EQueueSet(Const.Group, Const.TOPIC_2)]
    public class Handler2 : BaseHandler<TextMessage> {


        [Import]
        public ILog Logger {
            get;
            set;
        }

        public override Task<bool> Handle(EQueue.Protocols.QueueMessage message) {
            var data = this.ParseData(message.Body);
            this.Logger.Info(data);
            return Task.FromResult(true);
        }
    }
}
