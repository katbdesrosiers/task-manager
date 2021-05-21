using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManager.Models
{
    public class Membership
    {
        private static ApplicationDbContext db = new ApplicationDbContext();

        private static RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(
            new RoleStore<IdentityRole>(db)
        );

        private static UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(
            new UserStore<ApplicationUser>(db)
        );

        public static IEnumerable<string> GetAllRoles()
        {
            return roleManager.Roles.Select(r => r.Name).ToList();
        }

        public static bool CreateUser(ApplicationUser user, string password)
        {
            var result = userManager.Create(user, password);
            return result.Succeeded;
        }

        //Check if user is in a role
        public static bool UserInRole(string userId, string role)
        {
            return userManager.IsInRole(userId, role);
        }

        // Add User To Role
        public static bool AddUserToRole(string userId, string role)
        {
            if (UserInRole(userId, role))
                return false;

            userManager.AddToRole(userId, role);
            return true;
        }

        // Remove User From Role
        public static bool RemoveUserFromRole(string userId, string role)
        {
            if (!UserInRole(userId, role))
                return false;

            userManager.RemoveFromRole(userId, role);
            return true;
        }

        // AddRole
        public static void AddRole(string role)
        {
            if (!roleManager.RoleExists(role))
                roleManager.Create(new IdentityRole { Name = role });
        }

        // RemoveRole
        public static void RemoveRole(string role)
        {
            if (roleManager.RoleExists(role))
                roleManager.Delete(roleManager.FindByName(role));
        }

        // Get all roles of user
        public static IEnumerable<string> GetAllRolesOfUser(string userId)
        {
            return userManager.GetRoles(userId);
        }
    }
}