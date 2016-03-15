using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Scheduler.DAL;
using Scheduler.Models;
using Scheduler.ViewModels;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using System.Diagnostics;

namespace Scheduler.Controllers
{
    public class PersonController : Controller
    {
        private SchedulerContext db = new SchedulerContext();

        // GET: Person
        public ActionResult Index()
        {
            PersonViewModel personViewModel = new PersonViewModel();
            personViewModel.Persons = Person.getAll();
            
            foreach (Person person in personViewModel.Persons)
            {
                person.Assignments = Assignment.getAssignmentsByPersonID(person.ID);
                
                foreach (Assignment assignment in person.Assignments)
                {
                    Event eve = new Event();
                    eve = Event.getEventByID(assignment.EventID);
                    assignment.Event = eve;
                    Role role = new Role(); 
                    role = Role.getRoleByID(assignment.RoleID);
                    assignment.Role = role;
                    
                }
            }
            //demoMethods();
            //PersonViewModel personViewModel = createTestData();
            return View(personViewModel);
        }

        

        private void demoMethods() {
            /**********************************************************************/
            //
            // the getAll() method
            //
            // getAll() taks no parameters
            // getAll() returns a list of objects. List<objectType>
            //
            //Create an empty list of Persons
            List<Person> persons = new List<Person>();

            //Call the Person.getAll() method and store the return value in our list.
            persons = Person.getAll();

            //output data
            Debug.WriteLine("****** PERSONS ********");
            foreach (Person p in persons) {
                Debug.WriteLine("ID: " + p.ID);
                Debug.WriteLine("Name: " + p.FirstName + " " + p.LastName);
                if (p.Assignments == null) { Debug.WriteLine("Assignments not loaded"); }
                else { Debug.WriteLine("Assignment count: " + p.Assignments.Count.ToString()); }
                Debug.WriteLine("-------------------------------------------------------");
            }


            /**********************************************************************/
            //
            // the getByID(int Id) method
            //
            // getByID(int Id) takes one parameter, an integer value that represents the unique ID of the object to get
            // getAll(int Id) returns one object matching the Id provided. Returns null if no matching object is found.
            //
            //Create a empty object
            Event eve = new Event();

            //Create two dummy event IDs for testing.
            int ExistingEventID = 1;
            int NonExistingEventID = 999;

            //Call the Event.getById(int Id) method and store the return value in our empty event object
            eve = Event.getEventByID(ExistingEventID);

            //output data
            if (eve == null) {
                Debug.WriteLine("****** EVENT DOES NOT EXIST ********");
            }
            else {
                Debug.WriteLine("****** EXISITNG EVENT ********");
                Debug.WriteLine("ID: " + eve.ID);
                Debug.WriteLine("Name: " + eve.Name);
                Debug.WriteLine("Room: " + eve.Room);
                if (eve.Assignments == null) { Debug.WriteLine("Assignments not loaded"); }
                else { Debug.WriteLine("Assignment count: " + eve.Assignments.Count.ToString()); }
                Debug.WriteLine("-------------------------------------------------------");
            }

            //Call the Event.getById(int Id) method with a NonExisting event id. The return value wil be null.
            eve = Event.getEventByID(NonExistingEventID);

            //output data
            if (eve == null) {
                Debug.WriteLine("****** EVENT DOES NOT EXIST ********");
            }
        
        }

        // GET: Person/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.Persons.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // GET: Person/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Person/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.Persons.Add(person);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(person);
        }

        // GET: Person/Edit/5
        public ActionResult Edit(int? id)
        {
            //my method

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.Persons.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: Person/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(person);
        }

        // GET: Person/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.Persons.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: Person/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Person person = db.Persons.Find(id);
            db.Persons.Remove(person);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private PersonViewModel createTestData() {
            //
            //
            // THIS is KINDA THE SOLUTION DON"T LOOK AT IT!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            //
            // ALL YOU NEED TO KNOW IS THAT IT CREATES THE DATA TO PUT ON THE WEBPAGE, WITHOUT NEEDING TO HAVE THE DATA IN THE DATABASE
            //
            //



            PersonViewModel personViewModel = new PersonViewModel();

            var persons = new List<Person> {
                new Person{ID = 1, FirstName="Khechar",LastName="Boorla",Email="khechar.boorla@hcss.com"},
                new Person{ID = 2, FirstName="Nick",LastName="Hill",Email="Nicolas.Hill@hcss.com"},
                new Person{ID = 3, FirstName="Philly",LastName="TheKid",Email="Phillip.Waller@hcss.com"},
                new Person{ID = 4, FirstName="Wyatt",LastName="Earp",Email="Wyatt.Beavers@hcss.com"},
                new Person{ID = 5, FirstName="Michael",LastName="GlueStick",Email="Michael.Glueck@hcss.com"},
                new Person{ID = 6, FirstName="Barack",LastName="Obama",Email="POTUS@hcss.com"},
            };

            var events = new List<Event> {
                new Event{ID = 1, Name="Keynote",Room="Legends I",EventDate=DateTime.Parse("2016-02-01"),StartTime=DateTime.Parse("2016-02-01 08:00 AM"),EndTime=DateTime.Parse("2016-02-01 09:00 AM")},
                new Event{ID = 2, Name="Keynote Setup",Room="Legends I",EventDate=DateTime.Parse("2016-02-01"),StartTime=DateTime.Parse("2016-02-01 07:00 AM"),EndTime=DateTime.Parse("2016-02-01 09:00 AM")},
                new Event{ID = 3, Name="Manage Your Fleet",Room="Founders I-II",EventDate=DateTime.Parse("2016-02-01"),StartTime=DateTime.Parse("2016-02-01 09:00 AM"),EndTime=DateTime.Parse("2016-02-01 12:00 PM")},
                new Event{ID = 4, Name="Coaches Corner",Room="Founders III-IV",EventDate=DateTime.Parse("2016-02-01"),StartTime=DateTime.Parse("2016-02-01 04:00 PM"),EndTime=DateTime.Parse("2016-02-01 06:00 PM")},
                new Event{ID = 5, Name="Lunch",Room="Champions I-II",EventDate=DateTime.Parse("2016-02-01"),StartTime=DateTime.Parse("2016-02-01 12:00 PM"),EndTime=DateTime.Parse("2016-02-01 01:00 PM")},
                new Event{ID = 6, Name="Breakfast",Room="Champions I-II",EventDate=DateTime.Parse("2016-02-01"),StartTime=DateTime.Parse("2016-02-01 07:00 AM"),EndTime=DateTime.Parse("2016-02-01 08:00 AM")},
                new Event{ID = 7, Name="Understanding HeavyBid",Room="Champions III-IV",EventDate=DateTime.Parse("2016-02-01"),StartTime=DateTime.Parse("2016-02-01 01:00 PM"),EndTime=DateTime.Parse("2016-02-01 04:00 PM")},
                new Event{ID = 8, Name="How To Move To Mobile",Room="Summit",EventDate=DateTime.Parse("2016-02-01"),StartTime=DateTime.Parse("2016-02-01 01:00 PM"),EndTime=DateTime.Parse("2016-02-01 04:00 PM")},
            };

            var roles = new List<Role> {
                new Role{ID = 1, Name="Attendee"},
                new Role{ID = 2, Name="Speaker"},
            };

            var assignments = new List<Assignment> {
                new Assignment{ID = 1, PersonID=6,EventID=1,StartTime=DateTime.Parse("2016-02-01 08:00 AM"), EndTime=DateTime.Parse("2016-02-01 09:00 AM"), RoleID=2},
                new Assignment{ID = 2, PersonID=5,EventID=1,StartTime=DateTime.Parse("2016-02-01 08:00 AM"), EndTime=DateTime.Parse("2016-02-01 09:00 AM"), RoleID=1},
                new Assignment{ID = 3, PersonID=4,EventID=1,StartTime=DateTime.Parse("2016-02-01 08:00 AM"), EndTime=DateTime.Parse("2016-02-01 09:00 AM"), RoleID=1},
                new Assignment{ID = 4, PersonID=3,EventID=1,StartTime=DateTime.Parse("2016-02-01 08:00 AM"), EndTime=DateTime.Parse("2016-02-01 09:00 AM"), RoleID=1},
                new Assignment{ID = 5, PersonID=2,EventID=1,StartTime=DateTime.Parse("2016-02-01 08:00 AM"), EndTime=DateTime.Parse("2016-02-01 09:00 AM"), RoleID=1},
                new Assignment{ID = 6, PersonID=1,EventID=1,StartTime=DateTime.Parse("2016-02-01 08:00 AM"), EndTime=DateTime.Parse("2016-02-01 09:00 AM"), RoleID=1},

                new Assignment{ID = 7, PersonID=5,EventID=2,StartTime=DateTime.Parse("2016-02-01 07:00 AM"), EndTime=DateTime.Parse("2016-02-01 07:45 AM"), RoleID=1},
                new Assignment{ID = 8, PersonID=4,EventID=2,StartTime=DateTime.Parse("2016-02-01 07:00 AM"), EndTime=DateTime.Parse("2016-02-01 07:45 AM"), RoleID=1},
                new Assignment{ID = 9, PersonID=3,EventID=2,StartTime=DateTime.Parse("2016-02-01 07:00 AM"), EndTime=DateTime.Parse("2016-02-01 07:45 AM"), RoleID=1},
                new Assignment{ID = 10, PersonID=2,EventID=2,StartTime=DateTime.Parse("2016-02-01 07:00 AM"), EndTime=DateTime.Parse("2016-02-01 08:00 AM"), RoleID=1},
                new Assignment{ID = 11, PersonID=1,EventID=2,StartTime=DateTime.Parse("2016-02-01 07:00 AM"), EndTime=DateTime.Parse("2016-02-01 08:00 AM"), RoleID=1},

                new Assignment{ID = 12, PersonID=3,EventID=3,StartTime=DateTime.Parse("2016-02-01 09:00 AM"), EndTime=DateTime.Parse("2016-02-01 12:00 PM"), RoleID=1},
                new Assignment{ID = 13, PersonID=2,EventID=3,StartTime=DateTime.Parse("2016-02-01 09:00 AM"), EndTime=DateTime.Parse("2016-02-01 12:00 PM"), RoleID=1},
                new Assignment{ID = 14, PersonID=4,EventID=3,StartTime=DateTime.Parse("2016-02-01 09:00 AM"), EndTime=DateTime.Parse("2016-02-01 12:00 PM"), RoleID=1},

                new Assignment{ID = 15, PersonID=4,EventID=4,StartTime=DateTime.Parse("2016-02-01 05:00 PM"), EndTime=DateTime.Parse("2016-02-01 06:00 pM"), RoleID=1},
                new Assignment{ID = 16, PersonID=3,EventID=4,StartTime=DateTime.Parse("2016-02-01 04:00 PM"), EndTime=DateTime.Parse("2016-02-01 05:45 PM"), RoleID=1},
            };

            foreach (Person person in persons) {
                person.Assignments = assignments.Where(a => a.PersonID == person.ID).ToList();

                foreach (Assignment assignment in person.Assignments) {
                    assignment.Event = events.Where(e => e.ID == assignment.EventID).SingleOrDefault();
                    assignment.Role = roles.Where(r => r.ID == assignment.RoleID).SingleOrDefault();
                }
            }
            personViewModel.Persons = persons;

            return personViewModel;

        }







    }
}
