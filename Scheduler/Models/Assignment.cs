using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Scheduler.Models {
    public class Assignment {
        public int ID { get; set; }
        public int PersonID { get; set; }
        public int EventID { get; set; }
        public int RoleID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public virtual Person Person { get; set; }
        public virtual Event Event { get; set; }
        public virtual Role Role { get; set; }
    }
}