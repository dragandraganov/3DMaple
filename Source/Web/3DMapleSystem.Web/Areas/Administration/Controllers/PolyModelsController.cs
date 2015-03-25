using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _3DMapleSystem.Data;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using _3DMapleSystem.Web.Areas.Administration.ViewModels;
using _3DMapleSystem.Data.Models;
using _3DMapleSystem.Common;

namespace _3DMapleSystem.Web.Areas.Administration.Controllers
{
    [Authorize(Roles = GlobalConstants.AdminRole)]
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

        //POST: Admin edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PolyModelComplexViewModel complexModel)
        {

            if (complexModel != null && ModelState.IsValid)
            {
                var modelView = complexModel.PolyModel;

                var existingPolyModel = this.Data.PolyModels
                    .All()
                    .Where(m => m.Id == complexModel.PolyModel.Id)
                    .FirstOrDefault();

                existingPolyModel.Title = modelView.Title;
                existingPolyModel.SubCategoryId = modelView.SubCategoryId;
                existingPolyModel.SubPlatformId = modelView.SubPlatformId;
                existingPolyModel.StyleId = modelView.StyleId;
                existingPolyModel.RankId = modelView.RankId;
                existingPolyModel.Description = modelView.Description;
                existingPolyModel.IsApproved = modelView.IsApproved;

                if (modelView.UploadedPreview != null)
                {
                    using (var memory = new MemoryStream())
                    {
                        complexModel.PolyModel.UploadedPreview.InputStream.CopyTo(memory);
                        var content = memory.GetBuffer();

                        existingPolyModel.Preview = new AppFile
                        {
                            Content = content,
                            FileExtension = complexModel.PolyModel.UploadedPreview.FileName.Split(new[] { '.'}).Last()
                        };
                    }
                }

                var tags = complexModel.PolyModel.Tags
                    .FirstOrDefault()
                    .ToString();

                var tagsArray=tags.Split(new char[] { ',',' ' }, StringSplitOptions.RemoveEmptyEntries);
                
                existingPolyModel.Tags.Clear();

                foreach (var tag in tagsArray)
                {
                    var existingTag = this.Data.Tags
                        .All()
                        .FirstOrDefault(t => t.Name == tag);

                    if (existingTag == null)
                    {
                        var newTag = new Tag();
                        newTag.Name = tag;
                        this.Data.Tags.Add(newTag);
                        this.Data.SaveChanges();
                        existingPolyModel.Tags.Add(newTag);
                    }

                    else
                    {
                        existingPolyModel.Tags.Add(existingTag);
                    }
                }

                this.Data.PolyModels.Update(existingPolyModel);
                this.Data.SaveChanges();
            }

            AttachPropertiesToComplexModel(complexModel);
            return View(complexModel);
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