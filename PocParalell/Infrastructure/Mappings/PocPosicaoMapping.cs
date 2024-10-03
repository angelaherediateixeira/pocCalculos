using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PocParalell.Domain;

namespace PocParalell.Infrastructure.Mappings
{
    public class PocPosicaoMapping : IEntityTypeConfiguration<PocPosicao>
    {
        public void Configure(EntityTypeBuilder<PocPosicao> builder)
        {
            builder.HasKey(a => a.Id);

            builder.ToTable("PocPosicao");

        }
    }
}
