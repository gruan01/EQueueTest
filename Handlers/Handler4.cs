using EQueue.Clients.Consumers;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handlers {

    [Export(typeof(IMessageHandler))]
    public class Handler4 : IMessageHandler {
        public void Handle(EQueue.Protocols.QueueMessage message, IMessageContext context) {
            throw new NotImplementedException();
        }
    }
}
