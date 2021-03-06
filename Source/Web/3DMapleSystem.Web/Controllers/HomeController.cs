﻿using System;
using System.Linq;
using System.Web.Mvc;
using _3DMapleSystem.Data;
using _3DMapleSystem.Web.ViewModels;
using AutoMapper.QueryableExtensions;
using _3DMapleSystem.Web.ViewModels.PolyModels;
using _3DMapleSystem.Web.Infrastructure.Popularizers;

namespace _3DMapleSystem.Web.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(_3DMapleSystemData data, IListPopulizer populizer)
            : base(data,populizer)
        {
        }

        public ActionResult Index()
        {
            var homePageModel = new HomePageViewModel();

            homePageModel.Categories = this.Populizer.GetCategories()
                .AsQueryable()
                .Project()
                .To<CategoryViewModel>()
                .ToList();

            homePageModel.PolyModels = this.Populizer.GetPolyModels()
                .AsQueryable()
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