using Scheduler.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Scheduler.Models {
    public class Person {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        
        public ICollection<Assignment> Assignments { get; set; }
        public virtual ICollection<XPersonRole> Roles { get; set; }

        public static List<Person> getAll() {
            List<Person> persons = new List<Person>();
            
            using (var sc = new SchedulerContext())
            {
                persons = sc.Persons.ToList();
            }
            return persons;
        }

        public static Person getPersonByID(int? personID) {
            if (personID == null) { return null;  }
            
            Person person = new Person();

            using (var sc = new SchedulerContext()) {
                person = sc.Persons.Where(p => p.ID == personID).SingleOrDefault();
            }

            return person;
        }

        public static void delete(int? personID) {
            if (personID == null) { return; }

            using (var sc = new SchedulerContext()) {
                Person p = sc.Persons.Find(personID);
                sc.Persons.Remove(p);
                sc.SaveChanges();
            }
        }

        public void save() {
            if (this.ID <= 0) {
                //new person
                if (this.FirstName != null && this.FirstName.Length > 0 &&
                    this.LastName != null && this.LastName.Length > 0 &&
                    this.Email != null && this.Email.Length > 0) {

                        using (var sc = new SchedulerContext()) {
                            sc.Persons.Add(this);
                            sc.SaveChanges();
                        }
                }
            }
            else {
                //update existing
                if (this.FirstName != null && this.FirstName.Length > 0 &&
                    this.LastName != null && this.LastName.Length > 0 &&
                    this.Email != null && this.Email.Length > 0) {
                        
                        using (var sc = new SchedulerContext()) {
                                sc.Persons.Attach(this);
                                sc.Entry(this).State = EntityState.Modified;
                                sc.SaveChanges();
                            }
                }
            }
        }

    }
}