namespace Model.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Repo.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    internal sealed class Configuration : DbMigrationsConfiguration<Repo.Models.NoticeBoardContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Repo.Models.NoticeBoardContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            SeedRoles(context);
            SeedUser(context);
            //SeedAdvertisements(context);
            //SeedCaterogy(context);
            //SeedAdvertisement_Category(context);
            

        }
        private void SeedRoles(NoticeBoardContext context)
        {
            var roleManager = new RoleManager<Microsoft.AspNet.Identity.EntityFramework.IdentityRole>(new RoleStore<IdentityRole>());
            if (!roleManager.RoleExists("Admin"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";

                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("Moderator"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Moderator";

                roleManager.Create(role);
            }
        }
        private void SeedUser(NoticeBoardContext context)
        {
            var store = new UserStore<User>(context);
            var manager = new UserManager<User>(store);
            if(!context.User.Any(u => u.UserName == "Admin"))
            {
                var user = new User {
                    UserName = "admin@admin.pl",
                    Email = "admin@admin.pl",
                    FirsName = "Lukasz",
                    SecondName = "Wal"
                };

                var adminResult = manager.Create(user, "qwerty");
                if (adminResult.Succeeded)
                {
                    manager.AddToRole(user.Id, "Admin");
                }
            }

            if (!context.User.Any(u => u.UserName == "Lukasz"))
            {
                var user = new User
                {
                    UserName = "lukasz@lukasz.pl",
                    Email = "lukasz@lukasz.pl",
                    FirsName = "Lukasz",
                    SecondName = "Wal"
                };

                var adminResult = manager.Create(user, "qwerty");
                if (adminResult.Succeeded)
                {
                    manager.AddToRole(user.Id, "Moderator");
                }
            }

        }
        private void SeedAdvertisements(NoticeBoardContext context)
        {
            var userId = context.Set<User>().Where(u => u.UserName == "Admin").FirstOrDefault().Id;

            for (int i = 0; i < 25; i++)
            {
                var adv = new Advertisement()
                {
                    Id = i,
                    UserId = userId,
                    Description = "Tresc ogloszenia " + i.ToString(),
                    Title = "Tytu� og�oszenia " + i.ToString(),
                    Date = DateTime.Now.AddDays(-i)
                };
                context.Set<Advertisement>().AddOrUpdate(adv);
            }
            context.SaveChanges();
        }
        private void SeedCaterogy(NoticeBoardContext context)
        {
            for (int i = 0; i < 15; i++)
            {
                var cat = new Category()
                {
                    Id = i,
                    Name = "Kategoria " + i.ToString(),
                    ParentId = i
                };
                context.Set<Category>().AddOrUpdate(cat);
            }
            context.SaveChanges();
        }
        private void SeedAdvertisement_Category(NoticeBoardContext context)
        {
            for (int i = 0; i < 15; i++)
            {
                var advCat = new AdvertisementCategory()
                {
                    Id = i,
                    AdvertisementId = i + 1,
                    CategoryId = i + 2
                };
                context.Set<AdvertisementCategory>().AddOrUpdate(advCat);
            }
            context.SaveChanges();
        }
    }
}
