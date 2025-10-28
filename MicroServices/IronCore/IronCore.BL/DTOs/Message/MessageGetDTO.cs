using System.Text.Json.Serialization;

namespace IronCore.BL.DTOs.Message;
public record MessageGetDTO
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("content")]
    public string? Content { get; set; }

    [JsonPropertyName("storageId")]
    public string? StorageId { get; set; }

    [JsonPropertyName("isRead")]
    public bool IsRead { get; set; }

    [JsonPropertyName("sendedById")]
    public string SendedById { get; set; }
}