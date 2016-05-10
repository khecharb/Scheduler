using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Scheduler.Models;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using Scheduler.DAL;

namespace Scheduler
{
    public class BulkUpload
    {
        private SchedulerContext db = new SchedulerContext();

        public void Upload (string path, TextFieldParser parser, string EntityType)
        {
            Event eve = new Event();
            Person person = new Person();
            Assignment assignment = new Assignment();
            Role role = new Role();

            switch (EntityType)
            {
                case "Event":
                    {
                        while (!parser.EndOfData)
                        {
                            //Process row
                            string[] fields = parser.ReadFields();

                            eve.Name = fields[0];
                            eve.Room = fields[1];
                            eve.EventDate = Convert.ToDateTime(fields[2]);
                            eve.StartTime = DateTime.ParseExact(fields[3], "HH:mm", System.Globalization.CultureInfo.CurrentCulture);
                            eve.EndTime = DateTime.ParseExact(fields[4], "HH:mm", System.Globalization.CultureInfo.CurrentCulture);

                            Event SameEve = db.Events.Where(i => i.Name == eve.Name).FirstOrDefault();

                            if (SameEve == null)
                            {
                                db.Events.Add(eve);
                                db.SaveChanges();
                            }
                            /*else if (SameEve.Name == eve.Name)
                            {
                                db.Events.Attach(SameEve);
                                SameEve.Quantity += ingredient.Quantity;
                                db.SaveChanges();
                            }*/
                        }
                        parser.Close();
                        break;
                    }
                case "Person":
                    {
                        while (!parser.EndOfData)
                        {
                            string[] fields = parser.ReadFields();

                            person.FirstName = fields[0];
                            person.LastName = fields[1];
                            person.Email = fields[2];

                            Person SamePerson = db.Persons.Where(i => i.FirstName == person.FirstName && i.LastName == person.LastName).FirstOrDefault();

                            if(SamePerson == null)
                            {
                                db.Persons.Add(person);
                                db.SaveChanges();
                            }
                            //else if (SamePerson.LastName == person.LastName && SamePerson.FirstName == person.FirstName)
                            //{
                            //    db.SaveChanges();
                            //}
                        }
                        parser.Close();
                        break;
                    }
                case "Assignment":
                    {
                        while (!parser.EndOfData)
                        {
                            string[] fields = parser.ReadFields();

                            assignment.Event.Name = fields[0];
                            assignment.Person.FirstName = fields[1];
                            assignment.Role.Name = fields[2];
                            assignment.Event.StartTime = DateTime.ParseExact(fields[3], "HH:mm", System.Globalization.CultureInfo.CurrentCulture);
                            assignment.Event.EndTime = DateTime.ParseExact(fields[4], "HH:mm", System.Globalization.CultureInfo.CurrentCulture);

                            Assignment SameAssignment = db.Assignments.Where(i => i.Event.Name == assignment.Event.Name && i.Person.FirstName == assignment.Person.FirstName && i.Role.Name == assignment.Role.Name).FirstOrDefault();

                            if(SameAssignment == null)
                            {
                                db.Assignments.Add(assignment);
                                db.SaveChanges();
                            }
                            else if (SameAssignment.Event.Name == assignment.Event.Name && SameAssignment.Person.FirstName == assignment.Person.FirstName && SameAssignment.Role.Name == assignment.Role.Name)
                            {
                                db.SaveChanges();
                            }
                        }
                        parser.Close();
                        break;
                    }
                case "Role":
                    {
                        while (!parser.EndOfData)
                        {
                            string[] fields = parser.ReadFields();

                            role.Name = fields[0];

                            Role SameRole = db.Roles.Where(i => i.Name == role.Name).FirstOrDefault();

                            if(SameRole == null)
                            {
                                db.Roles.Add(role);
                                db.SaveChanges();
                            }
                            else if(SameRole.Name == role.Name)
                            {
                                db.SaveChanges();
                            }
                        }
                        parser.Close();
                        break;
                    }
            }

           
        }
    }
}