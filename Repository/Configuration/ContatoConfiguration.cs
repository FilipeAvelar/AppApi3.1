using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
    public class ContatoConfiguration : IEntityTypeConfiguration<Contato>
    {
        public void Configure(EntityTypeBuilder<Contato> builder)
        {
            builder.ToTable("Contatos");
            builder.HasKey(p => p.ID);
            builder.Property(p => p.Telefone).HasColumnType("VARCHAR(15)").IsRequired();
        }
    }
}
