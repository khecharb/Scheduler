using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Scheduler.Models;

namespace Scheduler.DAL {
    public class SchedulerInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<SchedulerContext> {
        public override void InitializeDatabase(SchedulerContext context) {
            context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction
                , string.Format("ALTER DATABASE [{0}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE", context.Database.Connection.Database));

            base.InitializeDatabase(context);
        }
        
        protected override void Seed(SchedulerContext context) {
            var events = new List<Event> {
                new Event{Name="Keynote",Room="Legends I",EventDate=DateTime.Parse("2016-02-01"),StartTime=DateTime.Parse("2016-02-01 08:00 AM"),EndTime=DateTime.Parse("2016-02-01 09:00 AM")},
                new Event{Name="Keynote Setup",Room="Legends I",EventDate=DateTime.Parse("2016-02-01"),StartTime=DateTime.Parse("2016-02-01 07:00 AM"),EndTime=DateTime.Parse("2016-02-01 09:00 AM")},
                new Event{Name="Manage Your Fleet",Room="Founders I-II",EventDate=DateTime.Parse("2016-02-01"),StartTime=DateTime.Parse("2016-02-01 09:00 AM"),EndTime=DateTime.Parse("2016-02-01 12:00 PM")},
                new Event{Name="Coaches Corner",Room="Founders III-IV",EventDate=DateTime.Parse("2016-02-01"),StartTime=DateTime.Parse("2016-02-01 04:00 PM"),EndTime=DateTime.Parse("2016-02-01 06:00 PM")},
                new Event{Name="Lunch",Room="Champions I-II",EventDate=DateTime.Parse("2016-02-01"),StartTime=DateTime.Parse("2016-02-01 12:00 PM"),EndTime=DateTime.Parse("2016-02-01 01:00 PM")},
                new Event{Name="Breakfast",Room="Champions I-II",EventDate=DateTime.Parse("2016-02-01"),StartTime=DateTime.Parse("2016-02-01 07:00 AM"),EndTime=DateTime.Parse("2016-02-01 08:00 AM")},
                new Event{Name="Understanding HeavyBid",Room="Champions III-IV",EventDate=DateTime.Parse("2016-02-01"),StartTime=DateTime.Parse("2016-02-01 01:00 PM"),EndTime=DateTime.Parse("2016-02-01 04:00 PM")},
                new Event{Name="How To Move To Mobile",Room="Summit",EventDate=DateTime.Parse("2016-02-01"),StartTime=DateTime.Parse("2016-02-01 01:00 PM"),EndTime=DateTime.Parse("2016-02-01 04:00 PM")},
            };

            events.ForEach(e => context.Events.Add(e));
            context.SaveChanges();
            
            var persons = new List<Person> {
                new Person{FirstName="Khechar",LastName="Boorla",Email="khechar.boorla@hcss.com"},
                new Person{FirstName="Nick",LastName="Hill",Email="Nicolas.Hill@hcss.com"},
                new Person{FirstName="Philly",LastName="TheKid",Email="Phillip.Waller@hcss.com"},
                new Person{FirstName="Wyatt",LastName="Earp",Email="Wyatt.Beavers@hcss.com"},
                new Person{FirstName="Michael",LastName="GlueStick",Email="Michael.Glueck@hcss.com"},
                new Person{FirstName="Barack",LastName="Obama",Email="POTUS@hcss.com"},
            };
            persons.ForEach(p => context.Persons.Add(p));
            context.SaveChanges();

            var roles = new List<Role> {
                new Role{Name="Attendee"},
                new Role{Name="Speaker"},
            };
            roles.ForEach(r => context.Roles.Add(r));
            context.SaveChanges();

            var assignments = new List<Assignment> {
                new Assignment{PersonID=6,EventID=1,StartTime=DateTime.Parse("2016-02-01 08:00 AM"), EndTime=DateTime.Parse("2016-02-01 09:00 AM"), RoleID=2},
                new Assignment{PersonID=5,EventID=1,StartTime=DateTime.Parse("2016-02-01 08:00 AM"), EndTime=DateTime.Parse("2016-02-01 09:00 AM"), RoleID=1},
                new Assignment{PersonID=4,EventID=1,StartTime=DateTime.Parse("2016-02-01 08:00 AM"), EndTime=DateTime.Parse("2016-02-01 09:00 AM"), RoleID=1},
                new Assignment{PersonID=3,EventID=1,StartTime=DateTime.Parse("2016-02-01 08:00 AM"), EndTime=DateTime.Parse("2016-02-01 09:00 AM"), RoleID=1},
                new Assignment{PersonID=2,EventID=1,StartTime=DateTime.Parse("2016-02-01 08:00 AM"), EndTime=DateTime.Parse("2016-02-01 09:00 AM"), RoleID=1},
                new Assignment{PersonID=1,EventID=1,StartTime=DateTime.Parse("2016-02-01 08:00 AM"), EndTime=DateTime.Parse("2016-02-01 09:00 AM"), RoleID=1},

                new Assignment{PersonID=5,EventID=2,StartTime=DateTime.Parse("2016-02-01 07:00 AM"), EndTime=DateTime.Parse("2016-02-01 07:45 AM"), RoleID=1},
                new Assignment{PersonID=4,EventID=2,StartTime=DateTime.Parse("2016-02-01 07:00 AM"), EndTime=DateTime.Parse("2016-02-01 07:45 AM"), RoleID=1},
                new Assignment{PersonID=3,EventID=2,StartTime=DateTime.Parse("2016-02-01 07:00 AM"), EndTime=DateTime.Parse("2016-02-01 07:45 AM"), RoleID=1},
                new Assignment{PersonID=2,EventID=2,StartTime=DateTime.Parse("2016-02-01 07:00 AM"), EndTime=DateTime.Parse("2016-02-01 08:00 AM"), RoleID=1},
                new Assignment{PersonID=1,EventID=2,StartTime=DateTime.Parse("2016-02-01 07:00 AM"), EndTime=DateTime.Parse("2016-02-01 08:00 AM"), RoleID=1},

                new Assignment{PersonID=3,EventID=3,StartTime=DateTime.Parse("2016-02-01 09:00 AM"), EndTime=DateTime.Parse("2016-02-01 12:00 PM"), RoleID=1},
                new Assignment{PersonID=2,EventID=3,StartTime=DateTime.Parse("2016-02-01 09:00 AM"), EndTime=DateTime.Parse("2016-02-01 12:00 PM"), RoleID=1},
                new Assignment{PersonID=4,EventID=3,StartTime=DateTime.Parse("2016-02-01 09:00 AM"), EndTime=DateTime.Parse("2016-02-01 12:00 PM"), RoleID=1},

                new Assignment{PersonID=4,EventID=4,StartTime=DateTime.Parse("2016-02-01 05:00 PM"), EndTime=DateTime.Parse("2016-02-01 06:00 pM"), RoleID=1},
                new Assignment{PersonID=3,EventID=4,StartTime=DateTime.Parse("2016-02-01 04:00 PM"), EndTime=DateTime.Parse("2016-02-01 05:45 PM"), RoleID=1},
            };
            assignments.ForEach(a => context.Assignments.Add(a));
            context.SaveChanges();

            
        }

    }
}