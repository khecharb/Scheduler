using Scheduler.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Scheduler.Models {
    public class Event
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Room { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public ICollection<Assignment> Assignments { get; set; }
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

        public static void delete(int? eveID)
        {
            if (eveID == null) { return; }

            using (var sc = new SchedulerContext())
            {
                Event p = sc.Events.Find(eveID);
                // also remove assignments associated with event p
                //p.Assignments = Assignment.getAssignmentsByEvent(p.ID);

                //foreach (Assignment assignment in p.Assignments)
                //{
                    
                //    sc.Assignments.Remove(assignment);
                //    sc.SaveChanges();
                //}

                sc.Events.Remove(p);
                sc.SaveChanges();
            }
        }

        public static Event getEventByID(int eventID)
        {
            Event eve = new Event();

            using (var sc = new SchedulerContext())
            {
                eve = sc.Events.Where(e => e.ID == eventID).SingleOrDefault();
            }

            return eve;
        }

        public void save()
        {
            if (this.ID <= 0)
            {
                //new person
                if (this.Name != null && this.Name.Length > 0 &&
                    this.Room != null && this.Room.Length > 0 &&
                    this.EventDate != null &&
                    this.StartTime != null &&
                    this.EndTime != null)
                {

                    using (var sc = new SchedulerContext())
                    {
                        sc.Events.Add(this);
                        sc.SaveChanges();
                    }
                }
            }
            else {
                //update existing
                if (this.Name != null && this.Name.Length > 0 &&
                    this.Room != null && this.Room.Length > 0 &&
                    this.EventDate != null &&
                    this.StartTime != null &&
                    this.EndTime != null)
                {

                    using (var sc = new SchedulerContext())
                    {
                        sc.Events.Attach(this);
                        sc.Entry(this).State = EntityState.Modified;
                        sc.SaveChanges();
                    }
                }
            }

        }
    }
}