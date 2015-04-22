﻿using AutoMapper;
using _3DMapleSystem.Data.Models;
using _3DMapleSystem.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _3DMapleSystem.Web.ViewModels
{
    public class OrderViewModel : IMapFrom<Order>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public int ProModelsOrderedNumber { get; set; }

        public int FreeModelsMonthsSubscription { get; set; }

        public string UserName { get; set; }

        public int UserPhotoId { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool PreserveCreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Order, OrderViewModel>()
                .ForMember(m => m.UserName, opt => opt.MapFrom(t => t.User.UserName))
                .ForMember(m => m.UserPhotoId, opt => opt.MapFrom(t => t.User.PhotoId))
                .ReverseMap();
        }
    }
}