using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KingMarket.Model.Models;
using KingMarket.Web.ProductPhotoService;

namespace KingMarket.Web.Controllers
{
    // GET: ProductPhotos
    public class ProductPhotosController : Controller
    {
        // GET: ProductPhotos
        public ActionResult Index(int id)
        {
            var proxy = new ProductPhotoServiceClient();
            var fileToRetrieve = proxy.GetProductPhoto(id);
            return File(fileToRetrieve.Content, fileToRetrieve.ContentType);
        }
    }
}