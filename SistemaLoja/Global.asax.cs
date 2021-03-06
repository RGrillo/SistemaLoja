﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SistemaLoja.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace SistemaLoja
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //Auto migration command
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<Models.SistemaLojaContext, Migrations.Configuration>());
            // Reference to default connection
            ApplicationDbContext db = new ApplicationDbContext();
            // Metodo para criar roles
            CreiarRoles(db);
            CriarSuperUser(db);
            AddPremissoesSuperUser(db);
            db.Dispose();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void AddPremissoesSuperUser(ApplicationDbContext db)
        {
            //add roles to users
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var user = userManager.FindByName("admin@admin.com");
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            if (!userManager.IsInRole(user.Id, "View"))
            {
                userManager.AddToRole(user.Id, "View");
            }

            if (!userManager.IsInRole(user.Id, "Create"))
            {
                userManager.AddToRole(user.Id, "Create");
            }

            if (!userManager.IsInRole(user.Id, "Edit"))
            {
                userManager.AddToRole(user.Id, "Edit");
            }

            if (!userManager.IsInRole(user.Id, "Delete"))
            {
                userManager.AddToRole(user.Id, "Delete");
            }
        }

        private void CriarSuperUser(ApplicationDbContext db)
        {
            //create super user
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var user = userManager.FindByName("admin@admin.com");

            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = "admin@admin.com", 
                    Email = "admin@admin.com"
                };

                userManager.Create(user, "ZAQ!xsw4321");
            }
        }

        private void CreiarRoles(ApplicationDbContext db)
        {
            //create roles using default connection
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            //will verify if exist those methods, if not will create
            if (!roleManager.RoleExists("View"))
            {
                roleManager.Create(new IdentityRole("View"));
            }

            if (!roleManager.RoleExists("Create"))
            {
                roleManager.Create(new IdentityRole("Create"));
            }

            if (!roleManager.RoleExists("Edit"))
            {
                roleManager.Create(new IdentityRole("Edit"));
            }

            if (!roleManager.RoleExists("Delete"))
            {
                roleManager.Create(new IdentityRole("Delete"));
            }

        }
    }
}
