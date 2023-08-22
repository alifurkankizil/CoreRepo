using CoreRepo.Core.Enums;

namespace CoreRepo.Data.Models;

public class ReceiptSearchModel
{
    public PaymentType? PaymentType { get; set; }
    public DateTimeOffset? Date { get; set; }
    public CurrencyType? CurrencyType { get; set; }
    public string? Value { get; set; }

    //FromHeader
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
}

