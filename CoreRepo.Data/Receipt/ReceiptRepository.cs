using CoreRepo.Core.Enums;
using CoreRepo.Data.Models;
using Microsoft.EntityFrameworkCore;
using TanvirArjel.EFCore.GenericRepository;

namespace CoreRepo.Data.Receipt;

public class ReceiptRepository : IReceiptRepository
{
    public ReceiptRepository(ICoreDbContext coreDbContext)
    {
        CoreDbContext = coreDbContext;
    }

    private ICoreDbContext CoreDbContext { get; }


    public async Task AddAsync(ReceiptEntity entity)
    {
        await CoreDbContext.Receipts.AddAsync(entity);
    }

    public void Delete(ReceiptEntity entity)
    { 
        CoreDbContext.Receipts.Remove(entity);
    }

    public async Task<ReceiptEntity?> GetById(Guid id)
    {
        return await CoreDbContext
            .Receipts
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<ReceiptEntity?> GetByIdDetail(Guid id)
    {
        return await CoreDbContext
            .Receipts
            .Include(x => x.ReceiptLines)
            .ThenInclude(x => x.ReceiptLineTags)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
    
    public async Task<ReceiptEntity?> GetByIdV3(Guid id, QueryModel<ReceiptEntity>? queryModel)
    {
        var query = CoreDbContext
            .Receipts
            .AsQueryable();
        
        if(queryModel is not null)
            query = queryModel.Includes(query);

        return await query.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<PaginatedList<ReceiptEntity>> Search(ReceiptSearchModel searchModel)
    {
        var query = CoreDbContext
            .Receipts
            .Include(x => x.ReceiptLines)
            .ThenInclude(x => x.ReceiptLineTags)
            .AsQueryable();

        if (searchModel.PaymentType.HasValue)
            query = query.Where(x => x.PaymentType == PaymentType.Cash);

        if (searchModel.CurrencyType.HasValue)
            query = query.Where(x => x.ReceiptLines.Any(y => y.CurrencyType == searchModel.CurrencyType));

        if (searchModel.Date.HasValue)
            query = query.Where(x => x.Date <= searchModel.Date);

        if (searchModel.Value is not null)
            query = query.Where(x => x.ReceiptLines.Any(y => y.ReceiptLineTags.Any(z => z.Value == searchModel.Value)));

        return new PaginatedList<ReceiptEntity>(
            await query.Skip((searchModel.PageIndex - 1) * searchModel.PageSize).Take(searchModel.PageSize).ToListAsync(),
            await query.CountAsync(),
            searchModel.PageIndex,
            searchModel.PageSize);
    }

    public async Task<decimal> TotalAmountById(Guid id)
    {
        var receipt = CoreDbContext
            .Receipts
            .Include(x => x.ReceiptLines)
            .Where(x => x.Id == id);
        
        return await receipt
            .SelectMany(x=> x.ReceiptLines)
            .SumAsync(x => x.AmountType == AmountType.Credit
                ? x.Amount
                : -x.Amount);
    }
}