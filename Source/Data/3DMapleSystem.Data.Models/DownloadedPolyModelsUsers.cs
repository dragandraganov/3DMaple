using _3DMapleSystem.Data.Common.Models;
using System;
using System.Linq;

namespace _3DMapleSystem.Data.Models
{
    public class DownloadedPolyModelsUsers : IAuditInfo, IDeletableEntity
    {
        public int Id { get; set; }

        public string PolyModelId { get; set; }

        public virtual PolyModel PolyModel { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public DateTime CreatedOn {get;set;}

        public bool PreserveCreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
