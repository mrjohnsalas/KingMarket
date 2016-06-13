using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KingMarket.Model.Models;
using KingMarket.Utility;
using KingMarket.Web.BuyOrderService;
using KingMarket.Web.ProductTypeService;
using KingMarket.Web.ProductService;
using KingMarket.Web.UnitMeasureService;
using KingMarket.Web.ProductPhotoService;
using KingMarket.Web.CartItemService;
using KingMarket.Web.CustomerService;
using Microsoft.AspNet.Identity;
using PagedList;

namespace KingMarket.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var proxy = new ProductServiceClient();

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

            var products = proxy.GetProducts();
            var proxyT = new ProductTypeServiceClient();
            var proxyU = new UnitMeasureServiceClient();
            var proxyF = new ProductPhotoServiceClient();
            foreach (var item in products)
            {
                item.ProductType = proxyT.GetProductType(item.ProductTypeId);
                item.UnitMeasure = proxyU.GetUnitMeasure(item.UnitMeasureId);
                var photo = proxyF.GetProductPhotoDefaultByProductId(item.ProductId);
                item.ProductPhotos = new List<ProductPhoto> { photo };
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
                default:
                    products = products.OrderBy(p => p.Name).ToList();
                    break;
            }

            //SI HAY PRODUCTOS ERN LA COLA
            //CREO LA ORDEN DE COMPTA CON LOS 
            //PRODUCTOS DE LA COLA
            if (User.IsInRole("Buyer"))
            {
                var pathQueue = @".\private$\products";
                var productsQueue = Utilities.ReadQueue<ProductQueue>(pathQueue, true);
                if (productsQueue != null && productsQueue.Count > 0)
                {
                    var buyOrder = new BuyOrder
                    {
                        UserId = User.Identity.GetUserId(),
                        SupplierId = 1,
                        DocumentTypeId = 6,
                        StatusId = 6,
                        BuyOrderDetails = new List<BuyOrderDetail>()
                    };
                    var proxyP = new ProductServiceClient();
                    foreach (var item in productsQueue)
                    {
                        var product = proxyP.GetProduct(item.ProductId);
                        if (product != null)
                        {
                            buyOrder.BuyOrderDetails.Add(new BuyOrderDetail
                            {
                                ProductId = product.ProductId,
                                Name = product.Name,
                                UnitPrice = product.UnitPrice,
                                Quantity = 10
                            });   
                        }
                    }
                    if (buyOrder.BuyOrderDetails.Count > 0)
                    {
                        var proxyB = new BuyOrderServiceClient();
                        proxyB.CreateBuyOrder(buyOrder);   
                    }
                }
            }

            int pageSize = 6;
            int pageNumber = (page ?? 1);
            return View(products.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public JsonResult AddCart(int? productId)
        {
            if (!productId.HasValue)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Result = "Error" });
            }
            try
            {
                var proxyP = new ProductServiceClient();
                var product = proxyP.GetProduct(productId.Value);
                if (product == null)
                {
                    Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return Json(new { Result = "Error" });
                }
                var proxyCu = new CustomerServiceClient();
                var customer = proxyCu.GetCustomerByEmail(User.Identity.GetUserName());
                var proxyC = new CartItemServiceClient();
                var cartItem = proxyC.GetCartItemByProductIdAndCustomerId(productId.Value, customer.CustomerId);
                if (cartItem == null)
                {
                    proxyC.CreateCartItem(new CartItem
                    {
                        CustomerId = customer.CustomerId,
                        DateCreated = DateTime.Now,
                        Quantity = 1,
                        ProductId = productId.Value
                    });
                }
                else
                {
                    cartItem.Quantity += 1;
                    proxyC.EditCartItem(cartItem);
                }
                return Json(new { Result = "Ok" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
    }
}