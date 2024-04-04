using System.Security.Cryptography;
using System.Text;

namespace HuutokauppaSivu.WebApi.Models;

public static class DataHelper
{
    public static string GetHash(string str)
    {
        string preHash = DateTime.Now.ToString("HH:mm:ss tt") + "funky extra hash string thing";
        MD5 sum = MD5.Create();
        return BitConverter.ToString(sum.ComputeHash(Encoding.ASCII.GetBytes(str + preHash))).Replace("-", "");
    }
}
