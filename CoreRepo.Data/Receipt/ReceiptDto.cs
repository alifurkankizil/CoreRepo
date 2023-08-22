using CoreRepo.Data.ReceiptLine;

namespace CoreRepo.Data.Receipt;

public class ReceiptDto : BaseEntity
{
    public ICollection<ReceiptLineDto> ReceiptLines { get; set; }
}