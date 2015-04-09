using _3DMapleSystem.Common;
using _3DMapleSystem.Data.Common.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace _3DMapleSystem.Data.Models
{
    public class User : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public User()
        {
            this.CreatedOn = DateTime.Now;
            this.PreserveCreatedOn = true;
            this.OwnPolyModels = new HashSet<PolyModel>();
            this.DownloadedPolyModels = new HashSet<DownloadedPolyModelsUsers>();
            this.Comments=new HashSet<Comment>();
        }

        public int? PhotoId { get; set; }

        public virtual AppFile Photo { get; set; }

        public virtual ICollection<PolyModel> OwnPolyModels { get; set; }

        public virtual ICollection<DownloadedPolyModelsUsers> DownloadedPolyModels { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public DateTime CreatedOn {get;set;}

        public bool PreserveCreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
