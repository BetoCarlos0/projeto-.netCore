using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SistemaChamados.Data.Identity;
using SistemaChamados.Models;

namespace SistemaChamados.Data
{
    public class SistemaDbContext : IdentityDbContext<UserCustom>
    {
        public SistemaDbContext(DbContextOptions<SistemaDbContext> options) : base(options) { }

        public DbSet<Calls> Calls { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
