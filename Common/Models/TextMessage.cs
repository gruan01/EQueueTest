using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models {

    [EQueueSet(Const.Group, Const.TOPIC_2)]
    public class TextMessage {

        public string Msg {
            get;
            set;
        }

    }
}
