using System.Security.Cryptography;
using System.Text;

namespace MvcMovie.Models;

public static partial class SeedData
{
    private static IDictionary<string, string> generatedKeys = new Dictionary<string, string>();

    public static void Initialize(IServiceProvider serviceProvider)
    {
        List<string> keys = new List<string>()
        {
            "rapier", "awp", "excal", "aghs"
        };

        // generate and store the keys so they can be easily referenced by other portions of the class
        GenerateKeys(keys);
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

    private static void GenerateKeys(List<string> names)
    {
        foreach (string name in names)
        {
            generatedKeys.Add(name, GetHash(name));
        }
    }

}
