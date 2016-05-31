using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;
using KingMarket.Model.Models;
using KingMarket.Web.SupplierService;
using KingMarket.Web.SupplierContactService;
using KingMarket.Web.DocumentTypeService;
using PagedList;

namespace KingMarket.Web.Controllers
{
    public class SupplierContactsController : Controller
    {
        // GET: SupplierContacts
        [Authorize(Roles = "Admin")]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var proxy = new SupplierContactServiceClient();

            ViewBag.CurrentSort = sortOrder;
            ViewBag.BusinessNameSortParm = String.IsNullOrEmpty(sortOrder) ? "BusinessName_Desc" : String.Empty;
            ViewBag.DocumentTypeSortParm = sortOrder == "Document Type" ? "DocumentType_Desc" : "Document Type";
            ViewBag.DocumentNumberSortParm = sortOrder == "Document Number" ? "DocumentNumber_Desc" : "Document Number";
            ViewBag.FirstNameSortParm = sortOrder == "First Name" ? "FirstName_Desc" : "First Name";
            ViewBag.LastNameSortParm = sortOrder == "Last Name" ? "LastName_Desc" : "Last Name";
            ViewBag.SecondLastNameSortParm = sortOrder == "Second Last Name" ? "SecondLastName_Desc" : "Second Last Name";
            ViewBag.EmailSortParm = sortOrder == "Email" ? "Email_Desc" : "Email";
            ViewBag.PhoneSortParm = sortOrder == "Phone" ? "Phone_Desc" : "Phone";

            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;

            ViewBag.CurrentFilter = searchString;

            var supplierContacts = proxy.GetSupplierContacts();
            var proxyS = new SupplierServiceClient();
            var proxyD = new DocumentTypeServiceClient();
            foreach (var item in supplierContacts)
            {
                item.Supplier = proxyS.GetSupplier(item.SupplierId);
                item.DocumentType = proxyD.GetDocumentType(item.DocumentTypeId);
            }

            if (!String.IsNullOrEmpty(searchString))
                supplierContacts = supplierContacts.FindAll(s =>
                    s.Supplier.BusinessName.Contains(searchString) ||
                    s.DocumentType.Name.Contains(searchString) ||
                    s.DocumentNumber.Contains(searchString) ||
                    s.FirstName.Contains(searchString) ||
                    s.LastName.Contains(searchString) ||
                    s.SecondLastName.Contains(searchString) ||
                    s.Email.Contains(searchString) ||
                    s.Phone.Contains(searchString));

            switch (sortOrder)
            {
                case "BusinessName_Desc":
                    supplierContacts = supplierContacts.OrderByDescending(p => p.Supplier.BusinessName).ToList();
                    break;
                case "DocumentType_Desc":
                    supplierContacts = supplierContacts.OrderByDescending(p => p.DocumentType.Name).ToList();
                    break;
                case "Document Type":
                    supplierContacts = supplierContacts.OrderBy(p => p.DocumentType.Name).ToList();
                    break;
                case "DocumentNumber_Desc":
                    supplierContacts = supplierContacts.OrderByDescending(p => p.DocumentNumber).ToList();
                    break;
                case "Document Number":
                    supplierContacts = supplierContacts.OrderBy(p => p.DocumentNumber).ToList();
                    break;
                case "FirstName_Desc":
                    supplierContacts = supplierContacts.OrderByDescending(p => p.FirstName).ToList();
                    break;
                case "First Name":
                    supplierContacts = supplierContacts.OrderBy(p => p.FirstName).ToList();
                    break;
                case "LastName_Desc":
                    supplierContacts = supplierContacts.OrderByDescending(p => p.LastName).ToList();
                    break;
                case "Last Name":
                    supplierContacts = supplierContacts.OrderBy(p => p.LastName).ToList();
                    break;
                case "Second Last Name":
                    supplierContacts = supplierContacts.OrderBy(p => p.SecondLastName).ToList();
                    break;
                case "SecondLastName_Desc":
                    supplierContacts = supplierContacts.OrderByDescending(p => p.SecondLastName).ToList();
                    break;
                case "Email":
                    supplierContacts = supplierContacts.OrderBy(p => p.Email).ToList();
                    break;
                case "Email_Desc":
                    supplierContacts = supplierContacts.OrderByDescending(p => p.Email).ToList();
                    break;
                case "Phone":
                    supplierContacts = supplierContacts.OrderBy(p => p.Phone).ToList();
                    break;
                case "Phone_Desc":
                    supplierContacts = supplierContacts.OrderByDescending(p => p.Phone).ToList();
                    break;
                default:
                    supplierContacts = supplierContacts.OrderBy(p => p.Supplier.BusinessName).ToList();
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(supplierContacts.ToPagedList(pageNumber, pageSize));
        }

        // GET: SupplierContacts/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var proxy = new SupplierContactServiceClient();
            var supplierContact = proxy.GetSupplierContact(id.Value);
            if (supplierContact == null)
                return HttpNotFound();
            return View(supplierContact);
        }

        // GET: SupplierContacts/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var proxyS = new SupplierServiceClient();
            var proxyD = new DocumentTypeServiceClient();
            ViewBag.SupplierId = new SelectList(proxyS.GetSuppliers().OrderBy(d => d.BusinessName), "SupplierId", "BusinessName");
            ViewBag.DocumentTypeId = new SelectList(proxyD.GetDocumentTypes().OrderBy(d => d.Name).ToList().FindAll(d => d.ClassDocumentTypeId.Equals(1) && !d.OnlyForEnterprise), "DocumentTypeId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "SupplierContactId,SupplierId,DocumentTypeId,DocumentNumber,FirstName,LastName,SecondLastName,Email,Phone")] SupplierContact supplierContact)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var proxy = new SupplierContactServiceClient();
                    proxy.CreateSupplierContact(supplierContact);
                    return RedirectToAction("Index");
                }
            }
            catch (FaultException<GeneralException> ex)
            {
                ViewBag.ErrorCode = String.Format("Error Code: {0}", ex.Detail.Id);
                ViewBag.ErrorMessage = String.Format("Error Message: {0}", ex.Detail.Description);
            }
            var proxyS = new SupplierServiceClient();
            var proxyD = new DocumentTypeServiceClient();
            ViewBag.SupplierId = new SelectList(proxyS.GetSuppliers().OrderBy(d => d.BusinessName), "SupplierId", "BusinessName", supplierContact.SupplierId);
            ViewBag.DocumentTypeId = new SelectList(proxyD.GetDocumentTypes().OrderBy(d => d.Name).ToList().FindAll(d => d.ClassDocumentTypeId.Equals(1) && !d.OnlyForEnterprise), "DocumentTypeId", "Name", supplierContact.DocumentTypeId);
            return View(supplierContact);
        }

        // GET: SupplierContacts/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var proxy = new SupplierContactServiceClient();
            var supplierContact = proxy.GetSupplierContact(id.Value);
            if (supplierContact == null)
                return HttpNotFound();
            var proxyS = new SupplierServiceClient();
            var proxyD = new DocumentTypeServiceClient();
            ViewBag.SupplierId = new SelectList(proxyS.GetSuppliers().OrderBy(d => d.BusinessName), "SupplierId", "BusinessName", supplierContact.SupplierId);
            ViewBag.DocumentTypeId = new SelectList(proxyD.GetDocumentTypes().OrderBy(d => d.Name).ToList().FindAll(d => d.ClassDocumentTypeId.Equals(1) && !d.OnlyForEnterprise), "DocumentTypeId", "Name", supplierContact.DocumentTypeId);
            return View(supplierContact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "SupplierContactId,SupplierId,DocumentTypeId,DocumentNumber,FirstName,LastName,SecondLastName,Email,Phone")] SupplierContact supplierContact)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var proxy = new SupplierContactServiceClient();
                    proxy.EditSupplierContact(supplierContact);
                    return RedirectToAction("Index");
                }
            }
            catch (FaultException<GeneralException> ex)
            {
                ViewBag.ErrorCode = String.Format("Error Code: {0}", ex.Detail.Id);
                ViewBag.ErrorMessage = String.Format("Error Message: {0}", ex.Detail.Description);
            }
            var proxyS = new SupplierServiceClient();
            var proxyD = new DocumentTypeServiceClient();
            ViewBag.SupplierId = new SelectList(proxyS.GetSuppliers().OrderBy(d => d.BusinessName), "SupplierId", "BusinessName", supplierContact.SupplierId);
            ViewBag.DocumentTypeId = new SelectList(proxyD.GetDocumentTypes().OrderBy(d => d.Name).ToList().FindAll(d => d.ClassDocumentTypeId.Equals(1) && !d.OnlyForEnterprise), "DocumentTypeId", "Name", supplierContact.DocumentTypeId);
            return View(supplierContact);
        }

        // GET: SupplierContacts/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var proxy = new SupplierContactServiceClient();
            var supplierContact = proxy.GetSupplierContact(id.Value);
            if (supplierContact == null)
                return HttpNotFound();
            return View(supplierContact);
        }

        // POST: SupplierContacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            var proxy = new SupplierContactServiceClient();
            proxy.DeleteSupplierContact(id);
            return RedirectToAction("Index");
        }
    }
}