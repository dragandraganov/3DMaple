using System;
using System.Collections.Generic;
using System.Linq;
using _3DMapleSystem.Data.Models;
using System.Web.Mvc;

namespace _3DMapleSystem.Web.Infrastructure.Popularizers
{
    public interface IListPopulizer
    {
        IEnumerable<Category> GetCategories();

        IEnumerable<PolyModel> GetPolyModels();

        IEnumerable<AppFile> GetPreviews();

        IEnumerable<Tag> GetTags();

    }
}
