using Newtonsoft.Json;
using Scheduler.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Scheduler.Models {
    public class Role {
        public int ID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Need> Needs { get; set; }
        public virtual ICollection<XPersonRole> XPersonRoles { get; set; }

        public static List<Role> getAll() {
            List<Role> roles = new List<Role>();

            using (var sc = new SchedulerContext()) {
                roles = sc.Roles.ToList();
            }
            return roles;
        }

        public static Role getRoleByID(int roleID) {
            Role role = new Role();

            using (var sc = new SchedulerContext()) {
                role = sc.Roles.Where(r => r.ID == roleID).SingleOrDefault();
            }
            return role;
        }
    }
}