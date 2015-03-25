using _3DMapleSystem.Data;
using AutoMapper;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using _3DMapleSystem.Web.Areas.Administration.ViewModels;
using _3DMapleSystem.Data.Models;
using _3DMapleSystem.Common;

namespace _3DMapleSystem.Web.Areas.Administration.Controllers
{
    [Authorize(Roles = GlobalConstants.AdminRole)]
    public class SubPlatformsController : AdminController
    {
        public SubPlatformsController(_3DMapleSystemData data)
            : base(data)
        {
        }

        // GET: Administration/SubPlatforms
        public ActionResult Index()
        {
            var allSubPlatforms = this.Data
               .SubPlatforms
               .AllWithDeleted()
               .Project()
               .To<SubPlatformViewModel>();

            return View(allSubPlatforms);
        }

        //GET: Add new sub-platform
        public ActionResult Add()
        {
            var newSubPlatformViewModel = new SubPlatformViewModel
            {
                Platforms = this.GetPlatforms()
            };

            return View(newSubPlatformViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(SubPlatformViewModel subPlatform)
        {
            if (subPlatform != null && ModelState.IsValid)
            {
                var subPlatformWithSameName = this.Data.SubPlatforms
                    .All()
                    .Where(c => c.Name == subPlatform.Name)
                    .FirstOrDefault();

                if (subPlatformWithSameName != null)
                {
                    ModelState.AddModelError(string.Empty, "Sub-platform with same name already exists !");
                }

                else
                {
                    var newSubPlatform = Mapper.Map<SubPlatform>(subPlatform);
                    this.Data.SubPlatforms.Add(newSubPlatform);
                    this.Data.SaveChanges();
                    return RedirectToAction("Index", "SubPlatforms");
                }
            }

            subPlatform.Platforms = this.GetPlatforms();
            return View(subPlatform);
        }

        //GET Edit sub-paltform
        public ActionResult Edit(int id)
        {
            var existingSubPlatform = this.Data
                .SubPlatforms
                .AllWithDeleted()
                .Where(pg => pg.Id == id)
                .Project()
                .To<SubPlatformViewModel>()
                .FirstOrDefault();
            if (existingSubPlatform == null)
            {
                throw new HttpException(404, "Sub-platform not found");
            }
            var allPlatforms = this.GetPlatforms();
            existingSubPlatform.Platforms = new SelectList(allPlatforms, "Value", "Text");
            return View(existingSubPlatform);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SubPlatformViewModel subPlatform)
        {
            if (subPlatform != null && ModelState.IsValid)
            {
                var subPlatformWithSameName = this.Data.SubPlatforms
                    .All()
                    .Where(sc => sc.Name == subPlatform.Name && sc.Id != subPlatform.Id)
                    .FirstOrDefault();

                if (subPlatformWithSameName != null)
                {
                    ModelState.AddModelError(string.Empty, "Sub-platform with this name already exists !");
                }

                else
                {
                    var existingSubPlatform = this.Data
                        .SubPlatforms
                        .GetById(subPlatform.Id);
                    Mapper.Map(subPlatform, existingSubPlatform);

                    this.Data.SubPlatforms.Update(existingSubPlatform);
                    this.Data.SaveChanges();

                    return RedirectToAction("Index", "SubPlatforms");
                }
            }

            subPlatform.Platforms = this.GetPlatforms();
            return View(subPlatform);
        }

        //GET Delete sub-category
        public ActionResult Delete(int id)
        {
            var existingSubPlatform = this.Data
                .SubPlatforms
                .AllWithDeleted()
                .Where(sc => sc.Id == id)
                .Project()
                .To<SubPlatformViewModel>()
                .FirstOrDefault();

            if (existingSubPlatform == null)
            {
                throw new HttpException(404, "Sub-platform not found");
            }

            return View(existingSubPlatform);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(SubPlatformViewModel subPlatform)
        {
            if (subPlatform != null && ModelState.IsValid)
            {
                var existingSubPlatform = this.Data
                    .SubPlatforms
                    .GetById(subPlatform.Id);

                this.Data.SubPlatforms.Delete(existingSubPlatform);
                this.Data.SaveChanges();

                return RedirectToAction("Index", "SubPlatforms");
            }

            return View(subPlatform);
        }

        //GET Hard delete sub-category
        public ActionResult HardDelete(int id)
        {
            return this.Delete(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HardDelete(SubPlatformViewModel subPlatform)
        {
            if (subPlatform != null && ModelState.IsValid)
            {
                var existingsSubPlatform = this.Data
                    .SubPlatforms
                    .GetById(subPlatform.Id);

                this.Data.SubPlatforms.ActualDelete(existingsSubPlatform);
                this.Data.SaveChanges();

                return RedirectToAction("Index", "SubPlatforms");
            }

            return View(subPlatform);
        }
    }
}