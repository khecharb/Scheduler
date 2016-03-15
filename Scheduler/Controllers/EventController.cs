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


namespace Scheduler.Controllers
{
    public class EventController : Controller
    {
        private SchedulerContext db = new SchedulerContext();

        // GET: Person
        public ActionResult Index(string SortOrder, string option, string search, DateTime? SearchDateTime)
        {

            EventViewModel eventViewModel = new EventViewModel(); //this constructor will return an object that conains a list of all events a list of roles... see the EventViewModel class

            //did the user type in a search term?
            if (search != null && search != "" && option != null)
            {
                //determine what field they are searching; i.e. Name, Room, etc...
                if (option == "Name")
                {
                    //  1. get a list of events whose name field matches the search term
                    List<Event> matchingEvents = db.Events.Where(s => s.Name.Contains(search)).ToList();
                    //  2. replace the list of all events in the eventViewModel with the list of matching events
                    eventViewModel.Events = matchingEvents;
                }
                else if (option == "Room")
                {
                    List<Event> matchingEvents = db.Events.Where(s => s.Room.Contains(search)).ToList();
                    eventViewModel.Events = matchingEvents;
                }
                else if (option == "Date")
                {
                    List<Event> matchingEvents = new List<Event>();
                    foreach (Event e in eventViewModel.Events)
                    {
                        if (e.EventDate.ToString().Contains(search) == true)
                        {
                            matchingEvents.Add(e);
                        }
                    }
                    eventViewModel.Events = matchingEvents;
                }
                else if (option == "Start Time")
                {
                    List<Event> matchingEvents = new List<Event>();
                    foreach (Event e in eventViewModel.Events)
                    {
                        if (e.StartTime.ToString().Contains(search) == true)
                        {
                            matchingEvents.Add(e);
                        }
                    }
                    eventViewModel.Events = matchingEvents;
                }
                else if (option == "End Time")
                {
                    List<Event> matchingEvents = new List<Event>();
                    foreach (Event e in eventViewModel.Events)
                    {
                        if (e.EndTime.ToString().Contains(search) == true)
                        {
                            matchingEvents.Add(e);
                        }
                    }
                    eventViewModel.Events = matchingEvents;
                }
            }
            else
            {
                //no search term; don't need to do anything          
            }

            ViewBag.search = search;
            ViewBag.option = option;
            ViewBag.NameSortParm = SortOrder == "Name" ? "Name_desc" : "Name";  
            ViewBag.RoomSortParm = SortOrder == "Room" ? "Room_desc" : "Room";
            ViewBag.DateSortParm = SortOrder == "Date" ? "Date_desc" : "Date";
            ViewBag.StartTimeSortParm = SortOrder == "StartTime" ? "StartTime_desc" : "StartTime";
            ViewBag.EndTimeSortParm = SortOrder == "EndTime" ? "EndTime_desc" : "EndTime";

            var Events = from s in eventViewModel.Events
                select s;

            switch (SortOrder)
            {
                case "Name":
                    //eventViewModel. Events = eventViewModel.Events.OrderBy(s => s.Name).ToList();
                    Events = Events.OrderBy(s => s.Name);
                    break;
                case "Name_desc":
                    //eventViewModel.Events = eventViewModel.Events.OrderBy(s => s.Name).ToList();
                    Events = Events.OrderByDescending(s => s.Name);
                    break;
                case "Room":
                    //eventViewModel.Events = eventViewModel.Events.OrderBy(s => s.Name).ToList();
                    Events = Events.OrderBy(s => s.Room);
                    break;
                case "Room_desc":
                    //eventViewModel.Events = eventViewModel.Events.OrderBy(s => s.Name).ToList();
                    Events = Events.OrderByDescending(s => s.Room);
                    break;
                case "Date":
                    //eventViewModel.Events = eventViewModel.Events.OrderBy(s => s.Name).ToList();
                    Events = Events.OrderBy(s => s.EventDate);
                    break;
                case "Date_desc":
                    //eventViewModel.Events = eventViewModel.Events.OrderBy(s => s.Name).ToList();
                    Events = Events.OrderByDescending(s => s.EventDate);
                    break;
                case "StartTime":
                    //eventViewModel.Events = eventViewModel.Events.OrderBy(s => s.Name).ToList();
                    Events = Events.OrderBy(s => s.StartTime);
                    break;
                case "StartTime_desc":
                    //eventViewModel.Events = eventViewModel.Events.OrderBy(s => s.Name).ToList();
                    Events = Events.OrderByDescending(s => s.StartTime);
                    break;
                case "EndTime":
                    //eventViewModel.Events = eventViewModel.Events.OrderBy(s => s.Name).ToList();
                    Events = Events.OrderBy(s => s.EndTime);
                    break;
                case "EndTime_desc":
                    //eventViewModel.Events = eventViewModel.Events.OrderBy(s => s.Name).ToList();
                    Events = Events.OrderByDescending(s => s.EndTime);
                    break;
                default:
                    //eventViewModel.Events = eventViewModel.Events.OrderBy(s => s.Name).ToList();
                    Events = Events.OrderBy(s => s.Name);
                    break;
            }
            //eventViewModel.Events.ToList();
            eventViewModel.Events = Events.ToList();
            return View(eventViewModel);
        }




        // GET: Person/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event Eve = db.Events.Find(id);
            if (Eve == null)
            {
                return HttpNotFound();
            }
            return View(Eve);
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
        public ActionResult Create([Bind(Include = "ID,Name,Room,EventDate,StartTime,EndTime")] Event Eve)
        {
            if (ModelState.IsValid)
            {
                db.Events.Add(Eve);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(Eve);
        }

        // GET: Event/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event Eve = db.Events.Find(id);
            if (Eve == null)
            {
                return HttpNotFound();
            }
            return View(Eve);
        }

        // POST: Event/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Room,EventDate,StartTime,EndTime")] Event Eve)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Eve).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Eve);
        }

        // GET: Person/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event Eve = db.Events.Find(id);
            if (Eve == null)
            {
                return HttpNotFound();
            }
            return View(Eve);
        }

        // POST: Event/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event Eve = db.Events.Find(id);
            db.Events.Remove(Eve);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            string path = string.Empty;
            string EntityType = "Event";

            BulkUpload EveUpload = new BulkUpload();  

            // Verify that the user selected a file
            if (file != null && file.ContentLength > 0)
            {
                // extract only the filename
                var fileName = Path.GetFileName(file.FileName);
                // store the file inside ~/App_Data/uploads folder
                path = Path.Combine(Server.MapPath("~/App_Data/Uploads"), fileName);
                file.SaveAs(path);
            }

            TextFieldParser parser = new TextFieldParser(path);
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(",");

            EveUpload.Upload(path, parser, EntityType);

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
    }
}