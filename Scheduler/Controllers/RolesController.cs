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
using System.IO;
using Microsoft.VisualBasic.FileIO;

namespace Scheduler.Controllers
{
    public class RolesController : Controller
    {
        private SchedulerContext db = new SchedulerContext();

        // GET: Roles
        public ActionResult Index(string SortOrder, string search)
        {
            List<Role> roles = Role.getAll();

            //search feature for Roles
            if (search != null && search != "")
            {
                List<Role> matchingRoles = db.Roles.Where(s => s.Name.Contains(search)).ToList();
                roles = matchingRoles;
            }
            else
            {
                roles = Role.getAll().ToList();
            }

            //sort feature for Roles
            ViewBag.search = search;
            ViewBag.NameSortParm = SortOrder == "Name" ? "Name_desc" : "Name";

            //var roles = from s in roleViewModel.Role
            //             select s;

            switch (SortOrder)
            {
                case "Name":
                    roles = roles.OrderBy(s => s.Name).ToList();
                    break;
                case "Name_desc":
                    roles = roles.OrderByDescending(s => s.Name).ToList();
                    break;
                default:
                    roles = roles.OrderBy(s => s.Name).ToList();
                    break;
            }

            return View(roles);
        }

        // GET: Roles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // GET: Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Roles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] Role role)
        {
            if (ModelState.IsValid)
            {
                db.Roles.Add(role);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(role);
        }

        // GET: Roles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: Roles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] Role role)
        {
            if (ModelState.IsValid)
            {
                db.Entry(role).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(role);
        }

        // GET: Roles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Role role = db.Roles.Find(id);
            db.Roles.Remove(role);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Upload(HttpPostedFileBase file)
        {
            string path = string.Empty;
            string EntityType = "Role";

            BulkUpload RoleUpload = new BulkUpload();

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

            RoleUpload.Upload(path, parser, EntityType);

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
