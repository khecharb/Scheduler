using Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Scheduler.ViewModels {
    public class EventViewModel {
        public List<Event> Events { get; set; }
        public List<Role> Roles { get; set; }

        //public EventViewModel() {
        //    Events = Event.getAll();
        //    Roles = Role.getAll();

        //}


    }
}