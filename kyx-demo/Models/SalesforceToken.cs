using System.Text.Json.Serialization;

namespace kyx_demo.Models;

public class SalesforceToken
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }

    [JsonPropertyName("token_type")]
    public string TokenType { get; set; }
}
