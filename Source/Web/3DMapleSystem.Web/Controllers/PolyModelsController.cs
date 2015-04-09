using System;
using System.Linq;
using System.Web.Mvc;
using _3DMapleSystem.Data;
using _3DMapleSystem.Web.ViewModels;
using _3DMapleSystem.Web.ViewModels.ComplexModels;
using AutoMapper.QueryableExtensions;
using _3DMapleSystem.Data.Models;
using System.IO;
using _3DMapleSystem.Common;
using _3DMapleSystem.Web.ViewModels.PolyModels;
using AutoMapper;

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

        [Authorize]
        //GET: Create PolyModel
        public ActionResult Create()
        {
            var model = new PolyModelComplexViewModel();

            AttachPropertiesToComplexModel(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(PolyModelComplexViewModel complexModel)
        {
            if (complexModel != null && ModelState.IsValid)
            {
                var newPolyModel = new PolyModel();

                newPolyModel.Title = complexModel.PolyModel.Title;
                newPolyModel.Description = complexModel.PolyModel.Description;
                newPolyModel.StyleId = complexModel.PolyModel.StyleId;
                newPolyModel.SubCategoryId = complexModel.PolyModel.SubCategoryId;
                newPolyModel.SubPlatformId = complexModel.PolyModel.SubPlatformId;
                newPolyModel.Author = this.UserProfile;

                //TODO remove in production mode - change the logic
                var rank = new ModelRank();

                if (this.Data.ModelRanks.All().Count() == 0 || this.Data.ModelRanks.All().FirstOrDefault(r => r.Name == GlobalConstants.DefaultRank) == null)
                {
                    rank.Name = GlobalConstants.DefaultRank;
                    this.Data.ModelRanks.Add(rank);
                    this.Data.SaveChanges();
                }

                else
                {
                    rank = this.Data.ModelRanks.All().FirstOrDefault(r => r.Name == GlobalConstants.DefaultRank);
                }

                newPolyModel.Rank = rank;

                using (var memory = new MemoryStream())
                {
                    complexModel.PolyModel.Uploaded3DModel.InputStream.CopyTo(memory);
                    var content = memory.GetBuffer();

                    newPolyModel.File3DModel = new AppFile
                    {
                        Content = content,
                        FileExtension = complexModel.PolyModel.Uploaded3DModel.FileName.Split(new[] { '.' }).Last(),
                        Size = complexModel.PolyModel.Uploaded3DModel.ContentLength,
                        OriginalName = complexModel.PolyModel.Uploaded3DModel.FileName.Split(new[] { '.' }).First()
                    };
                }

                using (var memory = new MemoryStream())
                {
                    complexModel.PolyModel.UploadedPreview.InputStream.CopyTo(memory);
                    var content = memory.GetBuffer();

                    newPolyModel.Preview = new AppFile
                    {
                        Content = content,
                        FileExtension = complexModel.PolyModel.UploadedPreview.FileName.Split(new[] { '.' }).Last(),
                    };
                }

                var tags = complexModel.PolyModel.Tags.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var tag in tags)
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
                        newPolyModel.Tags.Add(newTag);
                    }

                    else
                    {
                        newPolyModel.Tags.Add(existingTag);
                    }

                }

                this.Data.PolyModels.Add(newPolyModel);
                this.Data.SaveChanges();
                TempData["Success"] = "Successfully uploaded model with title '" + newPolyModel.Title + "'. Expect approval by administrator.";
                return RedirectToAction("Index", "Home");
            }

            AttachPropertiesToComplexModel(complexModel);

            return View(complexModel);
        }

        public ActionResult Details(Guid id)
        {
            var complexModel = new DetailsComplexModel();

            var polyModel = this.Data.PolyModels
                .All()
                .FirstOrDefault(pm => pm.Id == id);

            complexModel.PolyModel = Mapper.Map<PolyModelDetailsViewModel>(polyModel);

            if (polyModel.File3DModel.Size == null)
            {
                complexModel.SizeOfFileModel = String.Format("{0:0.00}", 0);
            }

            else
            {
                complexModel.SizeOfFileModel = String.Format("{0:0.00}", (double)polyModel.File3DModel.Size.Value / (1024 * 1024));
            }

            complexModel.CurrentUser = Mapper.Map<UserViewModel>(this.UserProfile);

            return View(complexModel);
        }

        public void DownloadSuccess(string modelId)
        {
            var polyModel = this.Data.PolyModels.All().FirstOrDefault(pm => pm.Id == new Guid(modelId));
            var downloadModelUser = new DownloadedPolyModelsUsers();
            downloadModelUser.PolyModel = polyModel;
            downloadModelUser.User = this.UserProfile;
            this.Data.ModelsUsers.Add(downloadModelUser);
            this.Data.SaveChanges();
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
        }
    }
}