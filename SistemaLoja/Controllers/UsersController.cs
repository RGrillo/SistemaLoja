using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SistemaLoja.Models;
using SistemaLoja.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaLoja.Controllers
{
    public class UsersController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Users
        public ActionResult Index()
        {
            //list user already registered on db
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            //list user already registered on db
            var users = userManager.Users.ToList();
            var usersView = new List<UserView>();
            
            foreach (var user in users)
            {
                var userView = new UserView
                {
                    Email = user.Email,
                    Nome = user.UserName,
                    UserId = user.Id

                };
                usersView.Add(userView);
            }
            return View(usersView);
        }

        public ActionResult Roles(string userId)
        {
            //list user already registered on db
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            //list user already registered on db
            var users = userManager.Users.ToList();


            //access to users roles
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var roles = roleManager.Roles.ToList();
            var rolesView = new List<RoleView>();

            var user = users.Find(u => u.Id == userId);

            foreach (var item in user.Roles)
            {
                var role = roles.Find(r => r.Id == item.RoleId);
                var roleView = new RoleView
                {
                    RoleId = role.Id,
                    Name = role.Name
                };

                rolesView.Add(roleView);

            }

            var userView = new UserView
            {
                Email = user.Email,
                Nome = user.UserName,
                UserId = user.Id, 
                Roles = rolesView

            };

            return View(userView);

        }



        //Close connection
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}