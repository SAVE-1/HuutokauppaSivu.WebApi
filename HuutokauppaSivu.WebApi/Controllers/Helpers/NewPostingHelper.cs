namespace HuutokauppaSivu.WebApi.Controllers.Helpers;

public record NewPostingHelper
{
    public int Price { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; } = null;
    public string? PromotionImage { get; set; }
    public List<string> Categories { get; init; }

#if DEBUG
    public IList<IFormFile>? File { get; set; } = default;
#else
    public IList<IFormFile> File { get; set; } = default;
#endif

}


