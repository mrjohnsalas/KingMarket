using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;
using KingMarket.Model.Models;
using KingMarket.Web.CartItemService;
using KingMarket.Web.ProductPhotoService;
using KingMarket.Web.ProductService;
using KingMarket.Web.ProductTypeService;
using KingMarket.Web.UnitMeasureService;
using PagedList;
using Microsoft.AspNet.Identity;

namespace KingMarket.Web.Controllers
{
    // GET: CartItems
    public class CartItemsController : Controller
    {
        // GET: CartItems
        [Authorize(Roles = "Admin")]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var proxy = new CartItemServiceClient();

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name_Desc" : String.Empty;
            ViewBag.ProductTypeSortParm = sortOrder == "Product Type" ? "ProductType_Desc" : "Product Type";
            ViewBag.UnitMeasureSortParm = sortOrder == "Unit Measure" ? "UnitMeasure_Desc" : "Unit Measure";
            ViewBag.DescriptionSortParm = sortOrder == "Description" ? "Description_Desc" : "Description";
            ViewBag.UnitPriceSortParm = sortOrder == "Unit Price" ? "UnitPrice_Desc" : "Unit Price";

            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;

            ViewBag.CurrentFilter = searchString;

            var cartItems = proxy.GetCartItemsByUserId(User.Identity.GetUserId());
            var proxyP = new ProductServiceClient();
            var proxyT = new ProductTypeServiceClient();
            var proxyU = new UnitMeasureServiceClient();
            foreach (var item in cartItems)
            {
                item.Product = proxyP.GetProduct(item.ProductId);
                item.Product.ProductType = proxyT.GetProductType(item.Product.ProductTypeId);
                item.Product.UnitMeasure = proxyU.GetUnitMeasure(item.Product.UnitMeasureId);
            }

            if (!String.IsNullOrEmpty(searchString))
                cartItems = cartItems.FindAll(s =>
                    s.Product.Name.Contains(searchString) ||
                    s.Product.Description.Contains(searchString) ||
                    s.Product.UnitMeasure.ShortName.Contains(searchString));

            switch (sortOrder)
            {
                case "Name_Desc":
                    cartItems = cartItems.OrderByDescending(p => p.Product.Name).ToList();
                    break;
                case "Product Type":
                    cartItems = cartItems.OrderBy(p => p.Product.ProductType.Name).ToList();
                    break;
                case "ProductType_Desc":
                    cartItems = cartItems.OrderByDescending(p => p.Product.ProductType.Name).ToList();
                    break;
                case "Unit Measure":
                    cartItems = cartItems.OrderBy(p => p.Product.UnitMeasure.ShortName).ToList();
                    break;
                case "UnitMeasure_Desc":
                    cartItems = cartItems.OrderByDescending(p => p.Product.UnitMeasure.ShortName).ToList();
                    break;
                case "Description":
                    cartItems = cartItems.OrderBy(p => p.Product.Description).ToList();
                    break;
                case "Description_Desc":
                    cartItems = cartItems.OrderByDescending(p => p.Product.Description).ToList();
                    break;
                case "Unit Price":
                    cartItems = cartItems.OrderBy(p => p.Product.UnitPrice).ToList();
                    break;
                case "UnitPrice_Desc":
                    cartItems = cartItems.OrderByDescending(p => p.Product.UnitPrice).ToList();
                    break;
                default:
                    cartItems = cartItems.OrderBy(p => p.Product.Name).ToList();
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(cartItems.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public JsonResult DeleteCartItem(int? id)
        {
            if (!id.HasValue)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Result = "Error" });
            }
            try
            {
                var proxy = new CartItemServiceClient();
                var cartItem = proxy.GetCartItem(id.Value);
                if (cartItem == null)
                {
                    Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return Json(new { Result = "Error" });
                }
                proxy.DeleteCartItem(id.Value);
                return Json(new { Result = "Ok" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        // GET: ClassDocumentTypes/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var proxy = new CartItemServiceClient();
            var cartItem = proxy.GetCartItem(id.Value);
            if (cartItem == null)
                return HttpNotFound();
            var proxyP = new ProductServiceClient();
            var proxyT = new ProductTypeServiceClient();
            var proxyU = new UnitMeasureServiceClient();
            cartItem.Product = proxyP.GetProduct(cartItem.ProductId);
            cartItem.Product.ProductType = proxyT.GetProductType(cartItem.Product.ProductTypeId);
            cartItem.Product.UnitMeasure = proxyU.GetUnitMeasure(cartItem.Product.UnitMeasureId);
            return View(cartItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "CartItemId,UserId,DateCreated,ProductId,Quantity")] CartItem cartItem)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var proxy = new CartItemServiceClient();
                    proxy.EditCartItem(cartItem);
                    return RedirectToAction("Index");
                }
            }
            catch (FaultException<GeneralException> ex)
            {
                ViewBag.ErrorCode = String.Format("Error Code: {0}", ex.Detail.Id);
                ViewBag.ErrorMessage = String.Format("Error Message: {0}", ex.Detail.Description);
            }
            return View(cartItem);
        }
    }
}