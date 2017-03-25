// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="BddNamer.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All rights reserved. Licensed under the Apache License, Version 2.0.
//   See LICENSE in the project root for license information.
// </copyright>
// <created>2016-09-17</created>
// ---------------------------------------------------------------------------------------------------------------------

namespace MakeBddName
{
    using System;
    using System.Text;

    public static class BddNamer
    {
        /// <summary>
        /// Converts a string from sentence-style to a valid C# identifier with underscores between words.
        /// </summary>
        /// <param name="sentence">The sentence-style string to convert.</param>
        /// <returns></returns>
        public static string ToUnderscoreName(string sentence)
        {
            if (sentence == null) { throw new ArgumentNullException(nameof(sentence)); }

            // remove the whitespace and quotes from the beginning and the end
            string trimmed = sentence.Trim(' ', '\t', '\n', '\r', '\f', '"', '\'');
            var builder = new StringBuilder(trimmed.Length);
            foreach (char c in trimmed)
            {
                builder.Append(char.IsLetterOrDigit(c) ? c : '_');
            }

            // trim off extra underscores from the beginning and the end of the string
            string convertedString = builder.ToString();
            convertedString = convertedString.Trim('_');
            return convertedString;
        }
    }
}
