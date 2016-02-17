using Scheduler.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Scheduler.DAL {
    public class SchedulerContext : DbContext {

        public SchedulerContext(): base("SchedulerContext") {
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Need> Needs { get; set; }
        public DbSet<XPersonRole> XPersonRoles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}