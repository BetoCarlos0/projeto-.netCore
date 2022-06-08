using Mercado.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Mercado.Data
{
    public class MercadoDbContext : IdentityDbContext
    {
        public MercadoDbContext(DbContextOptions<MercadoDbContext> options) : base(options)
        {

        }

        public DbSet<Usuarios> Usuarios { get; set; }
    }
}
