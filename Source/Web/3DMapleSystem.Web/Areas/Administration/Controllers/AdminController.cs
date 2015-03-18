using _3DMapleSystem.Common;
using _3DMapleSystem.Data;
using _3DMapleSystem.Web.Controllers;
using System;
using System.Linq;
using System.Web.Mvc;

namespace _3DMapleSystem.Web.Areas.Administration.Controllers
{
    [Authorize(Roles = GlobalConstants.AdminRole)]
    public class AdminController : BaseController
    {
        public AdminController(_3DMapleSystemData data)
            : base(data)
        {
        }
    }
}