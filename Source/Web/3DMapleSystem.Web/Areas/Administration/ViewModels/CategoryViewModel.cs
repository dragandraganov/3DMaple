using _3DMapleSystem.Data.Models;
using _3DMapleSystem.Web.Infrastructure.Mapping;
using AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace _3DMapleSystem.Web.Areas.Administration.ViewModels
{
    public class CategoryViewModel : IMapFrom<Category>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool PreserveCreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Category, CategoryViewModel>()
                .ReverseMap();
        }
    }
}