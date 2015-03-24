using _3DMapleSystem.Data.Models;
using _3DMapleSystem.Web.Infrastructure.Mapping;
using AutoMapper;
using GridMvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace _3DMapleSystem.Web.Areas.Administration.ViewModels
{
    public class SubPlatformViewModel : IMapFrom<SubPlatform>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool PreserveCreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public int PlatformId { get; set; }

        [GridColumn(Title = "Platform")]
        public string PlatformName { get; set; }

        public IEnumerable<SelectListItem> Platforms { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<SubPlatform, SubPlatformViewModel>()
                .ForMember(m => m.PlatformName, opt => opt.MapFrom(t => t.Platform.Name))
                .ReverseMap();
        }
    }
}