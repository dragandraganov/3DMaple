using _3DMapleSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using _3DMapleSystem.Web.Areas.Administration.ViewModels;
using _3DMapleSystem.Data.Models;

namespace _3DMapleSystem.Web.Areas.Administration.Controllers
{
    public class CategoriesController : AdminController
    {
        public CategoriesController(_3DMapleSystemData data)
            : base(data)
        {
        }

        // GET: Administration/AdminCategories
        public ActionResult Index()
        {
            var allCategories = this.Data
                .Categories
                .AllWithDeleted()
                .Project()
                .To<CategoryViewModel>();

            return View(allCategories);
        }

        //GET: Add new category
        public ActionResult Add()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(CategoryViewModel category)
        {
            if (category != null && ModelState.IsValid)
            {
                var categoryWithSameName = this.Data.Categories
                    .All()
                    .Where(c => c.Name == category.Name)
                    .FirstOrDefault();

                if (categoryWithSameName != null)
                {
                    ModelState.AddModelError(string.Empty, "This category already exists !");
                }

                else
                {
                    var newCategory = Mapper.Map<Category>(category);
                    this.Data.Categories.Add(newCategory);
                    this.Data.SaveChanges();
                    TempData["Success"] = "A new category '" + category.Name + "' was created";
                    return RedirectToAction("Index", "Categories");
                }
            }

            return View();
        }

        //GET: Edit party game
        public ActionResult Edit(int id)
        {
            var existingCategory = this.Data
                .Categories
                .AllWithDeleted()
                .Where(pg => pg.Id == id)
                .Project()
                .To<CategoryViewModel>()
                .FirstOrDefault();
            if (existingCategory == null)
            {
                throw new HttpException(404, "Category not found");
            }

            return View(existingCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoryViewModel category)
        {
            if (category != null && ModelState.IsValid)
            {
                var otherCategoryWithSameName = this.Data.Categories
                    .All()
                    .Where(c => c.Name == category.Name&&c.Id!=category.Id)
                    .FirstOrDefault();

                if (otherCategoryWithSameName != null)
                {
                    ModelState.AddModelError(string.Empty, "This category already exists !");
                }

                else
                {
                    var existingCategory = this.Data
                        .Categories
                        .GetById(category.Id);
                    Mapper.Map(category, existingCategory);

                    this.Data.Categories.Update(existingCategory);
                    this.Data.SaveChanges();
                    TempData["Success"] = "The category '" + category.Name + "' was edited";
                    return RedirectToAction("Index", "Categories");
                }
            }

            return View(category);
        }

        //GET: Delete category

        public ActionResult Delete(int id)
        {
            var existingCategory = this.Data
                .Categories
                .AllWithDeleted()
                .Where(pg => pg.Id == id)
                .Project()
                .To<CategoryViewModel>()
                .FirstOrDefault();
            if (existingCategory == null)
            {
                throw new HttpException(404, "Category not found");
            }
            return View(existingCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(CategoryViewModel category)
        {
            if (category != null && ModelState.IsValid)
            {
                var existingCategory = this.Data
                    .Categories
                    .GetById(category.Id);
                this.Data.Categories.Delete(existingCategory);
                this.Data.SaveChanges();
                TempData["Success"] = "The category '" + category.Name + "' was deleted";
                return RedirectToAction("Index", "Categories");
            }

            return View(category);
        }

        public ActionResult HardDelete(int id)
        {
            return this.Delete(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HardDelete(CategoryViewModel category)
        {
            if (category != null && ModelState.IsValid)
            {
                var existingCategory = this.Data
                    .Categories
                    .GetById(category.Id);
                Mapper.Map(category, existingCategory);
                this.Data.Categories.ActualDelete(existingCategory);
                this.Data.SaveChanges();
                TempData["Success"] = "The category '" + category.Name + "' was hard deleted";
                return RedirectToAction("Index", "Categories");
            }

            return View(category);
        }
    }
}