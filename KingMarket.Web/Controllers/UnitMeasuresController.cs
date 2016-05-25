using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KingMarket.Model.Models;
using KingMarket.Web.UnitMeasureService;
using PagedList;

namespace KingMarket.Web.Controllers
{
    public class UnitMeasuresController : Controller
    {
        // GET: UnitMeasures
        [Authorize(Roles = "Admin")]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var proxy = new UnitMeasureServiceClient();

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name_Desc" : String.Empty;
            ViewBag.ShortNameSortParm = sortOrder == "Short Name" ? "ShortName_Desc" : "Short Name";

            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;

            ViewBag.CurrentFilter = searchString;

            var unitMeasures =
                String.IsNullOrEmpty(searchString) ?
                proxy.GetUnitMeasures().OrderBy(c => c.Name) :
                proxy.GetUnitMeasures().OrderBy(c => c.Name).Where(s =>
                    s.ShortName.Contains(searchString) ||
                    s.Name.Contains(searchString));

            switch (sortOrder)
            {
                case "Name_Desc":
                    unitMeasures = unitMeasures.OrderByDescending(p => p.Name);
                    break;
                case "Short Name":
                    unitMeasures = unitMeasures.OrderBy(p => p.ShortName);
                    break;
                case "ShortName_Desc":
                    unitMeasures = unitMeasures.OrderByDescending(p => p.ShortName);
                    break;
                default:
                    unitMeasures = unitMeasures.OrderBy(p => p.Name);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(unitMeasures.ToPagedList(pageNumber, pageSize));
        }

        // GET: UnitMeasures/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var proxy = new UnitMeasureServiceClient();
            var unitMeasure = proxy.GetUnitMeasure(id.Value);
            if (unitMeasure == null)
                return HttpNotFound();
            return View(unitMeasure);
        }

        // GET: UnitMeasures/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "UnitMeasureId,ShortName,Name")] UnitMeasure unitMeasure)
        {
            if (ModelState.IsValid)
            {
                var proxy = new UnitMeasureServiceClient();
                proxy.CreateUnitMeasure(unitMeasure);
                return RedirectToAction("Index");
            }

            return View(unitMeasure);
        }

        // GET: UnitMeasures/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var proxy = new UnitMeasureServiceClient();
            var unitMeasure = proxy.GetUnitMeasure(id.Value);
            if (unitMeasure == null)
                return HttpNotFound();
            return View(unitMeasure);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "UnitMeasureId,ShortName,Name")] UnitMeasure unitMeasure)
        {
            if (ModelState.IsValid)
            {
                var proxy = new UnitMeasureServiceClient();
                proxy.EditUnitMeasure(unitMeasure);
                return RedirectToAction("Index");
            }
            return View(unitMeasure);
        }

        // GET: UnitMeasures/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var proxy = new UnitMeasureServiceClient();
            var unitMeasure = proxy.GetUnitMeasure(id.Value);
            if (unitMeasure == null)
                return HttpNotFound();
            return View(unitMeasure);
        }

        // POST: UnitMeasures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            var proxy = new UnitMeasureServiceClient();
            proxy.DeleteUnitMeasure(id);
            return RedirectToAction("Index");
        }
    }
}