using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using KingMarket.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace KingMarket.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var db = new ApplicationDbContext();
            CreateRoles(db);
            CreateSuperUser(db);
            AddPermisionsToSuperUser(db);
            db.Dispose();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void CreateRoles(ApplicationDbContext db)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            if (!roleManager.RoleExists("Admin"))
                roleManager.Create(new IdentityRole("Admin"));
            if (!roleManager.RoleExists("Customer"))
                roleManager.Create(new IdentityRole("Customer"));
            if (!roleManager.RoleExists("Supplier"))
                roleManager.Create(new IdentityRole("Supplier"));
            if (!roleManager.RoleExists("Buyer"))
                roleManager.Create(new IdentityRole("Buyer"));
            if (!roleManager.RoleExists("Grocer"))
                roleManager.Create(new IdentityRole("Grocer"));
            if (!roleManager.RoleExists("View"))
                roleManager.Create(new IdentityRole("View"));
            if (!roleManager.RoleExists("Edit"))
                roleManager.Create(new IdentityRole("Edit"));
            if (!roleManager.RoleExists("Create"))
                roleManager.Create(new IdentityRole("Create"));
            if (!roleManager.RoleExists("Delete"))
                roleManager.Create(new IdentityRole("Delete"));
        }

        private void CreateSuperUser(ApplicationDbContext db)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var user = userManager.FindByName("manager@kingmarket.com");
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = "manager@kingmarket.com",
                    Email = "manager@kingmarket.com"
                };
                userManager.Create(user, "M@n123456");
            }
        }

        private void AddPermisionsToSuperUser(ApplicationDbContext db)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var user = userManager.FindByName("manager@kingmarket.com");
            if (!userManager.IsInRole(user.Id, "Admin"))
                userManager.AddToRole(user.Id, "Admin");
        }
    }
}
