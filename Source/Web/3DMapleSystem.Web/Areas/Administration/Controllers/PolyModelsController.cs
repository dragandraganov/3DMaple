using System;
using System.Linq;
using System.Web.Mvc;
using _3DMapleSystem.Data;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using _3DMapleSystem.Web.Areas.Administration.ViewModels;

namespace _3DMapleSystem.Web.Areas.Administration.Controllers
{
    public class PolyModelsController : AdminController
    {
        public PolyModelsController(_3DMapleSystemData data)
            : base(data)
        {
        }

        // GET: Administration/PolyModels
        public ActionResult Index()
        {
            var allModels = this.Data.PolyModels
                .AllWithDeleted()
                .Project()
                .To<PolyModelViewModel>()
                .ToList();

            return View(allModels);
        }
    }
}