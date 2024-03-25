namespace HuutokauppaSivu.WebApi.Models;

/*
    A lookup table for categories
*/

public class CategoryLookup
{
    public int Id { get; set; }

    public int CategoryId { get; set; }

    public string Name { get; set; } = string.Empty;


}

