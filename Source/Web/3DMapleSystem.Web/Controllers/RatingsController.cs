using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _3DMapleSystem.Data;
using _3DMapleSystem.Data.Models;

namespace _3DMapleSystem.Web.Controllers
{
    public class RatingsController : BaseController
    {
        public RatingsController(_3DMapleSystemData data)
            : base(data)
        {
        }

        // GET: Ratings
        public void Vote(Guid modelId)
        {

            if (!this.Request.IsAjaxRequest())
            {
                throw new HttpException();
                //return PartialView("_PartyGameSingleView"); //TODO Return appropriate message
            }

            var existingPolyModel = this.Data.PolyModels
                .All()
                .FirstOrDefault(pm => pm.Id == modelId);

            string rating = this.Request["rating"];

            if (rating != null)
            {
                //return PartialView("_PartyGameSingleView", votedGame);

                int ratingValue = int.Parse(rating);

                var existingRating = this.Data.Ratings.All().FirstOrDefault(r => r.PolyModelId == modelId && r.UserId == this.UserProfile.Id);

                if (existingRating != null)
                {
                    this.ModifyRating(ratingValue, existingRating);
                }

                else
                {
                    this.AddNewRating(ratingValue, existingPolyModel);
                }
            }
            //return View();
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