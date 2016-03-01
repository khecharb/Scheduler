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
        }
    }
}