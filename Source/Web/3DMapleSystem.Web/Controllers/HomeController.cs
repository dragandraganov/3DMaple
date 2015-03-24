using System;
using System.Linq;
using System.Web.Mvc;
using _3DMapleSystem.Data;
using _3DMapleSystem.Web.ViewModels;
using AutoMapper.QueryableExtensions;
using _3DMapleSystem.Web.ViewModels.PolyModels;

namespace _3DMapleSystem.Web.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(_3DMapleSystemData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            var homePageModel = new HomePageViewModel();

            homePageModel.Categories = this.Data.Categories
                .All()
                .ToList();

            homePageModel.PolyModels = this.Data.PolyModels
                .All()
                .OrderByDescending(m => m.CreatedOn)
                .Project()
                .To<SimplePolyModelViewModel>()
                .ToList();

            return View(homePageModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}