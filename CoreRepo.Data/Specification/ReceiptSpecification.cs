using CoreRepo.Data.Models;
using CoreRepo.Data.Receipt;
using TanvirArjel.EFCore.GenericRepository;

namespace CoreRepo.Data.Specification;

public static class ReceiptSpecification
{
    public static PaginationSpecification<ReceiptEntity> SearchSpecification(ReceiptSearchModel searchModel)
    {
        //Note : Constructor
        var specification = new PaginationSpecification<ReceiptEntity>
        {
            PageIndex = searchModel.PageIndex,
            PageSize = searchModel.PageSize
        };
        
        if(searchModel.PaymentType.HasValue)
            specification.Conditions.Add(x=> x.PaymentType == searchModel.PaymentType);

        if (searchModel.CurrencyType.HasValue)
            specification
                .Conditions
                .Add(x => x.ReceiptLines.Any(y => y.CurrencyType == searchModel.CurrencyType));

        if (searchModel.Date.HasValue)
            specification.Conditions.Add(x => x.Date <= searchModel.Date);
        
        if(searchModel.Value is not null)
            specification
                .Conditions
                .Add(x=>x.ReceiptLines.Any(y=> y.ReceiptLineTags.Any(z=> z.Value == searchModel.Value)));

        return specification;
    }
}