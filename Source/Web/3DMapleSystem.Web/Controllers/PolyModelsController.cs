using System;
using System.Linq;
using System.Web.Mvc;
using _3DMapleSystem.Data;

namespace _3DMapleSystem.Web.Controllers
{
    public class PolyModelsController : BaseController
    {
        public PolyModelsController(_3DMapleSystemData data)
            : base(data)
        {
        }

        // GET: PolyModels
        public ActionResult Index()
        {
            return View();
        }
    }
}