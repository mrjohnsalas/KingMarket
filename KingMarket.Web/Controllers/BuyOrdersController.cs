using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;
using KingMarket.Model.Models;
using KingMarket.Web.BuyOrderDetailService;
using KingMarket.Web.BuyOrderService;
using KingMarket.Web.DocumentTypeService;
using KingMarket.Web.EmployeeService;
using KingMarket.Web.ProductService;
using KingMarket.Web.ProductTypeService;
using KingMarket.Web.SaleOrderDetailService;
using KingMarket.Web.SaleOrderService;
using KingMarket.Web.SupplierService;
using KingMarket.Web.UnitMeasureService;
using Microsoft.AspNet.Identity;
using PagedList;

namespace KingMarket.Web.Controllers
{
    public class BuyOrdersController : Controller
    {
        // GET: BuyOrders
        [Authorize(Roles = "Admin, Buyer, Supplier")]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var proxy = new BuyOrderServiceClient();

            ViewBag.CurrentSort = sortOrder;
            ViewBag.DateOrderSortParm = String.IsNullOrEmpty(sortOrder) ? "DateOrder_Desc" : String.Empty;
            ViewBag.DocumentTypeSortParm = sortOrder == "Document Type" ? "DocumentType_Desc" : "Document Type";
            ViewBag.BusinessNameSortParm = sortOrder == "Business Name" ? "BusinessName_Desc" : "Business Name";

            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;

            ViewBag.CurrentFilter = searchString;
            var proxyS = new SupplierServiceClient();
            List<BuyOrder> buyOrders = null;
            if (User.IsInRole("Buyer"))
                buyOrders = proxy.GetBuyOrdersByUserId(User.Identity.GetUserId());
            else
            {
                var supplier = proxyS.GetSupplierByEmail(User.Identity.GetUserName());
                proxy.GetBuyOrdersBySupplierId(supplier.SupplierId);
            }
            if (buyOrders != null)
            {
                var proxyD = new DocumentTypeServiceClient();
                foreach (var item in buyOrders)
                {
                    item.DocumentType = proxyD.GetDocumentType(item.DocumentTypeId);
                    item.Supplier = proxyS.GetSupplier(item.SupplierId);
                }

                if (!String.IsNullOrEmpty(searchString))
                    buyOrders = buyOrders.FindAll(s =>
                        s.DateOrder.ToShortDateString().Contains(searchString) ||
                        s.DocumentType.Name.Contains(searchString) ||
                        s.Supplier.BusinessName.Contains(searchString));
                switch (sortOrder)
                {
                    case "DateOrder_Desc":
                        buyOrders = buyOrders.OrderByDescending(p => p.DateOrder).ToList();
                        break;
                    case "Document Type":
                        buyOrders = buyOrders.OrderBy(p => p.DocumentType.Name).ToList();
                        break;
                    case "DocumentType_Desc":
                        buyOrders = buyOrders.OrderByDescending(p => p.DocumentType.Name).ToList();
                        break;
                    case "Business Name":
                        buyOrders = buyOrders.OrderBy(p => p.Supplier.BusinessName).ToList();
                        break;
                    case "BusinessName_Desc":
                        buyOrders = buyOrders.OrderByDescending(p => p.Supplier.BusinessName).ToList();
                        break;
                    default:
                        buyOrders = buyOrders.OrderBy(p => p.DateOrder).ToList();
                        break;
                }

                var proxyOd = new BuyOrderDetailServiceClient();
                foreach (var item in buyOrders)
                    item.BuyOrderDetails = proxyOd.GetBuyOrderDetailsByBuyOrderId(item.BuyOrderId);
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(buyOrders.ToPagedList(pageNumber, pageSize));
        }

        [Authorize(Roles = "Admin, Buyer, Supplier")]
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var proxy = new BuyOrderServiceClient();
            var buyOrder = proxy.GetBuyOrder(id.Value);
            if (buyOrder == null)
                return HttpNotFound();
            var proxyD = new DocumentTypeServiceClient();
            buyOrder.DocumentType = proxyD.GetDocumentType(buyOrder.DocumentTypeId);
            var proxyS = new SupplierServiceClient();
            buyOrder.Supplier = proxyS.GetSupplier(buyOrder.SupplierId);
            var proxyOd = new BuyOrderDetailServiceClient();
            buyOrder.BuyOrderDetails = proxyOd.GetBuyOrderDetailsByBuyOrderId(buyOrder.BuyOrderId);
            if (buyOrder.BuyOrderDetails != null)
            {
                var proxyP = new ProductServiceClient();
                foreach (var item in buyOrder.BuyOrderDetails)
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
            return View(buyOrder);
        }

        // GET: Customers/Create
        [Authorize(Roles = "Admin, Buyer")]
        public ActionResult Create()
        {
            var buyOrder = new BuyOrder
            {
                BuyOrderDetails = new List<BuyOrderDetail>()
            };
            Session["BuyOrder"] = buyOrder;
            var proxyS = new SupplierServiceClient();
            var proxyD = new DocumentTypeServiceClient();
            ViewBag.SupplierId = new SelectList(proxyS.GetSuppliers().OrderBy(d => d.BusinessName), "SupplierId", "BusinessName");
            ViewBag.DocumentTypeId = new SelectList(proxyD.GetDocumentTypes().OrderBy(d => d.Name).ToList().FindAll(d => d.ClassDocumentTypeId.Equals(4) && !d.OnlyForEnterprise), "DocumentTypeId", "Name");
            return View(buyOrder);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Buyer")]
        public ActionResult Create([Bind(Include = "SupplierId,DocumentTypeId")]BuyOrder buyOrder)    
        {
            var buyOrderSession = Session["BuyOrder"] as BuyOrder;
            buyOrderSession.SupplierId = buyOrder.SupplierId;
            buyOrderSession.DocumentTypeId = buyOrder.DocumentTypeId;

            var proxyS = new SupplierServiceClient();
            var proxyD = new DocumentTypeServiceClient();
            try
            {
                if (buyOrderSession.BuyOrderDetails == null || buyOrderSession.BuyOrderDetails.Count.Equals(0))
                {
                    buyOrderSession.BuyOrderDetails = new List<BuyOrderDetail>();
                    ViewBag.SupplierId = new SelectList(proxyS.GetSuppliers().OrderBy(d => d.BusinessName), "SupplierId", "BusinessName", buyOrderSession.SupplierId);
                    ViewBag.DocumentTypeId = new SelectList(proxyD.GetDocumentTypes().OrderBy(d => d.Name).ToList().FindAll(d => d.ClassDocumentTypeId.Equals(4) && !d.OnlyForEnterprise), "DocumentTypeId", "Name", buyOrderSession.DocumentTypeId);
                    ViewBag.ErrorCode = "Error Code: UI";
                    ViewBag.ErrorMessage = "Error Message: You need to add products to the list.";
                    return View(buyOrderSession);
                }
                buyOrderSession.UserId = User.Identity.GetUserId();
                buyOrderSession.StatusId = 6;
                var proxyB = new BuyOrderServiceClient();
                proxyB.CreateBuyOrder(buyOrderSession);
                ViewBag.OkMessage = String.Format("Sucess Message: Buy order: {0} has been successfully created.", buyOrderSession.BuyOrderId);
                ViewBag.SupplierId = new SelectList(proxyS.GetSuppliers().OrderBy(d => d.BusinessName), "SupplierId", "BusinessName", buyOrderSession.SupplierId);
                ViewBag.DocumentTypeId = new SelectList(proxyD.GetDocumentTypes().OrderBy(d => d.Name).ToList().FindAll(d => d.ClassDocumentTypeId.Equals(4) && !d.OnlyForEnterprise), "DocumentTypeId", "Name", buyOrderSession.DocumentTypeId);
                buyOrder = new BuyOrder();
                buyOrder.BuyOrderDetails = new List<BuyOrderDetail>();
                Session["BuyOrder"] = buyOrder;
                return View(buyOrder);
            }
            catch (FaultException<GeneralException> ex)
            {
                ViewBag.ErrorCode = String.Format("Error Code: {0}", ex.Detail.Id);
                ViewBag.ErrorMessage = String.Format("Error Message: {0}", ex.Detail.Description);
            }
            ViewBag.SupplierId = new SelectList(proxyS.GetSuppliers().OrderBy(d => d.BusinessName), "SupplierId", "BusinessName", buyOrderSession.SupplierId);
            ViewBag.DocumentTypeId = new SelectList(proxyD.GetDocumentTypes().OrderBy(d => d.Name).ToList().FindAll(d => d.ClassDocumentTypeId.Equals(4) && !d.OnlyForEnterprise), "DocumentTypeId", "Name", buyOrderSession.DocumentTypeId);
            return View(buyOrderSession);
        }

        [Authorize(Roles = "Admin, Buyer")]
        public ActionResult AddProductOrder()
        {
            var proxyP = new ProductServiceClient();
            ViewBag.ProductId = new SelectList(proxyP.GetProducts().OrderBy(d => d.Name), "ProductId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Buyer")]
        public ActionResult AddProductOrder(ProductOrder productOrder)
        {
            var proxyP = new ProductServiceClient();
            try
            {
                if (productOrder.Quantity <= 0)
                {
                    ViewBag.ProductId = new SelectList(proxyP.GetProducts().OrderBy(d => d.Name), "ProductId", "Name", productOrder.ProductId);
                    ViewBag.ErrorCode = "Error Code: UI";
                    ViewBag.ErrorMessage = "Error Message: The quantity must be greater than 0.";
                    return View(productOrder);
                }
                var buyOrder = Session["BuyOrder"] as BuyOrder;
                if (buyOrder == null)
                {
                    ViewBag.ProductId = new SelectList(proxyP.GetProducts().OrderBy(d => d.Name), "ProductId", "Name", productOrder.ProductId);
                    return View(productOrder);
                }
                var buyOrderDetail = buyOrder.BuyOrderDetails.FirstOrDefault(p => p.ProductId.Equals(productOrder.ProductId));
                if (buyOrderDetail == null)
                {
                    var product = proxyP.GetProduct(productOrder.ProductId);
                    if (product == null)
                    {
                        ViewBag.ProductId = new SelectList(proxyP.GetProducts().OrderBy(d => d.Name), "ProductId", "Name", productOrder.ProductId);
                        return View(productOrder);
                    }
                    var proxyT = new ProductTypeServiceClient();
                    var proxyU = new UnitMeasureServiceClient();
                    product.ProductType = proxyT.GetProductType(product.ProductTypeId);
                    product.UnitMeasure = proxyU.GetUnitMeasure(product.UnitMeasureId);
                    buyOrder.BuyOrderDetails.Add(new BuyOrderDetail
                    {
                        ProductId = product.ProductId,
                        Name = product.Name,
                        UnitPrice = product.UnitPrice,
                        Quantity = productOrder.Quantity,
                        Product = product
                    });
                }
                else
                    buyOrderDetail.Quantity += productOrder.Quantity;
                var proxyS = new SupplierServiceClient();
                var proxyD = new DocumentTypeServiceClient();
                ViewBag.SupplierId = new SelectList(proxyS.GetSuppliers().OrderBy(d => d.BusinessName), "SupplierId", "BusinessName", buyOrder.SupplierId);
                ViewBag.DocumentTypeId = new SelectList(proxyD.GetDocumentTypes().OrderBy(d => d.Name).ToList().FindAll(d => d.ClassDocumentTypeId.Equals(4) && !d.OnlyForEnterprise), "DocumentTypeId", "Name", buyOrder.DocumentTypeId);
                return View("Create", buyOrder);
            }
            catch (FaultException<GeneralException> ex)
            {
                ViewBag.ErrorCode = String.Format("Error Code: {0}", ex.Detail.Id);
                ViewBag.ErrorMessage = String.Format("Error Message: {0}", ex.Detail.Description);
            }
            ViewBag.ProductId = new SelectList(proxyP.GetProducts().OrderBy(d => d.Name), "ProductId", "Name", productOrder.ProductId);
            return View(productOrder);
        }
    }
}