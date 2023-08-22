using System.Text.Json.Serialization;

namespace CoreRepo.Data;

public abstract class BaseEntity
{
    [JsonPropertyOrder(int.MinValue)]
    public Guid Id { get; set; }
}