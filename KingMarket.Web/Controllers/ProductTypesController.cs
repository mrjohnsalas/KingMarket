using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;
using KingMarket.Model.Models;
using KingMarket.Web.ProductTypeService;
using PagedList;

namespace KingMarket.Web.Controllers
{
    // GET: ProductTypes
    public class ProductTypesController : Controller
    {
        // GET: ProductTypes
        [Authorize(Roles = "Admin")]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var proxy = new ProductTypeServiceClient();

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name_Desc" : String.Empty;

            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;

            ViewBag.CurrentFilter = searchString;

            var productTypes = sortOrder == "Name_Desc" ?
                String.IsNullOrEmpty(searchString) ?
                proxy.GetProductTypes().OrderByDescending(c => c.Name) :
                proxy.GetProductTypes().OrderByDescending(c => c.Name).Where(s => s.Name.Contains(searchString)) :
                String.IsNullOrEmpty(searchString) ?
                proxy.GetProductTypes().OrderBy(c => c.Name) :
                proxy.GetProductTypes().OrderBy(c => c.Name).Where(s => s.Name.Contains(searchString));

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(productTypes.ToPagedList(pageNumber, pageSize));
        }

        // GET: ProductTypes/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var proxy = new ProductTypeServiceClient();
            var productType = proxy.GetProductType(id.Value);
            if (productType == null)
                return HttpNotFound();
            return View(productType);
        }

        // GET: ProductTypes/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "ProductTypeId,Name")] ProductType productType)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var proxy = new ProductTypeServiceClient();
                    proxy.CreateProductType(productType);
                    return RedirectToAction("Index");
                }
            }
            catch (FaultException<GeneralException> ex)
            {
                ViewBag.ErrorCode = String.Format("Error Code: {0}", ex.Detail.Id);
                ViewBag.ErrorMessage = String.Format("Error Message: {0}", ex.Detail.Description);
            }
            return View(productType);
        }

        // GET: ProductTypes/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var proxy = new ProductTypeServiceClient();
            var productType = proxy.GetProductType(id.Value);
            if (productType == null)
                return HttpNotFound();
            return View(productType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "ProductTypeId,Name")] ProductType productType)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var proxy = new ProductTypeServiceClient();
                    proxy.EditProductType(productType);
                    return RedirectToAction("Index");
                }
            }
            catch (FaultException<GeneralException> ex)
            {
                ViewBag.ErrorCode = String.Format("Error Code: {0}", ex.Detail.Id);
                ViewBag.ErrorMessage = String.Format("Error Message: {0}", ex.Detail.Description);
            }
            return View(productType);
        }

        // GET: ProductTypes/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var proxy = new ProductTypeServiceClient();
            var productType = proxy.GetProductType(id.Value);
            if (productType == null)
                return HttpNotFound();
            return View(productType);
        }

        // POST: ProductTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            var proxy = new ProductTypeServiceClient();
            proxy.DeleteProductType(id);
            return RedirectToAction("Index");
        }
    }
}