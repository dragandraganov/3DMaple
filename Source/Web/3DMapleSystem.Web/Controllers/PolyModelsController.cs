using System;
using System.Linq;
using System.Web.Mvc;
using _3DMapleSystem.Data;
using _3DMapleSystem.Web.ViewModels;
using _3DMapleSystem.Web.ViewModels.ComplexModels;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using _3DMapleSystem.Data.Models;
using System.IO;

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
            var model = new CreateModelComplexViewModel();

            AttachPropertiesToComplexModel(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateModelComplexViewModel complexModel)
        {
            if (complexModel != null && ModelState.IsValid)
            {
                var newPolyModel = new PolyModel();
                newPolyModel.Title = complexModel.PolyModel.Title;
                newPolyModel.Description = complexModel.PolyModel.Description;
                newPolyModel.StyleId = complexModel.PolyModel.StyleId;
                newPolyModel.SubCategoryId = complexModel.PolyModel.SubCategoryId;
                newPolyModel.SubPlatformId = complexModel.PolyModel.SubPlatformId;
                using (var memory = new MemoryStream())
                {
                    complexModel.PolyModel.Uploaded3DModel.InputStream.CopyTo(memory);
                    var content = memory.GetBuffer();

                    newPolyModel.File3DModel = new AppFile
                    {
                        Content = content,
                        FileExtension = complexModel.PolyModel.Uploaded3DModel.FileName.Split(new[] { '.' }).Last()
                    };
                }
                using (var memory = new MemoryStream())
                {
                    complexModel.PolyModel.UploadedPreview.InputStream.CopyTo(memory);
                    var content = memory.GetBuffer();

                    newPolyModel.Preview = new AppFile
                    {
                        Content = content,
                        FileExtension = complexModel.PolyModel.UploadedPreview.FileName.Split(new[] { '.' }).Last()
                    };
                }
                var tags = complexModel.PolyModel.Tags.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var tag in tags)
                {
                    var newTag=new Tag();
                    newTag.Name=tag;
                    this.Data.Tags.Add(newTag);
                    this.Data.SaveChanges();
                    newPolyModel.Tags.Add(newTag);
                }

                this.Data.PolyModels.Add(newPolyModel);
                this.Data.SaveChanges();
            }

            AttachPropertiesToComplexModel(complexModel);
            return View(complexModel);
        }

        private void AttachPropertiesToComplexModel(CreateModelComplexViewModel model)
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
        }
    }
}