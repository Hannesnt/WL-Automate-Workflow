using System.Text.Json.Serialization;

public class Case
{
    [JsonPropertyName("Id")]
    public string Id { get; set; }

    [JsonPropertyName("Subject")]
    public string Subject { get; set; }

    [JsonPropertyName("Description")]
    public string Description { get; set; }
}
