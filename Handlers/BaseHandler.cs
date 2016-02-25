using ECommon.Logging;
using ECommon.Serializing;
using EQueue.Clients.Consumers;
using EQueue.Protocols;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handlers {
    public abstract class BaseHandler<T> : IMessageHandler where T : class {

        [Import]
        private Lazy<ILogger> Logger {
            get;
            set;
        }

        [Import]
        private Lazy<IBinarySerializer> Serializer {
            get;
            set;
        }

        public virtual bool Retry {
            get {
                return true;
            }
        }

        public abstract Task<bool> Handle(T data, QueueMessage message);


        protected virtual T ParseData(byte[] datas) {
            try {
                return this.Serializer.Value.Deserialize<T>(datas);
            } catch {
                return null;
            }
        }

        public async void Handle(QueueMessage message, IMessageContext context) {
            var data = this.ParseData(message.Body);
            if (data == null) {
                this.Logger.Value.Fatal(string.Format("Topic : {0} only accept data type : {1} , message canceled", message.Topic, typeof(T).FullName));
                context.OnMessageHandled(message);
                return;
            }

            try {
                var success = await this.Handle(data, message);
                if (success || !this.Retry)
                    context.OnMessageHandled(message);
            } catch (Exception ex) {
                this.Logger.Value.Error(ex.Message, ex);

                if (!this.Retry)
                    context.OnMessageHandled(message);
            }
        }
    }
}
