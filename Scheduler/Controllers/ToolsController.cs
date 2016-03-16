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

            //YOUR CODE HERE

            return RedirectToAction("Index", new { personDelete = true });
        }

        public ActionResult DeleteEvents() {
            Debug.WriteLine("DELETING Event");

            //YOUR CODE HERE

            return RedirectToAction("Index", new { eventDelete = true });
        }

        public ActionResult DeleteAssignments() {
            Debug.WriteLine("DELETING Assignments");

            //YOUR CODE HERE

            return RedirectToAction("Index", new { assignmentDelete = true });
        }

        public ActionResult SeedDatabase() {
            
            Debug.WriteLine("Seed Database with test data");

            
            
            Person person1 = new Person();
            person1.FirstName = "Khechar_1";
            person1.LastName = "Boorla";
            person1.Email = "khechar.boorla@hcss.com";
            person1.save();

            Person person2 = new Person { FirstName = "Nick_1", LastName = "Hill", Email = "Nicolas.Hill@hcss.com" };
            person2.save();
            //add code for more people
            
            //Also add code for events



            //also add code for assignments *more complex*


            return RedirectToAction("Index", new { seedDataBase = true });
        }
    }
}