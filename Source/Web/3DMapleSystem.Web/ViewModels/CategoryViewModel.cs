using _3DMapleSystem.Data.Models;
using _3DMapleSystem.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _3DMapleSystem.Web.ViewModels
{
    public class CategoryViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<SubCategoryViewModel> SubCategories { get; set; }
    }
}