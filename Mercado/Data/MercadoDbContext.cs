using Mercado.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Mercado.Data
{
    public class MercadoDbContext : IdentityDbContext<IdentityUser>
    {
        public MercadoDbContext(DbContextOptions<MercadoDbContext> options) : base(options)
        {

        }

        public DbSet<Fornecedor> Fornecedor { get; set; }
        public DbSet<Produto> Produto { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
