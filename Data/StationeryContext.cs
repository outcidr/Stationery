using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Stationery.Models.Admin;
using Stationery.Models;


namespace Stationery.Data
{
    public class StationeryContext : DbContext
    {
        internal object User;

        public StationeryContext (DbContextOptions<StationeryContext> options)
            : base(options)
        {
        }

        public DbSet<Stationery.Models.Product> Product { get; set; } = default!;
        public DbSet<Stationery.Models.Category> Category { get; set; } = default!;
        public DbSet<Stationery.Models.Order> Order { get; set; } = default!;
        public DbSet<Stationery.Models.CartItem> CartItem { get; set; } = default!;
        public DbSet<Stationery.Models.User> Users { get; set; }
        public DbSet<Stationery.Models.Admin.AdminAccount> AdminAccounts { get; set; }
    }
}
