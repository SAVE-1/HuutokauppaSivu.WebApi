using System.Security.Cryptography;
using System.Text;

namespace MvcMovie.Models;

public static partial class SeedData
{
    private static IDictionary<string, string> generatedKeys = new Dictionary<string, string>();

    public static void Initialize(IServiceProvider serviceProvider)
    {
        SeedMagicalItems(serviceProvider);
        SeedCategoryLookup(serviceProvider);
        SeedItemCategories(serviceProvider);
    }

    private static string GetHash(string str)
    {
        string preHash = DateTime.Now.ToString("HH:mm:ss tt") + "funky extra hash string thing";
        MD5 sum = MD5.Create();
        return BitConverter.ToString(sum.ComputeHash(Encoding.ASCII.GetBytes(str + preHash))).Replace("-", "");
    }

}
