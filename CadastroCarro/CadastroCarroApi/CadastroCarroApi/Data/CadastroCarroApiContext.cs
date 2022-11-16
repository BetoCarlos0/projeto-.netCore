using CadastroCarroApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CadastroCarroApi.Data
{
    public class CadastroCarroApiContext : DbContext
    {
        public CadastroCarroApiContext (DbContextOptions<CadastroCarroApiContext> options)
            : base(options)
        {
        }

        public DbSet<Carro> Carro { get; set; } = default!;
    }
}
