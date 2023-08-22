using CoreRepo.Data.Receipt;
using CoreRepo.Data.ReceiptLine;
using CoreRepo.Data.ReceiptLineTag;
using Microsoft.EntityFrameworkCore;

namespace CoreRepo.Data;

public interface ICoreDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    public DbSet<ReceiptEntity> Receipts { get; set; }
    public DbSet<ReceiptLineEntity> ReceiptLines { get; set; }
    public DbSet<ReceiptLineTagEntity> ReceiptLineTags { get; set; }
}