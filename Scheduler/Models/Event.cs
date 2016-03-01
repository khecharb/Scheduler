using Scheduler.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Scheduler.Models {
    public class Event {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Room { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public virtual ICollection<Assignment> Assignments { get; set; }
        public virtual ICollection<Need> Needs { get; set; }

        public static List<Event> getAll()
        {
            List<Event> events = new List<Event>();

            using (var sc = new SchedulerContext())
            {
                events = sc.Events.ToList();
            }
            return events;
        }

    }
}