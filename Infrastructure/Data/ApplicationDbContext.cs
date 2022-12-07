using Core.Entities;
using Core.Entities.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var entityTypes = Assembly
                .GetAssembly(typeof(Entity))
                .GetTypes()
                .Where(t => t.IsSubclassOf(typeof(Entity)));

            foreach (var entityType in entityTypes )
            {
                modelBuilder.Entity(entityType);
            }

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Entity).Assembly);
        }
    }
}
