using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models {

    [EQueueSet(Const.Group, Const.TOPIC_3)]
    public class ExecCmd {

        public string CmdString {
            get;
            set;
        }

    }
}
