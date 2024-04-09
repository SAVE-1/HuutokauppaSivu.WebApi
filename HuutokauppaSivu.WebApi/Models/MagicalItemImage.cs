using System.Text.Json.Serialization;

namespace Huutokauppa_sivu.Server.Models;

public class MagicalItemImage
{
    [JsonIgnore]
    public int Id { get; set; }
    public string? DeleteIdentificationReference { get; set; }
    public string? Name { get; set; } = string.Empty;
}
