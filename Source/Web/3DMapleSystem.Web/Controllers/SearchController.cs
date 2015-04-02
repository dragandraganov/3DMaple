using _3DMapleSystem.Web.Infrastructure.Popularizers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using _3DMapleSystem.Web.ViewModels.PolyModels;

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
            var tagsMatchQuery = this.populizer.GetTags()
                .Where(t => t.Name.StartsWith(query));

            var polyModels = this.populizer.GetPolyModels()
                .Where(pm=>pm.Tags.Intersect(tagsMatchQuery).Any())
                .AsQueryable()
                .Project()
                .To<SimplePolyModelViewModel>()
                .ToList();

            return View(polyModels);
        }
    }
}