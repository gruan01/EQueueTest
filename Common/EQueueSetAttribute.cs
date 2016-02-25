using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common {

    [AttributeUsage(AttributeTargets.Class)]
    public class EQueueSetAttribute : Attribute {


        public string Topic {
            get;
            private set;
        }

        public string Group {
            get;
            private set;
        }

        public EQueueSetAttribute(string group, string topic, params string[] tags) {
            if (string.IsNullOrWhiteSpace(group))
                throw new ArgumentNullException("group");

            if (topic == null || topic.Length == 0)
                throw new ArgumentException("topic");

            this.Group = group;
            this.Topic = topic;
        }

    }
}
