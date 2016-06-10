using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;
using KingMarket.Model.Models;
using KingMarket.Web.BuyOrderService;
using KingMarket.Web.DocumentTypeService;
using KingMarket.Web.ProductService;
using KingMarket.Web.ProductTypeService;
using KingMarket.Web.SupplierService;
using KingMarket.Web.UnitMeasureService;
using Microsoft.AspNet.Identity;

namespace KingMarket.Web.Controllers
{
    public class BuyOrdersController : Controller
    {
        // GET: BuyOrders
        [Authorize(Roles = "Admin, Buyer")]
        public ActionResult Index()
        {
            return View();
        }

        // GET: Customers/Create
        [Authorize(Roles = "Admin, Buyer")]
        public ActionResult Create()
        {
            var buyOrder = new BuyOrder
            {
                Supplier = new Supplier(),
                DocumentType = new DocumentType(),
                Status = new Status(),
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
        public ActionResult Create(BuyOrder buyOrder)
        {
            buyOrder = Session["BuyOrder"] as BuyOrder;
            var proxyS = new SupplierServiceClient();
            var proxyD = new DocumentTypeServiceClient();
            if (ModelState.IsValid)
            {
                if (buyOrder.BuyOrderDetails == null || buyOrder.BuyOrderDetails.Count.Equals(0))
                {
                    buyOrder.BuyOrderDetails = new List<BuyOrderDetail>();
                    ViewBag.SupplierId = new SelectList(proxyS.GetSuppliers().OrderBy(d => d.BusinessName), "SupplierId", "BusinessName", buyOrder.SupplierId);
                    ViewBag.DocumentTypeId = new SelectList(proxyD.GetDocumentTypes().OrderBy(d => d.Name).ToList().FindAll(d => d.ClassDocumentTypeId.Equals(4) && !d.OnlyForEnterprise), "DocumentTypeId", "Name", buyOrder.DocumentTypeId);
                    ViewBag.ErrorCode = "Error Code: UI";
                    ViewBag.ErrorMessage = "Error Message: You need to add products to the list.";
                    return View(buyOrder);   
                }
                var proxyB = new BuyOrderServiceClient();
                proxyB.CreateBuyOrder(buyOrder);
                ViewBag.OkMessage = String.Format("Sucess Message: Buy order: {0} has been successfully created.", buyOrder.BuyOrderId);
                ViewBag.SupplierId = new SelectList(proxyS.GetSuppliers().OrderBy(d => d.BusinessName), "SupplierId", "BusinessName", buyOrder.SupplierId);
                ViewBag.DocumentTypeId = new SelectList(proxyD.GetDocumentTypes().OrderBy(d => d.Name).ToList().FindAll(d => d.ClassDocumentTypeId.Equals(4) && !d.OnlyForEnterprise), "DocumentTypeId", "Name", buyOrder.DocumentTypeId);
                return View();
            }
            ViewBag.SupplierId = new SelectList(proxyS.GetSuppliers().OrderBy(d => d.BusinessName), "SupplierId", "BusinessName", buyOrder.SupplierId);
            ViewBag.DocumentTypeId = new SelectList(proxyD.GetDocumentTypes().OrderBy(d => d.Name).ToList().FindAll(d => d.ClassDocumentTypeId.Equals(4) && !d.OnlyForEnterprise), "DocumentTypeId", "Name", buyOrder.DocumentTypeId);
            return View(buyOrder);
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