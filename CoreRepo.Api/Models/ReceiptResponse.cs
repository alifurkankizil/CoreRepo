using CoreRepo.Core.Enums;

namespace CoreRepo.Api.Models;

public class ReceiptResponse
{
    public Guid Id { get; set; }
    public PaymentType PaymentType { get; set; }
    public DateTimeOffset Date { get; set; }
}