using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PocParalell.Domain;

namespace PocParalell.Infrastructure.Mappings
{
    public class PocExecucaoMapping : IEntityTypeConfiguration<PocExecucao>
    {
        public void Configure(EntityTypeBuilder<PocExecucao> builder)
        {
            builder.HasKey(a => a.Id);

            builder.ToTable("PocExecucao");

        }
    }
}
