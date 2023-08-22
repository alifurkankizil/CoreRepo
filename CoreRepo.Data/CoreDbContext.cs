using System.Reflection;
using CoreRepo.Data.Receipt;
using CoreRepo.Data.ReceiptLine;
using CoreRepo.Data.ReceiptLineTag;
using Microsoft.EntityFrameworkCore;

namespace CoreRepo.Data;

public class CoreDbContext : DbContext, ICoreDbContext
{
    public CoreDbContext(DbContextOptions<CoreDbContext> options) : base(options) {}

    public DbSet<ReceiptEntity> Receipts { get; set; }
    public DbSet<ReceiptLineEntity> ReceiptLines { get; set; }
    public DbSet<ReceiptLineTagEntity> ReceiptLineTags { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("core");
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}