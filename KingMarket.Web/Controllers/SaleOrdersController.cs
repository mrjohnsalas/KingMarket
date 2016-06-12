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
using KingMarket.Web.CustomerService;
using KingMarket.Web.ProductService;
using KingMarket.Web.ProductTypeService;
using KingMarket.Web.SaleOrderDetailService;
using KingMarket.Web.SaleOrderService;
using KingMarket.Web.UnitMeasureService;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;

namespace KingMarket.Web.Controllers
{
    public class SaleOrdersController : Controller
    {
        [Authorize(Roles = "Admin, Customer")]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var proxy = new SaleOrderServiceClient();

            ViewBag.CurrentSort = sortOrder;
            ViewBag.DateOrderSortParm = String.IsNullOrEmpty(sortOrder) ? "DateOrder_Desc" : String.Empty;
            ViewBag.DocumentTypeSortParm = sortOrder == "Document Type" ? "DocumentType_Desc" : "Document Type";
            ViewBag.DocumentNumberSortParm = sortOrder == "Document Number" ? "DocumentNumber_Desc" : "Document Number";

            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;

            ViewBag.CurrentFilter = searchString;

            var proxyC = new CustomerServiceClient();
            var customer = proxyC.GetCustomerByEmail(User.Identity.GetUserName());
            var saleOrders = proxy.GetSaleOrdersByCustomerId(customer.CustomerId);
            if (saleOrders != null)
            {
                var proxyD = new DocumentTypeServiceClient();
                foreach (var item in saleOrders)
                    item.DocumentType = proxyD.GetDocumentType(item.DocumentTypeId);

                if (!String.IsNullOrEmpty(searchString))
                    saleOrders = saleOrders.FindAll(s =>
                        s.DateOrder.ToShortDateString().Contains(searchString) ||
                        s.DocumentType.Name.Contains(searchString) ||
                        s.DocumentNumber.Contains(searchString));

                switch (sortOrder)
                {
                    case "DateOrder_Desc":
                        saleOrders = saleOrders.OrderByDescending(p => p.DateOrder).ToList();
                        break;
                    case "Document Type":
                        saleOrders = saleOrders.OrderBy(p => p.DocumentType.Name).ToList();
                        break;
                    case "DocumentType_Desc":
                        saleOrders = saleOrders.OrderByDescending(p => p.DocumentType.Name).ToList();
                        break;
                    case "Document Number":
                        saleOrders = saleOrders.OrderBy(p => p.DocumentNumber).ToList();
                        break;
                    case "DocumentNumber_Desc":
                        saleOrders = saleOrders.OrderByDescending(p => p.DocumentNumber).ToList();
                        break;
                    default:
                        saleOrders = saleOrders.OrderBy(p => p.DateOrder).ToList();
                        break;
                }

                var proxyOd = new SaleOrderDetailServiceClient();
                foreach (var item in saleOrders)
                    item.SaleOrderDetails = proxyOd.GetSaleOrderDetailsBySaleOrderId(item.SaleOrderId);
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(saleOrders.ToPagedList(pageNumber, pageSize));
        }

        [Authorize(Roles = "Admin, Customer")]
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var proxy = new SaleOrderServiceClient();
            var saleOrder = proxy.GetSaleOrder(id.Value);
            if (saleOrder == null)
                return HttpNotFound();
            var proxyD = new DocumentTypeServiceClient();
            saleOrder.DocumentType = proxyD.GetDocumentType(saleOrder.DocumentTypeId);
            var proxyOd = new SaleOrderDetailServiceClient();
            saleOrder.SaleOrderDetails = proxyOd.GetSaleOrderDetailsBySaleOrderId(saleOrder.SaleOrderId);
            if (saleOrder.SaleOrderDetails != null)
            {
                var proxyP = new ProductServiceClient();
                foreach (var item in saleOrder.SaleOrderDetails)
                {
                    item.Product = proxyP.GetProduct(item.ProductId);
                    if (item.Product != null)
                    {
                        var proxyU = new UnitMeasureServiceClient();
                        item.Product.UnitMeasure = proxyU.GetUnitMeasure(item.Product.UnitMeasureId);
                        var proxyT = new ProductTypeServiceClient();
                        item.Product.ProductType = proxyT.GetProductType(item.Product.ProductTypeId);
                    }
                }
            }
            return View(saleOrder);
        }
    }
}