using System.Text;

namespace System
{
    public static class StringExtensions
    {
		public static string ReplaceChar(this string @this, char newChar, int charIndex) 
		{
			var stringBuilder = new StringBuilder(@this);
			stringBuilder[charIndex] = newChar;
			return stringBuilder.ToString();
		}
    }
}