using CoreRepo.Core.Enums;
using CoreRepo.Data.ReceiptLine;

namespace CoreRepo.Data.Receipt;

public class ReceiptEntity : BaseEntity
{
    public PaymentType PaymentType { get; set; }
    public DateTimeOffset Date { get; set; }
    
    public ICollection<ReceiptLineEntity> ReceiptLines { get; set; }
}