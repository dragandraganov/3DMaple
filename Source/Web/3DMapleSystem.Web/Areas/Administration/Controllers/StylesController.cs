using _3DMapleSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using _3DMapleSystem.Web.Areas.Administration.ViewModels;
using _3DMapleSystem.Data.Models;
using _3DMapleSystem.Common;

namespace _3DMapleSystem.Web.Areas.Administration.Controllers
{
    [Authorize(Roles = GlobalConstants.AdminRole)]
    public class StylesController : AdminController
    {
        public StylesController(_3DMapleSystemData data)
            : base(data)
        {
        }

        // GET: Administration/Styles
        public ActionResult Index()
        {
            var allStyles = this.Data
                .Styles
                .AllWithDeleted()
                .Project()
                .To<StyleViewModel>();

            return View(allStyles);
        }

        //GET: Add new style
        public ActionResult Add()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(StyleViewModel style)
        {
            if (style != null && ModelState.IsValid)
            {
                var styleWithSameName = this.Data.Styles
                    .All()
                    .Where(c => c.Name == style.Name)
                    .FirstOrDefault();

                if (styleWithSameName != null)
                {
                    ModelState.AddModelError(string.Empty, "This style already exists !");
                }

                else
                {
                    var newStyle = Mapper.Map<Style>(style);
                    this.Data.Styles.Add(newStyle);
                    this.Data.SaveChanges();
                    TempData["Success"] = "A new style '" + style.Name + "' was created";
                    return RedirectToAction("Index", "Styles");
                }
            }

            return View();
        }

        //GET: Edit style
        public ActionResult Edit(int id)
        {
            var existingStyle = this.Data
                .Styles
                .AllWithDeleted()
                .Where(pg => pg.Id == id)
                .Project()
                .To<StyleViewModel>()
                .FirstOrDefault();
            if (existingStyle == null)
            {
                throw new HttpException(404, "Style not found");
            }

            return View(existingStyle);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StyleViewModel style)
        {
            if (style != null && ModelState.IsValid)
            {
                var otherStyleWithSameName = this.Data.Styles
                    .All()
                    .Where(c => c.Name == style.Name&&c.Id!=style.Id)
                    .FirstOrDefault();

                if (otherStyleWithSameName != null)
                {
                    ModelState.AddModelError(string.Empty, "This style already exists !");
                }

                else
                {
                    var existingStyle = this.Data
                        .Styles
                        .GetById(style.Id);
                    Mapper.Map(style, existingStyle);

                    this.Data.Styles.Update(existingStyle);
                    this.Data.SaveChanges();

                    TempData["Success"] = "The style '" + style.Name + "' was edited";
                    return RedirectToAction("Index", "Styles");
                }
            }

            return View(style);
        }

        //GET: Delete style

        public ActionResult Delete(int id)
        {
            var existingStyle = this.Data
                 .Styles
                 .AllWithDeleted()
                 .Where(pg => pg.Id == id)
                 .Project()
                 .To<StyleViewModel>()
                 .FirstOrDefault();
            if (existingStyle == null)
            {
                throw new HttpException(404, "Style not found");
            }

            return View(existingStyle);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(StyleViewModel style)
        {
            if (style != null && ModelState.IsValid)
            {
                var existingStyle = this.Data
                    .Styles
                    .GetById(style.Id);

                this.Data.Styles.Delete(existingStyle);
                this.Data.SaveChanges();

                TempData["Success"] = "The style '" + style.Name + "' was deleted";
                return RedirectToAction("Index", "Styles");
            }

            return View(style);
        }

        public ActionResult HardDelete(int id)
        {
            return this.Delete(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HardDelete(StyleViewModel style)
        {
            if (style != null && ModelState.IsValid)
            {
                var existingStyle = this.Data
                    .Styles
                    .GetById(style.Id);

                this.Data.Styles.ActualDelete(existingStyle);
                this.Data.SaveChanges();

                TempData["Success"] = "The style '" + style.Name + "' was hard deleted";
                return RedirectToAction("Index", "Styles");
            }

            return View(style);
        }
    }
}