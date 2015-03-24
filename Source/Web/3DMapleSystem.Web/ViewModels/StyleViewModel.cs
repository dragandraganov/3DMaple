using _3DMapleSystem.Data.Models;
using _3DMapleSystem.Web.Infrastructure.Mapping;
using System;
using System.Linq;

namespace _3DMapleSystem.Web.ViewModels
{
    public class StyleViewModel : IMapFrom<Style>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}