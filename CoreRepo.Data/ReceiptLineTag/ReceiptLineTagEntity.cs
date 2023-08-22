namespace CoreRepo.Data.ReceiptLineTag;

public class ReceiptLineTagEntity : BaseEntity
{
    public Guid ReceiptLineId { get; set; }
    public string Key { get; set; }
    public string Value { get; set; }
}