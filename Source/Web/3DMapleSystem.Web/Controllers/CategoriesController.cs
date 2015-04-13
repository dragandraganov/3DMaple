using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _3DMapleSystem.Data;
using AutoMapper.QueryableExtensions;
using _3DMapleSystem.Web.Infrastructure.Popularizers;
using _3DMapleSystem.Web.ViewModels.PolyModels;

namespace _3DMapleSystem.Web.Controllers
{
    public class CategoriesController : BaseController
    {
        public CategoriesController(_3DMapleSystemData data, IListPopulizer populizer)
            : base(data, populizer)
        {
        }

        // GET: Categories
        public ActionResult Index(string name)
        {
            var allModelsInCategory = this.Populizer.GetPolyModels()
                .AsQueryable()
                .OrderByDescending(m => m.CreatedOn)
                .Where(pm => pm.SubCategory.Category.Name.ToLower() == name.ToLower())
                .Project()
                .To<SimplePolyModelViewModel>()
                .ToList();

            return View(allModelsInCategory);
        }
    }
}