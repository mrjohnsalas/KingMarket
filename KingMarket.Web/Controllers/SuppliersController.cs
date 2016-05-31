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
using KingMarket.Web.SupplierService;
using KingMarket.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;

namespace KingMarket.Web.Controllers
{
    // GET: Suppliers
    public class SuppliersController : Controller
    {
        // GET: Suppliers
        [Authorize(Roles = "Admin")]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var proxy = new SupplierServiceClient();

            ViewBag.CurrentSort = sortOrder;
            ViewBag.BusinessNameSortParm = String.IsNullOrEmpty(sortOrder) ? "BusinessName_Desc" : String.Empty;
            ViewBag.DocumentTypeSortParm = sortOrder == "Document Type" ? "DocumentType_Desc" : "Document Type";
            ViewBag.DocumentNumberSortParm = sortOrder == "Document Number" ? "DocumentNumber_Desc" : "Document Number";
            ViewBag.PhoneSortParm = sortOrder == "Phone" ? "Phone_Desc" : "Phone";

            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;

            ViewBag.CurrentFilter = searchString;

            var suppliers = proxy.GetSuppliers();
            var proxyC = new DocumentTypeServiceClient();
            foreach (var item in suppliers)
                item.DocumentType = proxyC.GetDocumentType(item.DocumentTypeId);

            if (!String.IsNullOrEmpty(searchString))
                suppliers = suppliers.FindAll(s =>
                    s.DocumentNumber.Contains(searchString) ||
                    s.BusinessName.Contains(searchString) ||
                    s.Phone.Contains(searchString));

            switch (sortOrder)
            {
                case "BusinessName_Desc":
                    suppliers = suppliers.OrderByDescending(p => p.BusinessName).ToList();
                    break;
                case "Document Type":
                    suppliers = suppliers.OrderBy(p => p.DocumentType.Name).ToList();
                    break;
                case "DocumentType_Desc":
                    suppliers = suppliers.OrderByDescending(p => p.DocumentType.Name).ToList();
                    break;
                case "Document Number":
                    suppliers = suppliers.OrderBy(p => p.DocumentNumber).ToList();
                    break;
                case "DocumentNumber_Desc":
                    suppliers = suppliers.OrderByDescending(p => p.DocumentNumber).ToList();
                    break;
                case "Phone":
                    suppliers = suppliers.OrderBy(p => p.Phone).ToList();
                    break;
                case "Phone_Desc":
                    suppliers = suppliers.OrderByDescending(p => p.Phone).ToList();
                    break;
                default:
                    suppliers = suppliers.OrderBy(p => p.BusinessName).ToList();
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(suppliers.ToPagedList(pageNumber, pageSize));
        }

        // GET: Suppliers/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var proxy = new SupplierServiceClient();
            var supplier = proxy.GetSupplier(id.Value);
            if (supplier == null)
                return HttpNotFound();
            return View(supplier);
        }

        // GET: Suppliers/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var proxyC = new DocumentTypeServiceClient();
            ViewBag.DocumentTypeId = new SelectList(proxyC.GetDocumentTypes().OrderBy(d => d.Name).ToList().FindAll(d => d.ClassDocumentTypeId.Equals(1) && d.OnlyForEnterprise), "DocumentTypeId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "SupplierId,DocumentTypeId,DocumentNumber,BusinessName,Address,Email,Web,Phone")] Supplier supplier)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var proxy = new SupplierServiceClient();
                    proxy.CreateSupplier(supplier);

                    //Create User
                    var db2 = new ApplicationDbContext();
                    var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db2));
                    var user = userManager.FindByName(supplier.Email);
                    if (user == null)
                    {
                        user = new ApplicationUser
                        {
                            UserName = supplier.Email,
                            Email = supplier.Email
                        };
                        userManager.Create(user, supplier.DocumentNumber);
                        //AddRole
                        userManager.AddToRole(user.Id, "Supplier");
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
            var proxyC = new DocumentTypeServiceClient();
            ViewBag.DocumentTypeId = new SelectList(proxyC.GetDocumentTypes().OrderBy(d => d.Name).ToList().FindAll(d => d.ClassDocumentTypeId.Equals(1) && d.OnlyForEnterprise), "DocumentTypeId", "Name", supplier.DocumentTypeId);
            return View(supplier);
        }

        // GET: Suppliers/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var proxy = new SupplierServiceClient();
            var supplier = proxy.GetSupplier(id.Value);
            if (supplier == null)
                return HttpNotFound();
            var proxyC = new DocumentTypeServiceClient();
            ViewBag.DocumentTypeId = new SelectList(proxyC.GetDocumentTypes().OrderBy(d => d.Name).ToList().FindAll(d => d.ClassDocumentTypeId.Equals(1) && d.OnlyForEnterprise), "DocumentTypeId", "Name", supplier.DocumentTypeId);
            return View(supplier);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "SupplierId,DocumentTypeId,DocumentNumber,BusinessName,Address,Email,Web,Phone")] Supplier supplier)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var proxy = new SupplierServiceClient();
                    proxy.EditSupplier(supplier);
                    return RedirectToAction("Index");
                }
            }
            catch (FaultException<GeneralException> ex)
            {
                ViewBag.ErrorCode = String.Format("Error Code: {0}", ex.Detail.Id);
                ViewBag.ErrorMessage = String.Format("Error Message: {0}", ex.Detail.Description);
            }
            var proxyC = new DocumentTypeServiceClient();
            ViewBag.DocumentTypeId = new SelectList(proxyC.GetDocumentTypes().OrderBy(d => d.Name).ToList().FindAll(d => d.ClassDocumentTypeId.Equals(1) && d.OnlyForEnterprise), "DocumentTypeId", "Name", supplier.DocumentTypeId);
            return View(supplier);
        }

        // GET: Suppliers/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var proxy = new SupplierServiceClient();
            var supplier = proxy.GetSupplier(id.Value);
            if (supplier == null)
                return HttpNotFound();
            return View(supplier);
        }

        // POST: Suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            var proxy = new SupplierServiceClient();
            var supplier = proxy.GetSupplier(id);
            proxy.DeleteSupplier(id);

            var db2 = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db2));
            var user = userManager.FindByName(supplier.Email);
            userManager.Delete(user);
            db2.Dispose();

            return RedirectToAction("Index");
        }
    }
}