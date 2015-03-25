using System;
using System.Linq;
using System.Web;
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

        //GET: Admin edit model
        public ActionResult Edit(Guid id)
        {
            var newComplexModel = new PolyModelComplexViewModel();

            var existingModel = this.Data.PolyModels
                .All()
                .Where(m => m.Id == id)
                .Project()
                .To<PolyModelViewModel>()
                .FirstOrDefault();

            if (existingModel == null)
            {
                throw new HttpException(404, "Model not found");
            }

            newComplexModel.PolyModel = existingModel;

            AttachPropertiesToComplexModel(newComplexModel);

            return View(newComplexModel);
        }

        private void AttachPropertiesToComplexModel(PolyModelComplexViewModel model)
        {
            model.SubCategories = this.Data.SubCategories
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

            model.SubPlatforms = this.Data.SubPlatforms
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

            model.Styles = this.Data.Styles
                .All()
                .Project()
                .To<StyleViewModel>()
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                })
                .ToList();

            model.Ranks = this.Data.ModelRanks
               .All()
               .Project()
               .To<ModelRankViewModel>()
               .Select(c => new SelectListItem
               {
                   Value = c.Id.ToString(),
                   Text = c.Name
               })
               .ToList();
        }
    }
}