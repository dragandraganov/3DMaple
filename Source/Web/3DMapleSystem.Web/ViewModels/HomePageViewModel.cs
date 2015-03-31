using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _3DMapleSystem.Data.Models;
using _3DMapleSystem.Web.ViewModels.PolyModels;

namespace _3DMapleSystem.Web.ViewModels
{
    public class HomePageViewModel
    {

        public IList<CategoryViewModel> Categories { get; set; }

        public IList<SimplePolyModelViewModel> PolyModels { get; set; }
    }
}