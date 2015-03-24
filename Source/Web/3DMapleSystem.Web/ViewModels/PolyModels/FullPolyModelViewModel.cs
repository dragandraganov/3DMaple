﻿using AutoMapper;
using _3DMapleSystem.Data.Models;
using _3DMapleSystem.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace _3DMapleSystem.Web.ViewModels
{
    public class FullPolyModelViewModel : IMapFrom<PolyModel>, IHaveCustomMappings
    {
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        //public string ModelUrl { get; set; }

        //public virtual AppFile Preview { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public int SubCategoryId { get; set; }

        public string SubCategoryName { get; set; }

        public string RankName { get; set; }

        public int StyleId { get; set; }

        public string StyleName { get; set; }

        public int PlatformId { get; set; }

        public string PlatformName { get; set; }

        public int SubPlatformId { get; set; }

        public string SubPlatformName { get; set; }

        public string Tags { get; set; }

        public string AuthorName { get; set; }

        public int DownloadedByUsersCount { get; set; }

        [DataType(DataType.Upload)]
        [Required]
        public HttpPostedFileBase Uploaded3DModel { get; set; }

        [DataType(DataType.Upload)]
        [Required]
        public HttpPostedFileBase UploadedPreview { get; set; }

        //public string AuthorId { get; set; }

        //public virtual User Author { get; set; }

        //public virtual ICollection<User> DownloadedByUsers { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool PreserveCreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public IList<GroupedSelectListItem> SubCategories { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<PolyModel, FullPolyModelViewModel>()
                .ForMember(m => m.SubCategoryName, opt => opt.MapFrom(t => t.SubCategory.Name))
                .ForMember(m => m.CategoryName, opt => opt.MapFrom(t => t.SubCategory.Category.Name))
                .ForMember(m => m.SubPlatformName, opt => opt.MapFrom(t => t.SubPlatform.Name))
                .ForMember(m => m.PlatformName, opt => opt.MapFrom(t => t.SubPlatform.Platform.Name))
                .ForMember(m => m.StyleName, opt => opt.MapFrom(t => t.Style.Name))
                .ForMember(m => m.RankName, opt => opt.MapFrom(t => t.Rank.Name))
                .ForMember(m => m.AuthorName, opt => opt.MapFrom(t => t.Author.UserName))
                .ForMember(m => m.DownloadedByUsersCount, opt => opt.MapFrom(t => t.DownloadedByUsers.Count))
                .ReverseMap();
        }
    }
}