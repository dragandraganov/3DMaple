using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _3DMapleSystem.Web.ViewModels.ComplexModels
{
    public class CreateModelComplexViewModel
    {
        public FullPolyModelViewModel PolyModel { get; set; }

        public IList<GroupedSelectListItem> SubCategories { get; set; }

        public IList<GroupedSelectListItem> SubPlatforms { get; set; }

        public IList<SelectListItem> Styles { get; set; }

    }
}