using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Scheduler.DAL;

namespace Scheduler.Models {
    public class XPersonRole {
        public int ID { get; set; }
        public int RoleID { get; set; }
        public int PersonID { get; set; }

        public virtual Role Role { get; set; }
        public virtual Person Person { get; set; }

    }


}