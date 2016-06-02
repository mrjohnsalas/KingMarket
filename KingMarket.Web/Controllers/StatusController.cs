using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;
using KingMarket.Model.Models;
using KingMarket.Web.StatusService;
using PagedList;

namespace KingMarket.Web.Controllers
{
    // GET: Status
    public class StatusController : Controller
    {
        // GET: Status
        [Authorize(Roles = "Admin")]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var proxy = new StatusServiceClient();

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name_Desc" : String.Empty;

            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;

            ViewBag.CurrentFilter = searchString;

            var status = sortOrder == "Name_Desc" ?
                String.IsNullOrEmpty(searchString) ?
                proxy.GetStatuses().OrderByDescending(c => c.Name) :
                proxy.GetStatuses().OrderByDescending(c => c.Name).Where(s => s.Name.Contains(searchString)) :
                String.IsNullOrEmpty(searchString) ?
                proxy.GetStatuses().OrderBy(c => c.Name) :
                proxy.GetStatuses().OrderBy(c => c.Name).Where(s => s.Name.Contains(searchString));

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(status.ToPagedList(pageNumber, pageSize));
        }

        // GET: Status/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var proxy = new StatusServiceClient();
            var status = proxy.GetStatus(id.Value);
            if (status == null)
                return HttpNotFound();
            return View(status);
        }

        // GET: Status/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "StatusId,Name")] Status status)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var proxy = new StatusServiceClient();
                    proxy.CreateStatus(status);
                    return RedirectToAction("Index");
                }
            }
            catch (FaultException<GeneralException> ex)
            {
                ViewBag.ErrorCode = String.Format("Error Code: {0}", ex.Detail.Id);
                ViewBag.ErrorMessage = String.Format("Error Message: {0}", ex.Detail.Description);
            }

            return View(status);
        }

        // GET: Status/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var proxy = new StatusServiceClient();
            var status = proxy.GetStatus(id.Value);
            if (status == null)
                return HttpNotFound();
            return View(status);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "StatusId,Name")] Status status)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var proxy = new StatusServiceClient();
                    proxy.EditStatus(status);
                    return RedirectToAction("Index");
                }
            }
            catch (FaultException<GeneralException> ex)
            {
                ViewBag.ErrorCode = String.Format("Error Code: {0}", ex.Detail.Id);
                ViewBag.ErrorMessage = String.Format("Error Message: {0}", ex.Detail.Description);
            }
            return View(status);
        }

        // GET: Status/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var proxy = new StatusServiceClient();
            var status = proxy.GetStatus(id.Value);
            if (status == null)
                return HttpNotFound();
            return View(status);
        }

        // POST: Status/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            var proxy = new StatusServiceClient();
            try
            {
                proxy.DeleteStatus(id);
                return RedirectToAction("Index");
            }
            catch (FaultException<GeneralException> ex)
            {
                ViewBag.ErrorCode = String.Format("Error Code: {0}", ex.Detail.Id);
                ViewBag.ErrorMessage = String.Format("Error Message: {0}", ex.Detail.Description);
            }
            var status = proxy.GetStatus(id);
            if (status == null)
                return HttpNotFound();
            return View(status);
        }
    }
}