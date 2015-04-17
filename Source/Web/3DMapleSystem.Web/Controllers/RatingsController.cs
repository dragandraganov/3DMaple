using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _3DMapleSystem.Data;
using _3DMapleSystem.Data.Models;
using _3DMapleSystem.Web.Infrastructure.Popularizers;
using AutoMapper;
using _3DMapleSystem.Web.ViewModels.PolyModels;

namespace _3DMapleSystem.Web.Controllers
{
    [Authorize]
    public class RatingsController : BaseController
    {

        public RatingsController(_3DMapleSystemData data, IListPopulizer populizer)
            : base(data, populizer)
        {
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Vote(string modelId, string rating)
        {
            if (!this.Request.IsAjaxRequest())
            {
                throw new HttpException();//TODO Return appropriate message
            }

            var existingRating = this.Data.Ratings
                    .All()
                    .FirstOrDefault(r => r.PolyModelId == new Guid(modelId) && r.UserId == this.UserProfile.Id);

            var existingPolyModel = this.Data.PolyModels
                   .All()
                   .FirstOrDefault(pm => pm.Id == new Guid(modelId));

            if (rating == null)
            {
                if (existingRating != null)
                {
                    this.Data.Ratings.Delete(existingRating);
                    this.Data.SaveChanges();
                }

                var polyModelViewModel = Mapper.Map<PolyModelDetailsViewModel>(existingPolyModel);

                this.AttachRatingProperties(existingPolyModel, polyModelViewModel);

                return PartialView("_RatingResult", polyModelViewModel);
            }

            else
            {
                if (rating != null)
                {
                    //return PartialView("_PartyGameSingleView", votedGame);

                    int ratingValue = int.Parse(rating);

                    if (existingRating != null)
                    {
                        this.ModifyRating(ratingValue, existingRating);
                    }

                    else
                    {
                        this.AddNewRating(ratingValue, existingPolyModel);
                    }

                    var polyModelViewModel = Mapper.Map<PolyModelDetailsViewModel>(existingPolyModel);

                    this.AttachRatingProperties(existingPolyModel, polyModelViewModel);

                    return PartialView("_RatingResult", polyModelViewModel);
                }
            }

            throw new HttpException();
        }

        private void ModifyRating(int ratingValue, Rating ratingEntity)
        {
            ratingEntity.Value = ratingValue;
            this.Data.Ratings.Update(ratingEntity);
            this.Data.SaveChanges();
        }

        private void AddNewRating(int ratingValue, PolyModel votedModel)
        {
            var newRating = new Rating();
            newRating.Value = ratingValue;
            newRating.PolyModelId = votedModel.Id;
            string currentUserId = this.UserProfile.Id;
            newRating.UserId = currentUserId;
            this.Data.Ratings.Add(newRating);
            this.Data.SaveChanges();
        }
    }
}