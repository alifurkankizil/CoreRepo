using CoreRepo.Data.Models;
using TanvirArjel.EFCore.GenericRepository;

namespace CoreRepo.Data.Receipt;

public interface IReceiptRepository
{
    Task AddAsync(ReceiptEntity entity);
    void Delete(ReceiptEntity entity);
    Task<ReceiptEntity?> GetById(Guid id);
    Task<ReceiptEntity?> GetByIdDetail(Guid id);

    Task<ReceiptEntity?> GetByIdV3(Guid id, QueryModel<ReceiptEntity>? queryModel);
    Task<PaginatedList<ReceiptEntity>> Search(ReceiptSearchModel searchModel);
    Task<decimal> TotalAmountById(Guid id);
}