using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Huutokauppa_sivu.Server.Models;

public class MagicalItem
{
    [JsonIgnore]
    public int Id { get; set; }
    [Required]
    public int Price { get; set; }
    [Required]
    public string? Name { get; set; }
    public string? Description { get; set; } = null;
    [JsonIgnore]
    public bool IsPromoted { get; set; }
    public string? PromotionImage { get; set; }
    public string? DeleteIdentification { get; set; }
    [JsonIgnore]
    public string? CreatedBy { get; set; }
}

