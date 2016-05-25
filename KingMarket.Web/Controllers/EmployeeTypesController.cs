using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KingMarket.Model.Models;
using KingMarket.Web.EmployeeTypeService;
using PagedList;

namespace KingMarket.Web.Controllers
{
    // GET: EmployeeTypes
    public class EmployeeTypesController : Controller
    {
        // GET: EmployeeTypes
        [Authorize(Roles = "Admin")]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var proxy = new EmployeeTypeServiceClient();

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name_Desc" : String.Empty;

            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;

            ViewBag.CurrentFilter = searchString;

            var employeeTypes = sortOrder == "Name_Desc" ?
                String.IsNullOrEmpty(searchString) ?
                proxy.GetEmployeeTypes().OrderByDescending(c => c.Name) :
                proxy.GetEmployeeTypes().OrderByDescending(c => c.Name).Where(s => s.Name.Contains(searchString)) :
                String.IsNullOrEmpty(searchString) ?
                proxy.GetEmployeeTypes().OrderBy(c => c.Name) :
                proxy.GetEmployeeTypes().OrderBy(c => c.Name).Where(s => s.Name.Contains(searchString));

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(employeeTypes.ToPagedList(pageNumber, pageSize));
        }

        // GET: EmployeeTypes/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var proxy = new EmployeeTypeServiceClient();
            var employeeType = proxy.GetEmployeeType(id.Value);
            if (employeeType == null)
                return HttpNotFound();
            return View(employeeType);
        }

        // GET: EmployeeTypes/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "EmployeeTypeId,Name")] EmployeeType employeeType)
        {
            if (ModelState.IsValid)
            {
                var proxy = new EmployeeTypeServiceClient();
                proxy.CreateEmployeeType(employeeType);
                return RedirectToAction("Index");
            }

            return View(employeeType);
        }

        // GET: EmployeeTypes/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var proxy = new EmployeeTypeServiceClient();
            var employeeType = proxy.GetEmployeeType(id.Value);
            if (employeeType == null)
                return HttpNotFound();
            return View(employeeType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "EmployeeTypeId,Name")] EmployeeType employeeType)
        {
            if (ModelState.IsValid)
            {
                var proxy = new EmployeeTypeServiceClient();
                proxy.EditEmployeeType(employeeType);
                return RedirectToAction("Index");
            }
            return View(employeeType);
        }

        // GET: EmployeeTypes/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var proxy = new EmployeeTypeServiceClient();
            var employeeType = proxy.GetEmployeeType(id.Value);
            if (employeeType == null)
                return HttpNotFound();
            return View(employeeType);
        }

        // POST: EmployeeTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            var proxy = new EmployeeTypeServiceClient();
            proxy.DeleteEmployeeType(id);
            return RedirectToAction("Index");
        }
    }
}