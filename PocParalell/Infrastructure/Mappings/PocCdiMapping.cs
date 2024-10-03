using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PocParalell.Domain;

namespace PocParalell.Infrastructure.Mappings
{
    public class PocCdiMapping : IEntityTypeConfiguration<PocCdi>
    {
        public void Configure(EntityTypeBuilder<PocCdi> builder)
        {
            builder.HasKey(a => a.Id);

            builder.ToTable("PocCdi");

        }
    }
}
