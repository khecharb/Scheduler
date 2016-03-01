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
        public ActionResult Index(string SortOrder, string option, string search/*, DateTime SearchDateTime*/) {
            
            //first I had to rename the third parameter from 'SearchString' to 'search' -- 'search' is what you called it in the index.cshtml file
            // the following will be a algorithm for search. I'll ignore the sorting feature for now
            
            EventViewModel eventViewModel = new EventViewModel(); //this constructor will return an object that conains a list of all events a list of roles... see the EventViewModel class

            //did the user type in a search term?
            if (search != null && search != "" && option != null) {
                //determine what field they are searching; i.e. Name, Room, etc...
                if (option == "Name") {
                    //  1. get a list of events whose name field matches the search term
                    List<Event> matchingEvents = db.Events.Where(s => s.Name == search).ToList();
                            //or use var instead of List ---> var MatchingEvents = db.Events.Where(s => s.Name == search).ToList();
                            //or don't go to the database, you already have a complete list of events in the view model. ---> List<Event> matchingEvents = eventViewModel.Events.Where(s => s.Name == search).ToList();
                    //  2. replace the list of all events in the eventViewModel with the list of matching events
                    eventViewModel.Events = matchingEvents;
                }
                //else if () ..... other searches go here

            }
            else {
                //no search term; don't need to do anything

            }






            //return the eventViewModel object which has either all of the events or a list of matching events
            return View(eventViewModel);
            
            
            //ViewBag.NameSortParm = String.IsNullOrEmpty(SortOrder) ? "Name_desc" : "";
            //ViewBag.RoomSortParm = SortOrder == "Room" ? "Room_desc" : "";
            //ViewBag.DateSortParm = SortOrder == "Date" ? "Date_desc" : "";
            //ViewBag.StartTimeSortParm = SortOrder == "StartTime" ? "StartTime_desc" : "";
            //ViewBag.EndTimeSortParm = SortOrder == "EndTime" ? "EndTime_desc" : "";


            //var Events = from s in db.Events
            //             select s;

            //if (option == "Name")
            //{
            //    var eve = db.Events.Where(s => s.Name == SearchString || SearchString == null).ToList();
            //    var eventViewModel = new EventViewModel();
            //    eventViewModel.Events = eve;
            //    return View(eventViewModel);
            //}
            //else if (option == "Room")
            //{
            //    return View(db.Events.Where(s => s.Room == SearchString || SearchString == null).ToList());
            //}
            //else if (option == "Date")
            //{
            //    DateTime SearchDateTime = Convert.ToDateTime(SearchString);
            //    return View(db.Events.Where(s => s.EventDate == SearchDateTime || SearchDateTime == null).ToList());
            //}
            //else if (option == "Start Time")
            //{
            //    DateTime SearchDateTime = Convert.ToDateTime(SearchString);
            //    return View(db.Events.Where(s => s.StartTime == SearchDateTime || SearchDateTime == null).ToList());
            //}
            //else if (option == "End Time")
            //{
            //    DateTime SearchDateTime = Convert.ToDateTime(SearchString);
            //    return View(db.Events.Where(s => s.EndTime == SearchDateTime || SearchDateTime == null).ToList());
            //}
            ///*if (!String.IsNullOrEmpty(SearchString))
            //{
            //    Events = Events.Where(s => s.Name.Contains(SearchString) || s.Room.Contains(SearchString));
            //}
            //else if (String = Convert.ToDateTime == null(SearchDateTime))
            //{
            //    Events = Events.Where(s => s.EventDate.Contains(SearchDateTime) || s.StartTime.Contains(SearchDateTime) || s.EndTime.Contains(SearchDateTime));
            //}
            //*/
            //switch (SortOrder)
            //{
            //    case "name_desc":
            //        Events = Events.OrderByDescending(s => s.Name);
            //        break;
            //    case "Room":
            //        Events = Events.OrderBy(s => s.Room);
            //        break;
            //    case "Room_desc":
            //        Events = Events.OrderByDescending(s => s.Room);
            //        break;
            //    case "Date":
            //        Events = Events.OrderBy(s => s.EventDate);
            //        break;
            //    case "Date_desc":
            //        Events = Events.OrderByDescending(s => s.EventDate);
            //        break;
            //    case "StartTime":
            //        Events = Events.OrderBy(s => s.StartTime);
            //        break;
            //    case "StartTime_desc":
            //        Events = Events.OrderByDescending(s => s.StartTime);
            //        break;
            //    case "EndTime":
            //        Events = Events.OrderBy(s => s.EndTime);
            //        break;
            //    case "EndTime_desc":
            //        Events = Events.OrderByDescending(s => s.EndTime);
            //        break;
            //    default:
            //        Events = Events.OrderBy(s => s.Name);
            //        break;
            //}

            //var personViewModel = new EventViewModel();
            //return View(personViewModel);
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