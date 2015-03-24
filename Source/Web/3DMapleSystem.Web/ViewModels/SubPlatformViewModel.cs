using _3DMapleSystem.Data.Models;
using _3DMapleSystem.Web.Infrastructure.Mapping;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _3DMapleSystem.Web.ViewModels
{
    public class SubPlatformViewModel : IMapFrom<SubPlatform>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool PreserveCreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public int PlatformId { get; set; }

        public string PlatformName { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<SubPlatform, SubPlatformViewModel>()
                .ForMember(m => m.PlatformName, opt => opt.MapFrom(sc => sc.Platform.Name))
                .ReverseMap();
        }
    }
}