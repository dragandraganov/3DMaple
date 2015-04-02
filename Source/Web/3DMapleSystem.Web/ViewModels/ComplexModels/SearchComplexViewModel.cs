using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _3DMapleSystem.Web.ViewModels.PolyModels;

namespace _3DMapleSystem.Web.ViewModels.ComplexModels
{
    public class SearchComplexViewModel
    {
        public IEnumerable<SimplePolyModelViewModel> PolyModels { get; set; }

        public IEnumerable<SubCategoryViewModel> SubCategories { get; set; }
    }
}