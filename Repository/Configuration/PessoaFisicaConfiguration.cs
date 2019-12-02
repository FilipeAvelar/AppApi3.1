using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
    public class PessoaFisicaConfiguration : IEntityTypeConfiguration<PessoaFisica>
    {
        public void Configure(EntityTypeBuilder<PessoaFisica> builder)
        {
            builder.ToTable("PessoasFisicas");
            builder.Property(t => t.CPF).HasColumnType("VARCHAR(11)");
            builder.Property(t => t.NomeCompleto).HasColumnType("VARCHAR(100)");
        }
    }
}
