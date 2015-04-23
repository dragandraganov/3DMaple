using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _3DMapleSystem.Data;
using _3DMapleSystem.Web.Infrastructure.Popularizers;
using _3DMapleSystem.Common;
using _3DMapleSystem.Web.Infrastructure.Helpers;

namespace _3DMapleSystem.Web.Controllers
{
    public class AppFilesController : BaseController
    {
        public AppFilesController(_3DMapleSystemData data, IListPopulizer populizer)
            : base(data, populizer)
        {
        }

        // GET: Images
        public ActionResult Index()
        {
            return View();
        }

        public virtual ActionResult GetPreview(int id)
        {
            var file = this.Data.AppFiles.GetById(id);

            if (file.FileExtension == "rar" || file.FileExtension == "zip")
            {
                throw new HttpException(403, "You have no permission");
            }

            if (file == null)
            {
                throw new HttpException(404, "File not found");
            }

            return File(file.Content, "file/" + file.FileExtension);
        }

        /// <summary>
        /// Downloads the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="modelId">The model id.</param>
        /// <returns></returns>
        //[AdminOnly(Roles = GlobalConstants.AdminRole, Message="The application is in development. Only admins can download models!")]
        [Authorize]
        public ActionResult Download(int id, Guid modelId)
        {

            //if (this.UserProfile == null || !this.HttpContext.User.IsInRole(GlobalConstants.AdminRole))
            //{
            //    return RedirectToAction("Login", "Account");
            //}

            var polyModel = this.Data.PolyModels
            .All()
            .FirstOrDefault(pm => pm.Id == modelId);

            var rankName = polyModel.Rank.Name;

            if ((rankName == "free" && this.UserProfile.AvailableFreeModels <= 0) || (rankName == "pro" && this.UserProfile.AvailableProModels <= 0))
            {
                TempData["Rank"] = rankName;
                return new HttpStatusCodeResult(400);
            }

            else
            {
                var file = this.Data.AppFiles.GetById(id);

                if (file == null)
                {
                    throw new HttpException(404, "File not found");
                }

                return File(file.Content, "application/zip", file.OriginalName + "." + file.FileExtension);
            }
        }
    }
}