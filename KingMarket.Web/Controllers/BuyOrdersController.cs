using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;
using KingMarket.Model.Models;
using KingMarket.Web.DocumentTypeService;
using KingMarket.Web.ProductService;
using KingMarket.Web.SupplierService;

namespace KingMarket.Web.Controllers
{
    public class BuyOrdersController : Controller
    {
        // GET: BuyOrders
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }

        // GET: Customers/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var buyOrder = new BuyOrder
            {
                DateOrder = DateTime.Now,
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
            ViewBag.DateOrder = DateTime.Now;
            return View(buyOrder);
            //return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(BuyOrder buyOrder)
        {
            var proxyS = new SupplierServiceClient();
            var proxyD = new DocumentTypeServiceClient();
            ViewBag.SupplierId = new SelectList(proxyS.GetSuppliers().OrderBy(d => d.BusinessName), "SupplierId", "BusinessName", buyOrder.SupplierId);
            ViewBag.DocumentTypeId = new SelectList(proxyD.GetDocumentTypes().OrderBy(d => d.Name).ToList().FindAll(d => d.ClassDocumentTypeId.Equals(4) && !d.OnlyForEnterprise), "DocumentTypeId", "Name", buyOrder.DocumentTypeId);
            return View(buyOrder);
        }

        public ActionResult AddProductOrder()
        {
            var proxyP = new ProductServiceClient();
            ViewBag.ProductId = new SelectList(proxyP.GetProducts().OrderBy(d => d.Name), "ProductId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult AddProductOrder(ProductOrder productOrder)
        {
            var proxyP = new ProductServiceClient();
            try
            {
                if (productOrder.ProductId > 0 && productOrder.Quantity > 0)
                {
                    var product = proxyP.GetProduct(productOrder.ProductId);
                    if (product == null)
                    {
                        ViewBag.ProductId = new SelectList(proxyP.GetProducts().OrderBy(d => d.Name), "ProductId", "Name", productOrder.ProductId);
                        return View(productOrder);
                    }
                    var buyOrder = Session["BuyOrder"] as BuyOrder;
                    if (buyOrder == null)
                    {
                        ViewBag.ProductId = new SelectList(proxyP.GetProducts().OrderBy(d => d.Name), "ProductId", "Name", productOrder.ProductId);
                        return View(productOrder);
                    }
                    buyOrder.BuyOrderDetails.Add(new BuyOrderDetail
                    {
                        ProductId = product.ProductId,
                        Name = product.Name,
                        UnitPrice = product.UnitPrice,
                        Quantity = productOrder.Quantity,
                        Product = product
                    });
                    var proxyS = new SupplierServiceClient();
                    var proxyD = new DocumentTypeServiceClient();
                    ViewBag.SupplierId = new SelectList(proxyS.GetSuppliers().OrderBy(d => d.BusinessName), "SupplierId", "BusinessName", buyOrder.SupplierId);
                    ViewBag.DocumentTypeId = new SelectList(proxyD.GetDocumentTypes().OrderBy(d => d.Name).ToList().FindAll(d => d.ClassDocumentTypeId.Equals(4) && !d.OnlyForEnterprise), "DocumentTypeId", "Name", buyOrder.DocumentTypeId);
                    return View("Create", buyOrder);
                }
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