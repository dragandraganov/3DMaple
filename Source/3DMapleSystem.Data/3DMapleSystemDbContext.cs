using _3DMapleSystem.Data.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DMapleSystem.Data
{
    public class _3DMapleSystemDbContext : IdentityDbContext<User>
    {
        public _3DMapleSystemDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static _3DMapleSystemDbContext Create()
        {
            return new _3DMapleSystemDbContext();
        }
    }
}
