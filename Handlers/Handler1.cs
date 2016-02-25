using Common;
using Common.Models;
using ECommon.Logging;
using EQueue.Clients.Consumers;
using EQueue.Protocols;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Handlers {

    [Export(typeof(IMessageHandler))]
    [EQueueSet(Const.Group, Const.TOPIC_1)]
    public class Handler1 : BaseHandler<UrlRequest> {

        [Import]
        public ILog Logger {
            get;
            set;
        }

        public async override Task<bool> Handle(UrlRequest data, QueueMessage message) {
            using (var client = new WebClient()) {
                var ctx = await client.DownloadStringTaskAsync(data.RequestUri);
            }

            return true;
        }
    }
}
