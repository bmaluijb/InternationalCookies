using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternationalCookies.Data
{
    public class CookieContext : DbContext
    {

        public CookieContext(DbContextOptions options) : base (options)
        {
            
        }



        public CookieContext()
        {
           
                }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<Cookie> Cookies { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<DatabaseServers> DatabaseServers { get; set; }

    }
}
