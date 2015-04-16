using _3DMapleSystem.Data.Models;
using _3DMapleSystem.Web.Infrastructure.Mapping;
using AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace _3DMapleSystem.Web.ViewModels
{
    public class CommentViewModel : IMapFrom<Comment>, IHaveCustomMappings
    {

        public CommentViewModel()
        {
        }

        public CommentViewModel(int id)
        {

        }

        public CommentViewModel(Guid id)
        {
            this.PolyModelId = id;
        }

        public int Id { get; set; }

        [MinLength(3, ErrorMessage = "The comment text must be at least 3 characters")]
        [Required(ErrorMessage = "The comment text must be at least 3 characters")]
        [AllowHtml]
        public string Content { get; set; }

        public Guid PolyModelId { get; set; }

        public string AuthorId { get; set; }

        public string AuthorName { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Comment, CommentViewModel>()
               .ForMember(cm => cm.AuthorName, opt => opt.MapFrom(c => c.Author.UserName))
               .ReverseMap();
        }
    }
}