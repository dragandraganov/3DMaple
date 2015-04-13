using _3DMapleSystem.Common;
using _3DMapleSystem.Data;
using _3DMapleSystem.Web.Controllers;
using System;
using System.Linq;
using System.Web.Mvc;
using _3DMapleSystem.Web.Infrastructure.Popularizers;

namespace _3DMapleSystem.Web.Areas.Administration.Controllers
{
    [Authorize(Roles = GlobalConstants.AdminRole)]
    public class AdminController : BaseController
    {
        public AdminController(_3DMapleSystemData data, IListPopulizer populizer = null)
            : base(data, populizer)
        {
        }
    }
}