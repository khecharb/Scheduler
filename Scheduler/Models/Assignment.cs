using Scheduler.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

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

        public static List<Assignment> getAssignmentsByPersonID (int personID) {
            List<Assignment> Assignments = new List<Assignment>();

            using (var sc = new SchedulerContext()) {
                Assignments = sc.Assignments.Where(a => a.PersonID == personID).ToList();
            }

            return Assignments;
        }
    }
}