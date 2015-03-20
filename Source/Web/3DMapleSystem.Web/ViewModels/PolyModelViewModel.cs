﻿using AutoMapper;
using _3DMapleSystem.Data.Models;
using _3DMapleSystem.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _3DMapleSystem.Web.ViewModels
{
    public class PolyModelViewModel : IMapFrom<PolyModel>, IHaveCustomMappings
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        //public string ModelUrl { get; set; }

        //public virtual AppFile Preview { get; set; }

        public string CategoryName { get; set; }

        public string SubCategoryName { get; set; }

        public string StyleName { get; set; }

        public string PlatformName { get; set; }

        public string SubPlatformName { get; set; }

        public ICollection<Tag> Tags { get; set; }

        public string AuthorName { get; set; }

        public int DownloadedByUsersCount { get; set; }

        //public string AuthorId { get; set; }

        //public virtual User Author { get; set; }

        //public virtual ICollection<User> DownloadedByUsers { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool PreserveCreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<PolyModel, PolyModelViewModel>()
                .ForMember(m => m.SubCategoryName, opt => opt.MapFrom(t => t.SubCategory.Name))
                .ForMember(m => m.CategoryName, opt => opt.MapFrom(t => t.SubCategory.Category.Name))
                .ForMember(m => m.SubPlatformName, opt => opt.MapFrom(t => t.SubPlatform.Name))
                .ForMember(m => m.PlatformName, opt => opt.MapFrom(t => t.SubPlatform.Platform.Name))
                .ForMember(m => m.AuthorName, opt => opt.MapFrom(t => t.Author.UserName))
                .ForMember(m => m.DownloadedByUsersCount, opt => opt.MapFrom(t => t.DownloadedByUsers.Count))
                .ReverseMap();
        }
    }
}