using AutoMapper;
using _3DMapleSystem.Data.Models;
using _3DMapleSystem.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;
using _3DMapleSystem.Common;
using System.IO;
using System.Web.Mvc;

namespace _3DMapleSystem.Web.ViewModels.PolyModels
{
    public class FullPolyModelViewModel : IMapFrom<PolyModel>, IHaveCustomMappings, IValidatableObject
    {
        public Guid Id { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "The minimum length of title is 3 characters")]
        [MaxLength(20, ErrorMessage = "The maximum length of title is 20 characters")]
        [RegularExpression("([a-zA-Z0-9 ]+)", ErrorMessage = "The title must contain only letters and numbers")]
        public string Title { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "The minimum length of description is 3 characters")]
        [MaxLength(200, ErrorMessage = "The maximum length of description is 200 characters")]
        [AllowHtml]
        public string Description { get; set; }

        public decimal Price { get; set; }

        public int? PreviewId { get; set; }

        //public string ModelUrl { get; set; }

        //public virtual AppFile Preview { get; set; }

        public int SubCategoryId { get; set; }

        public string SubCategoryName { get; set; }

        public int? RankId { get; set; }

        public string RankName { get; set; }

        public int StyleId { get; set; }

        public string StyleName { get; set; }

        //public int PlatformId { get; set; }

        //public string PlatformName { get; set; }

        public int SubPlatformId { get; set; }

        public string SubPlatformName { get; set; }

        [MinLength(3, ErrorMessage = "The minimum length of tags is 3 characters")]
        [MaxLength(40, ErrorMessage = "The maximum length of tags is 40 characters")]
        [RegularExpression("([a-zA-Z0-9, ]+)", ErrorMessage = "The tags must contain only alphabets, numbers and comma separator")]
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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.UploadedPreview == null || this.UploadedPreview.ContentLength == 0)
            {
                yield return new ValidationResult("The preview is required.", new[] { "UploadedPreview" });
            }

            else if (!GlobalConstants.ValidPreviewTypes.Contains(this.UploadedPreview.ContentType))
            {
                yield return new ValidationResult("Please choose JPG/JPEG file for preview.", new[] { "UploadedPreview" });
            }

            else if (this.UploadedPreview.ContentLength > GlobalConstants.MaxPreviewFileLength)
            {
                yield return new ValidationResult("Preview file must be maximum 1MB", new[] { "UploadedPreview" });
            }

            if (Path.GetExtension(this.Uploaded3DModel.FileName).ToLower() != ".rar"
                && Path.GetExtension(this.Uploaded3DModel.FileName).ToLower() != ".zip")
            {
                yield return new ValidationResult("Incorrect model file format - must be .zip or .rar format", new[] { "Uploaded3DModel" });

            }
            else if (this.Uploaded3DModel.ContentLength > GlobalConstants.Max3DModelFileLength)
            {
                yield return new ValidationResult("3D Model file must be maximum 50MB", new[] { "Uploaded3DModel" });
            }
        }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<PolyModel, FullPolyModelViewModel>()
                .ForMember(m => m.SubCategoryName, opt => opt.MapFrom(t => t.SubCategory.Name))
                .ForMember(m => m.SubPlatformName, opt => opt.MapFrom(t => t.SubPlatform.Name))
                .ForMember(m => m.StyleName, opt => opt.MapFrom(t => t.Style.Name))
                .ForMember(m => m.RankName, opt => opt.MapFrom(t => t.Rank.Name))
                .ForMember(m => m.AuthorName, opt => opt.MapFrom(t => t.Author.UserName))
                .ForMember(m => m.DownloadedByUsersCount, opt => opt.MapFrom(t => t.DownloadedByUsers.Count))
                .ReverseMap();
        }
    }
}