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
        public ActionResult Index(bool? personDelete, bool? eventDelete, bool? assignmentDelete)
        {
            ToolsViewModel tvc = new ToolsViewModel();
            if (personDelete != null && (bool)personDelete) {
                tvc.personDeleteSuccess = true;
            }
            if (eventDelete != null && (bool)eventDelete) {
                tvc.eventDeleteSuccess = true;
            }
            if (assignmentDelete != null && (bool)assignmentDelete) {
                tvc.assignmentDeleteSuccess = true;
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
    }
}