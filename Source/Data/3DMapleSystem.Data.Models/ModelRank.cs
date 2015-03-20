using _3DMapleSystem.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DMapleSystem.Data.Models
{
    public class ModelRank : IAuditInfo, IDeletableEntity
    {
        public ModelRank()
        {
            this.PolyModels=new HashSet<PolyModel>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool PreserveCreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<PolyModel> PolyModels { get; set; }
    }
}
