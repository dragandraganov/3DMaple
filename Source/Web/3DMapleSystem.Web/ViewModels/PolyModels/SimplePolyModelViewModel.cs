using _3DMapleSystem.Data.Models;
using _3DMapleSystem.Web.Infrastructure.Mapping;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _3DMapleSystem.Web.ViewModels.PolyModels
{
    public class SimplePolyModelViewModel : IMapFrom<PolyModel>, IHaveCustomMappings
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public int? PreviewId { get; set; }

        public string RankName { get; set; }

        public int CommentsCount { get; set; }

        public int DownloadedByUsersCount { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<PolyModel, SimplePolyModelViewModel>()
                .ForMember(m => m.RankName, opt => opt.MapFrom(t => t.Rank.Name))
                .ForMember(m => m.CommentsCount, opt => opt.MapFrom(t => t.Comments.Count))
                .ForMember(m => m.DownloadedByUsersCount, opt => opt.MapFrom(t => t.DownloadedByUsers.Count))
                .ReverseMap();
        }
    }
}