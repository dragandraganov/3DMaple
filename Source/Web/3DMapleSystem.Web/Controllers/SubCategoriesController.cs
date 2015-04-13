using System;
using System.Linq;
using System.Web.Mvc;
using _3DMapleSystem.Data;
using AutoMapper.QueryableExtensions;
using _3DMapleSystem.Web.ViewModels.PolyModels;
using _3DMapleSystem.Web.Infrastructure.Popularizers;

namespace _3DMapleSystem.Web.Controllers
{
    public class SubCategoriesController : BaseController
    {
        public SubCategoriesController(_3DMapleSystemData data, IListPopulizer populizer)
            : base(data, populizer)
        {
        }

        // GET: SubCategories
        public ActionResult Index(string categoryName, string subCategoryName)
        {
            var allModelsInSubCategory = this.Populizer.GetPolyModels()
                .AsQueryable()
                .OrderByDescending(m => m.CreatedOn)
                .Where(pm => pm.SubCategory.Category.Name.ToLower() == categoryName.ToLower())
                .Where(pm => pm.SubCategory.Name.ToLower() == subCategoryName.ToLower())
                .Project()
                .To<SimplePolyModelViewModel>()
                .ToList();

            return View(allModelsInSubCategory);
        }
    }
}