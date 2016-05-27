using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KingMarket.Model.Models;
using KingMarket.Web.ProductTypeService;
using KingMarket.Web.ProductService;
using KingMarket.Web.UnitMeasureService;
using KingMarket.Web.ProductPhotoService;
using PagedList;

namespace KingMarket.Web.Controllers
{
    // GET: Products
    public class ProductsController : Controller
    {
        // GET: Products
        [Authorize(Roles = "Admin")]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var proxy = new ProductServiceClient();

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name_Desc" : String.Empty;
            ViewBag.ProductTypeSortParm = sortOrder == "Product Type" ? "ProductType_Desc" : "Product Type";
            ViewBag.UnitMeasureSortParm = sortOrder == "Unit Measure" ? "UnitMeasure_Desc" : "Unit Measure";
            ViewBag.DescriptionSortParm = sortOrder == "Description" ? "Description_Desc" : "Description";
            ViewBag.UnitPriceSortParm = sortOrder == "Unit Price" ? "UnitPrice_Desc" : "Unit Price";
            ViewBag.UnitCostSortParm = sortOrder == "Unit Cost" ? "UnitCost_Desc" : "Unit Cost";
            ViewBag.StockSortParm = sortOrder == "Stock" ? "Stock_Desc" : "Stock";
            ViewBag.MinStockSortParm = sortOrder == "Min Stock" ? "MinStock_Desc" : "Min Stock";
            ViewBag.MaxStockSortParm = sortOrder == "Max Stock" ? "MaxStock_Desc" : "Max Stock";

            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;

            ViewBag.CurrentFilter = searchString;

            var products = proxy.GetProducts();
            var proxyP = new ProductTypeServiceClient();
            var proxyU = new UnitMeasureServiceClient();
            foreach (var item in products)
            {
                item.ProductType = proxyP.GetProductType(item.ProductTypeId);
                item.UnitMeasure = proxyU.GetUnitMeasure(item.UnitMeasureId);
            }

            if (!String.IsNullOrEmpty(searchString))
                products = products.FindAll(s =>
                    s.Name.Contains(searchString) ||
                    s.Description.Contains(searchString) ||
                    s.UnitMeasure.ShortName.Contains(searchString));

            switch (sortOrder)
            {
                case "Name_Desc":
                    products = products.OrderByDescending(p => p.Name).ToList();
                    break;
                case "Product Type":
                    products = products.OrderBy(p => p.ProductType.Name).ToList();
                    break;
                case "ProductType_Desc":
                    products = products.OrderByDescending(p => p.ProductType.Name).ToList();
                    break;
                case "Unit Measure":
                    products = products.OrderBy(p => p.UnitMeasure.ShortName).ToList();
                    break;
                case "UnitMeasure_Desc":
                    products = products.OrderByDescending(p => p.UnitMeasure.ShortName).ToList();
                    break;
                case "Description":
                    products = products.OrderBy(p => p.Description).ToList();
                    break;
                case "Description_Desc":
                    products = products.OrderByDescending(p => p.Description).ToList();
                    break;
                case "Unit Price":
                    products = products.OrderBy(p => p.UnitPrice).ToList();
                    break;
                case "UnitPrice_Desc":
                    products = products.OrderByDescending(p => p.UnitPrice).ToList();
                    break;
                case "Unit Cost":
                    products = products.OrderBy(p => p.UnitCost).ToList();
                    break;
                case "UnitCost_Desc":
                    products = products.OrderByDescending(p => p.UnitCost).ToList();
                    break;
                case "Stock":
                    products = products.OrderBy(p => p.Stock).ToList();
                    break;
                case "Stock_Desc":
                    products = products.OrderByDescending(p => p.Stock).ToList();
                    break;
                case "Min Stock":
                    products = products.OrderBy(p => p.MinStock).ToList();
                    break;
                case "MinStock_Desc":
                    products = products.OrderByDescending(p => p.MinStock).ToList();
                    break;
                case "Max Stock":
                    products = products.OrderBy(p => p.MaxStock).ToList();
                    break;
                case "MaxStock_Desc":
                    products = products.OrderByDescending(p => p.MaxStock).ToList();
                    break;
                default:
                    products = products.OrderBy(p => p.Name).ToList();
                    break;
            }

            int pageSize = 25;
            int pageNumber = (page ?? 1);
            return View(products.ToPagedList(pageNumber, pageSize));
        }

        // GET: Products/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var proxy = new ProductServiceClient();
            var product = proxy.GetProduct(id.Value);
            if (product == null)
                return HttpNotFound();
            var proxyF = new ProductPhotoServiceClient();
            product.ProductPhotos = proxyF.GetProductPhotosByProductId(product.ProductId);
            return View(product);
        }

        // GET: Products/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var proxyP = new ProductTypeServiceClient();
            var proxyU = new UnitMeasureServiceClient();
            ViewBag.ProductTypeId = new SelectList(proxyP.GetProductTypes().OrderBy(d => d.Name), "ProductTypeId", "Name");
            ViewBag.UnitMeasureId = new SelectList(proxyU.GetUnitMeasures().OrderBy(d => d.ShortName), "UnitMeasureId", "ShortName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "ProductId,Name,Description,UnitPrice,UnitCost,ProductTypeId,Stock,MinStock,MaxStock,UnitMeasureId")] Product product)
        {
            if (ModelState.IsValid)
            {
                var productPhotos = new List<ProductPhoto>();
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];
                    if (file != null && file.ContentLength > 0)
                    {
                        var photo = new ProductPhoto
                        {
                            FileName = Path.GetFileName(file.FileName),
                            FileType = FileType.Photo,
                            ContentType = file.ContentType
                        };
                        using (var reader = new BinaryReader(file.InputStream))
                            photo.Content = reader.ReadBytes(file.ContentLength);
                        productPhotos.Add(photo);
                    }
                }
                product.ProductPhotos = productPhotos;
                var proxy = new ProductServiceClient();
                proxy.CreateProduct(product);
                return RedirectToAction("Index");
            }
            var proxyP = new ProductTypeServiceClient();
            var proxyU = new UnitMeasureServiceClient();
            ViewBag.ProductTypeId = new SelectList(proxyP.GetProductTypes().OrderBy(d => d.Name), "ProductTypeId", "Name", product.ProductTypeId);
            ViewBag.UnitMeasureId = new SelectList(proxyU.GetUnitMeasures().OrderBy(d => d.ShortName), "UnitMeasureId", "ShortName", product.UnitMeasureId);
            return View(product);
        }

        // GET: Products/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var proxy = new ProductServiceClient();
            var product = proxy.GetProduct(id.Value);
            if (product == null)
                return HttpNotFound();
            var proxyF = new ProductPhotoServiceClient();
            product.ProductPhotos = proxyF.GetProductPhotosByProductIdNoContent(product.ProductId);
            var proxyP = new ProductTypeServiceClient();
            var proxyU = new UnitMeasureServiceClient();
            ViewBag.ProductTypeId = new SelectList(proxyP.GetProductTypes().OrderBy(d => d.Name), "ProductTypeId", "Name", product.ProductTypeId);
            ViewBag.UnitMeasureId = new SelectList(proxyU.GetUnitMeasures().OrderBy(d => d.ShortName), "UnitMeasureId", "ShortName", product.UnitMeasureId);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "ProductId,Name,Description,UnitPrice,UnitCost,ProductTypeId,Stock,MinStock,MaxStock,UnitMeasureId")] Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.ProductPhotos == null)
                    product.ProductPhotos = new List<ProductPhoto>();

                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];
                    if (file != null && file.ContentLength > 0)
                    {
                        var photo = new ProductPhoto
                        {
                            FileName = Path.GetFileName(file.FileName),
                            FileType = FileType.Photo,
                            ContentType = file.ContentType,
                            ProductId = product.ProductId
                        };
                        using (var reader = new BinaryReader(file.InputStream))
                            photo.Content = reader.ReadBytes(file.ContentLength);
                        product.ProductPhotos.Add(photo);
                        //db.Entry(photo).State = EntityState.Added;
                    }
                }
                var proxy = new ProductServiceClient();
                proxy.EditProduct(product);
                return RedirectToAction("Index");
            }
            var proxyP = new ProductTypeServiceClient();
            var proxyU = new UnitMeasureServiceClient();
            ViewBag.ProductTypeId = new SelectList(proxyP.GetProductTypes().OrderBy(d => d.Name), "ProductTypeId", "Name", product.ProductTypeId);
            ViewBag.UnitMeasureId = new SelectList(proxyU.GetUnitMeasures().OrderBy(d => d.ShortName), "UnitMeasureId", "ShortName", product.UnitMeasureId);
            return View(product);
        }

        // GET: Products/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var proxy = new ProductServiceClient();
            var product = proxy.GetProduct(id.Value);
            if (product == null)
                return HttpNotFound();
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            var proxy = new ProductServiceClient();
            var product = proxy.GetProduct(id);
            if (product == null)
                return HttpNotFound();
            var proxyF = new ProductPhotoServiceClient();
            proxyF.DeleteProductPhotosByProductId(product.ProductId);
            proxy.DeleteProduct(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public JsonResult DeleteFile(int? id)
        {
            if (!id.HasValue)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Result = "Error" });
            }
            try
            {
                var proxyF = new ProductPhotoServiceClient();
                var productPhoto = proxyF.GetProductPhoto(id.Value);
                if (productPhoto == null)
                {
                    Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return Json(new { Result = "Error" });
                }
                proxyF.DeleteProductPhoto(id.Value);
                return Json(new { Result = "Ok" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        public FileResult Download(int? id)
        {
            Server.MapPath("~");
            var proxyF = new ProductPhotoServiceClient();
            var productPhoto = proxyF.GetProductPhoto(id.Value);
            return File(productPhoto.Content, System.Net.Mime.MediaTypeNames.Application.Octet, productPhoto.FileName);
        }
    }
}