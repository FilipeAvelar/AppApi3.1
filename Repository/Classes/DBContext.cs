using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository.Classes
{
    public class VettaDBContext : DbContext
    {
        public VettaDBContext(DbContextOptions<VettaDBContext> options) 
                : base(options)
        {

        }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Contato> Contatos { get; set; }
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<PessoaFisica> PessoaFisicas { get; set; }
        public DbSet<PessoaJuridica> PessoaJuridicas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configurar todas as classes que implementam: "IEntityTypeConfiguration" para não precisar adicionar classe a classe 
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(VettaDBContext).Assembly);
        }
    }
}
