using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KingMarket.Model.Models;
using KingMarket.Web.DocumentTypeService;
using KingMarket.Web.ClassDocumentTypeService;
using PagedList;

namespace KingMarket.Web.Controllers
{
    public class DocumentTypesController : Controller
    {
        // GET: DocumentTypes
        [Authorize(Roles = "Admin")]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var proxy = new DocumentTypeServiceClient();

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name_Desc" : String.Empty;
            ViewBag.ClassDocumentSortParm = sortOrder == "Class Document" ? "ClassDocument_Desc" : "Class Document";
            ViewBag.OnlyForEnterpriseSortParm = sortOrder == "Only For Enterprise?" ? "OnlyForEnterprise_Desc" : "Only For Enterprise?";

            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;

            ViewBag.CurrentFilter = searchString;

            var documentTypes = proxy.GetDocumentTypes();
            var proxyC = new ClassDocumentTypeServiceClient();
            foreach (var item in documentTypes)
                item.ClassDocumentType = proxyC.GetClassDocumentType(item.ClassDocumentTypeId);

            if (!String.IsNullOrEmpty(searchString))
                documentTypes = documentTypes.FindAll(s => s.ClassDocumentType.Name.Contains(searchString) || s.Name.Contains(searchString));

            switch (sortOrder)
            {
                case "Name_Desc":
                    documentTypes = documentTypes.OrderByDescending(p => p.Name).ToList();
                    break;
                case "Class Document":
                    documentTypes = documentTypes.OrderBy(p => p.ClassDocumentType.Name).ToList();
                    break;
                case "ClassDocument_Desc":
                    documentTypes = documentTypes.OrderByDescending(p => p.ClassDocumentType.Name).ToList();
                    break;
                case "Only For Enterprise?":
                    documentTypes = documentTypes.OrderBy(p => p.OnlyForEnterprise).ToList();
                    break;
                case "OnlyForEnterprise_Desc":
                    documentTypes = documentTypes.OrderByDescending(p => p.OnlyForEnterprise).ToList();
                    break;
                default:
                    documentTypes = documentTypes.OrderBy(p => p.Name).ToList();
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(documentTypes.ToPagedList(pageNumber, pageSize));
        }

        // GET: DocumentTypes/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var proxy = new DocumentTypeServiceClient();
            var documentType = proxy.GetDocumentType(id.Value);
            if (documentType == null)
                return HttpNotFound();
            return View(documentType);
        }

        // GET: DocumentTypes/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var proxyC = new ClassDocumentTypeServiceClient();
            ViewBag.ClassDocumentTypeId = new SelectList(proxyC.GetClassDocumentTypes().OrderBy(d => d.Name), "ClassDocumentTypeId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "DocumentTypeId,Name,OnlyForEnterprise,ClassDocumentTypeId")] DocumentType documentType)
        {
            if (ModelState.IsValid)
            {
                var proxy = new DocumentTypeServiceClient();
                proxy.CreateDocumentType(documentType);
                return RedirectToAction("Index");
            }

            var proxyC = new ClassDocumentTypeServiceClient();
            ViewBag.ClassDocumentTypeId = new SelectList(proxyC.GetClassDocumentTypes().OrderBy(d => d.Name), "ClassDocumentTypeId", "Name", documentType.ClassDocumentTypeId);
            return View(documentType);
        }

        // GET: DocumentTypes/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var proxy = new DocumentTypeServiceClient();
            var documentType = proxy.GetDocumentType(id.Value);
            if (documentType == null)
                return HttpNotFound();
            var proxyC = new ClassDocumentTypeServiceClient();
            ViewBag.ClassDocumentTypeId = new SelectList(proxyC.GetClassDocumentTypes().OrderBy(d => d.Name), "ClassDocumentTypeId", "Name", documentType.ClassDocumentTypeId);
            return View(documentType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "DocumentTypeId,Name,OnlyForEnterprise,ClassDocumentTypeId")] DocumentType documentType)
        {
            if (ModelState.IsValid)
            {
                var proxy = new DocumentTypeServiceClient();
                proxy.EditDocumentType(documentType);
                return RedirectToAction("Index");
            }
            var proxyC = new ClassDocumentTypeServiceClient();
            ViewBag.ClassDocumentTypeId = new SelectList(proxyC.GetClassDocumentTypes().OrderBy(d => d.Name), "ClassDocumentTypeId", "Name", documentType.ClassDocumentTypeId);
            return View(documentType);
        }

        // GET: DocumentTypes/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var proxy = new DocumentTypeServiceClient();
            var documentType = proxy.GetDocumentType(id.Value);
            if (documentType == null)
                return HttpNotFound();
            return View(documentType);
        }

        // POST: DocumentTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            var proxy = new DocumentTypeServiceClient();
            proxy.DeleteDocumentType(id);
            return RedirectToAction("Index");
        }
    }
}