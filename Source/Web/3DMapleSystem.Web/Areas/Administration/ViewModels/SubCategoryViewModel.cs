﻿using AutoMapper;
using _3DMapleSystem.Data.Models;
using _3DMapleSystem.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GridMvc.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace _3DMapleSystem.Web.Areas.Administration.ViewModels
{
    public class SubCategoryViewModel : IMapFrom<SubCategory>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool PreserveCreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public int CategoryId { get; set; }

        [GridColumn(Title = "Category")]
        public string CategoryName { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<SubCategory, SubCategoryViewModel>()
                .ForMember(m => m.CategoryName, opt => opt.MapFrom(t => t.Category.Name))
                .ReverseMap();
        }
    }
}