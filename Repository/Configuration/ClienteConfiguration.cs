using Domain.Enum;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Clientes");
            builder.HasKey(p => p.ID);
            builder.Property(p => p.Email).HasColumnType("VARCHAR(150)").IsRequired();
            builder.Property(p => p.CEP).HasColumnType("VARCHAR(8)");
            builder.Property(p => p.Classificacao).HasConversion(x => (int)x, x => (Classificacao)x);
            builder.HasMany(t => t.Contatos)
             .WithOne(x => x.Cliente)
             .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
