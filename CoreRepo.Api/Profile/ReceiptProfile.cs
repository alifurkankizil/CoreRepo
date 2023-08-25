using CoreRepo.Api.Models;
using CoreRepo.Data.Receipt;
using CoreRepo.Data.ReceiptLine;
using CoreRepo.Data.ReceiptLineTag;

namespace CoreRepo.Api.Profile;

public class ReceiptProfile : AutoMapper.Profile
{
    public ReceiptProfile()
    {
        CreateMap<ReceiptModel, ReceiptEntity>();
        CreateMap<ReceiptLineModel, ReceiptLineEntity>();
        CreateMap<ReceiptLineTagModel, ReceiptLineTagEntity>();
        CreateMap<ReceiptEntity, ReceiptResponse>();
    }
}