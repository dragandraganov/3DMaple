using System;
using System.Linq;
using System.Web.Mvc;
using _3DMapleSystem.Data;
using AutoMapper.QueryableExtensions;
using _3DMapleSystem.Web.Areas.Administration.ViewModels;
using System.Web;
using AutoMapper;

namespace _3DMapleSystem.Web.Areas.Administration.Controllers
{
    public class UsersController : AdminController
    {
        public UsersController(_3DMapleSystemData data)
            : base(data)
        {
        }

        // GET: Administration/Users
        public ActionResult Index()
        {
            var allUsers = this.Data
               .Users
               .AllWithDeleted()
               .Project()
               .To<UserViewModel>();

            return View(allUsers);
        }

        public ActionResult Edit(string id)
        {
            var existingUser = this.Data
                .Users
                .AllWithDeleted()
                .Where(u => u.Id == id)
                .Project()
                .To<UserViewModel>()
                .FirstOrDefault();

            if (existingUser == null)
            {
                throw new HttpException(404, "User not found");
            }

            return View(existingUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserViewModel user)
        {
            if (user != null && ModelState.IsValid)
            {
                var existingUser = this.Data
                    .Users
                    .AllWithDeleted()
                    .FirstOrDefault(u => u.Id == user.Id);

                existingUser.AvailableFreeModels = user.AvailableFreeModels;
                existingUser.AvailableProModels = user.AvailableProModels;

                this.Data.Users.Update(existingUser);
                this.Data.SaveChanges();

                TempData["Success"] = "The info for user '" + existingUser.UserName + "' was updated";

                return RedirectToAction("Index", "Users");

            }

            return View(user);
        }
    }
}