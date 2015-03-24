using System.Collections.Generic;
using _3DMapleSystem.Data.Common.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;

namespace _3DMapleSystem.Data.Models
{
    public class PolyModel : IAuditInfo, IDeletableEntity
    {
        public PolyModel()
        {
            this.Id = Guid.NewGuid();
            this.Tags=new HashSet<Tag>();
            this.Comments=new HashSet<Comment>();
            this.DownloadedByUsers=new HashSet<User>();
        }

        [Key]
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int? File3DModelId { get; set; }

        public virtual AppFile File3DModel { get; set; }

        public int? PreviewId { get; set; }

        public virtual AppFile Preview { get; set; }

        public int SubCategoryId { get; set; }

        public virtual SubCategory SubCategory { get; set; }

        public int StyleId { get; set; }

        public virtual Style Style { get; set; }

        public int RankId { get; set; }

        public virtual ModelRank Rank { get; set; }

        public int SubPlatformId { get; set; }

        public virtual SubPlatform SubPlatform { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public string AuthorId { get; set; }

        [InverseProperty("OwnPolyModels")]
        public virtual User Author { get; set; }

        public virtual ICollection<User> DownloadedByUsers { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool PreserveCreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
