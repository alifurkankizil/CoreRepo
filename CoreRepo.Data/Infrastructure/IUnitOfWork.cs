using CoreRepo.Data.Receipt;

namespace CoreRepo.Data.Infrastructure;

public interface IUnitOfWork
{
    IReceiptRepository Receipt { get; }
    Task<int> SaveChangesAsync();
}