using _3DMapleSystem.Data;
using _3DMapleSystem.Data.Models;
using _3DMapleSystem.Web.Infrastructure.ActionResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using _3DMapleSystem.Web.Infrastructure.Popularizers;
using _3DMapleSystem.Web.ViewModels.PolyModels;

namespace _3DMapleSystem.Web.Controllers
{
    public class BaseController : Controller
    {
        protected _3DMapleSystemData Data { get; private set; }

        protected IListPopulizer Populizer { get; set; }

        protected User UserProfile { get; private set; }

        public BaseController(_3DMapleSystemData data, IListPopulizer populizer)
        {
            this.Data = data;
            this.Populizer = populizer;
        }

        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            this.UserProfile = this.Data.Users.All().Where(u => u.UserName == requestContext.HttpContext.User.Identity.Name).FirstOrDefault();

            return base.BeginExecute(requestContext, callback, state);
        }

        //protected override void OnActionExecuted(ActionExecutedContext filterContext)
        //{
        //    base.OnActionExecuted(filterContext);
        //}

        public IEnumerable<SelectListItem> GetCategories()
        {
            var categories = this.Data.Categories
                       .All()
                       .Select(c => new SelectListItem
                       {
                           Value = c.Id.ToString(),
                           Text = c.Name
                       })
                       .ToList();

            return categories;
        }

        public IEnumerable<SelectListItem> GetPlatforms()
        {
            var platforms = this.Data.Platforms
                       .All()
                       .Select(c => new SelectListItem
                       {
                           Value = c.Id.ToString(),
                           Text = c.Name
                       })
                       .ToList();

            return platforms;
        }

        [Obsolete("Do not use the standard Json helpers to return JSON data to the client.  Use either JsonSuccess or JsonError instead.")]
        protected JsonResult Json<T>(T data)
        {
            throw new InvalidOperationException("Do not use the standard Json helpers to return JSON data to the client.  Use either JsonSuccess or JsonError instead.");
        }

        protected StandardJsonResult JsonValidationError()
        {
            var result = new StandardJsonResult();

            foreach (var validationError in ModelState.Values.SelectMany(v => v.Errors))
            {
                result.AddError(validationError.ErrorMessage);
            }

            return result;
        }

        protected StandardJsonResult JsonError(string errorMessage)
        {
            var result = new StandardJsonResult();

            result.AddError(errorMessage);

            return result;
        }

        protected StandardJsonResult<T> JsonSuccess<T>(T data)
        {
            return new StandardJsonResult<T> { Data = data };
        }

        protected void AttachRatingProperties(PolyModel polyModel, PolyModelDetailsViewModel polyModelView)
        {
            var sumOfAllRatings = polyModel.Ratings
                .Where(r => !r.IsDeleted)
                .Sum(r => r.Value);

            var countRatings = polyModel.Ratings
                .Where(r => !r.IsDeleted)
                .Count();

            if (countRatings > 0)
            {
                polyModelView.AverageRating = Math.Round((double)sumOfAllRatings / countRatings, 1);
            }

            polyModelView.CountRatings = countRatings;

            if (this.UserProfile != null)
            {
                var currentUserRating = this.Data.Ratings
                    .All()
                    .FirstOrDefault(r => r.PolyModel.Id == polyModel.Id && r.User.Id == this.UserProfile.Id);

                if (currentUserRating != null)
                {
                    polyModelView.CurrentUserRating = currentUserRating.Value;
                }
            }
        }
    }
}