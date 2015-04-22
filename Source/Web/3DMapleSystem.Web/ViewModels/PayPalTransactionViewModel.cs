using AutoMapper;
using _3DMapleSystem.Data.Models;
using _3DMapleSystem.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _3DMapleSystem.Web.ViewModels
{
    public class PayPalTransactionViewModel : IMapFrom<Order>, IHaveCustomMappings
    {


        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Order, PayPalTransactionViewModel>()
                .ReverseMap();
        }
    }
}