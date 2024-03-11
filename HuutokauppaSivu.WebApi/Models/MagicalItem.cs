using System.Text.Json.Serialization;

namespace Huutokauppa_sivu.Server.Models;

public class MagicalItem
{
    public int Id { get; set; }
    public int Price { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; } = null;
    [JsonIgnore]
    public bool IsPromoted { get; set; }
    public string? PromotionImage { get; set; }
}

