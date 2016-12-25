using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.ModelConfiguration.Conventions;
using Repo.IRepo;
using System;

namespace Repo.Models
{
    // You can add profile data for the user by adding more properties to your User class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.

    public class NoticeBoardContext : IdentityDbContext, IAdvertisementContext
    {
        public NoticeBoardContext()
            : base("DefaultConnection")
        {
        }

        public static NoticeBoardContext Create()
        {
            return new NoticeBoardContext();
        }

        public DbSet<Category> Category { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<AdvertisementCategory> AdvertisementCategory { get; set; }
        public DbSet<AdvertisementImage> AdvertisementImage { get; set; }
        public DbSet<Advertisement> Advertisement { get; set; }

        public Database database { get; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>(); // plural name index of table id Db turn off
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>(); // global turn off CascadeDelete

            modelBuilder.Entity<Advertisement>().HasRequired(x => x.User).WithMany(x => x.Advertisements).HasForeignKey(x => x.UserId).WillCascadeOnDelete(true);
            //Fluent API, relation beetwen table in DB and turn on CascadeDelete in relation Advertisement - User
        }
    }
}