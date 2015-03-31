using System;
using System.Linq;
using System.Web.Mvc;
using _3DMapleSystem.Data;
using AutoMapper.QueryableExtensions;
using _3DMapleSystem.Web.ViewModels.PolyModels;

namespace _3DMapleSystem.Web.Controllers
{
    public class SubCategoriesController : BaseController
    {
        public SubCategoriesController(_3DMapleSystemData data)
            : base(data)
        {
        }

        // GET: SubCategories
        public ActionResult Index(int id)
        {
            var allModelsInSubCategory = this.Data.PolyModels
                .All()
                .Where(pm => pm.SubCategoryId == id)
                .Project()
                .To<SimplePolyModelViewModel>()
                .ToList();

            return View(allModelsInSubCategory);
        }
    }
}