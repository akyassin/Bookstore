using DataAccess.Data;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bookstore.Extensions
{
    public static class ExtensionMethods
    {
        public static IHost MigrateAndTryToSeed(this IHost webHost)
        {
            using var scope = webHost.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<BookstoreDbContext>();
            db.Database.Migrate();
            //db.TryToSeedData(); //This option has beed replaced by OnModelCreating method ceeding
            return webHost;
        }

        private static void TryToSeedData(this BookstoreDbContext db)
        {
            if (!db.Authors.Any())
            {
                db.Authors.AddRange(
                new Author() { FullName = "Ahmad Yassin" },
                new Author() { FullName = "Mohammad Abdulaal" },
                new Author() { FullName = "Yassin Darwish" },
                new Author() { FullName = "Yousef Sabri" },
                new Author() { FullName = "Mikael Bros" });
                db.SaveChanges();

            }
            if (!db.Books.Any())
            {
                db.Books.AddRange(
                new Book() { Title = "C# For Programming", Description = "No Desc", ImageName = "", ImageUrl = "", Author = db.Authors.FirstOrDefault(a => a.FullName == "Ahmad Yassin") },
                new Book() { Title = "Java For Programming", Description = "No Desc", ImageName = "", ImageUrl = "", Author = db.Authors.FirstOrDefault(a => a.FullName == "Mohammad Abdulaal") },
                new Book() { Title = "Python For Programming", Description = "No Desc", ImageName = "", ImageUrl = "", Author = db.Authors.FirstOrDefault(a => a.FullName == "Yassin Darwish") },
                new Book() { Title = "PHP For Programming", Description = "No Desc", ImageName = "", ImageUrl = "", Author = db.Authors.FirstOrDefault(a => a.FullName == "Yousef Sabri") },
                new Book() { Title = "Clean Code", Description = "No Desc", ImageName = "", ImageUrl = "", Author = db.Authors.FirstOrDefault(a => a.FullName == "Mikael Bros") });
                db.SaveChanges();
            }
        }

    }
}
