using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreRepo.Data.Receipt;

public class ReceiptEntityConfiguration : IEntityTypeConfiguration<ReceiptEntity>
{
    public void Configure(EntityTypeBuilder<ReceiptEntity> builder)
    {
        builder.ToTable("Receipts");

        builder.HasKey(x => x.Id);
        
        builder.HasMany(x => x.ReceiptLines)
            .WithOne()
            .HasForeignKey(x => x.ReceiptId);
    }
}