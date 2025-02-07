using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Stationery.Models;

namespace Stationery.Data
{
    public class StationeryContext : DbContext
    {
        public StationeryContext (DbContextOptions<StationeryContext> options)
            : base(options)
        {
        }

        public DbSet<Stationery.Models.Product> Product { get; set; } = default!;
        public DbSet<Stationery.Models.Category> Category { get; set; } = default!;
        public DbSet<Stationery.Models.Order> Order { get; set; } = default!;
        public DbSet<Stationery.Models.CartItem> CartItem { get; set; } = default!;
        public DbSet<Stationery.Models.User> User { get; set; } = default!;
        public DbSet<Stationery.Models.Admin> Admin { get; set; } = default!;
        public DbSet<Stationery.Models.RegularUser> RegularUser { get; set; } = default!;
    }
}
