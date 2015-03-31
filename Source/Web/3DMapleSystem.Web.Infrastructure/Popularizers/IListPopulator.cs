using System;
using System.Collections.Generic;
using System.Linq;
using _3DMapleSystem.Data.Models;

namespace _3DMapleSystem.Web.Infrastructure.Popularizers
{
    public interface IListPopulator
    {
        IEnumerable<Category> GetCategories();

        IEnumerable<PolyModel> GetPolyModels();
    }
}
