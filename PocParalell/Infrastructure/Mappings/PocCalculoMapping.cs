using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PocParalell.Domain;

namespace PocParalell.Infrastructure.Mappings
{
    public class PocCalculoMapping : IEntityTypeConfiguration<PocCalculo>
    {
        public void Configure(EntityTypeBuilder<PocCalculo> builder)
        {
            builder.HasKey(a => a.Id);

            builder.ToTable("PocCalculo");

        }
    }
}