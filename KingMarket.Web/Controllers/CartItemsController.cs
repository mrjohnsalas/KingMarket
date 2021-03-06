﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;
using KingMarket.Model.Models;
using KingMarket.Web.CartItemService;
using KingMarket.Web.CustomerService;
using KingMarket.Web.DocumentTypeService;
using KingMarket.Web.ProductPhotoService;
using KingMarket.Web.ProductService;
using KingMarket.Web.ProductTypeService;
using KingMarket.Web.SaleOrderService;
using KingMarket.Web.UnitMeasureService;
using PagedList;
using Microsoft.AspNet.Identity;

namespace KingMarket.Web.Controllers
{
    // GET: CartItems
    public class CartItemsController : Controller
    {
        //MUESTRA LOS PRODUCTOS ACTUALES DEL CARRITO DE COMPRAS
        [Authorize(Roles = "Admin, Customer")]
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

            var proxyC = new CustomerServiceClient();
            var customer = proxyC.GetCustomerByEmail(User.Identity.GetUserName());
            var cartItems = proxy.GetCartItemsByCustomerId(customer.CustomerId);
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

        //ELIMINA UN PRODUCTO DEL CARRITO DE COMPRAS
        [HttpPost]
        [Authorize(Roles = "Admin, Customer")]
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

        //MUESTRA LA VISTA QUE PERMITE EDITAR
        [Authorize(Roles = "Admin, Customer")]
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
            var proxyC = new CustomerServiceClient();
            cartItem.Product = proxyP.GetProduct(cartItem.ProductId);
            cartItem.Product.ProductType = proxyT.GetProductType(cartItem.Product.ProductTypeId);
            cartItem.Product.UnitMeasure = proxyU.GetUnitMeasure(cartItem.Product.UnitMeasureId);
            cartItem.Customer = proxyC.GetCustomerByEmail(User.Identity.GetUserName());
            return View(cartItem);
        }

        //EDITA EL PRODUCTO DEL CARRITO DE COMPRAS
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Customer")]
        public ActionResult Edit([Bind(Include = "CartItemId,CustomerId,DateCreated,ProductId,Quantity")] CartItem cartItem)
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

        //CREA LA ORDEN
        [HttpPost]
        [Authorize(Roles = "Admin, Customer")]
        public ActionResult Index()
        {
            var pageSize = 10;
            var pageNumber = 1;
            var proxyC = new CustomerServiceClient();
            var customer = proxyC.GetCustomerByEmail(User.Identity.GetUserName());
            var proxy = new CartItemServiceClient();
            var cartItems = proxy.GetCartItemsByCustomerId(customer.CustomerId);

            if (cartItems == null || cartItems.Count.Equals(0))
                return CallError("UI", "You need to add products to the list.", cartItems);
            var proxyP = new ProductServiceClient();
            foreach (var item in cartItems)
                item.Product = proxyP.GetProduct(item.ProductId);

            try
            {
                var proxyD = new DocumentTypeServiceClient();
                var documentType = proxyD.GetDocumentTypeForPay(customer.DocumentTypeId);
                var saleOrder = new SaleOrder
                {
                    CustomerId = customer.CustomerId,
                    DocumentTypeId = documentType.DocumentTypeId,
                    StatusId = 6,
                    SaleOrderDetails = new List<SaleOrderDetail>()
                };
                foreach (var item in cartItems)
                {
                    saleOrder.SaleOrderDetails.Add(new SaleOrderDetail
                    {
                        ProductId = item.Product.ProductId,
                        Name = item.Product.Name,
                        UnitPrice = item.Product.UnitPrice,
                        Quantity = item.Quantity
                    });
                }
                var proxyS = new SaleOrderServiceClient();
                proxyS.CreateSaleOrder(saleOrder);
                ViewBag.OkMessage = String.Format("Sucess Message: Sale order: {0} has been successfully created.", saleOrder.SaleOrderId);
                cartItems = proxy.GetCartItemsByCustomerId(customer.CustomerId);
                return View(cartItems.ToPagedList(pageNumber, pageSize));
            }
            catch (FaultException<GeneralException> ex)
            {
                ViewBag.ErrorCode = String.Format("Error Code: {0}", ex.Detail.Id);
                ViewBag.ErrorMessage = String.Format("Error Message: {0}", ex.Detail.Description);
            }
            var proxyT = new ProductTypeServiceClient();
            var proxyU = new UnitMeasureServiceClient();
            foreach (var item in cartItems)
            {
                item.Product.ProductType = proxyT.GetProductType(item.Product.ProductTypeId);
                item.Product.UnitMeasure = proxyU.GetUnitMeasure(item.Product.UnitMeasureId);
            }
            return View(cartItems.ToPagedList(pageNumber, pageSize));
        }

        private ActionResult CallError(string errorCode, string errorMessage, List<CartItem> cartItems)
        {
            ViewBag.ErrorCode = String.Format("Error Code: {0}", errorCode);
            ViewBag.ErrorMessage = String.Format("Error Message: {0}", errorMessage);
            var pageSize = 10;
            var pageNumber = 1;
            return View(cartItems.ToPagedList(pageNumber, pageSize));
        }
    }
}