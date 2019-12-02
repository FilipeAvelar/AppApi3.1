using System.Text.RegularExpressions;

namespace Domain.DomainService.Utils
{
    public static class StringExtensions
    {
        public static string SomenteNumeros(this string texto)
        {
            Regex digitsOnly = new Regex(@"[^\d]");
            return digitsOnly.Replace(texto, string.Empty);
        }
    }
}
