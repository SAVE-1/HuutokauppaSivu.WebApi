namespace HuutokauppaSivu.WebApi.Models;

/*
    A lookup table for categories
*/

public class CategoryLookup
{
    public int Id { get; set; }

    public int CategoryId { get; set; }

    /*

    This should allow subcategories, for example in this order:
        Top
            Sub
                Sub
                    Sub
                        Sub
            Sub
            Sub
        Top
        Top
        Top
        Top
            Sub

    */
    public int SubCategoryId { get; set; }

    public string Name { get; set; } = string.Empty;


}

