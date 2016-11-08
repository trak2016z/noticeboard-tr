using Repo.Models;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.IRepo
{
    public interface IAdvertisementContext
    {
        DbSet<Category> Category { get; set; }
        DbSet<Advertisement> Advertisement { get; set; }
        DbSet<User> User { get; set; }
        DbSet<AdvertisementCategory> AdvertisementCategory { get; set; }

        int SaveChanges();
        Database database { get; }
    }
}
