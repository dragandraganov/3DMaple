using System;
using System.Data.Entity;
using System.Linq;

namespace _3DMapleSystem.Data
{
    public interface I3DMapleSystemData
    {
        DbContext Context { get; }

        //IDeletableEntityRepository<PartyGame> PartyGames { get; }

        //IDeletableEntityRepository<Category> Categories { get; }

        //IDeletableEntityRepository<AppFile> Images { get; }

        //IDeletableEntityRepository<User> Users { get; }

        //IDeletableEntityRepository<Rating> Ratings { get; }

        //IDeletableEntityRepository<Comment> Comments { get; }

        //IDeletableEntityRepository<Like> Likes { get; }

        void Dispose();

        int SaveChanges();
    }
}
