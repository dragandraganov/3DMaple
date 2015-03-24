using System;
using System.Linq;
using System.Web.Mvc;
using _3DMapleSystem.Data;
using _3DMapleSystem.Web.ViewModels;
using _3DMapleSystem.Web.ViewModels.ComplexModels;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace _3DMapleSystem.Web.Controllers
{
    public class PolyModelsController : BaseController
    {
        public PolyModelsController(_3DMapleSystemData data)
            : base(data)
        {
        }

        // GET: PolyModels
        public ActionResult Index()
        {
            return View();
        }


        //GET: Create PolyModel
        public ActionResult Create()
        {
            var newModel = new CreateModelComplexViewModel();

            newModel.SubCategories = this.Data.SubCategories
                .All()
                .Project()
                .To<SubCategoryViewModel>()
                .Select(c => new GroupedSelectListItem
                {
                    GroupKey = c.CategoryId.ToString(),
                    GroupName = c.CategoryName,
                    Value = c.Id.ToString(),
                    Text = c.Name
                })
                .ToList();

            newModel.SubPlatforms = this.Data.SubPlatforms
                .All()
                .Project()
                .To<SubPlatformViewModel>()
                .Select(c => new GroupedSelectListItem
                {
                    GroupKey = c.PlatformId.ToString(),
                    GroupName = c.PlatformName,
                    Value = c.Id.ToString(),
                    Text = c.Name
                })
                .ToList();

            newModel.Styles = this.Data.Styles
                .All()
                .Project()
                .To<StyleViewModel>()
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                })
                .ToList();

            return View(newModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateModelComplexViewModel complexModel)
        {


            return View();
        }
    }
}