using _3DMapleSystem.Data.Common.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace _3DMapleSystem.Data.Models
{
    public class Style : IAuditInfo, IDeletableEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool PreserveCreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
