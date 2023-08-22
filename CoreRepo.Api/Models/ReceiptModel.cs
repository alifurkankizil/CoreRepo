using CoreRepo.Core.Enums;

namespace CoreRepo.Api.Models;

public class ReceiptModel
{
    public PaymentType PaymentType { get; set; }
    public DateTimeOffset Date { get; set; }
    public ICollection<ReceiptLineModel> ReceiptLines { get; set; }
}