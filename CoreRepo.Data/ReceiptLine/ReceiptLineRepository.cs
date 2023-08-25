using CoreRepo.Core.Enums;
using TanvirArjel.EFCore.GenericRepository;

namespace CoreRepo.Data.ReceiptLine;

public class ReceiptLineRepository : IReceiptLineRepository
{
    public Task AddAsync(ReceiptLineEntity entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(ReceiptLineEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task<ReceiptLineEntity?> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<ReceiptLineEntity?> GetByIdDetail(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<PaginatedList<ReceiptLineEntity>> Search(CurrencyType currency)
    {
        throw new NotImplementedException();
    }
}