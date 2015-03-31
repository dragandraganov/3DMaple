using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _3DMapleSystem.Data;
using AutoMapper.QueryableExtensions;
using _3DMapleSystem.Web.ViewModels.PolyModels;

namespace _3DMapleSystem.Web.Controllers
{
    public class CategoriesController : BaseController
    {
        public CategoriesController(_3DMapleSystemData data)
            : base(data)
        {
        }

        // GET: Categories
        public ActionResult Index(int id)
        {
            var allModelsInCategory = this.Data.PolyModels
                .All()
                .Where(pm => pm.SubCategory.CategoryId == id)
                .Project()
                .To<SimplePolyModelViewModel>()
                .ToList();

            return View(allModelsInCategory);
        }
    }
}