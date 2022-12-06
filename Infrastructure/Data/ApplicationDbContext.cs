using Core.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            Database.EnsureCreated();
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
