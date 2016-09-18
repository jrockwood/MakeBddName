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
        public static string ToBddName(string quotedString, char nonAlphaNumericReplacementChar = '_')
        {
            if (quotedString == null) { throw new ArgumentNullException(nameof(quotedString)); }

            // remove the whitespace and quotes from the beginning and the end
            string nonQuotedString = quotedString.Trim(' ', '\t', '\n', '\r', '\f', '"', '\'');
            var builder = new StringBuilder(nonQuotedString.Length);
            foreach (char c in nonQuotedString)
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
