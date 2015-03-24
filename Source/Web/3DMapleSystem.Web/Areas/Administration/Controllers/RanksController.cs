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

namespace _3DMapleSystem.Web.Areas.Administration.Controllers
{
    public class RanksController : AdminController
    {
        public RanksController(_3DMapleSystemData data)
            : base(data)
        {
        }

        // GET: Administration/Styles
        public ActionResult Index()
        {
            var allRanks = this.Data
                .ModelRanks
                .AllWithDeleted()
                .Project()
                .To<ModelRankViewModel>();

            return View(allRanks);
        }

        //GET: Add new style
        public ActionResult Add()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ModelRankViewModel rank)
        {
            if (rank != null && ModelState.IsValid)
            {
                var rankWithSameName = this.Data.Styles
                    .All()
                    .Where(c => c.Name == rank.Name)
                    .FirstOrDefault();

                if (rankWithSameName != null)
                {
                    ModelState.AddModelError(string.Empty, "This rank already exists !");
                }

                else
                {
                    var newRank = Mapper.Map<ModelRank>(rank);
                    this.Data.ModelRanks.Add(newRank);
                    this.Data.SaveChanges();
                    TempData["Success"] = "A new rank '" + rank.Name + "' was created";
                    return RedirectToAction("Index", "Ranks");
                }
            }

            return View();
        }

        //GET: Edit style
        public ActionResult Edit(int id)
        {
            var existingRank = this.Data
                .ModelRanks
                .AllWithDeleted()
                .Where(pg => pg.Id == id)
                .Project()
                .To<ModelRankViewModel>()
                .FirstOrDefault();

            if (existingRank == null)
            {
                throw new HttpException(404, "Rank not found");
            }

            return View(existingRank);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ModelRankViewModel rank)
        {
            if (rank != null && ModelState.IsValid)
            {
                var otherRankWithSameName = this.Data.ModelRanks
                    .All()
                    .Where(c => c.Name == rank.Name&&c.Id!=rank.Id)
                    .FirstOrDefault();

                if (otherRankWithSameName != null)
                {
                    ModelState.AddModelError(string.Empty, "This rank already exists !");
                }

                else
                {
                    var existingRank = this.Data
                        .ModelRanks
                        .GetById(rank.Id);

                    Mapper.Map(rank, existingRank);

                    this.Data.ModelRanks.Update(existingRank);
                    this.Data.SaveChanges();

                    TempData["Success"] = "The rank '" + rank.Name + "' was edited";
                    return RedirectToAction("Index", "Ranks");
                }
            }

            return View(rank);
        }

        //GET: Delete style

        public ActionResult Delete(int id)
        {
            var existingRank = this.Data
                 .Styles
                 .AllWithDeleted()
                 .Where(pg => pg.Id == id)
                 .Project()
                 .To<ModelRankViewModel>()
                 .FirstOrDefault();

            if (existingRank == null)
            {
                throw new HttpException(404, "Rank not found");
            }

            return View(existingRank);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ModelRankViewModel rank)
        {
            if (rank != null && ModelState.IsValid)
            {
                var existingRank = this.Data
                    .ModelRanks
                    .GetById(rank.Id);

                this.Data.ModelRanks.Delete(existingRank);
                this.Data.SaveChanges();

                TempData["Success"] = "The rank '" + rank.Name + "' was deleted";
                return RedirectToAction("Index", "Ranks");
            }

            return View(rank);
        }

        public ActionResult HardDelete(int id)
        {
            return this.Delete(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HardDelete(ModelRankViewModel rank)
        {
            if (rank != null && ModelState.IsValid)
            {
                var existingRank = this.Data
                    .ModelRanks
                    .GetById(rank.Id);

                this.Data.ModelRanks.ActualDelete(existingRank);
                this.Data.SaveChanges();

                TempData["Success"] = "The rank '" + rank.Name + "' was hard deleted";
                return RedirectToAction("Index", "Ranks");
            }

            return View(rank);
        }
    }
}