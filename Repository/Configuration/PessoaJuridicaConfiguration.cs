using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
    public class PessoaJuridicaConfiguration : IEntityTypeConfiguration<PessoaJuridica>
    {
        public void Configure(EntityTypeBuilder<PessoaJuridica> builder)
        {
            builder.ToTable("PessoasJuridicas");
            builder.Property(t => t.CNPJ).HasColumnType("VARCHAR(15)");
            builder.Property(t => t.RazaoSocial).HasColumnType("VARCHAR(100)");
        }
    }
}
