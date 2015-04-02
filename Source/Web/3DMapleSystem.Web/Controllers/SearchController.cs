using _3DMapleSystem.Web.Infrastructure.Popularizers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using _3DMapleSystem.Web.ViewModels.PolyModels;
using _3DMapleSystem.Web.ViewModels.ComplexModels;
using _3DMapleSystem.Web.ViewModels;

namespace _3DMapleSystem.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly IListPopulizer populizer;

        public SearchController(IListPopulizer populizer)
        {
            this.populizer = populizer;
        }

        // GET: Search
        public ActionResult Index(string query)
        {
            var complexModel = new SearchComplexViewModel();

            var tagsMatchQuery = this.populizer.GetTags()
                .Where(t => t.Name.StartsWith(query)).Select(tag => tag.Name);

            var polyModels = this.populizer.GetPolyModels()
                .Where(pm => pm.Tags.Select(tag => tag.Name).Intersect(tagsMatchQuery).Any())
                .AsQueryable();

            complexModel.PolyModels = polyModels
                .Project()
                .To<SimplePolyModelViewModel>()
                .ToList();

            complexModel.SubCategories = polyModels
                .Select(pm => pm.SubCategory)
                .GroupBy(sc => sc.Id)
                .Select(grp => grp.First())
                .Project()
                .To<SubCategoryViewModel>()
                .ToList();

            return View(complexModel);
        }
    }
}