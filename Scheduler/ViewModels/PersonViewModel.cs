using Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Scheduler.ViewModels {
    public class PersonViewModel {
        public List<Person> Persons { get; set; }
        public List<Role> Roles { get; set; }

        //public PersonViewModel() {
        //    Persons = Person.getAll();
        //    Roles = Role.getAll();
            
        //}
    }
}