using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Northwnd.Models;
using Northwnd.Service.Interface;
using Northwnd.Service;

namespace Northwnd.Web.Controllers
{
    public class CategoriesController : Controller
    {
        //private readonly ICategoryService categoryService;
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService service)
        {
            this._categoryService = service;
        }

        // GET: Categories
        public ActionResult Index()
        {
            return View(_categoryService.GetList(x => true));
        }

        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Category category = _categoryService.GetInfo(x => x.CategoryID == id.Value);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryID,CategoryName,Description,Picture")] Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryService.Create(category);
                _categoryService.SaveChange();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = _categoryService.GetInfo(x => x.CategoryID == id.Value);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "CategoryID,CategoryName,Description,Picture")] Category category)
        //{
        //    if (ModelState.IsValid)
        //    {

        //        _categoryService.Update(category);
        //        return RedirectToAction("Index");
        //    }
        //    return View(category);
        //}

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var category = _categoryService.GetInfo(x => x.CategoryID == id.Value);

            return View(category);
        }

        //// POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _categoryService.Delete(x => x.CategoryID == id);
            _categoryService.SaveChange();
            return RedirectToAction("Index");
        }


    }
}
