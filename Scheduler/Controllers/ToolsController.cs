using Scheduler.Models;
using Scheduler.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Scheduler.Controllers
{
    public class ToolsController : Controller
    {
        // GET: Tools
        public ActionResult Index(bool? personDelete, bool? eventDelete, bool? assignmentDelete, bool? seedDatabase)
        {
            ToolsViewModel tvc = new ToolsViewModel();
            ViewBag.seedDataBase = false;
            if (personDelete != null && (bool)personDelete) {
                tvc.personDeleteSuccess = true;
            }
            if (eventDelete != null && (bool)eventDelete) {
                tvc.eventDeleteSuccess = true;
            }
            if (assignmentDelete != null && (bool)assignmentDelete) {
                tvc.assignmentDeleteSuccess = true;
            }
            if (seedDatabase != null) {
                ViewBag.seedDataBase = seedDatabase;
            }

            return View(tvc);
        }

        public ActionResult DeletePersons() {
            Debug.WriteLine("DELETING PEOPLE");
            foreach (Person person in Person.getAll())
            {
                person.Assignments = Assignment.getAssignmentsByPersonID(person.ID);

                foreach(Assignment assignment in person.Assignments)
                {
                    Assignment.delete(assignment.ID);
                }
                Person.delete(person.ID);
            }

            return RedirectToAction("Index", new { personDelete = true });
        }

        public ActionResult DeleteEvents() {
            Debug.WriteLine("DELETING Event");
            foreach (Event eve in Event.getAll())
            {
                eve.Assignments = Assignment.getAssignmentsByEvent(eve.ID);

                foreach(Assignment assignment in eve.Assignments)
                {
                    Assignment.delete(assignment.ID);
                }

                Event.delete(eve.ID);
            }

            return RedirectToAction("Index", new { eventDelete = true });
        }

        public ActionResult DeleteAssignments() {
            Debug.WriteLine("DELETING Assignments");
            EventViewModel eventViewModel = new EventViewModel();
            eventViewModel.Events = Event.getAll();
            foreach (Event eve in eventViewModel.Events)
            {
                eve.Assignments = Assignment.getAssignmentsByEvent(eve.ID);

                foreach (Assignment assignment in eve.Assignments)
                {
                    Assignment.delete(assignment.ID);
                }
            }

            return RedirectToAction("Index", new { assignmentDelete = true });
        }

        public ActionResult SeedDatabase() {

            Debug.WriteLine("Seed Database with test data");
            EventViewModel eventViewModel = new EventViewModel();


            Person person1 = new Person();
            person1.FirstName = "Khechar_1";
            person1.LastName = "Boorla";
            person1.Email = "khechar.boorla@hcss.com";
            person1.save();

            Person person2 = new Person { FirstName = "Nick_1", LastName = "Hill", Email = "Nicolas.Hill@hcss.com" };
            person2.save();

            Person person3 = new Person { FirstName = "Santa_1", LastName = "Claus", Email = "Santa@NorthPole.net" };
            person3.save();

            Person person4 = new Person { FirstName = "John_1", LastName = "Dillinger", Email = "John@EscapePlan.org" };
            person4.save();

            Person person5 = new Person { FirstName = "Bruce_1", LastName = "Wayne", Email = "Batman@OrphanRevenge.com" };
            person5.save();

            //Also add code for events
            Event event1 = new Event { Name = "Opening Ceremony", Room = "Auditorium", EventDate = Convert.ToDateTime("4/1/2016"), StartTime = Convert.ToDateTime("9:00 PM"), EndTime = Convert.ToDateTime("10:30 AM") };
            event1.save();

            Event event2 = new Event { Name = "Morning Class", Room = "202", EventDate = Convert.ToDateTime("4/1/2016"), StartTime = Convert.ToDateTime("10:30 AM"), EndTime = Convert.ToDateTime("12:00 PM") };
            event2.save();

            Event event3 = new Event { Name = "Lunch", Room = "Cafeteria", EventDate = Convert.ToDateTime("4/1/2016"), StartTime = Convert.ToDateTime("12:00 AM"), EndTime = Convert.ToDateTime("1:00 PM") };
            event3.save();

            Event event4 = new Event { Name = "Afternoon Class", Room = "500", EventDate = Convert.ToDateTime("4/1/2016"), StartTime = Convert.ToDateTime("1:00 PM"), EndTime = Convert.ToDateTime("5:00 PM") };
            event4.save();

            eventViewModel.Events = Event.getAll();

            //The getRoleByName call is duplicating Roles here
            Role r = Role.getRoleByName("Hero");

            Assignment assignment1 = new Assignment { PersonID = person3.ID, Event = event3, RoleID = r.ID, StartTime = Convert.ToDateTime("12:00PM"), EndTime = Convert.ToDateTime("1:00 PM") };
            assignment1.save();

            //Assignment assignment2 = new Assignment { Person = person4, Event = event2, Role = Role.getRoleByName("Bank Robber"), StartTime = Convert.ToDateTime("12:00PM"), EndTime = Convert.ToDateTime("1:00 PM") };
            //assignment2.save();

            //Assignment assignment3 = new Assignment { Person = person5, Event = event1, Role = Role.getRoleByName("Hero"), StartTime = Convert.ToDateTime("12:00PM"), EndTime = Convert.ToDateTime("1:00 PM") };
            //assignment3.save();

            //Assignment assignment4 = new Assignment { Person = person1, Event = event4, Role = Role.getRoleByName("Speaker"), StartTime = Convert.ToDateTime("12:00PM"), EndTime = Convert.ToDateTime("1:00 PM") };
            //assignment4.save();

            //Assignment assignment5 = new Assignment { Person = person5, Event = event4, Role = Role.getRoleByName("Guest of Honor"), StartTime = Convert.ToDateTime("12:00PM"), EndTime = Convert.ToDateTime("1:00 PM") };
            //assignment5.save();

            return RedirectToAction("Index", new { seedDataBase = true });
        }
    }
}