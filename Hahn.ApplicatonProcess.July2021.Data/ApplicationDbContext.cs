using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hahn.ApplicatonProcess.July2021.Domain.Entities;
using Hahn.ApplicatonProcess.July2021.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Hahn.ApplicatonProcess.July2021.Data
{
    public class ApplicationDbContext : DbContext
    {
        readonly IStoreContext _storeContext;

        public ApplicationDbContext(IStoreContext storeContext, DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            _storeContext = storeContext;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Asset> Assets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany(s => s.Assets);
            modelBuilder.Entity<User>().HasKey(a => a.Id);
            modelBuilder.Entity<Asset>().HasKey(a => a.Id);

            //.NET5 feature to add detault QueryFilter
            modelBuilder.Entity<User>().HasQueryFilter(p => p.Status != Domain.Common.Status.Deleted);
            modelBuilder.Entity<Asset>().HasQueryFilter(p => p.Status != Domain.Common.Status.Deleted);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            BeforeSave();
            return base.SaveChangesAsync(cancellationToken);
        }
        private void BeforeSave()
        {
            try
            {
                var entities = ChangeTracker.Entries().Where(x => x.Entity is IBaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));
                foreach (var entity in entities)
                {
                    var now = DateTime.Now;
                    var baseEntity = (IBaseEntity)entity.Entity;
                    var user = _storeContext?.CurrentUser?.Email ?? "Unknown";

                    if (entity.State == EntityState.Added)
                    {
                        baseEntity.CreatedDate = now;
                        baseEntity.CreatedBy ??= user;
                    }
                    else if (entity.State == EntityState.Modified)
                    {
                        baseEntity.ModifiedDate = now;
                        baseEntity.ModifiedBy ??= user;
                    }
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
