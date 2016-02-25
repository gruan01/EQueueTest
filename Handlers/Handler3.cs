using Common;
using Common.Models;
using ECommon.Logging;
using EQueue.Clients.Consumers;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handlers {


    [Export(typeof(IMessageHandler))]
    [EQueueSet(Const.Group, Const.TOPIC_3)]
    public class Handler3 : BaseHandler<ExecCmd> {

        [Import]
        public ILog Logger {
            get;
            set;
        }


        public override Task<bool> Handle(ExecCmd data, EQueue.Protocols.QueueMessage message) {
            try {
                Process.Start(data.CmdString);
            } catch {

            }

            return Task.FromResult(true);
        }
    }
}
