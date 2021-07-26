namespace RepairWebPlatform.Infrastructure
{
    using System.Linq;
    using RepairWebPlatform.Data;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using RepairWebPlatform.Data.Models;


    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var data = scopedServices.ServiceProvider.GetService<RepairDbContext>();

            data.Database.Migrate();

            SeedCategories(data);

            return app;
        }

        private static void SeedCategories(RepairDbContext data)
        {
            if (data.Categories.Any())
            {
                return;
            }

            data.Categories.AddRange(new[]
            {
                
                new Category(){Name = "Home Appliances"},
                new Category(){Name = "House Work"},
                new Category(){Name = "Autos"},
                new Category(){Name = "Interior Decoration"},
                new Category(){Name = "TV,Computers and Phones"},
                new Category(){Name = "Watches and Jewelry"},
                new Category(){Name = "Others"},
                 
               
            });
            data.SaveChanges();
        }
    }
}
