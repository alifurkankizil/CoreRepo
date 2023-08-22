using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreRepo.Data.ReceiptLineTag;

public class ReceiptLineTagEntityConfiguration : IEntityTypeConfiguration<ReceiptLineTagEntity>
{
    public void Configure(EntityTypeBuilder<ReceiptLineTagEntity> builder)
    {
        builder.ToTable("ReceiptLineTags");

        builder.HasKey(x => x.Id);
    }
}