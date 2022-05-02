using Microsoft.EntityFrameworkCore;
using ProjetoUsuarios.Models;

namespace ProjetoUsuarios.Data
{
    public class ProjetoUsuariosContext : DbContext
    {
        public ProjetoUsuariosContext (DbContextOptions<ProjetoUsuariosContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuario { get; set; }

        public DbSet<Endereco> Endereco { get; set; }
    }
}
