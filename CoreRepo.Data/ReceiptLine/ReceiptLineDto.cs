using CoreRepo.Core.Enums;
using CoreRepo.Data.ReceiptLineTag;

namespace CoreRepo.Data.ReceiptLine;

public class ReceiptLineDto : BaseEntity
{
    public Guid ReceiptId { get; set; }
    public AmountType AmountType { get; set; }
    public decimal Amount { get; set; }
    public CurrencyType CurrencyType { get; set; }
    public ICollection<ReceiptLineTagDto> ReceiptLineTags { get; set; }
}