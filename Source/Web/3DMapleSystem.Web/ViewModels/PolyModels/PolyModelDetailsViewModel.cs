using _3DMapleSystem.Data.Models;
using _3DMapleSystem.Web.Areas.Administration.ViewModels;
using _3DMapleSystem.Web.Infrastructure.Mapping;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _3DMapleSystem.Web.ViewModels.PolyModels
{
    public class PolyModelDetailsViewModel : IMapFrom<PolyModel>, IHaveCustomMappings
    {
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public int SubCategoryId { get; set; }

        public string SubCategoryName { get; set; }

        public int? RankId { get; set; }

        public string RankName { get; set; }

        public int StyleId { get; set; }

        public string StyleName { get; set; }

        public int PlatformId { get; set; }

        public string PlatformName { get; set; }

        public int SubPlatformId { get; set; }

        public string SubPlatformName { get; set; }

        //public string TagsAsString { get; set; }

        public ICollection<string> Tags { get; set; }

        public string AuthorName { get; set; }

        public int AuthorPhotoId { get; set; }

        public bool IsApproved { get; set; }

        public int DownloadedByUsersCount { get; set; }

        public int? File3DModelId { get; set; }

        public int? PreviewId { get; set; }

        //[DataType(DataType.Upload)]
        //[Required]
        //public HttpPostedFileBase Uploaded3DModel { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool PreserveCreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public IList<GroupedSelectListItem> SubCategories { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<PolyModel, PolyModelDetailsViewModel>()
                .ForMember(m => m.SubCategoryName, opt => opt.MapFrom(t => t.SubCategory.Name))
                .ForMember(m => m.CategoryName, opt => opt.MapFrom(t => t.SubCategory.Category.Name))
                .ForMember(m => m.SubPlatformName, opt => opt.MapFrom(t => t.SubPlatform.Name))
                .ForMember(m => m.PlatformName, opt => opt.MapFrom(t => t.SubPlatform.Platform.Name))
                .ForMember(m => m.StyleName, opt => opt.MapFrom(t => t.Style.Name))
                .ForMember(m => m.RankName, opt => opt.MapFrom(t => t.Rank.Name))
                .ForMember(m => m.AuthorName, opt => opt.MapFrom(t => t.Author.UserName))
                .ForMember(m => m.DownloadedByUsersCount, opt => opt.MapFrom(t => t.DownloadedByUsers.Count))
                .ForMember(m => m.Tags, opt => opt.MapFrom(m => m.Tags.Select(t => t.Name)))
                .ForMember(m => m.AuthorPhotoId, opt => opt.MapFrom(m => m.Author.PhotoId))
                .ReverseMap();
        }
    }
}