using System.Collections.Generic;
using _3DMapleSystem.Data.Common.Models;
using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _3DMapleSystem.Data.Models
{
    public class Tag  : IAuditInfo, IDeletableEntity
    {
        public Tag()
        {
            this.PolyModels=new HashSet<PolyModel>();
        }
        
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(450)]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool PreserveCreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public ICollection<PolyModel> PolyModels { get; set; }
    }
}
