namespace HuutokauppaSivu.WebApi.Models;

public class ItemCategories
{
    // running id
    public int Id { get; set; }

    // lookup id in CategoryLookup
    public int CategoryId { get; set; }

    // id in reference bid item
    public string DeleteIdentification { get; set; } = string.Empty;

}


