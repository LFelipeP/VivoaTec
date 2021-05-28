using Microsoft.EntityFrameworkCore;

namespace VivoaTec.Models
{
    public class CadastroContext : DbContext
    {
        public CadastroContext(DbContextOptions<CadastroContext> options)
            : base(options)
        {
        }

        public DbSet<Cadastro> Cadastros { get; set; }

    }
}
