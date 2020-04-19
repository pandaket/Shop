using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using test.Models;

namespace test
{
    public class ShopContext: IdentityDbContext<Users, UserRole, int>
    {       
        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        {
            if (Database.CanConnect())
                Database.EnsureCreated();
        }
		protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Users>().ToTable("users").Property(p => p.Id).HasColumnName("uid");
            builder.Entity<Users>().Property(p => p.Email).HasColumnName("email");
            builder.Entity<Users>().Property(p => p.NormalizedEmail).HasColumnName("normalizedemail");
            builder.Entity<Users>().Property(p => p.UserName).HasColumnName("login");
            builder.Entity<Users>().Property(p => p.NormalizedUserName).HasColumnName("normalizedlogin");
           

            builder.Entity<Users>(Entity =>
            {
                Entity.Ignore(E => E.AccessFailedCount)
                    .Ignore(E => E.EmailConfirmed)
                    .Ignore(E => E.TwoFactorEnabled)
                    .Ignore(E => E.LockoutEnabled)
                    .Ignore(E => E.LockoutEnd)
                    .Ignore(E => E.ConcurrencyStamp)
                    .Ignore(E => E.PhoneNumber)
                    .Ignore(E => E.PhoneNumberConfirmed);

            });

        }
		
		public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Busket> Busket { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Productsize> Productsizes { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<OrderStatus> OrderStatus { get; set; }
    }
}
