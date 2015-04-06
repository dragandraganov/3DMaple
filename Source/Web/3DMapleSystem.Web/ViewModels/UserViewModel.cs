using AutoMapper;
using _3DMapleSystem.Data.Models;
using _3DMapleSystem.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _3DMapleSystem.Web.ViewModels.PolyModels;

namespace _3DMapleSystem.Web.ViewModels
{
    public class UserViewModel : IMapFrom<User>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public int? PhotoId { get; set; }

        public ICollection<SimplePolyModelViewModel> OwnPolyModels { get; set; }

        public ICollection<SimplePolyModelViewModel> DownloadedPolyModels { get; set; }

        //public virtual ICollection<Comment> Comments { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool PreserveCreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<User, UserViewModel>()
                .ReverseMap();
        }
    }
}