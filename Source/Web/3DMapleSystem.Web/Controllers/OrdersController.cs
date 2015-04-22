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
    public class OrdersController : BaseController
    {
        public OrdersController(_3DMapleSystemData data, IListPopulizer populizer)
            : base(data, populizer)
        {
        }

        // GET: Create Order
        public ActionResult Create()
        {
            var orderModel = new OrderViewModel();

            orderModel.UserPhotoId = this.UserProfile.PhotoId;

            orderModel.UserName = this.UserProfile.UserName;

            return View(orderModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderViewModel order)
        {
            order.TotalSum = order.ProModelsOrderedNumber * order.ProModelPrice + order.FreeModelsMonthsSubscription * order.FreeModelsSubscritpionPrice;


            var newOrder = Mapper.Map<OrderViewModel, Order>(order);

            return View();
        }
    }
}