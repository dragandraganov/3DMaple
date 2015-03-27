using _3DMapleSystem.Data;
using _3DMapleSystem.Web.Areas.Administration.ViewModels;
using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using _3DMapleSystem.Data.Models;
using System.Web;
using _3DMapleSystem.Common;

namespace _3DMapleSystem.Web.Areas.Administration.Controllers
{
    [Authorize(Roles = GlobalConstants.AdminRole)]
    public class SubCategoriesController : AdminController
    {

        public SubCategoriesController(_3DMapleSystemData data)
            : base(data)
        {
        }

        // GET: Administration/SubCategories
        public ActionResult Index()
        {
            var allSubCategories = this.Data
               .SubCategories
               .AllWithDeleted()
               .Project()
               .To<SubCategoryViewModel>();

            return View(allSubCategories);
        }

        //GET: Add new party game
        public ActionResult Add()
        {
            var newPartyGameViewModel = new SubCategoryViewModel
            {
                Categories = this.GetCategories()
            };

            return View(newPartyGameViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(SubCategoryViewModel subCategory)
        {
            if (subCategory != null && ModelState.IsValid)
            {
                var subCategoryWithSameName = this.Data.SubCategories
                    .All()
                    .Where(c => c.Name == subCategory.Name)
                    .FirstOrDefault();

                if (subCategoryWithSameName != null)
                {
                    ModelState.AddModelError(string.Empty, "Sub-category with same name already exists !");
                }

                else
                {
                    var newSubCategory = Mapper.Map<SubCategory>(subCategory);
                    this.Data.SubCategories.Add(newSubCategory);
                    this.Data.SaveChanges();
                    TempData["Success"] = "A new sub-category '" + subCategory.Name + "' was created";
                    return RedirectToAction("Index", "SubCategories");
                }
            }

            subCategory.Categories = this.GetCategories();
            return View(subCategory);
        }

        //GET Edit sub-category
        public ActionResult Edit(int id)
        {
            var existingSubCategory = this.Data
                .SubCategories
                .AllWithDeleted()
                .Where(pg => pg.Id == id)
                .Project()
                .To<SubCategoryViewModel>()
                .FirstOrDefault();
            if (existingSubCategory == null)
            {
                throw new HttpException(404, "Party game not found");
            }
            var allCategories = this.GetCategories();
            existingSubCategory.Categories = new SelectList(allCategories, "Value", "Text");
            return View(existingSubCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SubCategoryViewModel subCategory)
        {
            if (subCategory != null && ModelState.IsValid)
            {
                var otherSubCategoryWithSameName = this.Data.SubCategories
                    .All()
                    .Where(sc => sc.Name == subCategory.Name && sc.Id != subCategory.Id)
                    .FirstOrDefault();

                if (otherSubCategoryWithSameName != null)
                {
                    ModelState.AddModelError(string.Empty, "Sub-category with this name already exists !");
                }

                else
                {
                    var existingSubCategory = this.Data
                        .SubCategories
                        .GetById(subCategory.Id);
                    Mapper.Map(subCategory, existingSubCategory);

                    this.Data.SubCategories.Update(existingSubCategory);
                    this.Data.SaveChanges();
                    TempData["Success"] = "The sub-category '" + subCategory.Name + "' was edited";
                    return RedirectToAction("Index", "SubCategories");
                }
            }

            subCategory.Categories = this.GetCategories();
            return View(subCategory);
        }

        //GET Delete sub-category
        public ActionResult Delete(int id)
        {
            var existingPartyGame = this.Data
                .SubCategories
                .AllWithDeleted()
                .Where(sc => sc.Id == id)
                .Project()
                .To<SubCategoryViewModel>()
                .FirstOrDefault();

            if (existingPartyGame == null)
            {
                throw new HttpException(404, "Party game not found");
            }

            return View(existingPartyGame);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(SubCategoryViewModel subCategory)
        {
            if (subCategory != null && ModelState.IsValid)
            {
                var existingsubCategory = this.Data
                    .SubCategories
                    .GetById(subCategory.Id);

                this.Data.SubCategories.Delete(existingsubCategory);
                this.Data.SaveChanges();
                TempData["Success"] = "The sub-category '" + subCategory.Name + "' was deleted";

                return RedirectToAction("Index", "SubCategories");
            }

            return View(subCategory);
        }

        //GET Hard delete sub-category
        public ActionResult HardDelete(int id)
        {
            return this.Delete(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HardDelete(SubCategoryViewModel subCategory)
        {
            if (subCategory != null && ModelState.IsValid)
            {
                var existingsubCategory = this.Data
                    .SubCategories
                    .GetById(subCategory.Id);

                this.Data.SubCategories.ActualDelete(existingsubCategory);
                this.Data.SaveChanges();
                TempData["Success"] = "The sub-category '" + subCategory.Name + "' was hard deleted";

                return RedirectToAction("Index", "SubCategories");
            }

            return View(subCategory);
        }
    }
}