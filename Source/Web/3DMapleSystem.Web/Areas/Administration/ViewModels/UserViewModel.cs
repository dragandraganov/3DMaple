using AutoMapper;
using _3DMapleSystem.Data.Models;
using _3DMapleSystem.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _3DMapleSystem.Web.Areas.Administration.ViewModels
{
    public class UserViewModel : IMapFrom<User>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public int? PhotoId { get; set; }

        public AppFile Photo { get; set; }

        public ICollection<PolyModel> OwnPolyModels { get; set; }

        public ICollection<DownloadedPolyModelsUsers> DownloadedPolyModels { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public int AvailableFreeModels { get; set; }

        public int AvailableProModels { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool PreserveCreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<User, UserViewModel>().ReverseMap();
        }
    }
}