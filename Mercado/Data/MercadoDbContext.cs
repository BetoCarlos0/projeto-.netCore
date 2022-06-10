using Mercado.Data.Identity;
using Mercado.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Mercado.Data
{
    public class MercadoDbContext : IdentityDbContext<UserCustom>
    {
        public MercadoDbContext(DbContextOptions<MercadoDbContext> options) : base(options)
        {

        }

        public DbSet<Provider> Provider { get; set; }
        public DbSet<Product> Product { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
