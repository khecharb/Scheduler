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

namespace Scheduler.Controllers
{
    public class AssignmentsController : Controller
    {
        private SchedulerContext db = new SchedulerContext();

        // GET: Assignments
        public ActionResult Index(string search, string option)
        {
            var assignments = db.Assignments.Include(a => a.Event).Include(a => a.Person).Include(a => a.Role);

            //search feature for assignments view
            //if (search != null && search != "" && option != null)
            //{
            //    if (option == "Name")
            //    {
            //        List<Assignment> matchingAssignments = db.Assignments.Where(s => s.Name.Contains(search)).ToList();
            //        assignments = matchingAssignments;
            //    }
            //    else if (option == "")
            //    {

            //    }
            //    else if (option == "")
            //    {

            //    }
            //}
            //else
            //{
            //    personViewModel.Persons = Person.getAll();
            //}
            return View(assignments.ToList());
        }

        // GET: Assignments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignment assignment = db.Assignments.Find(id);
            if (assignment == null)
            {
                return HttpNotFound();
            }
            return View(assignment);
        }

        // GET: Assignments/Create
        public ActionResult Create()
        {
            ViewBag.EventID = new SelectList(db.Events, "ID", "Name");
            ViewBag.PersonID = new SelectList(db.Persons, "ID", "FirstName");
            ViewBag.RoleID = new SelectList(db.Roles, "ID", "Name");
            return View();
        }

        // POST: Assignments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,PersonID,EventID,RoleID,StartTime,EndTime")] Assignment assignment)
        {
            if (ModelState.IsValid)
            {
                db.Assignments.Add(assignment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EventID = new SelectList(db.Events, "ID", "Name", assignment.EventID);
            ViewBag.PersonID = new SelectList(db.Persons, "ID", "FirstName", assignment.PersonID);
            
            return View(assignment);
        }

        // GET: Assignments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignment assignment = db.Assignments.Find(id);
            if (assignment == null)
            {
                return HttpNotFound();
            }
            ViewBag.EventID = new SelectList(db.Events, "ID", "Name", assignment.EventID);
            ViewBag.PersonID = new SelectList(db.Persons, "ID", "FirstName", assignment.PersonID);
            ViewBag.RoleID = new SelectList(db.Roles, "ID", "Name", assignment.RoleID);
            return View(assignment);
        }

        // POST: Assignments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,PersonID,EventID,RoleID,StartTime,EndTime")] Assignment assignment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assignment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EventID = new SelectList(db.Events, "ID", "Name", assignment.EventID);
            ViewBag.PersonID = new SelectList(db.Persons, "ID", "FirstName", assignment.PersonID);
            ViewBag.RoleID = new SelectList(db.Roles, "ID", "Name", assignment.RoleID);
            return View(assignment);
        }

        // GET: Assignments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignment assignment = db.Assignments.Find(id);
            if (assignment == null)
            {
                return HttpNotFound();
            }
            return View(assignment);
        }

        // POST: Assignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Assignment assignment = db.Assignments.Find(id);
            db.Assignments.Remove(assignment);
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
    }
}
