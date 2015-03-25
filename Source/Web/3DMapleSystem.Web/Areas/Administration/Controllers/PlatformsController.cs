using _3DMapleSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using _3DMapleSystem.Web.Areas.Administration.ViewModels;
using AutoMapper;
using _3DMapleSystem.Data.Models;
using _3DMapleSystem.Common;

namespace _3DMapleSystem.Web.Areas.Administration.Controllers
{
    [Authorize(Roles = GlobalConstants.AdminRole)]
    public class PlatformsController : AdminController
    {
        public PlatformsController(_3DMapleSystemData data)
            : base(data)
        {
        }

        // GET: Administration/Platforms
        public ActionResult Index()
        {
            var allPlatforms = this.Data
                .Platforms
                .AllWithDeleted()
                .Project()
                .To<PlatformViewModel>();

            return View(allPlatforms);
        }

        //GET: Add new category
        public ActionResult Add()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(PlatformViewModel platform)
        {
            if (platform != null && ModelState.IsValid)
            {
                var paltformWithSameName = this.Data.Platforms
                    .All()
                    .Where(c => c.Name == platform.Name)
                    .FirstOrDefault();

                if (paltformWithSameName != null)
                {
                    ModelState.AddModelError(string.Empty, "This platform already exists !");
                }

                else
                {
                    var newPlatform = Mapper.Map<Platform>(platform);
                    this.Data.Platforms.Add(newPlatform);
                    this.Data.SaveChanges();
                    TempData["Success"] = "A new platform '" + platform.Name + "' was created";
                    return RedirectToAction("Index", "Platforms");
                }
            }

            return View();
        }

        //GET: Edit platform
        public ActionResult Edit(int id)
        {
            var existingPlatform = this.Data
                .Platforms
                .AllWithDeleted()
                .Where(pg => pg.Id == id)
                .Project()
                .To<PlatformViewModel>()
                .FirstOrDefault();

            if (existingPlatform == null)
            {
                throw new HttpException(404, "Platform not found");
            }

            return View(existingPlatform);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PlatformViewModel platform)
        {
            if (platform != null && ModelState.IsValid)
            {
                var otherPlatformWithSameName = this.Data.Categories
                    .All()
                    .Where(c => c.Name == platform.Name&&c.Id!=platform.Id)
                    .FirstOrDefault();

                if (otherPlatformWithSameName != null)
                {
                    ModelState.AddModelError(string.Empty, "This platform already exists !");
                }

                else
                {
                    var existingPlatform = this.Data
                        .Platforms
                        .GetById(platform.Id);

                    Mapper.Map(platform, existingPlatform);

                    this.Data.Platforms.Update(existingPlatform);
                    this.Data.SaveChanges();
                    TempData["Success"] = "The paltform '" + platform.Name + "' was edited";
                    return RedirectToAction("Index", "Platforms");
                }
            }

            return View(platform);
        }

        //GET: Delete platform

        public ActionResult Delete(int id)
        {
            var existingPlatform = this.Data
                .Platforms
                .AllWithDeleted()
                .Where(pg => pg.Id == id)
                .Project()
                .To<PlatformViewModel>()
                .FirstOrDefault();

            if (existingPlatform == null)
            {
                throw new HttpException(404, "Platform not found");
            }

            return View(existingPlatform);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(PlatformViewModel paltform)
        {
            if (paltform != null && ModelState.IsValid)
            {
                var existingPlatform = this.Data
                    .Platforms
                    .GetById(paltform.Id);
                this.Data.Platforms.Delete(existingPlatform);
                this.Data.SaveChanges();
                TempData["Success"] = "The platform '" + paltform.Name + "' was deleted";
                return RedirectToAction("Index", "Platforms");
            }

            return View(paltform);
        }

        public ActionResult HardDelete(int id)
        {
            return this.Delete(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HardDelete(PlatformViewModel paltform)
        {
            if (paltform != null && ModelState.IsValid)
            {
                var existingPlatform = this.Data
                    .Platforms
                    .GetById(paltform.Id);

                Mapper.Map(paltform, existingPlatform);

                this.Data.Platforms.ActualDelete(existingPlatform);
                this.Data.SaveChanges();

                TempData["Success"] = "The platform '" + paltform.Name + "' was hard deleted";
                return RedirectToAction("Index", "Platforms");
            }

            return View(paltform);
        }
    }
}