using Ecommerce.MVC.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.MVC.Data
{
    public class EcommerceDbContext : DbContext
    {
        public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options) : base(options)
        {
                
        }
        public DbSet<Address> Address { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<CartProduct> CartProduct { get; set; }
        public DbSet<Customer> Customer { get; set; }
        //public DbSet<CustomerReport> CustomerReport { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderCancel> OrderCancel { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Return> Return { get; set; }
    }
}
