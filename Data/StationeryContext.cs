using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Stationery.Models;

namespace Stationery.Data
{
    public class StationeryContext : IdentityDbContext<IdentityUser>
    {
        internal object User;

        public StationeryContext (DbContextOptions<StationeryContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Product { get; set; } = default!;
        public DbSet<Category> Category { get; set; } = default!;
        public DbSet<Order> Order { get; set; } = default!;
        public DbSet<CartItem> CartItem { get; set; } = default!;
        public DbSet<User> Users { get; set; }
    }
}
