using System;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SecretSanta.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Gift> Gift{get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Group> Group { get; set; }
        private IHttpContextAccessor HttpContextAccessor { get; }

#nullable disable
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            HttpContextAccessor = httpContextAccessor;
        }
#nullable enable
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            if(modelBuilder == null)
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }
            modelBuilder.Entity<UserGroupRelation>().HasKey(gur => new { gur.UserId, gur.GroupId});
            modelBuilder.Entity<UserGroupRelation>().HasOne(gur => gur.Group).WithMany(ur => ur.UserGroupRelations).HasForeignKey(gur => gur.UserId);
            modelBuilder.Entity<UserGroupRelation>().HasOne(gur => gur.User).WithMany(gu => gu.UserGroupRelations).HasForeignKey(gur => gur.GroupId);
        }

        public override int SaveChanges()
        {
            AddFingerPrinting();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            AddFingerPrinting();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void AddFingerPrinting()
        {
            var modified = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified);
            var added = ChangeTracker.Entries().Where(e => e.State == EntityState.Added);

            foreach(var entry in added)
            {
                var fingerPrintEntity = entry.Entity as FingerPrintyEntityBase;
                if (fingerPrintEntity != null)
                {
                    fingerPrintEntity.CreatedOn = DateTime.UtcNow;
                    fingerPrintEntity.CreatedBy = HttpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier).Value ?? "";
                    fingerPrintEntity.ModifiedOn = DateTime.UtcNow;
                    fingerPrintEntity.ModifiedBy = HttpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier).Value ?? "";
                }
            }

            foreach(var entry in modified)
            {
                var fingerPrintEntity = entry.Entity as FingerPrintyEntityBase;
                if (fingerPrintEntity != null)
                {
                    fingerPrintEntity.ModifiedOn = DateTime.UtcNow;
                    fingerPrintEntity.ModifiedBy = HttpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier).Value ?? "";
                }
            }
        }
    }
}
