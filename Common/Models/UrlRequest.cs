using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models {

    [EQueueSet(Const.Group, Const.TOPIC_1)]
    public class UrlRequest {

        public Uri RequestUri {
            get;
            set;
        }

    }
}
