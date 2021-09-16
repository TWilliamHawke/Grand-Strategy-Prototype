using System;
using System.Text;

namespace Helpers
{
    public static class StringExtension
    {
        /// <summary>Transform camelCase or PascalCase into the Capitalized Text</summary>
        public static string InsertSpaces(this String str)
        {
            
            StringBuilder builder = new StringBuilder();
            builder.Append(Char.ToUpper(str[0]));

            char prevChar = 'a';

            foreach (char c in str.Substring(1))
            {
                if (Char.IsUpper(c) && prevChar != ' ')
                {
                    builder.Append(' ');
                };
                builder.Append(c);
                prevChar = c;
            }

            return builder.ToString();
        }
    }
}