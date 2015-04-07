using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _3DMapleSystem.Data;
using _3DMapleSystem.Web.Infrastructure.Popularizers;
using _3DMapleSystem.Common;

namespace _3DMapleSystem.Web.Controllers
{
    public class AppFilesController : BaseController
    {
        private IListPopulizer populator;

        public AppFilesController(_3DMapleSystemData data, IListPopulizer populator)
            : base(data)
        {
            this.populator = populator;
        }

        // GET: Images
        public ActionResult Index()
        {
            return View();
        }

        public virtual ActionResult GetPreview(int id)
        {
            var file = this.Data.AppFiles.GetById(id);

            if (file.FileExtension=="rar" || file.FileExtension=="zip")
            {
                throw new HttpException(404, "You have no permission");
            }

            if (file == null)
            {
                throw new HttpException(404, "File not found");
            }

            return File(file.Content, "file/" + file.FileExtension);
        }

        [Authorize(Roles = GlobalConstants.AdminRole)]
        public ActionResult Download(int id)
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