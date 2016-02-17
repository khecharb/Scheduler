using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Scheduler.Models {
    public class Need {
        public int ID { get; set; }
        public int NeedType { get; set; }
        public int Quantity { get; set; }
        public int RoleID { get; set; }
        public int EventID { get; set; }

        public virtual Role Role { get; set; }
        public virtual Event Event { get; set; }

    }
}