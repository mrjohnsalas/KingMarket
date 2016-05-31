using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;
using KingMarket.Model.Models;
using KingMarket.Web.CustomerService;
using KingMarket.Web.CustomerContactService;
using KingMarket.Web.DocumentTypeService;
using PagedList;

namespace KingMarket.Web.Controllers
{
    public class CustomerContactsController : Controller
    {
        // GET: CustomerContacts
        [Authorize(Roles = "Admin")]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var proxy = new CustomerContactServiceClient();

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

            var customerContacts = proxy.GetCustomerContacts();
            var proxyC = new CustomerServiceClient();
            var proxyD = new DocumentTypeServiceClient();
            foreach (var item in customerContacts)
            {
                item.Customer = proxyC.GetCustomer(item.CustomerId);
                item.DocumentType = proxyD.GetDocumentType(item.DocumentTypeId);
            }

            if (!String.IsNullOrEmpty(searchString))
                customerContacts = customerContacts.FindAll(s =>
                    s.Customer.BusinessName.Contains(searchString) ||
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
                    customerContacts = customerContacts.OrderByDescending(p => p.Customer.BusinessName).ToList();
                    break;
                case "DocumentType_Desc":
                    customerContacts = customerContacts.OrderByDescending(p => p.DocumentType.Name).ToList();
                    break;
                case "Document Type":
                    customerContacts = customerContacts.OrderBy(p => p.DocumentType.Name).ToList();
                    break;
                case "DocumentNumber_Desc":
                    customerContacts = customerContacts.OrderByDescending(p => p.DocumentNumber).ToList();
                    break;
                case "Document Number":
                    customerContacts = customerContacts.OrderBy(p => p.DocumentNumber).ToList();
                    break;
                case "FirstName_Desc":
                    customerContacts = customerContacts.OrderByDescending(p => p.FirstName).ToList();
                    break;
                case "First Name":
                    customerContacts = customerContacts.OrderBy(p => p.FirstName).ToList();
                    break;
                case "LastName_Desc":
                    customerContacts = customerContacts.OrderByDescending(p => p.LastName).ToList();
                    break;
                case "Last Name":
                    customerContacts = customerContacts.OrderBy(p => p.LastName).ToList();
                    break;
                case "Second Last Name":
                    customerContacts = customerContacts.OrderBy(p => p.SecondLastName).ToList();
                    break;
                case "SecondLastName_Desc":
                    customerContacts = customerContacts.OrderByDescending(p => p.SecondLastName).ToList();
                    break;
                case "Email":
                    customerContacts = customerContacts.OrderBy(p => p.Email).ToList();
                    break;
                case "Email_Desc":
                    customerContacts = customerContacts.OrderByDescending(p => p.Email).ToList();
                    break;
                case "Phone":
                    customerContacts = customerContacts.OrderBy(p => p.Phone).ToList();
                    break;
                case "Phone_Desc":
                    customerContacts = customerContacts.OrderByDescending(p => p.Phone).ToList();
                    break;
                default:
                    customerContacts = customerContacts.OrderBy(p => p.Customer.BusinessName).ToList();
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(customerContacts.ToPagedList(pageNumber, pageSize));
        }

        // GET: CustomerContacts/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var proxy = new CustomerContactServiceClient();
            var customerContact = proxy.GetCustomerContact(id.Value);
            if (customerContact == null)
                return HttpNotFound();
            return View(customerContact);
        }

        // GET: CustomerContacts/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var proxyC = new CustomerServiceClient();
            var proxyD = new DocumentTypeServiceClient();
            ViewBag.CustomerId = new SelectList(proxyC.GetCustomers().OrderBy(d => String.IsNullOrEmpty(d.BusinessName) ? d.FirstName : d.BusinessName), "CustomerId", "FullName");
            ViewBag.DocumentTypeId = new SelectList(proxyD.GetDocumentTypes().OrderBy(d => d.Name).ToList().FindAll(d => d.ClassDocumentTypeId.Equals(1) && !d.OnlyForEnterprise), "DocumentTypeId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "CustomerContactId,CustomerId,DocumentTypeId,DocumentNumber,FirstName,LastName,SecondLastName,Email,Phone")] CustomerContact customerContact)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var proxy = new CustomerContactServiceClient();
                    proxy.CreateCustomerContact(customerContact);
                    return RedirectToAction("Index");
                }
            }
            catch (FaultException<GeneralException> ex)
            {
                ViewBag.ErrorCode = String.Format("Error Code: {0}", ex.Detail.Id);
                ViewBag.ErrorMessage = String.Format("Error Message: {0}", ex.Detail.Description);
            }
            var proxyC = new CustomerServiceClient();
            var proxyD = new DocumentTypeServiceClient();
            ViewBag.CustomerId = new SelectList(proxyC.GetCustomers().OrderBy(d => String.IsNullOrEmpty(d.BusinessName) ? d.FirstName : d.BusinessName), "CustomerId", "FullName", customerContact.CustomerId);
            ViewBag.DocumentTypeId = new SelectList(proxyD.GetDocumentTypes().OrderBy(d => d.Name).ToList().FindAll(d => d.ClassDocumentTypeId.Equals(1) && !d.OnlyForEnterprise), "DocumentTypeId", "Name", customerContact.DocumentTypeId);
            return View(customerContact);
        }

        // GET: CustomerContacts/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var proxy = new CustomerContactServiceClient();
            var customerContact = proxy.GetCustomerContact(id.Value);
            if (customerContact == null)
                return HttpNotFound();
            var proxyC = new CustomerServiceClient();
            var proxyD = new DocumentTypeServiceClient();
            ViewBag.CustomerId = new SelectList(proxyC.GetCustomers().OrderBy(d => String.IsNullOrEmpty(d.BusinessName) ? d.FirstName : d.BusinessName), "CustomerId", "FullName", customerContact.CustomerId);
            ViewBag.DocumentTypeId = new SelectList(proxyD.GetDocumentTypes().OrderBy(d => d.Name).ToList().FindAll(d => d.ClassDocumentTypeId.Equals(1) && !d.OnlyForEnterprise), "DocumentTypeId", "Name", customerContact.DocumentTypeId);
            return View(customerContact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "CustomerContactId,CustomerId,DocumentTypeId,DocumentNumber,FirstName,LastName,SecondLastName,Email,Phone")] CustomerContact customerContact)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var proxy = new CustomerContactServiceClient();
                    proxy.EditCustomerContact(customerContact);
                    return RedirectToAction("Index");
                }
            }
            catch (FaultException<GeneralException> ex)
            {
                ViewBag.ErrorCode = String.Format("Error Code: {0}", ex.Detail.Id);
                ViewBag.ErrorMessage = String.Format("Error Message: {0}", ex.Detail.Description);
            }
            var proxyC = new CustomerServiceClient();
            var proxyD = new DocumentTypeServiceClient();
            ViewBag.CustomerId = new SelectList(proxyC.GetCustomers().OrderBy(d => String.IsNullOrEmpty(d.BusinessName) ? d.FirstName : d.BusinessName), "CustomerId", "FullName", customerContact.CustomerId);
            ViewBag.DocumentTypeId = new SelectList(proxyD.GetDocumentTypes().OrderBy(d => d.Name).ToList().FindAll(d => d.ClassDocumentTypeId.Equals(1) && !d.OnlyForEnterprise), "DocumentTypeId", "Name", customerContact.DocumentTypeId);
            return View(customerContact);
        }

        // GET: CustomerContacts/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var proxy = new CustomerContactServiceClient();
            var customerContact = proxy.GetCustomerContact(id.Value);
            if (customerContact == null)
                return HttpNotFound();
            return View(customerContact);
        }

        // POST: CustomerContacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            var proxy = new CustomerContactServiceClient();
            proxy.DeleteCustomerContact(id);
            return RedirectToAction("Index");
        }
    }
}