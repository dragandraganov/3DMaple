using _3DMapleSystem.Data.Common.Models;
using System;
using System.Linq;

namespace _3DMapleSystem.Data.Models
{
    public class SubPlatform : IAuditInfo, IDeletableEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool PreserveCreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public int PlatformId { get; set; }

        public virtual Category Platform { get; set; }
    }
}
