using CoreRepo.Core.Enums;

namespace CoreRepo.Api.Models;

public class ReceiptLineModel
{
    public AmountType AmountType { get; set; }
    public decimal Amount { get; set; }
    public CurrencyType CurrencyType { get; set; }
    public ICollection<ReceiptLineTagModel> ReceiptLineTags { get; set; }
}