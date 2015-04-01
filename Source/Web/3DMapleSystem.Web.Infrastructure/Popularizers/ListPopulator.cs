using _3DMapleSystem.Data;
using _3DMapleSystem.Data.Models;
using _3DMapleSystem.Web.Infrastructure.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;

namespace _3DMapleSystem.Web.Infrastructure.Popularizers
{
    public class ListPopulator : IListPopulator
    {
        private I3DMapleSystemData data;
        private ICacheService cache;

        public ListPopulator(I3DMapleSystemData data, ICacheService cache)
        {
            this.data = data;
            this.cache = cache;
        }

        public IEnumerable<Category> GetCategories()
        {
            var categories = this.cache.Get<IEnumerable<Category>>("categories",
                () =>
                {
                    return this.data.Categories
                       .All()
                       .ToList();
                });

            return categories;
        }

        public IEnumerable<PolyModel> GetPolyModels()
        {
            var polyModels = this.cache.Get<IEnumerable<PolyModel>>("polyModels",
                () =>
                {
                    return this.data.PolyModels
                       .All()
                       .Where(pm => pm.IsApproved)
                       .ToList();
                });

            return polyModels;
        }

        public IEnumerable<AppFile> GetPreviews()
        {
            var previews = this.GetPolyModels().Select(pm => pm.Preview);
            return previews;
        }
    }
}
