using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace _3DMapleSystem.Web.Areas.Administration.ViewModels
{
    public class PolyModelComplexViewModel
    {
        public PolyModelViewModel PolyModel { get; set; }

        public IList<GroupedSelectListItem> SubCategories { get; set; }

        public IList<GroupedSelectListItem> SubPlatforms { get; set; }

        public IList<SelectListItem> Styles { get; set; }

        public IList<SelectListItem> Ranks { get; set; }
    }
}