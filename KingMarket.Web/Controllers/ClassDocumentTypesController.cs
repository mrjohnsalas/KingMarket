using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;
using KingMarket.Model.Models;
using KingMarket.Web.ClassDocumentTypeService;
using PagedList;

namespace KingMarket.Web.Controllers
{
    // GET: ClassDocumentTypes
    public class ClassDocumentTypesController : Controller
    {
        // GET: ClassDocumentTypes
        [Authorize(Roles = "Admin")]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var proxy = new ClassDocumentTypeServiceClient();

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name_Desc" : String.Empty;

            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;

            ViewBag.CurrentFilter = searchString;

            var classDocumentTypes = sortOrder == "Name_Desc" ?
            String.IsNullOrEmpty(searchString) ?
            proxy.GetClassDocumentTypes().OrderByDescending(c => c.Name) :
            proxy.GetClassDocumentTypes().OrderByDescending(c => c.Name).Where(s => s.Name.Contains(searchString)) :
            String.IsNullOrEmpty(searchString) ?
            proxy.GetClassDocumentTypes().OrderBy(c => c.Name) :
            proxy.GetClassDocumentTypes().OrderBy(c => c.Name).Where(s => s.Name.Contains(searchString));

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(classDocumentTypes.ToPagedList(pageNumber, pageSize));
        }

        // GET: ClassDocumentTypes/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var proxy = new ClassDocumentTypeServiceClient();
            var classDocumentType = proxy.GetClassDocumentType(id.Value);
            if (classDocumentType == null)
                return HttpNotFound();
            return View(classDocumentType);
        }

        // GET: ClassDocumentTypes/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "ClassDocumentTypeId,Name")] ClassDocumentType classDocumentType)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var proxy = new ClassDocumentTypeServiceClient();
                    proxy.CreateClassDocumentType(classDocumentType);
                    return RedirectToAction("Index");
                }
            }
            catch (FaultException<GeneralException> ex)
            {
                ViewBag.ErrorCode = String.Format("Error Code: {0}", ex.Detail.Id);
                ViewBag.ErrorMessage = String.Format("Error Message: {0}", ex.Detail.Description);
            }
            return View(classDocumentType);
        }

        // GET: ClassDocumentTypes/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var proxy = new ClassDocumentTypeServiceClient();
            var classDocumentType = proxy.GetClassDocumentType(id.Value);
            if (classDocumentType == null)
                return HttpNotFound();
            return View(classDocumentType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "ClassDocumentTypeId,Name")] ClassDocumentType classDocumentType)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var proxy = new ClassDocumentTypeServiceClient();
                    proxy.EditClassDocumentType(classDocumentType);
                    return RedirectToAction("Index");
                }
            }
            catch (FaultException<GeneralException> ex)
            {
                ViewBag.ErrorCode = String.Format("Error Code: {0}", ex.Detail.Id);
                ViewBag.ErrorMessage = String.Format("Error Message: {0}", ex.Detail.Description);
            }
            return View(classDocumentType);
        }

        // GET: ClassDocumentTypes/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var proxy = new ClassDocumentTypeServiceClient();
            var classDocumentType = proxy.GetClassDocumentType(id.Value);
            if (classDocumentType == null)
                return HttpNotFound();
            return View(classDocumentType);
        }

        // POST: ClassDocumentTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            var proxy = new ClassDocumentTypeServiceClient();
            proxy.DeleteClassDocumentType(id);
            return RedirectToAction("Index");
        }
    }
}