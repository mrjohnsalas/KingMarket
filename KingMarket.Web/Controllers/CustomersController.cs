using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KingMarket.Model.Models;
using KingMarket.Web.DocumentTypeService;
using KingMarket.Web.CustomerService;
using KingMarket.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;

namespace KingMarket.Web.Controllers
{
    // GET: Customers
    public class CustomersController : Controller
    {
        // GET: Customers
        [Authorize(Roles = "Admin")]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var proxy = new CustomerServiceClient();

            ViewBag.CurrentSort = sortOrder;
            ViewBag.BusinessNameSortParm = String.IsNullOrEmpty(sortOrder) ? "BusinessName_Desc" : String.Empty;
            ViewBag.DocumentTypeSortParm = sortOrder == "Document Type" ? "DocumentType_Desc" : "Document Type";
            ViewBag.DocumentNumberSortParm = sortOrder == "Document Number" ? "DocumentNumber_Desc" : "Document Number";
            ViewBag.FirstNameSortParm = sortOrder == "First Name" ? "FirstName_Desc" : "First Name";
            ViewBag.LastNameSortParm = sortOrder == "Last Name" ? "LastName_Desc" : "Last Name";
            ViewBag.SecondLastNameSortParm = sortOrder == "Second Last Name" ? "SecondLastName_Desc" : "Second Last Name";
            ViewBag.PhoneSortParm = sortOrder == "Phone" ? "Phone_Desc" : "Phone";

            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;

            ViewBag.CurrentFilter = searchString;

            var customers = proxy.GetCustomers();
            var proxyC = new DocumentTypeServiceClient();
            foreach (var item in customers)
                item.DocumentType = proxyC.GetDocumentType(item.DocumentTypeId);

            if (!String.IsNullOrEmpty(searchString))
                customers = customers.FindAll(s =>
                    s.DocumentType.Name.Contains(searchString) ||
                    s.DocumentNumber.Contains(searchString) ||
                    s.BusinessName.Contains(searchString) ||
                    s.FirstName.Contains(searchString) ||
                    s.LastName.Contains(searchString) ||
                    s.SecondLastName.Contains(searchString) ||
                    s.Phone.Contains(searchString));

            switch (sortOrder)
            {
                case "BusinessName_Desc":
                    customers = customers.OrderByDescending(p => p.BusinessName).ToList();
                    break;
                case "Document Type":
                    customers = customers.OrderBy(p => p.DocumentType.Name).ToList();
                    break;
                case "DocumentType_Desc":
                    customers = customers.OrderByDescending(p => p.DocumentType.Name).ToList();
                    break;
                case "Document Number":
                    customers = customers.OrderBy(p => p.DocumentNumber).ToList();
                    break;
                case "DocumentNumber_Desc":
                    customers = customers.OrderByDescending(p => p.DocumentNumber).ToList();
                    break;
                case "First Name":
                    customers = customers.OrderBy(p => p.FirstName).ToList();
                    break;
                case "FirstName_Desc":
                    customers = customers.OrderByDescending(p => p.FirstName).ToList();
                    break;
                case "Last Name":
                    customers = customers.OrderBy(p => p.LastName).ToList();
                    break;
                case "LastName_Desc":
                    customers = customers.OrderByDescending(p => p.LastName).ToList();
                    break;
                case "Second Last Name":
                    customers = customers.OrderBy(p => p.SecondLastName).ToList();
                    break;
                case "SecondLastName_Desc":
                    customers = customers.OrderByDescending(p => p.SecondLastName).ToList();
                    break;
                case "Phone":
                    customers = customers.OrderBy(p => p.Phone).ToList();
                    break;
                case "Phone_Desc":
                    customers = customers.OrderByDescending(p => p.Phone).ToList();
                    break;
                default:
                    customers = customers.OrderBy(p => p.BusinessName).ToList();
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(customers.ToPagedList(pageNumber, pageSize));
        }

        // GET: Customers/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var proxy = new CustomerServiceClient();
            var customer = proxy.GetCustomer(id.Value);
            if (customer == null)
                return HttpNotFound();
            return View(customer);
        }

        // GET: Customers/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var proxyC = new DocumentTypeServiceClient();
            ViewBag.DocumentTypeId = new SelectList(proxyC.GetDocumentTypes().OrderBy(d => d.Name).ToList().FindAll(d => d.ClassDocumentTypeId.Equals(1)), "DocumentTypeId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "CustomerId,DocumentTypeId,DocumentNumber,BusinessName,FirstName,LastName,SecondLastName,Address,Email,Web,Phone")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                var proxy = new CustomerServiceClient();
                proxy.CreateCustomer(customer);

                //Create User
                var db2 = new ApplicationDbContext();
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db2));
                var user = userManager.FindByName(customer.Email);
                if (user == null)
                {
                    user = new ApplicationUser
                    {
                        UserName = customer.Email,
                        Email = customer.Email
                    };
                    userManager.Create(user, customer.DocumentNumber);
                    //AddRole
                    userManager.AddToRole(user.Id, "Customer");
                }
                db2.Dispose();
                return RedirectToAction("Index");
            }

            var proxyC = new DocumentTypeServiceClient();
            ViewBag.DocumentTypeId = new SelectList(proxyC.GetDocumentTypes().OrderBy(d => d.Name).ToList().FindAll(d => d.ClassDocumentTypeId.Equals(1)), "DocumentTypeId", "Name", customer.DocumentTypeId);
            return View(customer);
        }

        // GET: Customers/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var proxy = new CustomerServiceClient();
            var customer = proxy.GetCustomer(id.Value);
            if (customer == null)
                return HttpNotFound();
            var proxyC = new DocumentTypeServiceClient();
            ViewBag.DocumentTypeId = new SelectList(proxyC.GetDocumentTypes().OrderBy(d => d.Name).ToList().FindAll(d => d.ClassDocumentTypeId.Equals(1)), "DocumentTypeId", "Name", customer.DocumentTypeId);
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "CustomerId,DocumentTypeId,DocumentNumber,BusinessName,FirstName,LastName,SecondLastName,Address,Email,Web,Phone")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                var proxy = new CustomerServiceClient();
                proxy.EditCustomer(customer);
                return RedirectToAction("Index");
            }
            var proxyC = new DocumentTypeServiceClient();
            ViewBag.DocumentTypeId = new SelectList(proxyC.GetDocumentTypes().OrderBy(d => d.Name).ToList().FindAll(d => d.ClassDocumentTypeId.Equals(1)), "DocumentTypeId", "Name", customer.DocumentTypeId);
            return View(customer);
        }

        // GET: Customers/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var proxy = new CustomerServiceClient();
            var customer = proxy.GetCustomer(id.Value);
            if (customer == null)
                return HttpNotFound();
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            var proxy = new CustomerServiceClient();
            var customer = proxy.GetCustomer(id);
            proxy.DeleteCustomer(id);

            var db2 = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db2));
            var user = userManager.FindByName(customer.Email);
            userManager.Delete(user);
            db2.Dispose();

            return RedirectToAction("Index");
        }
    }
}