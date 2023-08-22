using CoreRepo.Data.Receipt;

namespace CoreRepo.Data.Infrastructure;

public class UnitOfWork : IUnitOfWork
{

    public UnitOfWork(ICoreDbContext context, IReceiptRepository receipt)
    {
        Context = context;
        Receipt = receipt;
    }

    public ICoreDbContext Context { get; }
    public IReceiptRepository Receipt { get; }
    public Task<int> SaveChangesAsync()
    {
        return Context.SaveChangesAsync();
    }
}