using System.Globalization;
using System.Text;

namespace Inkukan.Application.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveNonAsciiCharacters(this string str, char replacementChar = '_')
        {
            string normalizedString = str.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new();

            foreach(char c in normalizedString)
            {
                UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (char.IsAscii(c) && unicodeCategory != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            }

            return sb.ToString()
                .Normalize(NormalizationForm.FormC);
        }
    }
}
