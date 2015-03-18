using System.Collections.Generic;
using _3DMapleSystem.Data.Common.Models;
using System;
using System.Linq;

namespace _3DMapleSystem.Data.Models
{
    public class Category : IAuditInfo, IDeletableEntity
    {
        public Category()
        {
            this.SubCategories=new HashSet<SubCategory>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool PreserveCreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<SubCategory> SubCategories { get; set; }
    }
}
