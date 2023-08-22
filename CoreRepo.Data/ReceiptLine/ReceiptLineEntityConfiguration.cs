using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreRepo.Data.ReceiptLine;

public class ReceiptLineEntityConfiguration : IEntityTypeConfiguration<ReceiptLineEntity>
{
    public void Configure(EntityTypeBuilder<ReceiptLineEntity> builder)
    {
        builder.ToTable("ReceiptLines");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Amount).HasPrecision(18, 6);
        builder.HasMany(x => x.ReceiptLineTags)
            .WithOne()
            .HasForeignKey(x => x.ReceiptLineId);
    }
}