using CoreRepo.Core.Enums;
using TanvirArjel.EFCore.GenericRepository;

namespace CoreRepo.Data.ReceiptLine;

public interface IReceiptLineRepository
{
    Task AddAsync(ReceiptLineEntity entity);
    void Delete(ReceiptLineEntity entity);
    Task<ReceiptLineEntity?> GetById(Guid id);
    Task<ReceiptLineEntity?> GetByIdDetail(Guid id);
    Task<PaginatedList<ReceiptLineEntity>> Search(CurrencyType currency);
}