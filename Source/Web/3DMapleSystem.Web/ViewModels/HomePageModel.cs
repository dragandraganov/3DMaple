using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _3DMapleSystem.Data.Models;

namespace _3DMapleSystem.Web.ViewModels
{
    public class HomePageModel
    {

        public IList<Category> Categories { get; set; }

        public IList<PolyModel> PolyModels { get; set; }
    }
}