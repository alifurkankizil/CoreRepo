using Bogus;
using CoreRepo.Core.Enums;
using CoreRepo.Data.Receipt;
using CoreRepo.Data.ReceiptLine;
using CoreRepo.Data.ReceiptLineTag;

namespace CoreRepo.Data;

public static class FakeData
{
    public static List<ReceiptEntity> GetReceipts(int count)
    {
        
        var tags = new Faker<ReceiptLineTagEntity>()
            .RuleFor(o => o.Id, f => f.Random.Guid())
            .RuleFor(o => o.Key, f => f.Lorem.Word())
            .RuleFor(o => o.Value, f => f.Random.Words());

        var lines = new Faker<ReceiptLineEntity>()
            .RuleFor(o => o.Id, f => f.Random.Guid())
            .RuleFor(o => o.Amount, f => f.Finance.Amount())
            .RuleFor(o => o.AmountType, f => f.PickRandom<AmountType>())
            .RuleFor(o => o.CurrencyType, f => f.PickRandom<CurrencyType>())
            .RuleFor(o => o.ReceiptLineTags, f => tags.Generate(13));

        var receipt = new Faker<ReceiptEntity>()
            .RuleFor(o => o.Id, f => f.Random.Guid())
            .RuleFor(o=> o.Date,f=> f.Date.BetweenOffset(
                new DateTimeOffset(2023,1,1,0,0,0,TimeSpan.Zero),
                new DateTimeOffset(2023,12,31,23,59,59,TimeSpan.Zero)))
            .RuleFor(o=> o.PaymentType,f=> f.PickRandom<PaymentType>())
            .RuleFor(o => o.ReceiptLines, f => lines.Generate(5));

        return receipt.Generate(count);
    }
    
}