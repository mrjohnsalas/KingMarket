using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;
using KingMarket.Model.Models;
using KingMarket.Web.DocumentTypeService;
using KingMarket.Web.EmployeeTypeService;
using KingMarket.Web.EmployeeService;
using KingMarket.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;

namespace KingMarket.Web.Controllers
{
    public class EmployeesController : Controller
    {
        // GET: Employees
        [Authorize(Roles = "Admin")]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var proxy = new EmployeeServiceClient();

            ViewBag.CurrentSort = sortOrder;
            ViewBag.FirstNameSortParm = String.IsNullOrEmpty(sortOrder) ? "FirstName_Desc" : String.Empty;
            ViewBag.DocumentTypeSortParm = sortOrder == "Document Type" ? "DocumentType_Desc" : "Document Type";
            ViewBag.EmployeeTypeSortParm = sortOrder == "Employee Type" ? "EmployeeType_Desc" : "Employee Type";
            ViewBag.DocumentNumberSortParm = sortOrder == "Document Number" ? "DocumentNumber_Desc" : "Document Number";
            ViewBag.LastNameSortParm = sortOrder == "Last Name" ? "LastName_Desc" : "Last Name";
            ViewBag.SecondLastNameSortParm = sortOrder == "Second Last Name" ? "SecondLastName_Desc" : "Second Last Name";
            ViewBag.EmailSortParm = sortOrder == "Email" ? "Email_Desc" : "Email";
            ViewBag.PhoneSortParm = sortOrder == "Phone" ? "Phone_Desc" : "Phone";

            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;

            ViewBag.CurrentFilter = searchString;

            var employees = proxy.GetEmployees();
            var proxyD = new DocumentTypeServiceClient();
            var proxyE = new EmployeeTypeServiceClient();
            foreach (var item in employees)
            {
                item.DocumentType = proxyD.GetDocumentType(item.DocumentTypeId);
                item.EmployeeType = proxyE.GetEmployeeType(item.EmployeeTypeId);
            }

            if (!String.IsNullOrEmpty(searchString))
                employees = employees.FindAll(s =>
                    s.EmployeeType.Name.Contains(searchString) ||
                    s.DocumentNumber.Contains(searchString) ||
                    s.FirstName.Contains(searchString) ||
                    s.LastName.Contains(searchString) ||
                    s.SecondLastName.Contains(searchString) ||
                    s.Email.Contains(searchString) ||
                    s.Phone.Contains(searchString));

            switch (sortOrder)
            {
                case "FirstName_Desc":
                    employees = employees.OrderByDescending(p => p.FirstName).ToList();
                    break;
                case "DocumentType_Desc":
                    employees = employees.OrderByDescending(p => p.DocumentType.Name).ToList();
                    break;
                case "Document Type":
                    employees = employees.OrderBy(p => p.DocumentType.Name).ToList();
                    break;
                case "EmployeeType_Desc":
                    employees = employees.OrderByDescending(p => p.EmployeeType.Name).ToList();
                    break;
                case "Employee Type":
                    employees = employees.OrderBy(p => p.EmployeeType.Name).ToList();
                    break;
                case "DocumentNumber_Desc":
                    employees = employees.OrderByDescending(p => p.DocumentNumber).ToList();
                    break;
                case "Document Number":
                    employees = employees.OrderBy(p => p.DocumentNumber).ToList();
                    break;
                case "LastName_Desc":
                    employees = employees.OrderByDescending(p => p.LastName).ToList();
                    break;
                case "Last Name":
                    employees = employees.OrderBy(p => p.LastName).ToList();
                    break;
                case "Second Last Name":
                    employees = employees.OrderBy(p => p.SecondLastName).ToList();
                    break;
                case "SecondLastName_Desc":
                    employees = employees.OrderByDescending(p => p.SecondLastName).ToList();
                    break;
                case "Email":
                    employees = employees.OrderBy(p => p.Email).ToList();
                    break;
                case "Email_Desc":
                    employees = employees.OrderByDescending(p => p.Email).ToList();
                    break;
                case "Phone":
                    employees = employees.OrderBy(p => p.Phone).ToList();
                    break;
                case "Phone_Desc":
                    employees = employees.OrderByDescending(p => p.Phone).ToList();
                    break;
                default:
                    employees = employees.OrderBy(p => p.FirstName).ToList();
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(employees.ToPagedList(pageNumber, pageSize));
        }

        // GET: Employees/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var proxy = new EmployeeServiceClient();
            var employee = proxy.GetEmployee(id.Value);
            if (employee == null)
                return HttpNotFound();
            return View(employee);
        }

        // GET: Employees/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var proxyD = new DocumentTypeServiceClient();
            var proxyE = new EmployeeTypeServiceClient();
            ViewBag.DocumentTypeId = new SelectList(proxyD.GetDocumentTypes().OrderBy(d => d.Name).ToList().FindAll(d => d.ClassDocumentTypeId.Equals(1) && !d.OnlyForEnterprise), "DocumentTypeId", "Name");
            ViewBag.EmployeeTypeId = new SelectList(proxyE.GetEmployeeTypes().OrderBy(d => d.Name), "EmployeeTypeId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "EmployeeId,DocumentTypeId,EmployeeTypeId,DocumentNumber,FirstName,LastName,SecondLastName,Email,Phone")] Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var proxy = new EmployeeServiceClient();
                    proxy.CreateEmployee(employee);

                    //Create User
                    var db2 = new ApplicationDbContext();
                    var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db2));
                    var user = userManager.FindByName(employee.Email);
                    if (user == null)
                    {
                        user = new ApplicationUser
                        {
                            UserName = employee.Email,
                            Email = employee.Email
                        };
                        userManager.Create(user, employee.DocumentNumber);
                        //AddRole
                        if (employee.EmployeeTypeId.Equals(1))
                            userManager.AddToRole(user.Id, "Buyer");
                        else
                            userManager.AddToRole(user.Id, "Grocer");
                    }
                    db2.Dispose();
                    return RedirectToAction("Index");
                }
            }
            catch (FaultException<GeneralException> ex)
            {
                ViewBag.ErrorCode = String.Format("Error Code: {0}", ex.Detail.Id);
                ViewBag.ErrorMessage = String.Format("Error Message: {0}", ex.Detail.Description);
            }
            var proxyD = new DocumentTypeServiceClient();
            var proxyE = new EmployeeTypeServiceClient();
            ViewBag.DocumentTypeId = new SelectList(proxyD.GetDocumentTypes().OrderBy(d => d.Name).ToList().FindAll(d => d.ClassDocumentTypeId.Equals(1) && !d.OnlyForEnterprise), "DocumentTypeId", "Name", employee.DocumentTypeId);
            ViewBag.EmployeeTypeId = new SelectList(proxyE.GetEmployeeTypes().OrderBy(d => d.Name), "EmployeeTypeId", "Name", employee.EmployeeTypeId);
            return View(employee);
        }

        // GET: Employees/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var proxy = new EmployeeServiceClient();
            var employee = proxy.GetEmployee(id.Value);
            if (employee == null)
                return HttpNotFound();
            var proxyD = new DocumentTypeServiceClient();
            var proxyE = new EmployeeTypeServiceClient();
            ViewBag.DocumentTypeId = new SelectList(proxyD.GetDocumentTypes().OrderBy(d => d.Name).ToList().FindAll(d => d.ClassDocumentTypeId.Equals(1) && !d.OnlyForEnterprise), "DocumentTypeId", "Name", employee.DocumentTypeId);
            ViewBag.EmployeeTypeId = new SelectList(proxyE.GetEmployeeTypes().OrderBy(d => d.Name), "EmployeeTypeId", "Name", employee.EmployeeTypeId);
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "EmployeeId,DocumentTypeId,EmployeeTypeId,DocumentNumber,FirstName,LastName,SecondLastName,Email,Phone")] Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var proxy = new EmployeeServiceClient();
                    proxy.EditEmployee(employee);

                    //Update Role
                    var db2 = new ApplicationDbContext();
                    var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db2));
                    var user = userManager.FindByName(employee.Email);
                    if (user != null)
                    {
                        if (employee.EmployeeTypeId.Equals(1))
                        {
                            if (userManager.IsInRole(user.Id, "Grocer"))
                                userManager.RemoveFromRole(user.Id, "Grocer");
                            if (!userManager.IsInRole(user.Id, "Buyer"))
                                userManager.AddToRole(user.Id, "Buyer");
                        }
                        else
                        {
                            if (userManager.IsInRole(user.Id, "Buyer"))
                                userManager.RemoveFromRole(user.Id, "Buyer");
                            if (!userManager.IsInRole(user.Id, "Grocer"))
                                userManager.AddToRole(user.Id, "Grocer");
                        }
                    }
                    db2.Dispose();
                    return RedirectToAction("Index");
                }
            }
            catch (FaultException<GeneralException> ex)
            {
                ViewBag.ErrorCode = String.Format("Error Code: {0}", ex.Detail.Id);
                ViewBag.ErrorMessage = String.Format("Error Message: {0}", ex.Detail.Description);
            }
            var proxyD = new DocumentTypeServiceClient();
            var proxyE = new EmployeeTypeServiceClient();
            ViewBag.DocumentTypeId = new SelectList(proxyD.GetDocumentTypes().OrderBy(d => d.Name).ToList().FindAll(d => d.ClassDocumentTypeId.Equals(1) && !d.OnlyForEnterprise), "DocumentTypeId", "Name", employee.DocumentTypeId);
            ViewBag.EmployeeTypeId = new SelectList(proxyE.GetEmployeeTypes().OrderBy(d => d.Name), "EmployeeTypeId", "Name", employee.EmployeeTypeId);
            return View(employee);
        }

        // GET: Employees/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var proxy = new EmployeeServiceClient();
            var employee = proxy.GetEmployee(id.Value);
            if (employee == null)
                return HttpNotFound();
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            var proxy = new EmployeeServiceClient();
            var employee = proxy.GetEmployee(id);
            proxy.DeleteEmployee(id);

            var db2 = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db2));
            var user = userManager.FindByName(employee.Email);
            userManager.Delete(user);
            db2.Dispose();

            return RedirectToAction("Index");
        }
    }
}