
using AbySalto.Mid.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AbySalto.Mid.Infrastructure.Context
{
    public class DatabaseContext: IdentityDbContext<ApplicationUser, ApplicationRole, int, IdentityUserClaim<int>, ApplicationUserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>//DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        #region DbSets
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Product>  Products { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(e => e.UserRoles)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .IsRequired();

            modelBuilder.Entity<ApplicationRole>()
                .HasMany(e => e.UserRoles)
                .WithOne(e => e.Role)
                .HasForeignKey(e => e.RoleId)
                .IsRequired();
        }
    }
}
