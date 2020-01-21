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

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            HttpContextAccessor = httpContextAccessor;
        }

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

        private void AddFingerPrinting()
        {

        }
    }
}
