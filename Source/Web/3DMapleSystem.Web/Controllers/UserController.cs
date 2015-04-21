using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _3DMapleSystem.Web.Infrastructure.Popularizers;
using _3DMapleSystem.Data;
using _3DMapleSystem.Data.Models;
using _3DMapleSystem.Web.ViewModels;
using AutoMapper;

namespace _3DMapleSystem.Web.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {
        public UserController(_3DMapleSystemData data, IListPopulizer populizer)
            : base(data, populizer)
        {
        }

        // GET: User
        public ActionResult BuyModels()
        {
            var currentUserModel = Mapper.Map<User, UserViewModel>(this.UserProfile);

            return View(currentUserModel);
        }
    }
}