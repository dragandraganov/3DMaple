using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _3DMapleSystem.Data;

namespace _3DMapleSystem.Web.Controllers
{
    public class AppFilesController : BaseController
    {
        public AppFilesController(_3DMapleSystemData data)
            : base(data)
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

            if (file == null)
            {
                throw new HttpException(404, "File not found");
            }

            return File(file.Content, "file/" + file.FileExtension);
        }
    }
}