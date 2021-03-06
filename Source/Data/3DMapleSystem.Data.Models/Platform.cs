﻿using _3DMapleSystem.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _3DMapleSystem.Data.Models
{
    public class Platform : IAuditInfo, IDeletableEntity
    {
        public Platform()
        {
            this.SubPlatforms = new HashSet<SubPlatform>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool PreserveCreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<SubPlatform> SubPlatforms { get; set; }
    }
}
