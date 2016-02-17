using Scheduler.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Scheduler.Models {
    public class Person {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        
        public virtual ICollection<Assignment> Assignments { get; set; }
        public virtual ICollection<XPersonRole> Roles { get; set; }

        public static List<Person> getAll() {
            List<Person> persons = new List<Person>();
            
            using (var sc = new SchedulerContext())
            {
                persons = sc.Persons.ToList();
            }
            return persons;
        }
    }
}